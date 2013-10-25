Create View [dbo].[uvw_RecruitmentTestRights_Details]
As
	Select
		[Rd].RecruitmentTestRights_DetailsID
		, [Sma].RecruitmentTestRightsID
		, [Sma].Rights_Name
		, [Sma].System_ModulesID_Parent
		, [Sma].System_ModulesID
		, [Sma].System_Modules_AccessID
		, [Sma].System_Modules_AccessLibID
		, [Sma].[Module_Parent_Name]
		, [Sma].[Module_Name]
		, [Sma].[Module_Code]
		, [Sma].[Access_Desc]
		, IsNull([Rd].[IsAllowed], 0) As [IsAllowed]
	From
		[RecruitmentTestRights] As [R]
		Left Join [RecruitmentTestRights_Details] As [Rd]
			On [R].[RecruitmentTestRightsID] = [Rd].[RecruitmentTestRightsID]
		Right Join
			(
			Select
				[R].RecruitmentTestRightsID
				, [R].[Name] As [Rights_Name]
				, [Sma].System_ModulesID_Parent
				, [Sma].System_ModulesID
				, [Sma].System_Modules_AccessID
				, [Sma].System_Modules_AccessLibID
				, [Sma].[Parent_Name] As [Module_Parent_Name]
				, [Sma].[Module_Name]
				, [Sma].[Module_Code]
				, [Sma].[Access_Desc]
			From
				RecruitmentTestRights As [R]
				, uvw_System_Modules_Access As [Sma]
			) As [Sma]
			On [Sma].[System_Modules_AccessID] = [Rd].[System_Modules_AccessID]
			And [Sma].RecruitmentTestRightsID = [R].RecruitmentTestRightsID
