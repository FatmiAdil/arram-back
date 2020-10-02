
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lien]') AND type in (N'U'))
ALTER TABLE [dbo].[Lien] DROP CONSTRAINT IF EXISTS [FK_dbo.Lien_dbo.RefCategorieLien_CategorieId]
GO
/****** Object:  Index [IX_CategorieId]    Script Date: 02/10/2020 16:35:26 ******/
DROP INDEX IF EXISTS [IX_CategorieId] ON [dbo].[Lien]
GO
/****** Object:  Table [dbo].[Lien]    Script Date: 02/10/2020 16:35:26 ******/
DROP TABLE IF EXISTS [dbo].[Lien]
GO

/****** Object:  Table [dbo].[Lien]    Script Date: 02/10/2020 16:35:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [varchar](100) NOT NULL,
	[Texte] [varchar](500) NOT NULL,
	[Desc] [varchar](1000) NOT NULL,
	[Ordre] [int] NULL,
	[CategorieId] [int] NOT NULL,
	[SuppressorId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NULL,
 CONSTRAINT [PK_dbo.Lien] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [IX_CategorieId]    Script Date: 02/10/2020 16:35:36 ******/
CREATE NONCLUSTERED INDEX [IX_CategorieId] ON [dbo].[Lien]
(
	[CategorieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lien]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Lien_dbo.RefCategorieLien_CategorieId] FOREIGN KEY([CategorieId])
REFERENCES [dbo].[RefCategorieLien] ([Id])
GO

ALTER TABLE [dbo].[Lien] CHECK CONSTRAINT [FK_dbo.Lien_dbo.RefCategorieLien_CategorieId]
GO