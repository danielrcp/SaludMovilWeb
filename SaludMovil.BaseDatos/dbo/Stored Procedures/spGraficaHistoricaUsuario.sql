


-- ===================================================================================
-- Author:		Andres Felipe Silva Pascuas
-- Create date: 10 Mayo 2016
-- Description:	Consulta 
-- ===================================================================================
 CREATE PROCEDURE [dbo].[spGraficaHistoricaUsuario]
	@idTipoIdentificacion int,
	@numeroIdentificacion nvarchar(50),
	@tipoEvento int
AS
BEGIN
	SET NOCOUNT ON;
	   SELECT isnull(gp.valor1,0) as valor1
	   ,isnull(gp.valor2,0) as valor2
	   ,isnull(gp.valor3,0) as valor3
	   ,isnull(gp.valor4,0) as valor4
	   ,isnull(gp.valor5,0) as valor5
	   ,Convert(nvarchar,gp.fechaEvento,103) as fechaCadena	   
	FROM sm_GuiaPaciente gp
    INNER JOIN sm_Guia g ON g.idGuia = gp.idGuia
    WHERE gp.idTipoIdentificacion = @idTipoIdentificacion and gp.numeroIdentificacion = @numeroIdentificacion
    AND tipoEvento = @tipoEvento
	 ORDER BY fechaCadena
	
END


