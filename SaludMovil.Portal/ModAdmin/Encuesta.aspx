<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encuesta.aspx.cs" Inherits="SaludMovil.Portal.ModAdmin.Encuesta" MasterPageFile="~/Site.Master"%>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <%-- ESTE CONTENT NO SE TOCA --%>
</asp:Content>

<asp:Content runat="server" ID="Encuesta" ContentPlaceHolderID="MainContent">
    <h1>Encuesta</h1>
    <div runat="server" class="panel panel-default divEncuesta">
        <div runat="server" class="panel-heading headEncuesta">
            <h3 id="Titulo" class="panel-title nombreEncuesta"><asp:Literal runat="server" ID="temaEncuesta"></asp:Literal></h3>
        </div>
        <div class="panel-body">
            <h4 class="text-danger">Pregunta obligatoria <span style="font-size:22px;">*</span></h4>
            <hr />
            <div  ID="pregunta" runat="server" oninit="preguntas_Init" >
            </div>
            <asp:Button runat="server" OnClick="EnviarEncuesta" Text="Enviar respuestas" CssClass="btn-primary btnEnviarEncuesta"/>
        </div>
        
    </div>
<%--    <div id="contenido" class="container-fluid">
        
        <asp:Button OnClick="control" runat="server"/>
        <div runat="server"  ID="pregunta" class="jumbotron jbnEncuesta" >

        </div>
    </div>--%>
</asp:Content>
 
