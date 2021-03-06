SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddDonation2]
    @idUser     [int],
    @amount     [decimal](18,4)
AS
BEGIN
    declare @idVariabilny int;
    select @idVariabilny = [IdVariabilny] 
    from [dbo].[registracia]
    where [Id] = @idUser;

    update [dbo].[receivable] set
        [donation] = [donation] + (case when [idCurrency] = 2 then @amount * 27.61 else @amount end),
        [dtModified] = getutcdate()
    where [id] = @idVariabilny;
END
GO
