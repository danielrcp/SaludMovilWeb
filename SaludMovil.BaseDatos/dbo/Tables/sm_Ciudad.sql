CREATE TABLE [dbo].[sm_Ciudad] (
    [idCiudad]       INT           IDENTITY (1, 1) NOT NULL,
    [idDepartamento] INT           NULL,
    [nombre]         NVARCHAR (50) NULL,
    [codigo]         NVARCHAR (50) NULL,
    [CreatedBy]      NVARCHAR (50) NULL,
    [CreatedDate]    DATETIME      NULL,
    [UpdatedBy]      NVARCHAR (50) NULL,
    [UpdatedDate]    DATETIME      NULL,
    CONSTRAINT [PK_salud_Ciudad] PRIMARY KEY CLUSTERED ([idCiudad] ASC),
    CONSTRAINT [FK_sm_Ciudad_sm_Departamento] FOREIGN KEY ([idDepartamento]) REFERENCES [dbo].[sm_Departamento] ([idDepartamento])
);

