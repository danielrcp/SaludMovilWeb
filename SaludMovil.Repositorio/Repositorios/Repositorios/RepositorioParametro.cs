using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioParametro:Repository<sm_Parametro>
    {
        internal RepositorioParametro(SaludMovilContext context)
            : base(context)
        {

        }
        /// <summary>
        /// Lista los parametros del sistema
        /// </summary>
        /// <returns></returns>
        public IList<sm_Parametro> ListarParametros()
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
    }
}
