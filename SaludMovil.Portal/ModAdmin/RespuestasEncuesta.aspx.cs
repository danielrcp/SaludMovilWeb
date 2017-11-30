using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SaludMovil.Portal.ModAdmin
{
    public partial class RespuestasEncuesta : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                Div1.Visible = false;
            }
        }

        public void ConsultarEncuestas(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
            //int idEncuesta = Convert.ToInt32(hdfEncuestaActual.Value);
            string Command =    "select pr.numeroIdentificacion, pr.idTipoIdentificacion, pr.idEncuesta, primerNombre, primerApellido, estadoEncuesta, tema, fechaEstado" +
                                " from sm_ProgramacionEncuestas pr left join sm_Persona pe on pe.idTipoIdentificacion = pr.idTipoIdentificacion and "+
                                "pe.numeroIdentificacion = pr.numeroIdentificacion join sm_Encuesta en on pr.idEncuesta=en.idEncuesta";
            
            using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
            {
                DataTable dtResult = new DataTable();
                myDataAdapter.Fill(dtResult);
                respuestas.DataSource = dtResult;
            }   

        }

        protected void mostrarRespuestas(int tipoIdentificacion, string identificacion, int encuesta) 
        {
            LinkButton1.Visible = true;
            respuestas.Visible = false;
            Div1.Visible = true;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
            string Command = "select nombrePregunta, r.respuestaTexto,o.enunciadoPregunta, enc.tema, p.idPregunta "+
                "from sm_Pregunta p join sm_Respuesta r on p.idPregunta = r.idPregunta "+
                "left join sm_OpcionPregunta o on r.idOpcion = o.idOpcionPregunta "+
                "join sm_Encuesta enc on p.idEncuesta=enc.idEncuesta  "+
                "where p.idEncuesta = " + encuesta + " and r.idTipoIdentificacion = " + tipoIdentificacion + " and r.numeroIdentificacion=" + identificacion + " order by ordenPregunta asc";

            using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
            {
                int idPreguntaAnterior = -1;
                DataTable dtResult = new DataTable();
                myDataAdapter.Fill(dtResult);
                string tema = (string)dtResult.Rows[0]["tema"];
                temaEncuesta.Text = tema;
                bool hayRespuesta = false;
                int iteracion = 0;
                int totalPreguntas = dtResult.Rows.Count;
                foreach (DataRow row in dtResult.Rows)
                {
                    iteracion++;
                    int idPregunta = Convert.ToInt32(row["idPregunta"]);
                    if (idPreguntaAnterior != idPregunta)
                    {
                        if (!hayRespuesta && idPreguntaAnterior != -1)
                        {
                            Label sinRespuesta = new Label();
                            sinRespuesta.Text = "&nbsp &nbsp • No respondida";
                            pregunta.Controls.Add(sinRespuesta);
                        }

                        if (idPreguntaAnterior != -1 )
                        {
                            pregunta.Controls.Add(new LiteralControl("<hr />"));
                        }
                        
                        hayRespuesta = false;
                        idPreguntaAnterior = idPregunta;
                        Label nombrePregunta = new Label();
                        nombrePregunta.Text = row["nombrePregunta"].ToString();
                        nombrePregunta.Attributes["class"] = "lblPregunta";
                        pregunta.Controls.Add(nombrePregunta);
                        pregunta.Controls.Add(new LiteralControl("<br />"));
                    }
                    Label respuesta = new Label();
                    string respuestTexto = row["enunciadoPregunta"].ToString() == "" ? row["respuestaTexto"].ToString() : row["enunciadoPregunta"].ToString();
                    hayRespuesta = respuestTexto != "" ? true : hayRespuesta;
                    respuesta.Text = "&nbsp &nbsp • " + respuestTexto;
                    if (respuestTexto != "")
                    {
                        pregunta.Controls.Add(respuesta);
                        pregunta.Controls.Add(new LiteralControl("<br />"));
                    }
                    if(!hayRespuesta && (iteracion==totalPreguntas))
                    {
                        Label sinRespuesta = new Label();
                        sinRespuesta.Text = "&nbsp &nbsp • No respondida";
                        pregunta.Controls.Add(sinRespuesta);
                    }
                }
            }   
        }

        protected void Respuestas_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "verRespuesta")
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (item["estadoEncuesta"].Text != "respondida")
                {
                    RadWindowManager1.RadAlert("Encuesta aún no respondida", 330, 180, "Alerta", "", "");
                }
                else 
                {
                    Div1.Visible = true;
                    string identificacion = item["numeroIdentificacion"].Text;
                    int tipoidentificacion = Convert.ToInt32(item["idTipoIdentificacion"].Text);
                    int encuesta = Convert.ToInt32(item["idEncuesta"].Text);
                    mostrarRespuestas(tipoidentificacion, identificacion, encuesta);
                }
                
            }
        }

        protected void atras(object sender, EventArgs e)
        {
            pregunta.Controls.Clear();
            respuestas.Visible = true;
            Div1.Visible = false;
            LinkButton1.Visible = false;
        } 

    }
}