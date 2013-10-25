CREATE TABLE [dbo].[RecruitmentTestRights_Details] (
    [RecruitmentTestRights_DetailsID] BIGINT IDENTITY (1, 1) NOT NULL,
    [RecruitmentTestRightsID]         BIGINT NULL,
    [System_Modules_AccessID]         BIGINT NULL,
    [IsAllowed]                       BIT    NULL
);

