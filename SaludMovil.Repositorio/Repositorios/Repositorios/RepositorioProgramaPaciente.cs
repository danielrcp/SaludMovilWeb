using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System.Collections.Generic;
using System.Linq;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioProgramaPaciente : Repository<sm_PacientePrograma>
    {
        internal RepositorioProgramaPaciente(SaludMovilContext context)
            : base(context)
        {

        }
        public List<sm_PacientePrograma> ListarProgramaPaciente(int idTipoIdentificacion, string numeroIdentificacion)
        {
            List<sm_PacientePrograma> lista = Contexto.sm_PacientePrograma.AsParallel().Where(pp => pp.idTipoIdentificacion == idTipoIdentificacion && pp.numeroIdentificacion == numeroIdentificacion).ToList<sm_PacientePrograma>();
            return lista;
        }
    }
}
