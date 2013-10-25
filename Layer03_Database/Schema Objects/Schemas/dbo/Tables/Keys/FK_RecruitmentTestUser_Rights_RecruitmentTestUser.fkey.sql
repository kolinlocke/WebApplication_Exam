ALTER TABLE [dbo].[RecruitmentTestUser_Rights]
    ADD CONSTRAINT [FK_RecruitmentTestUser_Rights_RecruitmentTestUser] FOREIGN KEY ([RecruitmentTestUserID]) REFERENCES [dbo].[RecruitmentTestUser] ([RecruitmentTestUserID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

