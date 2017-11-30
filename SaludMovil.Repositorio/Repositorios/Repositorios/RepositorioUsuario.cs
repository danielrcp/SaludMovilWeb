using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaludMovil.Repositorio
{
    public sealed class RepositorioUsuario : Repository<sm_Usuario>
    {
        internal RepositorioUsuario(SaludMovilContext context)
            : base(context)
        {

        }

        public IList<sm_Usuario> ListarCiudades()
        {
            return this.Query().Get().ToList();
        }

        public IList<MedidasPaciente> obtenerDatosLecturas(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento)
        {
            return this.Contexto.Database.SqlQuery<MedidasPaciente>("spGraficaHistoricaUsuario {0},{1},{2}", new object[] { idTipoIdentificacion, numeroIdentificacion, tipoEvento}).ToList();
        }

        public void ActualizarUsuarioSinContrasena(sm_Usuario usuario)
        {
            int idUsuario = usuario.idUsuario;
            int idTipoID = (int)usuario.idTipoIdentificacion;
            string numID = usuario.numeroIdentificacion;
            string usu = usuario.usuario;
            bool estado = usuario.estado;
            int? intentos = 0;
            int? idEmpresa = usuario.idEmpresa;
            string updatedBy = usuario.updatedBy;
            this.Contexto.Database.ExecuteSqlCommand("SP_ActualizarUsuario {0},{1},{2},{3},{4},{5},{6},{7}", new object[] 
            { idUsuario, idTipoID, numID, usu, estado, intentos, idEmpresa, updatedBy });
        }
    }
}