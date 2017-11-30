CREATE TABLE [dbo].[sm_EncabezadoValoracionRiesgo] (
    [idMedicionRiesgo]   INT           IDENTITY (1, 1) NOT NULL,
    [idTipoCalificacion] INT           NOT NULL,
    [idPaciente]         INT           NOT NULL,
    [resultado]          INT           NOT NULL,
    [createdDate]        DATETIME      NULL,
    [createdBy]          NVARCHAR (50) NULL,
    [updatedDate]        DATETIME      NULL,
    [updatedBy]          NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_EncabezadoValoracionRiesgo] PRIMARY KEY CLUSTERED ([idMedicionRiesgo] ASC)
);

