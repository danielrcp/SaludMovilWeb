CREATE TABLE [dbo].[sm_Rol] (
    [idRol]        INT           NOT NULL,
    [nombre]       VARCHAR (50)  NOT NULL,
    [soloConsulta] BIT           NOT NULL,
    [createdBy]    VARCHAR (255) NOT NULL,
    [createdDate]  DATETIME      NOT NULL,
    [updatedBy]    VARCHAR (255) NULL,
    [updatedDate]  DATETIME      NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([idRol] ASC)
);

