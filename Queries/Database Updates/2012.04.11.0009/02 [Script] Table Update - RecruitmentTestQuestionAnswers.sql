/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.RecruitmentTestQuestionAnswers
	DROP CONSTRAINT FK_QuestionAnswer_Question
GO
ALTER TABLE dbo.RecruitmentTestQuestions SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.RecruitmentTestQuestionAnswers
	DROP CONSTRAINT FK_QuestionAnswer_Answer
GO
ALTER TABLE dbo.RecruitmentTestAnswers SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_RecruitmentTestQuestionAnswers
	(
	RecruitmentTestQuestionAnswersID bigint NOT NULL IDENTITY (1, 1),
	Lkp_RecruitmentTestQuestionsID bigint NULL,
	Lkp_RecruitmentTestAnswersID bigint NULL,
	IsAnswer bit NULL,
	IsFixed bit NULL,
	OrderIndex int NULL,
	IsDeleted bit NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_RecruitmentTestQuestionAnswers SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_RecruitmentTestQuestionAnswers ON
GO
IF EXISTS(SELECT * FROM dbo.RecruitmentTestQuestionAnswers)
	 EXEC('INSERT INTO dbo.Tmp_RecruitmentTestQuestionAnswers (RecruitmentTestQuestionAnswersID, Lkp_RecruitmentTestQuestionsID, Lkp_RecruitmentTestAnswersID, IsAnswer, IsDeleted)
		SELECT RecruitmentTestQuestionAnswersID, Lkp_RecruitmentTestQuestionsID, Lkp_RecruitmentTestAnswersID, IsAnswer, IsDeleted FROM dbo.RecruitmentTestQuestionAnswers WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_RecruitmentTestQuestionAnswers OFF
GO
DROP TABLE dbo.RecruitmentTestQuestionAnswers
GO
EXECUTE sp_rename N'dbo.Tmp_RecruitmentTestQuestionAnswers', N'RecruitmentTestQuestionAnswers', 'OBJECT' 
GO
ALTER TABLE dbo.RecruitmentTestQuestionAnswers ADD CONSTRAINT
	PK_QuestionAnswer PRIMARY KEY CLUSTERED 
	(
	RecruitmentTestQuestionAnswersID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.RecruitmentTestQuestionAnswers ADD CONSTRAINT
	FK_QuestionAnswer_Answer FOREIGN KEY
	(
	Lkp_RecruitmentTestAnswersID
	) REFERENCES dbo.RecruitmentTestAnswers
	(
	RecruitmentTestAnswersID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.RecruitmentTestQuestionAnswers ADD CONSTRAINT
	FK_QuestionAnswer_Question FOREIGN KEY
	(
	Lkp_RecruitmentTestQuestionsID
	) REFERENCES dbo.RecruitmentTestQuestions
	(
	RecruitmentTestQuestionsID
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
