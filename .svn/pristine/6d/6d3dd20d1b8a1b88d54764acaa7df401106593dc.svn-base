/*
[Query] eAccessLib
*/

Select 
	'eAccessLib_' + Replace([Desc],' ','_') 
	+ ' = ' + Cast([System_Modules_AccessLibID] As VarChar) 
	+ ','
	As [eAccessLib]
From
	System_Modules_AccessLib
Where
	[Desc] <> 'Unused'