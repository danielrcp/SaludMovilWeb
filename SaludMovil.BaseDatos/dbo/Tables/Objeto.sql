CREATE TABLE [dbo].[Objeto] (
    [IdObjeto]                 INT          NOT NULL,
    [nombreObjeto]             VARCHAR (50) NOT NULL,
    [idTipoObjeto]             SMALLINT     NOT NULL,
    [nombreTablaMaestra]       VARCHAR (30) NOT NULL,
    [esAdministrable]          BIT          NULL,
    [admiteAtributosVariables] BIT          NULL
);

