CREATE TABLE [dbo].[sm_Notificacion] (
    [idNotificacion]     INT            IDENTITY (1, 1) NOT NULL,
    [idGuia]             INT            NULL,
    [idUsuario_Solicita] INT            NULL,
    [idUsuario_Envia]    INT            NULL,
    [idUsuario_Destino]  INT            NULL,
    [asunto]             VARCHAR (MAX)  NULL,
    [fecha_Respuesta]    DATETIME       NULL,
    [Resuelto]           BIT            NULL,
    [Leido]              BIT            NULL,
    [idTipoNotificacion] VARCHAR (1)    NULL,
    [detalle]            VARCHAR (MAX)  NULL,
    [idEstado]           NVARCHAR (256) NULL,
    [FechaCierre]        DATETIME       NULL,
    [enviado]            INT            NULL,
    CONSTRAINT [PK_sm_Notificaciones] PRIMARY KEY CLUSTERED ([idNotificacion] ASC)
);

