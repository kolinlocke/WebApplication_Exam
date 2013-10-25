CREATE VIEW dbo.uvw_RecruitmentTestUser
AS 
	Select
		[Tb].*
		, [LkpUT].[Desc] As [UserType_Desc]
	From 
		RecruitmentTestUser As [Tb]
		Left Join LookupUserType As [LkpUT]
			On [LkpUT].LookupUserTypeID = [Tb].LookupUserTypeID
			