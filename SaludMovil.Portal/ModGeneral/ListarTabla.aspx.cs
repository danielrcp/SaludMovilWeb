#region Imports
using System;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
//using System.Web.Security;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using Telerik.Web.UI;
using SaludMovil.Transversales;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
#endregion

namespace SaludMovil.Portal.ModGeneral
{
    public partial class ListarTabla : System.Web.UI.Page
    {
        private AdministracionNegocio adminNegocio;
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
                    //RadNotificationMensajes.Show(ex.Message.ToString());
                }
            }
            ////this.BOF2005 = Session["BOF2005"];
            ////this.BOFBD = Session["BOFBD"];            
            //DataTable dt = default(DataTable);
            //string rolesStr = "";
            ////facSeguridad = New Seguridad(BOF2005);
            ////facSeguridad.srv_ValidarSesion(Response, Session, ConfigurationManager.AppSettings["urlError"]);
            //DataTable roles = facSeguridad.srv_GetRolesUsuario(Session["IdUsuario"]);
            ////DataRow rolRow = default(DataRow);
            //foreach (DataRow rolRow in roles.Rows)
            //{
            //    string r = rolRow["RoleId"].ToString().Trim();
            //    rolesStr = rolesStr + r + ",";
            //}
            ////Se elimina la ultima coma de roleStr
            //int iUltima = rolesStr.LastIndexOf(",");
            //rolesStr = rolesStr.Remove(iUltima, 1);
            ////Put user code to initialize the page here
            //List<string> listaAtributos = new List<string>();
            //string tabla = null;            
            //this.PullState();
        }

        #region Metodos
        public void cargarTablas()
        {
            adminNegocio = new AdministracionNegocio();
            //Falta pasar por parametro el role del usuario que se loguea (el prefijo puede estar en el web.config)
            cboListaTablas.DataSource = adminNegocio.ConsultarTablasAdministrables("1", "0");
            cboListaTablas.DataTextField = "nombreTablaMaestra";
            cboListaTablas.DataValueField = "nombreObjeto";
            cboListaTablas.DataBind();
            cboListaTablas.Items.Insert(0, new ListItem("Seleccione una tabla para administrar...", "_TABLA_"));
        }
        #endregion

        #region Eventos botones
        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Eventos Combos
        protected void cboListaTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboListaTablas.SelectedValue != "_TABLA_")
            {
                cargarGrilla();
            }
        }
        #endregion

        #region Cargar Grilla
        private void cargarGrilla()
        {
            adminNegocio = new AdministracionNegocio();
            //adminNegocio.ConsultarEsquemaTabla(cboListaTablas.SelectedValue);
            RadGridAutomatica.DataSource = adminNegocio.ConsultarEspecificacion(cboListaTablas.SelectedValue);
            RadGridAutomatica.DataBind();
            RadGridAutomatica.Enabled = true;
        }
        #endregion

        #region Metodos - bindingInfo
//        private void bindingInfo(bool estado)
//        {
//            DataSet ds = default(DataSet);
//            DataSet objKey = default(DataSet);
//            string sql = null;
//            string keyNames = null;
//            string sqlv = null;
//            string auxObjName = null;
//            Database db = default(Database);
//            DataSet dsEs = default(DataSet);
//            string filtroActual = null;
//            string idTablaActual = null;

//            db = DatabaseFactory.CreateDatabase;

//            //Si NO existe algún atributo de la llave en los atributos.

//            sql = "SELECT c.name AS column_name " + ", c.column_id " + 
//                ", SCHEMA_NAME(t.schema_id) AS type_schema " + 
//                ", t.name AS type_name " + 
//                ", t.is_user_defined " + 
//                ", t.is_assembly_type " + 
//                ", c.max_length " + 
//                ", c.precision " + 
//                ", c.scale " + 
//                ", c.is_identity " + 
//                ", c.is_nullable " + 
//                ", case when ind.column_id is null then 0 else 1 end as is_primary_key " + 
//                "FROM sys.columns AS c  " + 
//                "JOIN sys.types AS t ON c.user_type_id=t.user_type_id " + "LEFT JOIN ( " + 
//                "select ic.object_id, ic.column_id " + 
//                "from sys.indexes AS i " + 
//                "INNER JOIN sys.index_columns AS ic " + 
//                "ON i.object_id = ic.object_id AND i.index_id = ic.index_id " + 
//                "where i.is_primary_key = 1) as ind " + 
//                "ON ind.object_id = c.object_id AND c.column_id = ind.column_id " + 
//                "WHERE c.object_id = OBJECT_ID('" + this.tabla + "') " + 
//                " and ind.column_id is not null " + 
//                "ORDER BY c.column_id";

//            objKey = db.ExecuteDataSet(CommandType.Text, sql);

//            auxObjName = this.tabla;
//            //Se asigna el nombre que debe tener la vista que se va a usar, si existe la vista
//            if (this.tabla.Contains(".")) {
//                auxObjName = "V" + this.tabla.Split(".")(1);
//            } else {
//                auxObjName = "V" + this.tabla;
//            }

//            sqlv = "SELECT * FROM " + auxObjName;

//            Hashtable orders = new Hashtable();
//            if (auxObjName.Equals("VtblConductor")) {
//                sqlv += " ORDER BY apellidos";
//                orders.Add("Apellidos", Order.Asc);
//            }

//            try {
//                //Si existe, no se debe lanzar excepción
//                ds = db.ExecuteDataSet(CommandType.Text, sqlv);
//                ds = this.getObjects(auxObjName, null, orders);
//                //MGA 04032011 se actualiza para corregir errores de carga de vistas
//                dsEs = getEspecification(auxObjName);
//                this.tabla = auxObjName;
//                //FIN MGA 04032011
//            } catch (SqlClient.SqlException ex) {
//                //Como no existe, entonces se va a usar la tabla
//                ds = this.getObjects(this.tabla);

//            }
//            //ds = Me.getObjects(Me.tabla)
//            //MGA 04032011 
//            dsEs = getEspecification(this.tabla);
//***************
//            DataTable td = dsEs.Tables(0);
//            DataRow i = default(DataRow);
//            string column_name = "";

//            foreach ( i in td.Rows) {
//                column_name = i("column_name").ToString;
//                listaAtributos.Add(column_name);
//            }
//            Session["LISTA_ATRIBUTOS"] = listaAtributos;
//            //Eliminar de la lista las tablas de auditoria.
//            if (ds.Tables(0).Columns.Contains("CreatedBy")) {
//                ds.Tables(0).Columns.Remove("CreatedBy");
//            }
//            if (ds.Tables(0).Columns.Contains("CreatedDate")) {
//                ds.Tables(0).Columns.Remove("CreatedDate");
//            }
//            if (ds.Tables(0).Columns.Contains("UpdatedBy")) {
//                ds.Tables(0).Columns.Remove("UpdatedBy");
//            }
//            if (ds.Tables(0).Columns.Contains("UpdatedDate")) {
//                ds.Tables(0).Columns.Remove("UpdatedDate");
//            }

//            //GridView1.DataSource = ds

//            keyNames = "";
//            foreach (DataRow dr in objKey.Tables(0).Rows) {
//                keyNames = dr("column_name") + "|";
//            }
//            //keyNames = "CodigoUsuario" & "|"
//            keyNames = keyNames.Substring(0, keyNames.Length - 1);

//            GridView1.DataKeyNames = keyNames.Split("|");

//            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
//            int idUsuario = 0;
//            string idActual = null;
//            string pagActual = null;

//            idUsuario = -1;

//            DataSet dsEstado = default(DataSet);
//            dsEstado = this.PageState.DsActual;

//            if (Information.IsDBNull(dsEstado) | (dsEstado == null) | estado) {
//                this.GridView1.DataSource = ds;
//                GridView1.DataBind();


//            } else {
//                filtroActual = this.PageState.FiltroActual;
//                idTablaActual = this.PageState.IdTablaActual;

//                tbBusqueda.Text = filtroActual;
//                if (!Information.IsDBNull(DropDownList1.Items.FindByValue(idTablaActual))) {
//                    DropDownList1.SelectedItem.Selected = false;
//                    DropDownList1.Items.FindByValue(idTablaActual).Selected = true;
//                }

//                this.GridView1.DataSource = realizarBusqueda();
//                pagActual = this.PageState.PagActual.ToString();
//                if (!(pagActual.Equals(string.Empty))) {
//                    GridView1.PageIndex = Convert.ToInt32(pagActual);
//                }
//                GridView1.DataBind();
//                idActual = this.PageState.IdActual.ToString();
//                if (!idActual.Equals(string.Empty)) {
//                    string idgrilla = null;
//                    foreach (GridViewRow gvRow in GridView1.Rows) {
//                        int algo = 0;
//                        algo = gvRow.RowIndex;
//                        idgrilla = GridView1.DataKeys(algo).Values(keyNames).ToString();
//                        if (idgrilla.Equals(idActual)) {
//                            gvRow.RowState = DataControlRowState.Selected;
//                        }
//                    }
//                }
//            }
//            //GridView1.DataBind()
//            obtenerRegistrosMostrados();
//            Session["llave"] = null;
//            if (this.tabla.Equals("Produccion.tblPorcentajeCambioProyeccion")) {
//                this.Button2.Enabled = false;
//            } else {
//                this.Button2.Enabled = true;
//            }
//            btnBusqueda.Enabled = true;
//            //btnReporte.Enabled = True
//            this.GridView1.Enabled = true;
//        }
        #endregion
    }
}