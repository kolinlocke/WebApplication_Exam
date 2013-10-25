ALTER TABLE [dbo].[RecruitmentTestQuestions]
    ADD CONSTRAINT [FK_RecruitmentTestQuestions_RecruitmentTestUser1] FOREIGN KEY ([RecruitmentTestUserID_UpdatedBy]) REFERENCES [dbo].[RecruitmentTestUser] ([RecruitmentTestUserID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

