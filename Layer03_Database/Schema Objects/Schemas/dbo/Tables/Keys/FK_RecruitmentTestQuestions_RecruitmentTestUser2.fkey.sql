ALTER TABLE [dbo].[RecruitmentTestQuestions]
    ADD CONSTRAINT [FK_RecruitmentTestQuestions_RecruitmentTestUser2] FOREIGN KEY ([RecruitmentTestUserID_ApprovedBy]) REFERENCES [dbo].[RecruitmentTestUser] ([RecruitmentTestUserID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

