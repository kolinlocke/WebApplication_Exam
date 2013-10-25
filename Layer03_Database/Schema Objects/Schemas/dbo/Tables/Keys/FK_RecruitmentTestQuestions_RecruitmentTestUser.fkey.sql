ALTER TABLE [dbo].[RecruitmentTestQuestions]
    ADD CONSTRAINT [FK_RecruitmentTestQuestions_RecruitmentTestUser] FOREIGN KEY ([RecruitmentTestUserID_CreatedBy]) REFERENCES [dbo].[RecruitmentTestUser] ([RecruitmentTestUserID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

