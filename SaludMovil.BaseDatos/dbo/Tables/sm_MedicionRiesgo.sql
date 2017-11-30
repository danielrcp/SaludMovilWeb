﻿CREATE TABLE [dbo].[sm_MedicionRiesgo] (
    [idMedicionesRiesgo] INT             NOT NULL,
    [idTipoMedicion]     INT             NOT NULL,
    [idPaciente]         INT             NOT NULL,
    [sexo]               NCHAR (10)      NULL,
    [edad]               INT             NULL,
    [colesterolTotal]    DECIMAL (18, 2) NULL,
    [colesterolHDL]      DECIMAL (18, 2) NULL,
    [presionSistolica]   DECIMAL (18, 2) NULL,
    [esFumador]          BIT             NULL,
    [diabetes]           BIT             NULL,
    [peso]               DECIMAL (18, 2) NULL,
    [talla]              DECIMAL (18, 2) NULL,
    [creatininaSerica]   DECIMAL (18, 2) NULL,
    [idEstado]           INT             NULL,
    [createdDate]        DATETIME        NULL,
    [createdBy]          NVARCHAR (50)   NULL,
    [updatedDate]        DATETIME        NULL,
    [updatedBy]          NVARCHAR (50)   NULL,
    CONSTRAINT [PK_sm_MedicionesRiesgo] PRIMARY KEY CLUSTERED ([idMedicionesRiesgo] ASC)
);

