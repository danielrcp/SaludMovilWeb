using SaludMovil.Entidades;
using SaludMovil.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaludMovil.Negocio
{
    public class AdministracionNegocio
    {
        private UnidadTrabajo unitOfWork;

        public AdministracionNegocio()
        { 
        
        }

        #region Modulo Autenticacion
        /// <summary>
        /// Autenticacion por formulario que recibe usuario, contraseña y como se va a autenticar
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="contrasena"></param>
        /// <param name="formaAuth"></param>
        /// <returns></returns>
        public Persona Autenticar(string usuario, string contrasena, string formaAuth)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonaRepository.Autenticar(usuario, contrasena, formaAuth);
            }
        }

        /// <summary>
        /// Listar las opciones por rol
        /// </summary>
        /// <param name="idRol"></param>
        /// <returns></returns>
        public IList<RolOpcion> OpcionesRol(int idRol)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.rolOpcionRepository.OpcionesRol(idRol);
            }
        }
        #endregion

        #region Modulo de tablas administrables
        /// <summary>
        /// Obtiene las tablas administrables disponibles. Recibe por parametro el rol del usuario y el prefijo de las tablas
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="prefijo"></param>
        /// <returns></returns>
        public IList<TablaAdministrable> ConsultarTablasAdministrables(string roles, string prefijo)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TablasAdministrablesRepository.ListarTablas(roles,prefijo);
            }
        }
                
        /// <summary>
        /// Obtiene la especificacion de la tabla recibiendo como parametro el nombre de la tabla
        /// </summary>
        /// <param name="nombreTabla"></param>
        /// <returns></returns>
        public IList<EspecificacionObjeto> ConsultarEspecificacion(string nombreTabla)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TablasAdministrablesRepository.ConsultarEspecificacion(nombreTabla);
            }
        }
        #endregion

        #region Modulo formularios dinamicos
        /// <summary>
        /// Consultar las llaves foraneas de una tabla
        /// </summary>
        /// <param name="nombreTabla"></param>
        /// <returns></returns>
        public IList<Llave> ConsultarLlaves(string nombreTabla)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TablasAdministrablesRepository.ConsultarLlaves(nombreTabla);
            }
        }

        /// <summary>
        /// Consulta las llaves homologadas de una tabla especifica
        /// </summary>
        /// <param name="nombreTabla"></param>
        /// <returns></returns>
        public IList<Llave> ConsultarLlavesHomologadas(string nombreTabla)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.TablasAdministrablesRepository.ConsultarLlavesHomologadas(nombreTabla);
            }
        }
        #endregion

        #region Metodos generales
        public IList<sm_Estado> ConsultarEstados(string tabla)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.EstadoRepository.ConsultarEstados(tabla);
            }
        }

        public IList<sm_Poblacion> ListarPoblaciones()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.poblacionRepository.ListarPoblaciones();
            }
        }
        #endregion

        #region Modulo de usuarios del sistema
        /// <summary>
        /// Consulta todos los usuarios o uno en particular
        /// </summary>
        /// <param name="tipoID"></param>
        /// <param name="id"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IList<Usuario> ListarUsuarios(int tipoID, string id, int filtro)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonaRepository.ListarUsuarios(tipoID, id, filtro);
            }
        }

        /// <summary>
        /// Consulta el listado de tipos de identificacion
        /// </summary>
        /// <returns></returns>
        public IList<sm_TipoIdentificacion> ListarTiposID()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonaRepository.ListarTiposID();
            }
        }

        /// <summary>
        /// Consulta listado de tipos de identificacion
        /// </summary>
        /// <returns></returns>
        public IList<sm_Ciudad> ListarCiudades()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.CiudadRepository.ListarCiudades();
            }
        }

        /// <summary>
        /// Consulta los roles del sistema
        /// </summary>
        /// <returns></returns>
        public IList<sm_Rol> ListarRoles()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonaRepository.ListarRoles();
            }
        }

        /// <summary>
        /// Consulta las empresas del sistema
        /// </summary>
        /// <returns></returns>
        public IList<sm_Empresa> ListarEmpresas()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.PersonaRepository.ListarEmpresas();
            }
        }

        /// <summary>
        /// Guardar objeto persona, usuario
        /// </summary>
        /// <param name="programa"></param>
        /// <returns></returns>
        public bool GuardarUsuario(sm_Persona persona, sm_Usuario usuario)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonaRepository.Insert(persona);
                unitOfWork.UsuarioRepository.Insert(usuario);
                unitOfWork.SaveChanges();                
            }
            return true;
        }

        /// <summary>
        /// Guardar objeto persona
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        public bool GuardarPersona(sm_Persona persona)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonaRepository.Insert(persona);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Guardar el objeto personal medico
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        public bool GuardarMedico(sm_PersonalMedico medico)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonalMedicoRepository.Insert(medico);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Guarda entidad usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool GuardarUsuario(sm_Usuario usuario) 
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.UsuarioRepository.Insert(usuario);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Guarda el objeto usuarioRol
        /// </summary>
        /// <param name="usuarioRol"></param>
        /// <returns></returns>
        public bool GuardarRol(sm_UsuarioRol usuarioRol)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.usuarioRolRepository.Insert(usuarioRol);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualizar objeto persona, usuario
        /// </summary>
        /// <param name="programa"></param>
        /// <returns></returns>
        public bool ActualizarUsuario(sm_Persona persona, sm_Usuario usuario)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonaRepository.Update(persona);
                if (usuario.contrasena.Equals(string.Empty))
                    unitOfWork.UsuarioRepository.ActualizarUsuarioSinContrasena(usuario);
                else
                    unitOfWork.UsuarioRepository.Update(usuario);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualizar el objeto persona
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        public bool ActualizarPersona(sm_Persona persona)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonaRepository.Update(persona);                
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualizar el objeto peronal medico
        /// </summary>
        /// <param name="medico"></param>
        /// <returns></returns>
        public bool ActualizarMedico(sm_PersonalMedico medico)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.PersonalMedicoRepository.Update(medico);
                unitOfWork.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Eliminar los usuarioRol
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        public bool EliminarRol(int idUsuario, int idRol)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.usuarioRolRepository.BorrarUsuarioRol(idUsuario, idRol);
                unitOfWork.SaveChanges();
            }
            return true;
        }
        #endregion

        #region Modulo control de opciones del sistema
        public IList<sm_Opcion> ListarOpciones()
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                return unitOfWork.opcionRepository.ListarOpciones();
            }
        }

        /// <summary>
        /// Inserta los permisos de un rol a una opcion
        /// </summary>
        /// <param name="idRol"></param>
        /// <param name="nuevaOpcion"></param>
        public void InsertarPermisosMenu(int idRol, sm_RolOpcion nuevaOpcion)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.rolOpcionRepository.InsertarOpcion(nuevaOpcion);
                unitOfWork.SaveChanges();                
            }
        }

        /// <summary>
        /// Eliminar todos los registros de opciones para el rol especificado
        /// </summary>
        /// <param name="idRol"></param>
        public void BorrarPermisosMenu(int idRol)
        {
            using (unitOfWork = new UnidadTrabajo())
            {
                unitOfWork.rolOpcionRepository.EliminarOpcionesRol(idRol);
                unitOfWork.SaveChanges();
            }
        }
        #endregion
    }
}