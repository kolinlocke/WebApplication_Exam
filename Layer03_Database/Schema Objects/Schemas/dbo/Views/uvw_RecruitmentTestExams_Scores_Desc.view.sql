CREATE View [dbo].[uvw_RecruitmentTestExams_Scores_Desc]
As
	Select
		[Tb].*
		, (
		Case [Tb].[IsScoreChanged]
			When 1 Then 'Yes'
			Else 'No'
		End) As [ScoreChanged_Desc]
	From
		(
		Select
			[Tb].*
			, [Score].[Score] As [Computed_Score]
			, [Score].[Total] As [Computed_TotalItems]
			, Cast(
			(
			Case
				When [Tb].[Score] <> [Score].[Score] Then 1
				Else 0
			End
			)
			As Bit) As [IsScoreChanged]
		From
			uvw_RecruitmentTestExams As [Tb]
			Left Join uvw_RecruitmentTestExams_Scores As [Score]
				On [Score].RecruitmentTestExamsID = [Tb].RecruitmentTestExamsID
		) As [Tb]
		