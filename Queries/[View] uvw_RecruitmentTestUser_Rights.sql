Create View [dbo].[uvw_RecruitmentTestUser_Rights]
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
			On Ur.RecruitmentTestRightsID = Rd.RecruitmentTestRightsID