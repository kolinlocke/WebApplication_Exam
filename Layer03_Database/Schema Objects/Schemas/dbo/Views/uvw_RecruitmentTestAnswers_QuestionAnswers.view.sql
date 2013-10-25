CREATE View [dbo].[uvw_RecruitmentTestAnswers_QuestionAnswers] 
As
	Select 
		[Tb].* 
		, [Qa].Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
	From 
		RecruitmentTestAnswers As [Tb]
		Inner Join RecruitmentTestQuestionAnswers As [Qa]
			On [Qa].Lkp_RecruitmentTestAnswersID = [Tb].RecruitmentTestAnswersID
			And IsNull([Qa].IsDeleted,0) = 0