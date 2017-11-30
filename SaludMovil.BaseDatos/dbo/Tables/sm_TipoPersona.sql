CREATE TABLE [dbo].[sm_TipoPersona] (
    [idTipo]      INT           IDENTITY (1, 1) NOT NULL,
    [nombre]      NVARCHAR (50) NOT NULL,
    [createdDate] DATETIME      NOT NULL,
    [createdBy]   NVARCHAR (50) NOT NULL,
    [updatedDate] DATETIME      NULL,
    [updatedBy]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_TipoPersona] PRIMARY KEY CLUSTERED ([idTipo] ASC)
);

