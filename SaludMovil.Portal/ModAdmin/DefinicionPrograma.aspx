<%@ Page Title="Definición programa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefinicionPrograma.aspx.cs" Inherits="SaludMovil.Portal.ModAdmin.DefinicionPrograma" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="DefinicionPrograma" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        /*Se adiciona excepción en los estilos boostrap para ampliar el tamaño de la bandeja principal pacientes*/
        .container {
            width: 100% !Important;
        }
    </style>
    <h6 class="text-danger" id="hRuta" >ADMINISTRACIÓN / DEFINICIÓN DE PROGRAMAS</h6>
    <div class="container-fluid">
        .<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cboDiagnosticos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cboDiagnosticos" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridTipoGuia">
                    <UpdatedControls>
                        <%--<telerik:AjaxUpdatedControl ControlID="RadGridTipoGuia" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="RadGridGuia" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>--%>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
            EnableShadow="true" Position="Center" Animation="FlyIn" Title="Sistema de notificaciones" VisibleTitlebar="false">
        </telerik:RadNotification>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <div class="row">
            <div class="col-md-6" style="height: 380px !Important">
                <h3 class="encabezado">Programa</h3>
                <telerik:RadGrid ID="RadGridPrograma" runat="server"
                    OnNeedDataSource="RadGridPrograma_NeedDataSource"
                    OnInsertCommand="RadGridPrograma_InsertCommand"
                    OnUpdateCommand="RadGridPrograma_UpdateCommand"
                    OnItemCommand="RadGridPrograma_ItemCommand"
                    OnPreRender="RadGridPrograma_PreRender"
                    OnItemDataBound="RadGridPrograma_ItemDataBound"
                    AutoGenerateColumns="false">
                    <GroupingSettings CaseSensitive="false" />
                    <ClientSettings EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" EditMode="InPlace"
                        CommandItemSettings-AddNewRecordText="Agregar"
                        CommandItemSettings-ShowAddNewRecordButton="true"
                        CommandItemSettings-ShowRefreshButton="false"
                        DataKeyNames="idPrograma,idEstado,poblacionObjetivo,idRiesgoPrograma,createdBy,createdDate">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderText="Nombre" DataField="descripcion" AutoPostBackOnFilter="true" />
                            <telerik:GridDateTimeColumn HeaderText="Fecha Inicio" DataField="fechaInicio" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" />
                            <telerik:GridDateTimeColumn HeaderText="Fecha Fin" DataField="fechaFin" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" />
                            <telerik:GridTemplateColumn HeaderText="Población Objetivo" DataField="descPoblacion" AutoPostBackOnFilter="true" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" FilterControlWidth="100px">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "descPoblacion")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="rddlPoblacion" runat="server" Filter="Contains" DropDownAutoWidth="Enabled">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemStyle Width="40px" />
                            </telerik:GridTemplateColumn>                            
                            <telerik:GridNumericColumn HeaderText="Orden" DataField="orden" AutoPostBackOnFilter="true" />
                            <telerik:GridTemplateColumn HeaderText="Riesgo" DataField="descRiesgo" AutoPostBackOnFilter="true" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" FilterControlWidth="100px">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "descRiesgo")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="rddlRiesgoPro" runat="server" Filter="Contains" DropDownAutoWidth="Enabled" EmptyMessage="">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemStyle Width="40px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Habilitado" DataField="descEstado" AutoPostBackOnFilter="true" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" FilterControlWidth="100px">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "descEstado")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="rddlEstados" runat="server" Filter="Contains" DropDownAutoWidth="Enabled">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemStyle Width="40px" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-md-6 form-group">
                <h3  class="encabezado">Componentes guia</h3>
                <asp:Panel runat="server" ID="panelGrillaTipoGuia">
                    <telerik:RadGrid ID="RadGridTipoGuia" runat="server" ShowDesignTimeSmartTagMessage="true"
                        OnNeedDataSource="RadGridTipoGuia_NeedDataSource"
                        OnInsertCommand="RadGridTipoGuia_InsertCommand"
                        OnUpdateCommand="RadGridTipoGuia_UpdateCommand"
                        OnItemCommand="RadGridTipoGuia_ItemCommand"
                        OnPreRender="RadGridTipoGuia_PreRender"
                        OnItemDataBound="RadGridTipoGuia_ItemDataBound"
                        AutoGenerateColumns="false">
                        <GroupingSettings CaseSensitive="false" />
                        <ClientSettings EnablePostBackOnRowClick="true">
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace"
                            CommandItemSettings-AddNewRecordText="Agregar"
                            CommandItemSettings-ShowAddNewRecordButton="true"
                            CommandItemSettings-ShowRefreshButton="false"
                            DataKeyNames="idTipoGuia,idEstado,descripcion,createdBy,createdDate">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                    HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderText="Componente Guía" DataField="descripcion" AutoPostBackOnFilter="true"></telerik:GridBoundColumn>
                                <telerik:GridCheckBoxColumn HeaderText="¿Ponderado por grupo?" DataField="esPonderadoPorGrupo" AutoPostBackOnFilter="true"></telerik:GridCheckBoxColumn>
                                <telerik:GridNumericColumn HeaderText="Ponderador" DataField="ponderadorGrupo" KeepNotRoundedValue="true" AutoPostBackOnFilter="true"></telerik:GridNumericColumn>
                                <telerik:GridTemplateColumn HeaderText="Habilitado" DataField="nombreEstado" AutoPostBackOnFilter="true" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px" FilterControlWidth="50px">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "nombreEstado")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="rddlEstados" runat="server" Filter="Contains" DropDownAutoWidth="Enabled">
                                        </telerik:RadComboBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="40px" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>                
            </div>            
        </div>
        <div class="row container-fluid">            
            <asp:Panel runat="server" ID="panelDiagnosticos" Visible="false">
                <h3 class="encabezado">Ingreso de información para la guía</h3>
                <asp:Panel runat="server" ID="panelRiesgo" Visible="false">
                <div class="col-sm-6 col-md-3">
                    <asp:Label runat="server" Text="Riesgo" ID="lblRiesgo" CssClass="lblTexto"></asp:Label>
                    <telerik:RadComboBox ID="cboRiesgo" runat="server" Filter="Contains" DropDownAutoWidth="Enabled" Width="100%" EnableLoadOnDemand="true"
                        EmptyMessage="Seleccione el riesgo" OnItemsRequested="cboRiesgo_ItemsRequested" LoadingMessage="Cargando Riesgos">
                    </telerik:RadComboBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label runat="server" Text="Frecuencia anual" ID="lblFrecuencia" CssClass="lblTexto"></asp:Label>
                    <div style="padding-top: 10px">
                        <input type="text" runat="server" class="form-control" value="" id="txtCantidadRiesgo" name="txtCantidadRiesgo"
                            enableviewstate="false" onkeydown="validateNumber(event);" />
                    </div>
                </div>
            </asp:Panel>
                <div class="col-md-6 pull-right">
                    <asp:Label runat="server" Text="Digite para buscar" ID="lblDiagnostico" CssClass="lblTexto"></asp:Label>
                    <telerik:RadComboBox ID="cboDiagnosticos" runat="server" Filter="Contains" DropDownAutoWidth="Enabled" Width="100%" EnableLoadOnDemand="true"
                        HighlightTemplatedItems="true" DropDownWidth="700px" EmptyMessage="Digite para buscar" OnItemDataBound="cboDiagnosticos_ItemDataBound"
                        OnItemsRequested="cboDiagnosticos_ItemsRequested" LoadingMessage="Cargando..." OnSelectedIndexChanged="cboDiagnosticos_SelectedIndexChanged"
                        AutoPostBack="true">
                    </telerik:RadComboBox>
                    <asp:HiddenField ID="hfApertura" runat="server" />
                    <div style="padding-top: 10px">
                        <asp:Button runat="server" ID="btnAgregarDiagnostico" OnClick="btnAgregarDiagnostico_Click" CssClass="btn btn-lg btn-primary btn-block"
                            Text="Agregar" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <div class="col-md-12 form-group">
                <h3  class="encabezado">Guias</h3>
                <asp:Panel runat="server" ID="pnlGrillaGuia">
                    <telerik:RadGrid ID="RadGridGuia" runat="server"
                        OnNeedDataSource="RadGridGuia_NeedDataSource"
                        OnPreRender="RadGridGuia_PreRender"
                        OnInsertCommand="RadGridGuia_InsertCommand"
                        OnUpdateCommand="RadGridGuia_UpdateCommand"
                        OnItemCommand="RadGridGuia_ItemCommand"
                        OnItemDataBound="RadGridGuia_ItemDataBound"
                        AutoGenerateColumns="false">
                        <GroupingSettings CaseSensitive="false" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <Scrolling AllowScroll="true" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" EditMode="InPlace"
                            CommandItemStyle-Height="1"
                            CommandItemSettings-AddNewRecordText="Agregar"
                            CommandItemSettings-ShowAddNewRecordButton="true"
                            CommandItemSettings-ShowRefreshButton="false"
                            DataKeyNames="idGuia,idTipoGuia,idEstado,idCodigoTipo,idRiesgo,descripcion,createdBy,createdDate,ce_titulo,ce_url,ce_estadoDoc,ce_fechaRegistro">
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                    HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn HeaderText="Código" DataField="idCodigoTipo" UniqueName="idCodigoTipo" AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="100px" ></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Guía" DataField="descripcion" UniqueName="descripcion" AutoPostBackOnFilter="true" ReadOnly="true" HeaderStyle-Width="400px" ItemStyle-Width="400px" FooterStyle-Width="400px" FilterControlWidth="100px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Riesgo" DataField="sm_Riesgo.nombre" UniqueName="sm_Riesgo.nombre" AutoPostBackOnFilter="true" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px" FilterControlWidth="50px">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "sm_Riesgo.nombre")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadComboBox ID="rddlRiesgos" runat="server" Filter="Contains" DropDownAutoWidth="Enabled">
                                    </telerik:RadComboBox>
                                </EditItemTemplate>
                                <ItemStyle Width="40px" />
                            </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="Frecuencia" DataField="cantidadRiesgo" UniqueName="cantidadRiesgo" AutoPostBackOnFilter="true"  HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Habilitado" DataField="sm_Estado.nombre" UniqueName="sm_Estado.nombre" AutoPostBackOnFilter="true" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px" FilterControlWidth="50px">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "sm_Estado.nombre")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="rddlEstados" runat="server" Filter="Contains" DropDownAutoWidth="Enabled">
                                        </telerik:RadComboBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="40px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridNumericColumn HeaderText="Peso" DataField="peso" AutoPostBackOnFilter="true" UniqueName="peso" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px"/>
                                <%-- CAMPOS DE EDUCACION Y HABITOS SALUDABLES --%>
                                <telerik:GridBoundColumn HeaderText="Título" DataField="ce_titulo" UniqueName="ce_titulo" AutoPostBackOnFilter="true"  HeaderStyle-Width="350px" ItemStyle-Width="350px" FooterStyle-Width="350px"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="URL" DataField="ce_url" UniqueName="ce_url" AutoPostBackOnFilter="true"  HeaderStyle-Width="750px" ItemStyle-Width="750px" FooterStyle-Width="750px"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Habilitado" DataField="descripcion" UniqueName="ce_estadoDoc" AutoPostBackOnFilter="true" HeaderStyle-Width="40px" ItemStyle-Width="40px" FooterStyle-Width="40px" FilterControlWidth="50px">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "descripcion")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <telerik:RadComboBox ID="rddlEstadoDocCE" runat="server" Filter="Contains" DropDownAutoWidth="Enabled">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Vigente" Value ="1" />
                                                <telerik:RadComboBoxItem Text="No Vigente" Value ="0" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="40px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn HeaderText="Fecha de registro" DataField="ce_fechaRegistro" UniqueName="ce_fechaRegistro" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" DataFormatString="{0:dd/MM/yyyy}" EditDataFormatString="dd/MM/yyyy" />
                                <%-- FIN CAMPOS DE EDUCACION Y HABITOS SALUDABLES --%>
                                <%-- CAMPOS DE PORCENTAJES DE PONDERACION --%>                                
                                <telerik:GridNumericColumn HeaderText="Porcentaje por cada actividad" DataField="po_act" AutoPostBackOnFilter="true" UniqueName="po_act" KeepNotRoundedValue="true" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px"/>
                                <telerik:GridNumericColumn HeaderText="Porcentaje al grupo de actividades" DataField="po_grupoAct" AutoPostBackOnFilter="true" UniqueName="po_grupoAct" KeepNotRoundedValue="true" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px"/>
                                <%-- FIN CAMPOS DE PORCENTAJES DE PONDERACION --%>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>                    
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlInstrucciones">
                    <div class="text-center">
                        <div class="center-block">
                            <img class="profile-img"
                                src="../Images/IniciarSesionPredeterminado.png" alt="">
                        </div>
                        <h3>Instrucciones</h3>
                        <h4>1-Seleccione un programa</h4>
                        <h4>2-Seleccione un componente guía</h4>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function validateNumber(evt) {
                var e = evt || window.event;
                var key = e.keyCode || e.which;
                if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                    // comma, period and minus, . on keypad
                key == 190 || key == 188 || key == 109 || key == 110 ||
                    // numbers   
                key >= 48 && key <= 57 ||
                    // Numeric keypad
                key >= 96 && key <= 105 ||
                    // Backspace and Tab and Enter
                key == 8 || key == 9 || key == 13 ||
                    // Home and End
                key == 35 || key == 36 ||
                    // left and right arrows
                key == 37 || key == 39 ||
                    // Del and Ins
                key == 46 || key == 45) {
                    // input is VALID
                }
                else {
                    // input is INVALID
                    e.returnValue = false;
                    if (e.preventDefault) e.preventDefault();
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
