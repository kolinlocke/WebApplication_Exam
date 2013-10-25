CREATE View [dbo].[uvw_RecruitmentTestQuestionAnswers] 
As
	Select 
		[Tb].* 
		, [A].Answer As [Lkp_RecruitmentTestAnswersID_Desc]
	From 
		RecruitmentTestQuestionAnswers As [Tb]
		Left Join RecruitmentTestAnswers As [A]
			On [A].RecruitmentTestAnswersID = [Tb].Lkp_RecruitmentTestAnswersID
