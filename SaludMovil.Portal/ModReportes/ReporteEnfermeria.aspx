<%@ Page Title="Reporte de enfermería" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReporteEnfermeria.aspx.cs" Inherits="SaludMovil.Portal.ModReportes.ReporteEnfermeria" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="ContentReporteEnfermeria" ContentPlaceHolderID="MainContent">
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
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
    <h6 class="text-danger" id="hRuta">
        <asp:Label ID="lblLeyenda" runat="server"></asp:Label>
    </h6>
    <asp:HiddenField ID="hdfValidador" runat="server" />
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-3">
                <asp:Label Text="Fecha inicial" runat="server" Width="500px" CssClass="lblTexto">
                </asp:Label>
                <telerik:RadDatePicker ID="rdpFechaInicio" runat="server" Width="250px" SkipMinMaxDateValidationOnServer="true" MinDate="01/01/1900">
                </telerik:RadDatePicker>
            </div>
            <div class="col-md-3">
                <asp:Label Text="Fecha final" runat="server" Width="500px" CssClass="lblTexto">
                </asp:Label>
                <telerik:RadDatePicker ID="rdpFechaFin" runat="server" Width="250px" SkipMinMaxDateValidationOnServer="true" MinDate="01/01/1900">
                </telerik:RadDatePicker>
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
        <div class="row">
            <div class="col-md-16">
                <h3 class="encabezado">Seguimiento</h3>
                <div class="row">
                    <div class="col-md-4"></div>
                    <div class="col-md-4">
                        <asp:Button Text="Exportar" runat="server" Visible="true" ID="btnVerMas" OnClick="btnVerMas_Click" Width="100%" CssClass="btn-block btn-link" />
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
                        limiteInferiorDiastolica,limiteSuperiorGlucosa,limiteInferiorGlucosa,Sistolica,Diastolica,FechaSistolica,FechaDiastolica"
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
                            <telerik:GridBoundColumn DataField="TipoIdentificacion" HeaderText="Tipo identificación" ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="70px"
                                ItemStyle-Width="70px"
                                FooterStyle-Width="70px" AutoPostBackOnFilter="true" UniqueName="TipoIdentificacion"
                                FilterControlWidth="30px" />
                            <telerik:GridBoundColumn DataField="NumeroIdentifacion" HeaderText="Identificación" ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="70px"
                                ItemStyle-Width="70px"
                                FooterStyle-Width="70px" AutoPostBackOnFilter="true" UniqueName="NumeroIdentifacion"
                                FilterControlWidth="30px" />
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
                            <telerik:GridBoundColumn DataField="correo"
                                HeaderText="Correo"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="correo"
                                FilterControlWidth="60px" />
                            <telerik:GridBoundColumn DataField="ciudad"
                                HeaderText="Ciudad"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="ciudad"
                                FilterControlWidth="60px" />
                            <telerik:GridBoundColumn DataField="segmento"
                                HeaderText="Segmento"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="segmento"
                                FilterControlWidth="60px" />
                            <telerik:GridBoundColumn DataField="planMp"
                                HeaderText="Plan mp"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="planMp"
                                FilterControlWidth="60px" />
                            <telerik:GridBoundColumn DataField="tipoContrato"
                                HeaderText="Tipo contrato"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="tipoContrato"
                                FilterControlWidth="60px" />
                            <telerik:GridBoundColumn DataField="programa"
                                HeaderText="Programa"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="programa"
                                FilterControlWidth="60px" />
                            <telerik:GridBoundColumn DataField="riesgodes"
                                HeaderText="Riesgo"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="riesgodes"
                                FilterControlWidth="60px" />
                            <telerik:GridDateTimeColumn DataField="FechaGlucosa" HeaderText="FechaGlucosa" ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="70px"
                                ItemStyle-Width="70px"
                                FooterStyle-Width="70px"
                                AutoPostBackOnFilter="true"
                                UniqueName="FechaGlucosa"
                                EnableTimeIndependentFiltering="true"
                                FilterDateFormat="dd/MM/yyyy"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="70px" />
                            <telerik:GridBoundColumn DataField="Glucosa"
                                HeaderText="Glucosa"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="Glucosa"
                                FilterControlWidth="60px" />
                            <telerik:GridDateTimeColumn DataField="FechaSistolica" HeaderText="FechaSistolica" ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="70px"
                                ItemStyle-Width="70px"
                                FooterStyle-Width="70px"
                                AutoPostBackOnFilter="true"
                                UniqueName="FechaSistolica"
                                EnableTimeIndependentFiltering="true"
                                FilterDateFormat="dd/MM/yyyy"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="70px" />
                            <telerik:GridBoundColumn DataField="Sistolica"
                                HeaderText="Sistolica"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="Sistolica"
                                FilterControlWidth="60px" />
                            <telerik:GridDateTimeColumn DataField="FechaDiastolica" HeaderText="FechaDiastolica" ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="70px"
                                ItemStyle-Width="70px"
                                FooterStyle-Width="70px"
                                AutoPostBackOnFilter="true"
                                UniqueName="FechaDiastolica"
                                EnableTimeIndependentFiltering="true"
                                FilterDateFormat="dd/MM/yyyy"
                                DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="70px" />
                            <telerik:GridBoundColumn DataField="Diastolica"
                                HeaderText="Diastolica"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="Diastolica"
                                FilterControlWidth="60px" />
                            <%--<telerik:GridBoundColumn DataField="Imc"
                                HeaderText="Imc"
                                ItemStyle-Font-Size="Small"
                                HeaderStyle-Width="60px"
                                ItemStyle-Width="60px"
                                FooterStyle-Width="60px"
                                AutoPostBackOnFilter="true"
                                UniqueName="Imc"
                                FilterControlWidth="60px" />--%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
</asp:Content>

