using SaludMovil.Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SaludMovil.Portal.ModReportes
{
    public partial class ReporteEnfermeria : Page
    {
        private PacienteNegocio negocioPaciente;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLeyenda.Text = "PORTAL MÉDICO / REPORTES / REPORTE DE SEGUIMIENTO";
            CambiarIdiomaFilterGridTelerik(radGridPaciente);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (GridColumn item in radGridPaciente.MasterTableView.Columns)
            {
                if (item.UniqueName.Equals("UltimoControl"))
                    item.Display = true;
                if (item.UniqueName.Equals("TipoControl"))
                    item.Display = true;
                if (item.UniqueName.Equals("RegistroControl"))
                    item.Display = true;
            }
        }

        protected void radGridPaciente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                if (rdpFechaInicio.SelectedDate != null && rdpFechaFin.SelectedDate != null)
                {
                    radGridPaciente.DataSource = negocioPaciente.listarPacientesFiltroFechas(rdpFechaInicio.SelectedDate,rdpFechaFin.SelectedDate);
                }
                else
                {
                    radGridPaciente.DataSource = negocioPaciente.listarPacientes();
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        protected void radGridPaciente_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Buscar")
            {
                GridEditableItem item = e.Item as GridEditableItem;
                string idTipoIdentificacion = item.GetDataKeyValue("idTipoIdentificacion").ToString();
                string numeroIdentificacion = item.GetDataKeyValue("NumeroIdentifacion").ToString();
                Response.Redirect("~/ModPacientes/RegistroPaciente.aspx?idTipoIdentificacion=" + idTipoIdentificacion + "&NumeroIdentifacion=" + numeroIdentificacion);
            }

            if (e.CommandName == "360")
            {
                GridEditableItem item = e.Item as GridEditableItem;
                string idTipoIdentificacion = item.GetDataKeyValue("idTipoIdentificacion").ToString();
                string numeroIdentificacion = item.GetDataKeyValue("NumeroIdentifacion").ToString();
                int riesgo = Convert.ToInt16(item.GetDataKeyValue("riesgo"));
                decimal Glucosa = Convert.ToDecimal(item.GetDataKeyValue("Glucosa"));
                decimal limiteSuperiorGlucosa = Convert.ToInt16(item.GetDataKeyValue("limiteSuperiorGlucosa"));
                decimal limiteInferiorGlucosa = Convert.ToInt16(item.GetDataKeyValue("limiteInferiorGlucosa"));
                decimal Sistolica = Convert.ToDecimal(item.GetDataKeyValue("Sistolica"));
                decimal limiteSuperiorSistolica = Convert.ToInt16(item.GetDataKeyValue("limiteSuperiorSistolica"));
                decimal limiteInferiorSistolica = Convert.ToInt16(item.GetDataKeyValue("limiteInferiorSistolica"));
                decimal Diastolica = Convert.ToDecimal(item.GetDataKeyValue("Diastolica"));
                decimal limiteSuperiorDiastolica = Convert.ToInt16(item.GetDataKeyValue("limiteSuperiorDiastolica"));
                decimal limiteInferiorDiastolica = Convert.ToInt16(item.GetDataKeyValue("limiteInferiorDiastolica"));
                DateTime FechaGlucosa = Convert.ToDateTime(item.GetDataKeyValue("FechaGlucosa"));
                DateTime FechaSistolica = Convert.ToDateTime(item.GetDataKeyValue("FechaSistolica"));
                DateTime FechaDiastolica = Convert.ToDateTime(item.GetDataKeyValue("FechaDiastolica"));
                Response.Redirect("~/ModPacientes/Vista360.aspx?idTipoIdentificacion=" + idTipoIdentificacion + "&NumeroIdentifacion=" + numeroIdentificacion
                    + "&riesgo=" + riesgo + "&Glucosa=" + Glucosa + "&limiteSuperiorGlucosa=" + limiteSuperiorGlucosa + "&limiteInferiorGlucosa=" + limiteInferiorGlucosa
                    + "&Sistolica=" + Sistolica + "&limiteSuperiorSistolica=" + limiteSuperiorSistolica + "&limiteInferiorSistolica=" + limiteInferiorSistolica
                    + "&Diastolica=" + Diastolica + "&limiteSuperiorDiastolica=" + limiteSuperiorDiastolica + "&limiteInferiorDiastolica=" + limiteInferiorDiastolica
                    + "&FechaGlucosa=" + FechaGlucosa + "&FechaSistolica=" + FechaSistolica + "&FechaDiastolica=" + FechaDiastolica);
            }
        }

        private void CambiarIdiomaFilterGridTelerik(RadGrid grilla)
        {
            List<GridFilterMenu> grids = new List<GridFilterMenu>();
            grids.Add(grilla.FilterMenu);
            foreach (GridFilterMenu gridFilterMenu in grids)
            {
                //GridFilterMenu menu = RadGridCasosRadicados.FilterMenu;
                GridFilterMenu menu = gridFilterMenu;
                foreach (RadMenuItem item in menu.Items)
                {
                    //change the text for the "StartsWith" menu item
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "No Filtrar";
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "Contiene";
                    }
                    if (item.Text == "DoesNotContain")
                    {
                        item.Text = "No Contiene";
                    }
                    if (item.Text == "StartsWith")
                    {
                        item.Text = "Empieza Con";
                    }
                    if (item.Text == "EndsWith")
                    {
                        item.Text = "Finaliza Con";
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "Es igual a";
                    }
                    if (item.Text == "NotEqualTo")
                    {
                        item.Text = "No es igual a";
                    }
                    if (item.Text == "GreaterThan")
                    {
                        item.Text = "Es mayor que";
                    }
                    if (item.Text == "LessThan")
                    {
                        item.Text = "Es menor que";
                    }
                    if (item.Text == "GreaterThanOrEqualTo")
                    {
                        item.Text = "Es mayor o igual";
                    }
                    if (item.Text == "LessThanOrEqualTo")
                    {
                        item.Text = "Es menor o igual";
                    }
                    if (item.Text == "IsEmpty")
                    {
                        item.Text = "Es vacio";
                    }
                    if (item.Text == "NotIsEmpty")
                    {
                        item.Text = "No es vacio";
                    }
                    if (item.Text == "IsNull")
                    {
                        item.Text = "Es Nulo";
                    }
                    if (item.Text == "NotIsNull")
                    {
                        item.Text = "No es Nulo";
                    }
                    if (item.Text == "Between")
                    {
                        item.Text = "";
                    }
                    if (item.Text == "NotBetween")
                    {
                        item.Text = "";
                    }
                }
            }
        }

        protected void radGridPaciente_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DateTime fechaControl;
                GridDataItem item = (GridDataItem)e.Item;
                TableCell celdaFechaControl = item["UltimoControl"];
                TableCell celdaTipoControl = item["TipoControl"];
                TableCell celdaRegistroControl = item["RegistroControl"];
                celdaFechaControl.Font.Size = 10;
                celdaFechaControl.Font.Bold = true;
                if (!item["UltimoControl"].Text.Equals("01/01/1900"))
                {
                    fechaControl = DateTime.ParseExact(item["UltimoControl"].Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string fecha1 = fechaControl.ToString("dd/MM/yyyy");
                    string fecha2 = DateTime.Now.ToString("dd/MM/yyyy");
                    string fecha3 = item["RegistroControl"].Text;
                    DateTime fechaSinDia = fechaControl.AddDays(-1);
                    string fecha4 = fechaSinDia.ToString("dd/MM/yyyy");
                    if (fechaControl > DateTime.Now && fecha3.Equals("01/01/1900") && (fechaControl - DateTime.Now).TotalDays > 1)
                    {
                        celdaFechaControl.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (fechaControl < DateTime.Now && fecha3.Equals("01/01/1900"))
                    {
                        celdaFechaControl.ForeColor = System.Drawing.Color.Red;
                    }
                    else if (fecha4.Equals(fecha2) && fecha3.Equals("01/01/1900"))
                    {
                        celdaFechaControl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#9F9110");
                    }
                    else if (fechaControl < DateTime.Now && !fecha3.Equals("01/01/1900"))
                    {
                        celdaFechaControl.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (!fecha3.Equals("01/01/1900"))
                    {
                        celdaFechaControl.ForeColor = System.Drawing.Color.Green;
                    }
                }
                if (item["UltimoControl"].Text.Equals("01/01/1900"))
                    item["UltimoControl"].Text = "Sin registro";
                if (item["RegistroControl"].Text.Equals("01/01/1900"))
                    item["RegistroControl"].Text = "Sin registro";
            }
        }

        protected void btnVerMas_Click(object sender, EventArgs e)
        {
            try
            {
                radGridPaciente.ExportSettings.IgnorePaging = true;
                radGridPaciente.ExportSettings.ExportOnlyData = true;
                radGridPaciente.ExportSettings.FileName = "ReporteEnfermeria";
                radGridPaciente.ExportSettings.HideStructureColumns = true;
                radGridPaciente.MasterTableView.ExportToExcel();
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }



        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            radGridPaciente.Rebind();
        }
    }
}