CREATE TABLE [dbo].[RecruitmentTestQuestionAnswers] (
    [RecruitmentTestQuestionAnswersID] BIGINT IDENTITY (1, 1) NOT NULL,
    [Lkp_RecruitmentTestQuestionsID]   BIGINT NULL,
    [Lkp_RecruitmentTestAnswersID]     BIGINT NULL,
    [IsAnswer]                         BIT    NULL,
    [IsFixed]                          BIT    NULL,
    [OrderIndex]                       INT    NULL,
    [IsDeleted]                        BIT    NULL
);

