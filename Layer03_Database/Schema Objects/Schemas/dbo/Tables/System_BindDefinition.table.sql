CREATE TABLE [dbo].[System_BindDefinition] (
    [System_BindDefinitionID] BIGINT         NOT NULL,
    [Name]                    VARCHAR (1000) NULL,
    [TableName]               VARCHAR (1000) NULL,
    [TableKey]                VARCHAR (1000) NULL,
    [Condition]               VARCHAR (8000) NULL,
    [Sort]                    VARCHAR (8000) NULL
);

