CREATE TABLE [dbo].[sm_Alarma] (
    [idAlarma]     INT            IDENTITY (1, 1) NOT NULL,
    [descripcion]  NVARCHAR (150) NOT NULL,
    [idTipoAlarma] INT            NOT NULL,
    [idGuia]       INT            NOT NULL,
    [idEstado]     INT            NOT NULL,
    [dias]         INT            NOT NULL,
    [idMedioEnvio] INT            NULL,
    [createdDate]  DATETIME       NOT NULL,
    [createdBy]    NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_sm_Alarmas] PRIMARY KEY CLUSTERED ([idAlarma] ASC)
);

