using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioGuia : Repository<sm_Guia>
    {
        internal RepositorioGuia(SaludMovilContext context)
            : base(context)
        {

        }
        public IList<sm_Guia> ListarGuias()
        {
            try
            {
                return this.Query().Get().ToList();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        public IList<sm_Guia> GuiasPorTipo(int idTipoGuia)
        {
            return this.Contexto.sm_Guia.Where(g => g.idTipoGuia == idTipoGuia).ToList<sm_Guia>();
        }

        public IList<sm_Guia> GuiasPorProgramaYTipo(int idPrograma, int idTipoGuia)
        {
            return this.Query().Include(r => r.sm_Riesgo).Include(e => e.sm_Estado).Get().Where(g => g.idTipoGuia == idTipoGuia).Where(p => p.idPrograma == idPrograma).ToList<sm_Guia>();
        }

        /// <summary>
        /// Retorna guia por tipo codigo, programa y tipo de guia
        /// </summary>
        /// <param name="idTipoCodigo"></param>
        /// <param name="idPrograma"></param>
        /// <param name="idTipoGuia"></param>
        /// <returns></returns>
        public sm_Guia GuiasPorProgramaTipoCodigoTipo(string idTipoCodigo, int idPrograma, int idTipoGuia, int idriesgo)
        {
            try
            {
                sm_Guia guia = null;
                guia = this.Query().Get().Where(g => g.idTipoGuia == idTipoGuia).Where(p => p.idPrograma == idPrograma).Where(p => p.idRiesgo == idriesgo).Where(g => g.idCodigoTipo.Equals(idTipoCodigo)).FirstOrDefault<sm_Guia>();
                return guia;
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }
    }
}
