CREATE TABLE [dbo].[sm_TipoDispositivo] (
    [idTipoDispositivo] INT            NOT NULL,
    [nombreDispositivo] NVARCHAR (100) NULL,
    [idEstado]          INT            NULL,
    [CreatedDate]       DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([idTipoDispositivo] ASC)
);

