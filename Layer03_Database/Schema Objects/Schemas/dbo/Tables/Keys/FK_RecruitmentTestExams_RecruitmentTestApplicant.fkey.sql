ALTER TABLE [dbo].[RecruitmentTestExams]
    ADD CONSTRAINT [FK_RecruitmentTestExams_RecruitmentTestApplicant] FOREIGN KEY ([RecruitmentTestApplicantID]) REFERENCES [dbo].[RecruitmentTestApplicant] ([RecruitmentTestApplicantID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

