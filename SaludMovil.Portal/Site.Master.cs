using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SaludMovil.Entidades;

namespace SaludMovil.Portal
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["persona"] != null)
            {
                string parametro = Request["__EVENTARGUMENT"];
                switch (parametro)
                {
                    case "CerrarSesion":
                        Session["Persona"] = null;
                        Response.Redirect("~/Iniciar.aspx");
                        break;
                    default:
                        break;
                }
                Persona persona = (Persona)Session["Persona"];
                lblNUsuario.Text = persona.primerNombre + ' ' + persona.primerApellido;
                lblFecha.Text = DateTime.Now.ToShortDateString();
                if (persona.Roles[0].nombre.Equals("Medico") || persona.Roles[0].nombre.Equals("SuperAdministrador"))
                    lblPortal.Text = "Portal médico";
                else
                    lblPortal.Text = "Portal usuario";
                cargarMenu(persona);
            }
            else
                Response.Redirect("~/Iniciar.aspx");            
            
        }

        public void cargarMenu(Persona persona)
        {
            menu.Items.Clear();
            cargarOpcionesPrincipales(persona.Opciones);
        }

        private void cargarOpcionesPrincipales(IList<RolOpcion> opciones)
        {
            RadMenuItem item = new RadMenuItem();
            foreach (RolOpcion opcion in opciones.Where(op => op.idOpcionPadre == 1).OrderBy(op2 => op2.orden))
            {                
                item = llenarOpcionesSecundarias(opcion, opciones.Where(o => o.idOpcionPadre == opcion.idOpcion).ToList());
                menu.Items.Add(item);
            }
            menu.DataBind();
        }

        private RadMenuItem llenarOpcionesSecundarias(RolOpcion papa, IList<RolOpcion> hijos)
        {
            RadMenuItem opcion, item;
            opcion = new RadMenuItem();
            opcion.Text = papa.NombreOpcion;
            opcion.Value = papa.idOpcion.ToString();
            opcion.NavigateUrl = papa.URL;
            if (hijos.Count > 0)
            {                
                foreach (RolOpcion opcion2 in hijos)
                {
                    item = llenarOpcionesSecundarias(opcion2, hijos.Where(o => o.idOpcionPadre == opcion2.idOpcion).ToList());
                    if (item != null)
                    {
                        opcion.Items.Add(item);
                    }
                }
                return opcion;
            }
            else
            {
                if (opcion != null)
                    return opcion;
                else
                    return null;
            }
        }

        /// <summary>
        /// Evento lanzado por javascript para cerrar sesion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Persona persona = (Persona)Session["persona"];
            persona.Roles = null;
            persona.Opciones = null;
            persona = null;
            Session["persona"] = persona;
            Response.Redirect("~/Iniciar.aspx");
        }

        /// <summary>
        /// Evento lanzado por javascript para redirigir al inicio de la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInicio_Click(object sender, EventArgs e)
        {            
            Response.Redirect("~/Default.aspx");
        } 
    }
}