/****** Object:  Table [dbo].[Relais]    Script Date: 02/10/2020 16:35:26 ******/
DROP TABLE IF EXISTS [dbo].[Relais]
GO
/****** Object:  Table [dbo].[Relais]    Script Date: 11/09/2020 15:40:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relais](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Region] [varchar](50) NOT NULL,
	[Site] [varchar](50) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[Altitude] [int] NOT NULL,
	[FreqEntree] [int] NOT NULL,
	[FreqSortie] [int] NOT NULL,
	[Bande] [int] NOT NULL,
	[Shift] [int] NOT NULL,
	[Tone] [varchar](50) NOT NULL,
	[Puissance] [int] NOT NULL,
	[QraLocator] [varchar](12) NOT NULL,
	[Latitude] [decimal](18, 12) NOT NULL,
	[Longitude] [decimal](18, 12) NOT NULL,
	[Position] [geography] NULL,
	[SuppressorId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime] NULL,
 CONSTRAINT [PK_dbo.Relais] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO