CREATE TABLE [dbo].[sm_RolOpcion] (
    [idRol]       INT           NOT NULL,
    [idOpcion]    INT           NOT NULL,
    [crear]       BIT           NULL,
    [leer]        BIT           NULL,
    [actualizar]  BIT           NULL,
    [eliminar]    BIT           NULL,
    [createdBy]   NVARCHAR (50) NOT NULL,
    [createdDate] DATETIME      NOT NULL,
    [updatedBy]   NVARCHAR (50) NULL,
    [updatedDate] DATETIME      NULL,
    CONSTRAINT [PK_sm_RolOpcion] PRIMARY KEY CLUSTERED ([idRol] ASC, [idOpcion] ASC),
    CONSTRAINT [FK_sm_RolOpcion_sm_Opcion] FOREIGN KEY ([idOpcion]) REFERENCES [dbo].[sm_Opcion] ([IdOpcion]),
    CONSTRAINT [FK_sm_RolOpcion_sm_Rol] FOREIGN KEY ([idRol]) REFERENCES [dbo].[sm_Rol] ([idRol])
);

