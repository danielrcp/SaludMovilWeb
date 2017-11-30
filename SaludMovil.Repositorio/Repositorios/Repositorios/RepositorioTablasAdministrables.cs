using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public class RepositorioTablasAdministrables : Repository<TablaAdministrable>
    {
        internal RepositorioTablasAdministrables(SaludMovilContext context)
            : base(context)
        {

        }

        #region Tablas administrables
        public IList<TablaAdministrable> ListarTablas(string roles, string prefijos)
        {
            IList<TablaAdministrable> resultado = null;
            /*
               1. Referencia al Context
             * 2. Usar una estrucutra DataReader
             * 3. Mapear el ResultSet de la BD a una Entidad fuertemente tipada - Reflection
             */

            resultado = this.Contexto.Database.SqlQuery<TablaAdministrable>("spTablasAdministrables {0}, {1}",
                new object[] { roles, prefijos }).ToList();


            /*Con mapeo desde el Entity Framework*/
            //resultado = this.Contexto.spTablasAdministrables(roles,prefijos);

            return resultado;
        }        

        public IList<EspecificacionObjeto> ConsultarEspecificacion(string nombreTabla)
        {
            IList<EspecificacionObjeto> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<EspecificacionObjeto>("spEspecificacionObjeto {0}", new object[] { nombreTabla }).ToList();
            return resultado;
        }
        #endregion

        #region Formularios dinamicos
        public IList<Llave> ConsultarLlaves(string nombreTabla)
        {
            IList<Llave> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<Llave>("spLlaves {0}", new object[] { nombreTabla }).ToList();
            return resultado;
        }

        public IList<Llave> ConsultarLlavesHomologadas(string nombreTabla)
        {
            IList<Llave> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<Llave>("SP_LlavesHomologadas {0}", new object[] { nombreTabla }).ToList();
            return resultado;
        }

        //public IList<spConsultaSistema_Result> ConsultarSPSistema(string querySQL)
        //{
        //    IList<spConsultaSistema_Result> resultado = null;
        //    resultado = this.Contexto.Database.SqlQuery<spConsultaSistema_Result>("spConsultaSistema {0}", new object[] { querySQL }).ToList();
        //    return resultado;
        //}
        #endregion                
    }
}
