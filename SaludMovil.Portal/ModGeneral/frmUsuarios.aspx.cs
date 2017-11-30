#region Imports
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using SaludMovil.Transversales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI;
using System.Linq;
using SaludMovil.Transversales;
using Newtonsoft.Json.Linq;
#endregion

namespace SaludMovil.Portal.ModGeneral
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        private AdministracionNegocio adminNegocio;
        private PacienteNegocio pacienteNegocio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarRoles();
                placeHolderControl.Controls.Add(ControlFiltroTextBoxExtender("txtNumeroIdentificacion", 15));
                placeHolderControl.Controls.Add(ControlFiltroTextBoxExtender("txtCelular", 15));
                placeHolderControl.Controls.Add(ControlFiltroTextBoxExtender("txtTelefonoFijo", 15));
            }
            CambiarIdiomaFilterGridTelerik(RadGridUsuarios);
        }

        #region Grilla usuarios
        protected void RadGridUsuarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                adminNegocio = new AdministracionNegocio();
                RadGridUsuarios.DataSource = adminNegocio.ListarUsuarios(-1, string.Empty, -1);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void RadGridUsuarios_ItemCommand(object sender, GridCommandEventArgs e)
        {

            try
            {
                switch (e.CommandName)
                {
                    case "InitInsert"://Insercion
                        e.Canceled = true;
                        panelGrilla.Visible = false;
                        panelComboUsuario.Visible = true;
                        panelAgregarUsuario.Visible = false;                        
                        cboTipoIdentificacion.Enabled = true;
                        txtNumeroIdentificacion.Enabled = true;
                        break;
                    case "Buscar":
                        pacienteNegocio = new PacienteNegocio();
                        sm_Persona persona = new sm_Persona();
                        Hashtable valores = new Hashtable();
                        GridDataItem item = (GridDataItem)e.Item;
                        item.ExtractValues(valores);
                        cargarEmpresas();
                        cargarCiudades();
                        cargarRoles();
                        cargarTiposIdentificacion();
                        /*Carga la informacion de la persona*/                        
                        cboTipoIdentificacion.SelectedValue = item.GetDataKeyValue("idTipoIdentificacion").ToString();
                        txtNumeroIdentificacion.Text = valores["numeroIdentificacion"].ToString();
                        persona = pacienteNegocio.ConsultarPersona(Convert.ToInt16(item.GetDataKeyValue("idTipoIdentificacion")), valores["numeroIdentificacion"].ToString());
                        txtPrimerNombre.Text = item.GetDataKeyValue("primerNombre").ToString();
                        if (item.GetDataKeyValue("segundoNombre") != null)
                            txtSegundoNombre.Text = item.GetDataKeyValue("segundoNombre").ToString();
                        else
                            txtSegundoNombre.Text = string.Empty;
                        txtPrimerApellido.Text = item.GetDataKeyValue("primerApellido").ToString();
                        if (item.GetDataKeyValue("segundoApellido") != null)
                            txtSegundoApellido.Text = item.GetDataKeyValue("segundoApellido").ToString();
                        else
                            txtSegundoApellido.Text = string.Empty;
                        rdpFechaNacimiento.SelectedDate = persona.fechaNacimiento;
                        cboCiudad.SelectedValue = item.GetDataKeyValue("idCiudad").ToString();
                        txtCelular.Text = valores["celular"].ToString();
                        txtTelefonoFijo.Text = valores["telefonoFijo"].ToString();
                        txtCorreo.Text = valores["correo"].ToString();
                        /*Carga la informacion del usuario*/
                        adminNegocio = new AdministracionNegocio();
                        int tipoID = Convert.ToInt32(item.GetDataKeyValue("idTipoIdentificacion").ToString());
                        Usuario usuario = adminNegocio.ListarUsuarios(tipoID, valores["numeroIdentificacion"].ToString(), 1).FirstOrDefault();
                        if (usuario == null)
                            chkMedicoTratante.Checked = true;
                        else
                        {
                            txtUsuario.Text = usuario.usuario;
                            txtContrasena.Text = usuario.contrasena;
                            cboEmpresa.SelectedValue = usuario.idEmpresa.ToString();
                            foreach (sm_Rol rol in usuario.Roles)
                            {
                                foreach (RadComboBoxItem c in cboRol.Items)
                                {
                                    if (c.Value.ToString().Equals(rol.idRol.ToString()))
                                    {
                                        c.Checked = true;
                                    }
                                }
                            }
                            cboEstado.SelectedValue = usuario.estado.ToString();
                        }
                        /*Hidden Fields*/
                        if (usuario != null)
                            hdfIdUsuario.Value = item.GetDataKeyValue("idUsuario").ToString();
                        else
                            hdfIdUsuario.Value = "0";
                        hdfCreatedBy.Value = item.GetDataKeyValue("createdBy").ToString();;
                        hdfCreatedDate.Value = item.GetDataKeyValue("createdDate").ToString();;
                        panelGrilla.Visible = false;
                        panelComboUsuario.Visible = false;
                        panelAgregarUsuario.Visible = true;
                        btnAgregarUsuario.Visible = false;
                        btnActualizar.Visible = true;
                        cboTipoIdentificacion.Enabled = false;
                        txtNumeroIdentificacion.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }
        #endregion

        #region Eventos combos
        protected void cboPersonalMedico_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                //JArray lista = new JArray();
                //JArray lista2 = new JArray();
                //string[] campos = e.Text.Split(' ');
                //string porCodigo = Constantes.PERSONALMEDICOCODIGOWS + campos[0] + "?NoAdscrito=false";
                //string porNombre = Constantes.PERSONALMEDICONOMBRESWS + "Nombre=" + campos[0];
                //if (campos.Length > 1)
                //    porNombre += "&PrimerApellido=" + campos[1];
                //if (campos.Length > 2)
                //    porNombre += "&SegundoApellido=" + campos[2];
                //porNombre += "&?NoAdscrito=false";                
                //lista = Comun.listarPersonalMedico(porCodigo);
                //lista2 = Comun.listarPersonalMedico(porNombre);
                //lista = Comun.CombinarListasPersonalMedico(lista, lista2);
                //cboPersonalMedico.DataSource = lista;
                //cboPersonalMedico.DataTextField = "Descripcion";
                //cboPersonalMedico.DataValueField = "Codigo";
                //cboPersonalMedico.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cboPersonalMedico_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            //e.Item.Text = ((JObject)e.Item.DataItem)["Descripcion"].ToString();
            //e.Item.Value = ((JObject)e.Item.DataItem)["Codigo"].ToString();
        }

        protected void cboPersonalMedico_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //sm_Persona persona = adminNegocio.
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cboTipoIdentificacion_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                cargarTiposIdentificacion();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cboCiudad_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                cargarCiudades();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cboEmpresa_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            try
            {
                cargarEmpresas();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cargarRoles()
        {
            try
            {
                adminNegocio = new AdministracionNegocio();
                IList<sm_Rol> roles = adminNegocio.ListarRoles();                
                cboRol.DataSource = roles;
                cboRol.DataTextField = "nombre";
                cboRol.DataValueField = "idRol";
                cboRol.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cargarTiposIdentificacion()
        {
            try
            {
                adminNegocio = new AdministracionNegocio();
                IList<sm_TipoIdentificacion> tipoID = adminNegocio.ListarTiposID();
                cboTipoIdentificacion.DataSource = tipoID;
                cboTipoIdentificacion.DataTextField = "nombre";
                cboTipoIdentificacion.DataValueField = "idTipoIdentificacion";
                cboTipoIdentificacion.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cargarCiudades()
        {
            try
            {
                adminNegocio = new AdministracionNegocio();
                IList<sm_Ciudad> ciudades = adminNegocio.ListarCiudades();
                cboCiudad.DataSource = ciudades;
                cboCiudad.DataTextField = "nombre";
                cboCiudad.DataValueField = "idCiudad";
                cboCiudad.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void cargarEmpresas()
        {
            try
            {
                adminNegocio = new AdministracionNegocio();
                IList<sm_Empresa> empresas = adminNegocio.ListarEmpresas();
                Persona persona = (Persona)Session["persona"];
                IList<sm_Rol> roles = persona.Roles;
                if (roles[0].idRol != 1)
                    empresas = (IList<sm_Empresa>)empresas.Where(r => r.idEmpresa != 1);
                cboEmpresa.DataSource = empresas;
                cboEmpresa.DataTextField = "nombre";
                cboEmpresa.DataValueField = "idEmpresa";
                cboEmpresa.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }
        #endregion

        #region Eventos botones
        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string campos = verificarCampos();

                if (campos.Equals(string.Empty))
                {
                    adminNegocio = new AdministracionNegocio();
                    IList<Usuario> existeUsuario = adminNegocio.ListarUsuarios(Convert.ToInt32(cboTipoIdentificacion.SelectedValue),
                        txtNumeroIdentificacion.Text.Trim(), 1);
                    if (existeUsuario.Count == 0)//No existe el usuario
                    {
                        sm_Persona persona = construirPersona(false);
                        sm_Usuario usuario = construirUsuario(false);
                        adminNegocio.GuardarPersona(persona);
                        if (chkMedicoTratante.Checked)//Si es medico tratante sin usuario del sistema no debe guardar opciones rol
                        {
                            sm_PersonalMedico medico = construirPersonalMedico();
                            adminNegocio.GuardarMedico(medico);
                        }
                        else//Si es un usuario del sistema
                        {
                            adminNegocio.GuardarUsuario(usuario);
                            int tipoID = (int)usuario.idTipoIdentificacion;
                            string numID = usuario.numeroIdentificacion;
                            Usuario nuevoUsuario = adminNegocio.ListarUsuarios(tipoID, numID, 1).FirstOrDefault();
                            IList<sm_UsuarioRol> roles = construirUsuarioRol(nuevoUsuario.idUsuario);
                            foreach (sm_UsuarioRol ur in roles)
                                adminNegocio.GuardarRol(ur);
                            var coleccionRoles = cboRol.CheckedItems;
                            foreach (var item in coleccionRoles)//sE REVISA QUE DENTRO DE LOS ROLES SELECCIONADOS ESTE EL ROL MEDICO PARA AGREGARLO A MEDICOS
                            {
                                if (item.Text.ToUpper().Equals("MEDICO"))
                                {
                                    sm_PersonalMedico medico = construirPersonalMedico();
                                    adminNegocio.GuardarMedico(medico);
                                    break;
                                }
                            }
                        }
                        limpiarCampos();
                        RadGridUsuarios.Rebind();
                    }
                    else
                        MostrarMensaje("La persona ya existe", false);
                }
                else
                    MostrarMensaje(campos, false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string campos = verificarCampos();
                if (campos.Equals(string.Empty))
                {
                    adminNegocio = new AdministracionNegocio();
                    sm_Persona persona = construirPersona(true);
                    if (!chkMedicoTratante.Checked)//Actualiza un usuario del sistema
                    {
                        /*Actualiza la persona*/
                        adminNegocio.ActualizarPersona(persona);
                        /*Consulta el usuario para ver si existe y si no lo crea*/
                        int tipoID = (string.IsNullOrEmpty(cboTipoIdentificacion.SelectedValue) ? 0 : Convert.ToInt32(cboTipoIdentificacion.SelectedValue));
                        Usuario usuario = adminNegocio.ListarUsuarios(tipoID, txtNumeroIdentificacion.Text, 1).FirstOrDefault();
                        sm_Usuario usuarioAct;
                        if (usuario == null)
                        {
                            usuarioAct = construirUsuario(false);
                            adminNegocio.GuardarUsuario(usuarioAct);
                            tipoID = (int)usuarioAct.idTipoIdentificacion;
                            string numID = usuarioAct.numeroIdentificacion;
                            Usuario nuevoUsuarioCrea = adminNegocio.ListarUsuarios(tipoID, numID, 1).FirstOrDefault();
                            IList<sm_UsuarioRol> rolesCrea = construirUsuarioRol(nuevoUsuarioCrea.idUsuario);
                            foreach (sm_UsuarioRol ur in rolesCrea)
                                adminNegocio.GuardarRol(ur);
                            var coleccionRolesCrea = cboRol.CheckedItems;
                            foreach (var item in coleccionRolesCrea)//SE REVISA QUE DENTRO DE LOS ROLES SELECCIONADOS ESTE EL ROL MEDICO PARA AGREGARLO A MEDICOS
                            {
                                if (item.Text.ToUpper().Equals("MEDICO"))
                                {
                                    sm_PersonalMedico medico = construirPersonalMedico();
                                    adminNegocio.ActualizarMedico(medico);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            usuarioAct = construirUsuario(true);
                            adminNegocio.ActualizarUsuario(persona, usuarioAct);
                            tipoID = (int)usuario.idTipoIdentificacion;
                            Usuario nuevoUsuario = adminNegocio.ListarUsuarios(tipoID, usuario.numeroIdentificacion, 1).FirstOrDefault();
                            /*Eliminar los roles anteriores del usuario*/
                            foreach (sm_Rol rol in nuevoUsuario.Roles)
                            {
                                adminNegocio.EliminarRol(nuevoUsuario.idUsuario, rol.idRol);
                            }
                            /*Insertar los roles del usuario*/
                            IList<sm_UsuarioRol> roles = construirUsuarioRol(nuevoUsuario.idUsuario);
                            foreach (sm_UsuarioRol ur in roles)
                            {
                                adminNegocio.GuardarRol(ur);
                            }
                            var coleccionRoles = cboRol.CheckedItems;
                            foreach (var item in coleccionRoles)//SE REVISA QUE DENTRO DE LOS ROLES SELECCIONADOS ESTE EL ROL MEDICO PARA ACTUALIZARLO
                            {
                                if (item.Text.ToUpper().Equals("MEDICO"))
                                {
                                    sm_PersonalMedico medico = construirPersonalMedico();
                                    adminNegocio.ActualizarMedico(medico);
                                    break;
                                }
                            }
                        }
                    }
                    else//Actualiza el personal medico
                    {
                        //verificar si los campos de usuario vienen llenos y l campo de medicotrtante no esta checkeado para crearlo en vez de actualizarlo
                        sm_PersonalMedico medico = construirPersonalMedico();
                        adminNegocio.ActualizarPersona(persona);
                        adminNegocio.ActualizarMedico(medico);
                    }
                    limpiarCampos();
                    RadGridUsuarios.Rebind();
                }
                else
                    MostrarMensaje(campos, false);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCampos();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }

        protected void btnAgregarManual_Click(object sender, EventArgs e)
        {
            try
            {
                panelGrilla.Visible = false;
                panelComboUsuario.Visible = false;
                panelAgregarUsuario.Visible = true;
                btnAgregarUsuario.Visible = true;
                btnActualizar.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, true);
            }
        }
        #endregion

        #region Manejador de mensajes
        /// <summary>
        /// Controla los mensajes del formulario
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="esError"></param>
        private void MostrarMensaje(string mensaje, bool esError)
        {
            string msg = mensaje;
            if (esError)
                msg = "Error: " + mensaje.Replace("'", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            RadNotificationMensajes.Show(msg);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Arma el objeto persona
        /// </summary>
        /// <param name="esActualizacion"></param>
        /// <returns></returns>
        private sm_Persona construirPersona(bool esActualizacion)
        {
            sm_Persona persona = new sm_Persona();
            persona.idTipoIdentificacion = Convert.ToInt32(cboTipoIdentificacion.SelectedValue);
            persona.numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
            persona.primerNombre = txtPrimerNombre.Text.Trim();
            persona.segundoNombre = txtSegundoNombre.Text.Trim();
            persona.primerApellido = txtPrimerApellido.Text.Trim();
            persona.segundoApellido = txtSegundoApellido.Text.Trim();
            persona.idTipo = Constantes.TIPOMEDICO;
            persona.fechaNacimiento = Convert.ToDateTime(rdpFechaNacimiento.SelectedDate);
            persona.idCiudad = Convert.ToInt32(cboCiudad.SelectedValue);
            persona.celular = txtCelular.Text.Trim();
            persona.telefonoFijo = txtTelefonoFijo.Text.Trim();
            persona.correo = txtCorreo.Text.Trim();
            persona.createdBy = Session["login"].ToString();
            persona.createdDate = DateTime.Now;
            if (esActualizacion)
            {
                persona.createdBy = hdfCreatedBy.Value;
                persona.createdDate = Convert.ToDateTime(hdfCreatedDate.Value);
                persona.updatedBy = Session["login"].ToString();
                persona.updatedDate = DateTime.Now;
            }
            return persona;
        }

        /// <summary>
        /// Arma el objeto usuario
        /// </summary>
        /// <param name="esActualizacion"></param>
        /// <returns></returns>
        private sm_Usuario construirUsuario(bool esActualizacion)
        {
            sm_Usuario usuario = new sm_Usuario();            
            usuario.idTipoIdentificacion = Convert.ToInt32(cboTipoIdentificacion.SelectedValue);
            usuario.numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
            usuario.usuario = txtUsuario.Text.Trim();
            usuario.idEmpresa = Convert.ToInt32(cboEmpresa.SelectedValue);
            usuario.contrasena = txtContrasena.Text;
            usuario.createdBy = Session["login"].ToString();
            usuario.createdDate = DateTime.Now;            
            if (cboEstado.SelectedValue.Equals("1"))//Activo
                usuario.estado = true;
            else
                usuario.estado = false;
            if (esActualizacion)
            {
                usuario.idUsuario = Convert.ToInt32(hdfIdUsuario.Value);
                usuario.createdBy = hdfCreatedBy.Value;
                usuario.createdDate = Convert.ToDateTime(hdfCreatedDate.Value);
                usuario.updatedBy = Session["login"].ToString();
                usuario.updatedDate = DateTime.Now;
            }
            return usuario;
        }

        /// <summary>
        /// Arma el objeto UsuarioRol
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        private IList<sm_UsuarioRol> construirUsuarioRol(int idUsuario)
        {
            IList<sm_UsuarioRol> roles = new List<sm_UsuarioRol>();
            sm_UsuarioRol rol;
            var coleccionRoles = cboRol.CheckedItems;
            foreach (var item in coleccionRoles)
            {
                rol = new sm_UsuarioRol();
                rol.IdUsuario = idUsuario;
                rol.RoleId = (int)Convert.ToInt32(item.Value.ToString());
                roles.Add(rol);
            }
            return roles;
        }

        /// <summary>
        /// Arma el objeto medico
        /// </summary>
        /// <returns></returns>
        private sm_PersonalMedico construirPersonalMedico()
        {
            sm_PersonalMedico medico = new sm_PersonalMedico();
            medico.idTipoIdentificacion = Convert.ToInt32(cboTipoIdentificacion.SelectedValue);
            medico.numeroIdentificacion = txtNumeroIdentificacion.Text.Trim();
            medico.idEspecialidad = 1;//TODO: Revisar si se necesita ingresar la especialidad en este punto
            medico.idEstado = 1;
            medico.createdBy = Session["login"].ToString();
            medico.createdDate = DateTime.Now;
            return medico;
        }

        /// <summary>
        /// Limpia todos los campos del formulario
        /// </summary>
        private void limpiarCampos()
        {
            txtPrimerNombre.Text = string.Empty;
            txtSegundoNombre.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            cboTipoIdentificacion.SelectedValue = string.Empty;
            cboTipoIdentificacion.Text = string.Empty;
            cboTipoIdentificacion.EmptyMessage = "Digite para buscar...";
            txtNumeroIdentificacion.Text = string.Empty;
            rdpFechaNacimiento.SelectedDate = null;
            cboCiudad.SelectedValue = string.Empty;
            cboCiudad.Text = string.Empty;
            cboCiudad.EmptyMessage = "Digite para buscar...";
            txtCelular.Text = string.Empty;
            txtTelefonoFijo.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            cboTipoIdentificacion.SelectedValue = string.Empty;
            cboTipoIdentificacion.Text = string.Empty;
            cboTipoIdentificacion.EmptyMessage = "Digite para buscar...";
            txtNumeroIdentificacion.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            cboEmpresa.SelectedValue = string.Empty;
            cboEmpresa.Text = string.Empty;
            cboEmpresa.EmptyMessage = "Digite para buscar...";
            txtContrasena.Text = string.Empty;
            cboRol.ClearCheckedItems();
            panelGrilla.Visible = true;
            panelComboUsuario.Visible = false;
            panelAgregarUsuario.Visible = false;
        }

        /// <summary>
        /// Verifica los campos (Todos son obligatorios)
        /// </summary>
        /// <returns></returns>
        private string verificarCampos()
        {
            string camposVacios = "El/Los campo/s ";
            if(cboTipoIdentificacion.SelectedValue.Equals(string.Empty))
                camposVacios += "Tipo identificación ";
            if (txtNumeroIdentificacion.Text.Equals(string.Empty))
                camposVacios += "Numero identificacion ";
            if(txtPrimerNombre.Text.Equals(string.Empty))
                camposVacios += "Primer nombre ";
            //if(txtSegundoNombre.Text.Equals(string.Empty))
            //    camposVacios += "Segundo nombre ";
            if(txtPrimerApellido.Text.Equals(string.Empty))
                camposVacios += "Primer apellido ";
            //if(txtSegundoApellido.Text.Equals(string.Empty))
            //    camposVacios += "Segundo apellido ";
            if(rdpFechaNacimiento.SelectedDate.ToString().Equals(string.Empty))
                camposVacios += "Fecha nacimiento ";
            if(cboCiudad.SelectedValue.Equals(string.Empty))
                camposVacios += "Ciudad ";
            if(txtCelular.Text.Equals(string.Empty))
                camposVacios += "Celular ";
            if(txtTelefonoFijo.Text.Equals(string.Empty))
                camposVacios += "Teléfono fijo ";
            if(txtCorreo.Text.Equals(string.Empty))
                camposVacios += "Correo ";
            if (!chkMedicoTratante.Checked)
            {
                if (cboRol.CheckedItems.Count == 0)
                    camposVacios += "Rol ";
                if (txtUsuario.Text.Equals(string.Empty))
                    camposVacios += "Usuario ";
                if (cboEmpresa.SelectedValue.Equals(string.Empty))
                    camposVacios += "Empresa ";
                if (txtContrasena.Text.Equals(string.Empty))
                {
                    if (btnAgregarUsuario.Visible == true)
                        camposVacios += "Contraseña ";
                }
                if (cboEstado.SelectedValue.Equals("0"))
                    camposVacios += "Estado ";
            }            
            if (camposVacios.Length > 15)
                camposVacios += "no puede/n estar vacíos";
            else
            {
                return string.Empty;
            }
            return camposVacios;
        }

        /// <summary>
        /// Cambia el idioma de los filtros de las grillas
        /// </summary>
        /// <param name="grilla"></param>
        private void CambiarIdiomaFilterGridTelerik(RadGrid grilla)
        {
            List<GridFilterMenu> grids = new List<GridFilterMenu>();
            grids.Add(grilla.FilterMenu);
            foreach (GridFilterMenu gridFilterMenu in grids)
            {
                //GridFilterMenu menu = RadGridCasosRadicados.FilterMenu;
                GridFilterMenu menu = gridFilterMenu;
                foreach (RadMenuItem item in menu.Items)
                {
                    //change the text for the "StartsWith" menu item
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
        #endregion
    }
}