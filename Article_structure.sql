IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
ALTER TABLE [dbo].[Article] DROP CONSTRAINT IF EXISTS [FK_Article_RefTypeArticle]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
ALTER TABLE [dbo].[Article] DROP CONSTRAINT IF EXISTS [FK_Article_Licence]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
ALTER TABLE [dbo].[Article] DROP CONSTRAINT IF EXISTS [DF_Article_DateModification]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
ALTER TABLE [dbo].[Article] DROP CONSTRAINT IF EXISTS [DF_Article_DateCreation]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
ALTER TABLE [dbo].[Article] DROP CONSTRAINT IF EXISTS [DF_Article_isDeleted]
GO
/****** Object:  Index [IX_TypeArticleId]    Script Date: 11/09/2020 12:03:13 ******/
DROP INDEX IF EXISTS [IX_TypeArticleId] ON [dbo].[Article]
GO
/****** Object:  Index [IX_LicenceId]    Script Date: 11/09/2020 12:03:14 ******/
DROP INDEX IF EXISTS [IX_LicenceId] ON [dbo].[Article]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 11/09/2020 12:03:14 ******/
DROP TABLE IF EXISTS [dbo].[Article]
GO
/****** Object:  Table [dbo].[Article]    Script Date: 11/09/2020 12:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Article](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titre] [varchar](200) NOT NULL,
	[Texte] [varchar](max) NOT NULL,
	[DateArticle] [datetime] NOT NULL,
	[LicenceId] [int] NOT NULL,
	[RefTypeArticleId] [int] NOT NULL,
	[SuppressorId] [int] NULL,
	[isDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
Go
/****** Object:  Index [IX_LicenceId]    Script Date: 11/09/2020 12:03:15 ******/
CREATE NONCLUSTERED INDEX [IX_LicenceId] ON [dbo].[Article]
(
	[LicenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TypeArticleId]    Script Date: 11/09/2020 12:03:15 ******/
CREATE NONCLUSTERED INDEX [IX_TypeArticleId] ON [dbo].[Article]
(
	[RefTypeArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_DateModification]  DEFAULT (getdate()) FOR [DateModification]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_Licence] FOREIGN KEY([LicenceId])
REFERENCES [dbo].[Licence] ([Id])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_Licence]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_RefTypeArticle] FOREIGN KEY([RefTypeArticleId])
REFERENCES [dbo].[RefTypeArticle] ([Id])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_RefTypeArticle]
GO

