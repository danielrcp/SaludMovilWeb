using Newtonsoft.Json.Linq;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using SaludMovil.Transversales;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.UI;
using Telerik.Web.UI;
using System.Threading.Tasks;
namespace SaludMovil.Portal.ModPacientes
{
    public partial class Vista360 : System.Web.UI.Page
    {
        private PacienteNegocio negocioPaciente;
        private sm_Persona persona;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Parallel.Invoke(
                  () => { CargarDatosLabel(); },
                  () => { CargarGraficaTension(); },
                  () => { CargarGraficaPeso(); },
                  () => { CargarGraficaTalla(); },
                  () => { CargarCalendario(); },
                  () => { CargarGaugeRiesgo(); },
                  () => { CargarGaugeGlucosa(); },
                  () => { CargarTensionSistolica(); },
                  () => { CargarTensionDiastolica(); }
                  );
            }
        }
        #region Graficas

        private void CargarGraficaTension()
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                RadHtmlChartTension.DataSource = negocioPaciente.ConsultaGraficaTension(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOTENSION);
                RadHtmlChartTension.DataBind();
                RadHtmlChartTension.PlotArea.XAxis.DataLabelsField = "fechaEvento";
                RadHtmlChartTension.PlotArea.XAxis.LabelsAppearance.RotationAngle = 90;

                PlotBand xAxisPlotBand = new PlotBand();
                xAxisPlotBand.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorSistolica"]);
                xAxisPlotBand.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorSistolica"]);
                xAxisPlotBand.Color = System.Drawing.ColorTranslator.FromHtml("#6453FD");
                xAxisPlotBand.Alpha = (byte)190;
                RadHtmlChartTension.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand);

                PlotBand xAxisPlotBand2 = new PlotBand();
                xAxisPlotBand2.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorDiastolica"]);
                xAxisPlotBand2.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorDiastolica"]);
                xAxisPlotBand2.Color = System.Drawing.ColorTranslator.FromHtml("#FC6969");
                xAxisPlotBand2.Alpha = (byte)190;
                RadHtmlChartTension.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand2);

            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        private void CargarGraficaPeso()
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                RadHtmlChartPeso.DataSource = negocioPaciente.ConsultaBandejaTomas(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOPESO);
                RadHtmlChartPeso.DataBind();
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        private void CargarGraficaTalla()
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                RadHtmlChartTalla.DataSource = negocioPaciente.ConsultaGraficaTension(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOGLUCOSA);
                RadHtmlChartTalla.DataBind();
                RadHtmlChartTalla.PlotArea.XAxis.DataLabelsField = "fechaEvento";
                RadHtmlChartTalla.PlotArea.XAxis.LabelsAppearance.RotationAngle = 90;
                PlotBand xAxisPlotBand = new PlotBand();
                xAxisPlotBand.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorGlucosa"]);
                xAxisPlotBand.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorGlucosa"]);
                xAxisPlotBand.Color = System.Drawing.ColorTranslator.FromHtml("#04B404");
                xAxisPlotBand.Alpha = (byte)190;
                RadHtmlChartTalla.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand);
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        #endregion Graficas

        #region Gauges

        private void CargarGaugeRiesgo()
        {
            try
            {
                int riesgo = Convert.ToInt16(Request.QueryString["riesgo"]);
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
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        private void CargarGaugeGlucosa()
        {
            try
            {
                decimal Glucosa = Convert.ToDecimal(Request.QueryString["Glucosa"]);
                decimal limiteSuperiorGlucosa = Convert.ToInt16(Request.QueryString["limiteSuperiorGlucosa"]);
                decimal limiteInferiorGlucosa = Convert.ToInt16(Request.QueryString["limiteInferiorGlucosa"]);
                RadLinealGaugeGlucometria.Pointer.Value = Glucosa;
                RadLinealGaugeGlucometria.Pointer.Color = System.Drawing.Color.Blue;
                RadLinealGaugeGlucometria.Pointer.Cap.Size = (float)0.1;
                RadLinealGaugeGlucometria.Scale.Min = 0;
                RadLinealGaugeGlucometria.Scale.Max = limiteSuperiorGlucosa + 20;
                RadLinealGaugeGlucometria.Scale.MinorUnit = (decimal)1;
                RadLinealGaugeGlucometria.Scale.MajorUnit = 10;
                RadLinealGaugeGlucometria.Scale.MinorTicks.Visible = true;
                RadLinealGaugeGlucometria.Scale.MajorTicks.Size = 1;
                RadLinealGaugeGlucometria.Scale.Labels.Visible = true;
                RadLinealGaugeGlucometria.Scale.Labels.Font = "8px Arial,Helvetica,sans-serif";
                RadLinealGaugeGlucometria.Scale.Labels.Color = System.Drawing.Color.Black;
                RadLinealGaugeGlucometria.Scale.Labels.Format = "{0}";
                RadLinealGaugeGlucometria.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;
                GaugeRange Glucosagr1 = new GaugeRange();
                Glucosagr1.From = 0;
                Glucosagr1.To = limiteInferiorGlucosa;
                if (limiteInferiorGlucosa == 0)
                {
                    Glucosagr1.Color = System.Drawing.Color.Gray;
                }
                else
                {
                    Glucosagr1.Color = System.Drawing.Color.Red;
                }
                GaugeRange Glucosagr2 = new GaugeRange();
                Glucosagr2.From = (decimal)limiteInferiorGlucosa + Convert.ToDecimal(0.1);
                Glucosagr2.To = (decimal)limiteSuperiorGlucosa - Convert.ToDecimal(0.1);
                Glucosagr2.Color = System.Drawing.Color.Green;
                GaugeRange Glucosagr3 = new GaugeRange();
                Glucosagr3.From = (decimal)limiteSuperiorGlucosa;
                Glucosagr3.To = (decimal)limiteSuperiorGlucosa + Convert.ToDecimal(50);
                if (limiteSuperiorGlucosa == 0)
                {
                    Glucosagr3.Color = System.Drawing.Color.Gray;
                }
                else
                {
                    Glucosagr3.Color = System.Drawing.Color.Red;
                }
                RadLinealGaugeGlucometria.Scale.Ranges.Add(Glucosagr1);
                RadLinealGaugeGlucometria.Scale.Ranges.Add(Glucosagr2);
                RadLinealGaugeGlucometria.Scale.Ranges.Add(Glucosagr3);
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }

        }

        private void CargarTensionSistolica()
        {
            try
            {
                decimal Sistolica = Convert.ToDecimal(Request.QueryString["Sistolica"]);
                decimal limiteSuperiorSistolica = Convert.ToInt16(Request.QueryString["limiteSuperiorSistolica"]);
                decimal limiteInferiorSistolica = Convert.ToInt16(Request.QueryString["limiteInferiorSistolica"]);
                radialGauge.Pointer.Value = Sistolica;
                radialGauge.Pointer.Color = System.Drawing.Color.Blue;
                radialGauge.Pointer.Cap.Size = (float)0.1;
                radialGauge.Scale.Min = 0;
                radialGauge.Scale.Max = limiteSuperiorSistolica + 20;
                radialGauge.Scale.MinorUnit = (decimal)1;
                radialGauge.Scale.MajorUnit = 10;
                radialGauge.Scale.MinorTicks.Visible = false;
                radialGauge.Scale.MajorTicks.Size = 1;
                radialGauge.Scale.Labels.Visible = true;
                radialGauge.Scale.Labels.Font = "8px Arial,Helvetica,sans-serif";
                radialGauge.Scale.Labels.Color = System.Drawing.Color.Black;
                radialGauge.Scale.Labels.Format = "{0}";
                radialGauge.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;
                GaugeRange Sistolicagr1 = new GaugeRange();
                Sistolicagr1.From = 0;
                Sistolicagr1.To = limiteInferiorSistolica;
                if (limiteInferiorSistolica == 0)
                {
                    Sistolicagr1.Color = System.Drawing.Color.Gray;
                }
                else
                {
                    Sistolicagr1.Color = System.Drawing.Color.Red;
                }
                GaugeRange Sistolicagr2 = new GaugeRange();
                Sistolicagr2.From = (decimal)limiteInferiorSistolica + Convert.ToDecimal(0.1);
                Sistolicagr2.To = (decimal)limiteSuperiorSistolica - Convert.ToDecimal(0.1);
                Sistolicagr2.Color = System.Drawing.Color.Green;
                GaugeRange Sistolicagr3 = new GaugeRange();
                Sistolicagr3.From = (decimal)limiteSuperiorSistolica;
                Sistolicagr3.To = (decimal)limiteSuperiorSistolica + Convert.ToDecimal(50);
                if (limiteSuperiorSistolica == 0)
                {
                    Sistolicagr3.Color = System.Drawing.Color.Gray;
                }
                else
                {
                    Sistolicagr3.Color = System.Drawing.Color.Red;
                }
                radialGauge.Scale.Ranges.Add(Sistolicagr1);
                radialGauge.Scale.Ranges.Add(Sistolicagr2);
                radialGauge.Scale.Ranges.Add(Sistolicagr3);
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }

        }

        private void CargarTensionDiastolica()
        {
            try
            {
                decimal Diastolica = Convert.ToDecimal(Request.QueryString["Diastolica"]);
                decimal limiteSuperiorDiastolica = Convert.ToInt16(Request.QueryString["limiteSuperiorDiastolica"]);
                decimal limiteInferiorDiastolica = Convert.ToInt16(Request.QueryString["limiteInferiorDiastolica"]);
                RadRadialGaugeTensionDiastolica.Pointer.Value = Diastolica;
                RadRadialGaugeTensionDiastolica.Pointer.Color = System.Drawing.Color.Blue;
                RadRadialGaugeTensionDiastolica.Pointer.Cap.Size = (float)0.1;
                RadRadialGaugeTensionDiastolica.Scale.Min = 0;
                RadRadialGaugeTensionDiastolica.Scale.Max = limiteSuperiorDiastolica + 20;
                RadRadialGaugeTensionDiastolica.Scale.MinorUnit = (decimal)1;
                RadRadialGaugeTensionDiastolica.Scale.MajorUnit = 10;
                RadRadialGaugeTensionDiastolica.Scale.MinorTicks.Visible = false;
                RadRadialGaugeTensionDiastolica.Scale.MajorTicks.Size = 1;
                RadRadialGaugeTensionDiastolica.Scale.Labels.Visible = true;
                RadRadialGaugeTensionDiastolica.Scale.Labels.Font = "8px Arial,Helvetica,sans-serif";
                RadRadialGaugeTensionDiastolica.Scale.Labels.Color = System.Drawing.Color.Black;
                RadRadialGaugeTensionDiastolica.Scale.Labels.Format = "{0}";
                RadRadialGaugeTensionDiastolica.Scale.Labels.Position = Telerik.Web.UI.Gauge.ScaleLabelsPosition.Outside;
                GaugeRange Diastolicagr1 = new GaugeRange();
                Diastolicagr1.From = 0;
                Diastolicagr1.To = limiteInferiorDiastolica;
                if (limiteInferiorDiastolica == 0)
                {
                    Diastolicagr1.Color = System.Drawing.Color.Gray;
                }
                else
                {
                    Diastolicagr1.Color = System.Drawing.Color.Red;
                }
                GaugeRange Diastolicagr2 = new GaugeRange();
                Diastolicagr2.From = (decimal)limiteInferiorDiastolica + Convert.ToDecimal(0.1);
                Diastolicagr2.To = (decimal)limiteSuperiorDiastolica - Convert.ToDecimal(0.1);
                Diastolicagr2.Color = System.Drawing.Color.Green;
                GaugeRange Diastolicagr3 = new GaugeRange();
                Diastolicagr3.From = (decimal)limiteSuperiorDiastolica;
                Diastolicagr3.To = (decimal)limiteSuperiorDiastolica + Convert.ToDecimal(50);
                if (limiteSuperiorDiastolica == 0)
                {
                    Diastolicagr3.Color = System.Drawing.Color.Gray;
                }
                else
                {
                    Diastolicagr3.Color = System.Drawing.Color.Red;
                }
                RadRadialGaugeTensionDiastolica.Scale.Ranges.Add(Diastolicagr1);
                RadRadialGaugeTensionDiastolica.Scale.Ranges.Add(Diastolicagr2);
                RadRadialGaugeTensionDiastolica.Scale.Ranges.Add(Diastolicagr3);
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        #endregion Gauges

        #region Grillas

        protected void radGridHospitalizaciones_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                string tipoIdentificacion = Request.QueryString["idTipoIdentificacion"];
                string numeroIdentificacion = Request.QueryString["NumeroIdentifacion"];
                string tipoColmedica = string.Empty;
                if (tipoIdentificacion.Equals("1"))
                {
                    tipoColmedica = "CC";
                }
                if (tipoIdentificacion.Equals("2"))
                {
                    tipoColmedica = "TI";
                }
                if (tipoIdentificacion.Equals("3"))
                {
                    tipoColmedica = "CE";
                }
                if (tipoIdentificacion.Equals("4"))
                {
                    tipoColmedica = "NIT";
                }
                WebClient proxy = new WebClient();
                string serviceURL = ConfigurationManager.AppSettings["URLHOSPITALIZACIONES"] + tipoColmedica + "/" + numeroIdentificacion;
                byte[] _data = proxy.DownloadData(serviceURL);
                Stream _mem = new MemoryStream(_data);
                var reader = new StreamReader(_mem);
                var result = reader.ReadToEnd();
                JArray v = JArray.Parse(result.ToString());
                radGridHospitalizaciones.DataSource = v;
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        protected void RadScheduler1_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            Calendario calendario = (Calendario)e.Appointment.DataItem;
            if (calendario.txt1.Equals("Asignada") && calendario.txt2.Equals("Si"))
                e.Appointment.BackColor = System.Drawing.Color.Green;
            if (calendario.txt1.Equals("Asignada") && calendario.txt2.Equals("No"))
                e.Appointment.BackColor = System.Drawing.Color.BlueViolet;
            if (calendario.txt1.Equals("Cancelada"))
                e.Appointment.BackColor = System.Drawing.Color.Red;
        }

        protected void radGridDiagnosticos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridDiagnosticos.DataSource = negocioPaciente.ConsultaBandejaDiagnostico(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTODIAGNOSTICO, Constantes.TIPOEVENTOOTROSDIAGNOSTICOS);
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        protected void radGridExamenesAyudas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridExamenesAyudas.DataSource = negocioPaciente.ConsultaBandejaInterconsultasAgrupadas(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOEXAMENES, Constantes.TIPOEVENTOOTRASAYUDAS);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void radGridExamenesAyudas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "DetalleExamenes":
                        {
                            string codigo = dataItem.GetDataKeyValue("Codigo").ToString();
                            negocioPaciente = new PacienteNegocio();
                            radGridExamenesAyudas.DataSource = negocioPaciente.ConsultarDetalleBandejaActividades(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOEXAMENES, Constantes.TIPOEVENTOOTRASAYUDAS, codigo);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void radGridMedicamentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridMedicamentos.DataSource = negocioPaciente.ConsultaMedicamentosPaciente(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOMEDICAMENTOS);
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        #endregion Grillas

        #region Checks

        protected void chkReseatGlucosa_CheckedChanged(object sender, EventArgs e)
        {
            string control = Request.Form["__EVENTTARGET"].ToString();
            if (control.Equals("ctl00$MainContent$RadDock8$C$chkReseatGlucosa"))
            {
                rdpFechaInicioGlucosa.Clear();
                rdpFechaFinGlucosa.Clear();
                CargarGraficaTalla();
            }
        }

        protected void chkReseat_CheckedChanged(object sender, EventArgs e)
        {
            string control = Request.Form["__EVENTTARGET"].ToString();
            if (control.Equals("ctl00$MainContent$RadDock7$C$chkReseat"))
            {
                rdpFechaInicio.Clear();
                rdpFecchaFin.Clear();
                CargarGraficaTension();
            }
        }

        #endregion Checks

        #region Combos

        protected void cboHoraTension_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cboHoraTension.SelectedValue != null && rdpFechaInicio.SelectedDate != null && rdpFecchaFin.SelectedDate != null)
                {
                    negocioPaciente = new PacienteNegocio();
                    RadHtmlChartTension.PlotArea.XAxis.DataLabelsField = "Fecha";
                    RadHtmlChartTension.PlotArea.Series[0].DataFieldY = "valor1";
                    RadHtmlChartTension.PlotArea.Series[1].DataFieldY = "valor2";
                    RadHtmlChartTension.DataSource = negocioPaciente.ConsultaGraficaFiltroHora(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOTENSION, Convert.ToDateTime(rdpFechaInicio.SelectedDate), Convert.ToDateTime(rdpFecchaFin.SelectedDate), cboHoraTension.SelectedValue);
                    RadHtmlChartTension.DataBind();

                    PlotBand xAxisPlotBand = new PlotBand();
                    xAxisPlotBand.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorSistolica"]);
                    xAxisPlotBand.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorSistolica"]);
                    xAxisPlotBand.Color = System.Drawing.ColorTranslator.FromHtml("#6453FD");
                    xAxisPlotBand.Alpha = (byte)190;
                    RadHtmlChartTension.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand);

                    PlotBand xAxisPlotBand2 = new PlotBand();
                    xAxisPlotBand2.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorDiastolica"]);
                    xAxisPlotBand2.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorDiastolica"]);
                    xAxisPlotBand2.Color = System.Drawing.ColorTranslator.FromHtml("#FC6969");
                    xAxisPlotBand2.Alpha = (byte)190;
                    RadHtmlChartTension.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand2);
                }
                else
                {
                    RadNotificationMensajes.Show("Por favor escoger primero el rango de fechas");
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        protected void cboRangoGlucosa_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (cboRangoGlucosa.SelectedValue != null && rdpFechaInicioGlucosa.SelectedDate != null && rdpFechaFinGlucosa.SelectedDate != null)
                {
                    negocioPaciente = new PacienteNegocio();
                    RadHtmlChartTalla.PlotArea.XAxis.DataLabelsField = "Fecha";
                    RadHtmlChartTalla.PlotArea.Series[0].DataFieldY = "valor1";
                    RadHtmlChartTalla.PlotArea.Series[1].DataFieldY = "valor2";
                    RadHtmlChartTalla.DataSource = negocioPaciente.ConsultaGraficaFiltroHora(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOGLUCOSA, Convert.ToDateTime(rdpFechaInicioGlucosa.SelectedDate), Convert.ToDateTime(rdpFechaFinGlucosa.SelectedDate), cboRangoGlucosa.SelectedValue);
                    RadHtmlChartTalla.DataBind();
                    PlotBand xAxisPlotBand = new PlotBand();
                    xAxisPlotBand.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorGlucosa"]);
                    xAxisPlotBand.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorGlucosa"]);
                    xAxisPlotBand.Color = System.Drawing.ColorTranslator.FromHtml("#e83737");
                    xAxisPlotBand.Alpha = (byte)190;
                    RadHtmlChartTalla.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand);
                }
                else
                {
                    RadNotificationMensajes.Show("Por favor escoger primero el rango de fechas");
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        #endregion Combos

        #region DatePicker

        protected void rdpFecchaFin_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                if (rdpFechaInicio.SelectedDate != null && rdpFecchaFin.SelectedDate != null)
                {
                    negocioPaciente = new PacienteNegocio();
                    RadHtmlChartTension.PlotArea.XAxis.DataLabelsField = "Fecha";
                    RadHtmlChartTension.PlotArea.Series[0].DataFieldY = "valor1";
                    RadHtmlChartTension.PlotArea.Series[1].DataFieldY = "valor2";
                    RadHtmlChartTension.DataSource = negocioPaciente.ConsultaGraficaFiltroFechas(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOTENSION, Convert.ToDateTime(rdpFechaInicio.SelectedDate), Convert.ToDateTime(rdpFecchaFin.SelectedDate));
                    RadHtmlChartTension.DataBind();

                    PlotBand xAxisPlotBand = new PlotBand();
                    xAxisPlotBand.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorSistolica"]);
                    xAxisPlotBand.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorSistolica"]);
                    xAxisPlotBand.Color = System.Drawing.ColorTranslator.FromHtml("#6453FD");
                    xAxisPlotBand.Alpha = (byte)190;
                    RadHtmlChartTension.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand);

                    PlotBand xAxisPlotBand2 = new PlotBand();
                    xAxisPlotBand2.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorDiastolica"]);
                    xAxisPlotBand2.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorDiastolica"]);
                    xAxisPlotBand2.Color = System.Drawing.ColorTranslator.FromHtml("#FC6969");
                    xAxisPlotBand2.Alpha = (byte)190;
                    RadHtmlChartTension.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand2);
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        protected void rdpFechaFinGlucosa_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            try
            {
                if (rdpFechaInicioGlucosa.SelectedDate != null && rdpFechaFinGlucosa.SelectedDate != null)
                {
                    negocioPaciente = new PacienteNegocio();
                    RadHtmlChartTalla.PlotArea.XAxis.DataLabelsField = "Fecha";
                    RadHtmlChartTalla.PlotArea.Series[0].DataFieldY = "valor1";
                    RadHtmlChartTalla.DataSource = negocioPaciente.ConsultaGraficaFiltroFechas(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOGLUCOSA, Convert.ToDateTime(rdpFechaInicioGlucosa.SelectedDate), Convert.ToDateTime(rdpFechaFinGlucosa.SelectedDate));
                    RadHtmlChartTalla.DataBind();
                    PlotBand xAxisPlotBand = new PlotBand();
                    xAxisPlotBand.From = (decimal?)Convert.ToInt16(Request.QueryString["limiteInferiorGlucosa"]);
                    xAxisPlotBand.To = (decimal?)Convert.ToInt16(Request.QueryString["limiteSuperiorGlucosa"]);
                    xAxisPlotBand.Color = System.Drawing.ColorTranslator.FromHtml("#e83737");
                    xAxisPlotBand.Alpha = (byte)190;
                    RadHtmlChartTalla.PlotArea.YAxis.PlotBands.Add(xAxisPlotBand);
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        #endregion DatePicker

        private void CargarCalendario()
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                RadScheduler1.DataSource = negocioPaciente.ConsultarCalendarioPaciente(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOCITASMEDICAS);
                RadScheduler1.DataBind();
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        private void CargarDatosLabel()
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                persona = new sm_Persona();
                persona = negocioPaciente.ConsultarPersona(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"]);
                lblNombres.Text = persona.primerNombre + " " + persona.segundoNombre;
                lblApellidos.Text = persona.primerApellido + " " + persona.segundoApellido;
                lblEdad.Text = "Edad:    " + persona.Edad.ToString();
                lblCelular.Text = "Celular:    " + persona.celular;
                lblFechaGlucosa.Text = Request.QueryString["FechaGlucosa"];
                lblDiastolica.Text = Request.QueryString["FechaDiastolica"];
                lblSistolica.Text = Request.QueryString["FechaSistolica"];
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

    }
}