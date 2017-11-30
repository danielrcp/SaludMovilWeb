using SaludMovil.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SaludMovil.Portal.ModAdmin
{
    public partial class Encuesta : System.Web.UI.Page
    {
        int totalPreguntas;
        Persona persona = null;
        Label[] labels;
        CheckBoxList[] checks;
        RadioButtonList[] radios;
        TextBox[] text;
        string encuestaQuery;
        string usuario;
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            persona = (Persona)Session["persona"];
            usuario = Request["usu"];
            encuestaQuery = Request["enc"];

            if (!Page.IsPostBack)
            {
               
            }
        }

        protected void preguntas_Init(object sender, EventArgs e)
        {
            usuario = Request["usu"];
            encuestaQuery = Request["enc"];
            int radioIndex = 0;
            int textIndex = 0;
            int checkIndex = 0;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
            string Command1 = "select tema, idPregunta, nombrePregunta, p.idTipoPregunta, esObligatoria, idOpcionDepende from sm_Encuesta e JOIN sm_Pregunta p ON e.idEncuesta = p.idEncuesta JOIN sm_TipoPregunta t ON t.idTipoPregunta = p.idTipoPregunta where e.idEncuesta = "+encuestaQuery;//
            using (SqlDataAdapter AdapterPreguntas = new SqlDataAdapter(Command1, con))
            {   
                DataTable dtResult = new DataTable();
                AdapterPreguntas.Fill(dtResult);
                int preguntasTotales = dtResult.Rows.Count;
                totalPreguntas = preguntasTotales;
                string Command2 = "select * from sm_OpcionPregunta where idEncuesta ='" + encuestaQuery + "'";
                using (SqlDataAdapter AdapterOpciones = new SqlDataAdapter(Command2, con))
                {
                    int checkTotales = dtResult.Select("idTipoPregunta = '2'").Length;
                    int radioTotales = dtResult.Select("idTipoPregunta = '1'").Length;
                    int textTotales = preguntasTotales-(checkTotales+radioTotales);
                    labels = new Label[preguntasTotales];
                    checks = new CheckBoxList[checkTotales];
                    radios = new RadioButtonList[radioTotales];
                    text = new TextBox[textTotales];
                    DataTable dtOpciones = new DataTable();
                    AdapterOpciones.Fill(dtOpciones);
                    temaEncuesta.Text = dtResult.Rows[0].Field<string>(0);
                    foreach (DataRow row in dtResult.Rows)
                    {
                        int index = dtResult.Rows.IndexOf(row);
                        labels[index] = new Label();
                        Label obligatoria = new Label();
                        obligatoria.Text = " *";
                        obligatoria.Attributes.Add("style", "font-size:22px");
                        obligatoria.Attributes["class"] = "text-danger";
                        //Label enunciado = new Label();
                        labels[index].Text = row["nombrePregunta"].ToString();
                        labels[index].Attributes["class"] = "lblPregunta";
                        pregunta.Controls.Add(labels[index]);
                        if((bool)row["esObligatoria"]== true){
                             pregunta.Controls.Add(obligatoria);
                        }
                        string tipo = row["idTipoPregunta"].ToString();
                        switch (tipo)
                        {
                            case "1":
                                radios[radioIndex] = new RadioButtonList();
                                radios[radioIndex].ID =row["idPregunta"].ToString();
                                pregunta.Controls.Add(radios[radioIndex]);
                                foreach (DataRow fila in dtOpciones.Rows)
                                {
                                    string preguntaId = row["idPregunta"].ToString();
                                    string opcionId = fila["idPregunta"].ToString();
                                    if (preguntaId == opcionId)
                                    {
                                        ListItem opcionItem = new ListItem();
                                        opcionItem.Value = fila["idOpcionPregunta"].ToString();
                                        opcionItem.Text = fila["enunciadoPregunta"].ToString();
                                        opcionItem.Attributes["class"]= "chbxEncuesta";
                                        radios[radioIndex].Items.Add(opcionItem);         
                                    }   
                                }
                                radioIndex++;
                                break;
                            case "2":
                                checks[checkIndex] = new CheckBoxList();
                                //CheckBoxList check = new CheckBoxList();
                                checks[checkIndex].ID =row["idPregunta"].ToString();
                                pregunta.Controls.Add(checks[checkIndex]);
                                foreach (DataRow fila in dtOpciones.Rows)
                                {
                                    string preguntaId = row["idPregunta"].ToString();
                                    string opcionId = fila["idPregunta"].ToString();
                                    if (preguntaId == opcionId)
                                    {
                                        ListItem opcionItem = new ListItem();
                                        opcionItem.Value = fila["idOpcionPregunta"].ToString();
                                        opcionItem.Text = fila["enunciadoPregunta"].ToString();
                                        opcionItem.Attributes["class"]= "chbxEncuesta";
                                        checks[checkIndex].Items.Add(opcionItem);         
                                    }   
                                }
                                checkIndex++;
                                break;
                            case "3":
                                pregunta.Controls.Add(new LiteralControl("<br />"));
                                pregunta.Controls.Add(new LiteralControl("<br />"));
                                text[textIndex] = new TextBox();
                                TextBox opcion = new TextBox();
                                text[textIndex].ID = row["idPregunta"].ToString();
                                pregunta.Controls.Add(text[textIndex]);
                                textIndex++;
                                break;
                        }

                        pregunta.Controls.Add(new LiteralControl("<hr />"));
                    }
                }
            }
        }

        protected void EnviarEncuesta(object sender, EventArgs e)
        {
            usuario = Request["usu"];
            encuestaQuery = Request["enc"];
             try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                string stmt = "INSERT INTO dbo.sm_Respuesta(idPregunta, numeroIdentificacion, idOpcion, respuestaTexto, respuestaNumero, respuestaSiNo, idTipoIdentificacion) VALUES(@idPregunta, @idUsuario, @idOpcion, @respuestaTexto, @respuestaNumero, @respuestaSiNo, @idTipoIdentificacion);";
                SqlCommand cmd = new SqlCommand(stmt, con);

                cmd.Parameters.Add("@idPregunta", SqlDbType.Int);
                cmd.Parameters.Add("@idUsuario", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@idOpcion", SqlDbType.Int);
                cmd.Parameters.Add("@respuestaTexto", SqlDbType.VarChar, 510);
                cmd.Parameters.Add("@respuestaNumero", SqlDbType.Int);
                cmd.Parameters.Add("@respuestaSiNo", SqlDbType.Bit);
                cmd.Parameters.Add("@idTipoIdentificacion", SqlDbType.Int);

                try
                {
                    con.Open();
                    foreach (CheckBoxList check in checks)
                    {
                        foreach (ListItem cb in check.Items)
                        {
                            if (cb.Selected)
                            {
                                //myList.Add(cb.Value);
                                cmd.Parameters["@idPregunta"].Value = check.ID;
                                cmd.Parameters["@idUsuario"].Value = persona.numeroIdentificacion;
                                cmd.Parameters["@idOpcion"].Value = cb.Value;
                                cmd.Parameters["@respuestaTexto"].Value = DBNull.Value;
                                cmd.Parameters["@respuestaNumero"].Value = DBNull.Value;
                                cmd.Parameters["@respuestaSiNo"].Value = DBNull.Value;
                                cmd.Parameters["@idTipoIdentificacion"].Value = persona.idTipoIdentificacion;
                                
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
                finally
                {
                    con.Close();
                }

                try
                {
                    con.Open();
                    foreach (TextBox textIn in text)
                    {
                        cmd.Parameters["@idPregunta"].Value = textIn.ID;
                        cmd.Parameters["@idUsuario"].Value = persona.numeroIdentificacion;
                        cmd.Parameters["@idOpcion"].Value = DBNull.Value;
                        cmd.Parameters["@respuestaTexto"].Value = textIn.Text;
                        cmd.Parameters["@respuestaNumero"].Value = DBNull.Value;
                        cmd.Parameters["@respuestaSiNo"].Value = DBNull.Value;
                        cmd.Parameters["@idTipoIdentificacion"].Value = persona.idTipoIdentificacion;
                        
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
                finally
                {
                    con.Close();
                }

                try
                {
                    con.Open();
                    foreach (RadioButtonList radio in radios)
                    {
                        cmd.Parameters["@idPregunta"].Value = radio.ID;
                        cmd.Parameters["@idUsuario"].Value = persona.numeroIdentificacion;
                        cmd.Parameters["@idOpcion"].Value = radio.SelectedValue;
                        cmd.Parameters["@respuestaTexto"].Value = DBNull.Value;
                        cmd.Parameters["@respuestaNumero"].Value = DBNull.Value;
                        cmd.Parameters["@respuestaSiNo"].Value = DBNull.Value;
                        cmd.Parameters["@idTipoIdentificacion"].Value = persona.idTipoIdentificacion;
                        
                        cmd.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                
             }
             catch (Exception ex)
             {
                 string error = ex.Message;
             }
             finally
             {
                 con.Close();
             }
        }
    }
}