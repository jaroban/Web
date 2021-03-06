SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEmails]
    @emails [dbo].[ListOfEmails] readonly
AS
BEGIN
    update o set 
        [success] = 1,
        [dtLastAttempt] = getutcdate()
    from [dbo].[email] o
    inner join @emails n on n.[id] = o.[id]
    where n.[success] = 1;
    
    update o set 
        [retries] = [retries] - 1,
        [result] = n.[result],
        [dtLastAttempt] = getutcdate()
    from [dbo].[email] o
    inner join @emails n on n.[id] = o.[id]
    where n.[success] = 0 and o.[retries] > 0;
END
GO
