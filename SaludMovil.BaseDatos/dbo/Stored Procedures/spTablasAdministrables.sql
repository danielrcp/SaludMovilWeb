-- ===================================================================================
-- Author:		Andres Felipe Silva Pascuas
-- Create date: 30 Marzo 2016
-- Description:	Recibiendo por parametro los roles y el prefijo de las tablas de la bd
--				se consultan las tablas administrables
-- ===================================================================================
CREATE PROCEDURE [dbo].[spTablasAdministrables]
	@roles nvarchar(20),
	@prefijo nvarchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	IF @roles IS NULL
		select distinct replace(substring(nombreObjeto, charindex('.', nombreObjeto) + 1, len(nombreObjeto)), @prefijo, '') as nombreObjeto
		, nombreTablaMaestra
		, o.idObjeto 
		from [Objeto] o 
		inner join RoleObjeto ro on o.idObjeto = ro.idObjeto
		where(esAdministrable = 1) And ro.idRole in (@roles)
		order by 1
	ELSE
		select replace(substring(nombreObjeto, charindex('.', nombreObjeto) + 1, len(nombreObjeto)), @prefijo, '') as nombreObjeto
		, nombreTablaMaestra 
		, idObjeto
		from [Objeto] 
		where esAdministrable = 1 
		order by nombreObjeto asc
END


