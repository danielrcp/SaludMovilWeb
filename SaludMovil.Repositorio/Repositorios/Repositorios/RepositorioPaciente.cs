using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioPaciente : Repository<sm_Paciente>
    {
        internal RepositorioPaciente(SaludMovilContext context)
            : base(context)
        {

        }

        public IList<BandejaDiagnostico> ConsultarBandejaDiagnostico(int idTipoIdentificacion,string numeroIdentificacion, int tipoDiagnostico,int tipoOtroDiagnostico)
        {
            IList<BandejaDiagnostico> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<BandejaDiagnostico>("spBandejaDiagnostico {0},{1},{2},{3}", new object[] { idTipoIdentificacion, numeroIdentificacion,tipoDiagnostico,tipoOtroDiagnostico }).ToList();
            return resultado;
        }

        public IList<Interconsultas> ConsultarDetalleBandejaActividades(int idTipoIdentificacion, string numeroIdentificacion, int tipoInterconsultas, int tipoOtrasInterconsultas,string codigo)
        {
            IList<Interconsultas> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<Interconsultas>("spRetornarInterconsultas {0},{1},{2},{3},{4}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoInterconsultas, tipoOtrasInterconsultas,codigo }).ToList();
            return resultado;
        }

        public IList<InterconsultasAgrupadas> ConsultarBandejaInterconsultasAgrupadas(int idTipoIdentificacion, string numeroIdentificacion, int tipoInterconsultas, int tipoOtrasInterconsultas)
        {
            IList<InterconsultasAgrupadas> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<InterconsultasAgrupadas>("spRetornarInterconsultasAgrupadas {0},{1},{2},{3}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoInterconsultas, tipoOtrasInterconsultas }).ToList();
            return resultado;
        }

        public IList<GraficaMes> ConsultarGraficaMes(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            IList<GraficaMes> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<GraficaMes>("spGraficaMes {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento }).ToList();
            return resultado;
        }

        public IList<FechasTomas> ConsultarFechasTomas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            IList<FechasTomas> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<FechasTomas>("spRetornarFechasTomas {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento }).ToList();
            return resultado;
        }

        public IList<HorasTomas> ConsultarHorasTomas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento,string fecha)
        {
            IList<HorasTomas> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<HorasTomas>("spRetornarHorasTomas {0},{1},{2},{3}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento,fecha }).ToList();
            return resultado;
        }

        public IList<GraficaTension> ConsultarGraficaTension(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            IList<GraficaTension> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<GraficaTension>("spGraficaTension {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento }).ToList();
            return resultado;
        }

        public IList<GraficaFiltrada> ConsultaGraficaFiltro(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento,string mes)
        {
            IList<GraficaFiltrada> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<GraficaFiltrada>("spGraficaFiltroFecha {0},{1},{2},{3}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento, mes }).ToList();
            return resultado;
        }

        public IList<GraficaFiltrada> ConsultaGraficaFiltroHora(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento, DateTime fechaInicio, DateTime fechaFin, string rango)
        {
            try
            {
                IList<GraficaFiltrada> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<GraficaFiltrada>("spGraficaFiltroHora {0},{1},{2},{3},{4},{5}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento, fechaInicio, fechaFin, rango }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
               throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        public IList<BandejaTomas> ConsultarBandejaTomas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            IList<BandejaTomas> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<BandejaTomas>("spBandejaTomas {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento }).ToList();
            return resultado;
        }

        public IList<BandejaPaciente> ConsultaBandejaPacientes()
        {
            try
            {
                IList<BandejaPaciente> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<BandejaPaciente>("spBandejaPrincipalPacientes").ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        public IList<Riesgos> ConsultaRiesgos()
        {
            IList<Riesgos> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<Riesgos>("spRetornarRiesgos").ToList();
            return resultado;
        }

        /// <summary>
        /// Retorna entidad paciente
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        public sm_Paciente ConsultarPaciente(int idTipoIdentificacion, string numeroIdentificacion)
        {
            return this.Contexto.sm_Paciente.Find(idTipoIdentificacion, numeroIdentificacion);
        }

        /// <summary>
        /// Retorna lista guias por programa y tipo
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idTipoGuia"></param>
        /// <param name="riesgo"></param>
        /// <returns></returns>
        public IList<DiagnosticoProgramaTipo> ConsultarGuiasPorProgramaTipo(int idPrograma, int idTipoGuia,int riesgo)
        {
            IList<DiagnosticoProgramaTipo> resultado = null;
            if (riesgo != 0)
            {
                resultado = this.Contexto.Database.SqlQuery<DiagnosticoProgramaTipo>("spRetornarGuiasPorProgramaTipoRiesgo {0},{1},{2}", new object[] { idPrograma, idTipoGuia,riesgo }).ToList();
                return resultado;
            }
            else
            {
                resultado = this.Contexto.Database.SqlQuery<DiagnosticoProgramaTipo>("spRetornarGuiasPorProgramaTipo {0},{1}", new object[] { idPrograma, idTipoGuia }).ToList();
                return resultado;
            }
        }

        /// <summary>
        /// Borra interconsultas por codigo
        /// </summary>
        /// <param name="codigoGuiaPaciente"></param>
        /// <param name="tipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        public int borrarInterconsultasCodigo(string codigoGuiaPaciente, string tipoIdentificacion, string numeroIdentificacion)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@idTipoIdentificacion", tipoIdentificacion);
                SqlParameter param2 = new SqlParameter("@numeroIdentificacion", numeroIdentificacion);
                SqlParameter param3 = new SqlParameter("@codigo", codigoGuiaPaciente);
                int resultado = 0;
                resultado = this.Contexto.Database.ExecuteSqlCommand("spBorrarInterconsultasPorCodigo @idTipoIdentificacion,@numeroIdentificacion,@codigo", param1, param2, param3);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Borra registro guia paciente por procedimiento almacenado
        /// </summary>
        /// <param name="codigoGuiaPaciente"></param>
        /// <param name="tipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        public int EliminarGuiaPaciente(int idGuiaPaciente)
        {
            try
            {
                int resultado = 0;
                SqlParameter param1 = new SqlParameter("@idGuiaPaciente", idGuiaPaciente);
                resultado = this.Contexto.Database.ExecuteSqlCommand("spBorrarGuiaPaciente @idGuiaPaciente", param1);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Filtro grafica por fechas
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="tipoEvento"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public IList<GraficaFiltrada> ConsultaGraficaFiltroFechas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                IList<GraficaFiltrada> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<GraficaFiltrada>("spGraficaFiltroFecha {0},{1},{2},{3},{4}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento, fechaInicio, fechaFin }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
              throw new Transversales.SaludMovilExceptionBD(ex);  
            }
        }

        /// <summary>
        /// Retorna lista de guias por paciente filtradas por codigo tipo
        /// </summary>
        /// <param name="idCodigoTipo"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public List<sm_GuiaPaciente> ConsultarGuiasPacientePorCodigo(string idCodigoTipo, string idTipoIdentificacion, string numeroIdentificacion)
        {
            try
            {
                List<sm_GuiaPaciente> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<sm_GuiaPaciente>("spConsultaGuiasPorCodigo {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, idCodigoTipo }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Transversales.SaludMovilExceptionBD(ex);  
            }
        }

        /// <summary>
        /// Elimina registros de guia paciente por codigo
        /// </summary>
        /// <param name="codigoGuiaPaciente"></param>
        /// <param name="tipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        public int EliminarGuiasPacientePorCodigo(string codigoGuiaPaciente, string tipoIdentificacion, string numeroIdentificacion)
        {
            try
            {
                SqlParameter param1 = new SqlParameter("@idTipoIdentificacion", tipoIdentificacion);
                SqlParameter param2 = new SqlParameter("@numeroIdentificacion", numeroIdentificacion);
                SqlParameter param3 = new SqlParameter("@codigo", codigoGuiaPaciente);
                int resultado = 0;
                resultado = this.Contexto.Database.ExecuteSqlCommand("spBorrarGuiasPacientePorCodigo @idTipoIdentificacion,@numeroIdentificacion,@codigo", param1, param2, param3);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Retorna lista de pacientes, filtro por tipo y numero de identificación
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        public IList<BandejaPaciente> ConsultaBandejaPacientesFiltro(string idTipoIdentificacion, string numeroIdentificacion)
        {
            IList<BandejaPaciente> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<BandejaPaciente>("spBandejaPrincipalPacientes {0},{1}", new object[] { idTipoIdentificacion, numeroIdentificacion }).ToList();
            return resultado;
        }


        /// <summary>
        /// Retorna lista de pacientes en un rango de fechas
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public IList<BandejaPaciente> ConsultaBandejaPacientesFiltroFechas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            IList<BandejaPaciente> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<BandejaPaciente>("spBandejaReporteSeguimiento {0},{1}", new object[] { fechaInicio, fechaFin }).ToList();
            return resultado;
        }
    }
}