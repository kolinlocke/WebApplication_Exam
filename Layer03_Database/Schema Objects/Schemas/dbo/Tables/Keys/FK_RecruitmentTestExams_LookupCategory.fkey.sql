ALTER TABLE [dbo].[RecruitmentTestExams]
    ADD CONSTRAINT [FK_RecruitmentTestExams_LookupCategory] FOREIGN KEY ([LookupCategoryID]) REFERENCES [dbo].[LookupCategory] ([LookupCategoryID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

