CREATE View [dbo].[uvw_RecruitmentTestExams_Questions]
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
