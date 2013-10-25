Create Procedure [dbo].[usp_RecruitmentTestUser_Rights_Load]
@ID As BigInt = 0
As
Begin
	Declare @Condition VarChar(Max)
	If @ID = 0
	Begin
		Set @Condition = '1 = 0'
	End
	Else
	Begin
		Set @Condition = 'RecruitmentTestUserID = ' + Cast(@ID As VarChar(50))
	End
	
	Declare @Query VarChar(Max)
	Set @Query =
		'		
		Select
			[Tb].RecruitmentTestUser_RightsID
			, [Tb].RecruitmentTestUserID
			, [Tb].IsActive
			, [R].RecruitmentTestRightsID
			, [R].Name As [Rights_Name]
		From
			(
			Select *
			From RecruitmentTestUser_Rights
			Where ' + @Condition + '
			) As [Tb]
			Right Join [RecruitmentTestRights] As [R]
				On [R].RecruitmentTestRightsID = [Tb].RecruitmentTestRightsID
		'
		Exec(@Query)

End
