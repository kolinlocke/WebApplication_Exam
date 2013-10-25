/****** Object:  Table [dbo].[System_Modules_AccessLib]    Script Date: 04/11/2012 00:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[System_Modules_AccessLib]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[System_Modules_AccessLib](
	[System_Modules_AccessLibID] [bigint] NOT NULL,
	[Desc] [varchar](50) NULL,
 CONSTRAINT [PK_System_Modules_AccessLib] PRIMARY KEY CLUSTERED 
(
	[System_Modules_AccessLibID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[System_Modules_Access]    Script Date: 04/11/2012 00:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[System_Modules_Access]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[System_Modules_Access](
	[System_Modules_AccessID] [bigint] NOT NULL,
	[System_ModulesID] [bigint] NULL,
	[System_Modules_AccessLibID] [bigint] NULL,
 CONSTRAINT [PK_System_Modules_Access] PRIMARY KEY CLUSTERED 
(
	[System_Modules_AccessID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[System_Modules]    Script Date: 04/11/2012 00:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[System_Modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[System_Modules](
	[System_ModulesID] [bigint] NOT NULL,
	[System_ModulesID_Parent] [varchar](1000) NULL,
	[Name] [varchar](1000) NULL,
	[Code] [varchar](1000) NULL,
	[Module_List] [varchar](1000) NULL,
	[Module_Details] [varchar](1000) NULL,
	[Arguments] [varchar](1000) NULL,
	[IsHidden] [bit] NULL,
	[OrderIndex] [int] NULL,
 CONSTRAINT [PK_System_Modules] PRIMARY KEY CLUSTERED 
(
	[System_ModulesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RecruitmentTestUser_Rights]    Script Date: 04/11/2012 00:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecruitmentTestUser_Rights]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RecruitmentTestUser_Rights](
	[RecruitmentTestUser_RightsID] [bigint] IDENTITY(1,1) NOT NULL,
	[RecruitmentTestUserID] [bigint] NULL,
	[RecruitmentTestRightsID] [bigint] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_RecruitmentTestUser_Rights] PRIMARY KEY CLUSTERED 
(
	[RecruitmentTestUser_RightsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RecruitmentTestRights_Details]    Script Date: 04/11/2012 00:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecruitmentTestRights_Details]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RecruitmentTestRights_Details](
	[RecruitmentTestRights_DetailsID] [bigint] IDENTITY(1,1) NOT NULL,
	[RecruitmentTestRightsID] [bigint] NULL,
	[System_Modules_AccessID] [bigint] NULL,
	[IsAllowed] [bit] NULL,
 CONSTRAINT [PK_RecruitmentTestRights_Details] PRIMARY KEY CLUSTERED 
(
	[RecruitmentTestRights_DetailsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RecruitmentTestRights]    Script Date: 04/11/2012 00:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecruitmentTestRights]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RecruitmentTestRights](
	[RecruitmentTestRightsID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_RecruitmentTestRights] PRIMARY KEY CLUSTERED 
(
	[RecruitmentTestRightsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
