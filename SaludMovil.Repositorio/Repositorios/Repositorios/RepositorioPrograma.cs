using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioPrograma:Repository<sm_Programa>
    {
        internal RepositorioPrograma(SaludMovilContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Consulta todos los programas
        /// </summary>
        /// <returns></returns>
        public IList<sm_Programa> ListarProgramas()
        {
            return this.Query().Include(p => p.sm_Poblacion).Include(e => e.sm_Estado).Get().Where(p => p.idEstado != 0).ToList();
        }

        /// <summary>
        /// Consulta tosoa loa programas de forma personalizada
        /// </summary>
        /// <returns></returns>
        public IList<Programa> ListarProgramasSP()
        {
            IList<Programa> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<Programa>("spRetornarProgramas", new object[] { }).ToList();
            return resultado;
        }

        /// <summary>
        /// Insertar los tipos de guias para el programa
        /// </summary>
        /// <param name="idPrograma"></param>
        public void InsertarTiposGuiasPrograma(int idPrograma)
        {
            this.Contexto.Database.ExecuteSqlCommand("spInsertarTiposGuiasXPrograma {0}", new object[] { idPrograma });
        }

        /// <summary>
        /// Listar los tipos guias por programa
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <returns></returns>
        public IList<sm_TipoGuiaXPrograma> ListarTiposGuiasXPrograma(int idPrograma)
        {
            IList<sm_TipoGuiaXPrograma> resultado = null;
            resultado = this.Contexto.sm_TipoGuiaXPrograma.ToList().Where(t => t.idPrograma == idPrograma).ToList();
            return resultado;
        }
    }
}
