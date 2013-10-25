CREATE Procedure [dbo].[usp_LoadExam]
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
