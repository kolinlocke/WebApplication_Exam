CREATE View [dbo].[uvw_RecruitmentTestQuestions]
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
				'Approved'
			Else
				'Open'
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
	