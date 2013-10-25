ALTER TABLE [dbo].[RecruitmentTestContributorRegistrationRequest]
    ADD CONSTRAINT [FK_RecruitmentTestContributorRegistrationRequest_RecruitmentTestUser] FOREIGN KEY ([RecruitmentTestUserID_ApprovedBy]) REFERENCES [dbo].[RecruitmentTestUser] ([RecruitmentTestUserID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

