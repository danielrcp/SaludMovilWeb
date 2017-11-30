<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="SaludMovil.Portal.Roles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
            EnableShadow="true" Position="TopCenter" Animation="FlyIn" Title="Sistema de notificaciones">
        </telerik:RadNotification>
        <script src="Scripts/Modulos/roles.js"></script>
        <div class="form-group">
            <asp:Label ID="lblRoles" Text="Seleccione un rol" runat="server" CssClass="form-control text-center"></asp:Label>
            <asp:DropDownList ID="cboRoles" runat="server" CssClass="form-control" >
            </asp:DropDownList>            
        </div>
        <div class="form-group">
            <asp:Button runat="server" ID="btnIniciarSesion" OnClick="btnIniciarSesion_Click" CssClass="btn btn-lg btn-primary btn-block"
                Visible="true" Text="Iniciar sesión" />
        </div>
    </form>
</body>
</html>
