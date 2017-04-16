CREATE TABLE [dbo].[Articles](
	[ArticleName] [varchar](50) NOT NULL,
	[ArticleNumber] [varchar](50) NULL,
	[Storage] [varchar](50) NULL,
	[ShelfNumber] [varchar](50) NULL,
	[BrandOfManufacturer] [varchar](50) NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
