ALTER TABLE [dbo].[RecruitmentTestExams_Questions]
    ADD CONSTRAINT [FK_RecruitmentTestExams_Questions_RecruitmentTestQuestions] FOREIGN KEY ([Lkp_RecruitmentTestQuestionsID]) REFERENCES [dbo].[RecruitmentTestQuestions] ([RecruitmentTestQuestionsID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

