using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaludMovil.Entidades;
using Telerik.Web.UI;

namespace SaludMovil.Portal
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                cargarRoles();
        }

        private void cargarRoles()
        {
            Persona persona = (Persona)Session["persona"];
            IList<sm_Rol> roles = persona.Roles;
            cboRoles.DataSource = roles;
            cboRoles.DataTextField = "nombre";
            cboRoles.DataValueField = "idRol";
            cboRoles.DataBind();
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {            
            if (!cboRoles.SelectedValue.Equals("0"))
            {
                Persona persona = (Persona)Session["persona"];
                IList<sm_Rol> roles = persona.Roles;
                int idRol = Convert.ToInt32(cboRoles.SelectedValue);
                roles = roles.Where(r => r.idRol == idRol).ToList();
                persona.Roles = roles;
                persona.Opciones = persona.Opciones.Where(o => o.idRol == idRol).Where(o => o.idOpcion != 10 && o.idOpcion != 11 && o.idOpcion != 12 && o.idOpcion != 13).ToList();
                Session["Persona"] = persona;
                string script = "function f(){closeWin(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);                
            }
            else 
                RadNotificationMensajes.Show("Debe seleccionar un rol");
        }
    }
}