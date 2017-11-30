CREATE TABLE [dbo].[sm_Riesgo] (
    [idRiesgo]    INT           IDENTITY (1, 1) NOT NULL,
    [nombre]      NVARCHAR (50) NOT NULL,
    [idEstado]    INT           NOT NULL,
    [createdDate] DATETIME      NOT NULL,
    [createdBy]   NVARCHAR (50) NULL,
    [updatedDate] DATETIME      NULL,
    [updatedBy]   NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_Riesgo] PRIMARY KEY CLUSTERED ([idRiesgo] ASC)
);

