SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ShowedUp]
    @id [int]
AS
    update [dbo].[registracia] set 
        [Prisli] = 1 
    where [id] = @id;
GO
