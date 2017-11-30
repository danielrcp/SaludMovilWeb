
/****** Object:  Stored Procedure dbo.getObjectDefinition    Script Date: 10/07/2006 04:37:54 p.m. ******/


CREATE  procedure [dbo].[SQL2005getObjectDefinitionByName] 
	@objectName varchar(255)
    , @languageId varchar(30) = null
    , @regionId varchar(30) = null
	, @isIdentity bit = null
	, @isObjectKey bit = null
	, @required bit = null
	, @active bit = null
	, @isOptional bit = null
as

if (@languageId is null) set @languageId = dbo.getDefaultConfiguration('LENGUAJE')
if (@regionId is null) set @regionId = dbo.getDefaultConfiguration('REGION')

-- Obtener los datos principales del objeto.
select 0 as ObjectId
    , TABLE_SCHEMA + '.' + TABLE_NAME as ObjectName
    , 0 as ObjectTypeId
    , '' as ObjectTypeName
    , TABLE_SCHEMA + '.' + TABLE_NAME as MainTableName
    , null as ObjectLabel
from INFORMATION_SCHEMA.TABLES
where (TABLE_TYPE = 'BASE TABLE' or TABLE_TYPE = 'VIEW')
	and TABLE_SCHEMA + '.' + TABLE_NAME = @objectName

-- Obtener los datos principales de los atributos del objeto
select 0 as ObjectId 
	, c.column_id as AttributeId
	, c.[name] as [Name]
	, 0 as DataTypeId
	, t.[name] as DataTypeName
	, '' as  DataTypeDefaultValue
	, c.is_identity as IsIdentity
	, case when ind.column_id is null then 0 else 1 end as IsObjectKey
	, case when (c.is_nullable = 1) then 0 else 1 end as Required
	, 0 as Active
	, case when ind.column_id is null then 0 else 1 end as [Unique]
	, c.max_length as Length
	, '' as DefaultValue
	, 0 as IsOptional
	, null as AttributeLabel
FROM sys.columns AS c 
    JOIN sys.types AS t ON c.user_type_id=t.user_type_id
    LEFT JOIN (
        select ic.object_id, ic.column_id
        from sys.indexes AS i
        INNER JOIN sys.index_columns AS ic
            ON i.object_id = ic.object_id AND i.index_id = ic.index_id
        where i.is_primary_key = 1) as ind
        ON ind.object_id = c.object_id AND c.column_id = ind.column_id
WHERE c.object_id = OBJECT_ID(@objectName)
	and ((@isIdentity is null) or (c.is_identity = @isIdentity))
	and ((@isObjectKey is null) or ((case when ind.column_id is null then 0 else 1 end) = @isObjectKey))
	and ((@required is null) or ((case when (c.is_nullable = 1) then 0 else 1 end) = @required))

select 0
    , 0
    , 0
    , Relation.CardinalityId
    , Cardinality.CardinalityName
	, Relation.RelationTableName
from Relation 
    inner join Cardinality ON Relation.CardinalityId = Cardinality.CardinalityId
where (Relation.ObjectId = 0)
	or ((Relation.RelatedObjectId  = 0) and (Relation.CardinalityId = 0))

