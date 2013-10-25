CREATE TABLE [dbo].[System_BindDefinition_Field] (
    [System_BindDefinition_FieldID] BIGINT         NOT NULL,
    [System_BindDefinitionID]       BIGINT         NULL,
    [Name]                          VARCHAR (1000) NULL,
    [Desc]                          VARCHAR (1000) NULL,
    [System_LookupID_FieldType]     INT            NULL,
    [IsReadOnly]                    BIT            NULL,
    [IsFilter]                      BIT            NULL,
    [Width]                         VARCHAR (50)   NULL,
    [NumberFormat]                  VARCHAR (50)   NULL,
    [CommandName]                   VARCHAR (1000) NULL,
    [System_LookupID_ButtonType]    INT            NULL,
    [FieldText]                     VARCHAR (50)   NULL,
    [OrderIndex]                    INT            NULL,
    [IsHidden]                      BIT            NULL
);

