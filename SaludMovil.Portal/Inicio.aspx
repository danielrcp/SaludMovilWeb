<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SaludMovil.Portal.Inicio" %>

<asp:Content runat="server" ID="Menu" ContentPlaceHolderID="LeftColumnContent">
    <div class="jumbotron">
        <h1>MENU</h1>
        <div id="MegaDropDown">
            <telerik:RadMenu ID="RadMenu1" runat="server" RenderMode="Lightweight">
                <Items>
                    <telerik:RadMenuItem Text="File">
                        <Items>
                            <telerik:RadMenuItem Text="New"></telerik:RadMenuItem>
                            <telerik:RadMenuItem Text="Open"></telerik:RadMenuItem>
                            <telerik:RadMenuItem Text="Save"></telerik:RadMenuItem>
                            <telerik:RadMenuItem Text="Save As..." NavigateUrl="~/ModAdmin/DefinicionPrograma.aspx">
                            </telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                    <telerik:RadMenuItem Text="File">
                        <Items>
                            <telerik:RadMenuItem Text="New"></telerik:RadMenuItem>
                            <telerik:RadMenuItem Text="Open"></telerik:RadMenuItem>
                            <telerik:RadMenuItem Text="Save"></telerik:RadMenuItem>
                            <telerik:RadMenuItem Text="Save As..."></telerik:RadMenuItem>
                        </Items>
                    </telerik:RadMenuItem>
                </Items>
            </telerik:RadMenu>
        </div>
    </div>
</asp:Content>
