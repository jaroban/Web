CREATE TYPE [dbo].[ListOfUsers] AS TABLE(
	[Meno] [nvarchar](max) NULL,
	[Priezvisko] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Telefon] [nvarchar](max) NULL,
	[IdSluzba] [int] NULL,
	[InaSluzba] [nvarchar](max) NULL,
	[Poznamka] [nvarchar](max) NULL
)
GO
