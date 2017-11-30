

-- ===================================================================================
-- Author:		Daniel Caballero
-- Create date: 2016/04/25
-- Description:	Consulta bandeja tomas de talla peso tension
-- ===================================================================================
CREATE PROCEDURE [dbo].[spBandejaTomas]
	@idTipoIdentificacion int,
	@numeroIdentificacion nvarchar(50),
	@tipoEvento int
AS
BEGIN
	SET NOCOUNT ON;
	SET LANGUAGE Español;
	SELECT gp.idGuiaPaciente as id,g.idCodigoTipo as Codigo,g.descripcion as Descripcion,CONVERT(VARCHAR(10),
              gp.createdDate,110) as FechaRegistro,isnull(gp.valor1,0) as valor1,isnull(gp.valor2,0) as valor2,isnull(gp.valor3,0) as valor3,gp.fechaEvento,
			  datename(month,fechaEvento) as Mes
		 FROM sm_GuiaPaciente gp
   INNER JOIN sm_Guia g ON g.idGuia = gp.idGuia
        WHERE gp.idTipoIdentificacion = @idTipoIdentificacion and gp.numeroIdentificacion = @numeroIdentificacion
          AND tipoEvento = @tipoEvento
END

