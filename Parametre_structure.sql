/****** Object:  Table [dbo].[Parametre]    Script Date: 02/10/2020 16:35:26 ******/
DROP TABLE IF EXISTS [dbo].[Parametre]
GO
/****** Object:  Table [dbo].[Parametre]    Script Date: 02/10/2020 16:35:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parametre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VersionRelais] [int] NOT NULL,
	[VersionNews] [int] NOT NULL,
	[VersionAnnuaire] [int] NOT NULL,
	[VersionPhoto] [int] NOT NULL,
	[VersionLien] [int] NOT NULL,
	[SuppressorId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NULL,
 CONSTRAINT [PK_dbo.Parametre] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

