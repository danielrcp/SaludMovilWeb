CREATE TABLE [dbo].[sm_Usuario] (
    [idUsuario]            INT           IDENTITY (1, 1) NOT NULL,
    [idTipoIdentificacion] INT           NULL,
    [numeroIdentificacion] NVARCHAR (50) NOT NULL,
    [usuario]              VARCHAR (255) NOT NULL,
    [contrasena]           VARCHAR (255) NULL,
    [estado]               BIT           NOT NULL,
    [intentos]             INT           NULL,
    [createdBy]            VARCHAR (50)  NOT NULL,
    [createdDate]          DATETIME      NOT NULL,
    [updatedBy]            VARCHAR (50)  NULL,
    [updatedDate]          DATETIME      NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([idUsuario] ASC),
    CONSTRAINT [FK_sm_Usuario_sm_Persona] FOREIGN KEY ([idTipoIdentificacion], [numeroIdentificacion]) REFERENCES [dbo].[sm_Persona] ([idTipoIdentificacion], [numeroIdentificacion])
);

