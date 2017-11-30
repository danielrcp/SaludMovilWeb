#region Informacion formulario
/* Autor: Andres Felipe Silva Pascuas - AFSP
 * 
 * Version 1.0 - 11 Julio 2016
 * 
 * Fecha Creacion: 8 Julio 2016
 * Descripcion: Formulario que se construye dinamicamente basandose en el esquema de la tabla que recibe por parametro, sus llaves foraneas y parametros de visualizacion
 *              adicionales en la forma de mostrar los campos
 * Fecha Modificacion: 8 Julio V 1.0 del formulario funcional con el esquema completo de la tabla inserciones, actualizaciones y carga de informacion.
 * Fecha Modificacion: 11 Julio 2016 Añadidos comentarios y revision final version 1.0
 */
#endregion

#region Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SaludMovil.Negocio;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using SaludMovil.Entidades;
using System.Collections;
using System.Text.RegularExpressions;
using System.Configuration;
#endregion

namespace SaludMovil.Portal.ModGeneral
{
    public partial class frmControlDinamico : System.Web.UI.Page
    {
        private AdministracionNegocio adminNegocio;
        private string tabla = "";
        private Hashtable llave;
        //TODO: Leer la conexion existente desde el web.config (esta pendiente por puebas realizadas en maquina local)
        private static string conexion = ConfigurationManager.ConnectionStrings["IconoCRM"].ToString(); //@"Data Source=DESKTOP-K2HN8P6\SQL2014ANDRES;Initial Catalog=FAM;User ID=sa;Password=icono";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    //TODO: Reemplazar la variable de sesion por lectura de querystring para la tabla en todo el formulario
                    tabla = Session["tabla"].ToString();
                    CargarFormulario();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message, true);
                }
            }
        }

        #region Cargar el formulario
        /// <summary>
        /// Metodo principal fachada para el armado dinamico del formulario
        /// </summary>
        private void CargarFormulario()
        {
            string nombreTabla = tabla;//tabla.Substring(tabla.IndexOf(".") + 1).Replace(ConfigurationManager.AppSettings["prefijo"].ToString(), "");
            Hashtable llave = (Hashtable)Session["llave"];
            bool hayTablaHomologada = true;//Especifica si existe una tabla que homologa las llaves foraneas cuando faltan las relaciones en el esquema de la tabla
            bool campoCompuesto = true;//Especifica si hay campos compuestos para mostrar en las tablas homologadas para la construccion de los combos
            if ((llave != null))
            {                
                PlaceHolder pl = placeHolderControl;
                if (pl != null)
                {   
                    EstructuraHTML(pl, 4, false, true, llave, nombreTabla, hayTablaHomologada, campoCompuesto);
                }
                lblTitle.Text = "ACTUALIZACIÓN DE DATOS EN LA TABLA " + nombreTabla.ToUpper();
            }
            else
            {
                EstructuraHTML(placeHolderControl, 4, false, true, null, nombreTabla, hayTablaHomologada, campoCompuesto);                
                lblTitle.Text = "INGRESO DE DATOS EN LA TABLA " + nombreTabla.ToUpper();
            }
        }
        #endregion

        #region Estructura html principal
        /// <summary>
        /// Metodo principal encargado de toda la construccion dinamica de los controles y validaciones de cada control con base en el esquema de la tabla, 
        /// llaves foraneas y control de tipos de datos de cada control, asi mismo recibe como parametros el placeholder que contiene a todos los controles,
        /// el numero de diviones en las que se quiere mostrar los campos que pueden ser entre 1-4 columnas, si se quiere o no agregar un jumbotron, el hash que contiene
        /// los datos a cargas, una opcion de mostrar la informacion descripctiva de cada campo, el nombre de la tabla que se va a pintar, y si se tiene una tabla que 
        /// homologa las llaves foraneas en caso tal que el esquema de la base de datos no cuente con las llaves para poder mostrar los combos correctamente de lo contrario 
        /// el formulario pintara un campo de texto.
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="divisiones"></param>
        /// <param name="jumbotron"></param>
        /// <param name="descripcionCampos"></param>
        /// <param name="cargarInfo"></param>
        /// <param name="nombreTabla"></param>
        /// <param name="hayTablaHomologada"></param>
        private void EstructuraHTML(PlaceHolder pl, int divisiones, bool jumbotron, bool descripcionCampos, Hashtable cargarInfo, string nombreTabla, bool hayTablaHomologada, bool campoCompuesto)
        { 
            try 
	        {
                adminNegocio = new AdministracionNegocio();
                IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(nombreTabla);//Consulta la especificacion de la tabla
                IList<Llave> llaves = adminNegocio.ConsultarLlaves(nombreTabla);//Consulta las llaves foraneas para los combos
                IList<Llave> llavesHomologadas = null;
                if(hayTablaHomologada)//Valida que si hay tabla homologada para armar los combos se consulte
                    llavesHomologadas = adminNegocio.ConsultarLlavesHomologadas(nombreTabla);
                int identificador = 1, contadorDivisiones = 1;
                string divRowID = string.Empty, divID = string.Empty, controlID = string.Empty;
                string divisionCSS = string.Empty;
                switch (divisiones)
                {
                    case 1:
                        divisionCSS = "col-md-12 col-centered";
                        break;
                    case 2:
                        divisionCSS = "col-md-6";
                        break;
                    case 3:
                        divisionCSS = "col-md-4";
                        break;
                    case 4:
                        divisionCSS = "col-md-3";
                        break;
                }
                int precision = 0, longitudMax = 0;
                bool esNulo = false, esIdentidad = false;
                string columna = string.Empty, tipo = string.Empty, nombreColumnaLlave = string.Empty, cmdTextObtenerValoresFKCHIMBIS = string.Empty, valor = string.Empty;
                DataTable valoresCargar = new DataTable();
                DataTable dtTablaChimbis = new DataTable();
                Llave objetoTablaLlave = new Llave();
                Llave objetoTablaLlaveHomologada = new Llave();
                if(cargarInfo != null)
                    valoresCargar = cargarInformacionTabla(nombreTabla, cargarInfo);
                TextBox txt;
                CheckBox chk;
                DropDownList cbo;
                RegularExpressionValidator regexp;
                foreach (EspecificacionObjeto objetoTabla in detalleTabla)
                {
                    columna = objetoTabla.column_name;
                    if (!columna.ToUpper().Equals("CREATEDBY") && !columna.ToUpper().Equals("CREATEDDATE") && !columna.ToUpper().Equals("UPDATEDBY") && !columna.ToUpper().Equals("UPDATEDDATE"))//Omitir los logs
                    {
                        if (contadorDivisiones == 1)
                        {
                            HtmlGenericControl divRow = new HtmlGenericControl("div");
                            divRowID = "divRow" + identificador;
                            divRow.ID = divRowID;
                            divRow.Attributes.Add("class", "row");
                            divRow.Style.Add("margin-top", "20px");
                            pl.Controls.Add(divRow);
                        }
                        HtmlGenericControl formGroup = new HtmlGenericControl("div");
                        divID = "formGroup" + identificador;
                        formGroup.ID = divID;
                        if (jumbotron)
                            formGroup.Attributes.Add("class", "jumbotron");
                        formGroup.Attributes.Add("class", divisionCSS);
                        precision = Convert.ToInt32(objetoTabla.precision);
                        esNulo = Convert.ToBoolean(objetoTabla.is_nullable);
                        esIdentidad = objetoTabla.is_identity;
                        tipo = objetoTabla.type_name;
                        longitudMax = objetoTabla.max_length;
                        pl.FindControl(divRowID).Controls.Add(formGroup);
                        pl.FindControl(divID).Controls.Add(ControlLabel(objetoTabla, descripcionCampos));//Buscar el div anterior y dentro colocar el label 
                        controlID = nombreTabla + "_" + columna;
                        objetoTablaLlave = obtenerAtributoPorNombre(columna, llaves);
                        if(hayTablaHomologada)
                            objetoTablaLlaveHomologada = obtenerAtributoPorNombre(columna, llavesHomologadas);
                        if (objetoTablaLlave != null || objetoTablaLlaveHomologada != null)
                            if (objetoTablaLlave != null)
                                nombreColumnaLlave = objetoTablaLlave.columnaOrigen;
                            else
                            {
                                nombreColumnaLlave = objetoTablaLlaveHomologada.columnaOrigen;
                                objetoTablaLlave = objetoTablaLlaveHomologada;
                            }
                        else
                            nombreColumnaLlave = string.Empty;
                        if (columna.Equals(nombreColumnaLlave))//El campo es un combo
                        {
                            try
                            {
                                dtTablaChimbis = ObtenerDatosTablaLlaves(objetoTablaLlave.tablaDestino, objetoTablaLlave.columnaDestino, objetoTablaLlave.nombreMostrar, campoCompuesto);
                                if (valoresCargar.Rows.Count > 0)
                                    valor = valoresCargar.Rows[0][columna].ToString();
                                cbo = controlCombo(dtTablaChimbis, "nombre", objetoTablaLlave.columnaDestino, controlID, valor);
                                pl.FindControl(divID).Controls.Add(cbo);
                            }
                            catch (Exception ex)
                            {                                                                
                                HtmlGenericControl divFallaCargarControl = new HtmlGenericControl("div");
                                divFallaCargarControl.Style.Add("background", "red");
                                divFallaCargarControl.Style.Add("color", "white");
                                divFallaCargarControl.Style.Add("text-align", "center");
                                divFallaCargarControl.Style.Add("text-transform", "uppercase");
                                divFallaCargarControl.Style.Add("height", "100%");
                                divFallaCargarControl.Style.Add("width", "100%");
                                divFallaCargarControl.InnerHtml = "Falla al cargar";
                                pl.FindControl(divID).Controls.Add(divFallaCargarControl);
                                //TODO: Acumular en un string los errores  de los nombres de los combos que van fallando para tan pronto cargue el formulario muestre cuales no cargaron
                            }
                        }
                        else
                        {
                            switch (objetoTabla.type_name)
                            {
                                case "tinyint": case "smallint": case "int": case "bigint":
                                    txt = ControlTextBox(controlID, precision, esNulo, esIdentidad);
                                    pl.FindControl(divID).Controls.Add(txt);
                                    if (valoresCargar.Rows.Count > 0 && !(valoresCargar.Rows[0][columna].Equals(string.Empty)))
                                        txt.Text = valoresCargar.Rows[0][columna].ToString();
                                    break;
                                case "decimal": case "float": case "numeric":
                                    txt = ControlTextBox(controlID, precision, esNulo, esIdentidad);
                                    pl.FindControl(divID).Controls.Add(txt);
                                    pl.FindControl(divID).Controls.Add(ControlFiltroTextBoxExtender(controlID, precision));
                                    if (valoresCargar.Rows.Count > 0 && !(valoresCargar.Rows[0][columna].Equals(string.Empty)))
                                        txt.Text = valoresCargar.Rows[0][columna].ToString();
                                    break;
                                case "char": case "nvarchar": case "varchar":
                                    txt = ControlTextBox(controlID, longitudMax, esNulo, esIdentidad);
                                    pl.FindControl(divID).Controls.Add(txt);
                                    if (valoresCargar.Rows.Count > 0 && !(valoresCargar.Rows[0][columna].Equals(string.Empty)))
                                        txt.Text = valoresCargar.Rows[0][columna].ToString();
                                    break;
                                case "datetime": case "date": case "smalldatetime":
                                    txt = ControlTextBox(controlID, 0, esNulo, esIdentidad);
                                    pl.FindControl(divID).Controls.Add(txt);
                                    //tagsHTML(pl, controlID, "span", "glyphicon glyphicon-calendar", controlID);
                                    pl.FindControl(divID).Controls.Add(ControlCalendarioExtender(identificador, controlID));
                                    if (valoresCargar.Rows.Count > 0 && !(valoresCargar.Rows[0][columna].Equals(string.Empty)))
                                        txt.Text = Convert.ToDateTime(valoresCargar.Rows[0][columna]).ToString("yyyy-MM-dd");
                                    break;
                                case "bit":
                                    chk = ControlCheckBox(controlID);
                                    pl.FindControl(divID).Controls.Add(chk);
                                    if (valoresCargar.Rows.Count > 0 && !(valoresCargar.Rows[0][columna].Equals(string.Empty)))
                                        chk.Checked = Convert.ToBoolean(valoresCargar.Rows[0][columna].ToString());
                                    break;
                                case "money":
                                    regexp = ControlExpresionesRegulares(controlID);
                                    pl.FindControl(divID).Controls.Add(regexp);
                                    if (valoresCargar.Rows.Count > 0 && !(valoresCargar.Rows[0][columna].Equals(string.Empty)))
                                        regexp.Text = valoresCargar.Rows[0][columna].ToString();
                                    break;
                            }
                        }
                        if (contadorDivisiones == divisiones)//Reiniciar las divisiones
                            contadorDivisiones = 1;
                        else
                            contadorDivisiones++;
                        identificador++;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Implementar mensaje de error
            }
        }
        #endregion        

        #region Controles
        /// <summary>
        /// Control asp label generico
        /// </summary>
        /// <param name="objetoTabla"></param>
        /// <param name="descripcionCampos"></param>
        /// <returns></returns>
        private Label ControlLabel(EspecificacionObjeto objetoTabla, bool descripcionCampos)
        {
            Label label = new Label();
            string descripcionOpcionalCampo = string.Empty;
            if (descripcionCampos)
            {
                switch (objetoTabla.type_name)
                {
                    case "tinyint": case "smallint": case "int": case "bigint":
                        descripcionOpcionalCampo = "(Numérico Máx " + objetoTabla.precision + ")";
                        break;
                    case "decimal": case "float": case "numeric":
                        descripcionOpcionalCampo = "(Numérico Máx " + Convert.ToDecimal(objetoTabla.precision) + " Caracteres)";
                        break;
                    case "char": case "nvarchar": case "varchar":
                        descripcionOpcionalCampo = "(Texto Máx " + objetoTabla.max_length + " Caracteres)";
                        break;
                    case "datetime": case "date": case "smalldatetime":
                        descripcionOpcionalCampo = "(Fecha yyyy-mm-dd)";
                        break;
                    case "bit":
                        descripcionOpcionalCampo = "(1-Si,Activo - 0-No,Inactivo)";
                        break;
                    case "money":
                        descripcionOpcionalCampo = "(Moneda Máx " + objetoTabla.precision + ")";
                        break;
                }
            }
            label.Text = string.Join("", Regex.Split(objetoTabla.column_name, "(?<!^)(?=[A-Z])")) + " " + descripcionOpcionalCampo;
            label.CssClass = "text-left";
            return label;
        }

        /// <summary>
        /// Control asp textbox generico
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="longitudMaxima"></param>
        /// <param name="esNulo"></param>
        /// <param name="esIdentidad"></param>
        /// <returns></returns>
        private TextBox ControlTextBox(string identificador, int longitudMaxima, bool esNulo, bool esIdentidad)
        {
            System.Web.UI.WebControls.TextBox controlTexto = new System.Web.UI.WebControls.TextBox();
            controlTexto.ID = identificador;
            if (longitudMaxima >= 30)
            {
                controlTexto.TextMode = TextBoxMode.MultiLine;
                controlTexto.Rows = 3;
            }
            if(longitudMaxima != 0)
                controlTexto.MaxLength = longitudMaxima;
            if (esNulo)
                controlTexto.Attributes.Add("placeholder", "Opcional");
            else
            {
                controlTexto.Attributes.Add("placeholder", "Requerido");
                controlTexto.Style.Add("border-color", "red");
            }
            if(esIdentidad)
            {
                controlTexto.Text="Asignado por el sistema";
                controlTexto.Enabled = false;
            }
            controlTexto.CssClass = "form-control";
            controlTexto.Style.Add("width","100%");
            return controlTexto;
        }            
        
        /// <summary>
        /// Control asp dropdownlist generico
        /// </summary>
        /// <param name="fuenteInfo"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="identificador"></param>
        /// <param name="seleccion"></param>
        /// <returns></returns>
        private DropDownList controlCombo(DataTable fuenteInfo, string textField, string valueField, string identificador, string seleccion)
        {
            DropDownList controlCombo = new DropDownList();
            controlCombo.ID = identificador;
            controlCombo.Width = Unit.Percentage(100);
            controlCombo.CssClass = "form-control";
            controlCombo.DataSource = fuenteInfo;
            controlCombo.DataTextField = textField;
            controlCombo.DataValueField = valueField;            
            controlCombo.DataBind();
            if (!seleccion.Equals(string.Empty))
                controlCombo.SelectedValue = seleccion;
            return controlCombo;
        }

        /// <summary>
        /// Control ajax control toolkit adicional para mostrar los caledarios, usa como base un textbox de asp generico
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="textboxID"></param>
        /// <returns></returns>
        private AjaxControlToolkit.CalendarExtender ControlCalendarioExtender(int identificador, string textboxID)
        {
            ImageButton botonImagen = ControlBotonImagen(identificador, true);            
            AjaxControlToolkit.CalendarExtender calendarioExtender = new AjaxControlToolkit.CalendarExtender();
            calendarioExtender.TargetControlID = textboxID;
            //calendarioExtender.CssClass = 
            calendarioExtender.Format = "yyyy-MM-dd";
            calendarioExtender.PopupButtonID = botonImagen.ID;
            return calendarioExtender;
        }

        /// <summary>
        /// Control asp imageButton generico para las imagenes que requieran los controles (usado por la libreria ajaxcontroltoolkit)
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="esParaCalendario"></param>
        /// <returns></returns>
        private ImageButton ControlBotonImagen(int identificador, bool esParaCalendario)
        {
            ImageButton imagenBoton = new ImageButton();
            if (esParaCalendario)
                imagenBoton.ID = "imagen_seleccionar_TextBox" + identificador;
            else
                imagenBoton.ID = "imagen_" + identificador;
            imagenBoton.ImageUrl = "~/Images/calendario.png";
            imagenBoton.AlternateText = "Mostrar calendario";
            imagenBoton.CausesValidation = false;
            return imagenBoton;
        }

        /// <summary>
        /// Control asp checkbox generico
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        private CheckBox ControlCheckBox(string identificador)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.ID = identificador;
            //checkbox.Text = "  Activo";
            checkbox.Style.Add("margin-top","5px");
            checkbox.Width = Unit.Percentage(100);
            return checkbox;
        }

        /// <summary>
        /// Control de filtros de los campos ajaxcontroltoolkit para las validaciones y control de tipos de caracteres
        /// </summary>
        /// <param name="controlID"></param>
        /// <param name="longitudMaxima"></param>
        /// <returns></returns>
        private AjaxControlToolkit.FilteredTextBoxExtender ControlFiltroTextBoxExtender(string controlID, int longitudMaxima)
        {
            AjaxControlToolkit.FilteredTextBoxExtender filtroExtender = new AjaxControlToolkit.FilteredTextBoxExtender();
            filtroExtender.TargetControlID = controlID;
            filtroExtender.ID = "masked_" + controlID;
            filtroExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
            return filtroExtender;
        }

        /// <summary>
        /// Control asp de expresiones regulares para validaciones de controles generico
        /// </summary>
        /// <param name="identificador"></param>
        /// <returns></returns>
        private System.Web.UI.WebControls.RegularExpressionValidator ControlExpresionesRegulares(string identificador)
        {
            System.Web.UI.WebControls.RegularExpressionValidator expresionRegular = new System.Web.UI.WebControls.RegularExpressionValidator();
            expresionRegular.ID = "masked_" + identificador;
            expresionRegular.ErrorMessage = "Verifique el valor";
            expresionRegular.ControlToValidate = identificador;
            expresionRegular.ValidationExpression = "^\\d{1,9}(.\\d{0,4})?$";
            expresionRegular.SetFocusOnError = true;
            expresionRegular.Text = "*";
            return expresionRegular;
        }

        /// <summary>
        /// Control de creacion de tags de html (En comentarios por ahora hasta que se realicen modificiaciones en los que se requiera mejorar la forma de pintar el formulario)
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="identificador"></param>
        /// <param name="tag"></param>
        /// <param name="clase"></param>
        /// <param name="controlID"></param>
        //private void tagsHTML(PlaceHolder pl, string identificador, string tag, string clase, string controlID)
        //{
        //    HtmlGenericControl tagGenerico = new HtmlGenericControl(tag);
        //    string tagID = "TAG" + identificador;
        //    tagGenerico.ID = tagID;
        //    tagGenerico.Attributes.Add("class", clase);
        //    pl.FindControl(controlID).Controls.Add(tagGenerico);
        //}
        #endregion

        #region Insercion-Actualizacion
        /// <summary>
        /// Metodo que genera el query de insercion dinamico recorriendo la estructura html generada
        /// </summary>
        /// <param name="nombreTabla"></param>
        /// <param name="atributos"></param>
        /// <returns></returns>
        public Hashtable insercionObjeto(string nombreTabla, Hashtable atributos)
        {            
            IDictionaryEnumerator i = default(IDictionaryEnumerator);
            Hashtable key = default(Hashtable);
            int identity = 0;
            bool hasIdentity = false;
            adminNegocio = new AdministracionNegocio();
            IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(nombreTabla);
            key = new Hashtable();
            hasIdentity = false;
            string nombreColumnaIdentidad = "";
            foreach (EspecificacionObjeto o in detalleTabla)
            {
                if (o.is_identity)
                {
                    nombreColumnaIdentidad = o.column_name;
                    hasIdentity = true;
                    break;
                }
            }
            string querySQLinsercionActualizacion = construirInsercionActualizacion(nombreTabla, atributos, detalleTabla, null);
            //Usar la transacción si existe.
            try
            {
                if (hasIdentity)
                    identity = adpatadorEscalar(querySQLinsercionActualizacion + "; select " + nombreColumnaIdentidad + " from " + nombreTabla + " order by 1 desc");
                else
                    adaptadorNonQuery(querySQLinsercionActualizacion);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                switch (ex.Errors[0].Number)
                {
                    case 2601:
                        //Error de Duplicacion de Indices
                        throw new InvalidOperationException("YA EXISTE UN REGISTRO IGUAL CON ESA INFORMACION");
                    case 2627:
                        //Error de Duplicacion de Indices
                        throw new InvalidOperationException("YA EXISTE UN REGISTRO IGUAL CON ESA INFORMACION");
                    case 547:
                        //CheckConstraint Violation
                        throw new InvalidConstraintException("DEBE VERIFICAR LOS RANGOS ...");
                    case 220:
                        throw new InvalidConstraintException("DEBE VERIFICAR EL NÚMERO DE SEMANA Y/O EL AÑO ...");
                    default:
                        throw ex;
                }
            }
            catch (Exception exep)
            {
                throw exep;
            }
            //Guardar en una colección la llave del objeto ingresado.
            foreach (EspecificacionObjeto o in detalleTabla)
            {
                if (Convert.ToBoolean(o.is_primary_key))
                {
                    if (o.is_identity)
                    {
                        key.Add(o.column_name, identity);
                    }
                    else
                    {
                        //key.Add(o.column_name, atributos(o.column_name));
                    }
                }
            }
            return key;
        }

        /// <summary>
        /// Metodo que genera el query de actualizacion dinamico recorriendo la estructura html generada
        /// </summary>
        /// <param name="nombreTabla"></param>
        /// <param name="atributosObjeto"></param>
        /// <param name="llaveObjeto"></param>
        public void ActualizarObjeto(string nombreTabla, Hashtable atributosObjeto, Hashtable llaveObjeto)
        {
            try
            {
                AdministracionNegocio adminNegocio = new AdministracionNegocio();
                IList<EspecificacionObjeto> atributos = adminNegocio.ConsultarEspecificacion(nombreTabla);
                string finalCmd = construirInsercionActualizacion(nombreTabla, atributosObjeto, atributos, llaveObjeto);
                adaptadorNonQuery(finalCmd);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                switch (ex.Errors[0].Number)
                {
                    case 2627:
                        //Error de Duplicacion de Indices
                        throw new InvalidOperationException("YA EXISTE UN REGISTRO IGUAL CON ESA INFORMACION");
                    case 2601:
                        //Error de Duplicacion de Indices
                        throw new InvalidOperationException("YA EXISTE UN REGISTRO IGUAL CON ESA INFORMACION");
                    case 547:
                        //CheckConstraint Violation
                        throw new InvalidConstraintException("DEBE VERIFICAR LOS RANGOS ...");
                    case 220:
                        throw new InvalidConstraintException("DEBE VERIFICAR EL NÚMERO DE SEMANA Y/O EL AÑO ...");
                    default:
                        throw ex;
                }
            }
            catch (Exception exep)
            {
                throw exep;
            }
        }
        #endregion

        #region Obtencion de atributos
        /// <summary>
        /// Metodo que retorna la recoleccion de atributos (tipos de datos y conversiones) de la tabla que recibe como parametro y la peticion http
        /// </summary>
        /// <param name="nombreObjeto"></param>
        /// <param name="requestObject"></param>
        /// <returns></returns>
        public Hashtable valorAtributos(string nombreObjeto, HttpRequest requestObject)
        {
            string key = null;
            Hashtable atributosObjeto = new Hashtable();
            bool hasValue = false;
            short i = 0;
            adminNegocio = new AdministracionNegocio();
            IList<EspecificacionObjeto> detalleTabla = adminNegocio.ConsultarEspecificacion(nombreObjeto);
            foreach (EspecificacionObjeto o in detalleTabla)
            {
                hasValue = false;
                i = 0;
                while (((i < requestObject.Form.Keys.Count - 1) & (!hasValue)))
                {
                    key = requestObject.Form.Keys[i];
                    string match = nombreObjeto.Replace(".", "_") + "_" + o.column_name;
                    if (key.Contains(match) & !key.Contains("masked"))
                    {
                        switch (o.column_name)
                        {
                            case "bit":
                                if (requestObject.Form[key] == "on")
                                    atributosObjeto.Add(o.column_name, "1");
                                else
                                    atributosObjeto.Add(o.column_name, "0");
                                break;
                            default:
                                if (string.IsNullOrEmpty(requestObject.Form[key]))
                                    atributosObjeto.Add(o.column_name, "null");
                                else
                                {
                                    if (!o.is_identity)
                                        atributosObjeto.Add(o.column_name, requestObject.Form[key]);
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
                        switch (o.type_name)
                        {
                            case "tinyint": case "smallint": case "int": case "decimal": case "numeric":
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

        #region Obtencion de tabla llaves foraneas
        private DataTable ObtenerDatosTablaLlaves(string tablaDestino, string columnaValueDestino, string columnaTextDestino, bool campoCompuesto)
        {
            string consulta = string.Empty;
            DataTable tablaLlaves = new DataTable();
            //TODO: Validar si se puede controlar que cada control desde las llavesDTO sepuede agregar si es campo compuesto para agilizar las construcciones
            //TODO: Quitar del metodo de armado principal el campo compuesto para que no genere error
            try
            {/*Se intenta inicialmente tratando de consultar directamente a la tabla*/
                consulta = "(select " + columnaValueDestino + ", nombre from " + tablaDestino + " union (select null, 'Seleccione...') ) order by 2 asc";
                tablaLlaves = adaptador(consulta);
            }
            catch (Exception)/*Sino se intenta con la vista anteponiendo una V al nombre de la tabla*/
            {
                try
                {
                    consulta = "(select " + columnaValueDestino + ", nombre from V" + tablaDestino + " union (select null, 'Seleccione...') )order by 2 asc";
                    tablaLlaves = adaptador(consulta);
                }
                catch (Exception)
                {
                    try
                    {//Si el campo es compuesto separado por '-' contra la tabla
                        consulta = "(select " + columnaValueDestino + ", " + columnaTextDestino.Split('-')[0] + "+ '-' + " + columnaTextDestino.Split('-')[1] +
                        " as nombre from " + tablaDestino + " union (select null, 'Seleccione...') ) order by 2 asc";
                        tablaLlaves = adaptador(consulta);
                    }
                    catch (Exception)
                    {
                        try
                        {//Si el campo es compuesto separado por '-' contra la vista
                            consulta = "(select " + columnaValueDestino + ", " + columnaTextDestino.Split('-')[0] + "+ '-' + " + columnaTextDestino.Split('-')[1] +
                                       " as nombre from V" + tablaDestino + " union (select null, 'Seleccione...') ) order by 2 asc";
                            tablaLlaves = adaptador(consulta);
                        }
                        catch (Exception)
                        {//Se intenta con el nombre del campo que se recibe y con la tabla normal
                            try
                            {
                                consulta = "(select " + columnaValueDestino + ", " + columnaTextDestino + " as nombre from " + tablaDestino + " union (select null, 'Seleccione...') ) order by 2 asc";
                                tablaLlaves = adaptador(consulta);
                            }
                            catch (Exception)
                            {//Se intenta con el nombre del campo que se recibe y con la vista
                                consulta = "(select " + columnaValueDestino + ", " + columnaTextDestino + " as nombre from V" + tablaDestino + " union (select null, 'Seleccione...') ) order by 2 asc";
                                tablaLlaves = adaptador(consulta);
                            }
                            
                        }                        
                    }                    
                }
            }
            return tablaLlaves;
        }
        #endregion

        #region Obtencion de datos de la tabla
        /// <summary>
        /// Retorna un datatable con la informacion de la tabla
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="objectKey"></param>
        /// <returns></returns>
        //TODO: Pendiente mejorar a nueva arquitectura con reflection para mayor rendimiento y usar la nueva teconologia
        public DataTable cargarInformacionTabla(string nombreTabla, Hashtable llave)
        {
            string sqlSelect = string.Empty, sqlIsNull = string.Empty, cadenacortada = string.Empty;
            IDictionaryEnumerator i = default(IDictionaryEnumerator);
            AdministracionNegocio adminNegocio = new AdministracionNegocio();
            IList<EspecificacionObjeto> esquema = adminNegocio.ConsultarEspecificacion(nombreTabla);
            sqlSelect = "select * from " + nombreTabla + " ";
            sqlSelect += "where ";
            i = llave.GetEnumerator();
            while (i.MoveNext())
            {
                foreach (EspecificacionObjeto e in esquema)
                {
                    if (i.Key.ToString().Equals(e.column_name))
                    {
                        if (e.type_name.Equals("datetime"))
                        {
                            if (!(e.column_name.ToUpper().Equals("UPDATEDDATE") && i.Value.Equals(string.Empty)))
                                sqlSelect += "CAST([" + e.column_name + "] as DATE) = ";
                                //sqlSelect += "(DATEADD(ms, -DATEPART(ms, [" + e.column_name + "]), [" + e.column_name + "]) = ";
                        }
                        else
                            if (!(e.column_name.ToUpper().Equals("UPDATEDBY") && i.Value.Equals(string.Empty)))
                            {
                                sqlSelect += "([" + e.column_name + "] = ";
                                sqlIsNull = " OR [" + e.column_name + "] IS NULL) and ";
                            }
                        switch (e.type_name)
                        {
                            case "tinyint": case "smallint": case "int": case "decimal": case "numeric":
                                if (i.Value.ToString().Equals(string.Empty))
                                    sqlSelect += "'') and ";
                                else
                                    sqlSelect += i.Value + ") and ";
                                break;
                            case "bit":
                                sqlSelect += (i.Value.Equals("True")?1:0) + ") and ";
                                break;
                            case "char": case "nvarchar": case "varchar":
                                if (!((e.column_name.ToUpper().Equals("UPDATEDDATE") || e.column_name.ToUpper().Equals("UPDATEDBY")) && i.Value.Equals(string.Empty)))
                                    sqlSelect += "'" + i.Value + "') and ";
                                break;
                            case "datetime": case "date": case "smalldatetime":
                                if (!((e.column_name.ToUpper().Equals("UPDATEDDATE") || e.column_name.ToUpper().Equals("UPDATEDBY")) && i.Value.Equals(string.Empty)))
                                    //if(!e.column_name.ToUpper().Equals("CREATEDDATE"))
                                        sqlSelect += "'" + convertirFecha(i.Value.ToString()) + "' and ";
                                break;
                        }
                        if (i.Value.Equals(string.Empty))
                        {
                            cadenacortada = sqlSelect.Substring(0, sqlSelect.Length - 6);
                            if (cadenacortada.Contains("where"))//Previene que se corte la cadena con la sentencia where
                                sqlSelect = cadenacortada + sqlIsNull;
                        }
                    }
                }
            }
            sqlSelect = sqlSelect.Substring(0, sqlSelect.Length - 5);
            return adaptador(sqlSelect);
        }
        #endregion

        #region Atributo por nombre
        /// <summary>
        /// Obtener el objeto particular de un listado de objetos Esquema
        /// </summary>
        /// <param name="nombreAtributo"></param>
        /// <param name="detalleTabla"></param>
        /// <returns></returns>
        private EspecificacionObjeto obtenerAtributoPorNombre(string nombreAtributo, IList<EspecificacionObjeto> detalleTabla)
        {   
            return detalleTabla.Where(dt => dt.column_name == nombreAtributo).FirstOrDefault();
        }

        /// <summary>
        /// Obtener el objeto particular de un listado de objetos Llaves
        /// </summary>
        /// <param name="nombreAtributo"></param>
        /// <param name="detalleTabla"></param>
        /// <returns></returns>
        private Llave obtenerAtributoPorNombre(string nombreAtributo, IList<Llave> llaves)
        {
            return llaves.Where(dt => dt.columnaOrigen == nombreAtributo).FirstOrDefault();
        }
        #endregion

        #region Eventos botones
        /// <summary>
        /// Evento generico del boton que dispara la insercion o la actualizacion del formulario y retorna al formulario de la tabla que lista la informacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Hashtable atributos = default(Hashtable);
            try
            {
                string tabla = (string)Session["tabla"];
                atributos = valorAtributos(tabla, Request);
                guardarEstadoFormulario(Request);
                //Consulta si es actualizacion o insercion
                if (Session["llave"] != null)//Actualizacion
                {
                    llave = (Hashtable)Session["llave"];
                    ActualizarObjeto(tabla, atributos, llave);
                    Response.Redirect("~/ModGeneral/frmTablasDinamicas.aspx?tabla=" + tabla);
                }
                else//Insercion
                {
                    llave = insercionObjeto(tabla, atributos);
                    Session["llave"] = llave;
                    Response.Redirect("~/ModGeneral/frmTablasDinamicas.aspx?tabla=" + tabla);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
        #endregion

        #region Guardar estado del formulario
        /// <summary>
        /// Guarda el estado del formulario para su posterior recorrido para mantener la integridad entre peticiones http
        /// </summary>
        /// <param name="objetoRequest"></param>
        private void guardarEstadoFormulario(HttpRequest objetoRequest)
        {
            try
            {
                Hashtable hashControles = new Hashtable();
                List<string> informacionControles = new List<string>();
                string cadenaInfo = "";
                IDictionaryEnumerator elemento = null;
                Hashtable hashValorControles = new Hashtable();
                if ((Session["controlesFormulario"] != null))
                {
                    hashControles = (Hashtable)Session["controlesFormulario"];
                    elemento = hashControles.GetEnumerator();
                    while (elemento.MoveNext())
                    {
                        string llave = elemento.Key.ToString();
                        if (!(llave.Contains("CreatedBy") | llave.Contains("UpdatedBy") | llave.Contains("CreatedDate") | llave.Contains("UpdatedDate")))
                        {
                            hashValorControles.Add(elemento.Key, Request.Form[elemento.Key.ToString()]);
                            cadenaInfo = llave + "," + elemento.Value + "," + Request.Form[elemento.Key.ToString()];
                            informacionControles.Add(cadenaInfo);
                        }
                    }
                }
                Session["estadoControles"] = informacionControles;
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
        #endregion

        #region Construccion de querys dinamicos - OK FALTA REVISAR COMO ACTUALIZAR SIN MODIFICAR LOS LOGS DE CREACION
        /// <summary>
        /// Construccion dinamica de los querys de insercion y actualizacion completos con sus datos ya transformados para retornar el string final a ejecutar
        /// </summary>
        /// <param name="nombreObjeto"></param>
        /// <param name="atributos"></param>
        /// <param name="detalleTabla"></param>
        /// <param name="objetoAntiguo"></param>
        /// <returns></returns>
        //TODO: Actualizar a nueva arquitectura con reflection
        private string construirInsercionActualizacion(string nombreObjeto, Hashtable atributos, 
            IList<EspecificacionObjeto> detalleTabla, Hashtable objetoAntiguo)
        {
            string sqlInsert = "set dateformat ymd insert into " + nombreObjeto + " (", sqlIsNull = string.Empty;
            string sqlValues = "values (";
            string sqlUpdate = "set dateformat ymd update " + nombreObjeto + " set ";
            bool updating = false;
            string updatingValues = string.Empty, targetColumns = string.Empty, targetValues = string.Empty, currentColumn = string.Empty, valorActual = string.Empty, finalCmd = string.Empty, whereUpdate = string.Empty;
            IDictionaryEnumerator i = atributos.GetEnumerator();
            Persona persona = (Persona)Session["Persona"];
            updating = (objetoAntiguo != null);
            while (i.MoveNext())
            {
                //De acuerdo al tipo de dato, insertar el valor y el nombre del atributo.
                EspecificacionObjeto atributoActual = obtenerAtributoPorNombre(i.Key.ToString(), detalleTabla);
                string columna = atributoActual.column_name.ToUpper();
                if (!columna.Equals("UPDATEDDATE") && !columna.Equals("UPDATEDBY"))
                    targetColumns += "[" + atributoActual.column_name + "], ";
                if (!columna.Equals("CREATEDDATE") && !columna.Equals("CREATEDBY"))
                    updatingValues += "[" + atributoActual.column_name + "]=";
                if (atributoActual.is_identity && (!updating))
                {
                    throw new Exception("No pueden guardarse explícitamente los atributos de tipo autonumérico.");
                }
                valorActual = i.Value.ToString();
                switch (columna)
                {
                    case "CREATEDBY": case "UPDATEDBY":
                        valorActual = "'" + Session["login"].ToString() + "'";
                        break;
                    case "CREATEDDATE": case "UPDATEDDATE":
                        valorActual = "GETDATE()";
                        break;
                    default:
                        switch (atributoActual.type_name)
                        {
                            case "tinyint": case "smallint": case "int": case "bigint":
                                if (valorActual.Equals(""))
                                    valorActual = "0";
                                break;
                            case "char": case "nvarchar": case "varchar": case "decimal": case "float": case "numeric":
                                if (!(valorActual == "null"))
                                    valorActual = "'" + valorActual + "'";
                                else
                                    valorActual = "null";
                                break;
                            case "datetime": case "date": case "smalldatetime":
                                if (!(valorActual == "null"))
                                    valorActual = "'" + valorActual + "'";
                                else
                                    valorActual = "null";
                                break;
                            case "bit":
                                if (valorActual == "on")
                                    valorActual = "1";
                                else
                                    valorActual = "0";
                                break;
                            case "money":
                                valorActual = valorActual.Replace(".", ",");
                                int index = valorActual.LastIndexOf(",");
                                if (index != -1)
                                {
                                    valorActual = valorActual.Insert(index, ".");
                                    valorActual = valorActual.Remove(index + 1, 1);
                                    valorActual = valorActual.Replace(",", "");
                                }
                                break;
                        }
                        break;
                }
                if (!columna.Equals("UPDATEDDATE") && !columna.Equals("UPDATEDBY"))
                    targetValues += valorActual + ", ";
                if (!columna.Equals("CREATEDDATE") && !columna.Equals("CREATEDBY"))
                    updatingValues += valorActual + ", ";
            }
            sqlInsert = sqlInsert + targetColumns.Substring(0, targetColumns.Length - 2) + ") ";
            sqlValues = sqlValues + targetValues.Substring(0, targetValues.Length - 2) + ") ";
            sqlUpdate = sqlUpdate + updatingValues.Substring(0, updatingValues.Length - 2) + " where ";
            finalCmd = sqlInsert + sqlValues;
            if (updating)
            {
                i = objetoAntiguo.GetEnumerator();
                updatingValues = string.Empty;
                while (i.MoveNext())
                {
                    //De acuerdo al tipo de dato, insertar el valor y el nombre del atributo.
                    EspecificacionObjeto atributoActual = obtenerAtributoPorNombre(i.Key.ToString(), detalleTabla);
                    string columna = atributoActual.column_name.ToUpper();
                    currentColumn = atributoActual.column_name;
                    if (!columna.Equals("CREATEDDATE") && !columna.Equals("UPDATEDDATE"))//Si el campo es la fecha de creacion/Actualizacion lo omite para poder realizar el where
                    {
                        if(!(columna.Equals("UPDATEDBY") && i.Value.Equals(string.Empty)))
                        {
                            if (updatingValues.Equals(string.Empty))
                                updatingValues = "([" + currentColumn + "]=";
                            else
                                updatingValues += "([" + currentColumn + "]=";
                            sqlIsNull = " OR [" + currentColumn + "] IS NULL) and ";
                            valorActual = i.Value.ToString();
                            switch (columna)
                            {
                                case "UPDATEDBY":
                                    valorActual = "'" + valorActual + "'";
                                    break;
                                case "UPDATEDDATE":
                                    valorActual = "GETDATE()";
                                    break;
                                default:
                                    switch (atributoActual.type_name)
                                    {
                                        case "tinyint": case "smallint": case "int": case "bigint":
                                            if (valorActual.Equals(""))
                                                valorActual = "0" + sqlIsNull.Substring(0,sqlIsNull.Length - 6);
                                            break;
                                        case "char": case "nvarchar": case "varchar": case "decimal": case "float": case "numeric":
                                            if (!(valorActual == "null"))
                                                valorActual = "'" + valorActual + "'";
                                            else
                                                valorActual = "null";
                                            break;
                                        case "datetime": case "date": case "smalldatetime":
                                            if (!(valorActual == "null"))
                                                valorActual = "'" + convertirFecha(valorActual) + "'";
                                            else
                                                valorActual = "null";
                                            break;
                                        case "bit":
                                            if (valorActual == "on" || valorActual == "True")
                                                valorActual = "1";
                                            else
                                                valorActual = "0";
                                            break;
                                    }
                                    break;
                            }
                            updatingValues += valorActual + ") and ";
                            if (valorActual.Equals("''"))
                                updatingValues = updatingValues.Substring(0, updatingValues.Length - 6) + sqlIsNull;
                        }
                    }
                }
                finalCmd = sqlUpdate + updatingValues.Substring(0, updatingValues.Length - 5);
            }
            return finalCmd;
        }
        #endregion

        #region Convertir fecha
        private string convertirFecha(string fecha)
        {
            if (!fecha.Equals(string.Empty))
            {
                string fechaStandarISO8601 = string.Empty;
                string[] cadenaSplit = fecha.Split(' ');
                string cadena = cadenaSplit[0];
                fechaStandarISO8601 = cadena.Split('/')[2] + "-" + cadena.Split('/')[0] + "-" + cadena.Split('/')[1];
                cadena = cadenaSplit[1];
                string hora = cadena.Split(':')[0] + ":" + cadena.Split(':')[1] + ":" + cadena.Split(':')[2];

                if (hora.Equals("12:00:00"))
                    hora = "00:00:00.000";
                return fechaStandarISO8601;// +" " + hora;
            }
            return string.Empty;
        }
        #endregion

        #region Manejador de mensajes
        private void MostrarMensaje(string mensaje, bool esError)
        {
            string msg = mensaje;
            if(esError)
                msg = "Error: " + mensaje.Replace("'", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            RadNotificationMensajes.Show(msg);
        }
        #endregion

        #region Adaptador SQL
        /// <summary>
        /// Adaptador SQL para realizar consultas
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        //TODO: Actualizar a reflection con nueva arquitectura
        private static DataTable adaptador(string queryString)
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
        /// <summary>
        /// Adaptador SQL para realizar transacciones de insercion o actualizacion
        /// </summary>
        /// <param name="queryString"></param>
        //TODO: Actualizar a reflection con nueva arquitectura
        private static void adaptadorNonQuery(string queryString)
        {
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
        /// Adaptador SQL para realizar consultas escalares
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        //TODO: Actualizar a reflection con nueva arquitectura
        private static int adpatadorEscalar(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(conexion))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(queryString, connection);
                int resultado = (Int32)command.ExecuteScalar();
                connection.Close();
                return resultado;
            }
        }
        #endregion        
    }
}