<%@ Page Title="Listar Tabla" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarTabla.aspx.cs" Inherits="SaludMovil.Portal.ModGeneral.ListarTabla" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="MainContent" ContentPlaceHolderID="MainContent">
    <div>
        <div class="container-fluid">
            <div class="">
                <asp:DropDownList ID="cboListaTablas" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboListaTablas_SelectedIndexChanged" 
                    Width="250px" AutoPostBack="true"></asp:DropDownList>
                <asp:Button ID="btnAgregar" runat="server" CssClass="form-control" Text="Agregar" OnClick="btnAgregar_Click" />
            </div>
            <div class="row">
                <telerik:RadGrid ID="RadGridAutomatica" 
                    runat="server"
                    AutoGenerateColumns="true"
                    AllowPaging="true"
                    AllowSorting="true">
                </telerik:RadGrid>
            </div>
        </div>
    </div>
</asp:Content>
