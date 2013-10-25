CREATE TABLE [dbo].[RecruitmentTestExams_Answers] (
    [RecruitmentTestExams_AnswersID] BIGINT IDENTITY (1, 1) NOT NULL,
    [RecruitmentTestExamsID]         BIGINT NULL,
    [Lkp_RecruitmentTestQuestionsID] BIGINT NULL,
    [Lkp_RecruitmentTestAnswersID]   BIGINT NULL,
    [IsAnswer]                       BIT    NULL
);

