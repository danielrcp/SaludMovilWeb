-- ==========================================================================
-- Author:		Andres Felipe Silva Pascuas
-- Create date: 6 Abril 2016
-- Description:	Recibe como parametro una consulta sql y retorna el resultado
-- ==========================================================================
CREATE PROCEDURE [dbo].[spConsultaSistema]
	@sql nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	EXEC sp_executesql @sql
END
