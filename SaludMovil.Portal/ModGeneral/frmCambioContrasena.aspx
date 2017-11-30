<%@ Page Title="Cambio contrasena" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCambioContrasena.aspx.cs" Inherits="SaludMovil.Portal.ModGeneral.frmCambioContrasena" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="CambioContrasena" ContentPlaceHolderID="MainContent">
    <div class="container-fluid">
        <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
            EnableShadow="true" Position="TopCenter" Animation="FlyIn" Title="Sistema de notificaciones" VisibleTitlebar="false">
        </telerik:RadNotification>
        <div class="row">
            <div class="col-sm-12 col-md-10  col-md-offset-1 ">
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="glyphicon glyphicon-lock"></i>
                        </span>
                        <asp:TextBox ID="txtContNue1" runat="server" class="form-control" placeholder="Password" TextMode="Password" autofocus="true" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="glyphicon glyphicon-lock"></i>
                        </span>
                        <asp:TextBox ID="txtContNue2" runat="server" class="form-control" placeholder="Password" TextMode="Password" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" class="btn btn-lg btn-primary btn-block" OnClick="btnCambiar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
