-- =============================================================================
-- Author:		Andres Felipe Silva Pascuas
-- Create date: 4 Abril 2016
-- Description:	Recibiendo el objeto como parametro se retorna su especificacion
-- =============================================================================
CREATE PROCEDURE [dbo].[spEspecificacionObjeto]
	@nombreObjeto nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT c.name AS column_name 
	, c.column_id 
	, SCHEMA_NAME(t.schema_id) AS type_schema 
	, t.name AS type_name 
	, t.is_user_defined 
	, t.is_assembly_type 
	, c.max_length 
	, c.precision 
	, c.scale 
	, c.is_identity 
	, c.is_nullable 
	, case when ind.column_id is null then 0 else 1 end as is_primary_key 
	FROM sys.columns AS c  
	JOIN sys.types AS t ON c.user_type_id=t.user_type_id 
	LEFT JOIN (select ic.object_id, ic.column_id from sys.indexes AS i 
	INNER JOIN sys.index_columns AS ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id where i.is_primary_key = 1) as ind 
	ON ind.object_id = c.object_id AND c.column_id = ind.column_id WHERE c.object_id = OBJECT_ID(@nombreObjeto) ORDER BY c.column_id
END
