ALTER TABLE [dbo].[RecruitmentTestExams_Questions]
    ADD CONSTRAINT [FK_RecruitmentTestExams_Questions_RecruitmentTestExams] FOREIGN KEY ([RecruitmentTestExamsID]) REFERENCES [dbo].[RecruitmentTestExams] ([RecruitmentTestExamsID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

