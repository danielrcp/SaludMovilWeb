<%@ Page Title="Pagina principal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SaludMovil.Portal._Default" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="PaginaPrincipal" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        /*Se adiciona excepción en los estilos boostrap para ampliar el tamaño de la bandeja principal pacientes*/
        .container {
            width: 100% !Important;
        }
    </style>
    <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
        EnableShadow="true" Position="TopCenter" Animation="FlyIn" Title="Sistema de notificaciones" VisibleTitlebar="false">
    </telerik:RadNotification>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnVerMas">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radGridPaciente" />
                </UpdatedControls>
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnVerMas" />
                </UpdatedControls>
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="hdfValidador" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
    <h6 class="text-danger" id="hRuta">
        <asp:Label ID="lblLeyenda" runat="server"></asp:Label>
    </h6>
    <asp:HiddenField ID="hdfValidador" runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-3">
                    <asp:Label ID="lblTipoIdentificacion" Text="Tipo de identificación" runat="server" Width="500px" CssClass="lblTexto">
                    </asp:Label>
                    <asp:DropDownList ID="cboTipoIdentificacion" runat="server" Width="100%" CssClass="form-control" AppendDataBoundItems="true">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblNumeroIdentificacion" Text="Número de identificación" runat="server" Width="500px" CssClass="lblTexto">
                    </asp:Label>
                    <asp:TextBox runat="server" ID="txtIdentificacion" Width="100%" CssClass="form-control" />
                </div>
                <div class="col-md-3">
                </div>
            </div>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <asp:Button Text="Buscar" runat="server" Visible="true" ID="btnBuscar" OnClick="btnBuscar_Click" Width="100%" CssClass="btn-block btn-primary" />
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <h3 class="encabezado" style="padding-top: 5px">Bandeja de pacientes</h3>
            <div class="row" style="padding-top: 5px">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <asp:Button Text="Ver más información" runat="server" Visible="true" ID="btnVerMas" OnClick="btnVerMas_Click" Width="100%" CssClass="btn-block btn-link" />
                </div>
                <div class="col-md-4"></div>
            </div>
            <telerik:RadGrid ID="radGridPaciente"
                runat="server"
                OnNeedDataSource="radGridPaciente_NeedDataSource"
                OnItemDataBound="radGridPaciente_ItemDataBound"
                OnItemCommand="radGridPaciente_ItemCommand"
                Height="600px"
                AllowSorting="True" Width="100%"
                AllowPaging="True"
                AllowFilteringByColumn="True" GroupingSettings-CaseSensitive="false"
                AutoGenerateColumns="false">
                <ClientSettings>
                    <Scrolling AllowScroll="true" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True"
                    DataKeyNames="idTipoIdentificacion,NumeroIdentifacion,riesgo,limiteSuperiorSistolica,limiteInferiorSistolica,limiteSuperiorDiastolica,
                        limiteInferiorDiastolica,limiteSuperiorGlucosa,limiteInferiorGlucosa,Glucosa,Sistolica,Diastolica,FechaGlucosa,FechaSistolica,FechaDiastolica"
                    EditFormSettings-EditColumn-AutoPostBackOnFilter="true"
                    ShowFooter="true"
                    CommandItemStyle-HorizontalAlign="Right"
                    EditMode="InPlace"
                    HorizontalAlign="Center"
                    Width="100%"
                    CommandItemSettings-AddNewRecordText="Agregar"
                    CommandItemSettings-RefreshText="Actualizar"
                    NoDetailRecordsText="No hay registros para mostrar."
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <Columns>
                        <telerik:GridButtonColumn HeaderStyle-Width="10px"
                            UniqueName="btnBuscar"
                            ItemStyle-Width="10px"
                            ItemStyle-Height="10px"
                            HeaderTooltip="Consultar"
                            HeaderStyle-Height="10px"
                            FooterStyle-Height="10px"
                            ImageUrl="Images/buscar.png"
                            ButtonType="ImageButton" Resizable="true"
                            CommandName="Buscar">
                            <ItemStyle Width="10px" Height="10px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn
                            HeaderStyle-Width="10px" UniqueName="btn360"
                            ItemStyle-Width="10px"
                            ItemStyle-Height="10px" HeaderTooltip="Vista 360"
                            HeaderStyle-Height="10px"
                            FooterStyle-Height="10px"
                            ImageUrl="Images/grafica.png"
                            ButtonType="ImageButton"
                            CommandName="360">
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn
                            DataField="TipoIdentificacion"
                            HeaderText="Tipo" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="20px"
                            ItemStyle-Width="20px"
                            FooterStyle-Width="20px" AutoPostBackOnFilter="true" UniqueName="TipoIdentificacion"
                            FilterControlWidth="20px" />
                        <telerik:GridBoundColumn DataField="NumeroIdentifacion" HeaderText="Identificación" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="50px"
                            ItemStyle-Width="50px"
                            FooterStyle-Width="50px" AutoPostBackOnFilter="true" UniqueName="NumeroIdentifacion"
                            FilterControlWidth="50px" />
                        <telerik:GridBoundColumn DataField="Nombres" HeaderText="Nombres" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="80px"
                            ItemStyle-Width="80px"
                            FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Nombres"
                            FilterControlWidth="80px" />
                        <telerik:GridBoundColumn DataField="Apellidos" HeaderText="Apellidos" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="80px"
                            ItemStyle-Width="80px"
                            FooterStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="Apellidos"
                            FilterControlWidth="80px" />
                        <telerik:GridDateTimeColumn DataField="FechaRegistro" HeaderText="FechaIngreso" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="70px"
                            ItemStyle-Width="70px"
                            FooterStyle-Width="70px"
                            AutoPostBackOnFilter="true"
                            UniqueName="FechaRegistro"
                            EnableTimeIndependentFiltering="true"
                            FilterDateFormat="dd/MM/yyyy"
                            DataFormatString="{0:dd/MM/yyyy}"
                            FilterControlWidth="70px" />
                        <telerik:GridBoundColumn DataField="medicoTratante" HeaderText="Medico Tratante" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="70px"
                            ItemStyle-Width="70px"
                            FooterStyle-Width="70px" AutoPostBackOnFilter="true" UniqueName="medicoTratante"
                            FilterControlWidth="70px" />
                        <telerik:GridBoundColumn DataField="institucion" HeaderText="IPS" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="70px"
                            ItemStyle-Width="60px"
                            FooterStyle-Width="90px" AutoPostBackOnFilter="true" UniqueName="institucion"
                            FilterControlWidth="90px" />
                        <telerik:GridDateTimeColumn DataField="UltimoControl" HeaderText="Fecha control" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="70px"
                            ItemStyle-Width="70px"
                            FooterStyle-Width="70px"
                            AutoPostBackOnFilter="true"
                            UniqueName="UltimoControl"
                            EnableTimeIndependentFiltering="true"
                            FilterDateFormat="dd/MM/yyyy"
                            DataFormatString="{0:dd/MM/yyyy}"
                            FilterControlWidth="70px" />
                        <telerik:GridBoundColumn DataField="TipoControl"
                            HeaderText="Tipo control"
                            ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="60px"
                            ItemStyle-Width="60px"
                            FooterStyle-Width="60px"
                            AutoPostBackOnFilter="true"
                            UniqueName="TipoControl"
                            FilterControlWidth="60px" />
                        <telerik:GridDateTimeColumn DataField="RegistroControl" HeaderText="RegistroControl" ItemStyle-Font-Size="Small"
                            HeaderStyle-Width="70px"
                            ItemStyle-Width="70px"
                            FooterStyle-Width="70px"
                            AutoPostBackOnFilter="true"
                            UniqueName="RegistroControl"
                            EnableTimeIndependentFiltering="true"
                            FilterDateFormat="dd/MM/yyyy"
                            DataFormatString="{0:dd/MM/yyyy}"
                            FilterControlWidth="70px" />
                        <telerik:GridTemplateColumn UniqueName="ChartRiesgo" AllowFiltering="false" HeaderText="Riesgo">
                            <ItemTemplate>
                                <telerik:RadRadialGauge ID="RadRadialGaugeRiesgo" Width="70" Height="70" runat="server">
                                </telerik:RadRadialGauge>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="ChartGlucometria" AllowFiltering="false" HeaderText="Glucosa">
                            <ItemTemplate>
                                <telerik:RadRadialGauge ID="RadLinealGaugeGlucometria" Width="55" Height="55" runat="server">
                                </telerik:RadRadialGauge>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="ChartTension" AllowFiltering="false" HeaderText="Sistólica">
                            <ItemTemplate>
                                <telerik:RadRadialGauge ID="RadRadialGaugeTension" Width="55" Height="55" runat="server">
                                </telerik:RadRadialGauge>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="ChartTensionDiastolica" AllowFiltering="false" HeaderText="Diastólica">
                            <ItemTemplate>
                                <telerik:RadRadialGauge ID="RadRadialGaugeTensionDiastolica" Width="55" Height="55" runat="server">
                                </telerik:RadRadialGauge>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>

