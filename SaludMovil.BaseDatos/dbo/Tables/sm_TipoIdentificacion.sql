CREATE TABLE [dbo].[sm_TipoIdentificacion] (
    [idTipoIdentificacion] INT           IDENTITY (1, 1) NOT NULL,
    [nombre]               NVARCHAR (50) NULL,
    [CreatedBy]            NVARCHAR (50) NULL,
    [CreatedDate]          DATETIME      NULL,
    [UpdatedBy]            NVARCHAR (50) NULL,
    [UpdatedDate]          DATETIME      NULL,
    CONSTRAINT [PK_salud_TiposIdentificacion] PRIMARY KEY CLUSTERED ([idTipoIdentificacion] ASC)
);

