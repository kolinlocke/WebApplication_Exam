CREATE TABLE [dbo].[RecruitmentTestExams] (
    [RecruitmentTestExamsID]     BIGINT   IDENTITY (1, 1) NOT NULL,
    [RecruitmentTestApplicantID] BIGINT   NULL,
    [LookupCategoryID]           BIGINT   NULL,
    [DateTaken]                  DATETIME NULL,
    [DateStart]                  DATETIME NULL,
    [DateEnd]                    DATETIME NULL,
    [Score]                      BIGINT   NULL,
    [TotalItems]                 BIGINT   NULL
);

