<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SimpleSite.Master" CodeBehind="Iniciar.aspx.cs" Inherits="SaludMovil.Portal.ModAdmin.Iniciar" %>

<asp:Content runat="server" ContentPlaceHolderID="main">
    <div class="container" style="margin-top: 40px">
        <div class="row">
            <div class="col-sm-6 col-md-4 col-md-offset-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <strong>Inicie sesión</strong>
                    </div>
                    <div class="panel-body">
                        <form role="form" action="#" method="POST" runat="server" id="form1" style="width:350px !Important">
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
                            <fieldset>
                                <div class="row">
                                    <div class="center-block">
                                        <img class="img-responsive" src="Images/LogoOriginal.png" alt="">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 col-md-10  col-md-offset-1 ">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-user"></i>
                                                </span>
                                                <asp:TextBox ID="txtUsuario" runat="server" class="form-control" placeholder="Username" name="loginname" autofocus />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="glyphicon glyphicon-lock"></i>
                                                </span>
                                                <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password" name="password" TextMode="Password" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" class="btn btn-lg btn-primary btn-block" OnClick="btnIngresar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </form>
                    </div>
                    <%--<div class="panel-footer ">
                        No tienes una cuenta! <a href="#" onclick="">Registrate aquí </a>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
