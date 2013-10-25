ALTER TABLE [dbo].[RecruitmentTestQuestionAnswers]
    ADD CONSTRAINT [FK_QuestionAnswer_Question] FOREIGN KEY ([Lkp_RecruitmentTestQuestionsID]) REFERENCES [dbo].[RecruitmentTestQuestions] ([RecruitmentTestQuestionsID]) ON DELETE CASCADE ON UPDATE NO ACTION;

