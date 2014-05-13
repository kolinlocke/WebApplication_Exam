Create View [dbo].[uvw_System_Lookup_Details]
As
	Select 
		[Tbd].*
		, [Tb].[Name] As [Lookup_Name]
	From 
		System_Lookup_Details As [Tbd]
		Left Join System_Lookup As [Tb]
			On [Tb].System_LookupID = [Tbd].System_LookupID

GO


