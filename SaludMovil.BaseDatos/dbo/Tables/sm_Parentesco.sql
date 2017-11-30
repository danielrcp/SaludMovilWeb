CREATE TABLE [dbo].[sm_Parentesco] (
    [idParentesco] INT           IDENTITY (1, 1) NOT NULL,
    [nombre]       NVARCHAR (50) NOT NULL,
    [idEstado]     INT           NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    [CreatedBy]    NVARCHAR (50) NOT NULL,
    [UpdatedDate]  DATETIME      NULL,
    [UpdatedBy]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_sm_Parentesco] PRIMARY KEY CLUSTERED ([idParentesco] ASC)
);

