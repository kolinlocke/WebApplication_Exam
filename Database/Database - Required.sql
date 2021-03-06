USE [Exam]
GO
/****** Object:  Table [dbo].[System_Parameters]    Script Date: 03/17/2012 00:14:53 ******/
INSERT [dbo].[System_Parameters] ([ParameterName], [ParameterValue]) VALUES (N'Administrator_Password', N'Password')
INSERT [dbo].[System_Parameters] ([ParameterName], [ParameterValue]) VALUES (N'Exam_NoItemsTotal', N'20')
INSERT [dbo].[System_Parameters] ([ParameterName], [ParameterValue]) VALUES (N'Exam_NoItemsPerPage', N'5')
INSERT [dbo].[System_Parameters] ([ParameterName], [ParameterValue]) VALUES (N'Exam_NoRequiredAnswers', N'2')
/****** Object:  Table [dbo].[LookupUserType]    Script Date: 03/17/2012 00:14:53 ******/
INSERT [dbo].[LookupUserType] ([LookupUserTypeID], [Desc]) VALUES (1, N'Administrator')
INSERT [dbo].[LookupUserType] ([LookupUserTypeID], [Desc]) VALUES (2, N'Contributor')
INSERT [dbo].[LookupUserType] ([LookupUserTypeID], [Desc]) VALUES (3, N'HR')
/****** Object:  Table [dbo].[LookupQuestionType]    Script Date: 03/17/2012 00:14:53 ******/
INSERT [dbo].[LookupQuestionType] ([LookupQuestionTypeID], [Desc]) VALUES (1, N'Single Answer')
INSERT [dbo].[LookupQuestionType] ([LookupQuestionTypeID], [Desc]) VALUES (2, N'Multiple Answer')
/****** Object:  Table [dbo].[LookupCategory]    Script Date: 03/17/2012 00:14:53 ******/
SET IDENTITY_INSERT [dbo].[LookupCategory] ON
INSERT [dbo].[LookupCategory] ([LookupCategoryID], [Desc]) VALUES (1, N'.Net')
INSERT [dbo].[LookupCategory] ([LookupCategoryID], [Desc]) VALUES (2, N'Java')
INSERT [dbo].[LookupCategory] ([LookupCategoryID], [Desc]) VALUES (3, N'iOS')
SET IDENTITY_INSERT [dbo].[LookupCategory] OFF
