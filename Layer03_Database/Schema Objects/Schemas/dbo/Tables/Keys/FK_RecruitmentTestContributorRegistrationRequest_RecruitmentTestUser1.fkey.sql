ALTER TABLE [dbo].[RecruitmentTestContributorRegistrationRequest]
    ADD CONSTRAINT [FK_RecruitmentTestContributorRegistrationRequest_RecruitmentTestUser1] FOREIGN KEY ([RecruitmentTestUserID]) REFERENCES [dbo].[RecruitmentTestUser] ([RecruitmentTestUserID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

