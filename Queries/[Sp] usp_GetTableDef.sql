Create Procedure [dbo].[usp_GetTableDef]
@TableName VarChar(Max)
, @SchemaName VarChar(Max) = ''
As
Set NOCOUNT On
Begin
	
	If IsNull(@SchemaName, '') = ''
	Begin
		Set @SchemaName = 'dbo'
	End
	
	Select *
	From [udf_GetTableDef](@SchemaName + '.' + @TableName)
	Order By Column_Id
	
End
