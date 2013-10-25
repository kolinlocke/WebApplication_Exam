Create Function [dbo].[udf_Get_System_Parameter]
(
@ParameterName VarChar(Max)
)
Returns VarChar(Max)
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
		Return ''
	End
	Else
	Begin
		Select @ParameterValue = ParameterValue
		From System_Parameters
		Where ParameterName = @ParameterName
	End
	
	Return @ParameterValue
End
