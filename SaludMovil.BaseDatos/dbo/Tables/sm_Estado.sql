CREATE TABLE [dbo].[sm_Estado] (
    [idEstado] INT           NOT NULL,
    [nombre]   NVARCHAR (50) NULL,
    [tabla]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_salud_Estados] PRIMARY KEY CLUSTERED ([idEstado] ASC)
);

