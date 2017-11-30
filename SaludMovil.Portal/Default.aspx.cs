using SaludMovil.Entidades;
using SaludMovil.Negocio;
using SaludMovil.Transversales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SaludMovil.Portal
{
    public partial class _Default : Page
    {
        private PacienteNegocio negocioPaciente;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLeyenda.Text = "PORTAL MÉDICO / BANDEJA DE PACIENTES";
            if (!Page.IsPostBack)
            {
                Comun.CambiarIdiomaFilterGridTelerik(radGridPaciente);
                CargarIdentificaciones();
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (hdfValidador.Value.Equals(string.Empty))
            {
                Parallel.ForEach(radGridPaciente.MasterTableView.Columns.Cast<GridColumn>().ToList(), handler =>
                {
                    if (handler.UniqueName.Equals("UltimoControl"))
                        handler.Display = false;
                    if (handler.UniqueName.Equals("TipoControl"))
                        handler.Display = false;
                    if (handler.UniqueName.Equals("RegistroControl"))
                        handler.Display = false;
                });
            }
            else
            {
                Parallel.ForEach(radGridPaciente.MasterTableView.Columns.Cast<GridColumn>().ToList(), item =>
               {
                   if (item.UniqueName.Equals("UltimoControl"))
                       item.Display = true;
                   if (item.UniqueName.Equals("TipoControl"))
                       item.Display = true;
                   if (item.UniqueName.Equals("RegistroControl"))
                       item.Display = true;
               
               });
            }
        }

        protected void radGridPaciente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                if (txtIdentificacion.Text != string.Empty && cboTipoIdentificacion.SelectedValue != "-1")
                {
                    radGridPaciente.DataSource = negocioPaciente.listarPacientesFiltro(cboTipoIdentificacion.SelectedValue, txtIdentificacion.Text);
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


        /// <summary>
        /// Tratamiento de fechas para visualizar la semaforización de estados de control de enfemería
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void radGridPaciente_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
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
                    ImageButton btnBuscar = (ImageButton)item["btnBuscar"].Controls[0];
                    btnBuscar.ToolTip = "Consultar paciente";
                    ImageButton btn360 = (ImageButton)item["btn360"].Controls[0];
                    btn360.ToolTip = "Vista 360";
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
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    //Grafica riesgo
                    GridDataItem item2 = e.Item as GridDataItem;
                    int riesgo = Convert.ToInt16(item2.GetDataKeyValue("riesgo"));
                    RadRadialGauge RadRadialGaugeRiesgo = (RadRadialGauge)item2.FindControl("RadRadialGaugeRiesgo");
                    if (riesgo == 1)
                        RadRadialGaugeRiesgo.Pointer.Value = Convert.ToDecimal(2.5);
                    else if (riesgo == 2)
                        RadRadialGaugeRiesgo.Pointer.Value = Convert.ToDecimal(1.5);
                    else if (riesgo == 3)
                        RadRadialGaugeRiesgo.Pointer.Value = Convert.ToDecimal(0.5);
                    RadRadialGaugeRiesgo.Pointer.Color = System.Drawing.Color.Blue;
                    RadRadialGaugeRiesgo.Scale.Min = 0;
                    RadRadialGaugeRiesgo.Scale.Max = (decimal)3;
                    RadRadialGaugeRiesgo.Scale.MinorUnit = (decimal)0;
                    RadRadialGaugeRiesgo.Scale.MajorUnit = (decimal)1;
                    RadRadialGaugeRiesgo.Scale.MinorTicks.Visible = false;
                    RadRadialGaugeRiesgo.Scale.MajorTicks.Size = 10;
                    RadRadialGaugeRiesgo.Scale.Labels.Visible = false;
                    RadRadialGaugeRiesgo.Scale.Labels.Font = "10px Arial,Helvetica,sans-serif";
                    RadRadialGaugeRiesgo.Scale.Labels.Color = System.Drawing.Color.Black;
                    RadRadialGaugeRiesgo.Scale.Labels.Format = "{0}";
                    RadRadialGaugeRiesgo.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;
                    GaugeRange gr1 = new GaugeRange();
                    gr1.From = 0;
                    gr1.To = (decimal)1;
                    gr1.Color = System.Drawing.Color.Green;
                    GaugeRange gr2 = new GaugeRange();
                    gr2.From = (decimal)1.1;
                    gr2.To = (decimal)2;
                    gr2.Color = System.Drawing.Color.Yellow;
                    GaugeRange gr3 = new GaugeRange();
                    gr3.From = (decimal)2.1;
                    gr3.To = (decimal)3;
                    gr3.Color = System.Drawing.Color.FromArgb(225, 0, 0);
                    RadRadialGaugeRiesgo.Scale.Ranges.Add(gr1);
                    RadRadialGaugeRiesgo.Scale.Ranges.Add(gr2);
                    RadRadialGaugeRiesgo.Scale.Ranges.Add(gr3);

                    //Grafica glucometria
                    RadRadialGauge RadLinealGaugeGlucometria = (RadRadialGauge)item2.FindControl("RadLinealGaugeGlucometria");
                    decimal Glucosa = Convert.ToDecimal(item2.GetDataKeyValue("Glucosa"));
                    decimal limiteSuperiorGlucosa = Convert.ToInt16(item2.GetDataKeyValue("limiteSuperiorGlucosa"));
                    decimal limiteInferiorGlucosa = Convert.ToInt16(item2.GetDataKeyValue("limiteInferiorGlucosa"));
                    RadLinealGaugeGlucometria.Pointer.Value = Glucosa;
                    RadLinealGaugeGlucometria.Pointer.Color = System.Drawing.Color.Blue;
                    RadLinealGaugeGlucometria.Pointer.Cap.Size = (float)0.1;
                    RadLinealGaugeGlucometria.Scale.Min = 0;
                    RadLinealGaugeGlucometria.Scale.Max = limiteSuperiorGlucosa + 20;
                    RadLinealGaugeGlucometria.Scale.MinorUnit = (decimal)1;
                    RadLinealGaugeGlucometria.Scale.MajorUnit = 30;
                    RadLinealGaugeGlucometria.Scale.MinorTicks.Visible = true;
                    RadLinealGaugeGlucometria.Scale.MajorTicks.Size = 1;
                    RadLinealGaugeGlucometria.Scale.Labels.Visible = false;
                    RadLinealGaugeGlucometria.Scale.Labels.Font = "8px Arial,Helvetica,sans-serif";
                    RadLinealGaugeGlucometria.Scale.Labels.Color = System.Drawing.Color.Black;
                    RadLinealGaugeGlucometria.Scale.Labels.Format = "{0}";
                    RadLinealGaugeGlucometria.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;
                    GaugeRange Glucosagr1 = new GaugeRange();
                    Glucosagr1.From = 0;
                    Glucosagr1.To = limiteInferiorGlucosa;
                    Glucosagr1.Color = System.Drawing.Color.Red;
                    GaugeRange Glucosagr2 = new GaugeRange();
                    Glucosagr2.From = (decimal)limiteInferiorGlucosa + Convert.ToDecimal(0.1);
                    Glucosagr2.To = (decimal)limiteSuperiorGlucosa - Convert.ToDecimal(0.1);
                    Glucosagr2.Color = System.Drawing.Color.Green;
                    GaugeRange Glucosagr3 = new GaugeRange();
                    Glucosagr3.From = (decimal)limiteSuperiorGlucosa;
                    Glucosagr3.To = (decimal)limiteSuperiorGlucosa + Convert.ToDecimal(50);
                    Glucosagr3.Color = System.Drawing.Color.Red;
                    RadLinealGaugeGlucometria.Scale.Ranges.Add(Glucosagr1);
                    RadLinealGaugeGlucometria.Scale.Ranges.Add(Glucosagr2);
                    RadLinealGaugeGlucometria.Scale.Ranges.Add(Glucosagr3);

                    //Grafica tension
                    RadRadialGauge radialGauge = (RadRadialGauge)item2.FindControl("RadRadialGaugeTension");

                    decimal Sistolica = Convert.ToDecimal(item2.GetDataKeyValue("Sistolica"));
                    //decimal Diastolica = Convert.ToDecimal(item2.GetDataKeyValue("Diastolica"));
                    decimal limiteSuperiorSistolica = Convert.ToInt16(item2.GetDataKeyValue("limiteSuperiorSistolica"));
                    decimal limiteInferiorSistolica = Convert.ToInt16(item2.GetDataKeyValue("limiteInferiorSistolica"));

                    radialGauge.Pointer.Value = Sistolica;
                    radialGauge.Pointer.Color = System.Drawing.Color.Blue;
                    radialGauge.Pointer.Cap.Size = (float)0.1;
                    radialGauge.Scale.Min = 0;
                    radialGauge.Scale.Max = limiteSuperiorSistolica + 20;
                    radialGauge.Scale.MinorUnit = (decimal)1;
                    radialGauge.Scale.MajorUnit = 30;
                    radialGauge.Scale.MinorTicks.Visible = false;
                    radialGauge.Scale.MajorTicks.Size = 1;
                    radialGauge.Scale.Labels.Visible = false;
                    radialGauge.Scale.Labels.Font = "8px Arial,Helvetica,sans-serif";
                    radialGauge.Scale.Labels.Color = System.Drawing.Color.Black;
                    radialGauge.Scale.Labels.Format = "{0}";
                    radialGauge.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;

                    GaugeRange Sistolicagr1 = new GaugeRange();
                    Sistolicagr1.From = 0;
                    Sistolicagr1.To = limiteInferiorSistolica;
                    Sistolicagr1.Color = System.Drawing.Color.Red;
                    GaugeRange Sistolicagr2 = new GaugeRange();
                    Sistolicagr2.From = (decimal)limiteInferiorSistolica + Convert.ToDecimal(0.1);
                    Sistolicagr2.To = (decimal)limiteSuperiorSistolica - Convert.ToDecimal(0.1);
                    Sistolicagr2.Color = System.Drawing.Color.Green;
                    GaugeRange Sistolicagr3 = new GaugeRange();
                    Sistolicagr3.From = (decimal)limiteSuperiorSistolica;
                    Sistolicagr3.To = (decimal)limiteSuperiorSistolica + Convert.ToDecimal(50);
                    Sistolicagr3.Color = System.Drawing.Color.Red;
                    radialGauge.Scale.Ranges.Add(Sistolicagr1);
                    radialGauge.Scale.Ranges.Add(Sistolicagr2);
                    radialGauge.Scale.Ranges.Add(Sistolicagr3);

                    //Tension diastólica
                    RadRadialGauge RadRadialGaugeTensionDiastolica = (RadRadialGauge)item2.FindControl("RadRadialGaugeTensionDiastolica");
                    decimal Diastolica = Convert.ToDecimal(item2.GetDataKeyValue("Diastolica"));
                    decimal limiteSuperiorDiastolica = Convert.ToInt16(item2.GetDataKeyValue("limiteSuperiorDiastolica"));
                    decimal limiteInferiorDiastolica = Convert.ToInt16(item2.GetDataKeyValue("limiteInferiorDiastolica"));
                    RadRadialGaugeTensionDiastolica.Pointer.Value = Diastolica;
                    RadRadialGaugeTensionDiastolica.Pointer.Color = System.Drawing.Color.Blue;
                    RadRadialGaugeTensionDiastolica.Pointer.Cap.Size = (float)0.1;
                    RadRadialGaugeTensionDiastolica.Scale.Min = 0;
                    RadRadialGaugeTensionDiastolica.Scale.Max = limiteSuperiorDiastolica + 20;
                    RadRadialGaugeTensionDiastolica.Scale.MinorUnit = (decimal)1;
                    RadRadialGaugeTensionDiastolica.Scale.MajorUnit = 30;
                    RadRadialGaugeTensionDiastolica.Scale.MinorTicks.Visible = false;
                    RadRadialGaugeTensionDiastolica.Scale.MajorTicks.Size = 1;
                    RadRadialGaugeTensionDiastolica.Scale.Labels.Visible = false;
                    RadRadialGaugeTensionDiastolica.Scale.Labels.Font = "8px Arial,Helvetica,sans-serif";
                    RadRadialGaugeTensionDiastolica.Scale.Labels.Color = System.Drawing.Color.Black;
                    RadRadialGaugeTensionDiastolica.Scale.Labels.Format = "{0}";
                    RadRadialGaugeTensionDiastolica.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;
                    GaugeRange Diastolicagr1 = new GaugeRange();
                    Diastolicagr1.From = 0;
                    Diastolicagr1.To = limiteInferiorDiastolica;
                    Diastolicagr1.Color = System.Drawing.Color.Red;
                    GaugeRange Diastolicagr2 = new GaugeRange();
                    Diastolicagr2.From = (decimal)limiteInferiorDiastolica + Convert.ToDecimal(0.1);
                    Diastolicagr2.To = (decimal)limiteSuperiorDiastolica - Convert.ToDecimal(0.1);
                    Diastolicagr2.Color = System.Drawing.Color.Green;
                    GaugeRange Diastolicagr3 = new GaugeRange();
                    Diastolicagr3.From = (decimal)limiteSuperiorDiastolica;
                    Diastolicagr3.To = (decimal)limiteSuperiorDiastolica + Convert.ToDecimal(50);
                    Diastolicagr3.Color = System.Drawing.Color.Red;
                    RadRadialGaugeTensionDiastolica.Scale.Ranges.Add(Diastolicagr1);
                    RadRadialGaugeTensionDiastolica.Scale.Ranges.Add(Diastolicagr2);
                    RadRadialGaugeTensionDiastolica.Scale.Ranges.Add(Diastolicagr3);
                }

            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }


        /// <summary>
        /// Muestra columnas iniciales para visualizar fechas y estados de control de enfermería
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVerMas_Click(object sender, EventArgs e)
        {
            string validador = hdfValidador.Value;
            if (validador.Equals(string.Empty))
            {
                hdfValidador.Value = "1";
                btnVerMas.Text = "Ocultar información";
            }
            else
            {
                hdfValidador.Value = string.Empty;
                btnVerMas.Text = "Ver más información";
            }
        }

        /// <summary>
        /// Carga lista de identificaciones
        /// </summary>
        private void CargarIdentificaciones()
        {

            negocioPaciente = new PacienteNegocio();
            cboTipoIdentificacion.DataSource = negocioPaciente.listarTiposIdentificacion();
            cboTipoIdentificacion.DataValueField = "idTipoIdentificacion";
            cboTipoIdentificacion.DataTextField = "nombre";
            cboTipoIdentificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboTipoIdentificacion.DataBind();
        }

        /// <summary>
        /// Botón para filtrar grilla por tipoo y número de identificación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            radGridPaciente.Rebind();
        }
    }
}