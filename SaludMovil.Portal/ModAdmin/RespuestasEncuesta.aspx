<%@ Page Title="Respuestas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RespuestasEncuesta.aspx.cs" Inherits="SaludMovil.Portal.ModAdmin.RespuestasEncuesta" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <%-- ESTE CONTENT NO SE TOCA --%>
</asp:Content>

<asp:Content runat="server" ID="DefinicionPrograma" ContentPlaceHolderID="MainContent">
    
    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
    
     <div id="contenido" class="container-fluid">
        <br />
        <br />
        <br />

        <h3 id="H1" class="encabezado" style="">Visualización de encuestas</h3>
         <asp:LinkButton ID="LinkButton1" runat="server" OnClick="atras" Visible="false" CssClass="btn btn-default btnCerrar btnCrearEncuesta" >
                    <i class="icono-sm glyphicon glyphicon-arrow-left"></i>
                     Volver
         </asp:LinkButton>
        <div id="Div1" runat="server" class="panel panel-default divEncuesta">
            <div id="Div2" runat="server" class="panel-heading headEncuesta">
                <h3 style="display: inline-block; margin-left:4px;" id="Titulo encuestaTitulo" class="panel-title nombreEncuesta"><asp:Literal runat="server" ID="temaEncuesta"></asp:Literal></h3>
            </div>
            <div class="panel-body">
                <div  ID="pregunta" runat="server" > <%--preguntas_Init  CargaEncuesta--%>
                </div>
            </div>
        </div>


        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="respuestass">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="respuestas" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div Class="encuestas" >
        <div Class="paramEncuestas" >
        <telerik:RadGrid  runat="server" OnItemCommand="Respuestas_ItemCommand" AutoPostBack="true" PageSize="10" ID="respuestas" AutoGenerateColumns="false" AllowPaging="false" OnNeedDataSource="ConsultarEncuestas" >
            <MasterTableView DataKeyNames="numeroIdentificacion" CommandItemDisplay="top" InsertItemPageIndexAction="ShowItemOnCurrentPage">
                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />
<%--                <SortExpressions>
                    <telerik:GridSortExpression FieldName="ordenPregunta" SortOrder="Ascending" />
                </SortExpressions>--%>
                <Columns>
                    <%--<telerik:GridBoundColumn DataField="idPregunta" HeaderText="ID pregunta" ReadOnly="true"
                          ForceExtractValue="Always" Display="false"/>--%>
                    <telerik:GridBoundColumn DataField="idTipoIdentificacion" HeaderText="tipoIdentificacion"  ForceExtractValue="Always" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idEncuesta" HeaderText="idEncuesta"  ForceExtractValue="Always"  Display="false">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="numeroIdentificacion" HeaderText="Identificación"  ForceExtractValue="Always">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primerNombre" HeaderText="Nombre"  ForceExtractValue="Always">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="primerApellido" HeaderText="Apellido"  ForceExtractValue="Always">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="estadoEncuesta" HeaderText="Estado"  ForceExtractValue="Always">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="fechaEstado" HeaderText="Fecha"  ForceExtractValue="Always">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="tema" HeaderText="Tema"  ForceExtractValue="Always">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn ButtonCssClass="btnCrearEncuesta" CommandName="verRespuesta" Text="ver respuestas"></telerik:GridButtonColumn>
                </Columns>
            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
            </div>
        <telerik:RadInputManager RenderMode="Lightweight" runat="server" ID="RadInputManager1" Enabled="true">
            <telerik:TextBoxSetting BehaviorID="temaConfiguracion">
            </telerik:TextBoxSetting>
            <telerik:NumericTextBoxSetting BehaviorID="NumericTextBoxSetting1" Type="Currency"
                AllowRounding="true" DecimalDigits="2">
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting BehaviorID="NumericTextBoxSetting2" Type="Number"
                AllowRounding="true" DecimalDigits="0" MinValue="0">
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>
        </div>
        
        </div>
    <br />
    <br />

</asp:Content>