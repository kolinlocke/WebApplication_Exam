CREATE TABLE [dbo].[RecruitmentTestAnswers] (
    [RecruitmentTestAnswersID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Answer]                   VARCHAR (8000) NULL,
    [IsDeleted]                BIT            NULL
);

