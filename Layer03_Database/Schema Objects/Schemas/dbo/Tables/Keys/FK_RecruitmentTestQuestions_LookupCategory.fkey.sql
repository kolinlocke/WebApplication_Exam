ALTER TABLE [dbo].[RecruitmentTestQuestions]
    ADD CONSTRAINT [FK_RecruitmentTestQuestions_LookupCategory] FOREIGN KEY ([LookupCategoryID]) REFERENCES [dbo].[LookupCategory] ([LookupCategoryID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

