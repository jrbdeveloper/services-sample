/****** Object:  Table [dbo].[Properties]    Script Date: 5/14/2016 9:30:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Properties](
	[PropertyId] [int] IDENTITY(1,1) NOT NULL,
	[AddressId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [float] NULL,
	[Style] [nvarchar](25) NULL,
	[YearBuilt] [int] NULL,
	[LotSize] [int] NULL,
	[SquareFootage] [int] NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


