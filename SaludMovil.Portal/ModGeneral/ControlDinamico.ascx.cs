#region Imports
using System;
using System.Web;
//using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web.UI.WebControls;
//using System.Web.Security;
//using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Telerik.Web.UI;
using SaludMovil.Transversales;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using System.Web.UI;
using System.Collections;
using System.Data.SqlClient;
#endregion

namespace SaludMovil.Portal.ModGeneral
{
    public partial class ControlDinamico : System.Web.UI.UserControl
    {
        private AdministracionNegocio adminNegocio;
        private string tabla = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    //tabla = Session["tabla"].ToString();
                    tabla = "sm_Departamento";
                    string idUsuario = "1";
                    string idRol = "1";
                    //string idUsuario = Session["usuario"].ToString();
                    //string idRol = Session["rol"].ToString();
                    CargarFormulario();
                }
                catch (Exception ex)
                {
                    //RadNotificationMensajes.Show("Error: " + ex.Message.ToString());
                }
            }
        }

        private void CargarFormulario()
        {
            string nombreTabla = tabla.Substring(tabla.IndexOf(".") + 1).Replace(ConfigurationManager.AppSettings["prefijo"].ToString(), "");
            if ((tabla != null))
            {
                /*Pruebas*/
                PlaceHolder pl = placeHolderControl;
                if (pl != null)
                {
                    HtmlGenericControl div1 = new HtmlGenericControl("div");
                    div1.InnerHtml = "Un div creado por CB";
                    div1.Attributes.Add("class", "jumbotron");
                    div1.ID = "div1";
                    pl.Controls.Add(div1);
                    //Master.FindControl("placeHolderControl").FindControl("div1");
                    placeHolderControl.FindControl("div1").Controls.Add(formaPorDefecto(tabla, "", "", "", null, true));
                    //placeHolderControl.Controls.Add(formaPorDefecto(tabla, "", "", "", null, true));

                }
                /*FIn Pruebas*/

                lblTitle.Text = "ACTUALIZACIÓN DE DATOS EN LA TABLA " + nombreTabla.ToUpper();
            }
            else
            {
                placeHolderControl.Controls.Add(formaPorDefecto(tabla, "", "", "", null, true));
                lblTitle.Text = "INGRESO DE DATOS EN LA TABLA " + nombreTabla.ToUpper();
            }
        }

        #region Construccion dinamica del formulario
        /// <summary>
        /// Consulta el esquema de la tabla que rcibe por parametro y crea dinamicamente el formulario
        /// Lineamientos:
        /// 1. Construir mètodos privados para cada tipo de control (TextBoxNormal, TextBowMultiLinea, RadioButton, DropDownList, Grilla, etc)
        /// 2. Verificar las variables
        /// 3. Quitar todos los DataSet --> Verificar si es posible cambiar esto por una Entidad
        /// 4. Los ciclos dentro nunca deben de llevar declaraciones de variables
        /// 5. No debe de existir programación estática, usen constantes de la clase Comun
        /// 6. Tres métodos privados (Auditoria, Llaves Foraneas y otro para el resto de campos)
        /// 7. 
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="languageId"></param>
        /// <param name="regionId"></param>
        /// <param name="roleId"></param>
        /// <param name="objectKey"></param>
        /// <param name="isOptional"></param>
        /// <returns></returns>
        public Control formaPorDefecto(string objectName,
            string languageId,
            string regionId,
            string roleId,
            Hashtable objectKey,
            Boolean isOptional)
        {
            bool es_combo = false;
            Table mitabla = new Table();
            RequiredFieldValidator v = default(RequiredFieldValidator);
            Label label = default(Label);
            System.Web.UI.WebControls.TextBox controlTextBox = default(System.Web.UI.WebControls.TextBox);
            System.Web.UI.WebControls.DropDownList combo = default(System.Web.UI.WebControls.DropDownList);
            //AjaxControlToolkit.CalendarExtender calendarControl = null;
            //AjaxControlToolkit.MaskedEditExtender maskedEdit = null;
            ImageButton imageButton = null;
            System.Web.UI.WebControls.CheckBox checkBox = default(System.Web.UI.WebControls.CheckBox);
            System.Web.UI.WebControls.DropDownList ddownList = default(System.Web.UI.WebControls.DropDownList);
            bool esCheckBox = false;
            bool esDdown = false;
            bool esCalendario = false;
            System.Web.UI.WebControls.Unit tamano = default(System.Web.UI.WebControls.Unit);
            string tipoDatoCampo = null;
            //AjaxControlToolkit.FilteredTextBoxExtender filtroNumeros = null;
            System.Web.UI.WebControls.RegularExpressionValidator expresionRegularMoneda = default(System.Web.UI.WebControls.RegularExpressionValidator);
            bool flag = false;
            //Flag creado para validar la adicion de filas al formulario cuando es CreatedBy, CreatedDate, UpdatedBy, UpdatedDate
            Hashtable hashControles = new Hashtable();
            TableRow tr = new TableRow();
            TableCell tc1 = new TableCell();
            TableCell tc2 = new TableCell();
            TableCell tc3 = new TableCell();
            string etiquetaCampo = null;
            //AjaxControlToolkit.ListSearchExtender searchExtender = null;
            DataSet values;
            //Si existe el objeto.
            if ((objectKey != null))
            {
                values = this.getObject(objectName, objectKey);
            }
            else
            {
                values = null;
            }
            //ds = detalleTabla
            adminNegocio = new AdministracionNegocio();
            IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(tabla);
            //foreignKeys = llaves
            IList<Llave> llaves = adminNegocio.ConsultarLlaves(tabla);

            foreach (EspecificacionObjeto objetoTabla in detalleTabla)
            {
                flag = false;
                int foreignKeyData = -1;
                esCheckBox = false;
                esDdown = false;
                esCalendario = false;
                label = new Label();
                controlTextBox = new System.Web.UI.WebControls.TextBox();
                combo = new System.Web.UI.WebControls.DropDownList();
                checkBox = new System.Web.UI.WebControls.CheckBox();
                ddownList = new System.Web.UI.WebControls.DropDownList();
                tamano = new System.Web.UI.WebControls.Unit(320);
                tr = new TableRow();
                tc1 = new TableCell();
                tc2 = new TableCell();
                tc3 = new TableCell();
                //averiguar si el campo es una llave foranea
                foreach (Llave llaveObjeto in llaves)
                {
                    foreignKeyData = foreignKeyData + 1;
                    string columnName = objetoTabla.column_name.ToString();
                    string foundField = llaveObjeto.columnaOrigen.ToString();
                    esDdown = esDdown | columnName.Equals(foundField);
                    //Si el campo tiene lave foranea se consulta la tabla a la que hace referencia para poblar el combo
                    if (esDdown)
                    {
                        string tablaDestino = llaveObjeto.tablaDestino.ToString();
                        string esquemaDestino = llaveObjeto.esquemaDestino.ToString();
                        string columnaDestino = llaveObjeto.columnaDestino.ToString();
                        //string nombreMostrar = llaveObjeto.nombreMostrar.ToString();
                        //string cmdTextObtenerValoresFK = "select " + columnaDestino + ", " + nombreMostrar + " from " + tablaDestino + " union (select null, 'Seleccione...')";
                        string cmdTextObtenerValoresFKCHIMBIS = "select " + columnaDestino + ", nombre from " + tablaDestino + " union (select null, 'Seleccione...')";
                        //IList<spConsultaSistema_Result> tablaAdicional = adminNegocio.ConsultarSPSistema(cmdTextObtenerValoresFK);
                        DataSet dsTablaChimbis = adaptador(cmdTextObtenerValoresFKCHIMBIS);
                        //ddownList.DataSource = tablaAdicional;
                        //ddownList.DataTextField = nombreMostrar;
                        ddownList.DataSource = dsTablaChimbis;
                        ddownList.DataTextField = "nombre";
                        ddownList.DataValueField = columnaDestino;
                        ddownList.DataBind();
                        break; // reventar el foreach
                    }
                }
                //Celda
                /*tc1.Attributes("align") = "left";*/
                //Propiedades básicas
                //Mostrar nombres más amigables para el usuario y fecha de creación.
                etiquetaCampo = objetoTabla.column_name.ToString();
                if (etiquetaCampo.Equals("CreatedBy") | etiquetaCampo.Equals("CreatedDate") | etiquetaCampo.Equals("UpdatedBy") | etiquetaCampo.Equals("UpdatedDate"))
                {
                    flag = true;
                }
                etiquetaCampo = (etiquetaCampo == "CreatedBy" ? "" : etiquetaCampo);
                etiquetaCampo = (etiquetaCampo == "CreatedDate" ? "" : etiquetaCampo);
                etiquetaCampo = (etiquetaCampo == "UpdatedBy" ? "" : etiquetaCampo);
                etiquetaCampo = (etiquetaCampo == "UpdatedDate" ? "" : etiquetaCampo);
                etiquetaCampo = (etiquetaCampo == "Nombre" ? "Nombre " + tabla : etiquetaCampo);
                //Separar por espacios el nombre a mostrar.
                //l.Text = string.Join(" ", Regex.Split(etiquetaCampo, "(?<!^)(?=[A-Z])"));
                if (!objetoTabla.is_identity)
                {
                    label.Text = label.Text.Replace("Id ", "Nombre ");
                }
                //CSSCLASS para el label del control a pintar
                label.CssClass = "formulario";
                string componentId = objectName.Replace(".", "_") + "_" + objetoTabla.column_name.ToString();
                controlTextBox.ID = componentId;
                combo.ID = componentId;
                checkBox.ID = componentId;
                ddownList.ID = componentId;
                controlTextBox.Width = tamano;
                ddownList.Width = tamano;
                if (objetoTabla.is_identity)
                {
                    controlTextBox.Enabled = false;
                    controlTextBox.Text = "Asignado por el sistema...";
                }
                object ctext = null;
                if ((values != null))
                {
                    ctext = values.Tables[0].Rows[0][objetoTabla.column_name];
                }
                else
                {
                    ctext = DBNull.Value;
                }
                //Ajustar el css
                if (Convert.ToBoolean(objetoTabla.is_nullable))
                {
                    if ((values != null))
                    {
                        if (ctext != null)
                        {
                            controlTextBox.Text = ctext.ToString();
                        }
                    }
                    controlTextBox.CssClass = "campo_opcional";
                    combo.CssClass = "form-control";
                }
                else
                {
                    if ((values != null))
                    {
                        controlTextBox.Text = values.Tables[0].Rows[0][objetoTabla.column_name].ToString();
                    }
                    controlTextBox.CssClass = "campo_opcional";
                    combo.CssClass = "campo_opcional";

                    if ((!objetoTabla.is_identity) & (Convert.ToString(objetoTabla.type_name) != "bit"))
                    {
                        v = new RequiredFieldValidator();
                        v.SetFocusOnError = true;
                        if (es_combo)
                        {
                            v.ControlToValidate = combo.ID;
                        }
                        else
                        {
                            v.ControlToValidate = controlTextBox.ID;
                        }
                        v.ErrorMessage = "El campo [" + label.Text + "] es requerido.";
                        v.Text = "*";
                        v.SetFocusOnError = true;
                        /*tc3.Attributes("align") = "left";*/
                        tc3.Controls.Add(v);
                    }
                }
                //2005-02-16 (JRA) Adición de la descripción del tipo de dato del campo.  
                switch (Convert.ToString(objetoTabla.type_name))
                {
                    case "char":
                    case "nvarchar":
                    case "varchar":
                    case "decimal":
                        tipoDatoCampo = "text";
                        if (objetoTabla.max_length != null)
                        {
                            controlTextBox.MaxLength = objetoTabla.max_length;
                            if (controlTextBox.MaxLength > 50)
                            {
                                controlTextBox.TextMode = TextBoxMode.MultiLine;
                                controlTextBox.Rows = 3;
                            }
                        }
                        break;
                    case "tinyint":
                    case "smallint":
                    case "int":
                    case "numeric":
                        tipoDatoCampo = "number";
                        if (!esDdown)
                        {
                            //filtroNumeros = new AjaxControlToolkit.FilteredTextBoxExtender();
                            //filtroNumeros.TargetControlID = componentId;
                            //filtroNumeros.ID = "masked_" + componentId;
                            //filtroNumeros.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
                            controlTextBox.MaxLength = 9;
                        }
                        if (objetoTabla.is_identity)
                        {
                            //filtroNumeros.Enabled = false;
                        }
                        break;
                    case "money":
                        tipoDatoCampo = "money";
                        expresionRegularMoneda = new System.Web.UI.WebControls.RegularExpressionValidator();
                        expresionRegularMoneda.ID = "masked_" + componentId;
                        expresionRegularMoneda.ErrorMessage = "Verifique El Valor";
                        expresionRegularMoneda.ControlToValidate = componentId;
                        expresionRegularMoneda.ValidationExpression = "^\\d{1,9}(.\\d{0,4})?$";
                        expresionRegularMoneda.SetFocusOnError = true;
                        expresionRegularMoneda.Text = "*";
                        break;
                    case "datetime":
                    case "date":
                    case "smalldatetime":
                        tipoDatoCampo = "date";
                        imageButton = new ImageButton();
                        string strId = "image_seleccionar_" + componentId;
                        imageButton.ID = strId;
                        imageButton.ImageUrl = "~/images/calendario.png";
                        imageButton.AlternateText = "Mostrar Calendario";
                        imageButton.CausesValidation = false;
                        //calendarControl = new AjaxControlToolkit.CalendarExtender();
                        esCalendario = true;
                        //calendarControl.TargetControlID = componentId;
                        //calendarControl.CssClass = "MyCalendar";
                        //calendarControl.Format = "yyyy-MM-dd";
                        //calendarControl.PopupButtonID = strId;
                        //calendarControl.Animated = true;
                        if ((values != null))
                        {
                            controlTextBox.Text = Convert.ToDateTime(values.Tables[0].Rows[0][objetoTabla.column_name]).ToString("yyyy-MM-dd");
                        }
                        break;
                    case "bit":
                        if (ctext != null)
                        {
                            checkBox.Checked = true;
                        }
                        else
                        {
                            checkBox.Checked = Convert.ToBoolean(ctext);
                        }
                        checkBox.Text = "Activo";
                        tipoDatoCampo = objetoTabla.type_name;
                        esCheckBox = true;
                        break;
                    default:
                        tipoDatoCampo = objetoTabla.type_name;
                        break;
                }

                tc1.Controls.Add(label);
                if (esCheckBox)
                {
                    tc2.Controls.Add(checkBox);
                    hashControles.Add(checkBox.ID, "CheckBox");
                }
                else if (esDdown)
                {
                    //searchExtender = new AjaxControlToolkit.ListSearchExtender();
                    //searchExtender.ID = "masked_" + componentId;
                    //searchExtender.TargetControlID = componentId;
                    //searchExtender.PromptCssClass = "buscarEnListas";
                    //searchExtender.PromptText = "Buscar";
                    //searchExtender.PromptPosition = AjaxControlToolkit.ListSearchPromptPosition.Top;
                    ddownList.SelectedValue = ctext.ToString();
                    tc2.Controls.Add(ddownList);
                    //tc2.Controls.Add(searchExtender);
                    hashControles.Add(ddownList.ID, "DropDownList");
                }
                else
                {
                    if ((tipoDatoCampo != null))
                    {
                        switch (tipoDatoCampo)
                        {

                            case "money":
                                tc3.Controls.Add(expresionRegularMoneda);
                                break;
                            case "number":
                                if (!esDdown)
                                {
                                    //tc2.Controls.Add(filtroNumeros);
                                }
                                break;
                        }

                    }
                    if (esCalendario)
                    {
                        //tc2.Controls.Add(calendarControl);
                    }
                    tc2.Controls.Add(controlTextBox);
                    hashControles.Add(controlTextBox.ID, "TextBox");
                }
                tr.Cells.Add(tc1);
                tr.Cells.Add(tc2);
                tr.Cells.Add(tc3);
                if (controlTextBox.ID.Contains("CreatedBy"))
                {
                    Session["createdBy"] = controlTextBox.Text;

                }
                if (controlTextBox.ID.Contains("CreatedDate"))
                {
                    Session["createdDate"] = controlTextBox.Text;

                }
                //Obtener uniformidad en el formulario al no agregar filas cuando es createdby, createddate ...
                if (!flag)
                {
                    mitabla.Controls.Add(tr);
                }
            }
            mitabla.CellPadding = 2;
            mitabla.CellSpacing = 2;
            Session["controlesFormulario"] = hashControles;
            return mitabla;
        }

        /// <summary>
        /// Posiblemente este metodo se elimine (Realizar mas pruebas y ver en detalle si esta haciendo algo)
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        public DataSet getObject(string objectName, Hashtable objectKey)
        {
            string sqlSelect = null;
            IDictionaryEnumerator i = default(IDictionaryEnumerator);

            IList<spEsquemaTabla_Result> esquema = adminNegocio.ConsultarEsquemaTabla(objectName);
            //Armar la instrucción SQL para obtener el objeto.
            sqlSelect = "select * from " + objectName + " ";

            //Armar la parte WHERE de la instrucción SELECT.
            sqlSelect += "where ";
            /*i = objectKey.GetEnumerator;
            while (i.MoveNext)
            {*/
            //De acuerdo al tipo de dato, insertar el valor y el nombre del atributo.
            foreach (spEsquemaTabla_Result e in esquema)
            {
                if (i.Key == e.column_name)
                {
                    sqlSelect += "([" + e.column_name + "] = ";
                    switch (e.type_name.ToString())
                    {
                        case "tinyint":
                        case "smallint":
                        case "int":
                        case "decimal":
                        case "numeric":
                            sqlSelect += i.Value + ") and ";
                            break;
                        case "char":
                        case "nvarchar":
                        case "varchar":
                        case "datetime":
                        case "date":
                        case "smalldatetime":
                            sqlSelect += "'" + i.Value + "') and ";
                            break;
                    }
                }
            }
            //}
            //Quitar el último AND de la cadena.
            sqlSelect = sqlSelect.Substring(0, sqlSelect.Length - 5);
            return adaptador(sqlSelect);
        }
        #endregion

        #region Reescritura pintar
        /// Lineamientos:
        /// 1. Construir mètodos privados para cada tipo de control (TextBoxNormal, TextBowMultiLinea, RadioButton, Grilla, etc)
        /// 2. Verificar las variables
        /// 3. Quitar todos los DataSet --> Verificar si es posible cambiar esto por una Entidad
        /// 4. Los ciclos dentro nunca deben de llevar declaraciones de variables
        /// 5. No debe de existir programación estática, usen constantes de la clase Comun
        /// 6. Tres métodos privados (Auditoria, Llaves Foraneas y otro para el resto de campos)
        //public Control formaPorDefecto(string objectName, string languageId, string regionId, string roleId, Hashtable objectKey, Boolean isOptional)
        //{
        //    bool es_combo = false;
        //    Table mitabla = new Table();
        //    RequiredFieldValidator v = default(RequiredFieldValidator);
        //    Label label = default(Label);
        //    ImageButton imageButton = null;
        //    System.Web.UI.WebControls.CheckBox checkBox = default(System.Web.UI.WebControls.CheckBox);
        //    bool esCheckBox = false;
        //    bool esDdown = false;
        //    bool esCalendario = false;
        //    //System.Web.UI.WebControls.Unit tamano = default(System.Web.UI.WebControls.Unit);
        //    string tipoDatoCampo = null;
        //    System.Web.UI.WebControls.RegularExpressionValidator expresionRegularMoneda = default(System.Web.UI.WebControls.RegularExpressionValidator);
        //    bool flag = false;
        //    Hashtable hashControles = new Hashtable();
        //    TableRow tr = new TableRow();
        //    TableCell tc1 = new TableCell();
        //    TableCell tc2 = new TableCell();
        //    TableCell tc3 = new TableCell();
        //    string etiquetaCampo = null;
        //    DataSet values;
        //    if ((objectKey != null))
        //    {
        //        values = this.getObject(objectName, objectKey);
        //    }
        //    else
        //    {
        //        values = null;
        //    }
        //    adminNegocio = new AdministracionNegocio();
        //    IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(tabla);
        //    IList<Llave> llaves = adminNegocio.ConsultarLlaves(tabla);
        //    /*PINTAR TODOS LOS COMBOS*/
        //    string tablaDestino = string.Empty;
        //    string esquemaDestino = string.Empty;
        //    string columnaDestino = string.Empty;
        //    //string nombreMostrar = llaveObjeto.nombreMostrar.ToString();
        //    //string cmdTextObtenerValoresFK = "select " + columnaDestino + ", " + nombreMostrar + " from " + tablaDestino + " union (select null, 'Seleccione...')";
        //    string cmdTextObtenerValoresFKCHIMBIS = string.Empty;
        //    DataSet dsTablaChimbis = new DataSet();
        //    string identificador = string.Empty;
        //    int tamano = 250;
        //    foreach (Llave llaveObjeto in llaves)
        //    {
        //        identificador = tabla.Replace(".", "_") + "_" + llaveObjeto.columnaDestino;
        //        tablaDestino = llaveObjeto.tablaDestino;
        //        esquemaDestino = llaveObjeto.esquemaDestino;
        //        columnaDestino = llaveObjeto.columnaDestino;
        //        //nombreMostrar = llaveObjeto.nombreMostrar.ToString();
        //        //cmdTextObtenerValoresFK = "select " + columnaDestino + ", " + nombreMostrar + " from " + tablaDestino + " union (select null, 'Seleccione...')";
        //        cmdTextObtenerValoresFKCHIMBIS = "select " + columnaDestino + ", nombre from " + tablaDestino + " union (select null, 'Seleccione...')";
        //        //IList<spConsultaSistema_Result> tablaAdicional = adminNegocio.ConsultarSPSistema(cmdTextObtenerValoresFK);
        //        dsTablaChimbis = adaptador(cmdTextObtenerValoresFKCHIMBIS);
        //        /***************************VER COMO SE COLOCA EL CONTROL EN ESTE PUNTO***************************************/                
        //        controlCombo(dsTablaChimbis, "nombre", columnaDestino, identificador, tamano); 
        //        break;
        //    }
        //    /*FIN PINTAR TODOS LOS COMBOS*/
        //    /*PINTAR EL RESTPO DE LOS CAMPOS*/
        //    foreach (EspecificacionObjeto objetoTabla in detalleTabla)
        //    {
        //        identificador = objectName.Replace(".", "_") + "_" + objetoTabla.column_name.ToString();



        //        flag = false;
        //        int foreignKeyData = -1;
        //        esCheckBox = false;
        //        esDdown = false;
        //        esCalendario = false;
        //        label = new Label();
        //        checkBox = new System.Web.UI.WebControls.CheckBox();
        //        tr = new TableRow();
        //        tc1 = new TableCell();
        //        tc2 = new TableCell();
        //        tc3 = new TableCell();





        //        //Mostrar nombres más amigables para el usuario y fecha de creación.
        //        etiquetaCampo = objetoTabla.column_name.ToString();
        //        if (etiquetaCampo.Equals("CreatedBy") | etiquetaCampo.Equals("CreatedDate") | etiquetaCampo.Equals("UpdatedBy") | etiquetaCampo.Equals("UpdatedDate"))
        //        {
        //            flag = true;
        //        }
        //        etiquetaCampo = (etiquetaCampo == "CreatedBy" ? "" : etiquetaCampo);
        //        etiquetaCampo = (etiquetaCampo == "CreatedDate" ? "" : etiquetaCampo);
        //        etiquetaCampo = (etiquetaCampo == "UpdatedBy" ? "" : etiquetaCampo);
        //        etiquetaCampo = (etiquetaCampo == "UpdatedDate" ? "" : etiquetaCampo);
        //        etiquetaCampo = (etiquetaCampo == "Nombre" ? "Nombre " + tabla : etiquetaCampo);
        //        //Separar por espacios el nombre a mostrar.
        //        //l.Text = string.Join(" ", Regex.Split(etiquetaCampo, "(?<!^)(?=[A-Z])"));
        //        if (!objetoTabla.is_identity)
        //        {
        //            label.Text = label.Text.Replace("Id ", "Nombre ");
        //        }
        //        //CSSCLASS para el label del control a pintar
        //        label.CssClass = "formulario";
        //        checkBox.ID = componentId;
        //        controlTextBox.Width = tamano;
        //        if (objetoTabla.is_identity)
        //        {
        //            controlTextBox.Enabled = false;
        //            controlTextBox.Text = "Asignado por el sistema...";
        //        }
        //        object ctext = null;
        //        if ((values != null))
        //        {
        //            ctext = values.Tables[0].Rows[0][objetoTabla.column_name];
        //        }
        //        else
        //        {
        //            ctext = DBNull.Value;
        //        }
        //        //Ajustar el css
        //        if (Convert.ToBoolean(objetoTabla.is_nullable))
        //        {
        //            if ((values != null))
        //            {
        //                if (ctext != null)
        //                {
        //                    controlTextBox.Text = ctext.ToString();
        //                }
        //            }
        //            controlTextBox.CssClass = "campo_opcional";
        //            combo.CssClass = "form-control";
        //        }
        //        else
        //        {
        //            if ((values != null))
        //            {
        //                controlTextBox.Text = values.Tables[0].Rows[0][objetoTabla.column_name].ToString();
        //            }
        //            controlTextBox.CssClass = "campo_opcional";
        //            combo.CssClass = "campo_opcional";

        //            if ((!objetoTabla.is_identity) & (Convert.ToString(objetoTabla.type_name) != "bit"))
        //            {
        //                v = new RequiredFieldValidator();
        //                v.SetFocusOnError = true;
        //                if (es_combo)
        //                {
        //                    v.ControlToValidate = combo.ID;
        //                }
        //                else
        //                {
        //                    v.ControlToValidate = controlTextBox.ID;
        //                }
        //                v.ErrorMessage = "El campo [" + label.Text + "] es requerido.";
        //                v.Text = "*";
        //                v.SetFocusOnError = true;
        //                /*tc3.Attributes("align") = "left";*/
        //                tc3.Controls.Add(v);
        //            }
        //        }
        //        //2005-02-16 (JRA) Adición de la descripción del tipo de dato del campo.  
        //        switch (Convert.ToString(objetoTabla.type_name))
        //        {
        //            case "char":
        //            case "nvarchar":
        //            case "varchar":
        //            case "decimal":
        //                tipoDatoCampo = "text";
        //                if (objetoTabla.max_length != null)
        //                {
        //                    controlTextBox.MaxLength = objetoTabla.max_length;
        //                    if (controlTextBox.MaxLength > 50)
        //                    {
        //                        controlTextBox.TextMode = TextBoxMode.MultiLine;
        //                        controlTextBox.Rows = 3;
        //                    }
        //                }
        //                break;
        //            case "tinyint":
        //            case "smallint":
        //            case "int":
        //            case "numeric":
        //                tipoDatoCampo = "number";
        //                if (!esDdown)
        //                {
        //                    //filtroNumeros = new AjaxControlToolkit.FilteredTextBoxExtender();
        //                    //filtroNumeros.TargetControlID = componentId;
        //                    //filtroNumeros.ID = "masked_" + componentId;
        //                    //filtroNumeros.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        //                    controlTextBox.MaxLength = 9;
        //                }
        //                if (objetoTabla.is_identity)
        //                {
        //                    //filtroNumeros.Enabled = false;
        //                }
        //                break;
        //            case "money":
        //                tipoDatoCampo = "money";
        //                expresionRegularMoneda = new System.Web.UI.WebControls.RegularExpressionValidator();
        //                expresionRegularMoneda.ID = "masked_" + componentId;
        //                expresionRegularMoneda.ErrorMessage = "Verifique El Valor";
        //                expresionRegularMoneda.ControlToValidate = componentId;
        //                expresionRegularMoneda.ValidationExpression = "^\\d{1,9}(.\\d{0,4})?$";
        //                expresionRegularMoneda.SetFocusOnError = true;
        //                expresionRegularMoneda.Text = "*";
        //                break;
        //            case "datetime":
        //            case "date":
        //            case "smalldatetime":
        //                tipoDatoCampo = "date";
        //                imageButton = new ImageButton();
        //                string strId = "image_seleccionar_" + componentId;
        //                imageButton.ID = strId;
        //                imageButton.ImageUrl = "~/images/calendario.png";
        //                imageButton.AlternateText = "Mostrar Calendario";
        //                imageButton.CausesValidation = false;
        //                //calendarControl = new AjaxControlToolkit.CalendarExtender();
        //                esCalendario = true;
        //                //calendarControl.TargetControlID = componentId;
        //                //calendarControl.CssClass = "MyCalendar";
        //                //calendarControl.Format = "yyyy-MM-dd";
        //                //calendarControl.PopupButtonID = strId;
        //                //calendarControl.Animated = true;
        //                if ((values != null))
        //                {
        //                    controlTextBox.Text = Convert.ToDateTime(values.Tables[0].Rows[0][objetoTabla.column_name]).ToString("yyyy-MM-dd");
        //                }
        //                break;
        //            case "bit":
        //                if (ctext != null)
        //                {
        //                    checkBox.Checked = true;
        //                }
        //                else
        //                {
        //                    checkBox.Checked = Convert.ToBoolean(ctext);
        //                }
        //                checkBox.Text = "Activo";
        //                tipoDatoCampo = objetoTabla.type_name;
        //                esCheckBox = true;
        //                break;
        //            default:
        //                tipoDatoCampo = objetoTabla.type_name;
        //                break;
        //        }

        //        tc1.Controls.Add(label);
        //        if (esCheckBox)
        //        {
        //            tc2.Controls.Add(checkBox);
        //            hashControles.Add(checkBox.ID, "CheckBox");
        //        }
        //        else if (esDdown)
        //        {
        //            //searchExtender = new AjaxControlToolkit.ListSearchExtender();
        //            //searchExtender.ID = "masked_" + componentId;
        //            //searchExtender.TargetControlID = componentId;
        //            //searchExtender.PromptCssClass = "buscarEnListas";
        //            //searchExtender.PromptText = "Buscar";
        //            //searchExtender.PromptPosition = AjaxControlToolkit.ListSearchPromptPosition.Top;
        //            ddownList.SelectedValue = ctext.ToString();
        //            tc2.Controls.Add(ddownList);
        //            //tc2.Controls.Add(searchExtender);
        //            hashControles.Add(ddownList.ID, "DropDownList");
        //        }
        //        else
        //        {
        //            if ((tipoDatoCampo != null))
        //            {
        //                switch (tipoDatoCampo)
        //                {

        //                    case "money":
        //                        tc3.Controls.Add(expresionRegularMoneda);
        //                        break;
        //                    case "number":
        //                        if (!esDdown)
        //                        {
        //                            //tc2.Controls.Add(filtroNumeros);
        //                        }
        //                        break;
        //                }

        //            }
        //            if (esCalendario)
        //            {
        //                //tc2.Controls.Add(calendarControl);
        //            }
        //            tc2.Controls.Add(controlTextBox);
        //            hashControles.Add(controlTextBox.ID, "TextBox");
        //        }
        //        tr.Cells.Add(tc1);
        //        tr.Cells.Add(tc2);
        //        tr.Cells.Add(tc3);
        //        if (controlTextBox.ID.Contains("CreatedBy"))
        //        {
        //            Session["createdBy"] = controlTextBox.Text;

        //        }
        //        if (controlTextBox.ID.Contains("CreatedDate"))
        //        {
        //            Session["createdDate"] = controlTextBox.Text;

        //        }
        //        //Obtener uniformidad en el formulario al no agregar filas cuando es createdby, createddate ...
        //        if (!flag)
        //        {
        //            mitabla.Controls.Add(tr);
        //        }
        //    }
        //    mitabla.CellPadding = 2;
        //    mitabla.CellSpacing = 2;
        //    Session["controlesFormulario"] = hashControles;
        //    return mitabla;
        //}
        #endregion

        #region Controles
        private TextBox controlTextBox(string identificador, int tamano, Boolean esMultilinea)
        {
            System.Web.UI.WebControls.TextBox controlTexto = new System.Web.UI.WebControls.TextBox();
            controlTexto.ID = identificador;
            if (esMultilinea)
            {
                controlTexto.TextMode = TextBoxMode.MultiLine;
            }
            controlTexto.Width = tamano;
            controlTexto.CssClass = "form-control";
            return controlTexto;
        }            

        private DropDownList controlCombo(DataSet fuenteInfo, string textField, string valueField, string identificador, int tamano)
        {
            System.Web.UI.WebControls.DropDownList controlNuevoCombo = new System.Web.UI.WebControls.DropDownList();
            controlNuevoCombo.ID = identificador;
            controlNuevoCombo.Width = tamano;
            controlNuevoCombo.CssClass = "form-Control";
            controlNuevoCombo.DataSource = fuenteInfo;
            controlNuevoCombo.DataTextField = textField;
            controlNuevoCombo.DataValueField = valueField;            
            //controlNuevoCombo.DataBind();
            return controlNuevoCombo;
        }

        #endregion

        #region Carga de informacion
        #endregion

        #region CRUD dinamico

        //public Hashtable insercionObjeto(string nombreTabla, Hashtable atributos)
        //{
        //    DataRow attRow = default(DataRow);
        //    DataRow attIdentity = default(DataRow);
        //    string sqlInsert = null;
        //    string sqlValues = null;
        //    string sql = null;
        //    IDictionaryEnumerator i = default(IDictionaryEnumerator);
        //    Hashtable key = default(Hashtable);
        //    int identity = 0;
        //    bool hasIdentity = false;
        //    attIdentity = null;
        //    adminNegocio = new AdministracionNegocio();
        //    IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(tabla);
        //    key = new Hashtable();
        //    //Si algún argumento NO corresponde algún atributo del objeto.
        //    //Si NO existe algún atributo de la llave en los atributos.
        //    //Si NO se pasaron todos los atributos requeridos.
        //    //Armar la instrucción SQL para insertar el objeto.
        //    hasIdentity = false;
        //    foreach (EspecificacionObjeto o in detalleTabla)
        //    {
        //        if (o.is_identity)
        //        {
        //            hasIdentity = true;
        //            break;
        //        }
        //    }
        //    string querySQLinsercion = buildInsertCmd(nombreTabla, atributos, detalleTabla);
        //    //Usar la transacción si existe.
        //    try
        //    {
        //        if (hasIdentity)
        //        {
        //            identity = db.ExecuteScalar(CommandType.Text, finalCmd + " select @@identity");
        //        }
        //        else
        //        {
        //            db.ExecuteNonQuery(CommandType.Text, finalCmd);
        //        }

        //    }
        //    catch (System.Data.SqlClient.SqlException ex)
        //    {
        //        switch (ex.Errors[0].Number)
        //        {
        //            case 2601:
        //                //Error de Duplicacion de Indices
        //                throw new InvalidOperationException("YA EXISTE UN REGISTRO IGUAL CON ESA INFORMACION");
        //            case 2627:
        //                //Error de Duplicacion de Indices
        //                throw new InvalidOperationException("YA EXISTE UN REGISTRO IGUAL CON ESA INFORMACION");
        //            case 547:
        //                //CheckConstraint Violation
        //                throw new InvalidConstraintException("DEBE VERIFICAR LOS RANGOS ...");
        //            case 220:
        //                throw new InvalidConstraintException("DEBE VERIFICAR EL NÚMERO DE SEMANA Y/O EL AÑO ...");
        //            default:
        //                throw ex;
        //        }
        //    }
        //    catch (Exception exep)
        //    {
        //        throw exep;
        //    }
        //    //Guardar en una colección la llave del objeto ingresado.
        //    foreach (EspecificacionObjeto o in detalleTabla)
        //    {
        //        if (Convert.ToBoolean(o.is_primary_key))
        //        {
        //            if (o.is_identity)
        //            {
        //                key.Add(o.column_name, identity);
        //            }
        //            else
        //            {
        //                //key.Add(o.column_name, atributos(o.column_name));
        //            }
        //        }
        //    }
        //    return key;
        //}
        #endregion

        #region Obtencion de atributos
        public Hashtable valorAtributos(string objectName, HttpRequest requestObject)
        {
            string key = null;
            Hashtable atributosObjeto = new Hashtable();
            bool hasValue = false;
            short i = 0;
            adminNegocio = new AdministracionNegocio();
            IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(tabla);
            foreach (EspecificacionObjeto o in detalleTabla)
            {
                hasValue = false;
                i = 0;
                while (((i < requestObject.Form.Keys.Count - 1) & (!hasValue)))
                {
                    key = requestObject.Form.Keys[i];
                    string nombreCampo = o.column_name;
                    string match = objectName.Replace(".", "_") + "_" + o.column_name;
                    if (key.Equals(match) & !key.Contains("masked"))
                    {
                        switch (Convert.ToString(nombreCampo))
                        {
                            case "bit":
                                if (requestObject.Form[key] == "on")
                                {
                                    atributosObjeto.Add(o.column_name, "1");
                                }
                                else
                                {
                                    atributosObjeto.Add(o.column_name, "0");
                                }
                                break;
                            default:
                                if (string.IsNullOrEmpty(requestObject.Form[key]))
                                {
                                    atributosObjeto.Add(o.column_name, "null");
                                }
                                else
                                {
                                    if (!Convert.ToBoolean(o.is_identity))
                                    {
                                        atributosObjeto.Add(o.column_name, requestObject.Form[key]);
                                    }
                                }
                                break;
                        }
                        hasValue = true;
                    }
                    i += 1;
                }
                if (!hasValue)
                {
                    if (!o.is_identity)
                    {
                        switch (Convert.ToString(o.type_name))
                        {
                            case "tinyint":
                            case "smallint":
                            case "int":
                            case "decimal":
                            case "numeric":
                                atributosObjeto.Add(o.column_name, "0");
                                break;
                            default:
                                atributosObjeto.Add(o.column_name, "null");
                                break;
                        }
                    }
                }
            }
            return atributosObjeto;
        }
        #endregion

        #region Obtencion de objetos
        //public DataSet getObjects(string objectName, Hashtable additionalConditions = null, Hashtable orders = null)
        //{
        //    string sql = null;
        //    IDictionaryEnumerator i = default(IDictionaryEnumerator);
        //    adminNegocio = new AdministracionNegocio();
        //    //objAttributes = detalleTabla
        //    IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(tabla);
        //    //Armar la instrucción DELETE para borrar el objeto.
        //    sql = "select * from " + objectName + " ";
        //    if ((additionalConditions != null))
        //    {
        //        sql += "where ";
        //        i = additionalConditions.GetEnumerator;
        //        while (i.MoveNext)
        //        {
        //            //De acuerdo al tipo de dato, insertar el valor y el nombre del atributo.
        //            sql += "([" + obtenerAtributoPorNombre(i.Key.ToString(), detalleTabla) +"] = ";
        //            switch (o.type_name.)
        //            {
        //                case "tinyint" || "smallint" || "int" || "decimal" || "numeric" || "bit":
        //                    sql += i.Value + ") and ";
        //                    break;
        //                case "char"|| "nvarchar" || "varchar" || "datetime" || "smalldatetime":
        //                    sql += "'" + i.Value + "') and ";
        //                    break;
        //            }
        //        }
        //        //Quitar el último AND de la cadena.
        //        sql = sql.Substring(0, sql.Length - 5);
        //    }
        //    if ((orders != null))
        //    {
        //        if (orders.Keys.Count > 0)
        //        {
        //            sql += " order by ";
        //            i = orders.GetEnumerator;
        //            while (i.MoveNext)
        //            {
        //                switch (i.Value)
        //                {
        //                    case Order.Asc:
        //                        sql += "[" + i.Key + "] asc, ";
        //                        break;
        //                    case Order.Desc:
        //                        sql += "[" + i.Key + "] desc, ";
        //                        break;
        //                    default:
        //                        throw new Exception("No se especificó un orden válido para el atributo [" + i.Key + "]");
        //                }
        //            }
        //            //Quitar la última coma de la cadena.
        //            sql = sql.Substring(0, sql.Length - 2);
        //        }
        //    }
        //    return db.ExecuteDataSet(CommandType.Text, sql);
        //}
        #endregion

        #region Atributo por nombre
        /// <summary>
        /// Ontener el objetoAtributo en un listado de objetosAtributos del esquema de la tabla
        /// </summary>
        /// <param name="nombreAtributo"></param>
        /// <param name="detalleTabla"></param>
        /// <returns></returns>
        private EspecificacionObjeto obtenerAtributoPorNombre(string nombreAtributo, IList<EspecificacionObjeto> detalleTabla)
        {
            EspecificacionObjeto resultado = new EspecificacionObjeto();
            foreach (EspecificacionObjeto o in detalleTabla)
            {
                //if (o.column_name == nombreAtributo)
                //{
                //    resultado = o;
                //    break;
                //}
            }
            return resultado;
        }
        #endregion

        #region Eventos botones
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //    Hashtable atributos = default(Hashtable);
            //    try
            //    {
            //        atributos = valorAtributos(tabla, Request);
            //        guardarEstadoFormulario(Request);
            //        //Consulta si es actualizacion o insercion
            //        if (llave != null)//Actualizacion
            //        {
            //            DataSet ds = this.getObjects(tabla);
            //            //Adicionar atributos de usuario y fecha de creación.
            //            if (ds.Tables(0).Columns.Contains("CreatedBy"))
            //            {
            //                atributos["CreatedBy"] = Session["createdBy"];
            //            }
            //            if (ds.Tables(0).Columns.Contains("CreatedDate"))
            //            {
            //                atributos("CreatedDate") = Convert.ToDateTime(Session["createdDate"]).ToString(DATE_FORMAT);
            //            }
            //            //Adicionar atributos de usuario y fecha de actualización.
            //            if (ds.Tables(0).Columns.Contains("UpdatedBy"))
            //            {
            //                atributos("UpdatedBy") = userId;
            //            }
            //            if (ds.Tables(0).Columns.Contains("UpdatedDate"))
            //            {
            //                atributos("UpdatedDate") = Now.ToString(DATE_FORMAT);
            //            }
            //            updateObject(this.tabla, atributos, this.llave);

            //            //RadNotificationMensajes.Show("Actualizado");
            //            if ((Session["urlVolver"] != null))
            //            {
            //                Response.Redirect(Session["urlVolver"] + "?tabla=" + tabla);
            //            }
            //            else
            //            {
            //                Response.Redirect("ListaTablas.aspx?tabla=" + tabla);
            //            }
            //        }
            //        else//Insercion
            //        {
            //            llave = insercionObjeto(tabla, atributos);
            //            Session["llave"] = llave;
            //            //RadNotificationMensajes.Show("Guardado");
            //            if ((Session["urlVolver"] != null))
            //            {
            //                Response.Redirect(Session["urlVolver"] + "?tabla=" + tabla);
            //            }
            //            else
            //            {
            //                Response.Redirect("ListaTablas.aspx?tabla=" + tabla);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        //RadNotificationMensajes.Show("Error: " + ex.Message.ToString());
            //    }
        }
        #endregion

        #region Guardar estado del formulario
        //private void guardarEstadoFormulario(HttpRequest objetoRequest)
        //{
        //    try
        //    {
        //        Hashtable hashControles = new Hashtable();
        //        string tipoControl = "";
        //        DropDownList dropDown = null;
        //        TextBox textBox = null;
        //        CheckBox checkBox = null;
        //        List<string> informacionControles = new List<string>();
        //        string cadenaInfo = "";
        //        IDictionaryEnumerator elemento = null;
        //        Hashtable hashValorControles = new Hashtable();
        //        if ((Session["controlesFormulario"] != null))
        //        {
        //            hashControles = (Hashtable)Session["controlesFormulario"];
        //            elemento = hashControles.GetEnumerator();
        //            while (elemento.MoveNext())
        //            {
        //                string llave = elemento.Key;
        //                if (!(llave.Contains("CreatedBy") | llave.Contains("UpdatedBy") | llave.Contains("CreatedDate") | llave.Contains("UpdatedDate")))
        //                {
        //                    //hashValorControles.Add(elemento.Key, Request.Form.Item(elemento.Key))
        //                    cadenaInfo = llave + "," + elemento.Value + "," + Request.Form[elemento.Key];
        //                    informacionControles.Add(cadenaInfo);
        //                }
        //            }
        //        }
        //        Session["estadoControles"] = informacionControles;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        #endregion

        #region Construccion de querys dinamicos
        //private string construirInsertCmd(string nombreObjeto, Hashtable atributos, IList<EspecificacionObjeto> detalleTabla)
        //{
        //    string sqlInsert = "set dateformat ymd insert into " + nombreObjeto + " (";
        //    string sqlValues = "values (";
        //    string sqlUpdate = "set dateformat ymd update " + nombreObjeto + " set ";
        //    bool updating = false;
        //    string updatingValues = "";
        //    IDictionaryEnumerator i = atributos.GetEnumerator();
        //    string targetColumns = "";
        //    string targetValues = "";
        //    string currentColumn = "";
        //    string valorActual = "";
        //    string finalCmd = "";
        //    updating = (olderObject != null);
        //    while (i.MoveNext())
        //    {
        //        //De acuerdo al tipo de dato, insertar el valor y el nombre del atributo.
        //        EspecificacionObjeto atributoActual = obtenerAtributoPorNombre(i.Key.ToString(), detalleTabla);
        //        targetColumns += "[" + atributoActual.column_name + "], ";
        //        updatingValues += "[" + atributoActual.column_name + "]=";
        //        if (atributoActual.is_identity && (!updating))
        //        {
        //            throw new Exception("No pueden guardarse explícitamente los atributos de tipo autonumérico.");
        //        }
        //        valorActual = i.Value.ToString();
        //        switch (atributoActual.type_name.ToString())
        //        {
        //            case "tinyint":
        //            case "smallint":
        //            case "int":
        //            case "bigint":
        //                if (valorActual.Equals(""))
        //                {
        //                    valorActual = "0";
        //                }
        //                break;
        //            case "char":
        //            case "nvarchar":
        //            case "varchar":
        //            case "decimal":
        //            case "float":
        //            case "numeric":
        //                if (!(valorActual == "null"))
        //                {
        //                    valorActual = "'" + valorActual + "'";
        //                }
        //                else
        //                {
        //                    valorActual = "null";
        //                }
        //                break;
        //            case "datetime":
        //            case "date":
        //            case "smalldatetime":
        //                if (!(valorActual == "null"))
        //                {
        //                    valorActual = "'" + valorActual + "'";
        //                }
        //                else
        //                {
        //                    valorActual = "null";
        //                }
        //                break;
        //            case "bit":
        //                if (valorActual == "on")
        //                {
        //                    valorActual = "1";
        //                }
        //                else
        //                {
        //                    valorActual = "0";
        //                }
        //                break;
        //            case "money":
        //                valorActual = valorActual.Replace(".", ",");
        //                int index = valorActual.LastIndexOf(",");
        //                if (index != -1)
        //                {
        //                    valorActual = valorActual.Insert(index, ".");
        //                    valorActual = valorActual.Remove(index + 1, 1);
        //                    valorActual = valorActual.Replace(",", "");
        //                }
        //                break;
        //        }
        //        targetValues += valorActual + ", ";
        //        updatingValues += valorActual + ", ";
        //    }
        //    sqlInsert = sqlInsert + targetColumns.Substring(0, targetColumns.Length - 2) + ") ";
        //    sqlValues = sqlValues + targetValues.Substring(0, targetValues.Length - 2) + ") ";
        //    sqlUpdate = sqlUpdate + updatingValues.Substring(0, updatingValues.Length - 2) + " where ";
        //    finalCmd = sqlInsert + sqlValues;
        //    if (updating)
        //    {
        //        i = olderObject.GetEnumerator();
        //        while (i.MoveNext)
        //        {
        //            //De acuerdo al tipo de dato, insertar el valor y el nombre del atributo.

        //            DataRow attRow = null;
        //            attRow = this.FindAttributeByName(i.Key, objAttributes.Tables(0));
        //            currentColumn = attRow("column_name");
        //            updatingValues = "[" + currentColumn + "]=";
        //            valorActual = i.Value.ToString();
        //            switch (Convert.ToString(attRow("type_name")))
        //            {
        //                case "tinyint":
        //                case "smallint":
        //                case "int":
        //                case "bigint":
        //                    if (valorActual.Equals(""))
        //                    {
        //                        valorActual = "0";
        //                    }
        //                    break;
        //                case "char":
        //                case "nvarchar":
        //                case "varchar":
        //                case "decimal":
        //                case "float":
        //                case "numeric":
        //                    if (!(valorActual == "null"))
        //                    {
        //                        valorActual = "'" + valorActual + "'";
        //                    }
        //                    else
        //                    {
        //                        valorActual = "null";
        //                    }
        //                    break;
        //                case "datetime":
        //                case "date":
        //                case "smalldatetime":
        //                    if (!(valorActual == "null"))
        //                    {
        //                        valorActual = "'" + valorActual + "'";
        //                    }
        //                    else
        //                    {
        //                        valorActual = "null";
        //                    }
        //                    break;
        //                case "bit":
        //                    if (valorActual == "on")
        //                    {
        //                        valorActual = "1";
        //                    }
        //                    else
        //                    {
        //                        valorActual = "0";
        //                    }
        //                    break;
        //            }

        //            updatingValues += valorActual + ", ";
        //        }
        //        finalCmd = sqlUpdate + updatingValues.Substring(0, updatingValues.Length - 2);
        //    }
        //    return finalCmd;
        //}
        #endregion

        #region Adaptador SQL
        private static DataSet adaptador(string queryString)
        {
            DataSet dsResultado = new DataSet();
            using (SqlConnection connection = new SqlConnection("Data Source=192.168.1.100;Initial Catalog=SaludMovil;User ID=sa;Password=icono"))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(queryString, connection);
                adapter.Fill(dsResultado);
                return dsResultado;
            }
        }
        #endregion
    }    
}