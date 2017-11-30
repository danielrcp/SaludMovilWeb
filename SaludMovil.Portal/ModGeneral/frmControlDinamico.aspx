<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="frmControlDinamico.aspx.cs" Inherits="SaludMovil.Portal.ModGeneral.frmControlDinamico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AT" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="ControlDinamico" ContentPlaceHolderID="MainContent">    
    <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
        EnableShadow="true" Position="Center" Animation="FlyIn" VisibleTitlebar="false">
    </telerik:RadNotification>
    <h6 class="text-danger" id="hRuta" >ADMINISTRACIÓN / TABLAS ADMINISTRABLES / CONTROL DINÁMICO</h6>
    <div class="container">
        <h3 class="encabezado">
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
            <img src="../Images/Editar.png" /></h3>
        <div class="">
            <asp:ValidationSummary
                ID="vsAllFields"                
                runat="server"
                HeaderText=""
                CssClass="validationSummary"
                ShowSummary="false"
                ShowMessageBox="true"></asp:ValidationSummary>
        </div>
        <asp:PlaceHolder ID="placeHolderControl" runat="server"></asp:PlaceHolder>
        <div class="btn-block text-center" style="margin-top:20px">
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-lg btn-primary btn-block" Text="Guardar" OnClick="btnGuardar_Click"></asp:Button>            
        </div>
    </div>
</asp:Content>
