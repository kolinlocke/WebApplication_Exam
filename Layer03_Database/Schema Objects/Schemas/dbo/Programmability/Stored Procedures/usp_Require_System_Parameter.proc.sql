
Create Proc [dbo].[usp_Require_System_Parameter]
@ParameterName VarChar(Max)
, @ParameterValue VarChar(Max)
As
Begin
	Declare @Ct As Int	
	Select @Ct = Count(1)
	From System_Parameters
	Where ParameterName = @ParameterName
	
	If @Ct = 0
	Begin
		Insert Into System_Parameters (ParameterName, ParameterValue) Values (@ParameterName, @ParameterValue)
	End
End

