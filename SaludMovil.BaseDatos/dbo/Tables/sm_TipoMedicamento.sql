CREATE TABLE [dbo].[sm_TipoMedicamento] (
    [idTipo]      INT            IDENTITY (1, 1) NOT NULL,
    [descripcion] NVARCHAR (100) NULL,
    [idEstado]    INT            NULL,
    PRIMARY KEY CLUSTERED ([idTipo] ASC)
);

