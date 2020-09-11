
/****** Object:  Table [dbo].[Illustration]    Script Date: 11/09/2020 13:14:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Illustration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Url] [varchar](500) NOT NULL,
	[isDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NULL,
	[DateModification] [datetime] NULL,
 CONSTRAINT [PK_dbo.Illustration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [IX_ArticleId]    Script Date: 11/09/2020 13:14:47 ******/
CREATE NONCLUSTERED INDEX [IX_ArticleId] ON [dbo].[Illustration]
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Illustration] ADD  CONSTRAINT [DF_Illustration_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Illustration] ADD  CONSTRAINT [DF_Illustration_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO
ALTER TABLE [dbo].[Illustration] ADD  CONSTRAINT [DF_Illustration_DateModification]  DEFAULT (getdate()) FOR [DateModification]
GO
ALTER TABLE [dbo].[Illustration]  WITH CHECK ADD  CONSTRAINT [FK_Illustration_Article] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([Id])
GO
ALTER TABLE [dbo].[Illustration] CHECK CONSTRAINT [FK_Illustration_Article]
GO