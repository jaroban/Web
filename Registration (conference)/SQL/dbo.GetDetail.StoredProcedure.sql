SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDetail]
    @idUser [int]
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
    select [Id],[Meno],[Priezvisko],[Email],[Telefon],[IdZbor],[InyZbor],[PiatokVecera],[PiatokVecera2],[UbytovaniePiatokSobota],[TichaTriedaPiatokSobota],
        [SobotaRanajky],[SobotaObed],[SobotaVecera],[SobotaVecera2],[UbytovanieSobotaNedela],[TichaTriedaSobotaNedela],[NedelaRanajky],[NedelaObed],[Sach],[PingPong],
        [IdTricko],
        cast((case when [Sluziaci] = 'Dobrovolnik' then 1 else 0 end) as bit),
        [Poznamka],[DtModified],[Internat1],[Internat2],[Preplacame],[InternatZadarmo],[RegistraciaZadarmo],[JedloZadarmo],[DtPlatba],
        [Prisli],[Suma],[IstoPride],
        coalesce(p.[sum], 0),
        coalesce(o.[sum], 0),
        coalesce(d.[sum], 0),
        (coalesce(p.[sum], 0) - coalesce(o.[sum], 0) - coalesce(d.[sum], 0))
    from [dbo].[registracia] r
    left join paid           p on p.[idVariabilny] = r.[IdVariabilny]
    left join owed           o on o.[idVariabilny] = r.[IdVariabilny]
    left join donation       d on d.[idVariabilny] = r.[IdVariabilny]
    where r.[Id] = @idUser;
    
    declare @idVariabilny int;
    select @idVariabilny = [IdVariabilny] 
    from [dbo].[registracia]
    where [Id] = @idUser;
    
    select [Id], [Meno] + ' ' + [Priezvisko]
    from [dbo].[registracia]
    where [Id] <> @idUser and [IdVariabilny] = @idVariabilny;
END
GO
