using System.Linq;
using SaludMovil.Entidades;
using SaludMovil.Modelo;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data;
using System.Collections.Generic;
using System;

namespace SaludMovil.Repositorio
{
    public class RepositorioPersona : Repository<sm_Persona>
    {
        internal RepositorioPersona(SaludMovilContext context)
            : base(context)
        {

        }

        #region Metodos principales
        public Persona Autenticar(string usuario, string contrasena, string formaAuth)
        {
            Persona resultado = null;
            //resultado = this.Contexto.Database.SqlQuery<Persona>("spAutenticacion {0} , {1}", new object[] { usuario, contrasena }).FirstOrDefault();
            // Create command from the context in order to execute
            // the `GetReferrer` proc
            var command = this.Contexto.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[spAutenticacion]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("Usuario", usuario));
            command.Parameters.Add(new SqlParameter("Contrasena", contrasena));
            command.Parameters.Add(new SqlParameter("FormAut", formaAuth));
            try
            {
                this.Contexto.Database.Connection.Open();
                var reader = command.ExecuteReader();
                // Drop down to the wrapped `ObjectContext` to get access to
                // the `Translate` method
                ObjectContext objectContext = (this.Contexto as IObjectContextAdapter).ObjectContext;
                // Read Entity1 from the first resultset
                resultado = objectContext.Translate<Persona>(reader).FirstOrDefault();
                // Read Entity2 from the second resultset
                if (resultado != null)
                {
                    reader.NextResult();
                    resultado.Roles = objectContext.Translate<sm_Rol>(reader).ToList();
                    // Read Entity2 from the third resultset
                    reader.NextResult();
                    resultado.Opciones = objectContext.Translate<RolOpcion>(reader).ToList();
                }
            }
            finally
            {
                this.Contexto.Database.Connection.Close();
            }
            return resultado;
        }


        public sm_Persona ConsultarPersona(int idTipoIdentificacion, string numeroIdentificacion)
        {
            try
            {
                return this.Contexto.sm_Persona.Find(idTipoIdentificacion, numeroIdentificacion);
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        #endregion

        #region Modulo de usuarios del sistema
        /// <summary>
        /// Consultar todos los usuarios del sistema
        /// </summary>
        /// <returns></returns>
        public IList<Usuario> ListarUsuarios(int tipoID, string id, int filtro)
        {
            IList<Usuario> resultado = null;
            var command = this.Contexto.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[spRetornarUsuarios]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("tipoID", tipoID));
            command.Parameters.Add(new SqlParameter("ID", id));
            command.Parameters.Add(new SqlParameter("filtro", filtro));
            try
            {
                this.Contexto.Database.Connection.Open();
                var reader = command.ExecuteReader();
                // Drop down to the wrapped `ObjectContext` to get access to
                // the `Translate` method
                ObjectContext objectContext = (this.Contexto as IObjectContextAdapter).ObjectContext;
                // Read Entity1 from the first resultset
                resultado = objectContext.Translate<Usuario>(reader).ToList();
                // Read Entity2 from the second resultset
                if (filtro != -1 && resultado.Count != 0)
                {
                    reader.NextResult();
                    resultado[0].Roles = objectContext.Translate<sm_Rol>(reader).ToList();
                }
            }
            finally
            {
                this.Contexto.Database.Connection.Close();
            }
            return resultado;
        }

        /// <summary>
        /// Consultar los tipos de identificacion existentes
        /// </summary>
        /// <returns></returns>
        public IList<sm_TipoIdentificacion> ListarTiposID()
        {
            return this.Contexto.sm_TipoIdentificacion.ToList();            
        }

        /// <summary>
        /// Consultar los roles del sistema
        /// </summary>
        /// <returns></returns>
        public IList<sm_Rol> ListarRoles()
        {
            return this.Contexto.sm_Rol.ToList();
        }

        /// <summary>
        /// Consultar las empresas disponibles en el sistema
        /// </summary>
        /// <returns></returns>
        public IList<sm_Empresa> ListarEmpresas()
        {
            return this.Contexto.sm_Empresa.ToList();
        }
        #endregion
    }
}

