/****** Object:  StoredProcedure [dbo].[usp_LoadExam]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LoadExam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_LoadExam]
GO
/****** Object:  StoredProcedure [dbo].[usp_LoadExam_Detailed]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LoadExam_Detailed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_LoadExam_Detailed]
GO
/****** Object:  StoredProcedure [dbo].[usp_System_Modules_Load]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_System_Modules_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_System_Modules_Load]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_Scores_Desc]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_Scores_Desc]'))
DROP VIEW [dbo].[uvw_RecruitmentTestExams_Scores_Desc]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestUser_Rights]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestUser_Rights]'))
DROP VIEW [dbo].[uvw_RecruitmentTestUser_Rights]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestRights_Details]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestRights_Details]'))
DROP VIEW [dbo].[uvw_RecruitmentTestRights_Details]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_Scores]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_Scores]'))
DROP VIEW [dbo].[uvw_RecruitmentTestExams_Scores]
GO
/****** Object:  StoredProcedure [dbo].[usp_GenerateExam]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GenerateExam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GenerateExam]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_QuestionAnswers]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_QuestionAnswers]'))
DROP VIEW [dbo].[uvw_RecruitmentTestExams_QuestionAnswers]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_Questions]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_Questions]'))
DROP VIEW [dbo].[uvw_RecruitmentTestExams_Questions]
GO
/****** Object:  StoredProcedure [dbo].[usp_Get_System_Parameter]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Get_System_Parameter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Get_System_Parameter]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestQuestionAnswers]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestQuestionAnswers]'))
DROP VIEW [dbo].[uvw_RecruitmentTestQuestionAnswers]
GO
/****** Object:  View [dbo].[uvw_System_Modules_Access]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_System_Modules_Access]'))
DROP VIEW [dbo].[uvw_System_Modules_Access]
GO
/****** Object:  View [dbo].[uvw_System_Modules_AccessLib]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_System_Modules_AccessLib]'))
DROP VIEW [dbo].[uvw_System_Modules_AccessLib]
GO
/****** Object:  View [dbo].[uvw_System_Modules]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_System_Modules]'))
DROP VIEW [dbo].[uvw_System_Modules]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestQuestions]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestQuestions]'))
DROP VIEW [dbo].[uvw_RecruitmentTestQuestions]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestUser]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestUser]'))
DROP VIEW [dbo].[uvw_RecruitmentTestUser]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetTableDef]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetTableDef]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetTableDef]
GO
/****** Object:  StoredProcedure [dbo].[usp_Require_System_Parameter]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Require_System_Parameter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Require_System_Parameter]
GO
/****** Object:  StoredProcedure [dbo].[usp_Set_System_Parameter]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Set_System_Parameter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Set_System_Parameter]
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams]'))
DROP VIEW [dbo].[uvw_RecruitmentTestExams]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_Get_System_Parameter]    Script Date: 04/11/2012 00:16:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Get_System_Parameter]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_Get_System_Parameter]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetTableDef]    Script Date: 04/11/2012 00:16:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetTableDef]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetTableDef]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetTimeLength]    Script Date: 04/11/2012 00:16:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetTimeLength]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetTimeLength]
GO
/****** Object:  StoredProcedure [dbo].[usp_RecruitmentTestRights_Details_Load]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RecruitmentTestRights_Details_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RecruitmentTestRights_Details_Load]
GO
/****** Object:  StoredProcedure [dbo].[usp_RecruitmentTestUser_Rights_Load]    Script Date: 04/11/2012 00:16:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RecruitmentTestUser_Rights_Load]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RecruitmentTestUser_Rights_Load]
GO
