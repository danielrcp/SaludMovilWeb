#region Imports
using System;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using System.Collections;
using Telerik.Web.UI;
//using SaludMovil.Servicios.PortalMedico;
using System.Collections.Generic;
//using SaludMovil.Portal.WSDiagnostico;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using SaludMovil.Transversales;
using System.Linq;
using System.Configuration;
using System.Web.UI.WebControls;
#endregion

/***
 * Descripción: Formulario que permite la creacion y parametrizacion de los programas, tipos de guias y sus correspondientes diagnosticos
 * Fecha creacion: 24 Mayo 2016
 * Creado por: Andres Felipe Silva Pascuas - afsp
 * Modificacion: 24 Mayo 2016 - Se modifica metodo de guardar para permitir guardar diagnosticos sin necesidad de ingresar frecuancia y nivel de riesgo
 * Modificacion: 22 Agosto 2016 - se añaden referencias a Ws colmedica
 * Modificacion: 1 Septiembre 2016 - se cambia la forma de construir las url del servidio REST para que sean mas coherentes y rapidas a la hora de consultar y se pasa el metodo 
 *               de consumir los servicios a las transversales para que se  pueden usar desde cualquier parte de la aplicacion
 * Modificacion: 27-28 Septiembre 2016 - Se agregan los ws por codigo de diagnosticos y de medicamentos con la implemetnacion de nuevo metodo de combinar las listas,
 *               se pasaron las URL para la clase constantes, se agrego el control de codigo de apertura para las prestaciones de colmedica
 * Modificacion: 8-9 Noviembre 2017 Se agrega campo de riesgo para los programas, se controla la grilla guia para que se comporte de manera diferente en las diferentes 
 *               combinaciones entre programas y componentes de guias asi como la informacion que se solicita
 * Modificacion: 25-26-27 Noviembre se cambia la forma de conulstar los tipos de guias con tabla realacional con ponderadores de cada tipo de guia              
 * 
 * */

namespace SaludMovil.Portal.ModAdmin
{
    public partial class DefinicionPrograma : System.Web.UI.Page
    {
        private ProgramaNegocio programaNegocio;
        private AdministracionNegocio adminNegocio;
        private PacienteNegocio negocioPaciente;
        //private ServiciosMedicos webService;
        public static string login;
        private string ocurrenciaInterconsulta = "INTERCONSULTA";
        protected void Page_Load(object sender, EventArgs e)
        {
            programaNegocio = new ProgramaNegocio();
            if (!Page.IsPostBack)
            {
                //TODO: Cambiar este login por la implemetacion que se debe hacer para la BDV2
                login = (String)Session["login"];
                if (Request.QueryString["cborg"] != null && Request.QueryString["cborg"].ToString().Equals("1"))//NOTA: cborg -> Combo riesgo
                    panelRiesgo.Visible = true;
            }
            //TODO: implementar personalizacion para las diferentes eps del mercado
            int eps = 1;//Colmedica
            personalizarFormulario(eps);
        }

        #region Grilla programa
        /// <summary>
        /// PreRenderizado de la grilla programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridPrograma_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (GridDataItem item in RadGridPrograma.MasterTableView.Items)
                {
                    if (item.GetDataKeyValue("idPrograma").ToString() == Request.QueryString["pgm"])
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Metodo que controla la fuente de datos de la grilla programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridPrograma_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                IList<Programa> programas = programaNegocio.listarProgramasSP();
                RadGridPrograma.DataSource = programaNegocio.listarProgramasSP();
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que controla la insercion de datos de la grilla programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridPrograma_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                RadComboBox rddlPoblacion = ((RadComboBox)item.FindControl("rddlPoblacion"));
                RadComboBox rddlEstados = ((RadComboBox)item.FindControl("rddlEstados"));
                RadComboBox rddlRiesgosPrograma = ((RadComboBox)item.FindControl("rddlRiesgoPro"));
                sm_Programa pro = new sm_Programa();
                pro.descripcion = valores["descripcion"].ToString().Trim();
                pro.idEstado = Convert.ToInt32(rddlEstados.SelectedValue);
                pro.poblacionObjetivo = Convert.ToInt32(rddlPoblacion.SelectedValue);
                pro.fechaInicio = Convert.ToDateTime(valores["fechaInicio"].ToString());
                pro.fechaFin = Convert.ToDateTime(valores["fechaFin"].ToString());
                pro.createdBy = login;
                pro.createdDate = DateTime.Now;
                pro.orden = Convert.ToInt32(valores["orden"].ToString());
                pro.idRiesgoPrograma = (!string.IsNullOrEmpty(rddlRiesgosPrograma.SelectedValue) ? Convert.ToInt32(rddlRiesgosPrograma.SelectedValue) : 0);
                programaNegocio.GuardarPrograma(pro);
                programaNegocio.InsertarTiposGuiasPrograma(programaNegocio.listarProgramas().Last().idPrograma);//Consulta el ultimo programa e inserta sus tiposguias
                RadNotificationMensajes.Show("Se agregó el programa");
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo qque controla la actualizacion de datos de la grilla programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridPrograma_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                RadComboBox rddlPoblacion = ((RadComboBox)item.FindControl("rddlPoblacion"));
                RadComboBox rddlEstados = ((RadComboBox)item.FindControl("rddlEstados"));
                RadComboBox rddlRiesgosPrograma = ((RadComboBox)item.FindControl("rddlRiesgoPro"));
                sm_Programa pro = new sm_Programa();
                pro.idPrograma = Convert.ToInt32(item.GetDataKeyValue("idPrograma").ToString());
                pro.descripcion = valores["descripcion"].ToString().Trim();
                pro.idEstado = Convert.ToInt32(rddlEstados.SelectedValue);
                pro.poblacionObjetivo = Convert.ToInt32(rddlPoblacion.SelectedValue);
                pro.fechaInicio = Convert.ToDateTime(valores["fechaInicio"].ToString());
                pro.fechaFin = Convert.ToDateTime(valores["fechaFin"].ToString());
                pro.createdBy = item.GetDataKeyValue("createdBy").ToString();//TODO: Revisar tema de actualizacion no permite actualizar si no se pasan los logs de creacion y al pasarlos los actualiza tambien y no deberia
                pro.createdDate = Convert.ToDateTime(item.GetDataKeyValue("createdDate").ToString());//TODO: Revisar tema de actualizacion no permite actualizar si no se pasan los logs de creacion y al pasarlos los actualiza tambien y no deberia
                pro.updatedBy = login;
                pro.updatedDate = DateTime.Now;
                pro.orden = Convert.ToInt32(valores["orden"].ToString());
                pro.idRiesgoPrograma = (!string.IsNullOrEmpty(rddlRiesgosPrograma.SelectedValue) ? Convert.ToInt32(rddlRiesgosPrograma.SelectedValue) : 0);
                programaNegocio.ActualizarPrograma(pro);
                IList<sm_TipoGuiaXPrograma> tiposGuiasPrograma = programaNegocio.ListarTiposGuiasXPrograma(pro.idPrograma);
                if (tiposGuiasPrograma.Count == 0)
                    programaNegocio.InsertarTiposGuiasPrograma(pro.idPrograma);
                RadNotificationMensajes.Show("Se actualizó el programa");
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Controla las acciones de la grilla programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridPrograma_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("RowClick") && e.Item.IsInEditMode != true)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    int idPrograma = Convert.ToInt32(item.GetDataKeyValue("idPrograma").ToString());
                    IList<sm_TipoGuiaXPrograma> tiposGuiasPrograma = programaNegocio.ListarTiposGuiasXPrograma(idPrograma);
                    if (tiposGuiasPrograma.Count == 0)
                        programaNegocio.InsertarTiposGuiasPrograma(idPrograma);
                    RadGridGuia.Rebind();
                    Response.Redirect((Request.QueryString["pgm"] != null ? "~/ModAdmin/DefinicionPrograma.aspx?tg=" + (Request.QueryString["tg"] != null ? Request.QueryString["tg"] : "0") + "&pgm=" + item.GetDataKeyValue("idPrograma") :
                        "~/ModAdmin/DefinicionPrograma.aspx?pgm=" + item.GetDataKeyValue("idPrograma")));
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Controla las acciones resultantes de la grilla programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridPrograma_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                cargarCombosGrillas(e);
            }
        }
        #endregion

        #region Grilla componentes de la guia
        /// <summary>
        /// PreRenderizado de la grilla de componentes para controlar la seleccion que se realiza
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridTipoGuia_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (GridDataItem item in RadGridTipoGuia.MasterTableView.Items)
                {
                    if (item.GetDataKeyValue("idTipoGuia").ToString() == Request.QueryString["tg"])
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Controla el manejo de la fuente de datos de la grilla de componentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridTipoGuia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                int idPrograma = (string.IsNullOrEmpty(Request.QueryString["pgm"]) ? 0 : Convert.ToInt32(Request.QueryString["pgm"]));
                IList<TipoGuia> tiposGuias = programaNegocio.ListarTiposGuiasSP(idPrograma).Where(tp => tp.version != "Servicios").ToList();
                RadGridTipoGuia.DataSource = tiposGuias;
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Controla la insercion de la grilla de componentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridTipoGuia_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                RadComboBox rddlEstados = ((RadComboBox)item.FindControl("rddlEstados"));
                sm_TipoGuia tg = new sm_TipoGuia();
                tg.descripcion = valores["descripcion"].ToString().Trim();
                tg.idEstado = Convert.ToInt32(rddlEstados.SelectedValue);
                tg.createdBy = login;
                tg.createdDate = DateTime.Now;
                tg.esPonderadoPorGrupo = (valores["esPonderadoPorGrupo"] == null ? false : Convert.ToBoolean(valores["esPonderadoPorGrupo"].ToString()));
                tg.ponderadorGrupo = (valores["ponderadorGrupo"] == null ? 0 : Convert.ToDecimal(valores["ponderadorGrupo"].ToString()));
                programaNegocio.GuardarTipoGuia(tg);
                RadNotificationMensajes.Show("Se agregó el tipo de guia");
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Controla las actualizaciones de la grilla de componentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridTipoGuia_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                RadComboBox rddlEstados = ((RadComboBox)item.FindControl("rddlEstados"));
                sm_TipoGuia tg = new sm_TipoGuia();
                tg.idTipoGuia = Convert.ToInt32(item.GetDataKeyValue("idTipoGuia").ToString());
                tg.descripcion = valores["descripcion"].ToString().Trim();
                tg.idEstado = Convert.ToInt32(rddlEstados.SelectedValue);
                tg.createdBy = item.GetDataKeyValue("createdBy").ToString();//TODO: Revisar tema de actualizacion no permite actualizar si no se pasan los logs de creacion y al pasarlos los actualiza tambien y no deberia
                tg.createdDate = Convert.ToDateTime(item.GetDataKeyValue("createdDate").ToString());//TODO: Revisar tema de actualizacion no permite actualizar si no se pasan los logs de creacion y al pasarlos los actualiza tambien y no deberia
                tg.updatedBy = login;
                tg.updatedDate = DateTime.Now;
                tg.esPonderadoPorGrupo = (valores["esPonderadoPorGrupo"] == null ? false : Convert.ToBoolean(valores["esPonderadoPorGrupo"].ToString()));
                tg.ponderadorGrupo = (valores["ponderadorGrupo"] == null ? 0 : Convert.ToDecimal(valores["ponderadorGrupo"].ToString()));
                int idPrograma = (string.IsNullOrEmpty(Request.QueryString["pgm"]) ? 0 : Convert.ToInt32(Request.QueryString["pgm"]));
                IList<TipoGuia> tiposGuias = programaNegocio.ListarTiposGuiasSP(idPrograma).Where(tp => tp.version != "Servicios" && tp.esPonderadoPorGrupo == true).ToList();
                if (CumpleTotalCienPorciento(null, null, true, tiposGuias, tg))
                {
                    programaNegocio.ActualizarTipoGuia(tg);
                    programaNegocio.ActualizarTiposGuiasPrograma(idPrograma, tg.idTipoGuia, Convert.ToInt32(tg.esPonderadoPorGrupo), (decimal)tg.ponderadorGrupo);
                    RadNotificationMensajes.Show("Se actualizó el tipo de guia");
                }
                else
                    RadNotificationMensajes.Show("No se puede actualizar el componente guia porque supera el 100% del ponderador");
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        protected void RadGridTipoGuia_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName.Equals("RowClick") && e.Item.IsInEditMode != true)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadGridGuia.Rebind();
                int mostrarRiesgo = 0;
                if (item.GetDataKeyValue("descripcion").ToString().ToUpper().Contains(ocurrenciaInterconsulta) || item.GetDataKeyValue("descripcion").ToString().ToUpper().Contains("OTROS EXÁMENES") || item.GetDataKeyValue("descripcion").ToString().ToUpper().Contains("EXAMEN") || item.GetDataKeyValue("descripcion").ToString().ToUpper().Contains("AYUDA"))
                    mostrarRiesgo = 1;
                Response.Redirect((Request.QueryString["pgm"] != null ? "~/ModAdmin/DefinicionPrograma.aspx?pgm=" + Request.QueryString["pgm"] + "&tg=" + item.GetDataKeyValue("idTipoGuia") + "&nmtg='" + item.GetDataKeyValue("descripcion") + "'&cborg=" + mostrarRiesgo :
                    "~/ModAdmin/DefinicionPrograma.aspx?tg=" + item.GetDataKeyValue("idTipoGuia") + "&nmtg='" + item.GetDataKeyValue("descripcion") + "'&cborg=" + mostrarRiesgo));
            }
        }

        protected void RadGridTipoGuia_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                cargarCombosGrillas(e);
            }
        }
        #endregion

        #region Grilla Guia
        /// <summary>
        /// Pre renderizado de la grilla guia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridGuia_PreRender(object sender, EventArgs e)
        {
            if (Request.QueryString["tg"].ToString().Equals("3"))//Educacion y habitos saludables
            {
                RadGridGuia.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = true;
                RadGridGuia.Rebind();
            }
            else//Todos los demas componentes de guia
            {
                RadGridGuia.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                RadGridGuia.Rebind();
            }
        }

        /// <summary>
        /// Need source de la grilla guia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridGuia_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                if (Request.QueryString["tg"] != null && Request.QueryString["pgm"] != null)
                {
                    pnlGrillaGuia.Visible = true;
                    if (Request.QueryString["tg"].Equals("3"))//Educacion y habitos saludables
                        panelDiagnosticos.Visible = false;
                    else
                        panelDiagnosticos.Visible = true;
                    pnlInstrucciones.Visible = false;
                    int idPrograma = Convert.ToInt32(Request.QueryString["pgm"]);
                    int idTipoGuia = Convert.ToInt32(Request.QueryString["tg"]);
                    List<sm_Guia> guias = programaNegocio.listarGuiasPorProgramaYTipo(idPrograma, idTipoGuia).Where(g => g.version != "Servicios").Where(g => g.version != "servicio").Where(g => g.version != "App").ToList();
                    if (Request.QueryString["tg"].ToString().Equals("3"))//Educacion y habitos saludables
                    {
                        foreach (sm_Guia g in guias)
                        {
                            if (g.ce_estadoDoc == 1)
                                g.descripcion = "Vigente";
                            else
                                g.descripcion = "No vigente";
                        }
                    }
                    RadGridGuia.DataSource = guias;
                }
                else
                {
                    pnlGrillaGuia.Visible = false;
                    panelDiagnosticos.Visible = false;
                    pnlInstrucciones.Visible = true;
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Control de inserciones de la grilla guia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridGuia_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                RadComboBox rddlEstadoDocCE = ((RadComboBox)item.FindControl("rddlEstadoDocCE"));
                sm_Guia guia = new sm_Guia();
                guia.idTipoGuia = Convert.ToInt32(Request.QueryString["tg"].ToString());
                guia.idCodigoTipo = string.Empty;
                guia.descripcion = "EDUCACION Y HABITOS SALUDABLES";
                guia.idPrograma = Convert.ToInt32(Request.QueryString["pgm"]);
                guia.idRiesgo = 4;
                guia.idEstado = 0;
                guia.createdBy = login;
                guia.createdDate = DateTime.Now;
                guia.updatedBy = login;
                guia.updatedDate = DateTime.Now;
                /*Campos que realmente son importantes en esta insercion*/
                guia.ce_titulo = valores["ce_titulo"].ToString();
                guia.ce_url = valores["ce_url"].ToString();
                guia.ce_estadoDoc = Convert.ToInt32(rddlEstadoDocCE.SelectedValue);
                guia.ce_fechaRegistro = Convert.ToDateTime(valores["ce_fechaRegistro"].ToString());
                programaNegocio.GuardarGuia(guia);
                RadNotificationMensajes.Show("Se agregó el regsitro a la guia");
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Control de actualizaciones de la grilla guia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridGuia_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                sm_Guia guia = new sm_Guia();
                guia.idGuia = Convert.ToInt32(item.GetDataKeyValue("idGuia").ToString());
                guia.idTipoGuia = Convert.ToInt32(item.GetDataKeyValue("idTipoGuia").ToString());
                guia.idCodigoTipo = item.GetDataKeyValue("idCodigoTipo").ToString();
                guia.descripcion = item.GetDataKeyValue("descripcion").ToString().Trim();
                guia.idPrograma = Convert.ToInt32(Request.QueryString["pgm"]);
                if (item.GetDataKeyValue("createdBy") != null)
                    guia.createdBy = item.GetDataKeyValue("createdBy").ToString();
                guia.createdDate = Convert.ToDateTime(item.GetDataKeyValue("createdDate").ToString());
                guia.updatedBy = login;
                guia.updatedDate = DateTime.Now;
                if (Request.QueryString["tg"].ToString().Equals("3"))//Educacion y habitos saludables
                {
                    guia.idRiesgo = Convert.ToInt32(item.GetDataKeyValue("idRiesgo").ToString());
                    RadComboBox rddlEstadoDocCE = ((RadComboBox)item.FindControl("rddlEstadoDocCE"));
                    guia.ce_titulo = valores["ce_titulo"].ToString();
                    guia.ce_url = valores["ce_url"].ToString();
                    guia.ce_estadoDoc = Convert.ToInt32(rddlEstadoDocCE.SelectedValue);
                    guia.ce_fechaRegistro = Convert.ToDateTime(valores["ce_fechaRegistro"].ToString());
                    programaNegocio.ActualizarGuia(guia);
                    RadNotificationMensajes.Show("Se actualizó la guía");
                }
                else//Todos los demas componentes de guia
                {
                    RadComboBox rddlRiesgos = ((RadComboBox)item.FindControl("rddlRiesgos"));
                    RadComboBox rddlEstados = ((RadComboBox)item.FindControl("rddlEstados"));
                    guia.idRiesgo = Convert.ToInt32(rddlRiesgos.SelectedValue);
                    guia.cantidadRiesgo = Convert.ToInt32(valores["cantidadRiesgo"].ToString());
                    guia.idEstado = Convert.ToInt32(rddlEstados.SelectedValue);
                    guia.peso = (valores["peso"] == null ? 0 : Convert.ToInt32(valores["peso"].ToString()));
                    guia.po_act = (valores["po_act"] == null ? 0 : Convert.ToDecimal(valores["po_act"]));//Ponderador de actividad
                    guia.po_grupoAct = (valores["po_act"] == null ? 0 : Convert.ToDecimal(valores["po_act"]) / guia.cantidadRiesgo);//Ponderador de actividad por grupo
                    int idPrograma = Convert.ToInt32(Request.QueryString["pgm"]);
                    int idTipoGuia = Convert.ToInt32(Request.QueryString["tg"]);
                    IList<sm_Guia> guias = programaNegocio.listarGuiasPorProgramaYTipo(idPrograma, idTipoGuia).Where(g => g.idGuia != guia.idGuia).ToList();
                    if (!existeGuia(guia.idCodigoTipo, Convert.ToInt32(guia.cantidadRiesgo), guia.idRiesgo, false, guia.idGuia, guias))
                    {
                        if (CumpleTotalCienPorciento(guias, guia, false, null, null))
                        {
                            programaNegocio.ActualizarGuia(guia);
                            RadNotificationMensajes.Show("Se actualizó la guía");
                        }
                        else
                            RadNotificationMensajes.Show("No se puede actualizar el registro de la guia porque se excede el 100% de uno o más ponderadores");
                    }
                    else
                    {
                        RadNotificationMensajes.Show("No se puede guardar la guía debido a que entra en conflicto con otra guía que tiene la misma información");
                    }
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Control de eventos de la grilla guia ACTALMENTE NO SE ESTA USANDO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridGuia_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //NO SE ESTA USANDO ACTUALMENTE
        }

        /// <summary>
        /// Control de la grilla guia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadGridGuia_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridEditableItem && e.Item.IsInEditMode)
                {
                    switch (Request.QueryString["tg"].ToString())
                    {
                        case "1":
                            //no se tiene en cuenta por ahora en las reglas
                            break;
                        default:
                            GridEditableItem item = (GridEditableItem)e.Item;
                            RadNumericTextBox numPeso = (RadNumericTextBox)item["peso"].Controls[0];
                            RadNumericTextBox po_grupoAct = (RadNumericTextBox)item["po_grupoAct"].Controls[0];
                            numPeso.Visible = false;
                            po_grupoAct.Visible = false;
                            break;
                    }
                    cargarCombosGrillas(e);
                }
                //Controla los campos de la grilla dependiendo de la combinacion de programas y componentes de la guia
                if (Request.QueryString["tg"].ToString().Equals("3"))
                    MostrarOcultarColumnasgrillaGuia(false);
                else
                    MostrarOcultarColumnasgrillaGuia(true);
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }

        }
        #endregion

        #region Metodos Combos de grillas
        /// <summary>
        /// Controla la carga de datos de todos los combos de las grillas del formulario
        /// </summary>
        /// <param name="e"></param>
        private void cargarCombosGrillas(GridItemEventArgs e)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            RadComboBox rddlPoblacion = ((RadComboBox)item.FindControl("rddlPoblacion"));
            RadComboBox rddlEstados = ((RadComboBox)item.FindControl("rddlEstados"));
            RadComboBox rddlRiesgos = ((RadComboBox)item.FindControl("rddlRiesgos"));
            RadComboBox rddlRiesgosPrograma = ((RadComboBox)item.FindControl("rddlRiesgoPro"));
            adminNegocio = new AdministracionNegocio();
            rddlEstados.DataSource = adminNegocio.ConsultarEstados("General");
            rddlEstados.DataValueField = "idEstado";
            rddlEstados.DataTextField = "nombre";
            rddlEstados.DataBind();
            string nombreGrillaPrograma = "ctl00_MainContent_RadGridPrograma_ctl00";
            string nombreGrillaTipoGuia = "ctl00_MainContent_RadGridTipoGuia_ctl00";
            string nombreGrillaGuia = "ctl00_MainContent_RadGridGuia_ctl00";
            if (e.Item.OwnerID.Equals(nombreGrillaPrograma))
            {
                adminNegocio = new AdministracionNegocio();
                rddlPoblacion.DataSource = adminNegocio.ListarPoblaciones();
                rddlPoblacion.DataValueField = "idPoblacion";
                rddlPoblacion.DataTextField = "descripcion";
                rddlPoblacion.DataBind();
                programaNegocio = new ProgramaNegocio();
                rddlRiesgosPrograma.DataSource = programaNegocio.ListarParametros().Where(p => p.nombreParametro == "RIESGO_PROG").ToList();
                rddlRiesgosPrograma.DataValueField = "valor";
                rddlRiesgosPrograma.DataTextField = "descripcion";
                rddlRiesgosPrograma.DataBind();
            }
            if (e.Item.OwnerID.Equals(nombreGrillaGuia))
            {
                PacienteNegocio pacienteNegocio = new PacienteNegocio();
                rddlRiesgos.DataSource = pacienteNegocio.listarRiesgos();
                rddlRiesgos.DataValueField = "idRiesgo";
                rddlRiesgos.DataTextField = "nombre";
                rddlRiesgos.DataBind();
            }
            if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)//Insercion
            {
                //Por ahora no se necesita hacer nada en la insercion.
            }
            else//Actualizacion
            {
                if (Request.QueryString["tg"] != null && !Request.QueryString["tg"].ToString().Equals("3"))
                    rddlEstados.SelectedValue = item.GetDataKeyValue("idEstado").ToString();
                if (e.Item.OwnerID.Equals(nombreGrillaPrograma))
                {
                    rddlPoblacion.SelectedValue = item.GetDataKeyValue("poblacionObjetivo").ToString();
                    rddlEstados.SelectedValue = item.GetDataKeyValue("idEstado").ToString();
                    rddlRiesgosPrograma.SelectedValue = (item.GetDataKeyValue("idRiesgoPrograma") == null ? string.Empty : item.GetDataKeyValue("idRiesgoPrograma").ToString());
                }
                if ((e.Item.OwnerID.Equals(nombreGrillaTipoGuia)))
                    rddlEstados.SelectedValue = item.GetDataKeyValue("idEstado").ToString();
                if (e.Item.OwnerID.Equals(nombreGrillaGuia))
                    rddlRiesgos.SelectedValue = item.GetDataKeyValue("idRiesgo").ToString();
            }
        }

        #endregion

        #region Metodos Combos
        protected void cboDiagnosticos_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //set the Text and Value property of every item
            //here you can set any other properties like Enabled, ToolTip, Visible, etc.
            //e.Item.Text = ((System.Data.DataRowView)e.Item.DataItem)["Descripcion"].ToString();
            e.Item.Text = ((JObject)e.Item.DataItem)["Descripcion"].ToString();
            e.Item.Value = ((JObject)e.Item.DataItem)["Codigo"].ToString();
        }

        protected void cboDiagnosticos_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            //obtiene los registros que contengan e.Text
            //NOTA: nmtg -> nombre tipo guia .... rg -> riesgo
            string tipoGuia = Request.QueryString["nmtg"].ToString().Replace("'", "").ToUpper();
            JArray lista = new JArray();
            JArray lista2 = new JArray();
            if (!e.Text.Equals(string.Empty))
            {
                switch (tipoGuia)
                {
                    case "DIAGNÓSTICOS ASOCIADOS":
                    case "OTROS DIAGNÓSTICOS":
                        lista = Comun.listaGenerica(tipoGuia, ConfigurationManager.AppSettings["DIAGNOSTICONOMBREWS"].ToString() + e.Text);
                        lista2 = Comun.listaGenerica(tipoGuia, ConfigurationManager.AppSettings["DIAGNOSTICOCODIGOWS"].ToString() + e.Text);
                        lista = Comun.CombinarListas(lista, lista2);
                        break;
                    case "INTERCONSULTAS":
                        lista = Comun.listaGenerica(tipoGuia, ConfigurationManager.AppSettings["PRESTACIONESINTERCONSULTAWS"].ToString() + e.Text + "?Tipo=1");
                        break;
                    case "EDUCACIÓN Y HÁBITOS DE VIDA SALUDABLE":
                        //TODO: PENDIENTE EL WS
                        break;
                    case "ENCUESTAS DE SALUD":
                        //TODO: PENDIENTE EL WS
                        break;
                    case "EXAMENES DE LABORATORIO":
                        lista = Comun.listaGenerica(tipoGuia, ConfigurationManager.AppSettings["PRESTACIONESINTERCONSULTAWS"].ToString() + e.Text + "?Tipo=2");
                        break;
                    case "OTROS EXÁMENES Y PROCEDIMIENTOS":
                        lista = Comun.listaGenerica(tipoGuia, ConfigurationManager.AppSettings["PRESTACIONESINTERCONSULTAWS"].ToString() + e.Text + "?Tipo=3");
                        break;
                    case "MEDICAMENTO":
                        lista = Comun.listaGenerica(tipoGuia, ConfigurationManager.AppSettings["MEDICAMENTONOMBREWS"].ToString() + e.Text + "?Activos=true");
                        lista2 = Comun.listaGenerica(tipoGuia, ConfigurationManager.AppSettings["MEDICAMENTOCODIGOWS"].ToString() + e.Text + "?Activo=true");
                        lista = Comun.CombinarListas(lista, lista2);
                        break;
                    case "AYUDAS DIAGNÓSTICAS":
                        //TODO: PENDIENTE EL WS
                        break;
                    case "TOMAS BIOMÉTRICAS":
                        //TODO: PENDIENTE EL WS¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿ESTO QUE ES???????????????????
                        break;
                    case "CONTROLES MÉDICOS":
                        //TODO: PENDIENTE EL WS¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿ESTO QUE ES???????????????????
                        break;
                    case "ASPECTOS MONITOREADOS":
                        //TODO: PENDIENTE EL WS¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿ESTO QUE ES???????????????????
                        break;
                }
            }
            /*Comentarios temporalmente*/
            //webService = new ServiciosMedicos();
            //cboDiagnosticos.DataSource = webService.diagnosticos(e.Text, tipoGuia);
            cboDiagnosticos.DataSource = lista;
            cboDiagnosticos.DataTextField = "Descripcion";
            cboDiagnosticos.DataValueField = "Codigo";
            cboDiagnosticos.DataBind();
        }

        protected void cboDiagnosticos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                hfApertura.Value = e.Text;
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        protected void cboRiesgo_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            negocioPaciente = new PacienteNegocio();
            cboRiesgo.DataSource = negocioPaciente.listarRiesgos();
            cboRiesgo.DataValueField = "idRiesgo";
            cboRiesgo.DataTextField = "nombre";
            cboRiesgo.DataBind();
        }
        #endregion

        #region Eventos Botones
        protected void btnAgregarDiagnostico_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO: Homologar los estados a la bd V2 de SaludMovil
                if (!cboDiagnosticos.SelectedValue.Equals(string.Empty))
                {
                    if (Request.QueryString["nmtg"].ToString().ToUpper().Contains(ocurrenciaInterconsulta) || Request.QueryString["nmtg"].ToString().ToUpper().Contains("OTROS EXÁMENES") || Request.QueryString["nmtg"].ToString().ToUpper().Contains("EXAMEN") || Request.QueryString["nmtg"].ToString().ToUpper().Contains("AYUDA"))
                    {
                        if (!cboRiesgo.SelectedValue.Equals(string.Empty) && !txtCantidadRiesgo.Value.Equals(string.Empty))
                        {
                            guardarDiagnostico();
                            RadGridGuia.Rebind();
                        }
                        else
                            RadNotificationMensajes.Show("Debe seleccionar el nivel de riesgo y/o agregar la frecuencia anual");
                    }
                    else
                    {
                        guardarDiagnostico();
                        RadGridGuia.Rebind();
                    }
                }
                else
                    RadNotificationMensajes.Show("Debe seleccionar un diagnóstico");
                cboRiesgo.SelectedValue = string.Empty;
                cboRiesgo.Text = string.Empty;
                cboRiesgo.EmptyMessage = "Seleccione el riesgo...";
                txtCantidadRiesgo.Value = string.Empty;
                cboDiagnosticos.SelectedValue = string.Empty;
                cboDiagnosticos.Text = string.Empty;
                cboDiagnosticos.EmptyMessage = "Digite para buscar...";
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Guarda una guia
        /// </summary>
        private void guardarDiagnostico()
        {
            try
            {
                int idRiesgo = 4;
                int cantidad = 0;
                int idPrograma = Convert.ToInt32(Request.QueryString["pgm"]);
                int idTipoGuia = Convert.ToInt32(Request.QueryString["tg"]);
                IList<sm_Guia> guias = programaNegocio.listarGuiasPorProgramaYTipo(idPrograma, idTipoGuia).Where(g => g.idGuia != 0).ToList();
                if (!cboRiesgo.SelectedValue.Equals(string.Empty))
                {
                    idRiesgo = Convert.ToInt32(cboRiesgo.SelectedValue);
                    cantidad = Convert.ToInt32(txtCantidadRiesgo.Value);
                    if (cantidad >= 1 && cantidad <= 12)
                    {
                        if (!existeGuia(cboDiagnosticos.SelectedValue, cantidad, idRiesgo, true, 0, guias))
                        {
                            sm_Guia guia = new sm_Guia();
                            guia.idTipoGuia = idTipoGuia;
                            guia.idCodigoTipo = cboDiagnosticos.SelectedValue;
                            guia.descripcion = cboDiagnosticos.Text;
                            guia.idPrograma = idPrograma;
                            guia.idEstado = 1;//Inicialmente si se agrega siempre el estado es Habilitado                
                            guia.idRiesgo = idRiesgo;
                            guia.cantidadRiesgo = cantidad;
                            guia.createdBy = login;
                            guia.createdDate = DateTime.Now;
                            programaNegocio.GuardarGuia(guia);
                            RadNotificationMensajes.Show("Se agregó el registro correctamente");
                        }
                        else
                            RadNotificationMensajes.Show("Ya existe un registro con esta guia, frecuencia y nivel de riesgo");
                    }
                    else
                        RadNotificationMensajes.Show("La frecuencia anual debe estar en el rango de entre 1 y 12 meses");
                }
                else
                {
                    sm_Guia guia = new sm_Guia();
                    guia.idTipoGuia = idTipoGuia;
                    guia.idCodigoTipo = cboDiagnosticos.SelectedValue;
                    guia.descripcion = cboDiagnosticos.Text;
                    guia.idPrograma = idPrograma;
                    guia.idEstado = 1;//Inicialmente si se agrega siempre el estado es Habilitado                
                    guia.idRiesgo = idRiesgo;
                    guia.cantidadRiesgo = cantidad;
                    guia.createdBy = login;
                    guia.createdDate = DateTime.Now;
                    if (!existeGuia(guia.idCodigoTipo, Convert.ToInt32(guia.cantidadRiesgo), guia.idRiesgo, true, 0, guias))
                    {
                        programaNegocio.GuardarGuia(guia);
                        RadNotificationMensajes.Show("Se agregó el registro  a la guía");
                    }
                    else
                        RadNotificationMensajes.Show("No se puede agregar el registro a la guia debido a que ya existe otro registro con la misma información");
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Verifica si una guia ya ha sido creada para evita duplicidad en los registros
        /// </summary>
        /// <param name="codigoGuia"></param>
        /// <param name="frecuencia"></param>
        /// <param name="nivelRiesgo"></param>
        /// <param name="esInsercion"></param>
        /// <returns></returns>
        private bool existeGuia(string codigoGuia, int frecuencia, int nivelRiesgo, bool esInsercion, int idGuia, IList<sm_Guia> guias)
        {
            try
            {
                //int idPrograma = Convert.ToInt32(Request.QueryString["pgm"]);
                //int idTipoGuia = Convert.ToInt32(Request.QueryString["tg"]);
                //IList<sm_Guia> guias = programaNegocio.listarGuiasPorProgramaYTipo(idPrograma, idTipoGuia).Where(g => g.idGuia != idGuia).ToList();
                string codigoApertura = string.Empty, codigoDescripcionSeleccion = string.Empty;
                foreach (sm_Guia guia in guias)
                {
                    if (guia.idCodigoTipo.Equals(codigoGuia) && guia.cantidadRiesgo == frecuencia && guia.idRiesgo == nivelRiesgo)
                    {
                        codigoApertura = guia.descripcion.Split('-')[1].Trim();
                        if (esInsercion)
                            codigoDescripcionSeleccion = cboDiagnosticos.Text.Split('-')[1].Trim();
                        else
                            codigoDescripcionSeleccion = guia.descripcion.Split('-')[1].Trim();
                        if (codigoApertura.Equals(codigoDescripcionSeleccion))
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error: " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Controla que los ponderadores de todos los registros de las guias no superen el 100 porciento
        /// </summary>
        /// <param name="guias"></param>
        /// <param name="guia"></param>
        /// <returns></returns>
        private bool CumpleTotalCienPorciento(IList<sm_Guia> guias, sm_Guia guia, bool esComponenteGuia, IList<TipoGuia> tiposGuias, sm_TipoGuia tipoGuia)
        {
            try
            {
                decimal totalActividad = 0;
                if (esComponenteGuia)
                    if ((bool)tipoGuia.esPonderadoPorGrupo)
                        totalActividad = (decimal)(tiposGuias.Where(t => t.idTipoGuia != tipoGuia.idTipoGuia).Sum(t => t.ponderadorGrupo) + tipoGuia.ponderadorGrupo);
                    else
                        totalActividad = (decimal)(guias.Where(g => g.idGuia != guia.idGuia).Sum(g => g.po_act) + guia.po_act);
                //decimal totalGrupoActividad = (decimal)(guias.Sum(g => g.po_grupoAct) + guia.po_grupoAct);//Esta sobrando
                if (totalActividad > 100 /*|| totalGrupoActividad > 100*/)
                    return false;
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Error:" + ex.Message);
            }
            return true;
        }

        /// <summary>
        /// Metodo de personalizacion del formulario
        /// </summary>
        /// <param name="eps"></param>
        private void personalizarFormulario(int eps)
        {
            switch (eps)
            {
                case 1: //Colmedica
                    RadGridTipoGuia.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                    RadGridGuia.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                    //RadGridTipoGuia.MasterTableView.Columns.FindByUniqueName("descripcion").
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// ACtualizar las versiones de las tablas para sincronizar la app movil
        /// </summary>
        /// <param name="nombreTabla"></param>
        private void ActualizarVersionTabla(string nombreTabla)
        {
            //TODO: Crear SP que reciba como parametro el nombre de la tabla y que si no esta la ingrese con version 1 y si ya esta aumente la version en 1
        }

        /// <summary>
        /// Mostrar u ocultar columnas de la grilla guia
        /// </summary>
        /// <param name="esOcultar"></param>
        private void MostrarOcultarColumnasgrillaGuia(bool esOcultar)
        {
            RadGridGuia.MasterTableView.GetColumn("idCodigoTipo").Visible = esOcultar;
            RadGridGuia.MasterTableView.GetColumn("descripcion").Visible = esOcultar;
            RadGridGuia.MasterTableView.GetColumn("sm_Riesgo.nombre").Visible = esOcultar;
            RadGridGuia.MasterTableView.GetColumn("cantidadRiesgo").Visible = esOcultar;
            RadGridGuia.MasterTableView.GetColumn("cantidadRiesgo").Visible = esOcultar;
            RadGridGuia.MasterTableView.GetColumn("sm_Estado.nombre").Visible = esOcultar;
            RadGridGuia.MasterTableView.GetColumn("peso").Visible = esOcultar;
            //Columnas de educacion y habitos saludables
            RadGridGuia.MasterTableView.GetColumn("ce_titulo").Visible = !esOcultar;
            RadGridGuia.MasterTableView.GetColumn("ce_url").Visible = !esOcultar;
            RadGridGuia.MasterTableView.GetColumn("ce_estadoDoc").Visible = !esOcultar;
            RadGridGuia.MasterTableView.GetColumn("ce_fechaRegistro").Visible = !esOcultar;
            //Columnas de porcentajes ponderacion
            //RadGridGuia.MasterTableView.GetColumn("po_grupoAct").Visible = esOcultar;
            if (Request.QueryString["pgm"] != null && Request.QueryString["tg"] != null)
            {
                foreach (GridDataItem itemGrillaComponente in RadGridTipoGuia.SelectedItems)
                {
                    Hashtable valores = new Hashtable();
                    itemGrillaComponente.ExtractValues(valores);
                    RadGridGuia.MasterTableView.GetColumn("po_act").Visible = Convert.ToBoolean(valores["esPonderadoPorGrupo"].ToString());
                }
            }
        }
        #endregion


    }
}
