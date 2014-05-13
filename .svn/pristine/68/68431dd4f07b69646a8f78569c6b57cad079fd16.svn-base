/****** Object:  StoredProcedure [dbo].[usp_RecruitmentTestUser_Rights_Load]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RecruitmentTestUser_Rights_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'Create Procedure [dbo].[usp_RecruitmentTestUser_Rights_Load]
@ID As BigInt = 0
As
Begin
	Declare @Condition VarChar(Max)
	If @ID = 0
	Begin
		Set @Condition = ''1 = 0''
	End
	Else
	Begin
		Set @Condition = ''RecruitmentTestUserID = '' + Cast(@ID As VarChar(50))
	End
	
	Declare @Query VarChar(Max)
	Set @Query =
		''		
		Select
			[Tb].RecruitmentTestUser_RightsID
			, [Tb].RecruitmentTestUserID
			, [Tb].IsActive
			, [R].RecruitmentTestRightsID
			, [R].Name As [Rights_Name]
		From
			(
			Select *
			From RecruitmentTestUser_Rights
			Where '' + @Condition + ''
			) As [Tb]
			Right Join [RecruitmentTestRights] As [R]
				On [R].RecruitmentTestRightsID = [Tb].RecruitmentTestRightsID
		''
		Exec(@Query)

End
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_RecruitmentTestRights_Details_Load]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RecruitmentTestRights_Details_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[usp_RecruitmentTestRights_Details_Load]
@ID As BigInt = 0
As
Begin
	Declare @Condition VarChar(Max)
	If @ID = 0
	Begin
		Set @Condition = ''1 = 0''
	End
	Else
	Begin
		Set @Condition = ''RecruitmentTestRightsID = '' + Cast(@ID As VarChar(50))
	End
	
	Declare @Query VarChar(Max)
	Set @Query =
		''
		Select
			[Rd].RecruitmentTestRights_DetailsID
			, [Rd].RecruitmentTestRightsID
			, [Rd].[IsAllowed]
			, [Sma].System_ModulesID_Parent
			, [Sma].System_ModulesID
			, [Sma].System_Modules_AccessID
			, [Sma].System_Modules_AccessLibID
			, [Sma].[Parent_Name] As [Module_Parent_Name]
			, [Sma].[Module_Name]
			, [Sma].[Module_Code]
			, [Sma].[Access_Desc]
		From
			(
			Select [Rd].*
			From RecruitmentTestRights_Details As [Rd]
			Where '' + @Condition + ''
			) As [Rd]
			Right Join uvw_System_Modules_Access As [Sma]
				On [Sma].[System_Modules_AccessID] = [Rd].[System_Modules_AccessID]
		''
		Exec(@Query)

End

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetTimeLength]    Script Date: 04/11/2012 00:15:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetTimeLength]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE Function [dbo].[udf_GetTimeLength]
(
	@DateStart As DateTime
	, @DateEnd As DateTime
)
Returns VarChar(1000)
As
Begin
	Declare @Time_Second_Ex Int
	Set @Time_Second_Ex = DATEDIFF(SECOND, @DateStart, @DateEnd)

	Declare @Time_Minute_Ex As Int
	Set @Time_Minute_Ex = 0

	If @Time_Second_Ex >= 60
	Set @Time_Minute_Ex = @Time_Second_Ex / 60

	Declare @Time_Hour As Int
	Set @Time_Hour = 0

	If @Time_Minute_Ex >= 60
	Set @Time_Hour = @Time_Minute_Ex / 60

	Declare @Time_Second As Int
	Set @Time_Second = @Time_Second_Ex - (@Time_Minute_Ex * 60)

	Declare @Time_Minute As Int
	Set @Time_Minute = @Time_Minute_Ex - (@Time_Hour * 60)

	Declare @Time_Hour_St As VarChar(100)
	Set @Time_Hour_St = ''''

	If @Time_Hour > 0
	Set @Time_Hour_St = '' '' + Cast (@Time_Hour As VarChar(50)) +  '' hour(s) ''

	Declare @Time_Minute_St As VarChar(50)
	Set @Time_Minute_St = ''''

	If @Time_Minute > 0
	Set @Time_Minute_St = '' '' + Cast (@Time_Minute As VarChar(50)) +  '' minute(s) ''

	Declare @Time_Second_St As VarChar(50)
	Set @Time_Second_St = ''''

	If @Time_Second > 0
	Set @Time_Second_St = '' '' + Cast (@Time_Second As VarChar(50)) +  '' second(s) ''

	Declare @Rs As VarChar(50)
	Set @Rs = @Time_Hour_St + @Time_Minute_St + @Time_Second_St
	
	Return @Rs
End
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetTableDef]    Script Date: 04/11/2012 00:15:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetTableDef]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE Function [dbo].[udf_GetTableDef]
(
	@TableName VarChar(Max)
)	
Returns Table
As
Return
	(
	Select
		sCol.Column_id
		, sCol.Name As [ColumnName]
		, sTyp.Name As [DataType]
		, sCol.max_length As [Length]
		, sCol.Precision
		, sCol.Scale
		, sCol.Is_Identity As [IsIdentity]
		, Cast
		(
			(
			Case Count(IsCcu.Column_Name)
				When 0 Then 0
				Else 1
			End
			) 
		As Bit) As IsPk
	From 
		Sys.Columns As sCol
		Left Join Sys.Types As sTyp
			On sCol.system_type_id = sTyp.system_type_id
			And [sCol].User_Type_ID = [sTyp].User_Type_ID
		Inner Join Sys.Tables As sTab
			On sCol.Object_ID = sTab.Object_ID
		Inner Join Sys.Schemas As sSch
			On sSch.Schema_ID = sTab.Schema_ID
		Left Join Sys.Key_Constraints As Skc
			On sTab.Object_Id = Skc.Parent_Object_Id
			And Skc.Type = ''PK''
		Left Join INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE As IsCcu
			On Skc.Name = IsCcu.Constraint_Name
			And sTab.Name = IsCcu.Table_Name
			And sCol.Name = IsCcu.Column_Name
	Where
		sSch.Name + ''.'' + sTab.Name = @TableName
		And sCol.Is_Computed = 0
	Group By
		sCol.Name
		, sTyp.Name
		, sCol.max_length
		, sCol.Precision
		, sCol.Scale
		, sCol.Is_Identity
		, sCol.Column_id
	)

' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[udf_Get_System_Parameter]    Script Date: 04/11/2012 00:15:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Get_System_Parameter]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
Create Function [dbo].[udf_Get_System_Parameter]
(
@ParameterName VarChar(Max)
)
Returns VarChar(Max)
As
Begin
	Declare @ParameterValue As VarChar(Max)		
	Set @ParameterValue = ''''
	
	Declare @Ct As Int	
	Select @Ct = Count(1)
	From System_Parameters
	Where ParameterName = @ParameterName
	
	If @Ct = 0
	Begin
		Return ''''
	End
	Else
	Begin
		Select @ParameterValue = ParameterValue
		From System_Parameters
		Where ParameterName = @ParameterName
	End
	
	Return @ParameterValue
End


' 
END
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestExams]
As
	Select
		[Tb].*
		, [A].Name As [RecruitmentTestApplicant_Name]
		, [A].Email As [RecruitmentTestApplicant_Email]
		, [dbo].[udf_GetTimeLength]([Tb].DateStart, [Tb].DateEnd) As [Time]
		, DATEDIFF(SECOND, [Tb].DateStart, [Tb].DateEnd) As [Time_Value]
	From
		RecruitmentTestExams As [Tb]
		Left Join RecruitmentTestApplicant As [A]
			On [A].RecruitmentTestApplicantID = [Tb].RecruitmentTestApplicantID
			
		
		
'
GO
/****** Object:  StoredProcedure [dbo].[usp_Set_System_Parameter]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Set_System_Parameter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Procedure [dbo].[usp_Set_System_Parameter]
@ParameterName VarChar(Max)
, @ParameterValue VarChar(Max)
As
Begin
	Declare @Ct As Int	
	Select @Ct = Count(1)
	From System_Parameters
	Where ParameterName = @ParameterName
	
	If @Ct = 0
	Begin
		Insert Into System_Parameters (ParameterName, ParameterValue) Values (@ParameterName, @ParameterValue)
	End
	Else
	Begin
		Update System_Parameters Set ParameterValue = @ParameterValue Where ParameterName = @ParameterName
	End
End

' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Require_System_Parameter]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Require_System_Parameter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
Create Proc [dbo].[usp_Require_System_Parameter]
@ParameterName VarChar(Max)
, @ParameterValue VarChar(Max)
As
Begin
	Declare @Ct As Int	
	Select @Ct = Count(1)
	From System_Parameters
	Where ParameterName = @ParameterName
	
	If @Ct = 0
	Begin
		Insert Into System_Parameters (ParameterName, ParameterValue) Values (@ParameterName, @ParameterValue)
	End
End

' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetTableDef]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetTableDef]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[usp_GetTableDef]
@TableName VarChar(Max)
, @SchemaName VarChar(Max) = ''''
As
Set NOCOUNT On
Begin
	
	If IsNull(@SchemaName, '''') = ''''
	Begin
		Set @SchemaName = ''dbo''
	End
	
	Select *
	From [udf_GetTableDef](@SchemaName + ''.'' + @TableName)
	Order By Column_Id
	
End

' 
END
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestUser]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestUser]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[uvw_RecruitmentTestUser]
AS 
	Select
		[Tb].*
		, [LkpUT].[Desc] As [UserType_Desc]
	From 
		RecruitmentTestUser As [Tb]
		Left Join LookupUserType As [LkpUT]
			On [LkpUT].LookupUserTypeID = [Tb].LookupUserTypeID
			'
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestQuestions]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestQuestions]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestQuestions]
As
	Select
		[Tb].*
		, [Lc].[Desc] As [Category_Desc]
		, [Lqt].[Desc] As [QuestionType_Desc]
		, [U_Cb].Name As [UserName_CreatedBy]
		, [U_Ab].Name As [UserName_ApprovedBy]
		, (
		Case
			When [Tb].IsApproved = 1 Then
				''Approved''
			Else
				''Open''
		End
		) As [Status_Desc]
	From
		RecruitmentTestQuestions As [Tb]
		Left Join LookupCategory As [Lc]
			On [Lc].LookupCategoryID = [Tb].LookupCategoryID
		Left Join LookupQuestionType As [Lqt]
			On [Lqt].LookupQuestionTypeID = [Tb].LookupQuestionTypeID
		Left Join RecruitmentTestUser As [U_Cb]
			On [U_Cb].RecruitmentTestUserID = [Tb].RecruitmentTestUserID_CreatedBy
		Left Join RecruitmentTestUser As [U_Ab]
			On [U_Ab].RecruitmentTestUserID = [Tb].RecruitmentTestUserID_ApprovedBy
	'
GO
/****** Object:  View [dbo].[uvw_System_Modules]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_System_Modules]'))
EXEC dbo.sp_executesql @statement = N'Create View [dbo].[uvw_System_Modules]
As
	Select
		[Sm].*
		, [Psm].OrderIndex As [Parent_OrderIndex]
	From 
		System_Modules As [Sm]
		Left Join System_Modules As [Psm]
			On [Psm].System_ModulesID = [Sm].System_ModulesID_Parent
'
GO
/****** Object:  View [dbo].[uvw_System_Modules_AccessLib]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_System_Modules_AccessLib]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_System_Modules_AccessLib]
As
	Select
		Row_Number() Over (Order By System_ModulesID, System_Modules_AccessLibID) As [uvw_System_Modules_AccessID]
		, [Sm].System_ModulesID
		, [Sma].System_Modules_AccessLibID
		, [Sm].[Name] As [Module_Name]
		, [Sm].[Code] As [Module_Code]
		, [Sma].[Desc] As [Access_Desc]
	From
		System_Modules As [Sm]
		, System_Modules_AccessLib As [Sma]
'
GO
/****** Object:  View [dbo].[uvw_System_Modules_Access]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_System_Modules_Access]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_System_Modules_Access]
As
	Select Top (100) Percent
		IsNull([Sm].[Parent_Name],''Root'') As [Parent_Name]
		, [Smal].[Module_Name]
		, [Smal].[Module_Code]
		, [Smal].[Access_Desc]
		, [Sm].System_ModulesID_Parent
		, [Sma].*
	From
		System_Modules_Access As [Sma]
		Left Join uvw_System_Modules_AccessLib As [Smal]
			On [Smal].System_ModulesID = [Sma].System_ModulesID
			And [Smal].System_Modules_AccessLibID = [Sma].System_Modules_AccessLibID
		Left Join
			(
			Select
				[Psm].[Name] As [Parent_Name]
				, [Sm].System_ModulesID
				, [Sm].System_ModulesID_Parent
				, [Sm].OrderIndex
			From 
				System_Modules As [Sm]
				Left Join System_Modules As [Psm]
					On [Psm].System_ModulesID = [Sm].System_ModulesID_Parent
			) As [Sm]
			On [Sm].System_ModulesID = [Sma].System_ModulesID
	Order By
		[Sm].System_ModulesID_Parent
		, [Sm].OrderIndex
'
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestQuestionAnswers]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestQuestionAnswers]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestQuestionAnswers] 
As
	Select 
		[Tb].* 
		, [A].Answer As [Lkp_RecruitmentTestAnswersID_Desc]
	From 
		RecruitmentTestQuestionAnswers As [Tb]
		Left Join RecruitmentTestAnswers As [A]
			On [A].RecruitmentTestAnswersID = [Tb].Lkp_RecruitmentTestAnswersID
'
GO
/****** Object:  StoredProcedure [dbo].[usp_Get_System_Parameter]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Get_System_Parameter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[usp_Get_System_Parameter]
@ParameterName VarChar(Max)
, @DefaultValue VarChar(Max)
As
Begin
	Declare @ParameterValue As VarChar(Max)		
	Set @ParameterValue = ''''
	
	Declare @Ct As Int	
	Select @Ct = Count(1)
	From System_Parameters
	Where ParameterName = @ParameterName
	
	If @Ct = 0
	Begin
		Exec usp_Require_System_Parameter @ParameterName, @DefaultValue
		Set @ParameterValue = @DefaultValue
	End
	Else
	Begin
		Select @ParameterValue = ParameterValue
		From System_Parameters
		Where ParameterName = @ParameterName
	End
	
	Select @ParameterValue As [ParameterValue]
End


' 
END
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_Questions]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_Questions]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestExams_Questions]
As
	Select
		[Eq].RecruitmentTestExamsID
		, [Eq].Lkp_RecruitmentTestQuestionsID
		, [Eq].Ct As [Eq_Ct]
		, [Qa].Ct As [Qa_Ct]
		, Cast(
		(
		Case
			When IsNull([Eq].Ct,0) = IsNull([Qa].Ct,0) Then 1
			Else 0
		End
		)
		As Bit) As [IsCorrect]
	From 
		(
		Select
			[Tb].RecruitmentTestExamsID
			, [Tb].Lkp_RecruitmentTestQuestionsID
			, Sum(Ct) As [Ct]
		From
			(	
			Select 
				[Eq].RecruitmentTestExamsID
				, [Eq].Lkp_RecruitmentTestQuestionsID
				, (
				Case 
					When [Qa].RecruitmentTestQuestionAnswersID Is Not Null Then 1
					Else 0
				End
				) As [Ct]
			From
				RecruitmentTestExams_Questions As [Eq]
				Left Join RecruitmentTestExams_Answers As [Ea]
					On [Ea].RecruitmentTestExamsID = [Eq].RecruitmentTestExamsID
					And [Ea].Lkp_RecruitmentTestQuestionsID = [Eq].Lkp_RecruitmentTestQuestionsID
				Left Join RecruitmentTestQuestionAnswers As [Qa]
					On [Qa].Lkp_RecruitmentTestQuestionsID = [Eq].Lkp_RecruitmentTestQuestionsID
					And [Qa].Lkp_RecruitmentTestAnswersID = [Ea].Lkp_RecruitmentTestAnswersID
					And [Qa].IsAnswer = 1
					And IsNull([Qa].IsDeleted,0) = 0
					And [Ea].IsAnswer = 1
			) As [Tb]
		Group By
			[Tb].RecruitmentTestExamsID
			, [Tb].Lkp_RecruitmentTestQuestionsID
		) As [Eq]
		Left Join 
			(
			Select 
				[Qa].Lkp_RecruitmentTestQuestionsID
				, Count(1) As [Ct]
			From RecruitmentTestQuestionAnswers As [Qa]
			Where
				[Qa].IsAnswer = 1
				And IsNull([Qa].IsDeleted,0) = 0
			Group By
				[Qa].Lkp_RecruitmentTestQuestionsID
			) As [Qa]
			On [Qa].Lkp_RecruitmentTestQuestionsID = [Eq].Lkp_RecruitmentTestQuestionsID
'
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_QuestionAnswers]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_QuestionAnswers]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestExams_QuestionAnswers]
As	
	Select
		[Eq].RecruitmentTestExamsID
		, [Eq].Lkp_RecruitmentTestQuestionsID
		, [Eq].IsCorrect
		, [Qa].Lkp_RecruitmentTestAnswersID
		, [Qa].Lkp_RecruitmentTestAnswersID_Desc
		, [Qa].IsAnswer
		, [Ea].IsAnswer As [IsAnswered]
	From
		uvw_RecruitmentTestExams_Questions As [Eq]
		Inner Join uvw_RecruitmentTestQuestionAnswers As [Qa]
			On [Qa].Lkp_RecruitmentTestQuestionsID = [Eq].Lkp_RecruitmentTestQuestionsID
		Left Join RecruitmentTestExams_Answers As [Ea]
			On [Ea].Lkp_RecruitmentTestQuestionsID = [Eq].Lkp_RecruitmentTestQuestionsID
			And [Ea].Lkp_RecruitmentTestAnswersID = [Qa].Lkp_RecruitmentTestAnswersID
			And [Ea].RecruitmentTestExamsID = [Eq].RecruitmentTestExamsID	'
GO
/****** Object:  StoredProcedure [dbo].[usp_GenerateExam]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GenerateExam]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[usp_GenerateExam] 
@Question_Limit As BigInt
, @CategoryID As BigInt
As
Begin
	Create Table #Tmp_Question (QuestionID BigInt)
	
	Declare @Questions_Ct As BigInt
	Declare @NoRequiredAnswers As BigInt
	
	Select @NoRequiredAnswers = dbo.udf_Get_System_Parameter(''Exam_NoRequiredAnswers'')
	
	Select 
		@Questions_Ct = Count(1)
	From 
		RecruitmentTestQuestions As [Q]
		Inner Join 
			(
			Select
				Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
				, Count(1) As Ct
			From
				RecruitmentTestQuestionAnswers
			Group By
				Lkp_RecruitmentTestQuestionsID
			) As [Qa]
			On [Qa].RecruitmentTestQuestionsID = [Q].RecruitmentTestQuestionsID
	Where 
		[Q].IsApproved = 1
		And [Qa].Ct >= @NoRequiredAnswers
		And [Q].LookupCategoryID = @CategoryID
	
	If @Question_Limit < @Questions_Ct
	Begin
		Declare @Ct As BigInt
		Set @Ct = 0
		While @Ct < @Question_Limit
		Begin
			Declare @Selected_Questions_Ct As BigInt
			
			Declare @IsValid As Bit
			Set @IsValid = 0
			While @IsValid = 0
			Begin
				Set @Selected_Questions_Ct = Cast((Rand() * @Questions_Ct) + 1 As BigInt)
			
				Declare @QuestionID As BigInt
				Select 
					@QuestionID = RecruitmentTestQuestionsID
				From
					(
					Select 
						Row_Number() Over (Order By (Select 0)) As Ct
						, [Q].RecruitmentTestQuestionsID
					From 
						RecruitmentTestQuestions As [Q]
						Inner Join 
							(
							Select
								Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
								, Count(1) As Ct
							From
								RecruitmentTestQuestionAnswers
							Group By
								Lkp_RecruitmentTestQuestionsID
							) As [Qa]
							On [Qa].RecruitmentTestQuestionsID = [Q].RecruitmentTestQuestionsID
					Where
						[Q].IsApproved = 1
						And [Qa].Ct >= @NoRequiredAnswers
						And [Q].LookupCategoryID = @CategoryID
					) As [Tb]
				Where
					[Tb].Ct = @Selected_Questions_Ct
				
				If Not Exists(
					Select *
					From #Tmp_Question
					Where QuestionID = @QuestionID
					)
				Begin
					Set @IsValid = 1
				End
			End
			
			Insert Into #Tmp_Question (QuestionID) 
			Values (@QuestionID)
			
			Set @Ct = @Ct + 1
		End
	End
	Else
	Begin
		Insert Into #Tmp_Question
		(QuestionID)
		Select [Q].RecruitmentTestQuestionsID
		From 
			RecruitmentTestQuestions As [Q]
			Inner Join 
				(
				Select
					Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
					, Count(1) As Ct
				From
					RecruitmentTestQuestionAnswers
				Group By
					Lkp_RecruitmentTestQuestionsID
				) As [Qa]
				On [Qa].RecruitmentTestQuestionsID = [Q].RecruitmentTestQuestionsID
		Where 
			[Q].IsApproved = 1
			And [Qa].Ct >= @NoRequiredAnswers
			And [Q].LookupCategoryID = @CategoryID
	End
	
	--[-]
	
	Select 
		[Tb].*
	From 
		RecruitmentTestQuestions As [Tb]
		Inner Join #Tmp_Question As [Source]
			On [Source].QuestionID = [Tb].RecruitmentTestQuestionsID
	
	--[-]
	
	Create Table #Tmp_Answer (QuestionID BigInt, AnswerID BigInt, OrderIndex Int)
	Create Table #Tmp_AnswerEx (QuestionID BigInt, AnswerID BigInt, IsFixed Bit, OrderIndex Int)
	
	Declare @Answers_Ct As BigInt
	Declare @AnswersFixed_Ct As BigInt
	Declare @AnswerID As BigInt
	Set @QuestionID = Null
	
	Declare Cur Cursor Fast_Forward
	For
	Select QuestionID
	From #Tmp_Question As [Q]
	
	Open Cur
	Fetch Next From Cur
	Into @QuestionID
	
	While @@Fetch_Status = 0
	Begin
		Select @Answers_Ct = Count(1)
		From RecruitmentTestQuestionAnswers
		Where 
			Lkp_RecruitmentTestQuestionsID = @QuestionID
			And IsNull(IsDeleted,0) = 0
		
		Select @AnswersFixed_Ct = Count(1)
		From RecruitmentTestQuestionAnswers
		Where 
			Lkp_RecruitmentTestQuestionsID = @QuestionID
			And IsNull(IsFixed,0) = 1
			And IsNull(IsDeleted,0) = 0
		
		Truncate Table #Tmp_AnswerEx
		Insert Into #Tmp_AnswerEx 
			(QuestionID, AnswerID, IsFixed, OrderIndex)
		Select
			Lkp_RecruitmentTestQuestionsID, Lkp_RecruitmentTestAnswersID, IsFixed, Row_Number() Over (Order By OrderIndex)
		From
			RecruitmentTestQuestionAnswers
		Where
			Lkp_RecruitmentTestQuestionsID = @QuestionID
			And IsNull(IsDeleted,0) = 0
		
		Set @Ct = 1
		While @Ct <= @Answers_Ct
		Begin
			Declare @IsFixed As Bit
			Select
				@AnswerID = AnswerID
				, @IsFixed = IsNull(IsFixed,0)
			From 
				#Tmp_AnswerEx
			Where 
				QuestionID = @QuestionID
				And OrderIndex = @Ct
			
			If @IsFixed = 1
			Begin
				Insert #Tmp_Answer (QuestionID, AnswerID, OrderIndex)
				Values (@QuestionID, @AnswerID, @Ct)
			End
			Else
			Begin
				Set @IsValid = 0
				While @IsValid = 0
				Begin
					Declare @Rand_Ct As BigInt
					Set @Rand_Ct = Cast((Rand() * (@Answers_Ct - @AnswersFixed_Ct)) + 1 As BigInt)
					
					Select
						@AnswerID = [AnswerID]
					From
						(
						Select
							Row_Number() Over (Order By (Select 0)) As [Ct]
							, [AnswerID]
						From
							#Tmp_AnswerEx
						Where
							QuestionID = @QuestionID
							And IsNull(IsFixed,0) = 0
						) As [Tb]
					Where
						[Tb].Ct = @Rand_Ct
					
					If Not Exists(
						Select *
						From #Tmp_Answer
						Where 
							QuestionID = @QuestionID
							And AnswerID = @AnswerID
						)
					Begin
						Set @IsValid = 1
					End					
				End
				
				Insert #Tmp_Answer (QuestionID, AnswerID, OrderIndex)
				Values (@QuestionID, @AnswerID, @Ct)
			End
			
			Set @Ct = @Ct + 1
		End
		
		Fetch Next From Cur
		Into @QuestionID
	End
	
	Close Cur
	Deallocate Cur
	
	--[-]
	
	Select 
		[Tb].RecruitmentTestQuestionAnswersID
		, [Tb].Lkp_RecruitmentTestQuestionsID
		, [Tb].Lkp_RecruitmentTestAnswersID
		, [Tb].IsAnswer
		, [Tb].Lkp_RecruitmentTestAnswersID_Desc
		, [Source].OrderIndex
	From 
		uvw_RecruitmentTestQuestionAnswers As [Tb]
		Inner Join #Tmp_Answer As [Source]
			On [Source].QuestionID = [Tb].Lkp_RecruitmentTestQuestionsID
			And [Source].AnswerID = [Tb].Lkp_RecruitmentTestAnswersID
	Order By
		[Source].QuestionID, [Source].OrderIndex
	
	--[-]
	
	Drop Table #Tmp_Question
	Drop Table #Tmp_Answer
	Drop Table #Tmp_AnswerEx
End
' 
END
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_Scores]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_Scores]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestExams_Scores]
As
	Select
		[Tb].*
		, [Eq].Ct As [Total]
	From
		(
		Select
			[Tb].RecruitmentTestExamsID
			, Sum(Cast([Tb].IsCorrect As BigInt)) As [Score]	
		From
			uvw_RecruitmentTestExams_Questions As [Tb]
		Group By
			[Tb].RecruitmentTestExamsID
		) As [Tb]
		Left Join 
			(
			Select 
				RecruitmentTestExamsID
				, Count(1) As [Ct]
			From 
				RecruitmentTestExams_Questions 
			Group By
				RecruitmentTestExamsID
			) As [Eq]
			On [Eq].RecruitmentTestExamsID = [Tb].RecruitmentTestExamsID'
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestRights_Details]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestRights_Details]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestRights_Details]
As
	Select
		[Rd].RecruitmentTestRights_DetailsID
		, [Sma].RecruitmentTestRightsID
		, [Sma].Rights_Name
		, [Sma].System_ModulesID_Parent
		, [Sma].System_ModulesID
		, [Sma].System_Modules_AccessID
		, [Sma].System_Modules_AccessLibID
		, [Sma].[Module_Parent_Name]
		, [Sma].[Module_Name]
		, [Sma].[Module_Code]
		, [Sma].[Access_Desc]
		, IsNull([Rd].[IsAllowed], 0) As [IsAllowed]
	From
		[RecruitmentTestRights] As [R]
		Left Join [RecruitmentTestRights_Details] As [Rd]
			On [R].[RecruitmentTestRightsID] = [Rd].[RecruitmentTestRightsID]
		Right Join
			(
			Select
				[R].RecruitmentTestRightsID
				, [R].[Name] As [Rights_Name]
				, [Sma].System_ModulesID_Parent
				, [Sma].System_ModulesID
				, [Sma].System_Modules_AccessID
				, [Sma].System_Modules_AccessLibID
				, [Sma].[Parent_Name] As [Module_Parent_Name]
				, [Sma].[Module_Name]
				, [Sma].[Module_Code]
				, [Sma].[Access_Desc]
			From
				RecruitmentTestRights As [R]
				, uvw_System_Modules_Access As [Sma]
			) As [Sma]
			On [Sma].[System_Modules_AccessID] = [Rd].[System_Modules_AccessID]
			And [Sma].RecruitmentTestRightsID = [R].RecruitmentTestRightsID
'
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestUser_Rights]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestUser_Rights]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestUser_Rights]
As
	Select 
		[Ur].* 
		, [Rd].System_ModulesID
		, [Rd].System_Modules_AccessID
		, [Rd].System_Modules_AccessLibID
		, [Rd].IsAllowed
	From 
		RecruitmentTestUser_Rights As [Ur] 
		Left Join uvw_RecruitmentTestRights_Details As [Rd] 
			On Ur.RecruitmentTestRightsID = Rd.RecruitmentTestRightsID'
GO
/****** Object:  View [dbo].[uvw_RecruitmentTestExams_Scores_Desc]    Script Date: 04/11/2012 00:15:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[uvw_RecruitmentTestExams_Scores_Desc]'))
EXEC dbo.sp_executesql @statement = N'CREATE View [dbo].[uvw_RecruitmentTestExams_Scores_Desc]
As
	Select
		[Tb].*
		, (
		Case [Tb].[IsScoreChanged]
			When 1 Then ''Yes''
			Else ''No''
		End) As [ScoreChanged_Desc]
	From
		(
		Select
			[Tb].*
			, [Score].[Score] As [Computed_Score]
			, [Score].[Total] As [Computed_TotalItems]
			, Cast(
			(
			Case
				When [Tb].[Score] <> [Score].[Score] Then 1
				Else 0
			End
			)
			As Bit) As [IsScoreChanged]
		From
			uvw_RecruitmentTestExams As [Tb]
			Left Join uvw_RecruitmentTestExams_Scores As [Score]
				On [Score].RecruitmentTestExamsID = [Tb].RecruitmentTestExamsID
		) As [Tb]
		'
GO
/****** Object:  StoredProcedure [dbo].[usp_System_Modules_Load]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_System_Modules_Load]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[usp_System_Modules_Load]
@UserID As BigInt
As
Begin
	Select Distinct
		[Sm].*
		, [Psm].OrderIndex As [Psm_OrderIndex]
		, [Sm].OrderIndex As [Sm_OrderIndex]
	From 
		System_Modules As [Sm]
		Inner Join uvw_RecruitmentTestRights_Details As [Rd]
			On [Rd].System_ModulesID = [Sm].System_ModulesID
			And [Rd].Access_Desc = ''Access''
			And [Rd].IsAllowed = 1
			And IsNull([Sm].IsHidden,0) = 0
		Inner Join RecruitmentTestUser_Rights As [Ur]
			On [Ur].RecruitmentTestUserID = @UserID
			And [Ur].RecruitmentTestRightsID = [Rd].RecruitmentTestRightsID
			And [Ur].IsActive = 1
		Left Join System_Modules As [Psm]
			On [Psm].System_ModulesID = [Sm].System_ModulesID_Parent
	Order By
		[Psm].OrderIndex
		, [Sm].OrderIndex
End
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LoadExam_Detailed]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LoadExam_Detailed]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[usp_LoadExam_Detailed]
@ExamID As BigInt
As
Begin
	Select
		[Tb].[Desc]
		, [Tb].[Correct_Desc]
	From
		(
		Select
			ROW_NUMBER() Over (Order By QuestionsID, AnswersID) As [Ct]
			, (
			Case
				When [Tb].AnswersID Is Null Then 
					Cast([Tb].Q_Ct As VarChar(30)) + ''. '' + [Q].Question
				Else 
					''     '' + [A].Answer
			End
			) As [Desc]
			, (
			Case
				When IsCorrect = 1 And AnswersID Is Null Then
					''Answered Correctly''
				When IsAnswer = 1 Then
					''Correct Answer''
			End
			) As [Correct_Desc]
		From
			(
			Select 
				Lkp_RecruitmentTestQuestionsID As [QuestionsID]
				, Lkp_RecruitmentTestAnswersID As [AnswersID]
				, IsCorrect
				, IsAnswer
				, Null As [Q_Ct]
			From 
				uvw_RecruitmentTestExams_QuestionAnswers	
			Where 
				RecruitmentTestExamsID = @ExamID
			
			Union All
			
			Select
				Lkp_RecruitmentTestQuestionsID As [QuestionsID]
				, Null As [AnswersID]
				, IsCorrect
				, Null As [IsAnswer]
				, ROW_NUMBER() Over (Order By Lkp_RecruitmentTestQuestionsID) As [Q_Ct]
			From
				uvw_RecruitmentTestExams_Questions
			Where
				RecruitmentTestExamsID = @ExamID
			) As [Tb]
			Left Join RecruitmentTestQuestions As [Q]
				On [Q].RecruitmentTestQuestionsID = [Tb].QuestionsID
			Left Join RecruitmentTestAnswers As [A]
				On [A].RecruitmentTestAnswersID = [Tb].AnswersID
		) As [Tb]
	Order By
		[Ct]
End
' 
END
GO
/****** Object:  StoredProcedure [dbo].[usp_LoadExam]    Script Date: 04/11/2012 00:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LoadExam]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Procedure [dbo].[usp_LoadExam]
@ExamID As BigInt
As
Begin
	Select
		[Tb].Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
		, [Tb].IsCorrect
		, [Q].Question
		, [Q].IsMultipleAnswer
		, [Q].LookupQuestionTypeID
	From 
		uvw_RecruitmentTestExams_Questions As [Tb]
		Left Join RecruitmentTestQuestions As [Q]
			On [Q].RecruitmentTestQuestionsID = [Tb].Lkp_RecruitmentTestQuestionsID
	Where
		[Tb].RecruitmentTestExamsID = @ExamID
	
	/*
	Select
		[Eq].Lkp_RecruitmentTestQuestionsID
		, [Eq].IsCorrect
		, [Qa].Lkp_RecruitmentTestAnswersID
		, [Qa].Lkp_RecruitmentTestAnswersID_Desc
		, [Qa].IsAnswer
		, [Ea].IsAnswer As [IsAnswered]
	From
		uvw_RecruitmentTestExams_Questions As [Eq]
		Inner Join uvw_RecruitmentTestQuestionAnswers As [Qa]
			On [Qa].Lkp_RecruitmentTestQuestionsID = [Eq].Lkp_RecruitmentTestQuestionsID
		Left Join RecruitmentTestExams_Answers As [Ea]
			On [Ea].Lkp_RecruitmentTestQuestionsID = [Eq].Lkp_RecruitmentTestQuestionsID
			And [Ea].Lkp_RecruitmentTestAnswersID = [Qa].Lkp_RecruitmentTestAnswersID
			And [Ea].RecruitmentTestExamsID = [Eq].RecruitmentTestExamsID	
	Where
		[Eq].RecruitmentTestExamsID = @ExamID
	Order By
		[Eq].Lkp_RecruitmentTestQuestionsID	
	*/
	
	Select *
	From uvw_RecruitmentTestExams_QuestionAnswers
	Where RecruitmentTestExamsID = @ExamID
	Order By Lkp_RecruitmentTestQuestionsID	
	
End
' 
END
GO
