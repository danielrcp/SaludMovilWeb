CREATE TABLE [dbo].[sm_PersonalMedico] (
    [idTipoIdentificacion] INT           NOT NULL,
    [numeroIdentificacion] NVARCHAR (50) NOT NULL,
    [idEspecialidad]       INT           NOT NULL,
    [idEstado]             INT           NOT NULL,
    [createdDate]          DATETIME      NOT NULL,
    [createdBy]            NVARCHAR (50) NOT NULL,
    [updatedDate]          DATETIME      NULL,
    [updatedBy]            NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_PersonalMedico] PRIMARY KEY CLUSTERED ([idTipoIdentificacion] ASC, [numeroIdentificacion] ASC),
    CONSTRAINT [FK_sm_PersonalMedico_sm_Persona] FOREIGN KEY ([idTipoIdentificacion], [numeroIdentificacion]) REFERENCES [dbo].[sm_Persona] ([idTipoIdentificacion], [numeroIdentificacion])
);

