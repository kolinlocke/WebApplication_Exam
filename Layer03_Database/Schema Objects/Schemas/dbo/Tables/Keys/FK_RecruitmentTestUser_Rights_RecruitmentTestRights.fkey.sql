ALTER TABLE [dbo].[RecruitmentTestUser_Rights]
    ADD CONSTRAINT [FK_RecruitmentTestUser_Rights_RecruitmentTestRights] FOREIGN KEY ([RecruitmentTestRightsID]) REFERENCES [dbo].[RecruitmentTestRights] ([RecruitmentTestRightsID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

