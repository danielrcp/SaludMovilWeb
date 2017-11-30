<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlDinamico.ascx.cs" Inherits="SaludMovil.Portal.ModGeneral.ControlDinamico" %>

<div>
    <div class="container-fluid">
        <h3 class="jumbotron">
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
            <img src="../../images/icons/registro.gif" /></h3>
        <div class="">
            <asp:ValidationSummary
                ID="vsAllFields"
                runat="server"
                HeaderText=""
                CssClass="validationSummary"
                ShowSummary="false"
                ShowMessageBox="true"></asp:ValidationSummary>
            <asp:Button ID="btnAgregar" runat="server" CssClass="btn" Text="Agregar" />
        </div>
        <div class="row">
            <asp:PlaceHolder ID="placeHolderControl" runat="server"></asp:PlaceHolder>
        </div>
        <div class="btn-block text-center">
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn" Text="Guardar" OnClick="btnGuardar_Click"></asp:Button>
            <asp:Button ID="btnVolver" runat="server" CssClass="btn" Text="Volver" CausesValidation="False"></asp:Button>
        </div>
    </div>
</div>
