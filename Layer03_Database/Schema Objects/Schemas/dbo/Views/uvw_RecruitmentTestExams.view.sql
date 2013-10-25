CREATE View [dbo].[uvw_RecruitmentTestExams]
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
			
		
		
