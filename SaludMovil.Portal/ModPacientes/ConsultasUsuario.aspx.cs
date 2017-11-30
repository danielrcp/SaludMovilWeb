using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SaludMovil.Negocio;
using SaludMovil.Entidades;
using System.Globalization;

namespace SaludMovil.Portal.ModPacientes
{
    public partial class ConsultasUsuario : System.Web.UI.Page
    {
        private PacienteNegocio negocioPaciente;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                if (!Page.IsPostBack)
                    CargarGraficaHitorica();
            }
            catch (Exception ex)
            {
                //TODO: Implemetar el manejador de mensajes
            }
        }

        private void CargarGraficaHitorica()
        {
            Persona persona = (Persona)Session["Persona"];
            int idTipoIdentificacion = persona.idTipoIdentificacion;
            string numeroIdentificacion = persona.numeroIdentificacion;
            numeroIdentificacion = "5201889999";
            //Glucosa
            generarGrafica(idTipoIdentificacion, numeroIdentificacion, 1, "Glucosa", "mg/dL", "NA");
            //Peso
            generarGrafica(idTipoIdentificacion, numeroIdentificacion, 5, "Peso", "Kg", "NA");
            //Presion arterial
            generarGrafica(idTipoIdentificacion, numeroIdentificacion, 4, "Presion Arterial", "Sistólica", "Diastólica");
        }

        /// <summary>
        /// Metodo que genera las graficas dinamicamente con los parametros recibidos
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <param name="tipoEvento"></param>
        /// <param name="programa"></param>
        /// <param name="nombreSerie1"></param>
        /// <param name="nombreSerie2"></param>
        private void generarGrafica(int idTipoIdentificacion, string numeroIdentificacion, int tipoEvento, string programa, string nombreSerie1,
            string nombreSerie2)
        {
            try
            {
                string titulo = "";
                RadHtmlChart grafica = new RadHtmlChart();
                grafica.ID = "grafica" + programa;
                grafica.Width = Unit.Percentage(100);
                grafica.Height = Unit.Pixel(500);
                //grafica.Layout = Telerik.Web.UI.HtmlChart.ChartLayout.Stock;
                IList<MedidasPaciente> listaMediciones = negocioPaciente.obtenerDatosLecturas(idTipoIdentificacion, numeroIdentificacion, tipoEvento);
                titulo = "Última medición " + nombreSerie1 + ": " + listaMediciones[listaMediciones.Count - 1].valor1;
                grafica.PlotArea.XAxis.TitleAppearance.Text = "Fecha medición";
                grafica.PlotArea.YAxis.TitleAppearance.Text = nombreSerie1;
                LineSeries serie = new LineSeries();
                serie.DataFieldY = "valor1";
                serie.Name = nombreSerie1;
                serie.TooltipsAppearance.DataFormatString = "Resultado medición: {0} " + nombreSerie1 + " {1}";                
                grafica.PlotArea.Series.Add(serie);
                if (programa.Equals("Presion Arterial"))
                {
                    titulo += " - " + nombreSerie2 + ": " + listaMediciones[listaMediciones.Count - 1].valor2;
                    LineSeries serie2 = new LineSeries();
                    serie2.DataFieldY = "valor2";
                    serie2.Name = nombreSerie2;
                    serie2.TooltipsAppearance.DataFormatString = "Resultado medición: {0} " + nombreSerie2;
                    grafica.PlotArea.Series.Add(serie2);
                }
                grafica.PlotArea.XAxis.DataLabelsField = "fechaCadena";
                grafica.PlotArea.XAxis.StartAngle = 90;
                grafica.PlotArea.XAxis.LabelsAppearance.RotationAngle = 45;
                grafica.PlotArea.XAxis.TitleAppearance.TextStyle.FontSize = Unit.Pixel(50);
                grafica.PlotArea.YAxis.TitleAppearance.TextStyle.FontSize = Unit.Pixel(50);
                grafica.ChartTitle.Text = "HISTÓRICO MEDICIONES " + programa.ToUpper();
                grafica.DataSource = listaMediciones;
                grafica.Pan.Enabled = true;
                grafica.Zoom.Enabled = true;
                grafica.Zoom.MouseWheel.Enabled = true;
                grafica.Zoom.MouseWheel.Lock = Telerik.Web.UI.HtmlChart.AxisLock.Y;
                //grafica.Navigator.Visible = true;
                //grafica.Navigator.RangeSelector.From = DateTime.ParseExact(listaMediciones.FirstOrDefault<MedidasPaciente>().fechaCadena, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //grafica.Navigator.RangeSelector.To = DateTime.ParseExact(listaMediciones.Last<MedidasPaciente>().fechaCadena, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //AreaSeries areaSerie = new AreaSeries();
                //areaSerie.DataFieldY = "fechaCadena";                
                //grafica.Navigator.Series.Add(areaSerie);
                grafica.DataBind();
                Literal tituloEncabezado = new Literal();
                tituloEncabezado.Text = "<h1 class=@\"pull-right@\">" + titulo + "</h1>";
                HtmlChartHolder.Controls.Add(tituloEncabezado); 
                HtmlChartHolder.Controls.Add(grafica);
                HtmlChartHolder.Controls.Add(new LiteralControl("<br />"));
            }
            catch (Exception ex)
            {
                //TODO: Implemetar el manejador de mensajes
            }
        }
    }
}