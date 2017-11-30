using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SaludMovil.Portal.ModAdmin
{
    public partial class ParamEncuestas : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                hayRespondidas.Value = "0";
                hdfEncuestaActual.Value = "0";
                cargarEncuesta();
                CargarProgramas();
                CargarEncuestasProgramadas();
            }
        }

        #region  // carga todos los programas en el apartado de asignar encuestas a los programas

        protected void CargarProgramas() 
        {
            if (hdfEncuestaActual.Value == "0" || hdfEncuestaActual.Value == "")
            {
                programas.Visible = false;
            }
            else
            {
                programas.Visible = true;
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                string Command = "select idPrograma, descripcion from sm_Programa";
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
                {
                    DataTable dtResult = new DataTable();
                    myDataAdapter.Fill(dtResult);
                    programas.DataSource = dtResult;
                    programas.DataTextField = "descripcion";
                    programas.DataKeyField = "idPrograma";
                    programas.DataBind();
                }
            }
        }
        #endregion

        #region  // carga todos los programas que ya han sido asignados a la encuesta actual

        protected void CargarEncuestasProgramadas()
        {
            if (hdfEncuestaActual.Value == "0" || hdfEncuestaActual.Value == "")
            {
                asignados.Visible = false;
            }
            else
            {
                asignados.Visible = true;
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                string Command = " select programaAsignado, max(descripcion) as descripcion  from sm_ProgramacionEncuestas join sm_Programa on programaAsignado = idPrograma where idencuesta = " + hdfEncuestaActual.Value + " group by ProgramaAsignado,idPrograma,programaAsignado";
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
                {
                    DataTable dtResult = new DataTable();
                    myDataAdapter.Fill(dtResult);
                    asignados.DataSource = dtResult;
                    asignados.DataTextField = "descripcion";
                    asignados.DataKeyField = "programaAsignado";
                    asignados.DataBind();
                }
            }
        }
        #endregion

        protected void AsignarEncuesta(object sender, RadListBoxTransferringEventArgs e )
        {
            if (hdfEncuestaActual.Value != "0")
            {
                RadListBoxItem itemAsignado = programas.SelectedItem;
                int idPrograma = Convert.ToInt32(itemAsignado.DataKey);
                try
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                    string stmt = "INSERT INTO sm_programacionEncuestas(idEncuesta, numeroIdentificacion, idTipoIdentificacion, estadoEncuesta, fechaEstado, programaAsignado) " +
                    "select " + hdfEncuestaActual.Value + ", numeroIdentificacion, idTipoIdentificacion,'programada', GETDATE(), " + idPrograma + " from sm_pacientePrograma where idPrograma =" + idPrograma;
                    SqlCommand cmd = new SqlCommand(stmt, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
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
            else 
            { 
                
            }
        }

        protected void DesasignarEncuesta(object sender, RadListBoxDeletingEventArgs e)
        {
            RadListBoxItem itemDesasignado = asignados.SelectedItem;
            int idPrograma = Convert.ToInt32(itemDesasignado.DataKey);
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                string stmt = "delete from sm_ProgramacionEncuestas where idEncuesta =" + hdfEncuestaActual.Value + " and programaAsignado = " + idPrograma +"and estadoEncuesta = 'programada'";
                SqlCommand cmd = new SqlCommand(stmt, con);

                con.Open();
                cmd.ExecuteNonQuery();
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

        protected void GuardarEncuesta(object sender, EventArgs e)
        {
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                string temaVar = tema.Text;
                string stmt;
                //string idCombo = RadDropDownList2.SelectedValue.ToString();
                //bool activo = chkActivo.Checked;
                if (hdfEncuestaActual.Value == "0")
                {
                    stmt = "INSERT INTO dbo.sm_Encuesta(tema, fechaInicio, fechaFin) VALUES(@Tema, @FechaInicio, @FechaFin);";
                    hayRespondidas.Value = "0";
                }
                else
                {
                    stmt = "update sm_Encuesta set tema = @Tema, fechaInicio = @FechaInicio, fechaFin = @FechaFin where IdEncuesta = @IdEncuesta";
                }
                SqlCommand cmd = new SqlCommand(stmt, con);
                cmd.Parameters.Add("@IdEncuesta", SqlDbType.Int);
                cmd.Parameters.Add("@Tema", SqlDbType.VarChar, 510);
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime);
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime);
                //cmd.Parameters["@Nombre"].Value = nombre;
                cmd.Parameters["@IdEncuesta"].Value =  hdfEncuestaActual.Value.ToString();
                cmd.Parameters["@Tema"].Value = temaVar;
                cmd.Parameters["@FechaInicio"].Value = fechaInicial.SelectedDate;
                cmd.Parameters["@FechaFin"].Value = fechaFinal.SelectedDate;
                con.Open();
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "select idEncuesta from sm_Encuesta where idEncuesta =  (SELECT MAX(idEncuesta) FROM sm_Encuesta)";
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                var returnValue = cmd1.ExecuteScalar();
                con.Close();
                hdfEncuestaActual.Value = returnValue.ToString();

            }
            catch (Exception ex) 
            {
                string error = ex.Message;
            }
            finally
            {
                con.Close();
                //SeleccionaEncuesta(sender,e);
                cargarEncuesta();
                CargarProgramas();
                CargarEncuestasProgramadas();
            }
        }

        protected void cargarEncuesta()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
            string Command = "select idEncuesta, tema from sm_Encuesta";
            using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
            {
                DataTable dtResult = new DataTable();
                myDataAdapter.Fill(dtResult);
                cboListaEncuestas.DataSource = dtResult;
                cboListaEncuestas.DataTextField = "tema";
                cboListaEncuestas.DataValueField = "idEncuesta";
                cboListaEncuestas.DataBind();
                cboListaEncuestas.Items.Insert(0, new ListItem("Nueva encuesta", "0"));
                cboListaEncuestas.SelectedValue = hdfEncuestaActual.Value;
                preguntas.Rebind();
            }
        }

        protected void ConsultarPreguntas(object source, GridNeedDataSourceEventArgs e)
        {
            if (hdfEncuestaActual.Value == "0" || hdfEncuestaActual.Value == "")
            {
                preguntas.Visible = false;
            }
            else
            {
                preguntas.Visible = true;
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                int idEncuesta = Convert.ToInt32(hdfEncuestaActual.Value);
                    if (hayRespondidas.Value != "0")
                    {
                        preguntas.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                    }
                    else {
                        preguntas.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = true;
                    }
                string Command = "select idPregunta,nombrePregunta, sm_Pregunta.idTipoPregunta, esObligatoria, tipoPregunta, ordenPregunta from sm_Pregunta INNER JOIN sm_TipoPregunta ON sm_TipoPregunta.idTipoPregunta=sm_Pregunta.idTipoPregunta  where idEncuesta = '" + idEncuesta + "' order by ordenPregunta asc";
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
                {
                    DataTable dtResult = new DataTable();
                    myDataAdapter.Fill(dtResult);
                    preguntas.DataSource = dtResult;
                }
            }
        }

        protected void ConsultarTipos(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
            string Command = "select * from sm_TipoPregunta;";
            using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
            {
                DataTable dtResult = new DataTable();
                myDataAdapter.Fill(dtResult);
                RadComboBox tipoCombo = (RadComboBox)sender;
                tipoCombo.DataSource = dtResult;
                tipoCombo.DataValueField = "idTipoPregunta";
                tipoCombo.DataTextField = "tipoPregunta";
                tipoCombo.DataBind();
            }

        }

        protected void GuardaPregunta(object source, GridCommandEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;

            var editableItem = ((GridEditableItem)e.Item);
            Hashtable values = new Hashtable();
            editableItem.ExtractValues(values);
            String enunciado = (string)values["nombrePregunta"];
            RadComboBox cboTipo = (RadComboBox)item.FindControl("TipoP");
            int tipo = Convert.ToInt16(cboTipo.SelectedValue);
            bool obligatoria = (bool)values["esObligatoria"];
            try
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                string stmt = "select MAX(ordenPregunta) from sm_Pregunta where idEncuesta = " + hdfEncuestaActual.Value;
                SqlCommand cmd = new SqlCommand(stmt, con);
                con.Open();
                int orden = 0;
                object result = cmd.ExecuteScalar();
                string ordenDb = result.ToString();
                if (ordenDb != "")
                {
                    orden = Convert.ToInt32(result);
                }
                stmt = "INSERT INTO dbo.sm_Pregunta(nombrePregunta, idTipoPregunta, idEncuesta, esObligatoria, ordenPregunta) VALUES(@Enunciado, @Tipo, @Encuesta, @Obligatoria, @ordenPregunta);";
                cmd.CommandText = stmt;

                cmd.Parameters.Add("@Enunciado", SqlDbType.VarChar, 510);
                cmd.Parameters.Add("@Tipo", SqlDbType.Int);
                cmd.Parameters.Add("@Encuesta", SqlDbType.Int);
                cmd.Parameters.Add("@Obligatoria", SqlDbType.VarChar, 510);
                cmd.Parameters.Add("@OrdenPregunta", SqlDbType.Int);

                cmd.Parameters["@Enunciado"].Value = enunciado;
                cmd.Parameters["@Tipo"].Value = tipo;
                cmd.Parameters["@Encuesta"].Value = hdfEncuestaActual.Value;
                cmd.Parameters["@Obligatoria"].Value = obligatoria;
                cmd.Parameters["@OrdenPregunta"].Value = orden+1;

                cmd.ExecuteNonQuery();
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

        protected void EditarPregunta(object source, GridCommandEventArgs e)
        {
            if (hayRespondidas.Value != "0")
            {
                RadWindowManager1.RadAlert("Esta encuesta no puede ser editada debido a que fue respondida al menos por un usuario", 330, 180, "Alerta", "", "");
            }
            else
            {
                GridEditableItem item = (GridEditableItem)e.Item;

                var editableItem = ((GridEditableItem)e.Item);
                Hashtable values = new Hashtable();
                editableItem.ExtractValues(values);
                String enunciado = (string)values["nombrePregunta"];

                RadComboBox cboTipo = (RadComboBox)item.FindControl("TipoP");
                int tipo = Convert.ToInt16(cboTipo.SelectedValue);
                bool obligatoria = (bool)values["esObligatoria"];
                int id = Convert.ToInt32(values["idPregunta"]);

                try
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                    string stmt = "UPDATE dbo.sm_Pregunta SET nombrePregunta = @Enunciado, idTipoPregunta = @Tipo, idEncuesta = @Encuesta, esObligatoria = @Obligatoria where idPregunta= " + id;
                    SqlCommand cmd = new SqlCommand(stmt, con);

                    cmd.Parameters.Add("@Enunciado", SqlDbType.VarChar, 510);
                    cmd.Parameters.Add("@Tipo", SqlDbType.Int);
                    cmd.Parameters.Add("@Encuesta", SqlDbType.Int);
                    cmd.Parameters.Add("@Obligatoria", SqlDbType.VarChar, 510);

                    cmd.Parameters["@Enunciado"].Value = enunciado;
                    cmd.Parameters["@Tipo"].Value = tipo;
                    cmd.Parameters["@Encuesta"].Value = hdfEncuestaActual.Value;
                    cmd.Parameters["@Obligatoria"].Value = obligatoria;
                    con.Open();
                    cmd.ExecuteNonQuery();
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

        protected void EliminarPregunta(object source, GridCommandEventArgs e)
        {
            if (hayRespondidas.Value != "0")
            {
                RadWindowManager1.RadAlert("Esta encuesta no puede ser aliminada debido a que fue respondida al menos por un usuario", 330, 180, "Alerta", "", "");
            }
            else
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                var editableItem = ((GridEditableItem)e.Item);
                Hashtable values = new Hashtable();
                editableItem.ExtractValues(values);
                int id = Convert.ToInt32(values["idPregunta"]);
                int orden = Convert.ToInt32(values["ordenPregunta"]);

                try
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                    string stmt = "delete from sm_Pregunta where idPregunta = " + id;
                    string stmt1 = "delete from sm_OpcionPregunta where idPregunta = " + id;
                    string stmt2 = "update sm_Pregunta set ordenPregunta = ordenPregunta-1 where idEncuesta = " + hdfEncuestaActual.Value + " and ordenPregunta >= " + orden;
                    SqlCommand cmd = new SqlCommand(stmt, con);
                    SqlCommand cmd1 = new SqlCommand(stmt1, con);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
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

        protected void SeleccionaEncuesta(object sender, EventArgs e)
        {
            hdfEncuestaActual.Value = cboListaEncuestas.SelectedValue.ToString();
            if (hdfEncuestaActual.Value != "0")
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();

                string Command1 = "select count(estadoEncuesta) hayRespuesta  from sm_ProgramacionEncuestas where idEncuesta = " + hdfEncuestaActual.Value + " and estadoEncuesta= 'respondida'";
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command1, con))
                {
                    DataTable dtResult = new DataTable();
                    myDataAdapter.Fill(dtResult);
                    hayRespondidas.Value = Convert.ToString(dtResult.Rows[0]["hayRespuesta"]);
                }
                
                string Command = "select fechainicio, fechafin, tema from sm_Encuesta where idEncuesta = '" + cboListaEncuestas.SelectedValue.ToString() + "'";
                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
                {
                    DataTable dtResult = new DataTable();
                    myDataAdapter.Fill(dtResult);
                    tema.Text = dtResult.Rows[0].Field<string>(2);
                    //DateTime pruebafecha = dtResult.Rows[0].Field<string>(0);
                    //DateTime myDate = DateTime.ParseExact("2009-05-08 14:40:52,531", "dd-mm-yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                    //DateTime pruebafecha = dtResult.Rows[0].Field<DateTime>(0);
                }
                preguntas.Rebind();
            }
            else
            {
                //preguntas.DataSource = new string[] { };
                tema.Text = "";
            }
            //    preguntas.Rebind();
                //hdfEncuestaActual.Value = hdfEncuestaActual.Value;
            cargarEncuesta();
            CargarProgramas();
            CargarEncuestasProgramadas();

        }

        protected void preguntas_ItemCommand(object sender, GridCommandEventArgs e)
        {
            string itemNombreGrilla = e.Item.OwnerTableView.Name;
            if (hayRespondidas.Value != "0")
            {
                RadWindowManager1.RadAlert("Esta encuesta no puede ser editada debido a que fue respondida al menos por un usuario", 330, 180, "Alerta", "", "");
            }
            else
            {
                if (itemNombreGrilla.Equals("Opciones"))
                {
                    hdfEncuestaActual.Value = hdfEncuestaActual.Value;
                    switch (e.CommandName)
                    {
                        case "PerformInsert":
                            hdfEncuestaActual.Value = hdfEncuestaActual.Value;
                            var preguntaItem = e.Item.OwnerTableView.ParentItem;
                            var editableItem = ((GridEditableItem)e.Item);
                            Hashtable values = new Hashtable();
                            Hashtable valuesParent = new Hashtable();
                            preguntaItem.ExtractValues(valuesParent);
                            editableItem.ExtractValues(values);
                            int idPregunta = Convert.ToInt32(valuesParent["idPregunta"]);
                            string opcion = (string)values["enunciadoPregunta"];
                            string indice = (string)values["indiceOpcion"];
                            try
                            {
                                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                                string stmt = "INSERT INTO dbo.sm_OpcionPregunta(enunciadoPregunta, idPregunta, indiceOpcion, idEncuesta) VALUES(@Enunciado, @Pregunta, @Indice, @Encuesta);";
                                SqlCommand cmd = new SqlCommand(stmt, con);

                                cmd.Parameters.Add("@Enunciado", SqlDbType.VarChar, 510);
                                cmd.Parameters.Add("@Pregunta", SqlDbType.Int);
                                cmd.Parameters.Add("@Indice", SqlDbType.VarChar, 50);
                                cmd.Parameters.Add("@Encuesta", SqlDbType.Int);

                                cmd.Parameters["@Enunciado"].Value = opcion;
                                cmd.Parameters["@Pregunta"].Value = idPregunta;
                                cmd.Parameters["@Indice"].Value = 'a';
                                cmd.Parameters["@Encuesta"].Value = hdfEncuestaActual.Value;
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                string error = ex.Message;
                            }
                            finally
                            {
                                con.Close();
                            }
                            break;
                        case "Delete":
                            var editableDeleteItem = ((GridEditableItem)e.Item);
                            Hashtable valuesDelete = new Hashtable();
                            editableDeleteItem.ExtractValues(valuesDelete);
                            int idOpcion = Convert.ToInt32(valuesDelete["idOpcionPregunta"]);
                            try
                            {
                                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                                string stmt = "delete from dbo.sm_OpcionPregunta where idOpcionPregunta = '" + idOpcion + "';";
                                SqlCommand cmd = new SqlCommand(stmt, con);
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                string error = ex.Message;
                            }
                            finally
                            {
                                con.Close();
                            };
                            break;
                        case "Update":
                            var editableEditadoItem = ((GridEditableItem)e.Item);
                            Hashtable valuesEditados = new Hashtable();
                            editableEditadoItem.ExtractValues(valuesEditados);
                            string opcionEditada = (string)valuesEditados["enunciadoPregunta"];
                            string indiceEditado = (string)valuesEditados["indiceOpcion"];
                            int idOpcionEditada = Convert.ToInt32(valuesEditados["idOpcionPregunta"]);
                            try
                            {
                                con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                                string stmt = "UPDATE dbo.sm_OpcionPregunta SET enunciadoPregunta = @Enunciado , indiceOpcion = @Indice where idOpcionPregunta ='" + idOpcionEditada + "'";
                                SqlCommand cmd = new SqlCommand(stmt, con);

                                cmd.Parameters.Add("@Enunciado", SqlDbType.VarChar, 510);
                                cmd.Parameters.Add("@Indice", SqlDbType.VarChar, 50);

                                cmd.Parameters["@Enunciado"].Value = opcionEditada;
                                cmd.Parameters["@Indice"].Value = 'a';
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                string error = ex.Message;
                            }
                            finally
                            {
                                con.Close();
                            }
                            break;

                    }
                }
                else
                {
                    switch (e.CommandName)
                    {
                        case "PerformInsert": GuardaPregunta(sender, e);
                            break;
                        case "Delete": EliminarPregunta(sender, e);
                            break;
                        case "Update": EditarPregunta(sender, e);
                            break;
                    }
                }
            }
        }

        protected void ConsultaOpciones(object sender, GridDetailTableDataBindEventArgs e)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            Hashtable values = new Hashtable();
            dataItem.ExtractValues(values);
            string idPregunta = values["idPregunta"].ToString();
            string Command = "select * from sm_OpcionPregunta where idPregunta='" + idPregunta + "' and idEncuesta ='"+hdfEncuestaActual.Value+"'";
            using (SqlDataAdapter myDataAdapter = new SqlDataAdapter(Command, con))
            {
                DataTable dtResult = new DataTable();
                myDataAdapter.Fill(dtResult);
                e.DetailTableView.DataSource = dtResult;
            }
        }

        protected void Reordenar(object sender, GridDragDropEventArgs e)
        {
            if (e.DraggedItems.Count > 0 && e.DestDataItem != null)
            {
                Hashtable values = new Hashtable();
                e.DraggedItems[0].ExtractValues(values);
                int idDragged = Convert.ToInt32(values["idPregunta"]);
                int draggedIndex = ((int)e.DraggedItems[0].GetDataKeyValue("ordenPregunta"));
                int destinationIndex = ((int)e.DestDataItem.GetDataKeyValue("ordenPregunta"));
                int indexDragged;
                try
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString();
                    string stmt;
                    if(destinationIndex<draggedIndex)
                    {
                        indexDragged = draggedIndex++;
                        stmt = "update sm_Pregunta set ordenPregunta = ordenPregunta+1 where idEncuesta = "+ hdfEncuestaActual.Value+" and ordenPregunta >= "+destinationIndex+" and ordenPregunta<"+draggedIndex;
                    }else{
                        indexDragged = draggedIndex--;
                        stmt = "update sm_Pregunta set ordenPregunta = ordenPregunta-1 where idEncuesta = "+ hdfEncuestaActual.Value+" and ordenPregunta <= "+destinationIndex+" and ordenPregunta>"+draggedIndex;
                    }
                    SqlCommand cmd = new SqlCommand(stmt, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    stmt = "update sm_Pregunta set ordenPregunta =" + destinationIndex + " where idEncuesta =" + hdfEncuestaActual.Value + " and idPregunta = " + idDragged;
                    cmd.CommandText = stmt;
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {

                }
                finally 
                {
                    con.Close();
                }
                //SqlDataSource1.UpdateParameters["destinationCategoryId"].DefaultValue = destinationIndex.ToString();
                //SqlDataSource1.UpdateParameters["currentCategoryId"].DefaultValue = draggedIndex.ToString();
                //if (e.DropPosition == GridItemDropPosition.Below)
                //    SqlDataSource1.UpdateParameters["Position"].DefaultValue = "1";

                //SqlDataSource1.Update();

                preguntas.Rebind();
            }
        }

        protected void deshabilitar(object sender, EventArgs e)
        {
            foreach (GridDataItem item in preguntas.Items)
            {
                HiddenField hdfTipoPregunta = (HiddenField)item.FindControl("hdfTipo");
                string tipo = hdfTipoPregunta.Value;
                //if (hayRespondidas > 0)
                //{
                //    ImageButton btn1 = (ImageButton)item["EditCommandColumn"].Controls[0];
                //    btn1.Enabled = false;
                //}
                if (tipo == "3")
                {
                    Button expandcollapsebtn = (Button)item["ExpandColumn"].Controls[0];
                    expandcollapsebtn.Attributes.Add("style", "pointer-events:none;background: none !important;");
                }
            }
        }

        //protected void Unnamed_Click(object sender, EventArgs e)
        //{
        //    hdfEncuestaActual.Value = hdfEncuestaActual.Value;
        //}

        //select nombrePregunta, r.respuestaTexto,o.enunciadoPregunta from sm_Pregunta p join sm_Respuesta r on p.idPregunta = r.idPregunta left join sm_OpcionPregunta o on r.idOpcion = o.idOpcionPregunta where p.idEncuesta = 1
        //select nombrePregunta, r.respuestaTexto,o.enunciadoPregunta from sm_Pregunta p join sm_Respuesta r on p.idPregunta = r.idPregunta left join sm_OpcionPregunta o on r.idOpcion = o.idOpcionPregunta where p.idEncuesta = 1 order by ordenPregunta asc
    }
}