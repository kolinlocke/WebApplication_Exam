Create Function [dbo].[udf_System_Lookup]
(@Lookup_Name VarChar(Max))	
Returns Table
As
Return
	(
	Select
		System_Lookup_DetailsID As [System_LookupID]
		, [Name]
		, [Desc]
		, OrderIndex
	From uvw_System_Lookup_Details
	Where 
		Lookup_Name = @Lookup_Name
	)
