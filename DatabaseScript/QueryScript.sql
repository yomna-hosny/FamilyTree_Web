USE [master]
GO
/****** Object:  Database [FamilyTree]    Script Date: 21/05/2021 05:22:25 ص ******/
CREATE DATABASE [FamilyTree]
ALTER DATABASE [FamilyTree] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FamilyTree].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FamilyTree] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FamilyTree] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FamilyTree] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FamilyTree] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FamilyTree] SET ARITHABORT OFF 
GO
ALTER DATABASE [FamilyTree] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FamilyTree] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FamilyTree] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FamilyTree] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FamilyTree] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FamilyTree] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FamilyTree] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FamilyTree] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FamilyTree] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FamilyTree] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FamilyTree] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FamilyTree] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FamilyTree] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FamilyTree] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FamilyTree] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FamilyTree] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FamilyTree] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FamilyTree] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FamilyTree] SET  MULTI_USER 
GO
ALTER DATABASE [FamilyTree] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FamilyTree] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FamilyTree] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FamilyTree] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FamilyTree] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FamilyTree] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FamilyTree] SET QUERY_STORE = OFF
GO
USE [FamilyTree]
GO
/****** Object:  Table [dbo].[Family]    Script Date: 21/05/2021 05:22:25 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Family](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Comment] [nvarchar](70) NULL,
 CONSTRAINT [PK_Family] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 21/05/2021 05:22:25 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[Gender] [bit] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[DateOfDeath] [date] NULL,
	[Image] [nvarchar](50) NULL,
	[FamilyId] [int] NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RelationShip]    Script Date: 21/05/2021 05:22:25 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationShip](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Member1] [int] NOT NULL,
	[Member2] [int] NOT NULL,
	[Member1_RelationType] [int] NOT NULL,
	[Member2_RelationType] [int] NOT NULL,
	[FamilyId] [int] NOT NULL,
	[RelationStart] [date] NULL,
	[RelationEnd] [date] NULL,
 CONSTRAINT [PK_RelationShip] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RelationType]    Script Date: 21/05/2021 05:22:25 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_RelationType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Family] ON 

INSERT [dbo].[Family] ([Id], [Name], [Comment]) VALUES (1, N'family1', NULL)
INSERT [dbo].[Family] ([Id], [Name], [Comment]) VALUES (2, N'family2', NULL)
INSERT [dbo].[Family] ([Id], [Name], [Comment]) VALUES (3, N'family3', NULL)
INSERT [dbo].[Family] ([Id], [Name], [Comment]) VALUES (4, N'family4', NULL)
INSERT [dbo].[Family] ([Id], [Name], [Comment]) VALUES (5, N'family5', NULL)
SET IDENTITY_INSERT [dbo].[Family] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (2, N'YOMNAa', N'HOSNY', 1, CAST(N'1998-07-11' AS Date), NULL, NULL, 1)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (4, N'Abd0', N'Ahmed', 0, CAST(N'1998-09-20' AS Date), NULL, NULL, 2)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (9, N'Rehap', N'Hosny', 1, CAST(N'2004-02-03' AS Date), NULL, NULL, 1)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (10, N'Ahmed', N'Hosny', 0, CAST(N'1994-09-05' AS Date), NULL, NULL, 1)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (11, N'Marwa', N'Hosny', 1, CAST(N'2003-03-03' AS Date), NULL, NULL, 1)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (12, N'Amira', N'Hosny', 1, CAST(N'1998-05-01' AS Date), NULL, NULL, 1)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (13, N'Amr', N'Hosny', 0, CAST(N'1992-02-02' AS Date), NULL, NULL, 1)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (14, N'Sanaa', N'Mosaa', 1, CAST(N'1973-03-27' AS Date), NULL, NULL, 1)
INSERT [dbo].[Member] ([Id], [FirstName], [LastName], [Gender], [DateOfBirth], [DateOfDeath], [Image], [FamilyId]) VALUES (15, N'Hosny', N'Abd Al Aziez', 0, CAST(N'1955-01-01' AS Date), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
SET IDENTITY_INSERT [dbo].[RelationShip] ON 

INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (2, 2, 4, 1, 1, 1, CAST(N'2016-02-02' AS Date), NULL)
INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (3, 2, 9, 4, 4, 1, CAST(N'2004-02-03' AS Date), NULL)
INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (6, 2, 10, 4, 4, 1, CAST(N'2002-02-02' AS Date), NULL)
INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (7, 2, 11, 4, 4, 1, CAST(N'2003-03-03' AS Date), NULL)
INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (8, 2, 12, 4, 4, 1, CAST(N'2002-02-02' AS Date), NULL)
INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (9, 2, 13, 4, 4, 1, CAST(N'2003-03-03' AS Date), NULL)
INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (11, 14, 2, 2, 3, 1, CAST(N'1998-07-11' AS Date), NULL)
INSERT [dbo].[RelationShip] ([Id], [Member1], [Member2], [Member1_RelationType], [Member2_RelationType], [FamilyId], [RelationStart], [RelationEnd]) VALUES (32, 11, 15, 3, 5, 1, CAST(N'2003-03-03' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[RelationShip] OFF
GO
SET IDENTITY_INSERT [dbo].[RelationType] ON 

INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (1, N'Partner', N'Wife Or Husband')
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (2, N'Mother', NULL)
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (3, N'Child', NULL)
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (4, N'Sibling', NULL)
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (5, N'Father', NULL)
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (6, N'Aunt', NULL)
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (7, N'Cousine', NULL)
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (8, N'Uncle', NULL)
INSERT [dbo].[RelationType] ([Id], [Name], [Description]) VALUES (9, N'Children of sibling', NULL)
SET IDENTITY_INSERT [dbo].[RelationType] OFF
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Family] FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Family] ([Id])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_Family]
GO
ALTER TABLE [dbo].[RelationShip]  WITH CHECK ADD  CONSTRAINT [FK_RelationShip_Family] FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Family] ([Id])
GO
ALTER TABLE [dbo].[RelationShip] CHECK CONSTRAINT [FK_RelationShip_Family]
GO
ALTER TABLE [dbo].[RelationShip]  WITH CHECK ADD  CONSTRAINT [FK_RelationShip_Member] FOREIGN KEY([Member1])
REFERENCES [dbo].[Member] ([Id])
GO
ALTER TABLE [dbo].[RelationShip] CHECK CONSTRAINT [FK_RelationShip_Member]
GO
ALTER TABLE [dbo].[RelationShip]  WITH CHECK ADD  CONSTRAINT [FK_RelationShip_Member1] FOREIGN KEY([Member2])
REFERENCES [dbo].[Member] ([Id])
GO
ALTER TABLE [dbo].[RelationShip] CHECK CONSTRAINT [FK_RelationShip_Member1]
GO
ALTER TABLE [dbo].[RelationShip]  WITH CHECK ADD  CONSTRAINT [FK_RelationShip_RelationType] FOREIGN KEY([Member1_RelationType])
REFERENCES [dbo].[RelationType] ([Id])
GO
ALTER TABLE [dbo].[RelationShip] CHECK CONSTRAINT [FK_RelationShip_RelationType]
GO
ALTER TABLE [dbo].[RelationShip]  WITH CHECK ADD  CONSTRAINT [FK_RelationShip_RelationType1] FOREIGN KEY([Member2_RelationType])
REFERENCES [dbo].[RelationType] ([Id])
GO
ALTER TABLE [dbo].[RelationShip] CHECK CONSTRAINT [FK_RelationShip_RelationType1]
GO
USE [master]
GO
ALTER DATABASE [FamilyTree] SET  READ_WRITE 
GO
