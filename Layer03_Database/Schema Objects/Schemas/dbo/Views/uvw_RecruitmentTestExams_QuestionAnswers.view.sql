CREATE View [dbo].[uvw_RecruitmentTestExams_QuestionAnswers]
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
			And [Ea].RecruitmentTestExamsID = [Eq].RecruitmentTestExamsID	