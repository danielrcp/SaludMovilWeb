using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public class RepositorioOpcion : Repository<sm_Opcion>
    {
        internal RepositorioOpcion(SaludMovilContext context)
            : base(context)
        {

        }

        #region Metodos principales
        /// <summary>
        /// Lista todas las opciones del sistema
        /// </summary>
        /// <returns></returns>
        public IList<sm_Opcion> ListarOpciones()
        {
            try
            {
                return this.Contexto.sm_Opcion.ToList();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }
        #endregion
    }
}

