-- ===============================================================
-- Author:		Andres Felipe Silva Pascuas
-- Create date: 26 Abril 2016
-- Description:	Con el rol como parametro se retornan sus opciones
-- ===============================================================
CREATE PROCEDURE spOpcionesRol
	@rol int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT o.IdOpcion
	, ro.idRol
	, o.NombreOpcion
	, o.URL
	, o.Orden
	, o.IdOpcionPadre
	, o.Activa
	, o.URLImagen
	, o.CreatedBy
	, o.CreatedDate
	, o.UpdatedBy
	, o.UpdatedDate
	, ro.crear
	, ro.leer
	, ro.actualizar
	, ro.eliminar
	from sm_Opcion o
	inner join sm_RolOpcion ro on o.IdOpcion = ro.idOpcion
	where ro.idRol = @rol
END
