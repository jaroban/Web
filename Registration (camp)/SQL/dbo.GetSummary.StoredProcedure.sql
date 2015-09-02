SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSummary]
AS
BEGIN
    -- counts
    select count(*) from [dbo].[registracia];
    
    -- sluzby
    select id, name from [dbo].[sluzba];
END
GO
