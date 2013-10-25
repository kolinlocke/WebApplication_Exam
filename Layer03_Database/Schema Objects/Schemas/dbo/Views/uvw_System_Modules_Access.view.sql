CREATE View [dbo].[uvw_System_Modules_Access]
As
	Select Top (100) Percent
		IsNull([Sm].[Parent_Name],'Root') As [Parent_Name]
		, [Smal].[Module_Name]
		, [Smal].[Module_Code]
		, [Smal].[Access_Desc]
		, [Sm].System_ModulesID_Parent
		, [Sma].*
	From
		System_Modules_Access As [Sma]
		Left Join uvw_System_Modules_AccessLib As [Smal]
			On [Smal].System_ModulesID = [Sma].System_ModulesID
			And [Smal].System_Modules_AccessLibID = [Sma].System_Modules_AccessLibID
		Left Join
			(
			Select
				[Psm].[Name] As [Parent_Name]
				, [Sm].System_ModulesID
				, [Sm].System_ModulesID_Parent
				, [Sm].OrderIndex
			From 
				System_Modules As [Sm]
				Left Join System_Modules As [Psm]
					On [Psm].System_ModulesID = [Sm].System_ModulesID_Parent
			) As [Sm]
			On [Sm].System_ModulesID = [Sma].System_ModulesID
	Order By
		[Sm].System_ModulesID_Parent
		, [Sm].OrderIndex
