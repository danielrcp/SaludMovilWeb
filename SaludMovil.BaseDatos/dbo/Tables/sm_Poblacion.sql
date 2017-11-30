CREATE TABLE [dbo].[sm_Poblacion] (
    [idPoblacion] INT           IDENTITY (1, 1) NOT NULL,
    [descripcion] NVARCHAR (50) NOT NULL,
    [idEstado]    INT           NOT NULL,
    [createdBy]   NVARCHAR (50) NOT NULL,
    [createdDate] DATETIME      NOT NULL,
    [updatedBy]   NVARCHAR (50) NULL,
    [updatedDate] DATETIME      NULL,
    CONSTRAINT [PK_sm_Poblacion] PRIMARY KEY CLUSTERED ([idPoblacion] ASC)
);

