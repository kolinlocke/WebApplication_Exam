CREATE TABLE [dbo].[RecruitmentTestUser] (
    [RecruitmentTestUserID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                  VARCHAR (50)   NULL,
    [Password]              VARCHAR (50)   NULL,
    [Email]                 VARCHAR (1000) NULL,
    [LookupUserTypeID]      BIGINT         NULL,
    [IsAdministrator]       BIT            NULL,
    [IsDeleted]             BIT            NULL
);

