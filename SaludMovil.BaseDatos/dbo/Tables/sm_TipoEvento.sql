CREATE TABLE [dbo].[sm_TipoEvento] (
    [idTipoEvento] INT            IDENTITY (1, 1) NOT NULL,
    [descripcion]  NVARCHAR (100) NULL,
    [createdBy]    NVARCHAR (50)  NULL,
    [createdDate]  DATETIME       NULL,
    CONSTRAINT [PK_sm_TipoEvento] PRIMARY KEY CLUSTERED ([idTipoEvento] ASC)
);

