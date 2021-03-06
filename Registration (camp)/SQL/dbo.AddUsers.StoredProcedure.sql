SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUsers]
    @users          [dbo].[ListOfUsers] readonly,
    @emails         [dbo].[ListOfEmails] readonly,
    @payerEmail     [nvarchar](max),
    @amount         [decimal](18,4)
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
            ([email], [amount], [dtModified])
        values
            (@payerEmail, @amount, @now);
        
        declare @idVariabilny int = scope_identity();

        insert into [dbo].[registracia]
            ([IdVariabilny],[Meno],[Priezvisko],[Email],[Telefon],[IdSluzba],[InaSluzba],[Poznamka],[DtModified])
        select
            @idVariabilny,[Meno],[Priezvisko],[Email],[Telefon],[IdSluzba],[InaSluzba],[Poznamka],@now
        from @users;
        
        insert into [dbo].[email]
            ([address], [subject], [body], [retries], [success], [idVariabilny])
        select
            [address], [subject], [body], 5, 0, @idVariabilny
        from @emails;
    end
END
GO
