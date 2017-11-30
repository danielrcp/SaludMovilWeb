using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using Telerik.Web.UI;

namespace SaludMovil.Portal.ModAdmin
{
    public partial class Iniciar : System.Web.UI.Page
    {
        private AdministracionNegocio adminNegocio;
        //TODO:
        /*
         * 1. Crear la pàgina de Olvido su Contraseña-Hagan Prototipos primero
         * 2. Crear la pàgina de Cambiar Contraseña-Hagan Prototipos primero
         * 3. Hagan la lògica para que los puntos anteriores funcionen
         * 4. Con RolOpcion, saquen las opciones y dibujen el Menu - OK
         * 
         */


        protected void Page_Load(object sender, EventArgs e)
        {
            adminNegocio = new AdministracionNegocio();
            //TODO:Realizar metodo de seguridad integrada
            if (!Page.IsPostBack && (Session["persona"] != null))
            {
                Persona persona = (Persona)Session["persona"];
                IList<sm_Rol> roles = persona.Roles;
                if (roles.Count == 1)
                {
                    persona.Opciones = persona.Opciones.Where(o => o.idRol == persona.Roles[0].idRol).Where(o => o.idOpcion != 10 && o.idOpcion != 11 && o.idOpcion != 12 && o.idOpcion != 13).ToList();
                    Response.Redirect("~/Default.aspx");
                }
                else
                    lanzarVentana();
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text;
                string contrasena = txtPassword.Text;
                if (!usuario.Equals(string.Empty) && !contrasena.Equals(string.Empty))
                {
                    Persona persona = null;
                    persona = adminNegocio.Autenticar(usuario, contrasena, "WebForms");
                    if (persona != null)
                    {
                        Session["login"] = usuario;
                        Session["persona"] = persona;
                        if (persona.Roles.Count > 1)//Se encontraron varios perfiles
                        {
                            lanzarVentana();
                        }
                        else
                        {
                            persona.Opciones = persona.Opciones.Where(o => o.idRol == persona.Roles[0].idRol).Where(o => o.idOpcion != 10 && o.idOpcion != 11 && o.idOpcion != 12 && o.idOpcion != 13).ToList();
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                    else //No se encontro la persona
                    {
                        txtUsuario.Text = "";
                        txtPassword.Text = "";
                        MostrarMensaje("Error de inicio de sesión. No existe el usuario",false);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Se presento un error al iniciar sesión :( " + ex.Message, true);
            }
            /*
             * 1. Consultar a la base de datos si existe este usuario (usu, psw).....Lo hacen por un SP
             * (deben de consultar Tabla Usuario, Sì tiene màs de 1 Rol crear Nueva pantalla donde
             *   seleccione el Rol, Tabla Rol, Tabla RolOpcion)
             * Llenar el DTO de Usuario y Rol
             * Llenar la Entidad de RolOpcion
             * PENDIENTE: Cifrar el Password 
             * 2. Si no Existe (Saquen mensaje: No existe usuario o clave o usurio incorrecto.....sacar de archivo de Recursos) 
             * 3. Declarar una Variable de Session y van a meter el DTO (usuario, rol)
             * 4. Response Redirect(../ModGeneral/Default.aspx) a la pantalla que sigue
             */
        }

        private void lanzarVentana()
        {
            RadWindowManager manejadorVentanas = new RadWindowManager();
            RadWindow ventanaRoles = new RadWindow();
            ventanaRoles.NavigateUrl = "Roles.aspx";
            ventanaRoles.ID = "ventanaRoles";
            ventanaRoles.VisibleOnPageLoad = true; // Set this property to True for showing window from code
            ventanaRoles.Title = "Escoja un rol";
            ventanaRoles.CssClass = "text-center";
            ventanaRoles.Behaviors = WindowBehaviors.None;
            ventanaRoles.InitialBehaviors = WindowBehaviors.Resize;
            ventanaRoles.Modal = true;
            ventanaRoles.CenterIfModal = true;
            manejadorVentanas.Windows.Add(ventanaRoles);
            this.form1.Controls.Add(ventanaRoles);
        }

        #region Manejador de mensajes
        private void MostrarMensaje(string mensaje, bool esError)
        {
            string msg = mensaje;
            if (esError)
                msg = "Error: " + mensaje.Replace("'", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            RadNotificationMensajes.Show(msg);
        }
        #endregion
    }
}