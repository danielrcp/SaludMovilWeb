CREATE TABLE [dbo].[sm_Medicamento] (
    [idMedicamento]     INT            IDENTITY (1, 1) NOT NULL,
    [descripcion]       NVARCHAR (100) NULL,
    [idEstado]          INT            NULL,
    [idTipoMedicamento] INT            NULL,
    [CreatedBy]         NVARCHAR (50)  NULL,
    [CreatedDate]       DATETIME       NULL,
    [UpdatedBy]         NVARCHAR (50)  NULL,
    [UpdatedDate]       DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([idMedicamento] ASC)
);

