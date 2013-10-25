/*
[Query] eLookupCategory
*/

Select Replace([Desc], ' ','_') + ' = ' + Cast(LookupCategoryID As VarChar) + ',' As [eLookupCategory]
From LookupCategory