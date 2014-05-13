Create Procedure [dbo].[usp_Get_System_Parameter]
@ParameterName VarChar(Max)
, @DefaultValue VarChar(Max)
As
Begin
	Declare @ParameterValue As VarChar(Max)		
	Set @ParameterValue = ''
	
	Declare @Ct As Int	
	Select @Ct = Count(1)
	From System_Parameters
	Where ParameterName = @ParameterName
	
	If @Ct = 0
	Begin
		Exec usp_Require_System_Parameter @ParameterName, @DefaultValue
		Set @ParameterValue = @DefaultValue
	End
	Else
	Begin
		Select @ParameterValue = ParameterValue
		From System_Parameters
		Where ParameterName = @ParameterName
	End
	
	Select @ParameterValue As [ParameterValue]
End

