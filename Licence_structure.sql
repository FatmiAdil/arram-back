IF (EXISTS (SELECT *
   FROM INFORMATION_SCHEMA.TABLES
   WHERE TABLE_SCHEMA = 'dbo'
   AND TABLE_NAME = 'Licence'))
   DROP TABLE Licence
/****** Object:  Table [dbo].[Licence]    Script Date: 10/09/2020 21:10:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Indicatif] [varchar](10) NOT NULL,
	[Nom] [varchar](35) NULL,
	[Prenom] [varchar](35) NULL,
	[Adresse1] [varchar](80) NULL,
	[Adresse2] [varchar](80) NULL,
	[CodePostal] [varchar](6) NULL,
	[Ville] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Website] [varchar](80) NULL,
	[QraLocator] [char](10) NULL,
	[AnneeLicence] [int] NULL,
	[Actif] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NULL,
 CONSTRAINT [PK_dbo.Licence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
