CREATE TABLE [dbo].[sm_Pais] (
    [idPais]      INT            IDENTITY (1, 1) NOT NULL,
    [codigo]      NVARCHAR (50)  NULL,
    [nombre]      NVARCHAR (150) NOT NULL,
    [estado]      INT            NOT NULL,
    [CreatedDate] DATETIME       NULL,
    [CreatedBy]   NVARCHAR (50)  NULL,
    [UpdatedDate] DATETIME       NULL,
    [UpdatedBy]   NVARCHAR (50)  NULL,
    CONSTRAINT [PK_sm_Pais] PRIMARY KEY CLUSTERED ([idPais] ASC)
);

