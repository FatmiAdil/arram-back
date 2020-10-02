
/****** Object:  Table [dbo].[Video]    Script Date: 02/10/2020 16:35:26 ******/
DROP TABLE IF EXISTS [dbo].[Video]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Video](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [varchar](250) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Source] [varchar](250) NOT NULL,
	[SuppressorId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateModification] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.Video] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Video] ADD  CONSTRAINT [DF_Video_DateCreation]  DEFAULT (getdate()) FOR [DateCreation]
GO

ALTER TABLE [dbo].[Video] ADD  CONSTRAINT [DF_Video_DateModification]  DEFAULT (getdate()) FOR [DateModification]
GO
