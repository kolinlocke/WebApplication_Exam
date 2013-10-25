CREATE TABLE [dbo].[RecruitmentTestUser_Rights] (
    [RecruitmentTestUser_RightsID] BIGINT IDENTITY (1, 1) NOT NULL,
    [RecruitmentTestUserID]        BIGINT NULL,
    [RecruitmentTestRightsID]      BIGINT NULL,
    [IsActive]                     BIT    NULL
);

