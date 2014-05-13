Create Procedure [dbo].[usp_LoadExam_Detailed]
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
					Cast([Tb].Q_Ct As VarChar(30)) + '. ' + [Q].Question
				Else 
					'     ' + [A].Answer
			End
			) As [Desc]
			, (
			Case
				When IsCorrect = 1 And AnswersID Is Null Then
					'Answered Correctly'
				When IsAnswer = 1 Then
					'Correct Answer'
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
