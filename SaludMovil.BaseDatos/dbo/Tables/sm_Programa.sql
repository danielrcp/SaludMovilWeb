CREATE TABLE [dbo].[sm_Programa] (
    [idPrograma]        INT            IDENTITY (1, 1) NOT NULL,
    [descripcion]       NVARCHAR (200) NOT NULL,
    [idEstado]          INT            NOT NULL,
    [createdDate]       DATETIME       NOT NULL,
    [createdBy]         NVARCHAR (200) NOT NULL,
    [updatedDate]       DATETIME       NULL,
    [updatedBy]         NVARCHAR (200) NULL,
    [fechaInicio]       DATETIME       DEFAULT (getdate()) NOT NULL,
    [fechaFin]          DATETIME       DEFAULT (getdate()) NOT NULL,
    [poblacionObjetivo] INT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_salud_Programas] PRIMARY KEY CLUSTERED ([idPrograma] ASC),
    CONSTRAINT [FK_salud_Estado] FOREIGN KEY ([idEstado]) REFERENCES [dbo].[sm_Estado] ([idEstado]),
    CONSTRAINT [FK_salud_Poblacion] FOREIGN KEY ([poblacionObjetivo]) REFERENCES [dbo].[sm_Poblacion] ([idPoblacion])
);

