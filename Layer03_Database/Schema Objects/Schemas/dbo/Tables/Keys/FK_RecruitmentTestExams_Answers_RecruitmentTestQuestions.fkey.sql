ALTER TABLE [dbo].[RecruitmentTestExams_Answers]
    ADD CONSTRAINT [FK_RecruitmentTestExams_Answers_RecruitmentTestQuestions] FOREIGN KEY ([Lkp_RecruitmentTestQuestionsID]) REFERENCES [dbo].[RecruitmentTestQuestions] ([RecruitmentTestQuestionsID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

