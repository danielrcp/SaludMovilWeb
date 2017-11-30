using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaludMovil.Transversales;
using SaludMovil.Negocio;
using SaludMovil.Entidades;
using System.Web.Mvc;

namespace SaludMovil.Portal.ModPacientes
{
    public partial class Framingham : System.Web.UI.Page
    {
        private int idPaciente;

        protected void Page_Load(object sender, EventArgs e)
        {
            //idPaciente = Convert.ToInt32(Request.QueryString["idPaciente"].ToString());
            idPaciente = 1;
            CargarPagina();
            ConsultarPaciente(idPaciente);
        }


        private void CargarPagina() 
        {
            CargarGenero();
        }

        private void CargarGenero()
        {
            ddlGenero.DataSource = Comun.CargarGenero();
            ddlGenero.DataValueField = "Value";
            ddlGenero.DataTextField = "Text";
            ddlGenero.DataBind();
        }

        private void ConsultarPaciente(int idPaciente)
        {
            //sm_Paciente paciente = new sm_Paciente();
            //pacienteNegocio = new PacienteNegocio();
            //paciente = pacienteNegocio.ConsultarPaciente(idPaciente);
            //lblEdad.Text = paciente.Edad.ToString();   
        }
    }
}