Create View [dbo].[uvw_System_Modules]
As
	Select
		[Sm].*
		, [Psm].OrderIndex As [Parent_OrderIndex]
	From 
		System_Modules As [Sm]
		Left Join System_Modules As [Psm]
			On [Psm].System_ModulesID = [Sm].System_ModulesID_Parent
