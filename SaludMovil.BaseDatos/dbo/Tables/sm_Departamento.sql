CREATE TABLE [dbo].[sm_Departamento] (
    [idDepartamento] INT            IDENTITY (1, 1) NOT NULL,
    [idPais]         INT            NOT NULL,
    [codigo]         NVARCHAR (50)  NULL,
    [nombre]         NVARCHAR (150) NOT NULL,
    [estado]         INT            NOT NULL,
    [createdDate]    DATETIME       NOT NULL,
    [createdBy]      NVARCHAR (50)  NOT NULL,
    [updatedDate]    DATETIME       NULL,
    [updatedBy]      NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED ([idDepartamento] ASC),
    CONSTRAINT [FK_sm_Departamento_sm_Pais] FOREIGN KEY ([idPais]) REFERENCES [dbo].[sm_Pais] ([idPais])
);

