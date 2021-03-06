SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ImportToPayment]
AS
    insert into [dbo].[payment] 
        ([idVariabilny], [note], [payer], [idCurrency], [amount], [dtModified])
    select 
        v.[id], [description], [payer], 1, i.[amount], convert(date, [date1], 105)
    from [dbo].[import] i
    inner join [dbo].[variabilny] v on v.[vs] = i.[vs];

    delete i
    from [dbo].[import] i
    left join [dbo].[variabilny] v on v.[vs] = i.[vs]
    where v.[id] is not null;
    
    -- datum platby
    with firstPayment as
    (
        select [idVariabilny], min([dtModified]) as [dtModified]
        from [dbo].[payment]
        group by [idVariabilny]
    )
    update r set DtPlatba = p.[dtModified]
    -- select *
    from [dbo].[registracia] r
    inner join firstPayment p on p.[idVariabilny] = r.[IdVariabilny];
GO
