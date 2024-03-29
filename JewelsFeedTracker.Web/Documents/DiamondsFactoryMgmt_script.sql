USE [DiamondsFactoryMgmt]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[Value] [uniqueidentifier] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Stock_ID] [nvarchar](500) NULL,
	[StockNo] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[Value] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[stone_price1_description]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stone_price1_description](
	[stone_price_id] [int] NOT NULL,
	[fluorescence_color] [varchar](32) NULL,
	[measurement] [varchar](32) NOT NULL,
	[measlength] [decimal](9, 4) NULL,
	[measwidth] [decimal](9, 4) NULL,
	[measdepth] [decimal](9, 4) NULL,
	[ratio] [decimal](9, 4) NOT NULL,
	[cert] [varchar](32) NULL,
	[cert_url] [varchar](500) NULL,
	[stock] [varchar](32) NULL,
	[available] [varchar](10) NOT NULL,
	[depth] [decimal](9, 4) NULL,
	[table_table] [decimal](9, 4) NULL,
	[girdle] [varchar](32) NULL,
	[culet] [varchar](32) NULL,
	[culet_size] [varchar](32) NULL,
	[culet_condition] [varchar](255) NULL,
	[parcel_no_stone] [int] NOT NULL,
	[rapnet_lot] [varchar](32) NULL,
	[hascertfile] [varchar](32) NULL,
	[hasimagefile] [varchar](32) NULL,
	[image_url] [varchar](255) NULL,
	[video_url] [varchar](255) NULL,
	[packetID] [varchar](255) NULL,
	[packetNo] [varchar](255) NULL,
	[source] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[stone_price_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stone_price1]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stone_price1](
	[stone_price_id] [int] NOT NULL,
	[stone_vendor_id] [int] NOT NULL,
	[stone] [varchar](8) NOT NULL,
	[shape] [varchar](32) NULL,
	[crt_from] [decimal](9, 4) NOT NULL,
	[crt_to] [decimal](9, 4) NOT NULL,
	[weight] [decimal](9, 4) NULL,
	[color] [varchar](16) NULL,
	[intensity] [varchar](32) NULL,
	[clarity] [varchar](32) NULL,
	[cut_grade] [varchar](32) NULL,
	[polish] [varchar](32) NULL,
	[symmetry] [varchar](32) NULL,
	[fluorescence_intensity] [varchar](32) NULL,
	[lab] [varchar](32) NULL,
	[carat_price] [decimal](15, 2) NULL,
	[total_price] [decimal](15, 2) NULL,
	[country] [varchar](3) NULL,
	[sprice] [decimal](15, 2) NOT NULL,
	[mprice] [decimal](15, 2) NOT NULL,
	[mode] [char](1) NOT NULL,
	[diamond_code] [varchar](100) NULL,
	[checked] [smallint] NOT NULL,
	[status] [smallint] NULL,
 CONSTRAINT [PK__nj_stone__FF66FE0D29572725] PRIMARY KEY CLUSTERED 
(
	[stone_price_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stone_cut_rule]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stone_cut_rule](
	[stone_cut_rule_id] [int] IDENTITY(1,1) NOT NULL,
	[shape] [varchar](16) NULL,
	[polish] [varchar](16) NULL,
	[symmetry] [varchar](16) NULL,
	[depth_from] [varchar](16) NULL,
	[depth_to] [varchar](16) NULL,
	[table_from] [varchar](16) NULL,
	[table_to] [varchar](16) NULL,
	[cut] [varchar](16) NULL,
 CONSTRAINT [PK__nj_stone__63C7F3F15535A963] PRIMARY KEY CLUSTERED 
(
	[stone_cut_rule_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[stone_carat_range]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stone_carat_range](
	[stone_carat_range_id] [int] IDENTITY(1,1) NOT NULL,
	[stone] [varchar](16) NOT NULL,
	[shape] [varchar](16) NOT NULL,
	[from_carat] [decimal](9, 4) NOT NULL,
	[to_carat] [decimal](9, 4) NOT NULL,
	[position] [int] NOT NULL,
 CONSTRAINT [PK__nj_stone__30E1F0FA10216507] PRIMARY KEY CLUSTERED 
(
	[stone_carat_range_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rapnet_option_value]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rapnet_option_value](
	[rapnet_option_value_id] [int] IDENTITY(1,1) NOT NULL,
	[language_id] [int] NOT NULL,
	[rapnet_option_id] [int] NOT NULL,
	[name] [varchar](128) NOT NULL,
 CONSTRAINT [PK__nj_rapne__B0C9A59504AFB25B] PRIMARY KEY CLUSTERED 
(
	[rapnet_option_value_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rapnet_option]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rapnet_option](
	[rapnet_option_id] [int] IDENTITY(1,1) NOT NULL,
	[option_id] [int] NOT NULL,
	[language_id] [int] NOT NULL,
	[include] [smallint] NOT NULL,
	[name] [varchar](128) NOT NULL,
	[option_name] [varchar](32) NOT NULL,
 CONSTRAINT [PK__nj_rapne__D0D7DFAD00DF2177] PRIMARY KEY CLUSTERED 
(
	[rapnet_option_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[option_value_description]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[option_value_description](
	[option_value_id] [int] NOT NULL,
	[language_id] [int] NOT NULL,
	[option_id] [int] NOT NULL,
	[name] [varchar](128) NOT NULL,
	[title] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[option_value_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[option_value]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[option_value](
	[option_value_id] [int] IDENTITY(1,1) NOT NULL,
	[option_id] [int] NOT NULL,
	[display] [varchar](16) NOT NULL,
	[lab_grown] [smallint] NULL,
	[default] [smallint] NOT NULL,
	[minimum] [smallint] NOT NULL,
	[code] [varchar](32) NOT NULL,
	[image] [varchar](255) NOT NULL,
	[sort_order] [int] NOT NULL,
 CONSTRAINT [PK__nj_optio__3AAA210D625A9A57] PRIMARY KEY CLUSTERED 
(
	[option_value_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[option]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[option](
	[option_id] [int] IDENTITY(1,1) NOT NULL,
	[option_group_id] [int] NOT NULL,
	[html_block_id] [int] NOT NULL,
	[include] [smallint] NOT NULL,
	[display] [varchar](16) NOT NULL,
	[lab_grown] [smallint] NOT NULL,
	[type] [varchar](32) NOT NULL,
	[custom_field] [varchar](255) NOT NULL,
	[sort_order] [int] NOT NULL,
 CONSTRAINT [PK__nj_optio__F4EACE1B46B27FE2] PRIMARY KEY CLUSTERED 
(
	[option_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [EventLogging].[Log]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EventLogging].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FineStar]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FineStar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[REPORT_NO] [nvarchar](50) NULL,
	[PACKET_NO] [nvarchar](50) NULL,
	[SHAPE] [varchar](20) NOT NULL,
	[CTS] [numeric](3, 1) NOT NULL,
	[COLOR] [varchar](1) NOT NULL,
	[CUT] [varchar](2) NOT NULL,
	[POLISH] [varchar](2) NOT NULL,
	[SYMM] [varchar](2) NOT NULL,
	[FLS] [varchar](3) NOT NULL,
	[PURITY] [varchar](4) NOT NULL,
	[LAB] [varchar](3) NOT NULL,
	[LENGTH_1] [numeric](4, 2) NOT NULL,
	[WIDTH] [numeric](4, 2) NOT NULL,
	[DEPTH] [numeric](4, 2) NOT NULL,
	[TABLE_PER] [int] NOT NULL,
	[DEPTH_PER] [numeric](4, 1) NOT NULL,
	[CROWN_ANGLE] [numeric](5, 2) NOT NULL,
	[CROWN_HEIGHT] [numeric](5, 2) NOT NULL,
	[PAV_ANGLE] [numeric](5, 2) NOT NULL,
	[PAV_HEIGHT] [numeric](5, 2) NOT NULL,
	[SIDE_NATTS] [varchar](3) NOT NULL,
	[CROWN_OPEN] [varchar](1) NOT NULL,
	[REPORT_COMMENT] [varchar](30) NULL,
	[KEY_TO_SYMBOLS] [varchar](23) NOT NULL,
	[DISC_PER] [int] NOT NULL,
	[RAP_PRICE] [int] NOT NULL,
	[NET_RATE] [int] NOT NULL,
	[NET_VALUE] [int] NOT NULL,
	[HA] [varchar](50) NULL,
	[BRILLIANCY] [varchar](50) NOT NULL,
	[SHADE] [varchar](50) NOT NULL,
	[CULET] [varchar](50) NOT NULL,
	[GIRDLE] [varchar](50) NOT NULL,
	[CERTI_LINK] [varchar](30) NULL,
	[COMMENTS] [varchar](30) NULL,
	[CENTER_NATTS] [varchar](50) NOT NULL,
	[SIDE_FEATHER] [varchar](50) NOT NULL,
	[CENTER_FEATHER] [varchar](50) NOT NULL,
	[EYE_CLEAN] [varchar](50) NOT NULL,
	[MEASUREMENT] [varchar](18) NOT NULL,
	[AVG_DIA] [numeric](4, 2) NOT NULL,
	[DNA] [varchar](48) NOT NULL,
	[REAL_IMAGE] [varchar](74) NOT NULL,
	[HEART_IMAGE] [varchar](30) NULL,
	[ARROW_IMAGE] [varchar](30) NULL,
	[PLOTTING_IMAGE] [varchar](79) NOT NULL,
	[VIDEO] [varchar](129) NOT NULL,
	[CERTI_IMAGE] [varchar](76) NOT NULL,
	[LOCATION] [varchar](50) NOT NULL,
	[B2B_MP4] [varchar](67) NOT NULL,
	[B2C_MP4] [varchar](30) NULL,
 CONSTRAINT [PK_FineStar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[country]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[country](
	[country_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](128) NOT NULL,
	[iso_code_2] [varchar](2) NOT NULL,
	[iso_code_3] [varchar](3) NOT NULL,
	[address_format] [varchar](max) NOT NULL,
	[postcode_required] [smallint] NOT NULL,
	[status] [smallint] NOT NULL,
 CONSTRAINT [PK__nj_count__7E8CD05557DD0BE4] PRIMARY KEY CLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[certificate_image]    Script Date: 09/24/2021 15:15:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[certificate_image](
	[certificate_image_id] [int] IDENTITY(1,1) NOT NULL,
	[cert_no] [varchar](50) NOT NULL,
	[shape] [varchar](32) NULL,
	[color] [varchar](16) NULL,
	[clarity] [varchar](32) NULL,
	[lab] [varchar](32) NULL,
	[web_url] [varchar](max) NULL,
	[local_url] [varchar](max) NULL,
	[status] [smallint] NOT NULL,
	[date_added] [datetime2](0) NULL,
 CONSTRAINT [PK__nj_certi__5D36ABE174AE54BC] PRIMARY KEY CLUSTERED 
(
	[certificate_image_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [cert_no] UNIQUE NONCLUSTERED 
(
	[cert_no] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__nj_certif__cert___778AC167]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__cert___778AC167]  DEFAULT ('DI') FOR [cert_no]
GO
/****** Object:  Default [DF__nj_certif__shape__787EE5A0]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__shape__787EE5A0]  DEFAULT (NULL) FOR [shape]
GO
/****** Object:  Default [DF__nj_certif__color__797309D9]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__color__797309D9]  DEFAULT (NULL) FOR [color]
GO
/****** Object:  Default [DF__nj_certif__clari__7A672E12]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__clari__7A672E12]  DEFAULT (NULL) FOR [clarity]
GO
/****** Object:  Default [DF__nj_certific__lab__7B5B524B]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certific__lab__7B5B524B]  DEFAULT (NULL) FOR [lab]
GO
/****** Object:  Default [DF__nj_certif__web_u__7C4F7684]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__web_u__7C4F7684]  DEFAULT (NULL) FOR [web_url]
GO
/****** Object:  Default [DF__nj_certif__local__7D439ABD]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__local__7D439ABD]  DEFAULT (NULL) FOR [local_url]
GO
/****** Object:  Default [DF__nj_certif__statu__7E37BEF6]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__statu__7E37BEF6]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF__nj_certif__date___7F2BE32F]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[certificate_image] ADD  CONSTRAINT [DF__nj_certif__date___7F2BE32F]  DEFAULT (NULL) FOR [date_added]
GO
/****** Object:  Default [DF__nj_countr__statu__59C55456]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[country] ADD  CONSTRAINT [DF__nj_countr__statu__59C55456]  DEFAULT ((1)) FOR [status]
GO
/****** Object:  Default [DF__nj_option__inclu__489AC854]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[option] ADD  CONSTRAINT [DF__nj_option__inclu__489AC854]  DEFAULT ((0)) FOR [include]
GO
/****** Object:  Default [DF__nj_option__displ__498EEC8D]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[option] ADD  CONSTRAINT [DF__nj_option__displ__498EEC8D]  DEFAULT ('') FOR [display]
GO
/****** Object:  Default [DF__nj_option__lab_g__4A8310C6]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[option] ADD  CONSTRAINT [DF__nj_option__lab_g__4A8310C6]  DEFAULT ((0)) FOR [lab_grown]
GO
/****** Object:  Default [DF__nj_option__displ__6442E2C9]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[option_value] ADD  CONSTRAINT [DF__nj_option__displ__6442E2C9]  DEFAULT ('') FOR [display]
GO
/****** Object:  Default [DF__nj_option__lab_g__65370702]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[option_value] ADD  CONSTRAINT [DF__nj_option__lab_g__65370702]  DEFAULT ((0)) FOR [lab_grown]
GO
/****** Object:  Default [DF__nj_option__defau__662B2B3B]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[option_value] ADD  CONSTRAINT [DF__nj_option__defau__662B2B3B]  DEFAULT ((0)) FOR [default]
GO
/****** Object:  Default [DF__nj_option__minim__671F4F74]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[option_value] ADD  CONSTRAINT [DF__nj_option__minim__671F4F74]  DEFAULT ((0)) FOR [minimum]
GO
/****** Object:  Default [DF__nj_stone___shape__571DF1D5]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone___shape__571DF1D5]  DEFAULT (NULL) FOR [shape]
GO
/****** Object:  Default [DF__nj_stone___polis__5812160E]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone___polis__5812160E]  DEFAULT (NULL) FOR [polish]
GO
/****** Object:  Default [DF__nj_stone___symme__59063A47]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone___symme__59063A47]  DEFAULT (NULL) FOR [symmetry]
GO
/****** Object:  Default [DF__nj_stone___depth__59FA5E80]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone___depth__59FA5E80]  DEFAULT (NULL) FOR [depth_from]
GO
/****** Object:  Default [DF__nj_stone___depth__5AEE82B9]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone___depth__5AEE82B9]  DEFAULT (NULL) FOR [depth_to]
GO
/****** Object:  Default [DF__nj_stone___table__5BE2A6F2]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone___table__5BE2A6F2]  DEFAULT (NULL) FOR [table_from]
GO
/****** Object:  Default [DF__nj_stone___table__5CD6CB2B]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone___table__5CD6CB2B]  DEFAULT (NULL) FOR [table_to]
GO
/****** Object:  Default [DF__nj_stone_cu__cut__5DCAEF64]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_cut_rule] ADD  CONSTRAINT [DF__nj_stone_cu__cut__5DCAEF64]  DEFAULT (NULL) FOR [cut]
GO
/****** Object:  Default [DF__nj_stone___stone__2B3F6F97]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___stone__2B3F6F97]  DEFAULT ('DI') FOR [stone]
GO
/****** Object:  Default [DF__nj_stone___shape__2C3393D0]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___shape__2C3393D0]  DEFAULT (NULL) FOR [shape]
GO
/****** Object:  Default [DF__nj_stone___weigh__2D27B809]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___weigh__2D27B809]  DEFAULT (NULL) FOR [weight]
GO
/****** Object:  Default [DF__nj_stone___color__2E1BDC42]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___color__2E1BDC42]  DEFAULT (NULL) FOR [color]
GO
/****** Object:  Default [DF__nj_stone___inten__2F10007B]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___inten__2F10007B]  DEFAULT (NULL) FOR [intensity]
GO
/****** Object:  Default [DF__nj_stone___clari__300424B4]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___clari__300424B4]  DEFAULT (NULL) FOR [clarity]
GO
/****** Object:  Default [DF__nj_stone___cut_g__30F848ED]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___cut_g__30F848ED]  DEFAULT (NULL) FOR [cut_grade]
GO
/****** Object:  Default [DF__nj_stone___polis__31EC6D26]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___polis__31EC6D26]  DEFAULT (NULL) FOR [polish]
GO
/****** Object:  Default [DF__nj_stone___symme__32E0915F]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___symme__32E0915F]  DEFAULT (NULL) FOR [symmetry]
GO
/****** Object:  Default [DF__nj_stone___fluor__33D4B598]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___fluor__33D4B598]  DEFAULT (NULL) FOR [fluorescence_intensity]
GO
/****** Object:  Default [DF__nj_stone_pr__lab__34C8D9D1]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone_pr__lab__34C8D9D1]  DEFAULT (NULL) FOR [lab]
GO
/****** Object:  Default [DF__nj_stone___carat__35BCFE0A]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___carat__35BCFE0A]  DEFAULT (NULL) FOR [carat_price]
GO
/****** Object:  Default [DF__nj_stone___total__36B12243]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___total__36B12243]  DEFAULT (NULL) FOR [total_price]
GO
/****** Object:  Default [DF__nj_stone___count__37A5467C]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___count__37A5467C]  DEFAULT ('1') FOR [country]
GO
/****** Object:  Default [DF__nj_stone_p__mode__38996AB5]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone_p__mode__38996AB5]  DEFAULT ('s') FOR [mode]
GO
/****** Object:  Default [DF__nj_stone___diamo__398D8EEE]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___diamo__398D8EEE]  DEFAULT (NULL) FOR [diamond_code]
GO
/****** Object:  Default [DF__nj_stone___check__3A81B327]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___check__3A81B327]  DEFAULT ((0)) FOR [checked]
GO
/****** Object:  Default [DF__nj_stone___statu__3B75D760]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1] ADD  CONSTRAINT [DF__nj_stone___statu__3B75D760]  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF__nj_stone___fluor__403A8C7D]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [fluorescence_color]
GO
/****** Object:  Default [DF__nj_stone___measl__412EB0B6]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [measlength]
GO
/****** Object:  Default [DF__nj_stone___measw__4222D4EF]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [measwidth]
GO
/****** Object:  Default [DF__nj_stone___measd__4316F928]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [measdepth]
GO
/****** Object:  Default [DF__nj_stone_p__cert__440B1D61]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [cert]
GO
/****** Object:  Default [DF__nj_stone___cert___44FF419A]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [cert_url]
GO
/****** Object:  Default [DF__nj_stone___stock__45F365D3]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [stock]
GO
/****** Object:  Default [DF__nj_stone___depth__46E78A0C]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [depth]
GO
/****** Object:  Default [DF__nj_stone___table__47DBAE45]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [table_table]
GO
/****** Object:  Default [DF__nj_stone___girdl__48CFD27E]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [girdle]
GO
/****** Object:  Default [DF__nj_stone___culet__49C3F6B7]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [culet]
GO
/****** Object:  Default [DF__nj_stone___culet__4AB81AF0]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [culet_size]
GO
/****** Object:  Default [DF__nj_stone___culet__4BAC3F29]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [culet_condition]
GO
/****** Object:  Default [DF__nj_stone___rapne__4CA06362]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [rapnet_lot]
GO
/****** Object:  Default [DF__nj_stone___hasce__4D94879B]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [hascertfile]
GO
/****** Object:  Default [DF__nj_stone___hasim__4E88ABD4]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [hasimagefile]
GO
/****** Object:  Default [DF__nj_stone___image__4F7CD00D]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [image_url]
GO
/****** Object:  Default [DF__nj_stone___video__5070F446]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [video_url]
GO
/****** Object:  Default [DF__nj_stone___packe__5165187F]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [packetID]
GO
/****** Object:  Default [DF__nj_stone___packe__52593CB8]    Script Date: 09/24/2021 15:15:44 ******/
ALTER TABLE [dbo].[stone_price1_description] ADD  DEFAULT (NULL) FOR [packetNo]
GO
