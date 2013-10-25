ALTER TABLE [dbo].[RecruitmentTestQuestions]
    ADD CONSTRAINT [FK_RecruitmentTestQuestions_LookupQuestionType] FOREIGN KEY ([LookupQuestionTypeID]) REFERENCES [dbo].[LookupQuestionType] ([LookupQuestionTypeID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

