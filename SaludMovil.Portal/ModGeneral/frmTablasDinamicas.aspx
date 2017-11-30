<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="frmTablasDinamicas.aspx.cs" Inherits="SaludMovil.Portal.ModGeneral.frmTablasDinamicas" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="TablasDinamicas" ContentPlaceHolderID="MainContent">
    <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
        EnableShadow="true" Position="Center" Animation="FlyIn" VisibleTitlebar="false" >
    </telerik:RadNotification>
    <h6 class="text-danger" id="hRuta" >ADMINISTRACIÓN / TABLAS ADMINISTRABLES</h6>
    <div class="container-fluid">
        <h3 class="encabezado" style="" >TABLAS ADMINISTRABLES</h3>
        <div class="row">
            <div class="col-lg-4 col-lg-offset-4">
                <div class="form-group">
                    <asp:DropDownList ID="cboListaTablas" runat="server" CssClass="form-control" OnSelectedIndexChanged="cboListaTablas_SelectedIndexChanged"
                        Width="360px" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:Panel ID="pnlGrilla" runat="server" Visible="false">
                <telerik:RadGrid ID="RadGridAutomatica"
                    runat="server"
                    OnNeedDataSource="RadGridAutomatica_NeedDataSource"
                    OnItemCommand="RadGridAutomatica_ItemCommand"
                    OnColumnCreated="RadGridAutomatica_ColumnCreated"
                    OnItemDataBound="RadGridAutomatica_ItemDataBound"
                    AutoGenerateColumns="true"
                    ClientSettings-Selecting-AllowRowSelect="true"
                    AllowFilteringByColumn="true" Height="620"
                    AllowPaging="true"
                    AllowSorting="true">
                    <GroupingSettings CaseSensitive="false" />  
                    <ExportSettings Excel-Format="Biff" ></ExportSettings>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" />                        
                    </ClientSettings>       
                    <SelectedItemStyle BackColor="SkyBlue" BorderColor="Purple" BorderStyle="Dashed" BorderWidth="1px" />           
                    <MasterTableView CommandItemDisplay="Top"
                        CommandItemSettings-AddNewRecordText="Agregar nuevo registro" CommandItemSettings-ShowRefreshButton="false"
                        CommandItemSettings-ShowAddNewRecordButton="true" CommandItemSettings-ShowExportToExcelButton="true"
                        DataKeyNames="" PagerStyle-PageSizeLabelText="Registros por página"
                        CommandItemSettings-ExportToExcelText="Exportar a Excel">
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" EditText="Editar"
                                HeaderStyle-Width="20px" ItemStyle-Width="20px" FooterStyle-Width="20px">
                                <ItemStyle CssClass="MyImageButton" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete"
                                ImageUrl="~/Images/caneca.png" Text="Eliminar" UniqueName="DeleteColumn"
                                Resizable="false" ConfirmText="¿Eliminar registro de la tabla?">
                                <HeaderStyle CssClass="rgHeader ButtonColumnHeader"></HeaderStyle>
                                <ItemStyle CssClass="ButtonColumn" />
                            </telerik:GridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
