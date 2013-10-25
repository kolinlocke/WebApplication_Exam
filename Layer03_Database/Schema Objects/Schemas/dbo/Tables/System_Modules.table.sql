CREATE TABLE [dbo].[System_Modules] (
    [System_ModulesID]        BIGINT         NOT NULL,
    [System_ModulesID_Parent] VARCHAR (1000) NULL,
    [Name]                    VARCHAR (1000) NULL,
    [Code]                    VARCHAR (1000) NULL,
    [Module_List]             VARCHAR (1000) NULL,
    [Module_Details]          VARCHAR (1000) NULL,
    [Arguments]               VARCHAR (1000) NULL,
    [IsHidden]                BIT            NULL,
    [OrderIndex]              INT            NULL
);

