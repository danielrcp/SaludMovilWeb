CREATE TABLE [dbo].[sm_Opcion] (
    [IdOpcion]      INT           IDENTITY (1, 1) NOT NULL,
    [NombreOpcion]  VARCHAR (255) NOT NULL,
    [URL]           VARCHAR (255) NULL,
    [Orden]         INT           DEFAULT ((0)) NOT NULL,
    [Descripcion]   VARCHAR (255) DEFAULT (NULL) NULL,
    [IdOpcionPadre] INT           DEFAULT (NULL) NULL,
    [Activa]        BIT           DEFAULT ((0)) NULL,
    [URLImagen]     VARCHAR (255) DEFAULT (NULL) NULL,
    [CreatedBy]     VARCHAR (50)  NULL,
    [CreatedDate]   DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedBy]     VARCHAR (50)  NULL,
    [UpdatedDate]   DATETIME      DEFAULT (getdate()) NULL,
    CONSTRAINT [pk_Opcion] PRIMARY KEY CLUSTERED ([IdOpcion] ASC)
);

