CREATE TABLE [dbo].[sm_Persona] (
    [idTipoIdentificacion] INT            NOT NULL,
    [numeroIdentificacion] NVARCHAR (50)  NOT NULL,
    [primerNombre]         NVARCHAR (150) NOT NULL,
    [segundoNombre]        NVARCHAR (150) NULL,
    [primerApellido]       NVARCHAR (150) NOT NULL,
    [segundoApellido]      NVARCHAR (150) NULL,
    [idTipo]               INT            NOT NULL,
    [fechaNacimiento]      DATETIME       NOT NULL,
    [idCiudad]             INT            NULL,
    [celular]              NVARCHAR (50)  NULL,
    [telefonoFijo]         NVARCHAR (50)  NULL,
    [correo]               NVARCHAR (50)  NULL,
    [createdDate]          DATETIME       NOT NULL,
    [createdBy]            NVARCHAR (50)  NOT NULL,
    [updatedDate]          DATETIME       NULL,
    [updatedBy]            NVARCHAR (50)  NULL,
    CONSTRAINT [PK_sm_Persona_1] PRIMARY KEY CLUSTERED ([idTipoIdentificacion] ASC, [numeroIdentificacion] ASC),
    CONSTRAINT [FK_sm_Persona_sm_TipoPersona] FOREIGN KEY ([idTipo]) REFERENCES [dbo].[sm_TipoPersona] ([idTipo])
);

