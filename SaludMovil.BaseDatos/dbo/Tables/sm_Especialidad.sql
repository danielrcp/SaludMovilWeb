CREATE TABLE [dbo].[sm_Especialidad] (
    [idEspecialidad]   INT            IDENTITY (1, 1) NOT NULL,
    [tipoEspecialidad] INT            NOT NULL,
    [nombre]           NVARCHAR (150) NOT NULL,
    [estado]           INT            NOT NULL,
    [createdDate]      DATETIME       NOT NULL,
    [createdBy]        NVARCHAR (50)  NOT NULL,
    [updatedDate]      DATETIME       NULL,
    [updatedBy]        NVARCHAR (50)  NULL,
    CONSTRAINT [PK_sm_Especialidad] PRIMARY KEY CLUSTERED ([idEspecialidad] ASC)
);

