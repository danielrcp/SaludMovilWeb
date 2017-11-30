using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioMedioAtencion:Repository<sm_MedioAtencion>
    {
        internal RepositorioMedioAtencion(SaludMovilContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Retorna listado de medios de atención
        /// </summary>
        /// <returns></returns>
        public IList<sm_MedioAtencion> ListarMediosAtencion()
        {
            return this.Query().Get().ToList();
        }
    }
}
