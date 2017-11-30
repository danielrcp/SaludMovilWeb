using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioParentesco : Repository<sm_Parentesco>
    {
        internal RepositorioParentesco(SaludMovilContext context)
            : base(context)
        {

        }
        public IList<sm_Parentesco> ListarParentescos()
        {
            return this.Query().Get().ToList();
        }
    }
}
