CREATE TABLE [dbo].[sm_PacientePrograma] (
    [idTipoIdentificacion] INT           NOT NULL,
    [numeroIdentificacion] NVARCHAR (50) NOT NULL,
    [idPrograma]           INT           NOT NULL,
    [idEstado]             INT           NOT NULL,
    [observaciones]        NVARCHAR (50) NULL,
    [createdDate]          DATETIME      NOT NULL,
    [createdBy]            NVARCHAR (50) NOT NULL,
    [updatedDate]          DATETIME      NULL,
    [updatedBy]            NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_PacientePrograma] PRIMARY KEY CLUSTERED ([idTipoIdentificacion] ASC, [numeroIdentificacion] ASC),
    CONSTRAINT [FK_sm_PacientePrograma_sm_Persona] FOREIGN KEY ([idTipoIdentificacion], [numeroIdentificacion]) REFERENCES [dbo].[sm_Persona] ([idTipoIdentificacion], [numeroIdentificacion]),
    CONSTRAINT [FK_sm_PacientePrograma_sm_Programa] FOREIGN KEY ([idPrograma]) REFERENCES [dbo].[sm_Programa] ([idPrograma])
);

