USE [konfera]
GO
/****** Object:  Table [dbo].[zbor]    Script Date: 09/01/2015 23:41:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zbor](
	[id] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_zbor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
