CREATE TABLE [dbo].[sm_TipoIngreso] (
    [idTipoIngreso] INT           IDENTITY (1, 1) NOT NULL,
    [descripcion]   NVARCHAR (50) NOT NULL,
    [idEstado]      INT           NOT NULL,
    [CreatedDate]   DATETIME      NOT NULL,
    [CreatedBy]     NVARCHAR (50) NOT NULL,
    [UpdatedDate]   DATETIME      NULL,
    [UpdatedBy]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_TipoIngreso] PRIMARY KEY CLUSTERED ([idTipoIngreso] ASC)
);

