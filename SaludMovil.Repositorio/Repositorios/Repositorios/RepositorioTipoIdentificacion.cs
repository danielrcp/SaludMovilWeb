using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioTipoIdentificacion : Repository<sm_TipoIdentificacion>
    {
        internal RepositorioTipoIdentificacion(SaludMovilContext context)
            : base(context)
        {

        }
        public IList<sm_TipoIdentificacion> ListarTiposIdentificacion()
        {
            return this.Query().Get().ToList();
        }
    }
}
