using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System.Collections.Generic;
using System.Linq;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioEspecialidad : Repository<sm_Especialidad>
    {
        internal RepositorioEspecialidad(SaludMovilContext context)
            : base(context)
        {

        }

        public IList<sm_Especialidad> ListarEspecialidades()
        {
            return this.Query().Get().ToList();
        }
    }
}
