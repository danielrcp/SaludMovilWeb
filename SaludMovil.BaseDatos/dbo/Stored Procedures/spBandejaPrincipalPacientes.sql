
-- ===================================================================================
-- Author:		Daniel Caballero
-- Create date: 11 Abril 2016
-- Description:	Consulta bandeja principal portál médico
-- ===================================================================================
CREATE PROCEDURE [dbo].[spBandejaPrincipalPacientes]
	
AS
BEGIN
	SET NOCOUNT ON;
	
	 SELECT pac.idTipoIdentificacion,ti.nombre as TipoIdentificacion,pac.numeroIdentificacion as NumeroIdentifacion,per.primerNombre + ' ' +per.segundoNombre as Nombres, 
	        per.primerApellido + ' ' + per.segundoApellido as Apellidos,CONVERT(VARCHAR(10),pac.fechaRegistro,110) as FechaRegistro,
	        (select primerNombre + ' ' + primerApellido from sm_Persona where idtipoIdentificacion = med.idTipoIdentificacion AND numeroIdentificacion = med.numeroIdentificacion) as medicoTratante,
			cs.nombre as institucion
	   FROM sm_Persona per
 INNER JOIN sm_Paciente pac on per.idTipoIdentificacion = pac.idTipoIdentificacion AND per.numeroIdentificacion = pac.numeroIdentificacion
 INNER JOIN sm_PersonalMedico med on pac.idTipoIdentificacionMedico = med.idTipoIdentificacion AND pac.numeroIdentificacionMedico = med.numeroIdentificacion
 INNER JOIN sm_Ciudad ciudad on ciudad.idCiudad = per.idCiudad
 INNER JOIN sm_TipoIdentificacion ti on per.idTipoIdentificacion = ti.idTipoIdentificacion
 INNER JOIN sm_CentroSalud cs on cs.idCentroSalud = pac.institucion
END
