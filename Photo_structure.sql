IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]') AND type in (N'U'))
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT IF EXISTS [FK_dbo.Photo_dbo.Licence_LicenceId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]') AND type in (N'U'))
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT IF EXISTS [FK_dbo.Photo_dbo.Licence_LicenceId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]') AND type in (N'U'))
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT IF EXISTS [FK_dbo.Photo_dbo.Licence_LicenceId]
GO
/****** Object:  Index [IX_LicenceId]    Script Date: 02/10/2020 16:35:26 ******/
DROP INDEX IF EXISTS [IX_LicenceId] ON [dbo].[Photo]
GO

/****** Object:  Table [dbo].[Photo]    Script Date: 02/10/2020 16:35:26 ******/
DROP TABLE IF EXISTS [dbo].[Photo]
GO

/****** Object:  Table [dbo].[Photo]    Script Date: 02/10/2020 16:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LicenceId] [int] NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Url] [varchar](500) NOT NULL,
	[SuppressorId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NULL,
 CONSTRAINT [PK_dbo.Photo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE NONCLUSTERED INDEX [IX_LicenceId] ON [dbo].[Photo]
(
	[LicenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Photo_dbo.Licence_LicenceId] FOREIGN KEY([LicenceId])
REFERENCES [dbo].[Licence] ([Id])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_dbo.Photo_dbo.Licence_LicenceId]
GO