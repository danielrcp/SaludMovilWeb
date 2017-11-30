
-- ===================================================================================
-- Author:		Daniel Caballero
-- Create date: 11 Abril 2016
-- Description:	Retorna lista de medicos tratantes
-- ===================================================================================
CREATE PROCEDURE [dbo].[spRetornarMedicosTratantes]
  @idTipoPersona int
AS
BEGIN
	SET NOCOUNT ON;
	 SELECT cast(p.idTipoIdentificacion as varchar)+'|'+p.numeroIdentificacion as idPersonalMedico,p.primerNombre  + ' '+ p.primerApellido  as Nombres 
	   FROM sm_Persona p
	  WHERE p.idTipo = @idTipoPersona
END




