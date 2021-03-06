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
	[IdSluzba] [int] NULL,
	[InaSluzba] [nvarchar](max) NULL,
	[Poznamka] [nvarchar](max) NULL,
	[DtModified] [datetime] NULL,
	[DtPlatba] [datetime] NULL,
	[RegistracnyPoplatok]  AS (case when coalesce([DtPlatba],getdate())<CONVERT([datetime],'2015-08-18 23:59:59',0) then (28) else (30) end),
 CONSTRAINT [PK_registracia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[registracia]  WITH CHECK ADD  CONSTRAINT [FK_registracia_sluzba] FOREIGN KEY([IdSluzba])
REFERENCES [dbo].[sluzba] ([id])
GO
ALTER TABLE [dbo].[registracia] CHECK CONSTRAINT [FK_registracia_sluzba]
GO
ALTER TABLE [dbo].[registracia]  WITH CHECK ADD  CONSTRAINT [FK_registracia_variabilny] FOREIGN KEY([IdVariabilny])
REFERENCES [dbo].[variabilny] ([id])
GO
ALTER TABLE [dbo].[registracia] CHECK CONSTRAINT [FK_registracia_variabilny]
GO
