SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPayment2]
    @idUser     [int],
    @amount     [decimal](18,4),
    @note       [nvarchar](max)
AS
BEGIN
    declare @idVariabilny int;
    select @idVariabilny = [IdVariabilny] 
    from [dbo].[registracia]
    where [Id] = @idUser;
    
    insert into [dbo].[payment] 
        ([idVariabilny], [note], [payer], [idCurrency], [amount], [dtModified])
    select 
        @idVariabilny, @note, [Meno] + ' ' + [Priezvisko], 1, @amount, getutcdate()
    from [dbo].[registracia]
    where [id] = @idUser;

    update r set [DtPlatba] = coalesce([DtPlatba], getutcdate())
    from [dbo].[registracia] r
    where [Id] = @idUser;
END
GO
