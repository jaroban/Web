SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[receivable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](max) NULL,
	[amount] [decimal](18, 4) NOT NULL,
	[idCurrency] [int] NOT NULL,
	[needHelp] [bit] NOT NULL,
	[dtModified] [datetime] NULL,
	[donation] [decimal](18, 4) NULL,
 CONSTRAINT [PK_receivable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[receivable]  WITH CHECK ADD  CONSTRAINT [FK_receivable_currency] FOREIGN KEY([idCurrency])
REFERENCES [dbo].[currency] ([id])
GO
ALTER TABLE [dbo].[receivable] CHECK CONSTRAINT [FK_receivable_currency]
GO
ALTER TABLE [dbo].[receivable] ADD  CONSTRAINT [DF_receivable_donation]  DEFAULT ((0)) FOR [donation]
GO
