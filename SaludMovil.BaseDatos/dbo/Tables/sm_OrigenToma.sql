CREATE TABLE [dbo].[sm_OrigenToma] (
    [idOrigenToma] INT            IDENTITY (1, 1) NOT NULL,
    [descripcion]  NVARCHAR (100) NULL,
    [createdBy]    NVARCHAR (50)  NULL,
    [createdDate]  DATETIME       NULL,
    CONSTRAINT [PK_sm_OrigenToma] PRIMARY KEY CLUSTERED ([idOrigenToma] ASC)
);

