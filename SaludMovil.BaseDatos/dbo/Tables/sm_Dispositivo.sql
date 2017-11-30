CREATE TABLE [dbo].[sm_Dispositivo] (
    [idDispositivo]     INT            NOT NULL,
    [nombreDispositivo] NVARCHAR (100) NULL,
    [idTipoDispositivo] INT            NULL,
    [descripcion]       NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([idDispositivo] ASC)
);

