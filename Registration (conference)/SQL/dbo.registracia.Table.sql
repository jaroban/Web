SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[registracia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdVariabilny] [int] NULL,
	[Meno] [nvarchar](max) NULL,
	[Priezvisko] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Telefon] [nvarchar](max) NULL,
	[IdZbor] [int] NULL,
	[InyZbor] [nvarchar](max) NULL,
	[PiatokVecera] [bit] NOT NULL,
	[PiatokVecera2] [int] NOT NULL,
	[UbytovaniePiatokSobota] [bit] NOT NULL,
	[TichaTriedaPiatokSobota] [bit] NOT NULL,
	[SobotaRanajky] [bit] NOT NULL,
	[SobotaObed] [bit] NOT NULL,
	[SobotaVecera] [bit] NOT NULL,
	[SobotaVecera2] [int] NOT NULL,
	[UbytovanieSobotaNedela] [bit] NOT NULL,
	[TichaTriedaSobotaNedela] [bit] NOT NULL,
	[NedelaRanajky] [bit] NOT NULL,
	[NedelaObed] [bit] NOT NULL,
	[Sach] [bit] NOT NULL,
	[PingPong] [bit] NOT NULL,
	[IdTricko] [int] NULL,
	[Sluziaci] [nvarchar](max) NULL,
	[Poznamka] [nvarchar](max) NULL,
	[DtModified] [datetime] NULL,
	[Internat1] [bit] NULL,
	[Internat2] [bit] NULL,
	[Preplacame] [decimal](18, 4) NULL,
	[InternatZadarmo] [bit] NULL,
	[RegistraciaZadarmo] [bit] NULL,
	[JedloZadarmo] [bit] NULL,
	[DtPlatba] [datetime] NULL,
	[Prisli] [bit] NULL,
	[IstoPride] [bit] NULL,
	[Suma]  AS (((((1)-coalesce(CONVERT([int],[JedloZadarmo],0),(0)))*(((((1)*(CONVERT([int],[SobotaRanajky],0)+CONVERT([int],[NedelaRanajky],0))+(2.5)*(CONVERT([int],[SobotaObed],0)+CONVERT([int],[NedelaObed],0)))+(2.2)*(CONVERT([int],[PiatokVecera],0)+CONVERT([int],[SobotaVecera],0)))+(1)*(CONVERT([int],[PiatokVecera2],0)+CONVERT([int],[SobotaVecera2],0)))+(1)*(CONVERT([int],[UbytovaniePiatokSobota],0)+CONVERT([int],[UbytovanieSobotaNedela],0)))+((1)-coalesce(CONVERT([int],[InternatZadarmo],0),(0)))*((10)*coalesce(CONVERT([int],[Internat1],0),(0))+(14)*coalesce(CONVERT([int],[Internat2],0),(0))))+ -coalesce([Preplacame],(0)))+((1)-coalesce(CONVERT([int],[RegistraciaZadarmo],0),(0)))*case when [IdZbor]=(71) then (8) when [Sluziaci]='Dobrovolnik' then (4) when [DtModified]<CONVERT([datetime],'2015-01-16 23:59:59',0) AND coalesce([DtPlatba],getdate())<CONVERT([datetime],'2015-01-19 23:59:59',0) then (8) when [DtModified]<CONVERT([datetime],'2015-02-13 21:00:00',0) then (11) when [DtModified]<CONVERT([datetime],'2015-02-14 08:59:59',0) then (9) when [DtModified]<CONVERT([datetime],'2015-02-14 14:59:59',0) then (6) when [DtModified]<CONVERT([datetime],'2015-02-14 23:59:59',0) then (3) else (0) end),
	[RegistracnyPoplatok]  AS (((1)-coalesce(CONVERT([int],[RegistraciaZadarmo],0),(0)))*case when [IdZbor]=(71) then (8) when [Sluziaci]='Dobrovolnik' then (4) when [DtModified]<CONVERT([datetime],'2015-01-16 23:59:59',0) AND coalesce([DtPlatba],getdate())<CONVERT([datetime],'2015-01-19 23:59:59',0) then (8) when [DtModified]<CONVERT([datetime],'2015-02-13 21:00:00',0) then (11) when [DtModified]<CONVERT([datetime],'2015-02-14 08:59:59',0) then (9) when [DtModified]<CONVERT([datetime],'2015-02-14 14:59:59',0) then (6) when [DtModified]<CONVERT([datetime],'2015-02-14 23:59:59',0) then (3) else (0) end),
 CONSTRAINT [PK_registracia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[registracia]  WITH CHECK ADD  CONSTRAINT [FK_registracia_tricko] FOREIGN KEY([IdTricko])
REFERENCES [dbo].[tricko] ([id])
GO
ALTER TABLE [dbo].[registracia] CHECK CONSTRAINT [FK_registracia_tricko]
GO
ALTER TABLE [dbo].[registracia]  WITH CHECK ADD  CONSTRAINT [FK_registracia_variabilny] FOREIGN KEY([IdVariabilny])
REFERENCES [dbo].[variabilny] ([id])
GO
ALTER TABLE [dbo].[registracia] CHECK CONSTRAINT [FK_registracia_variabilny]
GO
ALTER TABLE [dbo].[registracia]  WITH CHECK ADD  CONSTRAINT [FK_registracia_zbor] FOREIGN KEY([IdZbor])
REFERENCES [dbo].[zbor] ([id])
GO
ALTER TABLE [dbo].[registracia] CHECK CONSTRAINT [FK_registracia_zbor]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_Internat1]  DEFAULT ((0)) FOR [Internat1]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_Internat2]  DEFAULT ((0)) FOR [Internat2]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_Preplacame]  DEFAULT ((0)) FOR [Preplacame]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_InternatZadarmo]  DEFAULT ((0)) FOR [InternatZadarmo]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_RegistraciaZadarmo]  DEFAULT ((0)) FOR [RegistraciaZadarmo]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_JedloZadarmo]  DEFAULT ((0)) FOR [JedloZadarmo]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_Prisli]  DEFAULT ((0)) FOR [Prisli]
GO
ALTER TABLE [dbo].[registracia] ADD  CONSTRAINT [DF_registracia_IstoPride]  DEFAULT ((0)) FOR [IstoPride]
GO
