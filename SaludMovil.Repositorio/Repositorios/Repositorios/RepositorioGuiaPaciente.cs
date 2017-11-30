using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioGuiaPaciente : Repository<sm_GuiaPaciente>
    {
        internal RepositorioGuiaPaciente(SaludMovilContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Retorna lista de límites de mediciones biométricas
        /// </summary>
        /// <param name="idTipoIdentificación"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="idGuiaMediciones"></param>
        /// <returns></returns>
        public List<GuiaPaciente> consultarMediciones(int idTipoIdentificacion, string numeroIdentificacion, int idGuiaMediciones,string tipo)
        {
            try
            {
                List<GuiaPaciente> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<GuiaPaciente>("spRetornarMedicionesBiometricas {0},{1},{2},{3}", new object[] { idTipoIdentificacion, numeroIdentificacion, idGuiaMediciones,tipo }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        ///  Retorna lista medicamentos paciente
        /// </summary>
        /// <param name="idTipoIdentificación"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="idTipoMedicamento"></param>
        /// <returns></returns>
        public List<GuiaPaciente> consultarMedicamentos(int idTipoIdentificacion, string numeroIdentificacion, int idTipoMedicamento)
        {
            try
            {
                List<GuiaPaciente> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<GuiaPaciente>("spRetornarMedicamentosPaciente {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, idTipoMedicamento }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Retorna lista de registros controles medicos por paciente
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="idTipoControlMedico"></param>
        /// <returns></returns>
        public List<GuiaPaciente> consultarControlesMedicos(int idTipoIdentificacion, string numeroIdentificacion, int idTipoControlMedico)
        {
            try
            {
                List<GuiaPaciente> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<GuiaPaciente>("spRetornarControlesPaciente {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, idTipoControlMedico }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Retorna lista de registros aspectos monitoreados
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="tipoAspecto"></param>
        /// <returns></returns>
        public List<sm_GuiaPaciente> ConsultarAspectosMonitoreados(int idTipoIdentificacion, string numeroIdentificacion, int tipoAspecto)
        {
            try
            {
                return this.Contexto.sm_GuiaPaciente.AsParallel().Where(g => g.idTipoIdentificacion == idTipoIdentificacion)
                    .Where(g => g.numeroIdentificacion == numeroIdentificacion).Where(g => g.tipoEvento == tipoAspecto).ToList<sm_GuiaPaciente>();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Retorna calendario de citas ingresados a partir de web service 
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="tipoCitas"></param>
        /// <returns></returns>
        public List<Calendario> ConsultarCalendario(int idTipoIdentificacion, string numeroIdentificacion, int tipoCitas)
        {
            try
            {
                List<Calendario> resultado = null;
                resultado = this.Contexto.Database.SqlQuery<Calendario>("spCalendario {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoCitas }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }
    }
}
