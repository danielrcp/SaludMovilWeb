CREATE TABLE [dbo].[sm_Sincronizacion] (
    [idVersion]     INT            IDENTITY (1, 1) NOT NULL,
    [fechaCambio]   DATETIME       NULL,
    [tipoCambio]    NVARCHAR (256) NULL,
    [total_Parcial] NCHAR (10)     NULL,
    [mandatoria]    NCHAR (10)     NULL,
    [instruccion]   NVARCHAR (256) NULL,
    [origen]        NVARCHAR (50)  NULL,
    [createdDate]   DATETIME       NULL,
    [createdBy]     NVARCHAR (50)  NULL,
    [updatedDate]   DATETIME       NULL,
    [updatedBy]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_sm_Sincronizacion] PRIMARY KEY CLUSTERED ([idVersion] ASC)
);

