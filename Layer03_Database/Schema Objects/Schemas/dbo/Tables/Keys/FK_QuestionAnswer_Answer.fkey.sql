ALTER TABLE [dbo].[RecruitmentTestQuestionAnswers]
    ADD CONSTRAINT [FK_QuestionAnswer_Answer] FOREIGN KEY ([Lkp_RecruitmentTestAnswersID]) REFERENCES [dbo].[RecruitmentTestAnswers] ([RecruitmentTestAnswersID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

