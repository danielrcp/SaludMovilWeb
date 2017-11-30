CREATE TABLE [dbo].[sm_CentroSalud] (
    [idCentroSalud] INT            IDENTITY (1, 1) NOT NULL,
    [nombre]        NVARCHAR (150) NULL,
    [nit]           NVARCHAR (50)  NULL,
    [idEstado]      INT            NULL,
    [idCiudad]      INT            NULL,
    [CreatedBy]     NVARCHAR (50)  NULL,
    [CreatedDate]   DATETIME       NULL,
    [UpdatedBy]     NVARCHAR (50)  NULL,
    [UpdatedDate]   DATETIME       NULL,
    CONSTRAINT [PK_salud_CentrosSalud] PRIMARY KEY CLUSTERED ([idCentroSalud] ASC)
);

