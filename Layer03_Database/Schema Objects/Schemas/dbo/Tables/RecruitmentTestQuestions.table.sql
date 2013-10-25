CREATE TABLE [dbo].[RecruitmentTestQuestions] (
    [RecruitmentTestQuestionsID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Question]                         VARCHAR (8000) NULL,
    [LookupCategoryID]                 BIGINT         NULL,
    [LookupQuestionTypeID]             BIGINT         NULL,
    [IsMultipleAnswer]                 BIT            NULL,
    [RecruitmentTestUserID_CreatedBy]  BIGINT         NULL,
    [RecruitmentTestUserID_UpdatedBy]  BIGINT         NULL,
    [DateCreated]                      DATETIME       NULL,
    [DateUpdated]                      DATETIME       NULL,
    [IsApproved]                       BIT            NULL,
    [RecruitmentTestUserID_ApprovedBy] BIGINT         NULL,
    [DateApproved]                     DATETIME       NULL,
    [IsDeleted]                        BIT            NULL
);

