ALTER TABLE [dbo].[RecruitmentTestRights_Details]
    ADD CONSTRAINT [FK_RecruitmentTestRights_Details_RecruitmentTestRights] FOREIGN KEY ([RecruitmentTestRightsID]) REFERENCES [dbo].[RecruitmentTestRights] ([RecruitmentTestRightsID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

