/*
[Query] eLookupQuestionType
*/

Select Replace([Desc], ' ','_') + ' = ' + Cast(LookupQuestionTypeID As VarChar) + ',' As [eLookupQuestionType]
From LookupQuestionType