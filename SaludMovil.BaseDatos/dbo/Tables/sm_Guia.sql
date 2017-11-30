CREATE TABLE [dbo].[sm_Guia] (
    [idGuia]       INT            IDENTITY (1, 1) NOT NULL,
    [idTipoGuia]   INT            NOT NULL,
    [idCodigoTipo] NVARCHAR (50)  NOT NULL,
    [descripcion]  NVARCHAR (256) NOT NULL,
    [version]      NVARCHAR (50)  NULL,
    [idPrograma]   INT            NOT NULL,
    [createdDate]  DATETIME       NOT NULL,
    [createdBy]    NVARCHAR (50)  NULL,
    [updatedDate]  DATETIME       NULL,
    [updatedBy]    NVARCHAR (50)  NULL,
    [idEstado]     INT            NULL,
    CONSTRAINT [PK_sm_Guias] PRIMARY KEY CLUSTERED ([idGuia] ASC),
    CONSTRAINT [FK_sm_Guia_sm_Estado] FOREIGN KEY ([idEstado]) REFERENCES [dbo].[sm_Estado] ([idEstado]),
    CONSTRAINT [FK_sm_Guias_sm_Programas] FOREIGN KEY ([idPrograma]) REFERENCES [dbo].[sm_Programa] ([idPrograma]),
    CONSTRAINT [FK_sm_Guias_sm_TipoGuia] FOREIGN KEY ([idTipoGuia]) REFERENCES [dbo].[sm_TipoGuia] ([idTipoGuia])
);

