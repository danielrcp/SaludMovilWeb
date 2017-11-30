CREATE TABLE [dbo].[sm_Antecedente] (
    [idAntecedente] INT            IDENTITY (1, 1) NOT NULL,
    [descripcion]   NVARCHAR (150) NOT NULL,
    [idEstado]      INT            NOT NULL,
    [createdDate]   DATETIME       NULL,
    [createdBy]     NVARCHAR (50)  NULL,
    [updatedDate]   DATETIME       NULL,
    [updatedBy]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_sm_antecedentes] PRIMARY KEY CLUSTERED ([idAntecedente] ASC)
);

