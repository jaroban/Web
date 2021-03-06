SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[team](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idSport] [int] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[player1] [nvarchar](max) NULL,
	[player2] [nvarchar](max) NULL,
	[player3] [nvarchar](max) NULL,
	[player4] [nvarchar](max) NULL,
	[player5] [nvarchar](max) NULL,
	[player6] [nvarchar](max) NULL,
	[player7] [nvarchar](max) NULL,
	[dtModified] [datetime] NULL,
 CONSTRAINT [PK_team] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[team]  WITH CHECK ADD  CONSTRAINT [FK_team_sport] FOREIGN KEY([idSport])
REFERENCES [dbo].[sport] ([id])
GO
ALTER TABLE [dbo].[team] CHECK CONSTRAINT [FK_team_sport]
GO
