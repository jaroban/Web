SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetList]
    @name           [nvarchar](max),
    @idChurch       [int],
    @from           [nvarchar](max),
    @to             [nvarchar](max),
    @notArrived     [bit]
AS
BEGIN
    with paid as
    (
        select [idVariabilny], SUM(case when [idCurrency] = 2 then [amount] / 27.61 else [amount] end) as [sum]
        from [dbo].[payment]
        group by [idVariabilny]
    ),
    owed as
    (
        select [idVariabilny], SUM([Suma]) as [sum]
        from [dbo].[registracia]
        group by [idVariabilny]
    ),
    donation as
    (
        select [id] as [idVariabilny], SUM(case when [idCurrency] = 2 then [donation] / 27.61 else [donation] end) as [sum]
        from [dbo].[receivable]
        group by [id]
    )
    select 
        r.[Id],
        r.[IdVariabilny],
        [Meno],
        [Priezvisko],
        coalesce(z.[name], r.[InyZbor]) as church,
        t.[name],
        --[DtPlatba], coalesce(p.[sum], 0) as paid, coalesce(o.[sum], 0) as owed, coalesce(d.[sum], 0) as donated,
        (coalesce(p.[sum], 0) - coalesce(o.[sum], 0) - coalesce(d.[sum], 0)) as diff,
        [Prisli]
    from [dbo].[registracia] r
    left join [dbo].[zbor]   z on z.[id] = r.[IdZbor]
    left join [dbo].[tricko] t on t.[id] = r.[IdTricko]
    left join paid           p on p.[idVariabilny] = r.[IdVariabilny]
    left join owed           o on o.[idVariabilny] = r.[IdVariabilny]
    left join donation       d on d.[idVariabilny] = r.[IdVariabilny]
    where 
        (@name is null or @name = '' or [Meno] + ' ' + [Priezvisko] like '%' + @name + '%' or [Email] like '%' + @name + '%') and
        (@idChurch is null or @idChurch = 0 or [IdZbor] = @idChurch) and
        (@from is null or @from = '' or @from <= left([Priezvisko], 1)) and
        (@to is null or @to = '' or left([Priezvisko], 1) <= @to) and
        (@notArrived is null or @notArrived = 0 or [Prisli] = 0)
    order by [Priezvisko] asc, [Meno] asc;
END
GO
