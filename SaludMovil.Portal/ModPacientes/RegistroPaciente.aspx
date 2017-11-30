<%@ Page Title="Administración Pacientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroPaciente.aspx.cs" Inherits="SaludMovil.Portal.ModPacientes.RegistroPaciente" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="RegistroPaciente" ContentPlaceHolderID="MainContent">
    <h6 class="text-danger" id="hRuta">
        <asp:Label ID="lblLeyenda" runat="server"></asp:Label>
    </h6>
    <script src="../Scripts/SaludMovil.js" type="text/javascript"></script>
    <div class="container-fluid">
        <asp:HiddenField ID="hdfidTipoIdentificacion" runat="server" />
        <asp:HiddenField ID="hdfNumeroIdentificacion" runat="server" />
        <asp:HiddenField ID="hdfPrograma" runat="server" />
        <asp:HiddenField ID="hdfRiesgo" runat="server" />
        <asp:HiddenField ID="hdfesMonitoreado" runat="server" />
        <asp:HiddenField ID="hdfEstadoPaciente" runat="server" />
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="radGridTensionSistolica">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridTensionSistolica" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="radGridPeso">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridPeso" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="radGridTalla">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridTalla" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="radGridControlEnfermeria">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridControlEnfermeria" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="radGridInterconsultas">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridInterconsultas" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnGuardarMonitoreo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadMultiPage2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
        <div class="form-group row">
            <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
                EnableShadow="true" Position="TopCenter" Animation="FlyIn" Title="Sistema de notificaciones" VisibleTitlebar="false">
            </telerik:RadNotification>
            <telerik:RadWizard runat="server" ID="RadWizard1" Height="100%" DisplayCancelButton="true" Culture="Es-es"
                OnNextButtonClick="RadWizard1_NextButtonClick" OnNavigationBarButtonClick="RadWizard1_NavigationBarButtonClick" Skin="Bootstrap"
                OnFinishButtonClick="RadWizard1_FinishButtonClick" Localization-Previous="Anterior" Localization-Finish="Finalizar" Localization-Next="Siguiente"
                Localization-Cancel="Cancelar">
                <WizardSteps>
                    <telerik:RadWizardStep ID="RadWizardStep1" Title="Datos generales" StepType="Start" runat="server" CausesValidation="true">
                        <div class="container-fluid" style="border: 1px solid #888; padding-bottom: 15px; box-shadow: 0px 2px 5px #ccc">
                            <h4 class="encabezadoWizard">Datos personales</h4>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblTipoIdentificacion" Text="Tipo de identificación" runat="server" Width="500px" CssClass="lblTexto">
                                    </asp:Label>
                                    <asp:DropDownList ID="cboTipoIdentificacion" runat="server" Width="250px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblNumeroIdentificacion" Text="Número de identificación" runat="server" Width="500px" CssClass="lblTexto">
                                    </asp:Label>
                                    <asp:TextBox runat="server" ID="txtIdentificacion" Width="250px" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtIdentificacion_TextChanged"
                                        onKeyPress="javascript:fixedlength(this,10);" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblPrimerNombre" Text="Primer nombre" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtPrimerNombre" Width="250px" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSegundoNombre" Text="Segundo nombre" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtSegundoNombre" Width="250px" CssClass="form-control" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblPrimerApellido" Text="Primer apellido" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtPrimerApellido" Width="250px" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSegundoApellido" Text="Segundo apellido" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtSegundoApellido" Width="250px" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFechaNacimiento" Text="Fecha de nacimiento" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <telerik:RadDatePicker ID="rdpFechaNacimiento" runat="server" Width="250px" SkipMinMaxDateValidationOnServer="true" MinDate="01/01/1900">
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblCiudad" Text="Ciudad" runat="server" Width="150px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboCiudad" runat="server" Width="250px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblSegmento" Text="Segmento" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtSegmento" Width="250px" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblPlanMp" Text="Plan mp" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtPlanmp" Width="250px" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTipoContrato" Text="Tipo de contrato" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtTipoContrato" Width="250px" CssClass="form-control" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblNombreColectivo" Text="Nombre colectivo" runat="server" Width="250px"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtNombreColectivo" Width="250px" CssClass="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                            <h4 class="encabezadoWizard">Datos de contacto</h4>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblCelular" Text="Celular" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCelular" Width="340px" CssClass="form-control" onKeyPress="javascript:fixedlength(this,9);"
                                        onblur="javascript:AlertaMinimaTexto(this,9);" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblTelefonoFijo" Text="Teléfono fijo" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtTelefonoFijo" Width="340px" CssClass="form-control" onKeyPress="javascript:fixedlength(this,6);"
                                        onblur="javascript:AlertaMinimaTexto(this,6);" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblCorreoElectronico" Text="Correo electrónico" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCorreo" Width="340px" CssClass="form-control" onKeyPress="javascript:validarCorreo(this);" />
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                            <h4 class="encabezadoWizard">Prestador y médico tratante</h4>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblInstitucionProfesional" Text="Institución / Profesional" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboCentroSalud" runat="server" Width="340px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblMedicoTratante" Text="Médico tratante" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <telerik:RadComboBox ID="cboMedicoTratante" runat="server" Filter="Contains" DropDownAutoWidth="Enabled"
                                        Width="340px">
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblEspecialidad" Text="Especialidad" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboEspecialidad" runat="server" Width="340px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                            <h4 class="encabezadoWizard">Responsable</h4>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblNotificacionesotroresponsable" Text="Notificaciones otro responsable" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboNotificacionesResponsable" runat="server" Width="340px" CssClass="form-control" AppendDataBoundItems="true"
                                        OnSelectedIndexChanged="cboNotificacionesResponsable_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Seleccione...">
                                        </asp:ListItem>
                                        <asp:ListItem Value="1" Text="Si">
                                        </asp:ListItem>
                                        <asp:ListItem Value="2" Text="No">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblNombresApellidos" Text="Nombres/Apellidos" runat="server" Width="200px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtNombresParentesco" Width="340px" CssClass="form-control" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblParentesco" Text="Parentesco" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboParentesco" runat="server" Width="340px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblCelularParentesco" Text="Celular" runat="server" Width="200px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCelularParentesco" Width="340px" CssClass="form-control" onKeyPress="javascript:fixedlength(this,9);"
                                        onblur="javascript:AlertaMinimaTexto(this,9);" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblTelefonofijoparentesco" Text="Teléfono fijo" runat="server" Width="200px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtTelefonofijoparentesco" Width="340px" CssClass="form-control" onKeyPress="javascript:fixedlength(this,6);"
                                        onblur="javascript:AlertaMinimaTexto(this,6);" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblCorreoParentesco" Text="Correo" runat="server" Width="200px" CssClass="lblTexto"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtCorreoParentesco" Width="340px" CssClass="form-control" onKeyPress="javascript:validarCorreo(this);" />
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                            <h4 class="encabezadoWizard">Asignación de programa</h4>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblPrograma" Text="Programa" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboPrograma" runat="server" Width="250px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblFechaIngreso" Text="Fecha de ingreso" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <telerik:RadDatePicker ID="rdpFechaIngreso" runat="server" CssClass="form-control" Width="250px">
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblTipoIngreso" Text="Tipo de ingreso" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboTipoIngreso" runat="server" Width="250px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblMedioAtencion" Text="Medio de atención" runat="server" Width="250px" CssClass="lblTexto"></asp:Label>
                                    <asp:DropDownList ID="cboMedioAtencion" runat="server" Width="250px" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                            <h4 class="encabezadoWizard">Clasificación del riesgo</h4>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblRiesgo" Text="Riesgo" runat="server" Width="500px" CssClass="lblTexto">
                                    </asp:Label>
                                    <asp:DropDownList ID="cboRiesgo" runat="server" Width="100%" CssClass="form-control" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Label1" Text="Estado" runat="server" Width="500px" CssClass="lblTexto">
                                    </asp:Label>
                                    <asp:DropDownList ID="cboEstadoPaciente" runat="server" Width="100%" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                        <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Suspendido"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Inactivo"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Identificado"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                     <asp:Label ID="Label3" Text="Fecha actualización" runat="server" Width="500px" CssClass="lblTexto">
                                    </asp:Label>
                                     <telerik:RadDatePicker ID="rdpFechaActualizacionPaciente" runat="server" Width="100%" Enabled="false" 
                                         SkipMinMaxDateValidationOnServer="true" MinDate="01/01/1900" CssClass="form-control">
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                        </div>
                    </telerik:RadWizardStep>
                    <telerik:RadWizardStep ID="RadWizardStep2" Title="Datos del Programa" DisplayCancelButton="false" runat="server">
                        <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                            <h4 class="encabezadoWizard">Registro de diagnósticos</h4>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblDiagnosticos" Text="Diagnósticos" runat="server" Width="500px" CssClass="lblTexto">
                                    </asp:Label>
                                    <telerik:RadComboBox ID="cboDiagnosticos" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                        Localization-CheckAllString="Seleccionar todos" CheckedItemsTexts="FitInInput" Localization-AllItemsCheckedString="Todos Seleccionados"
                                        Filter="Contains" Width="100%" EmptyMessage="Buscar diagnósticos">
                                        <CollapseAnimation Type="InOutElastic" />
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label17" Text="" runat="server" Width="500px">
                                    </asp:Label>
                                    <asp:Button ID="btnGuardarDiagnosticos" runat="server" CssClass="btn btn-md btn-block btn-primary" Text="Guardar" Width="80px" Height="31px" OnClick="btnGuardarDiagnosticos_Click" CausesValidation="false" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblOtrosDiagnosticos" Text="Otros diagnósticos" runat="server" Width="500px" CssClass="lblTexto">
                                    </asp:Label>
                                    <telerik:RadComboBox
                                        ID="cboOtrosDiagnosticos"
                                        runat="server"
                                        Localization-CheckAllString="Seleccionar todos"
                                        CheckedItemsTexts="FitInInput"
                                        Localization-AllItemsCheckedString="Todos Seleccionados"
                                        EnableLoadOnDemand="true"
                                        OnItemsRequested="cboOtrosDiagnosticos_ItemsRequested"
                                        DropDownWidth="700px"
                                        Filter="Contains"
                                        Width="100%"
                                        EmptyMessage="Buscar otros diagnósticos">
                                        <CollapseAnimation Type="InOutElastic" />
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label16" Text="" runat="server" Width="500px">
                                    </asp:Label>
                                    <asp:Button runat="server" ID="btnGuardarOtrosDiagnosticos" CssClass="btn btn-md btn-block btn-primary" Text="Guardar" Width="80px" Height="31px" OnClick="btnGuardarOtrosDiagnosticos_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="encabezadoWizard">Bandeja diagnósticos</h4>
                                    <telerik:RadGrid ID="radGridDiagnosticos"
                                        runat="server"
                                        Width="100%"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        OnNeedDataSource="radGridDiagnosticos_NeedDataSource"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                        </ClientSettings>
                                        <MasterTableView AllowMultiColumnSorting="True" NoMasterRecordsText="No hay registros">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" HeaderText="ID" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="id" FilterControlWidth="30px" Visible="false" />
                                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="100px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="100px" />
                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="200px" />
                                                <telerik:GridDateTimeColumn DataField="FechaRegistro" HeaderText="FechaRegistro" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px" FooterStyle-Width="200px" AutoPostBackOnFilter="true" UniqueName="FechaRegistro" FilterControlWidth="100px" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <h4 class="encabezadoWizard">Exámenes físicos</h4>
                            <div class="row">
                                <div class="col-md-4">
                                    <h4>Tensión arterial</h4>
                                    <telerik:RadGrid ID="radGridTensionSistolica"
                                        runat="server"
                                        GridLines="None"
                                        OnNeedDataSource="radGridTensionSistolica_NeedDataSource"
                                        OnInsertCommand="radGridTensionSistolica_InsertCommand"
                                        OnUpdateCommand="radGridTensionSistolica_UpdateCommand"
                                        OnDeleteCommand="radGridTensionSistolica_DeleteCommand"
                                        OnItemDataBound="radGridTensionSistolica_ItemDataBound"
                                        ShowStatusBar="true"
                                        Width="100%"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        PageSize="10"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                        </ClientSettings>
                                        <MasterTableView
                                            CommandItemDisplay="Top"
                                            ShowFooter="false"
                                            EditMode="InPlace"
                                            DataKeyNames="id"
                                            CommandItemSettings-CancelChangesText="Cancelar"
                                            CommandItemSettings-AddNewRecordText="Agregar"
                                            CommandItemSettings-RefreshText="Actualizar"
                                            NoMasterRecordsText="No hay registros para mostrar."
                                            EditFormSettings-EditColumn-UpdateText="Actualizar">
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                    HeaderStyle-Width="20px" FilterControlWidth="20px" ItemStyle-Width="20px" UpdateText="Actualizar" CancelText="Cancelar">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn HeaderText="Sistólica (mmHg)" DataField="valor1" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="100px" ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="valor1" FilterControlWidth="20px"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Diastólica (mmHg)" DataField="valor2" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="valor2" FilterControlWidth="20px"></telerik:GridBoundColumn>
                                                <telerik:GridDateTimeColumn HeaderText="Fecha Toma" DataField="fechaEvento" AutoPostBackOnFilter="true"
                                                    EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}"
                                                    EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small"
                                                    HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px"
                                                    UniqueName="FechaRegistro" FilterControlWidth="80px" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px" FilterControlWidth="20px"
                                                    Text="Eliminar" UniqueName="DeleteColumn"
                                                    Resizable="false" ConfirmText="Eliminar registro?">
                                                    <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="ButtonColumn" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div class="col-md-4">
                                    <h4>Peso</h4>
                                    <telerik:RadGrid ID="radGridPeso"
                                        runat="server"
                                        GridLines="None"
                                        OnNeedDataSource="radGridPeso_NeedDataSource"
                                        OnDeleteCommand="radGridPeso_DeleteCommand"
                                        OnInsertCommand="radGridPeso_InsertCommand"
                                        OnUpdateCommand="radGridPeso_UpdateCommand"
                                        OnItemDataBound="radGridPeso_ItemDataBound"
                                        Width="100%"
                                        ShowStatusBar="true"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        PageSize="10"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top"
                                            ShowFooter="false" DataKeyNames="id"
                                            EditMode="InPlace"
                                            CommandItemSettings-SaveChangesText="Agregar"
                                            CommandItemSettings-CancelChangesText="Cancelar"
                                            CommandItemSettings-AddNewRecordText="Agregar"
                                            CommandItemSettings-RefreshText="Actualizar"
                                            NoDetailRecordsText="No hay registros para mostrar."
                                            NoMasterRecordsText="No hay registros para mostrar."
                                            EditFormSettings-EditColumn-UpdateText="Actualizar">
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                    HeaderStyle-Width="80px" FilterControlWidth="50px" ItemStyle-Width="50px" UpdateText="Actualizar" CancelText="Cancelar">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridNumericColumn HeaderText="Peso (Kg)" DataField="valor1" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="valor1" FilterControlWidth="20px" />
                                                <telerik:GridDateTimeColumn HeaderText="Fecha Toma" DataField="fechaEvento" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px" FooterStyle-Width="200px" UniqueName="fechaEvento" FilterControlWidth="100px" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                    Text="Eliminar" UniqueName="DeleteColumn"
                                                    Resizable="false" ConfirmText="Eliminar registro?">
                                                    <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="ButtonColumn" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                                <div class="col-md-4">
                                    <h4>Talla</h4>
                                    <telerik:RadGrid ID="radGridTalla"
                                        runat="server"
                                        GridLines="None"
                                        OnNeedDataSource="radGridTalla_NeedDataSource"
                                        OnInsertCommand="radGridTalla_InsertCommand"
                                        OnUpdateCommand="radGridTalla_UpdateCommand"
                                        OnDeleteCommand="radGridTalla_DeleteCommand"
                                        OnItemDataBound="radGridTalla_ItemDataBound"
                                        Width="100%"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        PageSize="10"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                        </ClientSettings>
                                        <MasterTableView
                                            CommandItemDisplay="Top"
                                            ShowFooter="false" DataKeyNames="id"
                                            EditMode="InPlace"
                                            CommandItemSettings-AddNewRecordText="Agregar"
                                            CommandItemSettings-RefreshText="Actualizar"
                                            CommandItemSettings-SaveChangesText="Agregar"
                                            CommandItemSettings-CancelChangesText="Cancelar"
                                            NoDetailRecordsText="No hay registros para mostrar."
                                            NoMasterRecordsText="No hay registros para mostrar."
                                            EditFormSettings-EditColumn-UpdateText="Actualizar">
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                    HeaderStyle-Width="80px" FilterControlWidth="50px" ItemStyle-Width="50px" UpdateText="Actualizar" CancelText="Cancelar">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridNumericColumn HeaderText="Talla (m)" DataField="valor1" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="valor1" FilterControlWidth="20px" />
                                                <telerik:GridDateTimeColumn HeaderText="Fecha Toma" DataField="fechaEvento" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px" FooterStyle-Width="200px" UniqueName="fechaEvento" FilterControlWidth="100px" />
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                    Text="Eliminar" UniqueName="DeleteColumn"
                                                    Resizable="false" ConfirmText="Eliminar registro?">
                                                    <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="ButtonColumn" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </telerik:RadWizardStep>
                    <telerik:RadWizardStep ID="RadWizardStep3" Title="Actividades del programa" DisplayCancelButton="false" runat="server">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Bootstrap" MultiPageID="RadMultiPage1" Width="100%" SelectedIndex="0">
                            <Tabs>
                                <telerik:RadTab Text="Interconsultas" runat="server" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Exámenes de laboratorio" runat="server" Selected="true">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Otros exámenes y procedimientos" runat="server" Selected="true">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
                            <telerik:RadPageView runat="server" ID="RadPageViewInterconsultas" Visible="true">
                                <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                                    <h4 class="encabezadoWizard">Interconsultas</h4>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <telerik:RadComboBox
                                                ID="cboOtrasInterconsultas"
                                                OnItemsRequested="cboOtrasInterconsultas_ItemsRequested"
                                                AppendDataBoundItems="true"
                                                AllowCustomText="true"
                                                AutoPostBack="true"
                                                runat="server"
                                                EnableLoadOnDemand="true"
                                                Filter="Contains"
                                                Width="100%"
                                                EmptyMessage="Buscar...">
                                                <CollapseAnimation Type="InOutElastic" />
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 5px">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button runat="server"
                                                ID="btnGuardarOtrasInterconsultas"
                                                OnClick="btnGuardarOtrasInterconsultas_Click"
                                                CssClass="btn btn-md btn-block btn-primary"
                                                Text="Guardar"
                                                Width="100%"
                                                Height="31px"
                                                CausesValidation="false" />
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 30px">
                                        <div class="col-md-12">
                                            <h4 class="encabezadoWizard">Bandeja Interconsultas</h4>
                                            <telerik:RadGrid ID="radGridInterconsultas"
                                                runat="server"
                                                Width="1000px"
                                                AllowSorting="True"
                                                AllowPaging="True"
                                                OnNeedDataSource="radGridInterconsultas_NeedDataSource"
                                                OnItemCommand="radGridInterconsultas_ItemCommand"
                                                OnUpdateCommand="radGridInterconsultas_UpdateCommand"
                                                OnDeleteCommand="radGridInterconsultas_DeleteCommand"
                                                OnDetailTableDataBind="radGridInterconsultas_DetailTableDataBind"
                                                AutoGenerateColumns="false">
                                                <ClientSettings>
                                                </ClientSettings>
                                                <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="Codigo"
                                                    EditFormSettings-EditColumn-CancelText="Cancelar"
                                                    EditFormSettings-EditColumn-InsertText="Insertar"
                                                    EditFormSettings-EditColumn-UpdateText="Actualizar"
                                                    EditFormSettings-EditColumn-EditText="Editar">
                                                    <DetailTables>
                                                        <telerik:GridTableView DataKeyNames="id" Name="DetalleInterConsultas" Width="100%" NoMasterRecordsText="No hay registros para mostrar."
                                                            NoDetailRecordsText="No hay registros para mostrar." EditFormSettings-EditColumn-CancelText="Cancelar"
                                                            EditFormSettings-EditColumn-InsertText="Insertar"
                                                            EditFormSettings-EditColumn-UpdateText="Actualizar"
                                                            EditFormSettings-EditColumn-EditText="Editar">
                                                            <ColumnGroups>
                                                                <telerik:GridColumnGroup Name="DetalleInterConsultas" HeaderText="Detalle registros interconsultas" HeaderStyle-Font-Size="Larger"
                                                                    HeaderStyle-HorizontalAlign="Center" />
                                                            </ColumnGroups>
                                                            <Columns>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar" ColumnGroupName="DetalleInterConsultas"
                                                                    HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridBoundColumn DataField="id" HeaderText="ID" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="id" FilterControlWidth="30px" ReadOnly="true" Visible="false" ColumnGroupName="DetalleInterConsultas" />
                                                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="80px" ReadOnly="true" ColumnGroupName="DetalleInterConsultas" Visible="false" />
                                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="300px" ItemStyle-Width="300px" FooterStyle-Width="300px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="300px" ReadOnly="true" ColumnGroupName="DetalleInterConsultas" />
                                                                <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="80px" ColumnGroupName="DetalleInterConsultas">
                                                                    <ItemTemplate>
                                                                        <%#DataBinder.Eval(Container.DataItem, "Estado")%>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadComboBox runat="server" ID="cboEstadoGuia" SelectedValue='<%#Bind("idEstado") %>'>
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Value="1" Text="Programada" />
                                                                                <telerik:RadComboBoxItem Value="2" Text="No Programada" />
                                                                                <telerik:RadComboBoxItem Value="3" Text="Cumplida" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridDateTimeColumn DataField="fechaEvento" HeaderText="FechaEvento" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" DataFormatString="{0:M/d/yyyy}"
                                                                    ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="FechaRegistro" FilterControlWidth="80px" ColumnGroupName="DetalleInterConsultas" />
                                                            </Columns>
                                                        </telerik:GridTableView>
                                                    </DetailTables>
                                                    <Columns>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                            HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="80px" ReadOnly="true" />
                                                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="250px" ItemStyle-Width="250px" FooterStyle-Width="250px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="250px" ReadOnly="true" />
                                                        <telerik:GridBoundColumn DataField="valor1" HeaderText="Frecuencia anual" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="valor1" FilterControlWidth="80px" />
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                            Text="Eliminar" UniqueName="DeleteColumn" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px"
                                                            Resizable="false" ConfirmText="Eliminar registro?">
                                                            <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                            <ItemStyle CssClass="ButtonColumn" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageViewExamenes" Visible="true">
                                <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="encabezadoWizard">Bandeja exámenes</h4>
                                            <telerik:RadGrid ID="radGridExamenesAyudas"
                                                runat="server"
                                                Width="1000px"
                                                AllowSorting="True"
                                                AllowPaging="True"
                                                OnNeedDataSource="radGridExamenesAyudas_NeedDataSource"
                                                OnItemCommand="radGridExamenesAyudas_ItemCommand"
                                                OnUpdateCommand="radGridExamenesAyudas_UpdateCommand"
                                                OnInsertCommand="radGridExamenesAyudas_InsertCommand"
                                                OnDetailTableDataBind="radGridExamenesAyudas_DetailTableDataBind"
                                                OnDeleteCommand="radGridExamenesAyudas_DeleteCommand"
                                                AutoGenerateColumns="false">
                                                <ClientSettings>
                                                </ClientSettings>
                                                <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="Codigo"
                                                    EditFormSettings-EditColumn-CancelText="Cancelar" NoDetailRecordsText="No hay registros"
                                                    EditFormSettings-EditColumn-InsertText="Insertar" NoMasterRecordsText="No hay registros"
                                                    EditFormSettings-EditColumn-UpdateText="Actualizar"
                                                    EditFormSettings-EditColumn-EditText="Editar">
                                                    <DetailTables>
                                                        <telerik:GridTableView DataKeyNames="id" Name="DetalleExamenes" Width="100%" NoMasterRecordsText="No hay registros para mostrar." NoDetailRecordsText="No hay registros para mostrar."
                                                            EditFormSettings-EditColumn-CancelText="Cancelar"
                                                            EditFormSettings-EditColumn-InsertText="Insertar"
                                                            EditFormSettings-EditColumn-UpdateText="Actualizar"
                                                            EditFormSettings-EditColumn-EditText="Editar">
                                                            <ColumnGroups>
                                                                <telerik:GridColumnGroup Name="DetalleExamenes" HeaderText="Detalle registros exámenes" HeaderStyle-Font-Size="Larger"
                                                                    HeaderStyle-HorizontalAlign="Center" />
                                                            </ColumnGroups>
                                                            <Columns>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                                    HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                                    <ItemStyle CssClass="MyImageButton" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridBoundColumn DataField="id" HeaderText="ID" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="id" FilterControlWidth="30px" ReadOnly="true" Visible="false" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="80px" ReadOnly="true" Visible="false" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="250px" ItemStyle-Width="250px" FooterStyle-Width="250px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="250px" ReadOnly="true" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridBoundColumn DataField="valor3" HeaderText="Resultado" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="valor1" FilterControlWidth="80px" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridDateTimeColumn DataField="fechaEvento" HeaderText="FechaRegistro" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="FechaRegistro" FilterControlWidth="80px" DataFormatString="{0:M/d/yyyy}" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                                    Text="Eliminar" UniqueName="DeleteColumn" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px"
                                                                    Resizable="false" ConfirmText="Eliminar registro?">
                                                                    <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                                    <ItemStyle CssClass="ButtonColumn" />
                                                                </telerik:GridButtonColumn>
                                                            </Columns>
                                                        </telerik:GridTableView>
                                                    </DetailTables>
                                                    <Columns>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                            HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="80px" ReadOnly="true" />
                                                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="250px" ItemStyle-Width="250px" FooterStyle-Width="250px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="250px" ReadOnly="true" />
                                                        <telerik:GridBoundColumn DataField="valor1" HeaderText="Frecuencia anual" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="valor1" FilterControlWidth="80px" />
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageViewOtrosExamenesProcedimientos" runat="server" Visible="true">
                                <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                                    <h4 class="encabezadoWizard">Otros exámenes y procedimientos</h4>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <telerik:RadComboBox
                                                ID="cboOtrosExamenesProcedimientos"
                                                AppendDataBoundItems="true"
                                                OnItemsRequested="cboOtrosExamenesProcedimientos_ItemsRequested"
                                                runat="server"
                                                Localization-CheckAllString="Seleccionar todos"
                                                Localization-AllItemsCheckedString="Todos Seleccionados"
                                                EnableLoadOnDemand="true"
                                                DropDownWidth="700px"
                                                Filter="Contains"
                                                Width="100%"
                                                EmptyMessage="Buscar...">
                                                <CollapseAnimation Type="InOutElastic" />
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 5px">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="Label2" Text="" runat="server" Width="500px">
                                            </asp:Label>
                                            <asp:Button runat="server"
                                                ID="btnGuardarOtrosExamenesProcedimientos" OnClick="btnGuardarOtrosExamenesProcedimientos_Click"
                                                CssClass="btn btn-md btn-block btn-primary"
                                                Text="Guardar"
                                                Width="100%"
                                                Height="31px"
                                                CausesValidation="false" />
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 class="encabezadoWizard">Bandeja de otros exámenes y procedimientos</h4>
                                            <telerik:RadGrid ID="radGridOtrosExamenesProcedimiento"
                                                runat="server"
                                                Width="1000px"
                                                AllowSorting="True"
                                                AllowPaging="True"
                                                OnNeedDataSource="radGridOtrosExamenesProcedimiento_NeedDataSource"
                                                OnUpdateCommand="radGridOtrosExamenesProcedimiento_UpdateCommand"
                                                OnDetailTableDataBind="radGridOtrosExamenesProcedimiento_DetailTableDataBind"
                                                OnDeleteCommand="radGridOtrosExamenesProcedimiento_DeleteCommand"
                                                AutoGenerateColumns="false">
                                                <ClientSettings>
                                                </ClientSettings>
                                                <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="Codigo"
                                                    EditFormSettings-EditColumn-CancelText="Cancelar"
                                                    EditFormSettings-EditColumn-InsertText="Insertar"
                                                    EditFormSettings-EditColumn-UpdateText="Actualizar"
                                                    EditFormSettings-EditColumn-EditText="Editar">
                                                    <DetailTables>
                                                        <telerik:GridTableView DataKeyNames="id"
                                                            Name="DetalleOtrosExamenesProcedimientos" Width="100%"
                                                            NoMasterRecordsText="No hay registros para mostrar."
                                                            NoDetailRecordsText="No hay registros para mostrar."
                                                            EditFormSettings-EditColumn-CancelText="Cancelar"
                                                            EditFormSettings-EditColumn-InsertText="Insertar"
                                                            EditFormSettings-EditColumn-UpdateText="Actualizar"
                                                            TableLayout="Fixed"
                                                            EditFormSettings-EditColumn-EditText="Editar">
                                                            <ColumnGroups>
                                                                <telerik:GridColumnGroup Name="DetalleExamenes" HeaderText="Detalle registros otros exámenes y procedimientos" HeaderStyle-Font-Size="Larger"
                                                                    HeaderStyle-HorizontalAlign="Center" />
                                                            </ColumnGroups>
                                                            <Columns>
                                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                                    HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                                    <ItemStyle CssClass="MyImageButton" />
                                                                </telerik:GridEditCommandColumn>
                                                                <telerik:GridBoundColumn DataField="id" HeaderText="ID" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="id" FilterControlWidth="30px" ReadOnly="true" Visible="false" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="150px" ItemStyle-Width="150px" FooterStyle-Width="150px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="150px" ReadOnly="true" Visible="false" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="100px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="100px" ReadOnly="true" ColumnGroupName="DetalleExamenes" />
                                                                <telerik:GridTemplateColumn HeaderText="Reporte" ItemStyle-Width="150px" FilterControlWidth="150px" FooterStyle-Width="150px" HeaderStyle-Width="150px" ColumnGroupName="DetalleExamenes">
                                                                    <ItemTemplate>
                                                                        <%#DataBinder.Eval(Container.DataItem, "observaciones")%>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <telerik:RadTextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Width="300px">
                                                                        </telerik:RadTextBox>
                                                                    </EditItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridDateTimeColumn DataField="fechaEvento" HeaderText="FechaRegistro" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="FechaRegistro" FilterControlWidth="80px" DataFormatString="{0:M/d/yyyy}" ColumnGroupName="DetalleExamenes" />
                                                            </Columns>
                                                        </telerik:GridTableView>
                                                    </DetailTables>
                                                    <Columns>
                                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                            HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                        </telerik:GridEditCommandColumn>
                                                        <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="80px" ReadOnly="true" />
                                                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="250px" ItemStyle-Width="250px" FooterStyle-Width="250px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="250px" ReadOnly="true" />
                                                        <telerik:GridBoundColumn DataField="valor1" HeaderText="Frecuencia anual" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="valor1" FilterControlWidth="80px" />
                                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                            Text="Eliminar" UniqueName="DeleteColumn" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px"
                                                            Resizable="false" ConfirmText="Eliminar registro?">
                                                            <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                            <ItemStyle CssClass="ButtonColumn" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:RadWizardStep>
                    <telerik:RadWizardStep ID="RadWizardStep4" Title="Monitoreo remoto" DisplayCancelButton="false" runat="server">
                        <telerik:RadTabStrip ID="RadTabStrip2" runat="server" Skin="Bootstrap" MultiPageID="RadMultiPage2" Width="100%" SelectedIndex="0">
                            <Tabs>
                                <telerik:RadTab Text="Monitoreo Remoto" runat="server" Selected="True">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Medicamentos" runat="server" Selected="true">
                                </telerik:RadTab>
                                <telerik:RadTab Text="Mediciones biométricas" runat="server" Selected="true">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage runat="server" ID="RadMultiPage2" SelectedIndex="0">
                            <telerik:RadPageView runat="server" ID="RadPageViewMonitoreoRemoto" Visible="true">
                                <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                                    <h4 class="encabezadoWizard">Monitoreo remoto</h4>
                                    <div class="row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="cboSiNoMonotoreo" runat="server" Width="100%" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="Seleccione...">
                                                </asp:ListItem>
                                                <asp:ListItem Value="1" Text="Si">
                                                </asp:ListItem>
                                                <asp:ListItem Value="2" Text="No">
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 5px">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button ID="btnGuardarSiNo" runat="server" OnClick="btnGuardarSiNo_Click" CssClass="btn btn-lg btn-primary btn-block text-center" Width="100%" Text="Guardar" />
                                        </div>
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                    <asp:Panel ID="panelAspectos" runat="server" Visible="false">
                                        <h4 class="encabezadoWizard">Aspectos monitoreados</h4>
                                        <div class="row">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-4">
                                                <telerik:RadComboBox
                                                    ID="cboAspectos"
                                                    runat="server"
                                                    CheckBoxes="true"
                                                    EnableCheckAllItemsCheckBox="true"
                                                    Localization-CheckAllString="Seleccionar todos"
                                                    CheckedItemsTexts="FitInInput"
                                                    Localization-AllItemsCheckedString="Todos Seleccionados"
                                                    Filter="Contains"
                                                    Width="100%"
                                                    EmptyMessage="Aspectos monitoreados">
                                                    <CollapseAnimation Type="InOutElastic" />
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="1" Text="Medicamentos" />
                                                        <telerik:RadComboBoxItem Value="2" Text="Presión arterial" />
                                                        <telerik:RadComboBoxItem Value="3" Text="Glucometría" />
                                                        <telerik:RadComboBoxItem Value="4" Text="Peso" />
                                                        <telerik:RadComboBoxItem Value="5" Text="Ejercicio" />
                                                        <telerik:RadComboBoxItem Value="6" Text="Dieta" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top: 5px">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="Label32" Text="" runat="server" Width="500px">
                                                </asp:Label>
                                                <asp:Button runat="server"
                                                    ID="btnGuardarMonitoreo"
                                                    OnClick="btnGuardarMonitoreo_Click"
                                                    CssClass="btn btn-md btn-block btn-primary"
                                                    Text="Guardar"
                                                    Width="100%"
                                                    CausesValidation="false" />
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageViewMedicamentos" Visible="false">
                                <div class="row" runat="server" id="divGrilla">
                                    <div class="col-md-12">
                                        <h4 class="encabezadoWizard">Bandeja medicamentos</h4>
                                        <div class="row" style="padding-top: 20px" runat="server" id="divCombo">
                                            <div class="col-md-12">
                                                <telerik:RadComboBox
                                                    ID="cboMedicamentos" AutoPostBack="true" EnableLoadOnDemand="true"
                                                    runat="server" OnItemsRequested="cboMedicamentos_ItemsRequested"
                                                    Localization-CheckAllString="Seleccionar todos"
                                                    Localization-AllItemsCheckedString="Todos los medicamentos seleccionados"
                                                    Localization-ItemsCheckedString="Medicamentos seleccionados"
                                                    CheckedItemsTexts="FitInInput"
                                                    Filter="Contains"
                                                    Width="100%"
                                                    EmptyMessage="Buscar medicamentos">
                                                    <CollapseAnimation Type="InOutElastic" />
                                                </telerik:RadComboBox>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top: 5px">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Button ID="btnGuardarMedicamentosParametricos" runat="server" CssClass="btn btn-md btn-block btn-primary"
                                                    Text="Guardar" Width="100%" OnClick="btnGuardarMedicamentosParametricos_Click" CausesValidation="false" />
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top: 2px">
                                            <div class="col-md-12">
                                                <telerik:RadGrid ID="radGridMedicamentos" runat="server"
                                                    RegisterWithScriptManager="false"
                                                    ValidationSettings-EnableValidation="false"
                                                    OnNeedDataSource="radGridMedicamentos_NeedDataSource"
                                                    OnInsertCommand="radGridMedicamentos_InsertCommand"
                                                    OnUpdateCommand="radGridMedicamentos_UpdateCommand"
                                                    OnDeleteCommand="radGridMedicamentos_DeleteCommand"
                                                    OnItemDataBound="radGridMedicamentos_ItemDataBound"
                                                    ShowStatusBar="true"
                                                    Width="100%"
                                                    AllowSorting="True"
                                                    AllowPaging="True"
                                                    AutoGenerateColumns="false">
                                                    <ClientSettings>
                                                    </ClientSettings>
                                                    <MasterTableView CommandItemDisplay="Top" EditFormSettings-EditColumn-AutoPostBackOnFilter="true" DataKeyNames="idGuiaPaciente"
                                                        ShowFooter="true"
                                                        CommandItemStyle-HorizontalAlign="Right"
                                                        CommandItemSettings-ShowAddNewRecordButton="true"
                                                        CommandItemSettings-ShowRefreshButton="false"
                                                        EditMode="InPlace"
                                                        HorizontalAlign="Center"
                                                        CommandItemSettings-SaveChangesText="Agregar"
                                                        CommandItemSettings-CancelChangesText="Cancelar"
                                                        CommandItemSettings-AddNewRecordText="Agregar"
                                                        CommandItemSettings-RefreshText="Actualizar"
                                                        NoDetailRecordsText="No hay registros para mostrar."
                                                        NoMasterRecordsText="No hay registros para mostrar."
                                                        EditFormSettings-EditColumn-UpdateText="Actualizar">
                                                        <Columns>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                                HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                                <ItemStyle CssClass="MyImageButton" />
                                                            </telerik:GridEditCommandColumn>
                                                            <telerik:GridBoundColumn DataField="txt1" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px"
                                                                ItemStyle-Width="80px" FooterStyle-Width="80px"
                                                                AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="80px" />
                                                            <telerik:GridTemplateColumn HeaderText="Presentación" ItemStyle-Width="80px" FilterControlWidth="80px" FooterStyle-Width="80px" HeaderStyle-Width="80px">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Forma")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadComboBox runat="server" ID="cboForma" SelectedValue='<%#Bind("txt4") %>'>
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Value="0" Text="Seleccione.." />
                                                                            <telerik:RadComboBoxItem Value="1" Text="Capsulas" />
                                                                            <telerik:RadComboBoxItem Value="2" Text="Tabletas" />
                                                                            <telerik:RadComboBoxItem Value="3" Text="Gotas" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridNumericColumn DecimalDigits="0" HeaderText="Tomas" DataField="valor4" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="valor4" FilterControlWidth="20px" />
                                                            <telerik:GridNumericColumn DecimalDigits="0" HeaderText="Cantidad por toma" DataField="txt3" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="txt3" FilterControlWidth="20px" />
                                                            <telerik:GridTemplateColumn HeaderText="Frecuencia" ItemStyle-Width="70px" FilterControlWidth="70px" FooterStyle-Width="70px" HeaderStyle-Width="70px">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Periodicidad")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <telerik:RadComboBox runat="server" ID="cboPeriodicidad" SelectedValue='<%#Bind("txt5") %>'>
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Value="0" Text="Seleccione.." />
                                                                            <telerik:RadComboBoxItem Value="1" Text="Por día" />
                                                                            <telerik:RadComboBoxItem Value="2" Text="Por semana" />
                                                                            <telerik:RadComboBoxItem Value="3" Text="Quincenal" />
                                                                            <telerik:RadComboBoxItem Value="4" Text="Mensual" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                                </EditItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridDateTimeColumn HeaderText="Fecha Inicio" DataField="fecha1" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                                DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                                                                FooterStyle-Width="80px" UniqueName="fecha1" FilterControlWidth="80px" />
                                                            <telerik:GridDateTimeColumn HeaderText="Fecha Fin" DataField="fecha2" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                                DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px"
                                                                FooterStyle-Width="80px" UniqueName="fecha2" FilterControlWidth="80px" />
                                                            <telerik:GridBoundColumn DataField="observaciones" HeaderText="Recomendaciones" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px"
                                                                ItemStyle-Width="80px" FooterStyle-Width="80px"
                                                                AutoPostBackOnFilter="true" UniqueName="observaciones" FilterControlWidth="80px" />
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView runat="server" ID="RadPageViewMedicionesBiometricas" Visible="false">
                                <div class="row" style="padding-bottom: 15px; padding-top: 20px" runat="server" id="div1">
                                    <div class="col-md-6">
                                        <h4 class="encabezadoWizard">Limites de presión arterial</h4>
                                        <telerik:RadGrid ID="radGridMedicionesBiometricas"
                                            runat="server"
                                            RegisterWithScriptManager="false"
                                            ValidationSettings-EnableValidation="false"
                                            OnNeedDataSource="radGridMedicionesBiometricas_NeedDataSource"
                                            OnInsertCommand="radGridMedicionesBiometricas_InsertCommand"
                                            OnUpdateCommand="radGridMedicionesBiometricas_UpdateCommand"
                                            OnDeleteCommand="radGridMedicionesBiometricas_DeleteCommand"
                                            OnItemDataBound="radGridMedicionesBiometricas_ItemDataBound"
                                            ShowStatusBar="true"
                                            Width="100%"
                                            AllowSorting="True"
                                            AllowPaging="True"
                                            AutoGenerateColumns="false">
                                            <ClientSettings>
                                            </ClientSettings>
                                            <MasterTableView CommandItemDisplay="Top" EditFormSettings-EditColumn-AutoPostBackOnFilter="true" DataKeyNames="idGuiaPaciente"
                                                ShowFooter="false"
                                                CommandItemStyle-HorizontalAlign="Right"
                                                CommandItemSettings-ShowAddNewRecordButton="true"
                                                CommandItemSettings-ShowRefreshButton="false"
                                                EditMode="InPlace"
                                                HorizontalAlign="Center"
                                                CommandItemSettings-SaveChangesText="Agregar"
                                                CommandItemSettings-CancelChangesText="Cancelar"
                                                CommandItemSettings-AddNewRecordText="Agregar"
                                                CommandItemSettings-RefreshText="Actualizar"
                                                NoDetailRecordsText="No hay registros para mostrar."
                                                NoMasterRecordsText="No hay registros para mostrar."
                                                EditFormSettings-EditColumn-UpdateText="Actualizar">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                        HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                        <ItemStyle CssClass="MyImageButton" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridDateTimeColumn HeaderText="Fecha" DataField="fechaEvento" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px"
                                                        FooterStyle-Width="100px" UniqueName="fechaEvento" FilterControlWidth="100px" />
                                                    <telerik:GridNumericColumn HeaderText="Sistólica superior" DataField="valor1" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="40px"
                                                        ItemStyle-Width="40px" FooterStyle-Width="40px" UniqueName="valor1" FilterControlWidth="40px" />
                                                    <telerik:GridNumericColumn HeaderText="Sistólica inferior" DataField="valor2" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="40px"
                                                        ItemStyle-Width="40px" FooterStyle-Width="40px" UniqueName="valor2" FilterControlWidth="40px" />
                                                    <telerik:GridNumericColumn HeaderText="Diastólica superior" DataField="valor3" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px"
                                                        ItemStyle-Width="40px" FooterStyle-Width="40px" UniqueName="valor3" FilterControlWidth="40px" />
                                                    <telerik:GridNumericColumn HeaderText="Diastólica inferior" DataField="valor4" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="40px"
                                                        ItemStyle-Width="40px" FooterStyle-Width="40px" UniqueName="valor4" FilterControlWidth="40px" />
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                        Text="Eliminar" UniqueName="DeleteColumn"
                                                        Resizable="false" ConfirmText="Eliminar registro?">
                                                        <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                        <ItemStyle CssClass="ButtonColumn" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="col-md-6">
                                        <h4 class="encabezadoWizard">Monitoreo remoto de presión arterial</h4>
                                        <telerik:RadGrid ID="radGridMonitoreoPresion"
                                            runat="server"
                                            RegisterWithScriptManager="false"
                                            ValidationSettings-EnableValidation="false"
                                            OnNeedDataSource="radGridMonitoreoPresion_NeedDataSource"
                                            OnInsertCommand="radGridMonitoreoPresion_InsertCommand"
                                            OnUpdateCommand="radGridMonitoreoPresion_UpdateCommand"
                                            OnDeleteCommand="radGridMonitoreoPresion_DeleteCommand"
                                            OnItemDataBound="radGridMonitoreoPresion_ItemDataBound"
                                            ShowStatusBar="true"
                                            Width="100%"
                                            AllowSorting="True"
                                            AllowPaging="True"
                                            AutoGenerateColumns="false">
                                            <ClientSettings>
                                            </ClientSettings>
                                            <MasterTableView CommandItemDisplay="Top" EditFormSettings-EditColumn-AutoPostBackOnFilter="true"
                                                DataKeyNames="idGuiaPaciente"
                                                ShowFooter="false"
                                                CommandItemStyle-HorizontalAlign="Right"
                                                CommandItemSettings-ShowAddNewRecordButton="true"
                                                CommandItemSettings-ShowRefreshButton="false"
                                                EditMode="InPlace"
                                                HorizontalAlign="Center"
                                                CommandItemSettings-SaveChangesText="Agregar"
                                                CommandItemSettings-CancelChangesText="Cancelar"
                                                CommandItemSettings-AddNewRecordText="Agregar"
                                                CommandItemSettings-RefreshText="Actualizar"
                                                NoDetailRecordsText="No hay registros para mostrar."
                                                NoMasterRecordsText="No hay registros para mostrar."
                                                EditFormSettings-EditColumn-UpdateText="Actualizar">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                        HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                        <ItemStyle CssClass="MyImageButton" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn DataField="unidad2" HeaderText="Cantidad" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px"
                                                        ItemStyle-Width="50px" FooterStyle-Width="50px"
                                                        AutoPostBackOnFilter="true" UniqueName="unidad2" FilterControlWidth="80px" />
                                                    <telerik:GridTemplateColumn HeaderText="Frecuencia anual" ItemStyle-Width="50px" FilterControlWidth="50px" FooterStyle-Width="50px" UniqueName="unidad3" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "Periodicidad")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadComboBox runat="server" ID="cboPeriodicidad"
                                                                SelectedValue='<%#Bind("unidad3") %>'>
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Value="0" Text="Seleccione.." />
                                                                    <telerik:RadComboBoxItem Value="1" Text="Por día" />
                                                                    <telerik:RadComboBoxItem Value="2" Text="Por semana" />
                                                                    <telerik:RadComboBoxItem Value="3" Text="Quincenal" />
                                                                    <telerik:RadComboBoxItem Value="4" Text="Mensual" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridDateTimeColumn HeaderText="Fecha Inicio" DataField="fecha1" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px"
                                                        FooterStyle-Width="100px" UniqueName="fecha1" FilterControlWidth="100px" />
                                                    <telerik:GridDateTimeColumn HeaderText="Fecha Fin" DataField="fecha2" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px"
                                                        FooterStyle-Width="100px" UniqueName="fecha2" FilterControlWidth="100px" />
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                        Text="Eliminar" UniqueName="DeleteColumn"
                                                        Resizable="false" ConfirmText="Eliminar registro?">
                                                        <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                        <ItemStyle CssClass="ButtonColumn" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                                <div class="row" style="padding-top: 20px">
                                    <div class="col-md-6">
                                        <h4 class="encabezadoWizard">Limites de glucometría</h4>
                                        <telerik:RadGrid ID="radGridLimitesGluomtria"
                                            runat="server"
                                            RegisterWithScriptManager="false"
                                            ValidationSettings-EnableValidation="false"
                                            OnNeedDataSource="radGridLimitesGluomtria_NeedDataSource"
                                            OnInsertCommand="radGridLimitesGluomtria_InsertCommand"
                                            OnUpdateCommand="radGridLimitesGluomtria_UpdateCommand"
                                            OnDeleteCommand="radGridLimitesGluomtria_DeleteCommand"
                                            OnItemDataBound="radGridLimitesGluomtria_ItemDataBound"
                                            ShowStatusBar="true"
                                            Width="100%"
                                            AllowSorting="True"
                                            AllowPaging="True"
                                            AutoGenerateColumns="false">
                                            <ClientSettings>
                                            </ClientSettings>
                                            <MasterTableView CommandItemDisplay="Top" EditFormSettings-EditColumn-AutoPostBackOnFilter="true"
                                                DataKeyNames="idGuiaPaciente"
                                                ShowFooter="false"
                                                CommandItemStyle-HorizontalAlign="Right"
                                                CommandItemSettings-ShowAddNewRecordButton="true"
                                                CommandItemSettings-ShowRefreshButton="false"
                                                EditMode="InPlace"
                                                HorizontalAlign="Center"
                                                CommandItemSettings-SaveChangesText="Agregar"
                                                CommandItemSettings-CancelChangesText="Cancelar"
                                                CommandItemSettings-AddNewRecordText="Agregar"
                                                CommandItemSettings-RefreshText="Actualizar"
                                                NoDetailRecordsText="No hay registros para mostrar."
                                                NoMasterRecordsText="No hay registros para mostrar."
                                                EditFormSettings-EditColumn-UpdateText="Actualizar">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                        HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                        <ItemStyle CssClass="MyImageButton" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridDateTimeColumn HeaderText="Fecha" DataField="fechaEvento" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px"
                                                        FooterStyle-Width="100px" UniqueName="fechaEvento" FilterControlWidth="100px" />
                                                    <telerik:GridNumericColumn HeaderText="Glucometría superior" DataField="valor5" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px"
                                                        ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="valor5" FilterControlWidth="20px" />
                                                    <telerik:GridBoundColumn HeaderText="Glucometría inferior" DataField="unidad1" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px"
                                                        ItemStyle-Width="40px" FooterStyle-Width="40px" UniqueName="unidad1" FilterControlWidth="40px" />
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                        Text="Eliminar" UniqueName="DeleteColumn"
                                                        Resizable="false" ConfirmText="Eliminar registro?">
                                                        <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                        <ItemStyle CssClass="ButtonColumn" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div class="col-md-6">
                                        <h4 class="encabezadoWizard">Monitoreo remoto de glucometría</h4>
                                        <telerik:RadGrid ID="radGridMonitoreoGlucometria"
                                            runat="server"
                                            RegisterWithScriptManager="false"
                                            ValidationSettings-EnableValidation="false"
                                            OnNeedDataSource="radGridMonitoreoGlucometria_NeedDataSource"
                                            OnInsertCommand="radGridMonitoreoGlucometria_InsertCommand"
                                            OnUpdateCommand="radGridMonitoreoGlucometria_UpdateCommand"
                                            OnDeleteCommand="radGridMonitoreoGlucometria_DeleteCommand"
                                            OnItemDataBound="radGridMonitoreoGlucometria_ItemDataBound"
                                            ShowStatusBar="true"
                                            Width="100%"
                                            AllowSorting="True"
                                            AllowPaging="True"
                                            AutoGenerateColumns="false">
                                            <ClientSettings>
                                            </ClientSettings>
                                            <MasterTableView CommandItemDisplay="Top" EditFormSettings-EditColumn-AutoPostBackOnFilter="true" DataKeyNames="idGuiaPaciente"
                                                ShowFooter="false"
                                                CommandItemStyle-HorizontalAlign="Right"
                                                CommandItemSettings-ShowAddNewRecordButton="true"
                                                CommandItemSettings-ShowRefreshButton="false"
                                                EditMode="InPlace"
                                                HorizontalAlign="Center"
                                                CommandItemSettings-SaveChangesText="Agregar"
                                                CommandItemSettings-CancelChangesText="Cancelar"
                                                CommandItemSettings-AddNewRecordText="Agregar"
                                                CommandItemSettings-RefreshText="Actualizar"
                                                NoDetailRecordsText="No hay registros para mostrar."
                                                NoMasterRecordsText="No hay registros para mostrar."
                                                EditFormSettings-EditColumn-UpdateText="Actualizar">
                                                <Columns>
                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                                        HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                                        <ItemStyle CssClass="MyImageButton" />
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridBoundColumn DataField="unidad2" HeaderText="Cantidad" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px"
                                                        ItemStyle-Width="50px" FooterStyle-Width="50px"
                                                        AutoPostBackOnFilter="true" UniqueName="unidad2" FilterControlWidth="80px" />
                                                    <telerik:GridTemplateColumn HeaderText="Frecuencia anual" ItemStyle-Width="50px" FilterControlWidth="50px" FooterStyle-Width="50px" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "Periodicidad")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <telerik:RadComboBox runat="server" ID="cboPeriodicidad" SelectedValue='<%#Bind("unidad3") %>'>
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Value="0" Text="Seleccione.." />
                                                                    <telerik:RadComboBoxItem Value="1" Text="Por día" />
                                                                    <telerik:RadComboBoxItem Value="2" Text="Por semana" />
                                                                    <telerik:RadComboBoxItem Value="3" Text="Quincenal" />
                                                                    <telerik:RadComboBoxItem Value="4" Text="Mensual" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </EditItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridDateTimeColumn HeaderText="Fecha Inicio" DataField="fecha1" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px"
                                                        FooterStyle-Width="100px" UniqueName="fecha1" FilterControlWidth="100px" />
                                                    <telerik:GridDateTimeColumn HeaderText="Fecha Fin" DataField="fecha2" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                        DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px"
                                                        FooterStyle-Width="100px" UniqueName="fecha2" FilterControlWidth="100px" />
                                                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                        Text="Eliminar" UniqueName="DeleteColumn"
                                                        Resizable="false" ConfirmText="Eliminar registro?">
                                                        <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                        <ItemStyle CssClass="ButtonColumn" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </telerik:RadWizardStep>
                    <telerik:RadWizardStep ID="RadWizardStep5" Title="Control de enfermería" DisplayCancelButton="false" runat="server">
                        <div class="container-fluid" style="padding-bottom: 15px; border: 1px solid #888; box-shadow: 0px 2px 5px #ccc">
                            <div class="row" style="padding-top: 20px" runat="server" id="div4" visible="true">
                                <div class="col-md-12">
                                    <h4 class="encabezadoWizard">Programación control de enfermeria</h4>
                                    <telerik:RadGrid ID="radGridControlEnfermeria"
                                        runat="server"
                                        RegisterWithScriptManager="false"
                                        ValidationSettings-EnableValidation="false"
                                        OnNeedDataSource="radGridControlEnfermeria_NeedDataSource"
                                        OnInsertCommand="radGridControlEnfermeria_InsertCommand"
                                        OnUpdateCommand="radGridControlEnfermeria_UpdateCommand"
                                        OnDeleteCommand="radGridControlEnfermeria_DeleteCommand"
                                        OnItemDataBound="radGridControlEnfermeria_ItemDataBound"
                                        ShowStatusBar="true"
                                        Width="100%"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top" EditFormSettings-EditColumn-AutoPostBackOnFilter="true"
                                            DataKeyNames="idGuiaPaciente,observaciones"
                                            CommandItemStyle-HorizontalAlign="Right"
                                            CommandItemSettings-ShowAddNewRecordButton="true"
                                            CommandItemSettings-ShowRefreshButton="false"
                                            EditMode="InPlace"
                                            HorizontalAlign="Center"
                                            CommandItemSettings-SaveChangesText="Agregar"
                                            CommandItemSettings-CancelChangesText="Cancelar"
                                            CommandItemSettings-AddNewRecordText="Agregar"
                                            CommandItemSettings-RefreshText="Actualizar"
                                            EditFormSettings-EditColumn-EditText="Editar"
                                            EditFormSettings-EditColumn-CancelText="Cancelar"
                                            EditFormSettings-EditColumn-ButtonType="FontIconButton"
                                            EditFormSettings-EditColumn-InsertText="Insertar"
                                            NoDetailRecordsText="No hay registros para mostrar."
                                            NoMasterRecordsText="No hay registros para mostrar."
                                            EditFormSettings-EditColumn-UpdateText="Actualizar">
                                            <Columns>
                                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar" InsertText="Guardar"
                                                    HeaderStyle-Width="100px" FilterControlWidth="100px" ItemStyle-Width="100px" UpdateText="Actualizar" CancelText="Cancelar"
                                                    FooterStyle-Width="100px">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridDateTimeColumn HeaderText="Próximo Control" DataField="unidad4" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="100px" ItemStyle-Width="100px"
                                                    FooterStyle-Width="100px" UniqueName="unidad4" FilterControlWidth="100px" />
                                                <telerik:GridTemplateColumn HeaderText="Tipo control" ItemStyle-Width="100px" FilterControlWidth="100px" FooterStyle-Width="100px" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Periodicidad")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox Width="150px" runat="server" ID="cboPeriodicidad" SelectedValue='<%#Bind("unidad3") %>'>
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="1" Text="Personal" />
                                                                <telerik:RadComboBoxItem Value="2" Text="Telefónica" />
                                                                <telerik:RadComboBoxItem Value="3" Text="Revisión HC" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Estado" ItemStyle-Width="100px" FilterControlWidth="100px" FooterStyle-Width="100px" HeaderStyle-Width="100px" UniqueName="Estado">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Forma")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox runat="server" ID="cboEstado" SelectedValue='<%#Bind("txt4") %>'>
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="1" Text="Activo" />
                                                                <telerik:RadComboBoxItem Value="2" Text="Suspendido" />
                                                                <telerik:RadComboBoxItem Value="3" Text="Inactivo" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridDateTimeColumn HeaderText="Fecha Realización" DataField="fechaEvento" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true"
                                                    DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" ItemStyle-Font-Size="Small" HeaderStyle-Width="100px" ItemStyle-Width="100px"
                                                    FooterStyle-Width="100px" UniqueName="fechaEvento" FilterControlWidth="100px" />
                                                <telerik:GridTemplateColumn HeaderText="Causal" UniqueName="Causal" ItemStyle-Width="100px" FilterControlWidth="100px"
                                                    FooterStyle-Width="100px" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Causal")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox runat="server" ID="cboCausal" SelectedValue='<%#Bind("unidad5") %>'>
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="0" Text="No aplica" />
                                                                <telerik:RadComboBoxItem Value="1" Text="Contactos no efectivos" />
                                                                <telerik:RadComboBoxItem Value="2" Text="Usuario viajando" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Observaciones" UniqueName="Observaciones"
                                                    ItemStyle-Width="50px" FilterControlWidth="50px" FooterStyle-Width="50px" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "observaciones")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Width="250px">
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column"
                                                    ItemStyle-Width="50px" FilterControlWidth="50px" FooterStyle-Width="50px" HeaderStyle-Width="50px"
                                                    Text="Eliminar" UniqueName="DeleteColumn"
                                                    Resizable="false" ConfirmText="Eliminar registro?">
                                                    <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="ButtonColumn" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </telerik:RadWizardStep>
                    <telerik:RadWizardStep ID="RadWizardStep6" StepType="Complete" runat="server">
                        <asp:Literal ID="ltrCompleted" Text="<h2>Finalizó!!!</h2><p>Felicitaciones!!!</p>" runat="server" />
                    </telerik:RadWizardStep>
                </WizardSteps>
            </telerik:RadWizard>
        </div>
    </div>
</asp:Content>
