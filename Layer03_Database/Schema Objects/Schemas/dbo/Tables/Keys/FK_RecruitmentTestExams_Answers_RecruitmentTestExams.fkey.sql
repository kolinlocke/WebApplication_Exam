ALTER TABLE [dbo].[RecruitmentTestExams_Answers]
    ADD CONSTRAINT [FK_RecruitmentTestExams_Answers_RecruitmentTestExams] FOREIGN KEY ([RecruitmentTestExamsID]) REFERENCES [dbo].[RecruitmentTestExams] ([RecruitmentTestExamsID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

