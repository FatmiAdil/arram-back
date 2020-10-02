/****** Object:  Table [dbo].[RefCategorieLien]    Script Date: 02/10/2020 16:35:26 ******/
DROP TABLE IF EXISTS [dbo].[RefCategorieLien]
GO
/****** Object:  Table [dbo].[RefCategorieLien]    Script Date: 02/10/2020 16:35:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefCategorieLien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Libelle] [varchar](250) NOT NULL,
	[SuppressorId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NULL,
 CONSTRAINT [PK_dbo.RefCategorieLien] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
