CREATE View [dbo].[uvw_System_Modules_AccessLib]
As
	Select
		Row_Number() Over (Order By System_ModulesID, System_Modules_AccessLibID) As [uvw_System_Modules_AccessID]
		, [Sm].System_ModulesID
		, [Sma].System_Modules_AccessLibID
		, [Sm].[Name] As [Module_Name]
		, [Sm].[Code] As [Module_Code]
		, [Sma].[Desc] As [Access_Desc]
	From
		System_Modules As [Sm]
		, System_Modules_AccessLib As [Sma]
