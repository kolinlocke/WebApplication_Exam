Create Procedure [dbo].[usp_GenerateExam] 
@Question_Limit As BigInt
, @CategoryID As BigInt
As
Begin
	Create Table #Tmp_Question (QuestionID BigInt)
	
	Declare @Questions_Ct As BigInt
	Declare @NoRequiredAnswers As BigInt
	
	Select @NoRequiredAnswers = dbo.udf_Get_System_Parameter('Exam_NoRequiredAnswers')
	
	Select 
		@Questions_Ct = Count(1)
	From 
		RecruitmentTestQuestions As [Q]
		Inner Join 
			(
			Select
				Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
				, Count(1) As Ct
			From
				RecruitmentTestQuestionAnswers
			Group By
				Lkp_RecruitmentTestQuestionsID
			) As [Qa]
			On [Qa].RecruitmentTestQuestionsID = [Q].RecruitmentTestQuestionsID
	Where 
		[Q].IsApproved = 1
		And [Qa].Ct >= @NoRequiredAnswers
		And [Q].LookupCategoryID = @CategoryID
	
	If @Question_Limit < @Questions_Ct
	Begin
		Declare @Ct As BigInt
		Set @Ct = 0
		While @Ct < @Question_Limit
		Begin
			Declare @Selected_Questions_Ct As BigInt
			
			Declare @IsValid As Bit
			Set @IsValid = 0
			While @IsValid = 0
			Begin
				Set @Selected_Questions_Ct = Cast((Rand() * @Questions_Ct) + 1 As BigInt)
			
				Declare @QuestionID As BigInt
				Select 
					@QuestionID = RecruitmentTestQuestionsID
				From
					(
					Select 
						Row_Number() Over (Order By (Select 0)) As Ct
						, [Q].RecruitmentTestQuestionsID
					From 
						RecruitmentTestQuestions As [Q]
						Inner Join 
							(
							Select
								Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
								, Count(1) As Ct
							From
								RecruitmentTestQuestionAnswers
							Group By
								Lkp_RecruitmentTestQuestionsID
							) As [Qa]
							On [Qa].RecruitmentTestQuestionsID = [Q].RecruitmentTestQuestionsID
					Where
						[Q].IsApproved = 1
						And [Qa].Ct >= @NoRequiredAnswers
						And [Q].LookupCategoryID = @CategoryID
					) As [Tb]
				Where
					[Tb].Ct = @Selected_Questions_Ct
				
				If Not Exists(
					Select *
					From #Tmp_Question
					Where QuestionID = @QuestionID
					)
				Begin
					Set @IsValid = 1
				End
			End
			
			Insert Into #Tmp_Question (QuestionID) 
			Values (@QuestionID)
			
			Set @Ct = @Ct + 1
		End
	End
	Else
	Begin
		Insert Into #Tmp_Question
		(QuestionID)
		Select [Q].RecruitmentTestQuestionsID
		From 
			RecruitmentTestQuestions As [Q]
			Inner Join 
				(
				Select
					Lkp_RecruitmentTestQuestionsID As [RecruitmentTestQuestionsID]
					, Count(1) As Ct
				From
					RecruitmentTestQuestionAnswers
				Group By
					Lkp_RecruitmentTestQuestionsID
				) As [Qa]
				On [Qa].RecruitmentTestQuestionsID = [Q].RecruitmentTestQuestionsID
		Where 
			[Q].IsApproved = 1
			And [Qa].Ct >= @NoRequiredAnswers
			And [Q].LookupCategoryID = @CategoryID
	End
	
	--[-]
	
	Select 
		[Tb].*
	From 
		RecruitmentTestQuestions As [Tb]
		Inner Join #Tmp_Question As [Source]
			On [Source].QuestionID = [Tb].RecruitmentTestQuestionsID
	
	--[-]
	
	Create Table #Tmp_Answer (QuestionID BigInt, AnswerID BigInt, OrderIndex Int)
	Create Table #Tmp_AnswerEx (QuestionID BigInt, AnswerID BigInt, IsFixed Bit, OrderIndex Int)
	
	Declare @Answers_Ct As BigInt
	Declare @AnswersFixed_Ct As BigInt
	Declare @AnswerID As BigInt
	Set @QuestionID = Null
	
	Declare Cur Cursor Fast_Forward
	For
	Select QuestionID
	From #Tmp_Question As [Q]
	
	Open Cur
	Fetch Next From Cur
	Into @QuestionID
	
	While @@Fetch_Status = 0
	Begin
		Select @Answers_Ct = Count(1)
		From RecruitmentTestQuestionAnswers
		Where 
			Lkp_RecruitmentTestQuestionsID = @QuestionID
			And IsNull(IsDeleted,0) = 0
		
		Select @AnswersFixed_Ct = Count(1)
		From RecruitmentTestQuestionAnswers
		Where 
			Lkp_RecruitmentTestQuestionsID = @QuestionID
			And IsNull(IsFixed,0) = 1
			And IsNull(IsDeleted,0) = 0
		
		Truncate Table #Tmp_AnswerEx
		Insert Into #Tmp_AnswerEx 
			(QuestionID, AnswerID, IsFixed, OrderIndex)
		Select
			Lkp_RecruitmentTestQuestionsID, Lkp_RecruitmentTestAnswersID, IsFixed, Row_Number() Over (Order By OrderIndex)
		From
			RecruitmentTestQuestionAnswers
		Where
			Lkp_RecruitmentTestQuestionsID = @QuestionID
			And IsNull(IsDeleted,0) = 0
		
		Set @Ct = 1
		While @Ct <= @Answers_Ct
		Begin
			Declare @IsFixed As Bit
			Select
				@AnswerID = AnswerID
				, @IsFixed = IsNull(IsFixed,0)
			From 
				#Tmp_AnswerEx
			Where 
				QuestionID = @QuestionID
				And OrderIndex = @Ct
			
			If @IsFixed = 1
			Begin
				Insert #Tmp_Answer (QuestionID, AnswerID, OrderIndex)
				Values (@QuestionID, @AnswerID, @Ct)
			End
			Else
			Begin
				Set @IsValid = 0
				While @IsValid = 0
				Begin
					Declare @Rand_Ct As BigInt
					Set @Rand_Ct = Cast((Rand() * (@Answers_Ct - @AnswersFixed_Ct)) + 1 As BigInt)
					
					Select
						@AnswerID = [AnswerID]
					From
						(
						Select
							Row_Number() Over (Order By (Select 0)) As [Ct]
							, [AnswerID]
						From
							#Tmp_AnswerEx
						Where
							QuestionID = @QuestionID
							And IsNull(IsFixed,0) = 0
						) As [Tb]
					Where
						[Tb].Ct = @Rand_Ct
					
					If Not Exists(
						Select *
						From #Tmp_Answer
						Where 
							QuestionID = @QuestionID
							And AnswerID = @AnswerID
						)
					Begin
						Set @IsValid = 1
					End					
				End
				
				Insert #Tmp_Answer (QuestionID, AnswerID, OrderIndex)
				Values (@QuestionID, @AnswerID, @Ct)
			End
			
			Set @Ct = @Ct + 1
		End
		
		Fetch Next From Cur
		Into @QuestionID
	End
	
	Close Cur
	Deallocate Cur
	
	--[-]
	
	Select 
		[Tb].RecruitmentTestQuestionAnswersID
		, [Tb].Lkp_RecruitmentTestQuestionsID
		, [Tb].Lkp_RecruitmentTestAnswersID
		, [Tb].IsAnswer
		, [Tb].Lkp_RecruitmentTestAnswersID_Desc
		, [Source].OrderIndex
	From 
		uvw_RecruitmentTestQuestionAnswers As [Tb]
		Inner Join #Tmp_Answer As [Source]
			On [Source].QuestionID = [Tb].Lkp_RecruitmentTestQuestionsID
			And [Source].AnswerID = [Tb].Lkp_RecruitmentTestAnswersID
	Order By
		[Source].QuestionID, [Source].OrderIndex
	
	--[-]
	
	Drop Table #Tmp_Question
	Drop Table #Tmp_Answer
	Drop Table #Tmp_AnswerEx
End
