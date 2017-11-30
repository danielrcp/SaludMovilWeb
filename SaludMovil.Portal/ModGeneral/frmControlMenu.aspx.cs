#region Imports
using System;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using System.Collections;
using Telerik.Web.UI;

using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
#endregion

/***
 * Descripción: Formulario que permite administrar los permisos a las opciones de un rol
 * Fecha creacion: 3 Noviembre 2016
 * Creado por: Andres Felipe Silva Pascuas - afsp
 * Modificacion: 
 * 
 * 
 * 
 * */

namespace SaludMovil.Portal.ModGeneral
{
    public partial class frmControlMenu : System.Web.UI.Page
    {
        private AdministracionNegocio adminNegocio;
        

        private const string DEFAULT_ROLE = "_ROLE_";

        protected void Page_Load(object sender, EventArgs e)
        {
            adminNegocio = new AdministracionNegocio();
            if (!Page.IsPostBack)
            {
                try
                {
                    IList<sm_Rol> roles = adminNegocio.ListarRoles();
                    cboRoles.DataSource = roles;
                    cboRoles.DataTextField = "nombre";
                    cboRoles.DataValueField = "idRol";
                    cboRoles.DataBind();
                }
                catch (Exception ex)
                {
                    RadNotificationMensajes.Show("Error: " + ex.Message);
                }
            }
        }

        #region Metodos
        /// <summary>
        /// Carga la informacion del TreeView
        /// </summary>
        private void BindingInfo()
        {
            tvMenuCompleto.Nodes.Clear();
            TreeNode item;
            IList<sm_Opcion> opciones = adminNegocio.ListarOpciones();
            IList<RolOpcion> rolOpciones = adminNegocio.OpcionesRol(Convert.ToInt32(cboRoles.SelectedValue));
            foreach (sm_Opcion opcion in opciones.Where(op => op.IdOpcionPadre == 1).OrderBy(op2 => op2.Orden))
            {
                item = llenarOpcionesSelecciones(opcion, opciones.Where(o => o.IdOpcionPadre == opcion.IdOpcion).ToList(), rolOpciones);
                tvMenuCompleto.Nodes.Add(item);
            }
            tvMenuCompleto.DataBind();
        }

        /// <summary>
        /// Metodo recursivo para recorrer todos los hijos de cada opcion
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="hijos"></param>
        /// <param name="rolOpciones"></param>
        /// <returns></returns>
        private TreeNode llenarOpcionesSelecciones(sm_Opcion opcion, IList<sm_Opcion> hijos, IList<RolOpcion> rolOpciones)
        {
            TreeNode node = new TreeNode(), item = new TreeNode();
            string query = "../ManejarRegistroMenu.aspx?tabla=Opcion&idOpcion=IdOpcion&value=" + opcion.IdOpcion;
            node.Text = "<a href='" + query + "'>" + opcion.NombreOpcion + "</a>";
            node.Value = opcion.IdOpcion.ToString();
            if (!opcion.Descripcion.Equals(string.Empty))
                node.ToolTip = opcion.Descripcion;
            else
                node.ToolTip = opcion.NombreOpcion;
            node.SelectAction = TreeNodeSelectAction.None;     
            if (rolOpciones.Where(ro => ro.idOpcion == opcion.IdOpcion).ToList().Count > 0)
                node.Checked = true;
            else
                node.Checked = false;
            if (hijos.Count > 0)
            {
                foreach (sm_Opcion opcion2 in hijos)
                {
                    item = llenarOpcionesSelecciones(opcion2, hijos.Where(o => o.IdOpcionPadre == opcion2.IdOpcion).ToList(), rolOpciones);
                    if (item != null)
                        node.ChildNodes.Add(item);
                }
                return node;
            }
            else
            {
                if (node != null)
                    return node;
                else
                    return null;
            }
        }

        /// <summary>
        /// Funcion recursiva que recorre todos los niveles del arbol
        /// </summary>
        /// <param name="nodo"></param>
        /// <param name="idRol"></param>
        public void RecorrerNodos(TreeNode nodo, int idRol)
        {
            if (nodo.Checked)
            {
                sm_RolOpcion nuevaopcion = new sm_RolOpcion();
                nuevaopcion.idRol = idRol;
                nuevaopcion.idOpcion = Convert.ToInt32(nodo.Value);
                nuevaopcion.leer = true;
                nuevaopcion.eliminar = true;
                nuevaopcion.actualizar = true;
                nuevaopcion.crear = true;
                nuevaopcion.createdBy = Session["login"].ToString();
                nuevaopcion.createdDate = DateTime.Now;
                adminNegocio.InsertarPermisosMenu(idRol, nuevaopcion);
            }
            // Inicia la recursion por todos los nodos
            foreach (TreeNode subNodo in nodo.ChildNodes)
            {
                RecorrerNodos(subNodo,idRol);
            }
        }
        #endregion

        #region Eventos TreeView
        /// <summary>
        /// Listener del evento click de cada nodo del TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvMenuCompleto_TreeNodeCheckChanged(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
        {
            try
            {
                //TODO:Implementar la grilla para los permisos CRUD de cada opcion
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("Esta opcion no se encuentra disponible actualmente");
            }
        }
        #endregion

        #region Eventos botones
        /// <summary>
        /// Listener del evento click del boton de guardar los cambios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, System.EventArgs e)
        {
            int idRol = Convert.ToInt32(cboRoles.SelectedValue);
            try
            {
                adminNegocio.BorrarPermisosMenu(idRol);
                foreach(TreeNode nodo in tvMenuCompleto.Nodes)
                {
                    RecorrerNodos(nodo, idRol);                    
                }                
                RadNotificationMensajes.Show("Las opciones se han actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }
        #endregion

        #region Eventos combos
        /// <summary>
        /// Listener de cambio de seleccion del combo de roles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboRoles_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboRoles.SelectedValue.Equals(string.Empty))
                BindingInfo();
        }
        #endregion
    }
}
