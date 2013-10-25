ALTER TABLE [dbo].[RecruitmentTestExams_Answers]
    ADD CONSTRAINT [FK_RecruitmentTestExams_Answers_RecruitmentTestAnswers] FOREIGN KEY ([Lkp_RecruitmentTestAnswersID]) REFERENCES [dbo].[RecruitmentTestAnswers] ([RecruitmentTestAnswersID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

