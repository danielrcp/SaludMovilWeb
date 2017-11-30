CREATE TABLE [dbo].[sm_TipoGuia] (
    [idTipoGuia]  INT            IDENTITY (1, 1) NOT NULL,
    [descripcion] NVARCHAR (150) NOT NULL,
    [idEstado]    INT            NOT NULL,
    [createdDate] DATETIME       NOT NULL,
    [createdBy]   NVARCHAR (50)  NOT NULL,
    [updatedDate] DATETIME       NULL,
    [updatedBy]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_sm_TipoGuia] PRIMARY KEY CLUSTERED ([idTipoGuia] ASC),
    CONSTRAINT [FK_sm_TipoGuia_sm_Estado] FOREIGN KEY ([idEstado]) REFERENCES [dbo].[sm_Estado] ([idEstado])
);

