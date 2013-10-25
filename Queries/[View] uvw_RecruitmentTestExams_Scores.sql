Create View [dbo].[uvw_RecruitmentTestExams_Scores]
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
			On [Eq].RecruitmentTestExamsID = [Tb].RecruitmentTestExamsID