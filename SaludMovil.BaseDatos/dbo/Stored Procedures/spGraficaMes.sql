


-- ===================================================================================
-- Author:		Daniel Caballero
-- Create date: 2016/04/25
-- Description:	Consulta bandeja tomas de talla peso tension
-- ===================================================================================
CREATE PROCEDURE [dbo].[spGraficaMes]
	@idTipoIdentificacion int,
	@numeroIdentificacion nvarchar(50),
	@tipoEvento int
AS
BEGIN
	SET NOCOUNT ON;
	SET LANGUAGE Español;
	   SELECT isnull(AVG(gp.valor1),0) as valor1,isnull(avg(gp.valor2),0) as valor2,isnull(avg(gp.valor3),0) as valor3, datename(month,fechaEvento) as Mes,
			  DATEPART(month,gp.fechaEvento) as NumeroMes
		 FROM sm_GuiaPaciente gp
   INNER JOIN sm_Guia g ON g.idGuia = gp.idGuia
        WHERE gp.idTipoIdentificacion = @idTipoIdentificacion and gp.numeroIdentificacion = @numeroIdentificacion
          AND tipoEvento = @tipoEvento
     GROUP BY datename(month,gp.fechaEvento), DATEPART(month,gp.fechaEvento)
	 ORDER BY NumeroMes
	
END


