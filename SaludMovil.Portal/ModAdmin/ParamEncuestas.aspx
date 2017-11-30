<%@ Page Title="Parametrizacion encuestas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParamEncuestas.aspx.cs" Inherits="SaludMovil.Portal.ModAdmin.ParamEncuestas" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <%-- ESTE CONTENT NO SE TOCA --%>
</asp:Content>

<asp:Content runat="server" ID="DefinicionPrograma" ContentPlaceHolderID="MainContent">
    <%-- SUS COSAS VAN AQUI --%>

    <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server" EnableShadow="true">
    </telerik:RadWindowManager>
     <asp:HiddenField ID="hayRespondidas" runat="server" />

    <h6 class="text-danger" id="hRuta">PARAMETRIZACION DE ENCUESTAS</h6>
    <div class="row">
            <div class="col-lg-4 col-lg-offset-4">
                <div class="form-group">
                    <asp:DropDownList ID="cboListaEncuestas" runat="server" CssClass="form-control" OnSelectedIndexChanged="SeleccionaEncuesta"
                        Width="360px" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
    </div>

    <div id="contenido" class="container-fluid">
        <h3 id="Titulo" class="encabezado" style="">Parametrización de encuestas<asp:Literal runat="server" ID="temaEncuesta"></asp:Literal></h3>
        <asp:HiddenField ID="hdfEncuestaActual" runat="server" />
        <div class="row" >
            <div class="col-md-3 temaEncuesta">
                <asp:Label ID="temaL" Text="Tema de la encuesta: " runat="server" />
                <asp:TextBox ID="tema" runat="server" CssClass="txtTema">
                </asp:TextBox>
            </div>
            <div class="col-md-3 temaEncuesta">
                <asp:Label ID="Label1" Text="Fecha inicial: " runat="server" />
                <telerik:RadDateTimePicker Width="50%" runat="server" ID="fechaInicial">
                </telerik:RadDateTimePicker>
            </div>
            <div class="col-md-3 temaEncuesta">
                <asp:Label ID="Label2" Text="Fecha final: " runat="server" />
                <telerik:RadDateTimePicker Width="50%" runat="server" ID="fechaFinal">
                </telerik:RadDateTimePicker>
            </div>
            <div class="col-md-3 temaEncuesta">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="GuardarEncuesta" CssClass="btn btn-small btn-primary btnCrearEncuesta" >
                    <i class="icono-sm glyphicon glyphicon-floppy-disk"></i>
                     Crear encuesta
                </asp:LinkButton>
            </div>
        </div>
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="preguntas">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="preguntas" />
                        <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                        <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div Class="paramEncuestas" >
        <telerik:RadGrid  runat="server" ID="preguntas" AutoGenerateColumns="false" AllowPaging="false" OnItemCommand="preguntas_ItemCommand"
            OnNeedDataSource="ConsultarPreguntas" OnDataBound="deshabilitar" OnDetailTableDataBind="ConsultaOpciones" 
            MasterTableView-CommandItemSettings-AddNewRecordText="Agregar nueva pregunta" OnRowDrop="Reordenar" AutoPostBack="true">
            <MasterTableView DataKeyNames="ordenPregunta" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage">
                <CommandItemSettings  />
                <SortExpressions>
                    <telerik:GridSortExpression FieldName="ordenPregunta" SortOrder="Ascending" />
                </SortExpressions>
                <EditFormSettings EditColumn-ButtonType="ImageButton"/>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                        HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                        <ItemStyle CssClass="MyImageButton" />
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="idPregunta" HeaderText="ID pregunta" ReadOnly="true"
                          ForceExtractValue="Always" Display="false"/>
                    <telerik:GridBoundColumn DataField="ordenPregunta" UniqueName="ordenPregunta" DataType="System.Int32" HeaderText="Orden" ReadOnly="true" />
                    <telerik:GridBoundColumn DataField="nombrePregunta" HeaderText="Enunciado" />
                    <telerik:GridTemplateColumn HeaderText="Tipo de pregunta" ItemStyle-Width="240px">
                        <ItemTemplate >
                            <asp:Label ID="TipoPBound" runat="server">
                                <%#DataBinder.Eval(Container.DataItem, "tipoPregunta")%>
                            </asp:Label>
                             <asp:HiddenField ID="hdfTipo"  Value='<%# Eval("idTipoPregunta") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <telerik:RadComboBox  EnableLoadOnDemand="true" RenderMode="Lightweight" runat="server" ID="TipoP" DataTextField="tipoPregunta" 
                                DataValueField="idTipoPregunta" OnItemsRequested="ConsultarTipos" >
                            </telerik:RadComboBox>
                        </EditItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridCheckBoxColumn DataField="esObligatoria" HeaderText="Pregunta obligatoria" />
                    <telerik:GridButtonColumn ConfirmText="¿Eliminar pregunta?"
                        ConfirmTitle="Eliminar" ButtonType="FontIconButton" UniqueName="Delete" CommandName="Delete" />
                </Columns>
                <DetailTables>
                    <telerik:GridTableView CommandItemDisplay="Top" Name="Opciones" 
                        CommandItemSettings-ShowAddNewRecordButton="true" 
                        CommandItemSettings-AddNewRecordText="Agregar opción de respuesta">
                        <EditFormSettings EditColumn-ButtonType="ImageButton"/>
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="idOpcionPregunta" HeaderText="ID Opción" ReadOnly="true"
                                ForceExtractValue="Always" ConvertEmptyStringToNull="true" Display="false" />
                            <telerik:GridBoundColumn DataField="indiceOpcion" HeaderText="Indice de opción" Display="false" ReadOnly="true" />
                            <telerik:GridBoundColumn DataField="enunciadoPregunta" headertext="Enunciado"/>
                            <telerik:GridButtonColumn ConfirmText="¿Eliminar opción?" ConfirmDialogType="RadWindow"
                                ConfirmTitle="Delete" ButtonType="FontIconButton" CommandName="Delete" />
                        </Columns>
                    </telerik:GridTableView>
                </DetailTables>
            </MasterTableView>
            <ClientSettings AllowRowsDragDrop="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
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

        <hr />
       <h3 id="H1" class="encabezado" style="">Asignar esta encuesta</h3>
        <div class="row">
            <div class="col-lg-9 col-lg-offset-2 programaEncuestas" >
                <telerik:RadListBox runat="server" RenderMode="Lightweight"  ID="programas"
                    DataKeyField="ID" DataTextField="Name" DataSortField="SortOrder" Width="340px"
                    Height="340px" AllowTransfer="true" TransferToID="asignados"
                    TransferMode="Copy" OnTransferring="AsignarEncuesta" AutoPostBackOnTransfer="true" >
                    <HeaderTemplate>
                        Programas de salud
                    </HeaderTemplate>
                </telerik:RadListBox>
                <telerik:RadListBox runat="server" RenderMode="Lightweight"  ID="asignados"
                     Width="340px"
                    Height="340px" AllowReorder="true"  TransferToID="programas" AllowDelete="True"
                    AutoPostBackOnDelete="true" TransferMode="Copy" OnDeleting="DesasignarEncuesta" AutoPostBackOnTransfer="true" >
                    <HeaderTemplate>
                        Programas asignados a esta encuesta
                    </HeaderTemplate>
                </telerik:RadListBox>
            </div>
        </div>
        <br />
        <br />
    </div>

   <%--<asp:Button runat="server" OnClick="Unnamed_Click" Text="encuesta actual" />--%>

</asp:Content>

