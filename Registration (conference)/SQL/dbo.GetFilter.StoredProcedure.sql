SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFilter]
AS
BEGIN
    select z.[id], z.[name]
    from [dbo].[zbor] z
    where exists(select * from [dbo].[registracia] r where r.[IdZbor] = z.[id])
    order by z.[name] asc;
END
GO
