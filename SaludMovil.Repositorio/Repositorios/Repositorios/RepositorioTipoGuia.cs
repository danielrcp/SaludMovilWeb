using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioTipoGuia : Repository<sm_TipoGuia>
    {
        internal RepositorioTipoGuia(SaludMovilContext context)
            : base(context)
        {
            
        }
        public IList<sm_TipoGuia> ListarTiposGuias()
        {
            return this.Query().Include(p => p.sm_Estado).Get().ToList();
        }

        /// <summary>
        /// Listar Tipos Guia con la tabla relacional de ponderadores 
        /// </summary>
        /// <returns></returns>
        public IList<TipoGuia> ListarTiposGuiasSP(int idPrograma)
        {
            return this.Contexto.Database.SqlQuery<TipoGuia>("spRetornarTiposGuia {0}", new object[] { idPrograma }).ToList();
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
            this.Contexto.Database.ExecuteSqlCommand("spActualizarTiposGuiasXPrograma {0},{1},{2},{3}", new object[] { idPrograma, idTipoGuia, esPonderado, ponderador });
        }

    }
}
