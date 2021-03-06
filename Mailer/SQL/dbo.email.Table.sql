SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[email](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[address] [nvarchar](max) NULL,
	[subject] [nvarchar](max) NULL,
	[body] [nvarchar](max) NULL,
	[retries] [int] NOT NULL,
	[success] [bit] NOT NULL,
	[idVariabilny] [int] NULL,
	[result] [nvarchar](max) NULL,
	[dtLastAttempt] [datetime] NULL,
 CONSTRAINT [PK_email] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
