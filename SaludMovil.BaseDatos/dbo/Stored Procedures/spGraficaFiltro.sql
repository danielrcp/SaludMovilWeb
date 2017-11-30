


-- ===================================================================================
-- Author:		Daniel Caballero
-- Create date: 2016/04/25
-- Description:	Consulta bandeja tomas de talla peso tension
-- ===================================================================================
CREATE PROCEDURE [dbo].[spGraficaFiltro]
	@idTipoIdentificacion int,
	@numeroIdentificacion nvarchar(50),
	@tipoEvento int,
	@mes nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SET LANGUAGE Español;
	   SELECT isnull(gp.valor1,0) as valor1,isnull(gp.valor2,0) as valor2,isnull(gp.valor3,0) as valor3,datepart(day,gp.fechaEvento) as Dia
		 FROM sm_GuiaPaciente gp
   INNER JOIN sm_Guia g ON g.idGuia = gp.idGuia
        WHERE gp.idTipoIdentificacion = @idTipoIdentificacion and gp.numeroIdentificacion = @numeroIdentificacion
          AND tipoEvento = @tipoEvento AND datename(month,gp.fechaEvento) = @mes
END


