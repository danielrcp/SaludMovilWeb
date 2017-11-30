

-- ===================================================================================
-- Author:		Daniel Caballero
-- Create date: 2016/04/25
-- Description:	Consulta bandeja diagnostico
-- ===================================================================================
CREATE PROCEDURE [dbo].[spBandejaDiagnostico]
    @idTipoIdentificacion int,
	@numeroIdentificacion nvarchar(50),
	@tipoDiagnostico int,
	@tipoOtroDiagnostico int
	
AS
BEGIN
	SET NOCOUNT ON;

	 SELECT gp.idGuiaPaciente as id,g.idCodigoTipo as Codigo,g.descripcion as Descripcion,CONVERT(VARCHAR(10),
              gp.createdDate,110) as FechaRegistro,isnull(gp.valor1,0) as valor1,isnull(gp.valor2,0) as valor2,isnull(gp.valor3,0) as valor3,gp.fechaEvento
		 FROM sm_GuiaPaciente gp
   INNER JOIN sm_Guia g ON g.idGuia = gp.idGuia
        WHERE gp.idTipoIdentificacion = @idTipoIdentificacion and gp.numeroIdentificacion = @numeroIdentificacion
          AND tipoEvento = @tipoDiagnostico
	UNION ALL
	SELECT gp.idGuiaPaciente as id,g.idCodigoTipo as Codigo,g.descripcion as Descripcion,CONVERT(VARCHAR(10),
              gp.createdDate,110) as FechaRegistro,isnull(gp.valor1,0) as valor1,isnull(gp.valor2,0) as valor2,isnull(gp.valor3,0) as valor3,gp.fechaEvento
		 FROM sm_GuiaPaciente gp
   INNER JOIN sm_Guia g ON g.idGuia = gp.idGuia
        WHERE gp.idTipoIdentificacion = @idTipoIdentificacion and gp.numeroIdentificacion = @numeroIdentificacion
          AND tipoEvento = @tipoOtroDiagnostico
       
END

