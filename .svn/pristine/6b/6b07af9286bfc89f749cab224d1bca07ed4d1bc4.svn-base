Create Procedure [dbo].[usp_RecruitmentTestRights_Details_Load]
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
		Set @Condition = 'RecruitmentTestRightsID = ' + Cast(@ID As VarChar(50))
	End
	
	Declare @Query VarChar(Max)
	Set @Query =
		'
		Select
			[Rd].RecruitmentTestRights_DetailsID
			, [Rd].RecruitmentTestRightsID
			, [Rd].[IsAllowed]
			, [Sma].System_ModulesID_Parent
			, [Sma].System_ModulesID
			, [Sma].System_Modules_AccessID
			, [Sma].System_Modules_AccessLibID
			, [Sma].[Parent_Name] As [Module_Parent_Name]
			, [Sma].[Module_Name]
			, [Sma].[Module_Code]
			, [Sma].[Access_Desc]
		From
			(
			Select [Rd].*
			From RecruitmentTestRights_Details As [Rd]
			Where ' + @Condition + '
			) As [Rd]
			Right Join uvw_System_Modules_Access As [Sma]
				On [Sma].[System_Modules_AccessID] = [Rd].[System_Modules_AccessID]
		'
		Exec(@Query)

End

GO


