CREATE TABLE [dbo].[sm_Calendario] (
    [idCalendario] INT           IDENTITY (1, 1) NOT NULL,
    [idPaciente]   INT           NOT NULL,
    [fechaDesde]   DATE          NULL,
    [horaDesde]    TIME (7)      NULL,
    [fechaHasta]   DATE          NULL,
    [horaHasta]    TIME (7)      NULL,
    [descripcion]  NVARCHAR (50) NULL,
    [idOrigen]     INT           NOT NULL,
    [idEstado]     INT           NOT NULL,
    [createdDate]  DATETIME      NULL,
    [createdBy]    NVARCHAR (50) NULL,
    [updatedDate]  DATETIME      NULL,
    [updatedBy]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_Calendario] PRIMARY KEY CLUSTERED ([idCalendario] ASC)
);

