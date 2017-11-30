using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SaludMovil.Portal;
using System.Configuration;

namespace SaludMovil.Portal
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        /// <summary>
        /// Levanta la sesion actual del equipo para ingresar inmediatamente a la aplicacion y manejar los usuarios anonimos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_Start(Object sender, EventArgs e)
        {
            string name = User.Identity.Name;
            if (!string.IsNullOrEmpty(name))
            {
                string dominio = ConfigurationManager.AppSettings["Dominio"].ToString();
                string dominioEntrada = string.Empty;
                string usuarioEntrada = string.Empty;
                dominioEntrada = name.Split('\\')[0];
                usuarioEntrada = name.Split('\\')[1];
                if (dominioEntrada.Equals(dominio))//Si el dominio de entrada del usuario coincide con el permitido se levanta la sesion y se redirecciona
                {
                    SaludMovil.Entidades.Persona usuario = new SaludMovil.Entidades.Persona();
                    SaludMovil.Negocio.AdministracionNegocio adminNegocio = new SaludMovil.Negocio.AdministracionNegocio();
                    usuario = adminNegocio.Autenticar(usuarioEntrada, string.Empty, "WindowsAuth");
                    Session["login"] = usuarioEntrada;
                    Session["persona"] = usuario;
                    Response.Redirect("~/Iniciar.aspx");
                }
                else//Si el dominio no coincide se deja seguir la aplicacion al inicio por webForm
                {
                    Response.Redirect("~/Iniciar.aspx");
                }
            }
            else//Usuarios que no se encuetran dentro del dominio
            {
                Response.Redirect("~/Iniciar.aspx");
                //TODO Probar que llega cuando un usuario anonimo entra al sitio
            }
        }
    }
}
