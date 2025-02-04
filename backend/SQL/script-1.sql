USE [LibraRestaurant]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Picture] [nvarchar](max) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryItems]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryItems](
	[CategoryItemId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_CategoryItems] PRIMARY KEY CLUSTERED 
(
	[CategoryItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[NameEn] [nvarchar](max) NOT NULL,
	[Fullname] [nvarchar](max) NOT NULL,
	[FullnameEn] [nvarchar](max) NOT NULL,
	[CodeName] [nvarchar](max) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currencies](
	[CurrencyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Districts]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Districts](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[NameEn] [nvarchar](max) NOT NULL,
	[Fullname] [nvarchar](max) NOT NULL,
	[FullnameEn] [nvarchar](max) NOT NULL,
	[CodeName] [nvarchar](max) NOT NULL,
	[CityId] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [uniqueidentifier] NOT NULL,
	[StoreId] [uniqueidentifier] NULL,
	[Email] [nvarchar](320) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[Mobile] [nvarchar](15) NOT NULL,
	[Status] [int] NOT NULL,
	[RegisteredAt] [datetime2](7) NOT NULL,
	[LastLoggedinDate] [datetimeoffset](7) NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItems]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItems](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](75) NOT NULL,
	[Slug] [nvarchar](100) NOT NULL,
	[Summary] [nvarchar](1000) NULL,
	[SKU] [nvarchar](100) NOT NULL,
	[Picture] [nvarchar](max) NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Recipe] [nvarchar](1000) NULL,
	[Instruction] [nvarchar](1000) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[LastUpdatedAt] [datetime2](7) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_MenuItems] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[StoreId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeaders]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeaders](
	[OrderId] [uniqueidentifier] NOT NULL,
	[OrderNo] [nvarchar](max) NOT NULL,
	[StoreId] [uniqueidentifier] NOT NULL,
	[PaymentMethodId] [int] NULL,
	[PaymentTimeId] [int] NULL,
	[ServantId] [uniqueidentifier] NULL,
	[CashierId] [uniqueidentifier] NULL,
	[CustomerNotes] [nvarchar](max) NULL,
	[ReservationId] [int] NOT NULL,
	[PriceCalculated] [float] NOT NULL,
	[PriceAdjustment] [float] NULL,
	[PriceAdjustmentReason] [nvarchar](max) NULL,
	[Subtotal] [float] NOT NULL,
	[Tax] [float] NOT NULL,
	[Total] [float] NOT NULL,
	[LatestStatus] [int] NOT NULL,
	[LatestStatusUpdate] [datetime] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[IsPreparationDelayed] [bit] NOT NULL,
	[DelayedTime] [datetime2](7) NULL,
	[IsCanceled] [bit] NOT NULL,
	[CanceledTime] [datetime2](7) NULL,
	[CanceledReason] [nvarchar](max) NULL,
	[IsReady] [bit] NOT NULL,
	[ReadyTime] [datetime2](7) NULL,
	[IsCompleted] [bit] NOT NULL,
	[CompletedTime] [datetime2](7) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderHeaders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderLines]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderLines](
	[OrderLineId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ItemId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsCanceled] [bit] NOT NULL,
	[CanceledTime] [datetime] NULL,
	[CanceledReason] [nvarchar](max) NULL,
	[CustomerReview] [nvarchar](max) NULL,
	[CustomerLike] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderLines] PRIMARY KEY CLUSTERED 
(
	[OrderLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentHistories]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentHistories](
	[PaymentHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](max) NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[PaymentMethodId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[CurrencyId] [int] NULL,
	[Status] [int] NOT NULL,
	[ResponseJSON] [nvarchar](max) NULL,
	[CallbackURL] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentHistories] PRIMARY KEY CLUSTERED 
(
	[PaymentHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[PaymentMethodId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Picture] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[PaymentMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[ReservationId] [int] IDENTITY(1,1) NOT NULL,
	[TableNumber] [int] NOT NULL,
	[Capacity] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[StoreId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ReservationTime] [datetime] NULL,
	[CustomerName] [nvarchar](max) NULL,
	[CustomerPhone] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoredDomainEvents]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoredDomainEvents](
	[Id] [uniqueidentifier] NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
	[Employee] [nvarchar](100) NOT NULL,
	[CorrelationId] [nvarchar](100) NOT NULL,
	[AggregateId] [uniqueidentifier] NOT NULL,
	[AggregateNumberId] [int] NOT NULL,
	[Action] [varchar](100) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StoredDomainEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoredDomainNotifications]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoredDomainNotifications](
	[Id] [uniqueidentifier] NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
	[User] [nvarchar](100) NOT NULL,
	[CorrelationId] [nvarchar](100) NOT NULL,
	[AggregateId] [uniqueidentifier] NOT NULL,
	[AggregateNumberId] [int] NOT NULL,
	[MessageType] [nvarchar](100) NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](1024) NOT NULL,
	[Code] [nvarchar](100) NOT NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_StoredDomainNotifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[StoreId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CityId] [int] NOT NULL,
	[DistrictId] [int] NOT NULL,
	[WardId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TaxCode] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NOT NULL,
	[GpsLocation] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[Logo] [nvarchar](max) NULL,
	[BankBranch] [nvarchar](max) NULL,
	[BankCode] [nvarchar](max) NULL,
	[BankAccount] [nvarchar](max) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wards]    Script Date: 9/12/2024 8:25:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wards](
	[WardId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[NameEn] [nvarchar](max) NOT NULL,
	[Fullname] [nvarchar](max) NOT NULL,
	[FullnameEn] [nvarchar](max) NOT NULL,
	[CodeName] [nvarchar](max) NOT NULL,
	[DistrictId] [int] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[NumberId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Wards] PRIMARY KEY CLUSTERED 
(
	[WardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoryItems]  WITH CHECK ADD  CONSTRAINT [FK_CategoryItem_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[CategoryItems] CHECK CONSTRAINT [FK_CategoryItem_Category_CategoryId]
GO
ALTER TABLE [dbo].[CategoryItems]  WITH CHECK ADD  CONSTRAINT [FK_CategoryItem_Item_ItemId] FOREIGN KEY([ItemId])
REFERENCES [dbo].[MenuItems] ([ItemId])
GO
ALTER TABLE [dbo].[CategoryItems] CHECK CONSTRAINT [FK_CategoryItem_Item_ItemId]
GO
ALTER TABLE [dbo].[Districts]  WITH CHECK ADD  CONSTRAINT [FK_District_City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([CityId])
GO
ALTER TABLE [dbo].[Districts] CHECK CONSTRAINT [FK_District_City_CityId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employee_Store_StoreId]
GO
ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menu_Store_StoreId]
GO
ALTER TABLE [dbo].[OrderHeaders]  WITH CHECK ADD  CONSTRAINT [FK_Order_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[OrderHeaders] CHECK CONSTRAINT [FK_Order_Store_StoreId]
GO
ALTER TABLE [dbo].[OrderHeaders]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_PaymentMethod_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([PaymentMethodId])
GO
ALTER TABLE [dbo].[OrderHeaders] CHECK CONSTRAINT [FK_OrderHeader_PaymentMethod_PaymentMethodId]
GO
ALTER TABLE [dbo].[OrderHeaders]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_Reservation_ReservationId] FOREIGN KEY([ReservationId])
REFERENCES [dbo].[Reservations] ([ReservationId])
GO
ALTER TABLE [dbo].[OrderHeaders] CHECK CONSTRAINT [FK_OrderHeader_Reservation_ReservationId]
GO
ALTER TABLE [dbo].[OrderLines]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_Item_ItemId] FOREIGN KEY([ItemId])
REFERENCES [dbo].[MenuItems] ([ItemId])
GO
ALTER TABLE [dbo].[OrderLines] CHECK CONSTRAINT [FK_OrderLine_Item_ItemId]
GO
ALTER TABLE [dbo].[OrderLines]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[OrderHeaders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderLines] CHECK CONSTRAINT [FK_OrderLine_Order_OrderId]
GO
ALTER TABLE [dbo].[PaymentHistories]  WITH CHECK ADD  CONSTRAINT [FK_PaymentHistory_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[OrderHeaders] ([OrderId])
GO
ALTER TABLE [dbo].[PaymentHistories] CHECK CONSTRAINT [FK_PaymentHistory_Order_OrderId]
GO
ALTER TABLE [dbo].[PaymentHistories]  WITH CHECK ADD  CONSTRAINT [FK_PaymentHistory_PaymentMethod_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([PaymentMethodId])
GO
ALTER TABLE [dbo].[PaymentHistories] CHECK CONSTRAINT [FK_PaymentHistory_PaymentMethod_PaymentMethodId]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservation_Store_StoreId]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Store_City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([CityId])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Store_City_CityId]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Store_District_DistrictId] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([DistrictId])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Store_District_DistrictId]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Store_Ward_WardId] FOREIGN KEY([WardId])
REFERENCES [dbo].[Wards] ([WardId])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Store_Ward_WardId]
GO
ALTER TABLE [dbo].[Wards]  WITH CHECK ADD  CONSTRAINT [FK_Ward_District_DistrictId] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([DistrictId])
GO
ALTER TABLE [dbo].[Wards] CHECK CONSTRAINT [FK_Ward_District_DistrictId]
GO
