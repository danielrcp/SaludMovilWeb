using Newtonsoft.Json.Linq;
using SaludMovil.Entidades;
using SaludMovil.Negocio;
using SaludMovil.Transversales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SaludMovil.Portal.ModPacientes
{
    public partial class RegistroPaciente : System.Web.UI.Page
    {
        private PacienteNegocio negocioPaciente;
        private ProgramaNegocio negocioPrograma;
        private AdministracionNegocio negocioAdministracion;
        private sm_Paciente paciente;
        private sm_Persona persona;
        private sm_PacientePrograma pacientePrograma;
        private sm_GuiaPaciente guiaPaciente;
        private sm_PersonalMedico medico;
        private sm_Guia guia;
        public static string login;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblLeyenda.Text = "PORTAL MÉDICO / REGISTRO DE PACIENTES";
            login = (String)Session["login"];
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Request.QueryString["idTipoIdentificacion"] != null && Request.QueryString["NumeroIdentifacion"] != null)
                    {
                        persona = new sm_Persona();
                        negocioPaciente = new PacienteNegocio();
                        hdfidTipoIdentificacion.Value = Request.QueryString["idTipoIdentificacion"];
                        hdfNumeroIdentificacion.Value = Request.QueryString["NumeroIdentifacion"];
                        persona = negocioPaciente.ConsultarPersona(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                        CargarInformacion(persona);
                        txtIdentificacion.Enabled = false;
                        rdpFechaNacimiento.Enabled = false;
                        txtPrimerNombre.Enabled = false;
                        txtSegundoNombre.Enabled = false;
                        txtPrimerApellido.Enabled = false;
                        txtSegundoApellido.Enabled = false;
                        txtPlanmp.Enabled = false;
                        txtTipoContrato.Enabled = false;
                        txtNombreColectivo.Enabled = false;
                        txtSegmento.Enabled = false;
                        RadWizardStep2.Visible = true;
                        RadWizardStep3.Visible = true;
                        RadWizardStep4.Visible = true;
                        RadWizardStep5.Visible = true;
                    }
                    else
                    {
                        RadWizardStep2.Visible = false;
                        RadWizardStep3.Visible = false;
                        RadWizardStep4.Visible = false;
                        RadWizardStep5.Visible = false;
                        CargarPagina();
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message, true);
                }
            }
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            string msg = mensaje;
            if (esError)
                msg = "Error: " + mensaje.Replace("'", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
            RadNotificationMensajes.Show(msg);
        }

        #region Información Pestañas
        /// <summary>
        /// Inicializa listas con los valores para un registro de pacientes nuevo
        /// </summary>
        private void CargarPagina()
        {
            CargarIdentificaciones();
            CargarCiudades();
            CargarPrograma();
            CargarParentesco();
            CargarMedioAtencion();
            CargarTipoIngreso();
            CargarCentrosSalud();
            CargarEspecilidades();
            CargarMedicosTratantes();
            CargarRiesgos();
        }

        /// <summary>
        /// Carga combos y grillas de la  pestaña datos programa filtrados por parametrizacion previa en la creacion de la guia por cada programa asignado al paciente
        /// </summary>
        private void CargarPaginaDatosPrograma(int idPrograma)
        {
            Parallel.Invoke(
                () => CargarDiagnosticos(idPrograma),
                () => radGridDiagnosticos.Rebind(),
                () => radGridTensionSistolica.Rebind(),
                () => radGridPeso.Rebind(),
                () => radGridTalla.Rebind(),
                () => radGridInterconsultas.Rebind(),
                () => radGridExamenesAyudas.Rebind(),
                () => radGridMedicamentos.Rebind(),
                () => radGridControlEnfermeria.Rebind(),
                () => radGridMedicionesBiometricas.Rebind()
                );
        }

        /// <summary>
        /// Carga datos con la información almacenada para el paciente en la base de datos, para su modificación o consulta
        /// </summary>
        /// <param name="idPaciente"></param>
        private void CargarInformacion(sm_Persona persona)
        {
            negocioPaciente = new PacienteNegocio();
            paciente = new sm_Paciente();
            medico = new sm_PersonalMedico();
            List<sm_GuiaPaciente> ListGuiaPaciente = new List<sm_GuiaPaciente>();
            List<sm_PacientePrograma> myLists = new List<sm_PacientePrograma>();
            pacientePrograma = new sm_PacientePrograma();
            try
            {
                Parallel.Invoke(
                    () => CargarIdentificaciones(),
                     () => CargarCiudades(),
                      () => CargarPrograma(),
                       () => CargarParentesco(),
                        () => CargarMedioAtencion(),
                         () => CargarTipoIngreso(),
                          () => CargarCentrosSalud(),
                           () => CargarEspecilidades(),
                            () => CargarMedicosTratantes(),
                            () => CargarRiesgos()
                    );
               
                //Persona
                cboTipoIdentificacion.SelectedValue = persona.idTipoIdentificacion.ToString();
                txtIdentificacion.Text = persona.numeroIdentificacion;
                txtPrimerNombre.Text = persona.primerNombre;
                txtSegundoNombre.Text = persona.segundoNombre;
                txtPrimerApellido.Text = persona.primerApellido;
                txtSegundoApellido.Text = persona.segundoApellido;
                rdpFechaNacimiento.SelectedDate = persona.fechaNacimiento;
                cboCiudad.SelectedValue = persona.idCiudad.ToString();
                txtCelular.Text = persona.celular;
                txtTelefonoFijo.Text = persona.telefonoFijo;
                txtCorreo.Text = persona.correo;

                //Paciente
                paciente = negocioPaciente.ConsultarPaciente(persona.idTipoIdentificacion, persona.numeroIdentificacion);
                txtSegmento.Text = paciente.segmento;
                txtPlanmp.Text = paciente.planMp;
                txtTipoContrato.Text = paciente.tipoContrato;
                txtNombreColectivo.Text = paciente.nombreColectivo;
                cboCentroSalud.SelectedValue = paciente.institucion.ToString();
                cboMedicoTratante.SelectedValue = paciente.idTipoIdentificacionMedico + "|" + paciente.numeroIdentificacionMedico;
                cboNotificacionesResponsable.SelectedValue = paciente.recibeNotParentesco.ToString();
                txtNombresParentesco.Text = paciente.nombresParantesco;
                cboParentesco.SelectedValue = paciente.idParentesco.ToString();
                txtCelularParentesco.Text = paciente.celularParentesco;
                txtTelefonofijoparentesco.Text = paciente.telefonoFijoParentesco;
                txtCorreoParentesco.Text = paciente.correoParentesco;
                cboTipoIngreso.SelectedValue = paciente.idTipoIngreso.ToString();
                cboMedioAtencion.SelectedValue = paciente.idMedioAtencion.ToString();
                rdpFechaIngreso.SelectedDate = paciente.fechaRegistro;
                cboRiesgo.SelectedValue = paciente.riesgo.ToString();
                hdfEstadoPaciente.Value = paciente.idEstado.ToString();
                var ListEstados = negocioPaciente.ConsultaBandejaControles(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOCONTROLES);
                if (ListEstados.Count > 0)
                {
                    var estado = ListEstados.OrderByDescending(e => e.idGuiaPaciente).FirstOrDefault();
                    cboEstadoPaciente.SelectedValue = estado.txt4;
                    rdpFechaActualizacionPaciente.SelectedDate = estado.createdDate;
                }
                else
                {
                    rdpFechaActualizacionPaciente.SelectedDate = paciente.updatedDate;
                }
                if (paciente.idEstado == 4)
                {
                    cboEstadoPaciente.SelectedValue = "4";
                    rdpFechaActualizacionPaciente.SelectedDate = paciente.createdDate;
                }
                if (paciente.esMonitoreado.Equals(true))
                {
                    cboSiNoMonotoreo.SelectedValue = "1";
                    panelAspectos.Visible = true;
                    ListGuiaPaciente = negocioPaciente.ConsultarAspectosMonitoreados(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOASPECTOS);
                    foreach (sm_GuiaPaciente gp in ListGuiaPaciente)
                    {
                        foreach (RadComboBoxItem c in cboAspectos.Items)
                        {
                            if (c.Value.ToString().Equals(gp.txt1.ToString()))
                            {
                                c.Checked = true;
                            }
                        }
                        if (gp.txt1.Equals("1"))
                            RadPageViewMedicamentos.Visible = true;
                        else
                            RadPageViewMedicionesBiometricas.Visible = true;
                    }
                }
                else if (paciente.esMonitoreado.Equals(false))
                {
                    cboSiNoMonotoreo.SelectedValue = "2";
                }
                else
                {
                    cboSiNoMonotoreo.SelectedValue = "0";
                }
                //Médico
                medico = negocioPaciente.ConsultarMedicoTratante(paciente.idTipoIdentificacionMedico, paciente.numeroIdentificacionMedico);
                if (medico != null)
                {
                    cboEspecialidad.SelectedValue = medico.idEspecialidad.ToString();   
                }
                //PacientePrograma
                myLists = negocioPaciente.ConsultarPacientePrograma(paciente.idTipoIdentificacion, paciente.numeroIdentificacion);
                pacientePrograma = myLists.Find(T => T.idTipoIdentificacion == paciente.idTipoIdentificacion && T.numeroIdentificacion == paciente.numeroIdentificacion);
                cboPrograma.SelectedValue = pacientePrograma.idPrograma.ToString();
                hdfPrograma.Value = pacientePrograma.idPrograma.ToString();
                hdfRiesgo.Value = paciente.riesgo.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Información Pestañas

        #region CargaComboBox

        /// <summary>
        /// Carga combo con medicos tratantes
        /// </summary>
        private void CargarMedicosTratantes()
        {
            negocioPaciente = new PacienteNegocio();
            cboMedicoTratante.DataSource = negocioPaciente.ListarMedicosTratantes(Constantes.TIPOMEDICO);
            cboMedicoTratante.DataValueField = "idPersonalMedico";
            cboMedicoTratante.DataTextField = "Nombres";
            cboMedicoTratante.DataBind();
        }

        /// <summary>
        /// Carga combo con listado de riesgos
        /// </summary>
        private void CargarRiesgos()
        {
            negocioPaciente = new PacienteNegocio();
            cboRiesgo.DataSource = negocioPaciente.listarRiesgos();
            cboRiesgo.DataValueField = "idRiesgo";
            cboRiesgo.DataTextField = "nombre";
            cboRiesgo.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboRiesgo.DataBind();
        }

        /// <summary>
        /// Carga combo centros de salud
        /// </summary>
        private void CargarCentrosSalud()
        {
            negocioPaciente = new PacienteNegocio();
            cboCentroSalud.DataSource = negocioPaciente.ListarCentrosSalud();
            cboCentroSalud.DataValueField = "idCentroSalud";
            cboCentroSalud.DataTextField = "nombre";
            cboCentroSalud.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCentroSalud.DataBind();
        }

        /// <summary>
        /// Carga lista de especialidades
        /// </summary>
        private void CargarEspecilidades()
        {
            negocioPaciente = new PacienteNegocio();
            cboEspecialidad.DataSource = negocioPaciente.ListarEspecialidades();
            cboEspecialidad.DataValueField = "idEspecialidad";
            cboEspecialidad.DataTextField = "nombre";
            cboEspecialidad.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboEspecialidad.DataBind();
        }

        /// <summary>
        /// Carga lista de identificaciones
        /// </summary>
        private void CargarIdentificaciones()
        {

            negocioPaciente = new PacienteNegocio();
            cboTipoIdentificacion.DataSource = negocioPaciente.listarTiposIdentificacion();
            cboTipoIdentificacion.DataValueField = "idTipoIdentificacion";
            cboTipoIdentificacion.DataTextField = "nombre";
            cboTipoIdentificacion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboTipoIdentificacion.DataBind();
        }

        /// <summary>
        /// Carga lista de ciudades
        /// </summary>
        private void CargarCiudades()
        {
            negocioPaciente = new PacienteNegocio();
            cboCiudad.DataSource = negocioPaciente.listarCiudades();
            cboCiudad.DataValueField = "idCiudad";
            cboCiudad.DataTextField = "nombre";
            cboCiudad.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboCiudad.DataBind();
        }


        /// <summary>
        /// Carga lista de programas
        /// </summary>
        private void CargarPrograma()
        {
            negocioPrograma = new ProgramaNegocio();
            cboPrograma.DataSource = negocioPrograma.listarProgramas();
            cboPrograma.DataValueField = "idPrograma";
            cboPrograma.DataTextField = "descripcion";
            cboPrograma.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboPrograma.DataBind();
        }


        /// <summary>
        /// Carga lista con los parentescos
        /// </summary>
        private void CargarParentesco()
        {
            negocioPaciente = new PacienteNegocio();
            cboParentesco.DataSource = negocioPaciente.listarParentescos();
            cboParentesco.DataValueField = "idParentesco";
            cboParentesco.DataTextField = "nombre";
            cboParentesco.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboParentesco.DataBind();

        }

        /// <summary>
        /// Carga lista con los medios de atencion
        /// </summary>
        private void CargarMedioAtencion()
        {
            negocioPaciente = new PacienteNegocio();
            cboMedioAtencion.DataSource = negocioPaciente.listarMediosAtencion();
            cboMedioAtencion.DataValueField = "idMedioAtencion";
            cboMedioAtencion.DataTextField = "descripcion";
            cboMedioAtencion.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboMedioAtencion.DataBind();
        }

        /// <summary>
        /// Carga lista de tipos de ingreso
        /// </summary>
        private void CargarTipoIngreso()
        {
            negocioPaciente = new PacienteNegocio();
            cboTipoIngreso.DataSource = negocioPaciente.listarTiposIngreso();
            cboTipoIngreso.DataValueField = "idTipoIngreso";
            cboTipoIngreso.DataTextField = "descripcion";
            cboTipoIngreso.Items.Insert(0, new ListItem("Seleccione...", "-1"));
            cboTipoIngreso.DataBind();
        }

        /// <summary>
        /// Carga lista de diagnosticos filtrados por cada programa
        /// </summary>
        /// <returns></returns>
        private void CargarDiagnosticos(int idPrograma)
        {
            negocioPaciente = new PacienteNegocio();
            cboDiagnosticos.DataSource = negocioPaciente.ListarGuiasPorProgramaTipo(idPrograma, Constantes.TIPOGUIADIAGNOSTICOS, 0);
            cboDiagnosticos.DataValueField = "idGuia";
            cboDiagnosticos.DataTextField = "Descripcion";
            cboDiagnosticos.DataBind();
        }

        /// <summary>
        /// Carga lista con todos los diagnosticos disponibles
        /// </summary>
        private void CargarOtrosDiagnosticos(string ocurrencia)
        {

            cboOtrosDiagnosticos.DataSource = Comun.listaGenerica(Constantes.OTROSDIAGNOSTICOS, ConfigurationManager.AppSettings["DIAGNOSTICONOMBREWS"].ToString() + ocurrencia);
            cboOtrosDiagnosticos.DataValueField = "Codigo";
            cboOtrosDiagnosticos.DataTextField = "Descripcion";
            cboOtrosDiagnosticos.DataBind();
        }


        /// <summary>
        /// Simulación web service local 
        /// </summary>
        /// <param name="ocurrencia"></param>
        private void CargarOtrasInterconsultas(string ocurrencia)
        {
            cboOtrasInterconsultas.DataSource = Comun.listaGenerica(Constantes.INTERCONSULTA, ConfigurationManager.AppSettings["PRESTACIONESINTERCONSULTAWS"].ToString() + ocurrencia + "?Tipo=1"); //servicios.diagnosticos(ocurrencia, Constantes.INTERCONSULTA);
            cboOtrasInterconsultas.DataValueField = "Codigo";
            cboOtrasInterconsultas.DataTextField = "Descripcion";
            cboOtrasInterconsultas.DataBind();
        }

        /// <summary>
        /// Consume web service otras ayudas diagnosticas
        /// </summary>
        private void CargarOtrasAyudasDiagnosticas(string ocurrencia)
        {
            cboOtrosExamenesProcedimientos.DataSource = Comun.listaGenerica(Constantes.OTROSPROCEDIMIENTOS, ConfigurationManager.AppSettings["PRESTACIONESINTERCONSULTAWS"].ToString() + ocurrencia + "?Tipo=3"); //servicios.diagnosticos(ocurrencia, Constantes.INTERCONSULTA);
            cboOtrosExamenesProcedimientos.DataValueField = "Codigo";
            cboOtrosExamenesProcedimientos.DataTextField = "Descripcion";
            cboOtrosExamenesProcedimientos.DataBind();
        }

        /// <summary>
        /// Consume web service para medicamentos
        /// </summary>
        /// <param name="ocurrencia"></param>
        private void CargarListadoMedicamentos(string ocurrencia)
        {
            cboMedicamentos.DataSource = Comun.listaGenerica(Constantes.MEDICAMENTO, ConfigurationManager.AppSettings["MEDICAMENTONOMBREWS"].ToString() + ocurrencia + "?Activos=true"); //servicios.diagnosticos(ocurrencia, Constantes.INTERCONSULTA);
            cboMedicamentos.DataValueField = "Codigo";
            cboMedicamentos.DataTextField = "Descripcion";
            cboMedicamentos.DataBind();
        }


        /// <summary>
        /// Consume web service para cargar otros examenes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void CargarOtrosExamenesLaboratorios()
        //{
        //    cboOtrasAyudas.DataSource = Comun.listaGenerica("EXAMENES DE LABORATORIO", "http://10.17.4.4:9101/SrvPrestaciones.svc/rest/PrestacionesAgrupacionRiesgo?Tipo=2"); //servicios.diagnosticos(ocurrencia, Constantes.INTERCONSULTA);
        //    cboOtrasAyudas.DataValueField = "Codigo";
        //    cboOtrasAyudas.DataTextField = "Descripcion";
        //    cboOtrasAyudas.DataBind();
        //}

        /// <summary>
        /// Carga diagnósticos parametrizados
        /// </summary>
        /// <param name="idPrograma"></param>
        private void CargarDiagnosticosAutomaticos(int idPrograma)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                paciente = new sm_Paciente();
                paciente = negocioPaciente.ConsultarPaciente(Convert.ToInt32(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                GuardarDiagnosticosParametricos(negocioPaciente.ListarGuiasPorProgramaTipo(idPrograma, Constantes.TIPOGUIADIAGNOSTICOS, paciente.riesgo), 1);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Carga interconsultas parametrizadas
        /// </summary>
        /// <param name="idPrograma"></param>
        private void CargarInterconsultas(int idPrograma)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                paciente = new sm_Paciente();
                paciente = negocioPaciente.ConsultarPaciente(Convert.ToInt32(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                GuardarInterconsultas(negocioPaciente.ListarGuiasPorProgramaTipo(idPrograma, Constantes.TIPOGUIAINTERCONSULTAS, paciente.riesgo), 1);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Carga exámenes parametrizadas
        /// </summary>
        /// <param name="idPrograma"></param>
        private void CargarExamenes(int idPrograma)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                paciente = new sm_Paciente();
                paciente = negocioPaciente.ConsultarPaciente(Convert.ToInt32(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                GuardarExamenes(negocioPaciente.ListarGuiasPorProgramaTipo(idPrograma, Constantes.TIPOGUIAEXAMEN, paciente.riesgo), 1);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Carga exámenes y otros procedimientos parametrizados
        /// </summary>
        /// <param name="idPrograma"></param>
        private void CargarExamenesOtrosProcedimientos(int idPrograma)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                paciente = new sm_Paciente();
                paciente = negocioPaciente.ConsultarPaciente(Convert.ToInt32(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                GuardarExamenesProcedimientos(negocioPaciente.ListarGuiasPorProgramaTipo(idPrograma, Constantes.TIPOGUIAOTROSPROCEDIMIENTOS, paciente.riesgo), 1);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Carga medicamentos parametrizados
        /// </summary>
        /// <param name="idPrograma"></param>
        private void CargarMedicamentos(int idPrograma)
        {
            negocioPaciente = new PacienteNegocio();
            cboMedicamentos.DataSource = negocioPaciente.ListarGuiasPorProgramaTipo(idPrograma, Constantes.TIPOGUIAMEDICAMENTO, 0);
            cboMedicamentos.DataValueField = "idGuia";
            cboMedicamentos.DataTextField = "Descripcion";
            cboMedicamentos.DataBind();
        }

        #endregion CargaComboBox

        #region GuardarActualizarDatos

        /// <summary>
        /// Consume web service citas medicas
        /// </summary>
        private void GuardarCitasMedicasWs()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    WebClient proxy = new WebClient();
                    string tipoIdentificacion = cboTipoIdentificacion.SelectedItem.Text;
                    string serviceURL = ConfigurationManager.AppSettings["URLSERVICIOCITAS"] + tipoIdentificacion + "/" + txtIdentificacion.Text.Trim() + "?FechaInicial=" + DateTime.Today.Year + "-01-01&FechaFinal=" + DateTime.Today.Year + "-12-01";
                    if (Comun.isValidURL(serviceURL))
                    {
                        byte[] _data = proxy.DownloadData(serviceURL);
                        Stream _mem = new MemoryStream(_data);
                        var reader = new StreamReader(_mem);
                        var result = reader.ReadToEnd();
                        JArray v = JArray.Parse(result.ToString());
                        foreach (JObject oJson in v)
                        {
                            negocioPrograma = new ProgramaNegocio();
                            sm_Guia guia = new sm_Guia();
                            sm_GuiaPaciente guiaPaciente = new sm_GuiaPaciente();
                            guia.idPrograma = Convert.ToInt16(hdfPrograma.Value);
                            guia.idRiesgo = 1;
                            guia.cantidadRiesgo = 0;
                            guia.idCodigoTipo = oJson["IdCita"].ToString();
                            guia.descripcion = oJson["Especialidad"].ToString();
                            guia.idTipoGuia = Constantes.TIPOGUIACITASMEDICAS;
                            guia.idEstado = 1;
                            guia.createdBy = login;
                            guia.createdDate = DateTime.Now;
                            guia.version = Constantes.version;
                            negocioPrograma.GuardarGuia(guia);
                            guiaPaciente.idGuia = guia.idGuia;
                            guiaPaciente.createdDate = DateTime.Now;
                            guiaPaciente.createdBy = login;
                            guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                            guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                            guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                            guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                            guiaPaciente.tipoEvento = Constantes.TIPOEVENTOCITASMEDICAS;
                            guiaPaciente.valor1 = guia.idRiesgo;
                            guiaPaciente.valor2 = guia.cantidadRiesgo;
                            guiaPaciente.fechaEvento = Convert.ToDateTime(oJson["Fecha"]);
                            guiaPaciente.txt1 = oJson["Estado"].ToString();
                            guiaPaciente.txt2 = oJson["Asistio"].ToString();
                            negocioPaciente = new PacienteNegocio();
                            negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
        /// <summary>
        /// Guarda registro de paciente, se incluyen parámetros del wizard para poder hacer validaciones específicas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int GuardarPaciente(object sender, WizardEventArgs e)
        {
            paciente = new sm_Paciente();
            negocioPaciente = new PacienteNegocio();
            negocioAdministracion = new AdministracionNegocio();
            pacientePrograma = new sm_PacientePrograma();
            medico = new sm_PersonalMedico();
            persona = new sm_Persona();
            sm_Usuario usuario = new sm_Usuario();
            sm_UsuarioRol usuarioRol = new sm_UsuarioRol();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Persona
                    persona.idTipoIdentificacion = Convert.ToInt32(cboTipoIdentificacion.SelectedValue);
                    persona.numeroIdentificacion = txtIdentificacion.Text;
                    persona.primerNombre = txtPrimerNombre.Text;
                    persona.segundoNombre = txtSegundoNombre.Text;
                    persona.primerApellido = txtPrimerApellido.Text;
                    persona.segundoApellido = txtSegundoApellido.Text;
                    persona.idTipo = Constantes.TIPOPACIENTE;
                    persona.fechaNacimiento = Convert.ToDateTime(rdpFechaNacimiento.SelectedDate);
                    persona.idCiudad = Convert.ToInt32(cboCiudad.SelectedValue.ToString());
                    persona.celular = txtCelular.Text;
                    persona.telefonoFijo = txtTelefonoFijo.Text;
                    persona.correo = txtCorreo.Text;
                    persona.createdBy = login;
                    persona.createdDate = DateTime.Now;
                    if (persona.idTipoIdentificacion != -1 || persona.numeroIdentificacion != string.Empty || persona.primerNombre != string.Empty || persona.primerApellido != string.Empty ||
                        persona.fechaNacimiento.ToString() != string.Empty)
                    {
                        negocioPaciente.GuardarPersona(persona);
                    }
                    else
                    {
                        ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                        throw new Exception("Los campos obligatorios están vacíos");
                    }

                    //Paciente
                    paciente.idTipoIdentificacion = persona.idTipoIdentificacion;
                    paciente.numeroIdentificacion = persona.numeroIdentificacion;
                    string[] ids = cboMedicoTratante.SelectedValue.Split('|');
                    paciente.idTipoIdentificacionMedico = Convert.ToInt16(ids[0]);
                    paciente.numeroIdentificacionMedico = ids[1];
                    paciente.segmento = txtSegmento.Text;
                    paciente.planMp = txtPlanmp.Text;
                    paciente.tipoContrato = txtTipoContrato.Text;
                    paciente.nombreColectivo = txtNombreColectivo.Text;
                    paciente.institucion = Convert.ToInt16(cboCentroSalud.SelectedValue);
                    paciente.recibeNotParentesco = Convert.ToInt16(cboNotificacionesResponsable.SelectedValue);

                    if (paciente.recibeNotParentesco == 1)
                    {
                        paciente.nombresParantesco = txtNombresParentesco.Text;
                        paciente.idParentesco = Convert.ToInt16(cboParentesco.SelectedValue.ToString());
                        paciente.celularParentesco = txtCelularParentesco.Text;
                        paciente.telefonoFijoParentesco = txtTelefonofijoparentesco.Text;
                        paciente.correoParentesco = txtCorreoParentesco.Text;
                    }
                    else
                    {
                        paciente.nombresParantesco = string.Empty;
                        paciente.idParentesco = null;
                        paciente.celularParentesco = string.Empty;
                        paciente.telefonoFijoParentesco = string.Empty;
                        paciente.correoParentesco = string.Empty;
                    }
                    paciente.idTipoIngreso = Convert.ToInt16(cboTipoIngreso.SelectedValue.ToString());
                    paciente.idMedioAtencion = Convert.ToInt16(cboMedioAtencion.SelectedValue.ToString());
                    paciente.createdBy = login; //TODO: Falta el Login
                    paciente.createdDate = DateTime.Now;
                    paciente.fechaRegistro = rdpFechaIngreso.SelectedDate;
                    paciente.riesgo = Convert.ToInt32(cboRiesgo.SelectedValue);
                    paciente.idEstado = 1; // TODO:Falta estados
                    if (paciente.idTipoIngreso == -1 || paciente.riesgo == -1 || paciente.institucion == -1 || paciente.idMedioAtencion == -1)
                    {
                        ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                        throw new Exception("Los campos obligatorios están vacíos");
                    }
                    negocioPaciente.GuardarPaciente(paciente);
                    negocioPaciente.ActualizarEspecialidadMedico(paciente.idTipoIdentificacionMedico, paciente.numeroIdentificacionMedico, Convert.ToInt16(cboEspecialidad.SelectedValue));


                    //Programa paciente
                    pacientePrograma.idEstado = 1; //TODO: Falta la tabla
                    pacientePrograma.idTipoIdentificacion = paciente.idTipoIdentificacion;
                    pacientePrograma.numeroIdentificacion = paciente.numeroIdentificacion;
                    pacientePrograma.idPrograma = Convert.ToInt16(cboPrograma.SelectedValue);
                    pacientePrograma.createdBy = login; //TODO: Falta Login
                    pacientePrograma.createdDate = DateTime.Now;
                    if (pacientePrograma.idPrograma == -1)
                    {
                        ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                        throw new Exception("Los campo programa es obligatorio");
                    }
                    if (paciente.recibeNotParentesco == 0)
                    {
                        ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                        throw new Exception("Los campo recibe notificaciones parentesco es obligatorio");
                    }
                    negocioPaciente.GuardarPacientePrograma(pacientePrograma);

                    //Se implementa este framento de código para hacer la creación de usuario tipo paciente al momento de crearlo
                    usuario.idTipoIdentificacion = persona.idTipoIdentificacion;
                    usuario.numeroIdentificacion = persona.numeroIdentificacion;
                    usuario.usuario = persona.numeroIdentificacion;
                    usuario.contrasena = persona.numeroIdentificacion;
                    usuario.idEmpresa = 1;//TODO:Validar lista de empresas
                    usuario.createdBy = login;
                    usuario.createdDate = DateTime.Now;
                    negocioAdministracion.GuardarUsuario(usuario);
                    usuarioRol.IdUsuario = usuario.idUsuario;
                    usuarioRol.RoleId = 3;
                    negocioAdministracion.GuardarRol(usuarioRol);
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    hdfidTipoIdentificacion.Value = persona.idTipoIdentificacion.ToString();
                    hdfNumeroIdentificacion.Value = persona.numeroIdentificacion.ToString();
                    hdfPrograma.Value = pacientePrograma.idPrograma.ToString();
                    hdfRiesgo.Value = paciente.riesgo.ToString();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key"))
                {
                    MostrarMensaje("El usuario ya se encuentra registrado", true);
                }
                else
                {
                    MostrarMensaje(ex.Message, true);
                }
            }
            return pacientePrograma.idPrograma;
        }

        /// <summary>
        /// Actualiza información general paciente
        /// </summary>
        /// <param name="idTipoIdentificacion"></param>
        /// <param name="numeroIdentificacion"></param>
        /// <returns></returns>
        private int ActualizarPaciente(int idTipoIdentificacion, string numeroIdentificacion)
        {
            paciente = new sm_Paciente();
            negocioPaciente = new PacienteNegocio();
            pacientePrograma = new sm_PacientePrograma();
            medico = new sm_PersonalMedico();
            persona = new sm_Persona();

            try
            {
                //Persona
                persona.idTipoIdentificacion = Convert.ToInt32(cboTipoIdentificacion.SelectedValue);
                persona.numeroIdentificacion = txtIdentificacion.Text;
                persona.primerNombre = txtPrimerNombre.Text;
                persona.segundoNombre = txtSegundoNombre.Text;
                persona.primerApellido = txtPrimerApellido.Text;
                persona.segundoApellido = txtSegundoApellido.Text;
                persona.idTipo = Constantes.TIPOPACIENTE;
                persona.fechaNacimiento = Convert.ToDateTime(rdpFechaNacimiento.SelectedDate);
                persona.idCiudad = Convert.ToInt32(cboCiudad.SelectedValue.ToString());
                persona.celular = txtCelular.Text;
                persona.telefonoFijo = txtTelefonoFijo.Text;
                persona.correo = txtCorreo.Text;
                persona.createdBy = login;
                persona.createdDate = DateTime.Now;
                negocioPaciente.ActualizarPersona(persona);

                //Paciente
                paciente = negocioPaciente.ConsultarPaciente(persona.idTipoIdentificacion, persona.numeroIdentificacion);
                paciente.esMonitoreado = paciente.esMonitoreado;
                paciente.idTipoIdentificacion = persona.idTipoIdentificacion;
                paciente.numeroIdentificacion = persona.numeroIdentificacion;
                string[] ids = cboMedicoTratante.SelectedValue.Split('|');
                paciente.idTipoIdentificacionMedico = Convert.ToInt16(ids[0]);
                paciente.numeroIdentificacionMedico = ids[1];
                paciente.segmento = txtSegmento.Text;
                paciente.planMp = txtPlanmp.Text;
                paciente.tipoContrato = txtTipoContrato.Text;
                paciente.nombreColectivo = txtNombreColectivo.Text;
                if (Convert.ToInt16(cboCentroSalud.SelectedValue) == -1)
                {
                    paciente.institucion = 1;
                }
                else
                {
                    paciente.institucion = Convert.ToInt16(cboCentroSalud.SelectedValue);
                }
                paciente.recibeNotParentesco = Convert.ToInt16(cboNotificacionesResponsable.SelectedValue);
                if (paciente.recibeNotParentesco == 1)
                {
                    paciente.nombresParantesco = txtNombresParentesco.Text;
                    paciente.idParentesco = Convert.ToInt16(cboParentesco.SelectedValue.ToString());
                    paciente.celularParentesco = txtCelularParentesco.Text;
                    paciente.telefonoFijoParentesco = txtTelefonofijoparentesco.Text;
                    paciente.correoParentesco = txtCorreoParentesco.Text;
                }
                else
                {
                    paciente.nombresParantesco = string.Empty;
                    paciente.idParentesco = null;
                    paciente.celularParentesco = string.Empty;
                    paciente.telefonoFijoParentesco = string.Empty;
                    paciente.correoParentesco = string.Empty;
                }
                paciente.idTipoIngreso = Convert.ToInt16(cboTipoIngreso.SelectedValue.ToString());
                paciente.idMedioAtencion = Convert.ToInt16(cboMedioAtencion.SelectedValue.ToString());
                paciente.createdBy = login; //TODO: Falta el Login
                paciente.createdDate = DateTime.Now;
                paciente.fechaRegistro = rdpFechaIngreso.SelectedDate;
                paciente.riesgo = Convert.ToInt32(cboRiesgo.SelectedValue);
                paciente.idEstado = 1; // TODO:Falta estados
                //hdfEstadoPaciente.Value = paciente.idEstado.ToString();
                negocioPaciente.ActualizarPaciente(paciente);

                negocioPaciente.ActualizarEspecialidadMedico(paciente.idTipoIdentificacionMedico, paciente.numeroIdentificacionMedico, Convert.ToInt16(cboEspecialidad.SelectedValue));

                //Programa paciente
                pacientePrograma.idEstado = 1; //TODO: Falta la tabla
                pacientePrograma.idTipoIdentificacion = paciente.idTipoIdentificacion;
                pacientePrograma.numeroIdentificacion = paciente.numeroIdentificacion;
                pacientePrograma.idPrograma = Convert.ToInt16(cboPrograma.SelectedValue);
                pacientePrograma.createdBy = login; //TODO: Falta Login
                pacientePrograma.createdDate = DateTime.Now;
                //negocioPaciente.ActualizarPacientePrograma(pacientePrograma);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601)
                {
                    MostrarMensaje("El usuario que está ingresando ya se encuentra registrado", true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
            return pacientePrograma.idPrograma;
        }

        /// <summary>
        /// Guarda registro de diagnósticos por paciente
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="idsDiagnosticos"></param>
        /// <param name="marca"></param>
        private void GuardarDiagnosticos(sm_Paciente paciente, string[] idsDiagnosticos, int marca)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                for (int i = 0; i < idsDiagnosticos.Length; i++)
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = Convert.ToInt32(idsDiagnosticos[i]);
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    if (marca == 1)
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTODIAGNOSTICO;
                    }
                    else
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTROSDIAGNOSTICOS;
                    }
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                }
                radGridDiagnosticos.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }


        /// <summary>
        /// Guarda interconsultas parametrizadas en el programa
        /// </summary>
        /// <param name="ListarGuiasPorProgramaTipo"></param>
        /// <param name="marca"></param>
        private void GuardarDiagnosticosParametricos(IList<DiagnosticoProgramaTipo> ListarGuiasPorProgramaTipo, int marca)
        {
            try
            {
                negocioPrograma = new ProgramaNegocio();
                foreach (DiagnosticoProgramaTipo item in ListarGuiasPorProgramaTipo)
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    guia = new sm_Guia();
                    guia = negocioPrograma.retornarGuia(Convert.ToInt32(item.idGuia));
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = guia.idGuia;
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    if (marca == 1)
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTODIAGNOSTICO;
                    }
                    else
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTROSDIAGNOSTICOS;
                    }
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                }
                radGridDiagnosticos.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Guarda interconsultas parametrizadas en el programa
        /// </summary>
        /// <param name="ListarGuiasPorProgramaTipo"></param>
        /// <param name="marca"></param>
        private void GuardarInterconsultas(IList<DiagnosticoProgramaTipo> ListarGuiasPorProgramaTipo, int marca)
        {
            try
            {
                foreach (DiagnosticoProgramaTipo item in ListarGuiasPorProgramaTipo)
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    negocioPrograma = new ProgramaNegocio();
                    guia = new sm_Guia();
                    guia = negocioPrograma.retornarGuia(Convert.ToInt32(item.idGuia));
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = guia.idGuia;
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    if (marca == 1)
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTOINTERCONSULTAS;
                    }
                    else
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTRASINTERCONSULTAS;
                    }
                    guiaPaciente.valor1 = guia.idRiesgo;
                    guiaPaciente.valor2 = guia.cantidadRiesgo;
                    negocioPaciente = new PacienteNegocio();
                    int contador = 0;
                    int cantidad = Convert.ToInt16(guia.cantidadRiesgo);
                    while (contador < cantidad)
                    {
                        negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                        contador++;
                    }
                }
                radGridInterconsultas.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Guarda exámenes parametrizadas en el programa
        /// </summary>
        /// <param name="ListarGuiasPorProgramaTipo"></param>
        /// <param name="marca"></param>
        private void GuardarExamenes(IList<DiagnosticoProgramaTipo> ListarGuiasPorProgramaTipo, int marca)
        {
            try
            {
                foreach (DiagnosticoProgramaTipo item in ListarGuiasPorProgramaTipo)
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    negocioPrograma = new ProgramaNegocio();
                    guia = new sm_Guia();
                    guia = negocioPrograma.retornarGuia(Convert.ToInt32(item.idGuia));
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = guia.idGuia;
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    if (marca == 1)
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTOEXAMENES;
                    }
                    guiaPaciente.valor1 = guia.idRiesgo;
                    guiaPaciente.valor2 = guia.cantidadRiesgo;
                    negocioPaciente = new PacienteNegocio();
                    int contador = 0;
                    int cantidad = Convert.ToInt16(guia.cantidadRiesgo);
                    while (contador < cantidad)
                    {
                        negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                        contador++;
                    }
                }
                radGridExamenesAyudas.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Guarda exámenes y otros procedimientos parametrizados en el programa
        /// </summary>
        /// <param name="ListarGuiasPorProgramaTipo"></param>
        /// <param name="marca"></param>
        private void GuardarExamenesProcedimientos(IList<DiagnosticoProgramaTipo> ListarGuiasPorProgramaTipo, int marca)
        {
            try
            {
                foreach (DiagnosticoProgramaTipo item in ListarGuiasPorProgramaTipo)
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    negocioPrograma = new ProgramaNegocio();
                    guia = new sm_Guia();
                    guia = negocioPrograma.retornarGuia(Convert.ToInt32(item.idGuia));
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = guia.idGuia;
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    if (marca == 1)
                    {
                        guiaPaciente.tipoEvento = Constantes.TIPOEVENTOEXAMENESOTROSPROCEDIMIENTOS;
                    }
                    guiaPaciente.valor1 = guia.idRiesgo;
                    guiaPaciente.valor2 = guia.cantidadRiesgo;
                    negocioPaciente = new PacienteNegocio();
                    int contador = 0;
                    int cantidad = Convert.ToInt16(guia.cantidadRiesgo);
                    while (contador < cantidad)
                    {
                        negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                        contador++;
                    }
                }
                radGridOtrosExamenesProcedimiento.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Realiza el proceso de borrado e inserción de nuevas interconsultas modificando su periodicidad
        /// </summary>
        /// <param name="idCodigoTipo"></param>
        /// <param name="marca"></param>
        /// <param name="cantidadRiesgo"></param>
        private void GuardarInterconsultasModificadas(string idCodigoTipo, int marca, int cantidadRiesgo)
        {
            try
            {
                guiaPaciente = new sm_GuiaPaciente();
                negocioPrograma = new ProgramaNegocio();
                guia = new sm_Guia();
                guia = negocioPrograma.retornarGuiaPorCodigo(Convert.ToInt32(hdfPrograma.Value), Constantes.TIPOGUIAINTERCONSULTAS, idCodigoTipo, Convert.ToInt16(hdfRiesgo.Value));
                if (guia == null)
                {
                    guia = negocioPrograma.retornarGuiaPorCodigo(Convert.ToInt32(hdfPrograma.Value), Constantes.TIPOGUIAOTRASINTERCONSULTAS, idCodigoTipo, Convert.ToInt16(hdfRiesgo.Value));
                }
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = guia.idGuia;
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                if (marca == 1)
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOINTERCONSULTAS;
                }
                else
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTRASINTERCONSULTAS;
                }
                guiaPaciente.valor1 = guia.idRiesgo;
                guiaPaciente.valor2 = cantidadRiesgo;
                negocioPaciente = new PacienteNegocio();
                List<sm_GuiaPaciente> listGuiasExistentes = negocioPaciente.ConsultarGuiasPorCodigo(idCodigoTipo, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
                foreach (sm_GuiaPaciente item in listGuiasExistentes)
                {
                    item.valor2 = cantidadRiesgo;
                    negocioPaciente.ActualizarGuiaPaciente(item);
                }
                int contador = listGuiasExistentes.Count;
                int cantidad = Convert.ToInt16(guiaPaciente.valor2);
                while (contador < cantidad)
                {
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    contador++;
                }
                radGridInterconsultas.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Realiza el proceso de borrado e inserción de nuevos exámenes modificando su periodicidad
        /// </summary>
        /// <param name="idCodigoTipo"></param>
        /// <param name="marca"></param>
        /// <param name="cantidadRiesgo"></param>
        private void GuardarExamenesModificados(string idCodigoTipo, int marca, int cantidadRiesgo)
        {
            try
            {
                guiaPaciente = new sm_GuiaPaciente();
                negocioPrograma = new ProgramaNegocio();
                guia = new sm_Guia();
                guia = negocioPrograma.retornarGuiaPorCodigo(Convert.ToInt32(hdfPrograma.Value), Constantes.TIPOGUIAEXAMEN, idCodigoTipo, Convert.ToInt16(hdfRiesgo.Value));
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = guia.idGuia;
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                if (marca == 1)
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOEXAMENES;
                }
                else
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTRASAYUDAS;
                }
                guiaPaciente.valor1 = guia.idRiesgo;
                guiaPaciente.valor2 = cantidadRiesgo;
                negocioPaciente = new PacienteNegocio();
                List<sm_GuiaPaciente> listGuiasExistentes = negocioPaciente.ConsultarGuiasPorCodigo(idCodigoTipo, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
                foreach (sm_GuiaPaciente item in listGuiasExistentes)
                {
                    negocioPaciente.ActualizarGuiaPaciente(item);
                }
                int contador = listGuiasExistentes.Count;
                int cantidad = Convert.ToInt16(guiaPaciente.valor2);
                while (contador < cantidad)
                {
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    contador++;
                }
                radGridExamenesAyudas.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Realiza el proceso de borrado e inserción de nuevos exámenes y procedimientos modificando su periodicidad
        /// </summary>
        /// <param name="idCodigoTipo"></param>
        /// <param name="marca"></param>
        /// <param name="cantidadRiesgo"></param>
        private void GuardarExamenesyOtrosProcedimientoModificados(string idCodigoTipo, int marca, int cantidadRiesgo)
        {
            try
            {
                guiaPaciente = new sm_GuiaPaciente();
                negocioPrograma = new ProgramaNegocio();
                guia = new sm_Guia();
                guia = negocioPrograma.retornarGuiaPorCodigo(Convert.ToInt32(hdfPrograma.Value), Constantes.TIPOGUIAOTROSPROCEDIMIENTOS, idCodigoTipo, Convert.ToInt16(hdfRiesgo.Value));
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = guia.idGuia;
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                if (marca == 1)
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOEXAMENESOTROSPROCEDIMIENTOS;
                }
                else
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTRASAYUDAS;
                }
                guiaPaciente.valor1 = guia.idRiesgo;
                guiaPaciente.valor2 = cantidadRiesgo;
                negocioPaciente = new PacienteNegocio();
                List<sm_GuiaPaciente> listGuiasExistentes = negocioPaciente.ConsultarGuiasPorCodigo(idCodigoTipo, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
                foreach (sm_GuiaPaciente item in listGuiasExistentes)
                {
                    item.valor2 = cantidadRiesgo;
                    negocioPaciente.ActualizarGuiaPaciente(item);
                }
                int contador = listGuiasExistentes.Count;
                int cantidad = Convert.ToInt16(guiaPaciente.valor2);
                while (contador < cantidad)
                {
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    contador++;
                }
                radGridOtrosExamenesProcedimiento.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Guarda exámenes asociados al paciente
        /// </summary>
        /// <param name="idsExamenes"></param>
        /// <param name="marca"></param>
        private void GuardarExamenes(string idExamen, int marca)
        {
            try
            {
                //guiaPaciente = new sm_GuiaPaciente();
                //negocioPrograma = new ProgramaNegocio();
                //guiaPaciente.createdDate = DateTime.Now;
                //guiaPaciente.createdBy = "dcaballero";//TODO:Falta login
                //guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                //guiaPaciente.idGuia = Constantes.idgui
                //guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                //guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                //guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                //if (marca == 1)
                //{
                //    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOEXAMENES;
                //}
                //else
                //{
                //    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTRASAYUDAS;
                //}
                //guiaPaciente.valor1 = guia.idRiesgo;
                //guiaPaciente.valor2 = guia.cantidadRiesgo;
                //negocioPaciente = new PacienteNegocio();
                //int contador = 0;
                //int cantidad = Convert.ToInt16(guia.cantidadRiesgo);
                //while (contador < cantidad)
                //{
                //    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                //    contador++;
                //}
                //radGridExamenesAyudas.Rebind();
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show(ex.Message);
            }
        }

        /// <summary>
        /// Guarda otros aspectos asociados al paciente
        /// </summary>
        /// <param name="idsAspectos"></param>
        private void GuardarAspectos(string[] idsAspectos)
        {
            try
            {

                for (int i = 0; i < idsAspectos.Length; i++)
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    negocioPaciente = new PacienteNegocio();
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados   
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOASPECTOS;
                    guiaPaciente.txt1 = idsAspectos[i];
                    guiaPaciente.idGuia = Constantes.IDGUIAASPECTOS;
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    if (guiaPaciente.txt1.Equals("1"))
                        RadPageViewMedicamentos.Visible = true;
                    else
                        RadPageViewMedicionesBiometricas.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Guarda medicamentos asociados al paciente
        /// </summary>
        /// <param name="idsMedicamentos"></param>
        private void GuardarMedicamentos(string idMedicamento)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    negocioPrograma = new ProgramaNegocio();
                    guia = new sm_Guia();
                    guia.idTipoGuia = Constantes.TIPOGUIAMEDICAMENTO;
                    guia.idCodigoTipo = idMedicamento;
                    guia.descripcion = cboMedicamentos.Text;
                    guia.idPrograma = Convert.ToInt16(hdfPrograma.Value);
                    guia.idEstado = 1;//Inicialmente si se agrega siempre el estado es Habilitado                
                    guia.idRiesgo = 1;
                    guia.cantidadRiesgo = 0;
                    guia.createdBy = login;
                    guia.createdDate = DateTime.Now;
                    guia.version = Constantes.version;
                    negocioPrograma.GuardarGuia(guia);
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = guia.idGuia;
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICAMENTOS;
                    guiaPaciente.valor1 = guia.idRiesgo;
                    guiaPaciente.valor2 = guia.cantidadRiesgo;
                    negocioPaciente = new PacienteNegocio();
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    radGridMedicamentos.Rebind();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Consume web service para guardar otros diagnosticos asociados al paciente
        /// </summary>
        /// <param name="idOtroDiagnostico"></param>
        private void GuardarOtrosDiagnosticos(string idOtroDiagnostico)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    negocioPrograma = new ProgramaNegocio();
                    guia = new sm_Guia();
                    guia.idTipoGuia = Constantes.TIPOGUIAOTROSDIAGNOSTICOS;
                    guia.idCodigoTipo = idOtroDiagnostico;
                    guia.descripcion = cboOtrosDiagnosticos.Text;
                    guia.idPrograma = Convert.ToInt16(hdfPrograma.Value);
                    guia.idEstado = 1;//Inicialmente si se agrega siempre el estado es Habilitado                
                    guia.idRiesgo = 1;
                    guia.cantidadRiesgo = 0;
                    guia.createdBy = login;
                    guia.createdDate = DateTime.Now;
                    guia.version = Constantes.version;
                    negocioPrograma.GuardarGuia(guia);
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = guia.idGuia;
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTROSDIAGNOSTICOS;
                    guiaPaciente.valor1 = guia.idRiesgo;
                    guiaPaciente.valor2 = guia.cantidadRiesgo;
                    negocioPaciente = new PacienteNegocio();
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    radGridDiagnosticos.Rebind();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Guarda interconsultas asociadas al paciente
        /// </summary>
        /// <param name="idsInterconsultas"></param>
        /// <param name="marca"></param>
        private void GuardarInterconsultas(string idInterconsultas, int marca)
        {
            try
            {
                guiaPaciente = new sm_GuiaPaciente();
                negocioPrograma = new ProgramaNegocio();
                guia = new sm_Guia();
                guia.idTipoGuia = Constantes.TIPOGUIAOTRASINTERCONSULTAS;
                guia.idCodigoTipo = idInterconsultas;
                guia.descripcion = cboOtrasInterconsultas.Text;
                guia.idPrograma = Convert.ToInt16(hdfPrograma.Value);
                guia.idEstado = 1;//Inicialmente si se agrega siempre el estado es Habilitado                
                guia.idRiesgo = 1;
                guia.cantidadRiesgo = 0;
                guia.createdBy = login;
                guia.createdDate = DateTime.Now;
                guia.version = Constantes.version;
                negocioPrograma.GuardarGuia(guia);
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = guia.idGuia;
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                if (marca == 1)
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOINTERCONSULTAS;
                }
                else
                {
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTRASINTERCONSULTAS;
                }
                guiaPaciente.valor1 = guia.idRiesgo;
                guiaPaciente.valor2 = guia.cantidadRiesgo;
                negocioPaciente = new PacienteNegocio();
                int contador = 0;
                int cantidad = Convert.ToInt16(guia.cantidadRiesgo);
                while (contador <= cantidad)
                {
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    contador++;
                }
                radGridInterconsultas.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        /// <summary>
        /// Guarda otros examenes y procedimiento provenientes del web service colmedica asociadas al paciente
        /// </summary>
        /// <param name="idsInterconsultas"></param>
        /// <param name="marca"></param>
        private void GuardarOtrosExamenesProcedimientos(string idExamenProcedimiento, int marca)
        {
            try
            {
                guiaPaciente = new sm_GuiaPaciente();
                negocioPrograma = new ProgramaNegocio();
                guia = new sm_Guia();
                guia.idTipoGuia = Constantes.TIPOGUIAOTROSPROCEDIMIENTOS;
                guia.idCodigoTipo = idExamenProcedimiento;
                guia.descripcion = cboOtrosExamenesProcedimientos.Text;
                guia.idPrograma = Convert.ToInt16(hdfPrograma.Value);
                guia.idEstado = 1;//Inicialmente si se agrega siempre el estado es Habilitado                
                guia.idRiesgo = 1;
                guia.cantidadRiesgo = 0;
                guia.createdBy = login;
                guia.createdDate = DateTime.Now;
                guia.version = Constantes.version;
                negocioPrograma.GuardarGuia(guia);
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = guia.idGuia;
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOOTROSEXAMENESPROCEDIMIENTOS;
                guiaPaciente.valor1 = guia.idRiesgo;
                guiaPaciente.valor2 = guia.cantidadRiesgo;
                negocioPaciente = new PacienteNegocio();
                int contador = 0;
                int cantidad = Convert.ToInt16(guia.cantidadRiesgo);
                while (contador <= cantidad)
                {
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    contador++;
                }
                radGridOtrosExamenesProcedimiento.Rebind();
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        #endregion GuardarActualizarDatos

        #region Botones

        /// <summary>
        /// Guarda información si al paciente se le va a realizar monitoreo remoto o no
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarSiNo_Click(object sender, EventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                paciente = new sm_Paciente();
                paciente = negocioPaciente.ConsultarPaciente(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                if (cboSiNoMonotoreo.SelectedValue.Equals("1"))
                {
                    paciente.esMonitoreado = true;
                    negocioPaciente.ActualizarPaciente(paciente);
                    panelAspectos.Visible = true;
                }
                else if (cboSiNoMonotoreo.SelectedValue.Equals("2"))
                {
                    paciente.esMonitoreado = false;
                    negocioPaciente.ActualizarPaciente(paciente);
                    RadWizard1.ActiveStepIndex = 4;
                }
                else
                    throw new Exception("Por favor escoger una opción de la lista desplegable");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
        /// <summary>
        /// Evento que se dispara con la barra de navegación del wizzard registro paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadWizard1_NextButtonClick(object sender, WizardEventArgs e)
        {
            int idPrograma;
            try
            {
                if (hdfidTipoIdentificacion.Value != string.Empty && hdfNumeroIdentificacion.Value != string.Empty)
                {
                    idPrograma = ActualizarPaciente(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                    if (hdfEstadoPaciente.Value.Equals("4"))
                    {
                        CargarPaginaDatosPrograma(idPrograma);
                        //CargarDiagnosticosAutomaticos(idPrograma);
                        CargarInterconsultas(idPrograma);
                        CargarExamenes(idPrograma);
                        CargarExamenesOtrosProcedimientos(idPrograma);
                        CargarMedicamentos(idPrograma);
                        RadWizardStep2.Visible = true;
                        RadWizardStep3.Visible = true;
                        RadWizardStep4.Visible = true;
                        RadWizardStep5.Visible = true;
                        GuardarCitasMedicasWs();
                    }
                    else
                    {
                        CargarPaginaDatosPrograma(Convert.ToInt32(idPrograma));
                    }
                }
                else
                {
                    idPrograma = GuardarPaciente(sender, e);
                    if (idPrograma > 0)
                    {
                        CargarPaginaDatosPrograma(idPrograma);
                        CargarInterconsultas(idPrograma);
                        CargarExamenes(idPrograma);
                        CargarExamenesOtrosProcedimientos(idPrograma);
                        CargarMedicamentos(idPrograma);
                        RadWizardStep2.Visible = true;
                        RadWizardStep3.Visible = true;
                        RadWizardStep4.Visible = true;
                        RadWizardStep5.Visible = true;
                        GuardarCitasMedicasWs();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key"))
                {
                    MostrarMensaje("El usuario ya se encuentra registrado", true);
                    ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                }
                else
                {
                    MostrarMensaje(ex.Message, true);
                    ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                }
            }
        }


        /// <summary>
        /// Evento replica funcionalidad del boton next del wizard, para aplicar funcionalidad en ambas barras de navegación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadWizard1_NavigationBarButtonClick(object sender, WizardEventArgs e)
        {
            int idPrograma;
            try
            {
                if (hdfidTipoIdentificacion.Value != string.Empty && hdfNumeroIdentificacion.Value != string.Empty)
                {
                    idPrograma = ActualizarPaciente(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value);
                    if (hdfEstadoPaciente.Value.Equals("4"))
                    {
                        CargarPaginaDatosPrograma(idPrograma);
                        //CargarDiagnosticosAutomaticos(idPrograma);
                        CargarInterconsultas(idPrograma);
                        CargarExamenes(idPrograma);
                        CargarExamenesOtrosProcedimientos(idPrograma);
                        CargarMedicamentos(idPrograma);
                        RadWizardStep2.Visible = true;
                        RadWizardStep3.Visible = true;
                        RadWizardStep4.Visible = true;
                        RadWizardStep5.Visible = true;
                        GuardarCitasMedicasWs();
                    }
                    else
                    {
                        CargarPaginaDatosPrograma(Convert.ToInt32(idPrograma));
                    }  
                }
                else
                {
                    idPrograma = GuardarPaciente(sender, e);
                    if (idPrograma > 0)
                    {
                        CargarPaginaDatosPrograma(idPrograma);
                        CargarInterconsultas(idPrograma);
                        CargarExamenes(idPrograma);
                        CargarExamenesOtrosProcedimientos(idPrograma);
                        CargarMedicamentos(idPrograma);
                        RadWizardStep2.Visible = true;
                        RadWizardStep3.Visible = true;
                        RadWizardStep4.Visible = true;
                        RadWizardStep5.Visible = true;
                        GuardarCitasMedicasWs();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key"))
                {
                    MostrarMensaje("El usuario ya se encuentra registrado", true);
                    ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                }
                else
                {
                    MostrarMensaje(ex.Message, true);
                    ((RadWizard)sender).ActiveStepIndex = e.CurrentStepIndex;
                }
            }
        }
        /// <summary>
        /// Guarda diagnósticos seleccionados en combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarDiagnosticos_Click(object sender, EventArgs e)
        {
            int diagnosticosChequeados = cboDiagnosticos.CheckedItems.Count;
            String[] idsDiagnosticos = new String[diagnosticosChequeados];
            var collection = cboDiagnosticos.CheckedItems;
            int i = 0;
            foreach (var item in collection)
            {
                idsDiagnosticos[i] = item.Value.ToString();
                i++;
            }
            GuardarDiagnosticos(paciente, idsDiagnosticos, 1);
        }

        /// <summary>
        /// Guarda otros diagnósticos seleccionados en combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarOtrosDiagnosticos_Click(object sender, EventArgs e)
        {
            try
            {
                string idOtroDiagnostico = cboOtrosDiagnosticos.SelectedValue;
                GuardarOtrosDiagnosticos(idOtroDiagnostico);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Consume web service otros diagnosticos para asociarlos al paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarOtrasInterconsultas_Click(object sender, EventArgs e)
        {
            try
            {
                string idInterconsulta = cboOtrasInterconsultas.SelectedValue;
                GuardarInterconsultas(idInterconsulta, 2);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
        /// <summary>
        /// /Consume web service para otros examenes y procedimientos para asociarlos al paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarOtrosExamenesProcedimientos_Click(object sender, EventArgs e)
        {
            try
            {
                string idExamenProcedimiento = cboOtrosExamenesProcedimientos.SelectedValue;
                GuardarOtrosExamenesProcedimientos(idExamenProcedimiento, 2);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Guarda otras ayudas diagnósticas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarOtrasAyudas_Click(object sender, EventArgs e)
        {
            //int otrasChequeadas = cboOtrasAyudas.CheckedItems.Count;
            //String[] idsOtrasAyudas = new String[otrasChequeadas];
            //var collection = cboOtrasAyudas.CheckedItems;
            //int i = 0;
            //foreach (var item in collection)
            //{
            //    idsOtrasAyudas[i] = item.Value.ToString();
            //    i++;
            //}
            //GuardarExamenes(idsOtrasAyudas, 0);
        }

        /// <summary>
        /// Guarda datos para habilitar monitoreo remoto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarMonitoreo_Click(object sender, EventArgs e)
        {
            List<sm_GuiaPaciente> ListGuiaPaciente = new List<sm_GuiaPaciente>();
            negocioPaciente = new PacienteNegocio();
            try
            {
                if (cboSiNoMonotoreo.SelectedValue.Equals("1"))
                {
                    int aspectosChks = cboAspectos.CheckedItems.Count;
                    if (aspectosChks > 0)
                    {
                        String[] idsAspectos = new String[aspectosChks];
                        var collection = cboAspectos.CheckedItems;
                        int i = 0;
                        foreach (var item in collection)
                        {
                            idsAspectos[i] = item.Value.ToString();
                            i++;
                        }
                        ListGuiaPaciente = negocioPaciente.ConsultarAspectosMonitoreados(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOASPECTOS);
                        foreach (sm_GuiaPaciente gp in ListGuiaPaciente)
                        {
                            negocioPaciente.EliminarGuiaPaciente(gp.idGuiaPaciente);
                        }
                        GuardarAspectos(idsAspectos);
                    }
                    else
                    {
                        throw new Exception("Por favor escoger al menos una opción");
                    }
                }
                else
                    cboAspectos.Enabled = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Guarda medicamentos paramétricos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardarMedicamentosParametricos_Click(object sender, EventArgs e)
        {
            try
            {
                string idMedicamento = cboMedicamentos.SelectedValue;
                GuardarMedicamentos(idMedicamento);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        /// <summary>
        /// Botón finalizar wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RadWizard1_FinishButtonClick(object sender, WizardEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        /// <summary>
        /// Consume web service para cargar tabla medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboMedicamentos_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            CargarListadoMedicamentos(e.Text);
        }

        /// <summary>
        /// Consume web service para cargar lista de examenes y otros procedimientos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboOtrosExamenesProcedimientos_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            CargarOtrasAyudasDiagnosticas(e.Text);
        }

        #endregion Botones

        #region EventosControles

        /// <summary>
        /// Consume web service para cargar interconsultas desde combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboOtrasInterconsultas_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            CargarOtrasInterconsultas(e.Text);
        }

        /// <summary>
        /// Consulta base datos huella al momento de ingresar campo numero identificacion usando web service Colmedica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                sm_Ciudad ciudad = new sm_Ciudad();
                string tipoIdentificacion = cboTipoIdentificacion.SelectedItem.Text.Replace(".", "");
                string servicioURLPacientes = ConfigurationManager.AppSettings["URLSERVICIOPACIENTES"] + tipoIdentificacion + "/" + txtIdentificacion.Text + "/" + 1 + "?" + "TipoContrato=Odontologico";
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                var data = client.DownloadString(servicioURLPacientes);
                var jo = JObject.Parse(data);
                string Nombres = jo["Nombres"].ToString();
                string[] words = Nombres.Split(' ');
                txtPrimerNombre.Text = words[0];
                if (words.Length > 1)
                {
                    txtSegundoNombre.Text = words[1];
                }
                txtPrimerApellido.Text = jo["PrimerApellido"].ToString();
                txtSegundoApellido.Text = jo["SegundoApellido"].ToString();
                rdpFechaNacimiento.SelectedDate = Convert.ToDateTime(jo["FechaNacimiento"].ToString());
                string codigoCiudad = jo["CiudadResidencia"].ToString();
                bool esCruce = Convert.ToBoolean(jo["UsuarioCruce"]);
                string planMp = jo["NombrePlan"].ToString() + ' ' + jo["NombreAnexo"].ToString();
                bool tipoContrato = Convert.ToBoolean(jo["ContratoFamiliar"]);
                string nombreColectivo = jo["Colectivo"].ToString();
                string celular = jo["NumeroCelular"].ToString();
                string numeroFijo = jo["TelefonoResidencia"].ToString();
                string correo = jo["CorreoElectronico"].ToString();
                if (!codigoCiudad.Equals(string.Empty))
                    ciudad = negocioPaciente.ConsultarIdCiudad(codigoCiudad);
                if (ciudad != null)
                    cboCiudad.SelectedValue = ciudad.idCiudad.ToString();
                if (esCruce)
                    txtSegmento.Text = "Cruce";
                else
                    txtSegmento.Text = "Puro";
                txtPlanmp.Text = planMp;
                if (tipoContrato)
                    txtTipoContrato.Text = "Familiar";
                else
                    txtTipoContrato.Text = "Colectivo";
                txtNombreColectivo.Text = nombreColectivo;
                bool esCelular = !celular.Any(ch => ch < '0' || ch > '9');
                if (esCelular)
                    txtCelular.Text = celular;
                else
                    txtCelular.Text = "0000000000";
                bool digitsOnly = !numeroFijo.Any(ch => ch < '0' || ch > '9');
                if (digitsOnly)
                    txtTelefonoFijo.Text = numeroFijo;
                else
                    txtTelefonoFijo.Text = "0000000";
                txtCorreo.Text = correo;

            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }


        /// <summary>
        /// Inhabilita campos relacionados con parentesco al seleccionar no
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboNotificacionesResponsable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNotificacionesResponsable.SelectedValue.Equals("2"))
            {
                txtNombresParentesco.Enabled = false;
                cboParentesco.Enabled = false;
                txtCelularParentesco.Enabled = false;
                txtTelefonofijoparentesco.Enabled = false;
                txtCorreoParentesco.Enabled = false;
            }
            else if (cboNotificacionesResponsable.SelectedValue.Equals("1"))
            {
                txtNombresParentesco.Enabled = true;
                cboParentesco.Enabled = true;
                txtCelularParentesco.Enabled = true;
                txtTelefonofijoparentesco.Enabled = true;
                txtCorreoParentesco.Enabled = true;
            }
        }
        /// <summary>
        /// Consume web service para cargar otros diagnósticos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cboOtrosDiagnosticos_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            CargarOtrosDiagnosticos(e.Text);
        }

        #endregion EventosControles

        #region GrillaDiagnosticos

        protected void radGridDiagnosticos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            negocioPaciente = new PacienteNegocio();
            radGridDiagnosticos.DataSource = negocioPaciente.ConsultaBandejaDiagnostico(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTODIAGNOSTICO, Constantes.TIPOEVENTOOTROSDIAGNOSTICOS);
        }

        #endregion GrillaDiagnosticos

        #region GrillaOtrosDiagnosticos

        //protected void radGridOtrosDiagnosticos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    negocioPaciente = new PacienteNegocio();
        //    radGridOtrosDiagnosticos.DataSource = negocioPaciente.ConsultaBandejaDiagnostico(Convert.ToInt16(Request.QueryString["idTipoIdentificacion"]), Request.QueryString["NumeroIdentifacion"], Constantes.TIPOEVENTOOTROSDIAGNOSTICOS);
        //}

        #endregion GrillaOtrosDiagnosticos

        #region Grilla Tensión Sistólica
        protected void radGridTensionSistolica_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            negocioPaciente = new PacienteNegocio();
            radGridTensionSistolica.DataSource = negocioPaciente.ConsultaBandejaTomas(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOTENSION);
        }

        protected void radGridTensionSistolica_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                guiaPaciente = new sm_GuiaPaciente();
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = 2;//TODO: Falta especificar que pasa cuando son tomas ???
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOTENSION;
                guiaPaciente.unidad1 = "mm/Hg";
                if (valores["valor1"] != null)
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                else
                    throw new Exception("El campo tensión sistólica es requerido");
                if (valores["valor2"] != null)
                    guiaPaciente.valor2 = Convert.ToDecimal(valores["valor2"]);
                else
                    throw new Exception("El campo tensión diastólica es requerido");
                if (valores["fechaEvento"] != null)
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                else
                    throw new Exception("El campo fecha de toma es requerido");

                TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                guiaPaciente.fechaEvento = guiaPaciente.fechaEvento + ts;
                negocioPaciente = new PacienteNegocio();
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridTensionSistolica_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                guiaPaciente = new sm_GuiaPaciente();
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = 2;//TODO: Falta especificar que pasa cuando son tomas ???
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOTENSION;
                guiaPaciente.unidad1 = "mm/Hg";
                if (valores["valor1"] != null)
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                else
                    throw new Exception("El campo tensión sistólica es requerido");
                if (valores["valor2"] != null)
                    guiaPaciente.valor2 = Convert.ToDecimal(valores["valor2"]);
                else
                    throw new Exception("El campo tensión diastólica es requerido");
                if (valores["fechaEvento"] != null)
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                else
                    throw new Exception("El campo fecha de toma es requerido");

                TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
                guiaPaciente.fechaEvento = guiaPaciente.fechaEvento + ts;
                negocioPaciente = new PacienteNegocio();
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridTensionSistolica_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridTensionSistolica_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                TextBox nmcValor1 = (TextBox)item["valor1"].Controls[0];
                nmcValor1.Width = Unit.Pixel(50);
                TextBox nmcValor2 = (TextBox)item["valor2"].Controls[0];
                nmcValor2.Width = Unit.Pixel(50);
                RadDatePicker fechaToma = (RadDatePicker)item["FechaRegistro"].Controls[0];
                fechaToma.Width = Unit.Pixel(100);
            }
        }
        #endregion Grilla Tensión Sistólica

        #region Grilla Peso

        protected void radGridPeso_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            negocioPaciente = new PacienteNegocio();
            radGridPeso.DataSource = negocioPaciente.ConsultaBandejaTomas(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOPESO);
        }

        protected void radGridPeso_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridPeso_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                guiaPaciente = new sm_GuiaPaciente();
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = 2;//TODO: Falta especificar que pasa cuando son tomas ???
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOPESO;
                guiaPaciente.unidad1 = "Kg";//TODO: Falta especificar tomas
                if (valores["valor1"] != null)
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                else
                    throw new Exception("El campo valor es requerido");
                if (valores["fechaEvento"] != null)
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                else
                    throw new Exception("El campo fecha es requerido");
                negocioPaciente = new PacienteNegocio();
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        protected void radGridPeso_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                guiaPaciente = new sm_GuiaPaciente();
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = 2;//TODO: Falta especificar que pasa cuando son tomas ???
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOPESO;
                guiaPaciente.unidad1 = "Kg";//TODO: Falta especificar tomas
                if (valores["valor1"] != null)
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                else
                    throw new Exception("El campo valor es requerido");
                if (valores["fechaEvento"] != null)
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                else
                    throw new Exception("El campo fecha es requerido");
                negocioPaciente = new PacienteNegocio();
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        protected void radGridPeso_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadNumericTextBox nmcValor1 = (RadNumericTextBox)item["valor1"].Controls[0];
                nmcValor1.Width = Unit.Pixel(50);
                RadDatePicker fechaToma = (RadDatePicker)item["fechaEvento"].Controls[0];
                fechaToma.Width = Unit.Pixel(100);
            }
        }

        #endregion Grilla Peso

        #region GrillaTalla
        protected void radGridTalla_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            negocioPaciente = new PacienteNegocio();
            radGridTalla.DataSource = negocioPaciente.ConsultaBandejaTomas(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOTALLA);
        }

        protected void radGridTalla_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                guiaPaciente = new sm_GuiaPaciente();
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = 2;//TODO: Falta especificar que pasa cuando son tomas ???
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOTALLA;
                guiaPaciente.unidad1 = "mt";//TODO: Falta especificar tomas
                if (valores["valor1"] != null)
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                else
                    throw new Exception("El campo valor es requerido");
                if (valores["fechaEvento"] != null)
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                else
                    throw new Exception("El campo fecha es requerido");
                negocioPaciente = new PacienteNegocio();
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridTalla_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                item.ExtractValues(valores);
                guiaPaciente = new sm_GuiaPaciente();
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                guiaPaciente.idGuia = 2;//TODO: Falta especificar que pasa cuando son tomas ???
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOTALLA;
                guiaPaciente.unidad1 = "mt";//TODO: Falta especificar tomas
                if (valores["valor1"] != null)
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                else
                    throw new Exception("El campo valor es requerido");
                if (valores["fechaEvento"] != null)
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                else
                    throw new Exception("El campo fecha es requerido");
                negocioPaciente = new PacienteNegocio();
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridTalla_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridTalla_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadNumericTextBox nmcValor1 = (RadNumericTextBox)item["valor1"].Controls[0];
                nmcValor1.Width = Unit.Pixel(50);
                RadDatePicker fechaToma = (RadDatePicker)item["fechaEvento"].Controls[0];
                fechaToma.Width = Unit.Pixel(100);

            }
        }
        #endregion Grillatalla

        #region GrillaInterconsultas
        protected void radGridInterconsultas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridInterconsultas.DataSource = negocioPaciente.ConsultaBandejaInterconsultasAgrupadas(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOINTERCONSULTAS, Constantes.TIPOEVENTOOTRASINTERCONSULTAS);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void radGridInterconsultas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "DetalleInterConsultas":
                        {
                            string codigo = dataItem.GetDataKeyValue("Codigo").ToString();
                            negocioPaciente = new PacienteNegocio();
                            radGridInterconsultas.DataSource = negocioPaciente.ConsultarDetalleBandejaActividades(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOINTERCONSULTAS, Constantes.TIPOEVENTOOTRASINTERCONSULTAS, codigo);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridInterconsultas_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void radGridInterconsultas_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox combo = (RadComboBox)item.FindControl("cboEstadoGuia");
                RadComboBoxItem selectedItem = new RadComboBoxItem();
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                if (e.Item.OwnerTableView.Name.Equals("DetalleInterConsultas"))
                {
                    int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                    guiaPaciente = negocioPaciente.RetornarGuiaPaciente(idGuiaPaciente);
                    guiaPaciente.createdDate = guiaPaciente.createdDate;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.updatedDate = DateTime.Now;
                    guiaPaciente.updatedBy = login;
                    guiaPaciente.idEstado = Convert.ToInt32(combo.SelectedValue);
                    if (valores["fechaEvento"] == null && guiaPaciente.idEstado != 2)
                        throw new Exception("El campo fecha es requerido");
                    else if (valores["fechaEvento"] != null)
                        guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                    negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        string codigoGuiaPaciente = item.GetDataKeyValue("Codigo").ToString();
                        int resultado = negocioPaciente.borrarInterconsultasCodigo(codigoGuiaPaciente, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
                        GuardarInterconsultasModificadas(codigoGuiaPaciente, 1, Convert.ToInt32(valores["valor1"]));
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridInterconsultas_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                string codigoGuiaPaciente = item.GetDataKeyValue("Codigo").ToString();
                negocioPaciente.EliminarGuiasPacientePorCodigo(codigoGuiaPaciente, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
        #endregion GrillaInterconsultas

        #region GrillaExamenesOtrasAyudas

        protected void radGridExamenesAyudas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridExamenesAyudas.DataSource = negocioPaciente.ConsultaBandejaInterconsultasAgrupadas(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOEXAMENES, Constantes.TIPOEVENTOOTRASAYUDAS);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void radGridExamenesAyudas_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "DetalleExamenes":
                        {
                            string codigo = dataItem.GetDataKeyValue("Codigo").ToString();
                            negocioPaciente = new PacienteNegocio();
                            radGridExamenesAyudas.DataSource = negocioPaciente.ConsultarDetalleBandejaActividades(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOEXAMENES, Constantes.TIPOEVENTOOTRASAYUDAS, codigo);
                            break;
                        }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void radGridExamenesAyudas_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void radGridExamenesAyudas_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                if (e.Item.OwnerTableView.Name.Equals("DetalleExamenes"))
                {
                    int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                    guiaPaciente = negocioPaciente.RetornarGuiaPaciente(idGuiaPaciente);
                    guiaPaciente.createdDate = guiaPaciente.createdDate;
                    guiaPaciente.createdBy = login;//TODO:Falta login
                    guiaPaciente.updatedDate = DateTime.Now;
                    guiaPaciente.updatedBy = login;
                    guiaPaciente.idEstado = 1;
                    if (valores["fechaEvento"] != null)
                        guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                    else
                        throw new Exception("El campo fecha es requerido");
                    guiaPaciente.valor3 = Convert.ToDecimal(valores["valor3"]);
                    negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        string codigoGuiaPaciente = item.GetDataKeyValue("Codigo").ToString();
                        int resultado = negocioPaciente.borrarInterconsultasCodigo(codigoGuiaPaciente, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
                        GuardarExamenesModificados(codigoGuiaPaciente, 1, Convert.ToInt32(valores["valor1"]));
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        protected void radGridExamenesAyudas_InsertCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void radGridExamenesAyudas_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                string codigoGuiaPaciente = item.GetDataKeyValue("Codigo").ToString();
                negocioPaciente.EliminarGuiasPacientePorCodigo(codigoGuiaPaciente, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }
        #endregion GrillaExamenesOtrasAyudas

        #region GrillaMedicamentos
        protected void radGridMedicamentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridMedicamentos.DataSource = negocioPaciente.ConsultaMedicamentosPaciente(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOMEDICAMENTOS);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMedicamentos_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboForma = (RadComboBox)item.FindControl("cboForma");
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                using (TransactionScope scope = new TransactionScope())
                {
                    guiaPaciente = new sm_GuiaPaciente();
                    negocioPrograma = new ProgramaNegocio();
                    guia = new sm_Guia();
                    guia.idTipoGuia = Constantes.TIPOGUIAMEDICAMENTO;
                    guia.idCodigoTipo = valores["txt1"].ToString();
                    guia.descripcion = valores["txt1"].ToString();
                    guia.idPrograma = Convert.ToInt16(hdfPrograma.Value);
                    guia.idEstado = 1;//Inicialmente si se agrega siempre el estado es Habilitado                
                    guia.idRiesgo = 1;
                    guia.cantidadRiesgo = 0;
                    guia.createdBy = login;
                    guia.createdDate = DateTime.Now;
                    guia.version = Constantes.version;
                    negocioPrograma.GuardarGuia(guia);
                    guiaPaciente.createdDate = DateTime.Now;
                    guiaPaciente.createdBy = login;
                    guiaPaciente.idEstado = 1;//TODO:Falta tabla estados
                    guiaPaciente.idGuia = guia.idGuia;
                    guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                    guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                    guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                    guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICAMENTOS;
                    guiaPaciente.valor1 = guia.idRiesgo;
                    guiaPaciente.valor2 = guia.cantidadRiesgo;
                    guiaPaciente.fechaEvento = DateTime.Now;//TODO: Falta especificar que pasa cuando son tomas ???
                    if (valores["txt1"] != null)
                    {
                        guiaPaciente.txt1 = valores["txt1"].ToString();
                    }
                    if (cboForma.SelectedValue != null)
                    {
                        guiaPaciente.txt4 = cboForma.SelectedValue;
                    }
                    if (valores["valor4"] != null)
                    {
                        guiaPaciente.valor4 = Convert.ToDecimal(valores["valor4"].ToString());
                    }
                    if (cboPeriodicidad.SelectedValue != null)
                    {
                        guiaPaciente.txt5 = cboPeriodicidad.SelectedValue;
                    }
                    if (valores["observaciones"] != null)
                    {
                        guiaPaciente.observaciones = valores["observaciones"].ToString();
                    }
                    if (valores["fecha1"] != null)
                    {
                        guiaPaciente.fecha1 = Convert.ToDateTime(valores["fecha1"].ToString());
                    }
                    if (valores["fecha2"] != null)
                    {
                        guiaPaciente.fecha2 = Convert.ToDateTime(valores["fecha2"].ToString());
                    }
                    if (valores["txt3"] != null)
                    {
                        guiaPaciente.txt3 = valores["txt3"].ToString();
                    }
                    negocioPaciente = new PacienteNegocio();
                    negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
                    radGridMedicamentos.Rebind();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        protected void radGridMedicamentos_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboForma = (RadComboBox)item.FindControl("cboForma");
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                guiaPaciente = negocioPaciente.RetornarGuiaPaciente(idGuiaPaciente);
                guiaPaciente.createdDate = guiaPaciente.createdDate;
                guiaPaciente.createdBy = login;//TODO:Falta login
                guiaPaciente.updatedDate = DateTime.Now.ToUniversalTime();
                guiaPaciente.updatedBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.fechaEvento = DateTime.Now;//TODO: Falta especificar que pasa cuando son tomas ???
                if (valores["txt1"] != null)
                {
                    guiaPaciente.txt1 = valores["txt1"].ToString();
                }
                if (cboForma.SelectedValue != null)
                {
                    guiaPaciente.txt4 = cboForma.SelectedValue;
                }
                if (valores["valor4"] != null)
                {
                    guiaPaciente.valor4 = Convert.ToDecimal(valores["valor4"].ToString());
                }
                if (cboPeriodicidad.SelectedValue != null)
                {
                    guiaPaciente.txt5 = cboPeriodicidad.SelectedValue;
                }
                if (valores["observaciones"] != null)
                {
                    guiaPaciente.observaciones = valores["observaciones"].ToString();
                }
                if (valores["fecha1"] != null)
                {
                    guiaPaciente.fecha1 = Convert.ToDateTime(valores["fecha1"].ToString());
                }
                if (valores["fecha2"] != null)
                {
                    guiaPaciente.fecha2 = Convert.ToDateTime(valores["fecha2"].ToString());
                }
                if (valores["txt3"] != null)
                {
                    guiaPaciente.txt3 = valores["txt3"].ToString();
                }
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        protected void radGridMedicamentos_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMedicamentos_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        #endregion GrillaMedicamentos

        #region GrillaOtrosExamenesyProcedimientos
        protected void radGridOtrosExamenesProcedimiento_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridOtrosExamenesProcedimiento.DataSource = negocioPaciente.ConsultaBandejaInterconsultasAgrupadas(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOEXAMENESOTROSPROCEDIMIENTOS, Constantes.TIPOEVENTOOTROSEXAMENESPROCEDIMIENTOS);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridOtrosExamenesProcedimiento_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                if (e.Item.OwnerTableView.Name.Equals("DetalleOtrosExamenesProcedimientos"))
                {
                    int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("id"));
                    guiaPaciente = negocioPaciente.RetornarGuiaPaciente(idGuiaPaciente);
                    guiaPaciente.createdDate = guiaPaciente.createdDate;
                    guiaPaciente.createdBy = login;
                    guiaPaciente.updatedDate = DateTime.Now;
                    guiaPaciente.updatedBy = login;
                    guiaPaciente.idEstado = 1;
                    if (valores["fechaEvento"] != null)
                        guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                    else
                        throw new Exception("El campo fecha es requerido");
                    RadTextBox txtObservaciones = (RadTextBox)e.Item.FindControl("txtObservaciones");
                    guiaPaciente.observaciones = txtObservaciones.Text;
                    negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        string codigoGuiaPaciente = item.GetDataKeyValue("Codigo").ToString();
                        int resultado = negocioPaciente.borrarInterconsultasCodigo(codigoGuiaPaciente, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
                        GuardarExamenesyOtrosProcedimientoModificados(codigoGuiaPaciente, 1, Convert.ToInt32(valores["valor1"]));
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SaludMovil.Transversales.SaludMovilExceptionBD(ex);
            }
        }

        protected void radGridOtrosExamenesProcedimiento_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "DetalleOtrosExamenesProcedimientos":
                        {
                            string codigo = dataItem.GetDataKeyValue("Codigo").ToString();
                            negocioPaciente = new PacienteNegocio();
                            radGridOtrosExamenesProcedimiento.DataSource = negocioPaciente.ConsultarDetalleBandejaActividades(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOEXAMENESOTROSPROCEDIMIENTOS, Constantes.TIPOEVENTOOTRASINTERCONSULTAS, codigo);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                RadNotificationMensajes.Show("El sistema tiene problemas de conexión para cargar la información solicitada");
            }
        }

        protected void radGridOtrosExamenesProcedimiento_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                string codigoGuiaPaciente = item.GetDataKeyValue("Codigo").ToString();
                negocioPaciente.EliminarGuiasPacientePorCodigo(codigoGuiaPaciente, hdfidTipoIdentificacion.Value, hdfNumeroIdentificacion.Value);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        #endregion GrillaOtrosExamenesyProcedimientos

        #region GrillaMedicionesBiometricas

        protected void radGridMedicionesBiometricas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridMedicionesBiometricas.DataSource = negocioPaciente.ConsultaBandejaMediciones(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.IDGUIAMEDICIONES, "limPresion");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMedicionesBiometricas_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "limPresion";
                if (valores["fechaEvento"] != null)
                {
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"]);
                }
                if (valores["valor1"] != null)
                {
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                }
                if (valores["valor2"] != null)
                {
                    guiaPaciente.valor2 = Convert.ToDecimal(valores["valor2"]);
                }
                if (valores["valor3"] != null)
                {
                    guiaPaciente.valor3 = Convert.ToDecimal(valores["valor3"]);
                }
                if (valores["valor4"] != null)
                {
                    guiaPaciente.valor4 = Convert.ToDecimal(valores["valor4"]);
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMedicionesBiometricas_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "limPresion";
                if (valores["fechaEvento"] != null)
                {
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"]);
                }
                if (valores["valor1"] != null)
                {
                    guiaPaciente.valor1 = Convert.ToDecimal(valores["valor1"]);
                }
                if (valores["valor2"] != null)
                {
                    guiaPaciente.valor2 = Convert.ToDecimal(valores["valor2"]);
                }
                if (valores["valor3"] != null)
                {
                    guiaPaciente.valor3 = Convert.ToDecimal(valores["valor3"]);
                }
                if (valores["valor4"] != null)
                {
                    guiaPaciente.valor4 = Convert.ToDecimal(valores["valor4"]);
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMedicionesBiometricas_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMedicionesBiometricas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadNumericTextBox valor1 = (RadNumericTextBox)item["valor1"].Controls[0];
                valor1.Width = Unit.Pixel(50);
                RadNumericTextBox valor2 = (RadNumericTextBox)item["valor2"].Controls[0];
                valor2.Width = Unit.Pixel(50);
                RadNumericTextBox valor3 = (RadNumericTextBox)item["valor3"].Controls[0];
                valor3.Width = Unit.Pixel(50);
                RadNumericTextBox valor4 = (RadNumericTextBox)item["valor4"].Controls[0];
                valor4.Width = Unit.Pixel(50);
                RadDatePicker fecha = (RadDatePicker)item["fechaEvento"].Controls[0];
                fecha.Width = Unit.Pixel(100);
            }
        }

        #endregion MedicionesBiometricas

        #region ControlesPaciente
        protected void radGridControlEnfermeria_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridControlEnfermeria.DataSource = negocioPaciente.ConsultaBandejaControles(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.TIPOEVENTOCONTROLES);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridControlEnfermeria_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                RadComboBox cboEstado = (RadComboBox)item.FindControl("cboEstado");
                RadComboBox cboCausal = (RadComboBox)item.FindControl("cboCausal");
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                if (valores["unidad4"] == null)
                    throw new Exception("El campo próxima fecha control es requerido");
                guiaPaciente = new sm_GuiaPaciente()
                {
                    idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value),
                    numeroIdentificacion = hdfNumeroIdentificacion.Value,
                    createdBy = login,
                    createdDate = DateTime.Now,
                    idEstado = 1,
                    unidad4 = valores["unidad4"].ToString(),
                    unidad3 = cboPeriodicidad.SelectedValue,
                    txt4 = cboEstado.SelectedValue,
                    idGuia = Constantes.IDGUIACONTROLES,
                    idOrigen = Constantes.ORIGENTOMA,
                    tipoEvento = Constantes.TIPOEVENTOCONTROLES,
                    fecha1 = Convert.ToDateTime(valores["unidad4"].ToString())
                };
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridControlEnfermeria_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                RadComboBox cboEstado = (RadComboBox)item.FindControl("cboEstado");
                RadComboBox cboCausal = (RadComboBox)item.FindControl("cboCausal");
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                if (valores["fechaEvento"] != null)
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"].ToString());
                else
                    throw new Exception("El campo fecha es requerido");
                if (valores["unidad4"] != null)
                {
                    guiaPaciente.unidad4 = valores["unidad4"].ToString();
                    guiaPaciente.fecha1 = Convert.ToDateTime(valores["unidad4"].ToString());
                }
                else
                    throw new Exception("El campo próxima fecha control es requerido");
                if (cboPeriodicidad.SelectedValue != null)
                    guiaPaciente.unidad3 = cboPeriodicidad.SelectedValue;
                if (cboEstado.SelectedValue != null)
                    guiaPaciente.txt4 = cboEstado.SelectedValue;
                if (cboCausal.SelectedValue != null)
                    guiaPaciente.unidad5 = cboCausal.SelectedValue;
                RadTextBox txtObservaciones = (RadTextBox)e.Item.FindControl("txtObservaciones");
                if (txtObservaciones.Text != null)
                    guiaPaciente.observaciones = txtObservaciones.Text;
                else if ((guiaPaciente.txt4.Equals("2") || guiaPaciente.txt4.Equals("3")) && guiaPaciente.observaciones.Equals(string.Empty))
                    throw new Exception("El campo observaciones es requerido");
                guiaPaciente.idGuia = Constantes.IDGUIACONTROLES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOCONTROLES;
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridControlEnfermeria_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridControlEnfermeria_ItemDataBound(object sender, GridItemEventArgs e)
        {
            string observaciones;
            if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
            {
                (e.Item as GridEditableItem)["fechaEvento"].Visible = false;
                (e.Item as GridEditableItem)["Causal"].Visible = false;
                (e.Item as GridEditableItem)["fechaEvento"].Visible = false;
                (e.Item as GridEditableItem)["Estado"].Visible = false;
                RadTextBox txtObservacionesInsert = (RadTextBox)e.Item.FindControl("txtObservaciones");
                txtObservacionesInsert.Visible = false;
            }
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                if (item.DataSetIndex != -1)
                    observaciones = item.GetDataKeyValue("observaciones").ToString();
                else
                    observaciones = string.Empty;
                RadTextBox txtObservaciones = item.FindControl("txtObservaciones") as RadTextBox;
                txtObservaciones.Width = Unit.Pixel(150);
                if (txtObservaciones != null)
                    txtObservaciones.Text = observaciones;
            }
        }
        #endregion ControlesPaciente

        #region GrillaMonitoreoGlucometria
        protected void radGridMonitoreoGlucometria_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridMonitoreoGlucometria.DataSource = negocioPaciente.ConsultaBandejaMediciones(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.IDGUIAMEDICIONES, "monGlucosa");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMonitoreoGlucometria_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "monGlucosa";
                guiaPaciente.fechaEvento = DateTime.Now;//TODO: Falta especificar que pasa cuando son tomas ???
                if (cboPeriodicidad.SelectedValue != null)
                {
                    guiaPaciente.unidad3 = cboPeriodicidad.SelectedValue;
                }
                if (valores["unidad2"] != null)
                {
                    guiaPaciente.unidad2 = valores["unidad2"].ToString();
                }
                if (valores["fecha1"] != null)
                {
                    guiaPaciente.fecha1 = Convert.ToDateTime(valores["fecha1"].ToString());
                    DateTime fechaInicio = Convert.ToDateTime(valores["fecha1"].ToString());
                    guiaPaciente.unidad4 = fechaInicio.ToString("dd/MM/yyyy");
                }
                if (valores["fecha2"] != null)
                {
                    guiaPaciente.fecha2 = Convert.ToDateTime(valores["fecha2"].ToString());
                    DateTime fechaFin = Convert.ToDateTime(valores["fecha2"].ToString());
                    guiaPaciente.unidad5 = fechaFin.ToString("dd/MM/yyyy");
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMonitoreoGlucometria_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "monGlucosa";
                guiaPaciente.fechaEvento = DateTime.Now;//
                if (cboPeriodicidad.SelectedValue != null)
                {
                    guiaPaciente.unidad3 = cboPeriodicidad.SelectedValue;
                }
                if (valores["unidad2"] != null)
                {
                    guiaPaciente.unidad2 = valores["unidad2"].ToString();
                }
                if (valores["fecha1"] != null)
                {
                    guiaPaciente.fecha1 = Convert.ToDateTime(valores["fecha1"].ToString());
                    DateTime fechaInicio = Convert.ToDateTime(valores["fecha1"].ToString());
                    guiaPaciente.unidad4 = fechaInicio.ToString("dd/MM/yyyy");
                }
                if (valores["fecha2"] != null)
                {
                    guiaPaciente.fecha2 = Convert.ToDateTime(valores["fecha2"].ToString());
                    DateTime fechaFinal = Convert.ToDateTime(valores["fecha2"].ToString());
                    guiaPaciente.unidad5 = fechaFinal.ToString("dd/MM/yyyy");
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMonitoreoGlucometria_DeleteCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void radGridMonitoreoGlucometria_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                TextBox nmcValor1 = (TextBox)item["unidad2"].Controls[0];
                nmcValor1.Width = Unit.Pixel(50);
                RadDatePicker unidad4 = (RadDatePicker)item["fecha1"].Controls[0];
                unidad4.Width = Unit.Pixel(100);
                RadDatePicker unidad5 = (RadDatePicker)item["fecha2"].Controls[0];
                unidad5.Width = Unit.Pixel(100);
            }
        }
        #endregion GrillaMonitoreoGlucometria

        #region GrillaLimitesGlucometria
        protected void radGridLimitesGluomtria_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridLimitesGluomtria.DataSource = negocioPaciente.ConsultaBandejaMediciones(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.IDGUIAMEDICIONES, "limGlucosa");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridLimitesGluomtria_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "limGlucosa";
                if (valores["fechaEvento"] != null)
                {
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"]);
                }
                if (valores["valor5"] != null)
                {
                    guiaPaciente.valor5 = Convert.ToDecimal(valores["valor5"]);
                }
                if (valores["unidad1"] != null)
                {
                    guiaPaciente.unidad1 = valores["unidad1"].ToString();
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridLimitesGluomtria_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "limGlucosa";
                if (valores["fechaEvento"] != null)
                {
                    guiaPaciente.fechaEvento = Convert.ToDateTime(valores["fechaEvento"]);
                }
                if (valores["valor5"] != null)
                {
                    guiaPaciente.valor5 = Convert.ToDecimal(valores["valor5"]);
                }
                if (valores["unidad1"] != null)
                {
                    guiaPaciente.unidad1 = valores["unidad1"].ToString();
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridLimitesGluomtria_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridLimitesGluomtria_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadNumericTextBox valor5 = (RadNumericTextBox)item["valor5"].Controls[0];
                valor5.Width = Unit.Pixel(50);
                TextBox unidad1 = (TextBox)item["unidad1"].Controls[0];
                unidad1.Width = Unit.Pixel(50);
                RadDatePicker fecha = (RadDatePicker)item["fechaEvento"].Controls[0];
                fecha.Width = Unit.Pixel(100);
            }
        }
        #endregion GrillaLimitesGlucometria

        #region GrillaMonitoreoPresion
        protected void radGridMonitoreoPresion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                radGridMonitoreoPresion.DataSource = negocioPaciente.ConsultaBandejaMediciones(Convert.ToInt16(hdfidTipoIdentificacion.Value), hdfNumeroIdentificacion.Value, Constantes.IDGUIAMEDICIONES, "monPresion");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMonitoreoPresion_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "monPresion";
                guiaPaciente.fechaEvento = DateTime.Now;//TODO: Falta especificar que pasa cuando son tomas ???
                if (cboPeriodicidad.SelectedValue != null)
                {
                    guiaPaciente.unidad3 = cboPeriodicidad.SelectedValue;
                }
                if (valores["unidad2"] != null)
                {
                    guiaPaciente.unidad2 = valores["unidad2"].ToString();
                }
                if (valores["fecha1"] != null)
                {
                    guiaPaciente.fecha1 = Convert.ToDateTime(valores["fecha1"].ToString());
                    DateTime fechaInicio = Convert.ToDateTime(valores["fecha1"].ToString());
                    guiaPaciente.unidad4 = fechaInicio.ToString("dd/MM/yyyy");
                }
                if (valores["fecha2"] != null)
                {
                    guiaPaciente.fecha2 = Convert.ToDateTime(valores["fecha2"].ToString());
                    DateTime fechaFin = Convert.ToDateTime(valores["fecha2"].ToString());
                    guiaPaciente.unidad5 = fechaFin.ToString("dd/MM/yyyy");
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.GuardarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMonitoreoPresion_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                RadComboBox cboPeriodicidad = (RadComboBox)item.FindControl("cboPeriodicidad");
                guiaPaciente = new sm_GuiaPaciente();
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                item.ExtractValues(valores);
                guiaPaciente.idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                guiaPaciente.idTipoIdentificacion = Convert.ToInt16(hdfidTipoIdentificacion.Value);
                guiaPaciente.numeroIdentificacion = hdfNumeroIdentificacion.Value;
                guiaPaciente.createdDate = DateTime.Now;
                guiaPaciente.createdBy = login;
                guiaPaciente.idEstado = 1;
                guiaPaciente.observaciones = "monPresion";
                guiaPaciente.fechaEvento = DateTime.Now;//TODO: Falta especificar que pasa cuando son tomas ???
                if (cboPeriodicidad.SelectedValue != null)
                {
                    guiaPaciente.unidad3 = cboPeriodicidad.SelectedValue;
                }
                if (valores["unidad2"] != null)
                {
                    guiaPaciente.unidad2 = valores["unidad2"].ToString();
                }
                if (valores["fecha1"] != null)
                {
                    guiaPaciente.fecha1 = Convert.ToDateTime(valores["fecha1"].ToString());
                }
                if (valores["fecha2"] != null)
                {
                    guiaPaciente.fecha2 = Convert.ToDateTime(valores["fecha2"].ToString());
                }
                guiaPaciente.idGuia = Constantes.IDGUIAMEDICIONES;
                guiaPaciente.idOrigen = Constantes.ORIGENTOMA;
                guiaPaciente.tipoEvento = Constantes.TIPOEVENTOMEDICIONESBIOMETRICAS;
                negocioPaciente.ActualizarGuiaPaciente(guiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMonitoreoPresion_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                negocioPaciente = new PacienteNegocio();
                Hashtable valores = new Hashtable();
                GridEditableItem item = (GridEditableItem)e.Item;
                int idGuiaPaciente = Convert.ToInt32(item.GetDataKeyValue("idGuiaPaciente"));
                negocioPaciente.EliminarGuiaPaciente(idGuiaPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        protected void radGridMonitoreoPresion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                TextBox nmcValor1 = (TextBox)item["unidad2"].Controls[0];
                nmcValor1.Width = Unit.Pixel(50);
                RadDatePicker unidad4 = (RadDatePicker)item["fecha1"].Controls[0];
                unidad4.Width = Unit.Pixel(100);
                RadDatePicker unidad5 = (RadDatePicker)item["fecha2"].Controls[0];
                unidad5.Width = Unit.Pixel(100);
            }
        }
        #endregion GrillaMonitoreoPresion

    }
}