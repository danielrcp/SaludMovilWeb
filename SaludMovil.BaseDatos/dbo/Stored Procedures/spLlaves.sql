-- ===================================================================================================================
-- Author:		Andres Felipe Silva Pascuas
-- Create date: 6 Abril 2016
-- Description:	Recibiendo por parametro el nombre del objeto se retorna la informacion de las llaves correspondientes
-- ===================================================================================================================
CREATE PROCEDURE [dbo].[spLlaves]
	@nombreObjeto nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT tablaAnfitrion.[object_id] as idTablaTablaAnfitrion
	, tablaDestino.[object_id] as idTablaDestino
	, columnaorigen.[object_id] as idColumnaOrigen
	, columnaDestino.[object_id] as idColumnaDestino
	, restriccion.[name] as restriccion
	, tablaAnfitrion.[name] as tablaOrigen
	, columnaOrigen.[name] as columnaOrigen
	, esquemaDestino.[name] as esquemaDestino
	, tablaDestino.[name] as tablaDestino
	, columnaDestino.[name] as columnaDestino
	, (select Col_name(object_id(tablaDestino.[name]),2)) as nombreMostrar
	from sys.foreign_key_columns fkcols 
	join sys.objects restriccion on (fkcols.constraint_object_id = restriccion.[object_id]) 
	join sys.objects tablaAnfitrion on (fkcols.parent_object_id = tablaAnfitrion.[object_id] and tablaAnfitrion.[object_id] = object_id(@nombreObjeto)) 
	join sys.objects tablaDestino on (tablaDestino.[object_id] = fkcols.referenced_object_id) 
	join sys.schemas esquemaDestino on (tablaDestino.schema_id = esquemaDestino.schema_id) 
	join sys.columns columnaDestino on (fkcols.referenced_column_id = columnaDestino.[column_id] and tablaDestino.[object_id] = columnadestino.[object_id]) 
	join sys.columns columnaOrigen on (fkcols.parent_column_id = columnaOrigen.[column_id] and tablaAnfitrion.[object_id] = columnaOrigen.[object_id]) 
    
END
