CREATE TABLE [dbo].[sm_TipoAlarma] (
    [idTipoAlarma] SMALLINT     IDENTITY (1, 1) NOT NULL,
    [Nombre]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_TipoAlarma] PRIMARY KEY CLUSTERED ([idTipoAlarma] ASC)
);

