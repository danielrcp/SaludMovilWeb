CREATE TABLE [dbo].[sm_Cargo] (
    [idCargo]     INT           NOT NULL,
    [descripcion] NVARCHAR (50) NULL,
    [idEstado]    INT           NULL,
    CONSTRAINT [PK_salud_Cargos] PRIMARY KEY CLUSTERED ([idCargo] ASC)
);

