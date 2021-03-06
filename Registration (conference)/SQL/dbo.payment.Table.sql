SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idVariabilny] [int] NOT NULL,
	[note] [nvarchar](max) NULL,
	[payer] [nvarchar](max) NULL,
	[idCurrency] [int] NOT NULL,
	[amount] [decimal](18, 4) NOT NULL,
	[dtModified] [datetime] NULL,
 CONSTRAINT [PK_payments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payments_currency] FOREIGN KEY([idCurrency])
REFERENCES [dbo].[currency] ([id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payments_currency]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payments_variabilny] FOREIGN KEY([idVariabilny])
REFERENCES [dbo].[variabilny] ([id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payments_variabilny]
GO
