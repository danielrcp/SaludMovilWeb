using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioEstado : Repository<sm_Estado>
    {
        internal RepositorioEstado(SaludMovilContext context)
            : base(context)
        {
            
        }
        public IList<sm_Estado> ListarEstados()
        {
            return this.Query().Get().ToList();
        }

        public IList<sm_Estado> ConsultarEstados(string tabla)
        {
            return this.Contexto.sm_Estado.Where(g => g.tabla == tabla).ToList<sm_Estado>();
        }
    }
}
