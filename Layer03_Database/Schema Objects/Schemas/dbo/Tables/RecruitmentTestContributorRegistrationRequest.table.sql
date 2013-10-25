CREATE TABLE [dbo].[RecruitmentTestContributorRegistrationRequest] (
    [RecruitmentTestContributorRegistrationRequestID] BIGINT       IDENTITY (1, 1) NOT NULL,
    [Name]                                            VARCHAR (50) NULL,
    [Email]                                           VARCHAR (50) NULL,
    [DateRequested]                                   DATETIME     NULL,
    [IsApproved]                                      BIT          NULL,
    [DateApproved]                                    DATETIME     NULL,
    [RecruitmentTestUserID_ApprovedBy]                BIGINT       NULL,
    [RecruitmentTestUserID]                           BIGINT       NULL
);

