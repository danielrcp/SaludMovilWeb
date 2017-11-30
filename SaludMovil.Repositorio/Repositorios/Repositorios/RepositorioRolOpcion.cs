using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaludMovil.Entidades;
using SaludMovil.Modelo;

namespace SaludMovil.Repositorio
{
    public class RepositorioRolOpcion : Repository<RolOpcion>
    {
        internal RepositorioRolOpcion(SaludMovilContext context)
            : base(context)
        {

        }

        #region Metodos principales
        public IList<RolOpcion> OpcionesRol(int idRol)
        {
            IList<RolOpcion> resultado = null;
            resultado = this.Contexto.Database.SqlQuery<RolOpcion>("spOpcionesRol {0}", new object[] {idRol}).ToList();
            return resultado;
        }

        /// <summary>
        /// Eliminar todas las opciones de un rol
        /// </summary>
        /// <param name="idRol"></param>
        public void EliminarOpcionesRol(int idRol)
        {
            this.Contexto.Database.ExecuteSqlCommand("DELETE FROM sm_RolOpcion WHERE idRol = {0}", idRol);
           // this.Contexto.Database.SqlQuery<string>("DELETE FROM sm_RolOpcion WHERE idRol = " + idRol);
            this.Contexto.SaveChanges();
        }

        /// <summary>
        /// Insertar la entidad sm_ROlOpcion
        /// </summary>
        /// <param name="opcion"></param>
        public void InsertarOpcion(sm_RolOpcion opcion)
        {
            this.Contexto.sm_RolOpcion.Add(opcion);
        }
        #endregion
    }
}

