/****** Object:  Table [dbo].[Matchings]    Script Date: 5/15/2016 11:39:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Matchings](
	[MatchingId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
 CONSTRAINT [PK_Matchings] PRIMARY KEY CLUSTERED 
(
	[MatchingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


