CREATE TYPE [dbo].[ListOfUsers] AS TABLE(
	[Meno] [nvarchar](max) NULL,
	[Priezvisko] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Telefon] [nvarchar](max) NULL,
	[IdZbor] [int] NULL,
	[InyZbor] [nvarchar](max) NULL,
	[PiatokVecera] [bit] NOT NULL,
	[PiatokVecera2] [bit] NOT NULL,
	[UbytovaniePiatokSobota] [bit] NOT NULL,
	[TichaTriedaPiatokSobota] [bit] NOT NULL,
	[SobotaRanajky] [bit] NOT NULL,
	[SobotaObed] [bit] NOT NULL,
	[SobotaVecera] [bit] NOT NULL,
	[SobotaVecera2] [bit] NOT NULL,
	[UbytovanieSobotaNedela] [bit] NOT NULL,
	[TichaTriedaSobotaNedela] [bit] NOT NULL,
	[NedelaRanajky] [bit] NOT NULL,
	[NedelaObed] [bit] NOT NULL,
	[Sach] [bit] NOT NULL,
	[PingPong] [bit] NOT NULL,
	[IdTricko] [int] NULL,
	[Sluziaci] [nvarchar](max) NULL,
	[Poznamka] [nvarchar](max) NULL
)
GO
