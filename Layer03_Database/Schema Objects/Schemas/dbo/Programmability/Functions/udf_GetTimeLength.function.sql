CREATE Function [dbo].[udf_GetTimeLength]
(
	@DateStart As DateTime
	, @DateEnd As DateTime
)
Returns VarChar(1000)
As
Begin
	Declare @Time_Second_Ex Int
	Set @Time_Second_Ex = DATEDIFF(SECOND, @DateStart, @DateEnd)

	Declare @Time_Minute_Ex As Int
	Set @Time_Minute_Ex = 0

	If @Time_Second_Ex >= 60
	Set @Time_Minute_Ex = @Time_Second_Ex / 60

	Declare @Time_Hour As Int
	Set @Time_Hour = 0

	If @Time_Minute_Ex >= 60
	Set @Time_Hour = @Time_Minute_Ex / 60

	Declare @Time_Second As Int
	Set @Time_Second = @Time_Second_Ex - (@Time_Minute_Ex * 60)

	Declare @Time_Minute As Int
	Set @Time_Minute = @Time_Minute_Ex - (@Time_Hour * 60)

	Declare @Time_Hour_St As VarChar(100)
	Set @Time_Hour_St = ''

	If @Time_Hour > 0
	Set @Time_Hour_St = ' ' + Cast (@Time_Hour As VarChar(50)) +  ' hour(s) '

	Declare @Time_Minute_St As VarChar(50)
	Set @Time_Minute_St = ''

	If @Time_Minute > 0
	Set @Time_Minute_St = ' ' + Cast (@Time_Minute As VarChar(50)) +  ' minute(s) '

	Declare @Time_Second_St As VarChar(50)
	Set @Time_Second_St = ''

	If @Time_Second > 0
	Set @Time_Second_St = ' ' + Cast (@Time_Second As VarChar(50)) +  ' second(s) '

	Declare @Rs As VarChar(50)
	Set @Rs = @Time_Hour_St + @Time_Minute_St + @Time_Second_St
	
	Return @Rs
End
