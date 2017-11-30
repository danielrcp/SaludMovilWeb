-- ===========================================================================================
-- Author:		Andres Felipe Silva Pascuas
-- Create date: 25 Abril 2016
-- Description:	Recibe por parametro el usuario y la constraseña y retorna la persona completa
-- ===========================================================================================
CREATE PROCEDURE [dbo].[spAutenticacion]
	@Usuario nvarchar(50),
	@Contrasena nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	select p.idTipoIdentificacion
	,p.numeroIdentificacion
	,p.primerNombre
	,p.segundoNombre
	,p.primerApellido
	,p.segundoApellido
	,p.fechaNacimiento
	,p.idCiudad
	,c.nombre
	,p.celular
	,p.telefonoFijo
	,p.correo
	,case when pa.segmento is null then 'NA' else pa.segmento end segmento	
	,case when pa.planMp is null then 'NA' else pa.planMp end planMp
	,case when pa.tipoContrato is null then 'NA' else pa.tipoContrato end tipoContrato
	,case when pa.nombreColectivo is null then 'NA' else pa.nombreColectivo end  nombreColectivo
	,case when pa.institucion is null then 0 else pa.institucion end institucion
	,case when pa.idTipoIdentificacionMedico is null then 0 else pa.idTipoIdentificacionMedico end idTipoIdentificacionMedico
	,case when pa.numeroIdentificacionMedico is null then 'NA' else pa.numeroIdentificacionMedico end  numeroIdentificacionMedico
	,case when pa.recibeNotParentesco is null then 0 else pa.recibeNotParentesco end recibeNotParentesco
	,case when pa.nombresParantesco is null then 'NA' else pa.nombresParantesco end nombresParantesco
	,case when pa.idParentesco is null then 0 else pa.idParentesco end idParentesco	
	,case when par.nombre is null then 'NA' else par.nombre end parentesco
	,case when pa.celularParentesco is null then 'NA' else pa.celularParentesco end celularParentesco
	,case when pa.telefonoFijoParentesco is null then 'NA' else pa.telefonoFijoParentesco end telefonoFijoParentesco
	,case when pa.correoParentesco is null then 'NA' else pa.correoParentesco end correoParentesco
	,case when pa.idTipoIngreso is null then 0 else pa.idTipoIngreso end idTipoIngreso
	,case when ti.descripcion is null then 'NA' else ti.descripcion end nombreTipoIngreso	
	,case when pa.idMedioAtencion is null then 0 else pa.idMedioAtencion end idMedioAtencion
	,case when ma.descripcion is null then 'NA' else ma.descripcion end medioAtencion
	,case when pa.fechaRegistro is null then '1900-01-01' else pa.fechaRegistro end fechaRegistro
	,case when pa.riesgo is null then 0 else pa.riesgo end riesgo
	,case when pa.idEstado is null then 0 else pa.idEstado end idEstado
	,case when e.nombre is null then 'NA' else e.nombre end nombreEstado
	,'NA' as nombreEspecialidad
	,0 as tipoEspecialidad
	,p.createdBy
	,p.createdDate
	,p.updatedBy
	,p.updatedDate
	from sm_Persona p
	inner join sm_Usuario u on p.idTipoIdentificacion = u.idTipoIdentificacion and p.numeroIdentificacion = u.numeroIdentificacion
	left join sm_Paciente pa on p.idTipoIdentificacion = pa.idTipoIdentificacion and p.numeroIdentificacion = pa.numeroIdentificacion
	left join sm_Parentesco par on pa.idParentesco = par.idParentesco
	left join sm_Estado e on pa.idEstado = e.idEstado
	left join sm_MedioAtencion ma on pa.idMedioAtencion = ma.idMedioAtencion
	left join sm_TipoIngreso ti on pa.idTipoIngreso = ti.idTipoIngreso
	left join sm_Ciudad c on p.idCiudad = c.idCiudad
	where u.usuario = @Usuario and u.contrasena = @contrasena
	union all
	select p.idTipoIdentificacion
	,p.numeroIdentificacion
	,p.primerNombre
	,p.segundoNombre
	,p.primerApellido
	,p.segundoApellido
	,p.fechaNacimiento
	,p.idCiudad
	,c.nombre
	,p.celular
	,p.telefonoFijo
	,p.correo
	,'NA'
	,'NA'
	,'NA'
	,'NA'
	,0
	,0
	,'NA'
	,0
	,'NA'
	,0
	,'NA'
	,'NA'
	,'NA'
	,'NA'
	,0
	,'NA' as nombreTipoIngreso
	,0
	,'NA' as medioAtencion
	,0
	,0
	,case when pm.idEstado is null then 0 else pm.idEstado end idEstado
	,case when e.nombre is null then 'NA' else e.nombre end nombreEstado
	,case when esp.nombre is null then 'NA' else esp.nombre end nombre
	,case when esp.tipoEspecialidad is null then 0 else esp.tipoEspecialidad end tipoEspecialidad
	,p.createdBy
	,p.createdDate
	,p.updatedBy
	,p.updatedDate
	from sm_Persona p
	inner join sm_Usuario u on p.idTipoIdentificacion = u.idTipoIdentificacion and p.numeroIdentificacion = u.numeroIdentificacion
	left join sm_PersonalMedico pm on p.idTipoIdentificacion = pm.idTipoIdentificacion and p.numeroIdentificacion = pm.numeroIdentificacion
	left join sm_Estado e on pm.idEstado = e.idEstado
	left join sm_Especialidad esp on pm.idEspecialidad = esp.idEspecialidad
	left join sm_Ciudad c on p.idCiudad = c.idCiudad
	where u.usuario = @Usuario and u.contrasena = @Contrasena

	declare @idUsuario int
	select @idUsuario = idUsuario from sm_Usuario where usuario = @Usuario and contrasena = @Contrasena

	select r.[idRol]
      ,r.[nombre]
      ,r.[soloConsulta]
      ,r.[createdBy]
      ,r.[createdDate]
      ,r.[updatedBy]
      ,r.[updatedDate]
  from [dbo].[sm_Rol] r
  inner join sm_UsuarioRol ur on r.idRol = ur.RoleId
  where ur.IdUsuario = @idUsuario  

  select  ro.idRol
  ,ro.idOpcion
  ,ro.crear
  ,ro.leer
  ,ro.actualizar
  ,ro.eliminar
  ,o.NombreOpcion
  ,o.URL
  ,o.Orden
  ,o.IdOpcionPadre
  ,o.Descripcion
  ,o.Activa
  ,o.URLImagen
  ,o.createdBy
  ,o.createdDate
  ,o.UpdatedBy
  ,o.UpdatedDate
  from sm_RolOpcion ro
  inner join sm_Opcion o on ro.idOpcion = o.IdOpcion
END
