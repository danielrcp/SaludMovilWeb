CREATE TABLE [dbo].[sm_PacientePersonalMedico] (
    [idPacientePersonalMedico] INT           IDENTITY (1, 1) NOT NULL,
    [idPersonalMedico]         INT           NOT NULL,
    [idPaciente]               INT           NOT NULL,
    [idEstado]                 INT           NOT NULL,
    [observaciones]            NVARCHAR (50) NULL,
    [createdDate]              DATETIME      NOT NULL,
    [createdBy]                NCHAR (10)    NULL,
    [updatedDate]              DATETIME      NULL,
    [updatedBy]                NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_PacientexPersonalMedico] PRIMARY KEY CLUSTERED ([idPacientePersonalMedico] ASC)
);

