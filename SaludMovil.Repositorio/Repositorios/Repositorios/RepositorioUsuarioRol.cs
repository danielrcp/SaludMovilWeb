using System.Collections.Generic;
using System.Linq;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public class RepositorioUsuarioRol : Repository<sm_UsuarioRol>
    {
        internal RepositorioUsuarioRol(SaludMovilContext context)
            : base(context)
        {

        }

        public void BorrarUsuarioRol(int idUsuario, int idRol)
        {
            this.Contexto.Database.ExecuteSqlCommand("SP_BorrarUsuarioRol {0},{1}", new object[] { idUsuario, idRol });
        }
        
    }
}

