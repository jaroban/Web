CREATE TYPE [dbo].[ListOfEmails] AS TABLE(
	[id] [int] NULL,
	[address] [nvarchar](max) NULL,
	[subject] [nvarchar](max) NULL,
	[body] [nvarchar](max) NULL,
	[success] [bit] NULL,
	[result] [nvarchar](max) NULL
)
GO
