CREATE TABLE [dbo].[sm_Auditoria] (
    [idAuditoria] INT            IDENTITY (1, 1) NOT NULL,
    [tipo]        NVARCHAR (50)  NULL,
    [descripcion] NVARCHAR (MAX) NULL,
    [CreatedBy]   NVARCHAR (50)  NULL,
    [CreatedDate] DATETIME       NULL,
    CONSTRAINT [PK_sm_Auditoria] PRIMARY KEY CLUSTERED ([idAuditoria] ASC)
);

