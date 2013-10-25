Create Procedure [dbo].[usp_System_Modules_Load]
@UserID As BigInt
As
Begin
	Select Distinct
		[Sm].*
		, [Psm].OrderIndex As [Psm_OrderIndex]
		, [Sm].OrderIndex As [Sm_OrderIndex]
	From 
		System_Modules As [Sm]
		Inner Join uvw_RecruitmentTestRights_Details As [Rd]
			On [Rd].System_ModulesID = [Sm].System_ModulesID
			And [Rd].Access_Desc = 'Access'
			And [Rd].IsAllowed = 1
			And IsNull([Sm].IsHidden,0) = 0
		Inner Join RecruitmentTestUser_Rights As [Ur]
			On [Ur].RecruitmentTestUserID = @UserID
			And [Ur].RecruitmentTestRightsID = [Rd].RecruitmentTestRightsID
			And [Ur].IsActive = 1
		Left Join System_Modules As [Psm]
			On [Psm].System_ModulesID = [Sm].System_ModulesID_Parent
	Order By
		[Psm].OrderIndex
		, [Sm].OrderIndex
End
