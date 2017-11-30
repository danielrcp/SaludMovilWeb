CREATE TABLE [dbo].[sm_AntecedentePaciente] (
    [idAntecedentePaciente] INT           IDENTITY (1, 1) NOT NULL,
    [idAntecedente]         INT           NOT NULL,
    [idPaciente]            INT           NOT NULL,
    [idEstado]              INT           NULL,
    [createdDate]           DATETIME      NULL,
    [createdBy]             NVARCHAR (50) NULL,
    [updatedDate]           DATETIME      NULL,
    [updatedBy]             NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_AntecedentesPaciente] PRIMARY KEY CLUSTERED ([idAntecedentePaciente] ASC),
    CONSTRAINT [FK_sm_AntecedentesPaciente_sm_antecedentes] FOREIGN KEY ([idAntecedente]) REFERENCES [dbo].[sm_Antecedente] ([idAntecedente])
);

