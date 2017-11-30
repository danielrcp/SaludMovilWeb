CREATE TABLE [dbo].[sm_ParametroReglas] (
    [idParametroReglas] INT            NOT NULL,
    [idReglas]          INT            NULL,
    [descripcion]       NVARCHAR (100) NULL,
    [idEstado]          INT            NULL,
    PRIMARY KEY CLUSTERED ([idParametroReglas] ASC)
);

