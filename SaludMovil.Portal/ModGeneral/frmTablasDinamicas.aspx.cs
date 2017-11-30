#region Informacion formulario
/* Autor: Andres Felipe Silva Pascuas - AFSP
 * 
 * Version 1.0 - 11 Julio 2016
 * 
 * Fecha Creacion: 8 Julio 2016
 * Descripcion: Formulario que se lista dinamicamente en una grilla basandose en el esquema de la tabla que se escoje; permite realizar paginacion, filtrado y ordenamiento
 *              por columnas y realizar combinaciones entre estas, adioionalemnte permite exportar a excel la informacion mostrada
 * Fecha Modificacion: 8 Julio V 1.0 del formulario funcional
 * Fecha Modificacion: 11 Julio 2016 Añadidos comentarios y revision final version 1.0
 */
#endregion

#region Imports
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaludMovil.Negocio;
using Telerik.Web.UI;
using System.Data;
using SaludMovil.Entidades;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
#endregion

namespace SaludMovil.Portal.ModGeneral
{
    public partial class frmTablasDinamicas : System.Web.UI.Page
    {
        private AdministracionNegocio adminNegocio;
        private static string conexion = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString(); //@"Data Source=DESKTOP-K2HN8P6\SQL2014ANDRES;Initial Catalog=FAM;User ID=sa;Password=icono";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    cargarTablas();                    
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message, true);
                }
            }
        }


        #region Metodos
        /// <summary>
        /// Metodo que realiza la carga inicial de los objetos(tablas) que estan disponibles para ser administradas por este modulo
        /// </summary>
        public void cargarTablas()
        {                         
            adminNegocio = new AdministracionNegocio();
            //TODO: Falta pasar por parametro el role del usuario que se loguea (el prefijo puede estar en el web.config)
            cboListaTablas.DataSource = adminNegocio.ConsultarTablasAdministrables("1", "0");
            cboListaTablas.DataTextField = "nombreTablaMaestra";
            cboListaTablas.DataValueField = "nombreObjeto";
            cboListaTablas.DataBind();
            cboListaTablas.Items.Insert(0, new ListItem("Seleccione una tabla para administrar...", "_TABLA_"));
            string nombreTabla = "_TABLA_";
            if (Request.QueryString["tabla"] != null)
            {
                nombreTabla = Request.QueryString["tabla"].ToString();
                MostrarMensaje("Guardado", false);
                pnlGrilla.Visible = true;
                RadGridAutomatica.Rebind();
            }
            cboListaTablas.SelectedValue = nombreTabla;
        }
        #endregion
        
        #region Eventos Combos
        /// <summary>
        /// Metodo que controla el mostrar o no la grilla una vez seleccionado un objeto(tabla)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboListaTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboListaTablas.SelectedValue != "_TABLA_")
                {
                    pnlGrilla.Visible = true;
                    RadGridAutomatica.Rebind();
                }
                else
                    pnlGrilla.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }            
        }
        #endregion

        #region Grilla automatica
        /// <summary>
        /// Metodo protegido de la grilla dinamica que se llama cada vez que necesita cargarse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridAutomatica_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (!cboListaTablas.SelectedValue.Equals("_TABLA_"))
                {
                    RadGridAutomatica.DataSource = obtenerInfoTabla(cboListaTablas.SelectedValue);
                    RadGridAutomatica.Enabled = true;
                    CambiarIdiomaFilterGridTelerik(RadGridAutomatica);
                }                
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Metodo protegido de la grilla dinamica que se ejecuta con cada accion propia de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridAutomatica_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {                
                switch (e.CommandName)
                {
                    case "InitInsert":
                        if (Session["llave"] != null)
                            Session.Remove("llave");
                        else
                            Session["llave"] = null;
                        Session["tabla"] = cboListaTablas.SelectedValue;
                        Response.Redirect("~/ModGeneral/frmControlDinamico.aspx");
                        break;
                    case "Edit":
                        GridEditableItem item = (GridEditableItem)e.Item;
                        Hashtable llave = new Hashtable();                       
                        foreach(string i in RadGridAutomatica.MasterTableView.DataKeyNames)
                        {
                            llave.Add(i, item.GetDataKeyValue(i).ToString());
                        }
                        Session["llave"] = llave;
                        Session["tabla"] = cboListaTablas.SelectedValue;
                        Response.Redirect("~/ModGeneral/frmControlDinamico.aspx");
                        break;
                    case "Delete":
                        GridEditableItem itemD = (GridEditableItem)e.Item;
                        string campos = string.Empty, value = string.Empty;
                        foreach (string i in RadGridAutomatica.MasterTableView.DataKeyNames)
                        {
                            if (!campos.Equals(string.Empty))
                                campos += " and ";
                            if (itemD.GetDataKeyValue(i).GetType().ToString().Equals("System.DateTime"))
                                campos += "(DATEADD(ms, -DATEPART(ms," + i + "), " + i + ")) = ";
                            else
                                campos += "([" + i + "] = ";
                            value = itemD.GetDataKeyValue(i).GetType().ToString();
                            switch (value)
                            {
                                case "System.DateTime": 
                                        campos += "'" + itemD.GetDataKeyValue(i).ToString() + "'";
                                    break;
                                case "System.String":
                                    campos += "'" + itemD.GetDataKeyValue(i).ToString() + "')";
                                    break;
                                case "System.DBNull":
                                    campos += "'" + itemD.GetDataKeyValue(i).ToString() + "' OR [" + i + "] IS NULL)";
                                    break;
                                default:
                                    campos += "'" + itemD.GetDataKeyValue(i).ToString() + "')";
                                    break;
                            }
                        }
                        adaptadorNonQuery("DELETE FROM " + cboListaTablas.SelectedValue + " WHERE " + campos);
                        MostrarMensaje("Eliminado", false);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void RadGridAutomatica_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            try
            {
                //if (e.Column.UniqueName == "YourdataFieldName")
            // OR
            //if (e.Column.HeaderText == "YourdataFieldName")
            //{
                e.Column.AutoPostBackOnFilter = true;
                e.Column.FilterControlWidth = Unit.Pixel(100);
                //if(e.Column.ColumnType.Equals("GridDateTimeColumn"))
                //    e.Column.
            //}
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void RadGridAutomatica_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //if (e.Item is GridDataItem)
                //{
                //    GridDataItem item = e.Item as GridDataItem;
                //    string value = string.Empty;
                //    foreach (TableCell tc in item.Cells)
                //    {
                //        value = tc.NamingContainer.ToString();
                //        value = item.FindControl(value).GetType().ToString();
                //    }
                //}
                //string value = e.Item.da.GetType().ToString();
                //e.Column.AutoPostBackOnFilter = true;
                //e.Column.FilterControlWidth = Unit.Pixel(100);
                //if(e.Item.Controls..ColumnType.Equals("GridDateTimeColumn"))
                //    e.Column.
            //}
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }        
        #endregion        

        #region Manejador de mensajes
        private void MostrarMensaje(string mensaje, bool esError)
        {
            string msg = mensaje;
            if (esError)
                msg = "Error: " + mensaje.Replace("'", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            RadNotificationMensajes.Show(msg);
        }
        #endregion

        #region Metodos generales
        /// <summary>
        /// Metodo que consulta la informacion de la tabla y que se encarga de armar los datakeynames para las actualizaciones y eliminaciones
        /// </summary>
        /// <param name="nombreTabla"></param>
        /// <returns></returns>
        private DataTable obtenerInfoTabla(string nombreTabla)
        {
            adminNegocio = new AdministracionNegocio();
            IList<EspecificacionObjeto> especificacion = adminNegocio.ConsultarEspecificacion("V"+nombreTabla);//Consulta primero por la vista
            bool esVista = true;
            if (especificacion.Count == 0)
            {
                esVista = false;
                especificacion = adminNegocio.ConsultarEspecificacion(nombreTabla);//Si no tiene vista consulta la tabla                
            }
            string campos = string.Empty;
            string dataKeys = string.Empty;
            foreach (EspecificacionObjeto i in especificacion)
            {
                if (!campos.Equals(string.Empty))
                {
                    campos += ",";
                }
                campos += i.column_name;
                //if (i.is_primary_key == 1)
                //{
                //    if (!dataKeys.Equals(string.Empty))
                //        dataKeys += ",";
                //    dataKeys += i.column_name;
                //}
            }
            if (campos.Length > 0)
                RadGridAutomatica.MasterTableView.DataKeyNames = campos.Split(',');
            //else
            //RadGridAutomatica.MasterTableView;
            if(esVista)
                return adaptador("SELECT " + campos + " FROM V" + nombreTabla);
            else
                return adaptador("SELECT " + campos + " FROM " + nombreTabla);
        }

        /// <summary>
        /// Adaptador SQL para las consultas
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        //TODO: Actualizar a reflection con nueva arquitectura
        private static DataTable adaptador(string queryString)
        {
            try
            {
                DataTable dsResultado = new DataTable();
                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    connection.Open();
                    adapter.SelectCommand = new SqlCommand(queryString, connection);
                    adapter.Fill(dsResultado);
                    connection.Close();
                    return dsResultado;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Adaptador SQL para las inserciones, actualizaciones, eliminaciones 
        /// </summary>
        /// <param name="queryString"></param>
        //TODO: Actualizar a reflection con nueva arquitectura
        private static void adaptadorNonQuery(string queryString)
        {
            DataSet dsResultado = new DataSet();
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Metodo general para uso exclusivo de la grilla para cambiar los labels que se muestran en los filtros a idioma español
        /// </summary>
        /// <param name="Grilla"></param>
        private void CambiarIdiomaFilterGridTelerik(RadGrid Grilla)
        {
            List<GridFilterMenu> grids = new List<GridFilterMenu>();
            grids.Add(Grilla.FilterMenu);
            foreach (GridFilterMenu gridFilterMenu in grids)
            {
                GridFilterMenu menu = gridFilterMenu;
                foreach (RadMenuItem item in menu.Items)
                {
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
        #endregion
    }
}