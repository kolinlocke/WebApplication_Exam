﻿ALTER TABLE [dbo].[RecruitmentTestQuestionAnswers]
    ADD CONSTRAINT [PK_QuestionAnswer] PRIMARY KEY CLUSTERED ([RecruitmentTestQuestionAnswersID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
