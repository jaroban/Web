SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUsers4]
    @users          [dbo].[ListOfUsers] readonly,
    @emails         [dbo].[ListOfEmails] readonly,
    @payerEmail     [nvarchar](max),
    @amount         [decimal](18,4),
    @donation       [decimal](18,4),
    @idCurrency     [int],
    @needHelp       [bit]
AS
BEGIN
    declare @now datetime = getutcdate();
    
    if exists(
        select * 
        from [dbo].[registracia] o
        inner join @users n on lower(ltrim(rtrim(o.[Email]))) = lower(ltrim(rtrim(n.[Email]))))
    begin
        select cast(0 as int);
        
        select n.[Email]
        from [dbo].[registracia] o
        inner join @users n on lower(ltrim(rtrim(o.[Email]))) = lower(ltrim(rtrim(n.[Email])));
    end
    else
    begin
        select cast(1 as int);

        insert into [dbo].[receivable]
            ([email], [amount], [donation], [idCurrency], [needHelp], [dtModified])
        values
            (@payerEmail, @amount, @donation, @idCurrency, @needHelp, @now);
        
        declare @idVariabilny int = scope_identity();

        insert into [dbo].[registracia]
            ([IdVariabilny],[Meno],[Priezvisko],[Email],[Telefon],[IdZbor],[InyZbor],[PiatokVecera],
            [PiatokVecera2],[UbytovaniePiatokSobota],[TichaTriedaPiatokSobota],
            [SobotaRanajky],[SobotaObed],[SobotaVecera],[SobotaVecera2],
            [UbytovanieSobotaNedela],[TichaTriedaSobotaNedela],[NedelaRanajky],
            [NedelaObed],[Sach],[PingPong],[IdTricko],[Sluziaci],[Poznamka],[DtModified])
        select
            @idVariabilny,[Meno],[Priezvisko],[Email],[Telefon],[IdZbor],[InyZbor],[PiatokVecera],
            [PiatokVecera2],[UbytovaniePiatokSobota],[TichaTriedaPiatokSobota],
            [SobotaRanajky],[SobotaObed],[SobotaVecera],[SobotaVecera2],
            [UbytovanieSobotaNedela],[TichaTriedaSobotaNedela],[NedelaRanajky],
            [NedelaObed],[Sach],[PingPong],[IdTricko],[Sluziaci],[Poznamka],@now
        from @users;
        
        insert into [dbo].[email]
            ([address], [subject], [body], [retries], [success], [idVariabilny])
        select
            [address], [subject], [body], 5, 0, @idVariabilny
        from @emails;
    end
END
GO
