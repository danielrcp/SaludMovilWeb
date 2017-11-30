
-- ===================================================================================
-- Author:		Daniel Caballero
-- Create date: 01 Mayo 2016
-- Description:	Retorna listado de riesgos
-- ===================================================================================
CREATE PROCEDURE [dbo].[spRetornarRiesgos]
	
AS
BEGIN
	SET NOCOUNT ON;
	
	 SELECT idRiesgo,nombre   
	   FROM sm_riesgo 

END
