CREATE TABLE [dbo].[sm_UsuarioRol] (
    [IdUsuario] INT NOT NULL,
    [RoleId]    INT NOT NULL,
    CONSTRAINT [PK_sm_UsuarioRole] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [RoleId] ASC),
    CONSTRAINT [FK_sm_UsuarioRelRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[sm_Rol] ([idRol]),
    CONSTRAINT [FK_sm_UsuarioRol_sm_UsuarioRol] FOREIGN KEY ([IdUsuario], [RoleId]) REFERENCES [dbo].[sm_UsuarioRol] ([IdUsuario], [RoleId])
);

