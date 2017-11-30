<%@ Page Title="Cambio contrasena" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmControlMenu.aspx.cs" Inherits="SaludMovil.Portal.ModGeneral.frmControlMenu" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="ControlMenu" ContentPlaceHolderID="MainContent">
    <h6 class="text-danger" id="hRuta" >ADMINISTRACIÓN / OPCIONES DE MENÚ</h6>
    <div class="container-fluid">
        <h3 class="encabezado">Opciones de menú</h3>
        <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
            EnableShadow="true" Position="Center" Animation="FlyIn" Title="Sistema de notificaciones" VisibleTitlebar="false">
        </telerik:RadNotification>
        <div class="row" style="width:100%">
            <div class="col-lg-4 col-lg-offset-4">
                <asp:Label ID="lblRoles" Text="Roles del sistema" runat="server" Width="100%" CssClass="lblTexto" />
                <telerik:RadComboBox ID="cboRoles" runat="server" Filter="Contains" DropDownAutoWidth="Enabled" Width="100%"
                    HighlightTemplatedItems="true" EmptyMessage="Digite para buscar" LoadingMessage="Cargando..."
                    OnSelectedIndexChanged="cboRoles_SelectedIndexChanged"
                    AutoPostBack="true">
                </telerik:RadComboBox>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <asp:TreeView
                        ID="tvMenuCompleto"
                        runat="server"
                        ShowCheckBoxes="All"
                        ShowLines="True"
                        Width="100%">
                    </asp:TreeView>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-lg-offset-4">
                    <asp:Button ID="btnUpdate" runat="server" Text="Guardar cambios" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnUpdate_Click" /><br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
