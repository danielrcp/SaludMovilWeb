<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vista360.aspx.cs" Inherits="SaludMovil.Portal.ModPacientes.Vista360" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="vista360" ContentPlaceHolderID="MainContent">
    <script src="../Scripts/SaludMovil.js" type="text/javascript"></script>
    <div class="container-fluid">
        <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
            EnableShadow="true" Position="TopCenter" Animation="FlyIn" Title="Sistema de notificaciones" VisibleTitlebar="false">
        </telerik:RadNotification>
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager2" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="radGridExamenesAyudas">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridExamenesAyudas" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rdpFecchaFin">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadHtmlChartTension" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkReseat">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadHtmlChartTension" />
                        <telerik:AjaxUpdatedControl ControlID="rdpFechaInicio" />
                        <telerik:AjaxUpdatedControl ControlID="rdpFecchaFin" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cboHoraTension">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadHtmlChartTension" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="rdpFechaFinGlucosa">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadHtmlChartTalla" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="chkReseatGlucosa">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadHtmlChartTalla" />
                        <telerik:AjaxUpdatedControl ControlID="rdpFechaInicioGlucosa" />
                        <telerik:AjaxUpdatedControl ControlID="rdpFechaFinGlucosa" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="cboRangoGlucosa">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadHtmlChartTalla" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
        <div class="row" style="padding-top: 20px">
            <telerik:RadDockLayout runat="server" ID="RadDockLayout1">
                <div class="row">
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone1" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock1" Title="Hospitalizaciones" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadGrid ID="radGridHospitalizaciones"
                                        runat="server"
                                        Width="100%"
                                        Height="415px"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        AllowFilteringByColumn="false"
                                        OnNeedDataSource="radGridHospitalizaciones_NeedDataSource"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" />
                                        </ClientSettings>
                                        <MasterTableView AllowMultiColumnSorting="True" NoMasterRecordsText="No hay registros para mostrar">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="DiagnosticoPrincipal" HeaderText="DiagnosticoPrincipal" ItemStyle-Font-Size="Small" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" AutoPostBackOnFilter="true" UniqueName="DiagnosticoPrincipal" FilterControlWidth="200px" Visible="true" />
                                                <telerik:GridBoundColumn DataField="DiagnosticoSecundario" HeaderText="DiagnosticoSecundario" ItemStyle-Font-Size="Small" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" AutoPostBackOnFilter="true" UniqueName="DiagnosticoSecundario" FilterControlWidth="200px" />
                                                <telerik:GridBoundColumn DataField="DiasHospitalizacion" HeaderText="DiasHospitalizacion" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="DiasHospitalizacion" FilterControlWidth="50px" />
                                                <telerik:GridBoundColumn DataField="NombreIps" HeaderText="NombreIps" ItemStyle-Font-Size="Small" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="100px" AutoPostBackOnFilter="true" UniqueName="NombreIps" FilterControlWidth="100px" />
                                                <telerik:GridDateTimeColumn DataField="FechaIngreso" DataFormatString="{0:dd/MM/yyyy}" HeaderText="FechaIngreso" ItemStyle-Font-Size="Small" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="100px" AutoPostBackOnFilter="true" UniqueName="FechaIngreso" FilterControlWidth="100px" Visible="true" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone3" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock2" Title="Ultimas tomas" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-6" style="height: 200px">
                                            <h4 style="padding-top: 20px">Riesgo</h4>
                                            <telerik:RadRadialGauge ID="RadRadialGaugeRiesgo" Width="90%" Height="50%" runat="server">
                                            </telerik:RadRadialGauge>
                                        </div>
                                        <div class="col-md-6" style="height: 200px">
                                            <h4 style="padding-top: 20px">Glucometría</h4>
                                            <telerik:RadRadialGauge ID="RadLinealGaugeGlucometria" Width="100%" Height="50%" runat="server">
                                            </telerik:RadRadialGauge>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblFechaGlucosa" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" style="height: 200px">
                                            <h4 style="padding-top: 20px">Presión sistólica</h4>
                                            <telerik:RadRadialGauge ID="radialGauge" Width="100%" Height="70%" runat="server">
                                            </telerik:RadRadialGauge>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblSistolica" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" style="height: 200px">
                                            <h4 style="padding-top: 20px">Presión diastólica</h4>
                                            <telerik:RadRadialGauge ID="RadRadialGaugeTensionDiastolica" Width="100%" Height="70%" runat="server">
                                            </telerik:RadRadialGauge>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblDiastolica" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone4" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock4" Title="Datos generales" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <div class="col-md-6" style="margin-top: 50px; height: 350px">
                                        <img src="../Images/humano.png" width="160" />
                                    </div>
                                    <div class="col-md-6" style="margin-top: 50px; height: 350px">
                                        <asp:Label ID="lblNombres" Text="" runat="server" Width="100%" Font-Size="20px"> </asp:Label>
                                        <asp:Label ID="lblApellidos" Text="" runat="server" Width="100%" Font-Size="20px"> </asp:Label>
                                        <asp:Label ID="lblEdad" Text="" runat="server" Width="300px" Font-Size="15px"> </asp:Label>
                                        <asp:Label ID="lblCelular" Text="" runat="server" Width="300px" Font-Size="15px"> </asp:Label>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone2" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock3" Title="Diagnósticos" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadGrid ID="radGridDiagnosticos"
                                        runat="server"
                                        Width="300px"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        AllowFilteringByColumn="false"
                                        OnNeedDataSource="radGridDiagnosticos_NeedDataSource"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" />
                                        </ClientSettings>
                                        <MasterTableView AllowMultiColumnSorting="True" NoMasterRecordsText="No hay registros para mostrar">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" HeaderText="ID" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="id" FilterControlWidth="30px" Visible="false" />
                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="200px" />
                                                <telerik:GridDateTimeColumn DataField="FechaRegistro" HeaderText="FechaRegistro" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="100px" FooterStyle-Width="200px" AutoPostBackOnFilter="true" UniqueName="FechaRegistro" FilterControlWidth="100px" Visible="false" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone5" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock5" Title="Exámenes" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadGrid ID="radGridExamenesAyudas"
                                        runat="server"
                                        Width="300px"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        OnNeedDataSource="radGridExamenesAyudas_NeedDataSource"
                                        OnDetailTableDataBind="radGridExamenesAyudas_DetailTableDataBind"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" />
                                        </ClientSettings>
                                        <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="Codigo" NoMasterRecordsText="No hay registros para mostrar"
                                            EditFormSettings-EditColumn-CancelText="Cancelar"
                                            EditFormSettings-EditColumn-InsertText="Insertar"
                                            EditFormSettings-EditColumn-UpdateText="Actualizar"
                                            EditFormSettings-EditColumn-EditText="Editar">
                                            <DetailTables>
                                                <telerik:GridTableView DataKeyNames="id" Name="DetalleExamenes"
                                                    Width="100%"
                                                    NoMasterRecordsText="No hay registros para mostrar." NoDetailRecordsText="No hay registros para mostrar."
                                                    EditFormSettings-EditColumn-CancelText="Cancelar"
                                                    EditFormSettings-EditColumn-InsertText="Insertar"
                                                    EditFormSettings-EditColumn-UpdateText="Actualizar"
                                                    EditFormSettings-EditColumn-EditText="Editar">
                                                    <ColumnGroups>
                                                        <telerik:GridColumnGroup Name="DetalleExamenes" HeaderText="Detalle registros exámenes" HeaderStyle-Font-Size="Larger"
                                                            HeaderStyle-HorizontalAlign="Center" />
                                                    </ColumnGroups>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="id" HeaderText="ID" ItemStyle-Font-Size="Small" HeaderStyle-Width="50px" ItemStyle-Width="50px" FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="id" FilterControlWidth="30px" ReadOnly="true" Visible="false" ColumnGroupName="DetalleExamenes" />
                                                        <telerik:GridBoundColumn DataField="Codigo" HeaderText="Código" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Codigo" FilterControlWidth="80px" ReadOnly="true" Visible="false" ColumnGroupName="DetalleExamenes" />
                                                        <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="250px" ItemStyle-Width="250px" FooterStyle-Width="250px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="250px" ReadOnly="true" ColumnGroupName="DetalleExamenes" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="valor3" HeaderText="Resultado" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="valor1" FilterControlWidth="80px" ColumnGroupName="DetalleExamenes" />
                                                        <telerik:GridDateTimeColumn DataField="fechaEvento" HeaderText="FechaRegistro" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px" ItemStyle-Width="80px" FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="FechaRegistro" FilterControlWidth="80px" DataFormatString="{0:M/d/yyyy}" ColumnGroupName="DetalleExamenes" />
                                                    </Columns>
                                                </telerik:GridTableView>
                                            </DetailTables>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="250px" ItemStyle-Width="250px" FooterStyle-Width="250px" AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="250px" ReadOnly="true" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone6" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock6" Title="Medicamentos" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadGrid
                                        ID="radGridMedicamentos"
                                        runat="server"
                                        RegisterWithScriptManager="false"
                                        ValidationSettings-EnableValidation="false"
                                        OnNeedDataSource="radGridMedicamentos_NeedDataSource"
                                        ShowStatusBar="true"
                                        Width="100%"
                                        AllowSorting="True"
                                        AllowPaging="True"
                                        AutoGenerateColumns="false">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" />
                                        </ClientSettings>
                                        <MasterTableView
                                            DataKeyNames="idGuiaPaciente" NoMasterRecordsText="No hay registros para mostrar"
                                            ShowFooter="false"
                                            CommandItemStyle-HorizontalAlign="Right"
                                            HorizontalAlign="Center">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="txt1" HeaderText="Descripción" ItemStyle-Font-Size="Small" HeaderStyle-Width="150px"
                                                    ItemStyle-Width="150px" FooterStyle-Width="150px"
                                                    AutoPostBackOnFilter="true" UniqueName="Descripcion" FilterControlWidth="150px" />
                                                <telerik:GridTemplateColumn HeaderText="Forma" ItemStyle-Width="80px" FilterControlWidth="80px" FooterStyle-Width="80px" HeaderStyle-Width="80px" Visible="true">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container.DataItem, "Forma")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadComboBox RenderMode="Lightweight" runat="server" ID="cboForma" SelectedValue='<%#Bind("txt4") %>'>
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="0" Text="Seleccione.." />
                                                                <telerik:RadComboBoxItem Value="1" Text="Capsulas" />
                                                                <telerik:RadComboBoxItem Value="2" Text="Tabletas" />
                                                                <telerik:RadComboBoxItem Value="3" Text="Gotas" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridNumericColumn DecimalDigits="0" HeaderText="Posología" DataField="valor4" AutoPostBackOnFilter="true" ItemStyle-Font-Size="Small" HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px" UniqueName="valor4" FilterControlWidth="20px" Visible="true" />
                                                <telerik:GridBoundColumn DataField="observaciones" HeaderText="Recomendaciones" ItemStyle-Font-Size="Small" HeaderStyle-Width="80px"
                                                    ItemStyle-Width="80px" FooterStyle-Width="80px"
                                                    AutoPostBackOnFilter="true" UniqueName="observaciones" FilterControlWidth="80px" Visible="false" />
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone7" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock7" Title="Tensión" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlChartTension" Width="100%" Height="300" Transitions="true">
                                        <PlotArea>
                                            <Series>
                                                <telerik:LineSeries DataFieldY="valor1" Name="Sistólica">
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="Blue" />
                                                    </Appearance>
                                                    <LabelsAppearance DataFormatString="{0}">
                                                    </LabelsAppearance>
                                                    <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                                </telerik:LineSeries>
                                                <telerik:LineSeries DataFieldY="valor2" Name="Diastólica">
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="Red" />
                                                    </Appearance>
                                                    <LabelsAppearance DataFormatString="{0}">
                                                    </LabelsAppearance>
                                                    <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                                </telerik:LineSeries>
                                            </Series>
                                        </PlotArea>
                                        <Legend>
                                            <Appearance Position="Bottom" />
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <telerik:RadDatePicker ID="rdpFechaInicio" runat="server" Width="100%"></telerik:RadDatePicker>
                                        </div>
                                        <div class="col-md-6">
                                            <telerik:RadDatePicker ID="rdpFecchaFin" runat="server" OnSelectedDateChanged="rdpFecchaFin_SelectedDateChanged" AutoPostBack="true" Width="100%"></telerik:RadDatePicker>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <telerik:RadComboBox ID="cboHoraTension" runat="server" Width="80%" OnSelectedIndexChanged="cboHoraTension_SelectedIndexChanged"
                                                DropDownAutoWidth="Enabled" AutoPostBack="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="4" Text="Todos" />
                                                    <telerik:RadComboBoxItem Value="1" Text="Mañana" />
                                                    <telerik:RadComboBoxItem Value="2" Text="Tarde" />
                                                    <telerik:RadComboBoxItem Value="3" Text="Noche" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="col-md-6">
                                            <telerik:RadCheckBox ID="chkReseat" runat="server" OnCheckedChanged="chkReseat_CheckedChanged" Text="Actualizar" Width="100%" AutoPostBack="true"></telerik:RadCheckBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone8" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock8" Title="Glucometría" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlChartTalla" Width="300" Height="300" Transitions="true">
                                        <PlotArea>
                                            <Series>
                                                <telerik:LineSeries DataFieldY="valor1" Name="Glucometría">
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="YellowGreen" />
                                                    </Appearance>
                                                    <LabelsAppearance DataFormatString="{0:0.00}">
                                                    </LabelsAppearance>
                                                    <TooltipsAppearance DataFormatString="{0:0.00}"></TooltipsAppearance>
                                                </telerik:LineSeries>
                                            </Series>
                                            <YAxis>
                                            </YAxis>
                                        </PlotArea>
                                        <Legend>
                                            <Appearance Position="Bottom" />
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <telerik:RadDatePicker ID="rdpFechaInicioGlucosa" runat="server" Width="100%"></telerik:RadDatePicker>
                                        </div>
                                        <div class="col-md-6">
                                            <telerik:RadDatePicker ID="rdpFechaFinGlucosa" runat="server" OnSelectedDateChanged="rdpFechaFinGlucosa_SelectedDateChanged"
                                                AutoPostBack="true" Width="100%">
                                            </telerik:RadDatePicker>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <telerik:RadComboBox ID="cboRangoGlucosa" runat="server" Width="80%" OnSelectedIndexChanged="cboRangoGlucosa_SelectedIndexChanged"
                                                DropDownAutoWidth="Enabled" AutoPostBack="true">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="4" Text="Todos" />
                                                    <telerik:RadComboBoxItem Value="1" Text="Mañana" />
                                                    <telerik:RadComboBoxItem Value="2" Text="Tarde" />
                                                    <telerik:RadComboBoxItem Value="3" Text="Noche" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </div>
                                        <div class="col-md-6">
                                            <telerik:RadCheckBox ID="chkReseatGlucosa" runat="server" OnCheckedChanged="chkReseatGlucosa_CheckedChanged" Text="Actualizar" Width="100%" AutoPostBack="true"></telerik:RadCheckBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone9" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock9" Title="Indice de masa corporal" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadHtmlChart runat="server" ID="RadHtmlChartPeso" Width="300" Height="377" Transitions="true">
                                        <PlotArea>
                                            <Series>
                                                <telerik:LineSeries DataFieldY="Imc" Name="Imc">
                                                    <Appearance>
                                                        <FillStyle BackgroundColor="Green" />
                                                    </Appearance>
                                                    <LabelsAppearance DataFormatString="{0}">
                                                    </LabelsAppearance>
                                                    <TooltipsAppearance DataFormatString="{0}"></TooltipsAppearance>
                                                </telerik:LineSeries>
                                            </Series>
                                            <XAxis DataLabelsField="fechaEventos">
                                                <LabelsAppearance RotationAngle="90">
                                                </LabelsAppearance>
                                            </XAxis>
                                            <YAxis>
                                            </YAxis>
                                        </PlotArea>
                                        <Legend>
                                            <Appearance Position="Bottom" />
                                        </Legend>
                                    </telerik:RadHtmlChart>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <telerik:RadDockZone runat="server" ID="RadDockZone10" Orientation="Vertical" MinHeight="300px" Height="100%"
                            Width="100%">
                            <telerik:RadDock RenderMode="Lightweight" ID="RadDock10" Title="Calendario" runat="server" Width="230px" DockMode="Default" Resizable="true"
                                CssClass="higherZIndex">
                                <ContentTemplate>
                                    <telerik:RadScheduler
                                        runat="server"
                                        ID="RadScheduler1"
                                        Culture="es-ES"
                                        Width="100%"
                                        AllowEdit="false"
                                        AllowInsert="false"
                                        SelectedView="MonthView"
                                        OnAppointmentDataBound="RadScheduler1_AppointmentDataBound"
                                        MonthView-AdaptiveRowHeight="true"
                                        RowHeight="100px"
                                        AgendaView-DateColumnWidth="150px"
                                        Height="450px"
                                        DataEndField="fechaEvento"
                                        DataStartField="fechaEvento"
                                        DataKeyField="idGuiaPaciente"
                                        DataDescriptionField="descripcion"
                                        DataSubjectField="descripcion"
                                        Localization-AdvancedCalendarToday="Hoy" Localization-AdvancedAllDayEvent="Todos los dias"
                                        Localization-AdvancedCalendarCancel="Cancelar" Localization-AdvancedDay="Día"
                                        Localization-AdvancedDays="Días" Localization-AdvancedDescription="Descripción"
                                        Localization-AdvancedMonths="Meses" Localization-HeaderDay="Día" Localization-HeaderMonth="Mes"
                                        Localization-HeaderNextDay="Día Siguiente" Localization-HeaderWeek="Semana"
                                        Localization-ReminderDay="Día" Localization-ReminderDays="Días" Localization-ReminderHour="Hora"
                                        Localization-ReminderHours="Horas" Localization-ReminderMinute="minuto" Localization-ReminderMinutes="minutos"
                                        Localization-ReminderWeek="Semana" Localization-ReminderWeeks="Semanas" Localization-AdvancedInvalidNumber="Nune"
                                        Localization-AllDay="Todo el día" Localization-Cancel="Cancelar" Localization-ConfirmCancel="Cancelar"
                                        Localization-Save="Guardar" Localization-Show24Hours="Ver 24 horas...">
                                        <DayView ReadOnly="True" />
                                        <AdvancedForm Modal="true" EnableResourceEditing="false" />
                                        <TimelineView UserSelectable="false" />
                                        <TimeSlotContextMenuSettings EnableDefault="true" />
                                        <AppointmentContextMenuSettings EnableDefault="true" />
                                    </telerik:RadScheduler>
                                </ContentTemplate>
                            </telerik:RadDock>
                        </telerik:RadDockZone>
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </telerik:RadDockLayout>
        </div>
    </div>
</asp:Content>
