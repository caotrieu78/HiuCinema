USE master
GO
IF EXISTS (SELECT * FROM sysdatabases WHERE name='HiuCinemaDB') --EXISTS (nếu có rồi thì)
DROP DATABASE HiuCinemaDB
GO
CREATE DATABASE HiuCinemaDB
GO
USE HiuCinemaDB
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/19/2024 02:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Role] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [FullName], [Role]) VALUES (1, N'admin', N'123456', N'admin@gmail.com', N'Admin User', N'Admin')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [FullName], [Role]) VALUES (2, N'triều', N'123456', N'caotrieu@gmail.com', N'Triều(Khách)', N'Customer')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [FullName], [Role]) VALUES (3, N'yến', N'123456', N'yen@gmail.com', N'Hải Yến', N'khách')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [FullName], [Role]) VALUES (4, N'trí', N'123456', N'tri@gmail.com', N'Minh trí', N'khách')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [FullName], [Role]) VALUES (5, N'triều 3', N'123456', N'caotrieu0708@gmail.com', N'Cao Triều', N'Admin')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [FullName], [Role]) VALUES (6, N'trường', N'123456', N'truong@gmail.com', N'Nhật Trường', N'khách')
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[Theaters]    Script Date: 03/19/2024 02:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Theaters](
	[TheaterId] [int] IDENTITY(1,1) NOT NULL,
	[TheaterName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TheaterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Theaters] ON
INSERT [dbo].[Theaters] ([TheaterId], [TheaterName]) VALUES (1, N'Theater A')
INSERT [dbo].[Theaters] ([TheaterId], [TheaterName]) VALUES (2, N'Theater B')
INSERT [dbo].[Theaters] ([TheaterId], [TheaterName]) VALUES (3, N'Theater C')
SET IDENTITY_INSERT [dbo].[Theaters] OFF
/****** Object:  Table [dbo].[Movies]    Script Date: 03/19/2024 02:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ReleaseDate] [date] NULL,
	[Duration] [int] NULL,
	[Genre] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[img] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Movies] ON
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [ReleaseDate], [Duration], [Genre], [Age], [img]) VALUES (1, N'Kung Fu Panda 4', N'Po và đồng đội của mình tiếp tục phiêu lưu trên con đường trở thành những chiến binh kung fu hàng đầu, đối mặt với các thách thức mới và phải đối đầu với kẻ thù mạnh mẽ.', CAST(0x68480B00 AS Date), 90, N'Animation, Action, Adventure', 6, N'm-1.jpg')
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [ReleaseDate], [Duration], [Genre], [Age], [img]) VALUES (2, N'Mai', N'Mai là một câu chuyện tình yêu về sự hy sinh, cống hiến và lòng trung thành của một người phụ nữ với gia đình và người thân.', CAST(0xA7470B00 AS Date), 120, N'Drama, Romance', 16, N'mv-2.jpg')
INSERT [dbo].[Movies] ([MovieId], [Title], [Description], [ReleaseDate], [Duration], [Genre], [Age], [img]) VALUES (3, N'Dune 2', N'Tiếp tục cuộc phiêu lưu của Paul Atreides trên hành tinh Arrakis, nơi anh phải đối mặt với sự phản đối và những cuộc xung đột mới giữa các gia tộc.', CAST(0x214A0B00 AS Date), 150, N'Adventure, Drama, Sci-Fi', 14, N'm-3.jpg')
SET IDENTITY_INSERT [dbo].[Movies] OFF
/****** Object:  Table [dbo].[Rating]    Script Date: 03/19/2024 02:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[MovieId] [int] NULL,
	[UserId] [int] NULL,
	[RatingValue] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[RatingDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Rating] ON
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (1, 1, 2, 5, N'Great movie!', CAST(0x0000B13300A4CB80 AS DateTime))
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (2, 2, 3, 4, N'Classic!', CAST(0x0000B13300B54640 AS DateTime))
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (3, 3, 1, 5, N'Best superhero movie!', CAST(0x0000B13300C5C100 AS DateTime))
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (4, 3, 4, 0, N'êr', NULL)
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (5, 1, 4, 0, NULL, NULL)
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (6, 1, 4, 0, NULL, NULL)
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (7, 1, 4, 0, NULL, CAST(0x0000B138000FCA13 AS DateTime))
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (8, 1, 4, 4, N'as', CAST(0x0000B1380010EE18 AS DateTime))
INSERT [dbo].[Rating] ([RatingId], [MovieId], [UserId], [RatingValue], [Comment], [RatingDate]) VALUES (9, 1, 4, 4, N'asss', CAST(0x0000B1380016928E AS DateTime))
SET IDENTITY_INSERT [dbo].[Rating] OFF
/****** Object:  Table [dbo].[Showtimes]    Script Date: 03/19/2024 02:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Showtimes](
	[ShowtimeId] [int] IDENTITY(1,1) NOT NULL,
	[MovieId] [int] NULL,
	[TheaterId] [int] NULL,
	[ShowDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ShowtimeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Showtimes] ON
INSERT [dbo].[Showtimes] ([ShowtimeId], [MovieId], [TheaterId], [ShowDateTime]) VALUES (1, 1, 1, CAST(0x0000B13401391C40 AS DateTime))
INSERT [dbo].[Showtimes] ([ShowtimeId], [MovieId], [TheaterId], [ShowDateTime]) VALUES (2, 2, 2, CAST(0x0000B13401499700 AS DateTime))
INSERT [dbo].[Showtimes] ([ShowtimeId], [MovieId], [TheaterId], [ShowDateTime]) VALUES (3, 3, 3, CAST(0x0000B135015A11C0 AS DateTime))
SET IDENTITY_INSERT [dbo].[Showtimes] OFF
/****** Object:  Table [dbo].[Seats]    Script Date: 03/19/2024 02:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[SeatId] [int] IDENTITY(1,1) NOT NULL,
	[TheaterId] [int] NULL,
	[nRow] [nvarchar](5) NULL,
	[nColumn] [int] NULL,
	[Price] [decimal](10, 2) NULL,
	[IsBooked] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SeatId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Seats] ON
DECLARE @TheaterId INT = 1
DECLARE @Price DECIMAL(10, 2) = 50000.00
DECLARE @SeatId INT = 1

DECLARE @RowChar CHAR(1)
DECLARE @Column INT

-- Loop through rows from 'A' to 'Q'
SET @RowChar = 'A'
WHILE @RowChar <= 'Q'
BEGIN
    SET @Column = 1
    -- Loop through columns from 1 to 18
    WHILE @Column <= 18
    BEGIN
        -- Insert seat data
        INSERT INTO [dbo].[Seats] ([SeatId], [TheaterId], [nRow], [nColumn], [Price], [IsBooked])
        VALUES (@SeatId, @TheaterId, @RowChar, @Column, @Price, 0)
        
        SET @Column = @Column + 1
        SET @SeatId = @SeatId + 1
    END
    
    SET @RowChar = CHAR(ASCII(@RowChar) + 1)
END
SET IDENTITY_INSERT [dbo].[Seats] OFF
/****** Object:  Table [dbo].[SeatBookings]    Script Date: 03/19/2024 02:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatBookings](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[ShowtimeId] [int] NULL,
	[SeatId] [int] NULL,
	[UserId] [int] NULL,
	[BookingDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SeatBookings] ON

INSERT [dbo].[SeatBookings] ([BookingId], [ShowtimeId], [SeatId], [UserId], [BookingDate]) VALUES (25, 1, 14, 4, CAST(0x0000B138000BEBBD AS DateTime))
SET IDENTITY_INSERT [dbo].[SeatBookings] OFF
/****** Object:  Default [DF__Seats__IsBooked__1273C1CD]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[Seats] ADD  DEFAULT ((0)) FOR [IsBooked]
GO
/****** Object:  ForeignKey [FK__Rating__MovieId__1ED998B2]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([MovieId])
GO
/****** Object:  ForeignKey [FK__Rating__UserId__1FCDBCEB]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__SeatBooki__SeatI__1920BF5C]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[SeatBookings]  WITH CHECK ADD FOREIGN KEY([SeatId])
REFERENCES [dbo].[Seats] ([SeatId])
GO
/****** Object:  ForeignKey [FK__SeatBooki__Showt__182C9B23]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[SeatBookings]  WITH CHECK ADD FOREIGN KEY([ShowtimeId])
REFERENCES [dbo].[Showtimes] ([ShowtimeId])
GO
/****** Object:  ForeignKey [FK__SeatBooki__UserI__1A14E395]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[SeatBookings]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
/****** Object:  ForeignKey [FK__Seats__TheaterId__1367E606]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD FOREIGN KEY([TheaterId])
REFERENCES [dbo].[Theaters] ([TheaterId])
GO
/****** Object:  ForeignKey [FK__Showtimes__Movie__0CBAE877]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([MovieId])
GO
/****** Object:  ForeignKey [FK__Showtimes__Theat__0DAF0CB0]    Script Date: 03/19/2024 02:19:38 ******/
ALTER TABLE [dbo].[Showtimes]  WITH CHECK ADD FOREIGN KEY([TheaterId])
REFERENCES [dbo].[Theaters] ([TheaterId])
GO
