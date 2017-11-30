using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioPoblacion : Repository<sm_Poblacion>
    {
        internal RepositorioPoblacion(SaludMovilContext context)
            : base(context)
        {
            
        }
        public IList<sm_Poblacion> ListarPoblaciones()
        {
            return this.Query().Get().ToList();
        }
    }
}
