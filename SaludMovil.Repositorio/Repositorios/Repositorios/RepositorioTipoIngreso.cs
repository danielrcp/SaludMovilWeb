using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioTipoIngreso:Repository<sm_TipoIngreso>
    {
        internal RepositorioTipoIngreso(SaludMovilContext context)
            : base(context)
        {

        }
        public IList<sm_TipoIngreso> ListarTiposIngreso()
        {
            return this.Query().Get().ToList();
        }
    }
}
