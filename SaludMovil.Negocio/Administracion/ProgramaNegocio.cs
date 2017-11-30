#region Imports
using SaludMovil.Entidades;
using SaludMovil.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace SaludMovil.Negocio
{
    public class ProgramaNegocio
    {
        private UnidadTrabajo unitOfWork;

        public ProgramaNegocio()
        { 
        
        }

        #region Programa
        /// <summary>
        /// Guardar objeto programa
        /// </summary>
        /// <param name="programa"></param>
        /// <returns></returns>
        public bool GuardarPrograma(sm_Programa programa)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.ProgramaRepository.Insert(programa);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualizar un objeto programa
        /// </summary>
        /// <param name="programa"></param>
        /// <returns></returns>
        public bool ActualizarPrograma(sm_Programa programa)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.ProgramaRepository.Update(programa);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Consultar todos los programas
        /// </summary>
        /// <returns></returns>
        public IList<sm_Programa> listarProgramas()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ProgramaRepository.ListarProgramas();
            }
        }

        /// <summary>
        /// Consultar un programa especifico
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <returns></returns>
        public sm_Programa ConsultarPrograma(int idPrograma)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ProgramaRepository.FindById(idPrograma);
            }
        }

        /// <summary>
        /// Lista los parametros del sistema
        /// </summary>
        /// <returns></returns>
        public IList<sm_Parametro> ListarParametros()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ParametroRepository.ListarParametros();
            }
        }

        /// <summary>
        /// Consultar todos los programas (Se usa un sp)
        /// </summary>
        /// <returns></returns>
        public IList<Programa> listarProgramasSP()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ProgramaRepository.ListarProgramasSP();
            }
        }

        /// <summary>
        /// Insertar los tipos de guias para el programa
        /// </summary>
        /// <param name="idPrograma"></param>
        public bool InsertarTiposGuiasPrograma(int idPrograma)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.ProgramaRepository.InsertarTiposGuiasPrograma(idPrograma);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Listar los tipos guias por programa
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <returns></returns>
        public IList<sm_TipoGuiaXPrograma> ListarTiposGuiasXPrograma(int idPrograma)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.ProgramaRepository.ListarTiposGuiasXPrograma(idPrograma);
            }
        }
        #endregion

        #region Tipo Guia
        /// <summary>
        /// Guardar objeto tipo guia
        /// </summary>
        /// <param name="tipoGuia"></param>
        /// <returns></returns>
        public bool GuardarTipoGuia(sm_TipoGuia tipoGuia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.TipoGuiaRepository.Insert(tipoGuia);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualiza el objeto tipo guia
        /// </summary>
        /// <param name="TipoGuia"></param>
        /// <returns></returns>
        public bool ActualizarTipoGuia(sm_TipoGuia TipoGuia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.TipoGuiaRepository.Update(TipoGuia);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Consultar todos los tipos guias
        /// </summary>
        /// <returns></returns>
        public IList<sm_TipoGuia> listarTiposGuias()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TipoGuiaRepository.ListarTiposGuias();
            }
        }

        /// <summary>
        /// Consultar un tipo guia especifico
        /// </summary>
        /// <param name="idTipoGuia"></param>
        /// <returns></returns>
        public sm_TipoGuia ConsultarTipoGuia(int idTipoGuia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TipoGuiaRepository.FindById(idTipoGuia);
            }
        }

        /// <summary>
        /// Listar Tipos Guia con la tabla relacional de ponderadores 
        /// </summary>
        /// <returns></returns>
        public IList<TipoGuia> ListarTiposGuiasSP(int idPrograma)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TipoGuiaRepository.ListarTiposGuiasSP(idPrograma);
            }
        }

        /// <summary>
        /// Actualizar los ponderadores de un tipoguia x programa
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idTipoGuia"></param>
        /// <param name="esPonderado"></param>
        /// <param name="ponderador"></param>
        public void ActualizarTiposGuiasPrograma(int idPrograma, int idTipoGuia, int esPonderado, decimal ponderador)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.TipoGuiaRepository.ActualizarTiposGuiasPrograma(idPrograma, idTipoGuia, esPonderado, ponderador);
                unitOfWork.SaveChanges();
            }
        }
        #endregion

        #region Guia
        /// <summary>
        /// Guardar objeto guia
        /// </summary>
        /// <param name="Guia"></param>
        /// <returns></returns>
        public bool GuardarGuia(sm_Guia Guia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.GuiaRepository.Insert(Guia);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualiza el objeto guia
        /// </summary>
        /// <param name="Guia"></param>
        /// <returns></returns>
        public bool ActualizarGuia(sm_Guia Guia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.GuiaRepository.Update(Guia);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Consultar todas las guias
        /// </summary>
        /// <returns></returns>
        public IList<sm_Guia> listarGuias()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaRepository.ListarGuias();
            }
        }

        /// <summary>
        /// Consultar las guias asociadas a un tipo de guia especifico
        /// </summary>
        /// <param name="idTipoGuia"></param>
        /// <returns></returns>
        public IList<sm_Guia> listarGuiasPorTipo(int idTipoGuia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaRepository.GuiasPorTipo(idTipoGuia);
            }
        }

        /// <summary>
        /// Lista las guias que corresponden a un programa y tipo prdeterminado
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idTipoGuia"></param>
        /// <returns></returns>
        public IList<sm_Guia> listarGuiasPorProgramaYTipo(int idPrograma, int idTipoGuia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaRepository.GuiasPorProgramaYTipo(idPrograma, idTipoGuia);
            }
        }

        /// <summary>
        /// Consulta una guia por su id
        /// </summary>
        /// <param name="idGuia"></param>
        /// <returns></returns>
        public sm_Guia retornarGuia(int idGuia)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaRepository.FindById(idGuia);
            }
        }

        /// <summary>
        /// Consulta una guia por todas sus llaves
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idTipoGuia"></param>
        /// <param name="idTipoCodigo"></param>
        /// <param name="idRiesgo"></param>
        /// <returns></returns>
        public sm_Guia retornarGuiaPorCodigo(int idPrograma, int idTipoGuia,string idTipoCodigo,int idRiesgo)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.GuiaRepository.GuiasPorProgramaTipoCodigoTipo(idTipoCodigo, idPrograma, idTipoGuia,idRiesgo);
            }
        }

        #endregion

        #region Servicios WEB
        //TODO: Revisar como convertir este datatable en ilist con dto
        //public IList<ServiciosDiagnosticos> ListarServiciosDiagnosticos(string ocurrencia, string tipoGuia)
        //{
        //    IList<ServiciosDiagnosticos> diagnosticos = (IList<ServiciosDiagnosticos>)serviciosWeb.diagnosticos(ocurrencia, tipoGuia);
        //    return diagnosticos;
        //}
        #endregion

    }
}