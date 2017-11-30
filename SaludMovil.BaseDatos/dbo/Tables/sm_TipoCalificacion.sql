CREATE TABLE [dbo].[sm_TipoCalificacion] (
    [idTipoCalificacion] INT           NOT NULL,
    [descripcion]        NVARCHAR (50) NULL,
    [idEstado]           INT           NULL,
    CONSTRAINT [PK_salud_TipoRiesgos] PRIMARY KEY CLUSTERED ([idTipoCalificacion] ASC)
);

