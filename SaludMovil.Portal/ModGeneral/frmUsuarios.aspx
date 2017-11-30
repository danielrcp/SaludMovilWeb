<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="frmUsuarios.aspx.cs" Inherits="SaludMovil.Portal.ModGeneral.frmUsuarios" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content runat="server" ID="Usuarios" ContentPlaceHolderID="MainContent">
    <telerik:RadNotification ID="RadNotificationMensajes" runat="server" EnableRoundedCorners="true"
        EnableShadow="true" Position="Center" Animation="FlyIn" VisibleTitlebar="false">
    </telerik:RadNotification>
    <div class="row">
        <asp:HiddenField ID="hdfIdUsuario" runat="server" />
        <asp:HiddenField ID="hdfCreatedBy" runat="server" />
        <asp:HiddenField ID="hdfCreatedDate" runat="server" />
        <asp:Panel ID="panelGrilla" runat="server" Visible="true">
            <h3 class="encabezado">USUARIOS</h3>
            <telerik:RadGrid ID="RadGridUsuarios" GridLines="None" runat="server"
                AllowSorting="True"
                AllowPaging="True"
                OnNeedDataSource="RadGridUsuarios_NeedDataSource"
                OnItemCommand="RadGridUsuarios_ItemCommand"
                AllowFilteringByColumn="True"
                CellSpacing="0"
                Height="500px"
                AutoGenerateColumns="false">
                <PagerStyle Mode="NextPrevAndNumeric" />
                <GroupingSettings CaseSensitive="false" />
                <ClientSettings>
                    <Scrolling AllowScroll="true" UseStaticHeaders="true"/>
                    <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true"></Selecting>
                    <%--<ClientEvents OnRowDblClick="DobleClickFila" />--%>
                </ClientSettings>
                <SelectedItemStyle BackColor="SkyBlue" BorderColor="Purple" BorderStyle="Dashed" BorderWidth="1px" />
                <MasterTableView CommandItemDisplay="Top" DataKeyNames="idUsuario, primerNombre, segundoNombre, primerApellido, segundoApellido, idTipoIdentificacion, idCiudad, idTipo, createdBy, createdDate"
                    EditMode="EditForms" HorizontalAlign="Center" Width="100%"
                    NoMasterRecordsText="No hay registros para mostrar."
                    CommandItemSettings-ShowRefreshButton="false"
                    CommandItemSettings-AddNewRecordText="Agregar usuario" CommandItemStyle-CssClass="boton">
                    <Columns>
                        <telerik:GridButtonColumn HeaderStyle-Width="40px" UniqueName="btnBuscar" ItemStyle-Width="20px" ItemStyle-Height="20px" HeaderTooltip="Consultar"
                            HeaderStyle-Height="40px" FooterStyle-Height="40px"
                            ImageUrl="../Images/buscar.png"
                            ButtonType="ImageButton" Resizable="true"
                            CommandName="Buscar">
                            <ItemStyle Width="40px" Height="40px" />
                        </telerik:GridButtonColumn>
                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmDialogType="RadWindow" ConfirmText="Delete this product?" ConfirmTitle="Delete" Text="Delete" UniqueName="DeleteColumn" HeaderStyle-Width="10px" Visible="false">
                            <ItemStyle CssClass="MyImageButton" HorizontalAlign="Center" />
                        </telerik:GridButtonColumn>
                        <telerik:GridBoundColumn DataField="nombresPersona" HeaderText="Nombres" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="nombre" UniqueName="nombre" AutoPostBackOnFilter="true" />
                        <telerik:GridBoundColumn DataField="apellidosPersona" HeaderText="Apellidos" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="apellido" FilterControlAltText="apellido" UniqueName="apellido" AutoPostBackOnFilter="true" />
                        <telerik:GridBoundColumn DataField="usuario" HeaderText="Usuario" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" FilterControlAltText="usuario" UniqueName="usuario" AutoPostBackOnFilter="true" />
                        <telerik:GridBoundColumn DataField="nombreTipoIdentificacion" HeaderText="Tipo ID" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="nombreTipoIdentificacion" UniqueName="nombreTipoIdentificacion" AutoPostBackOnFilter="true" />
                        <telerik:GridBoundColumn DataField="numeroIdentificacion" HeaderText="No Identificación" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="numeroIdentificacion" FilterControlAltText="numeroIdentificacion" UniqueName="numeroIdentificacion" AutoPostBackOnFilter="true" />
                        <%--<telerik:GridBoundColumn DataField="tipoUsuario" HeaderText="Tipo" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="tipoUsuario" FilterControlAltText="tipoUsuario" UniqueName="tipoUsuario" AutoPostBackOnFilter="true" />--%>
                        <telerik:GridDateTimeColumn DataField="fechaNacimiento" HeaderText="Fecha Nacimiento" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="fechaNacimiento" FilterControlAltText="fechaNacimiento" UniqueName="fechaNacimiento" AutoPostBackOnFilter="true" EnableTimeIndependentFiltering="true" />
                        <telerik:GridBoundColumn DataField="nombreCiudad" HeaderText="Ciudad" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="nombreCiudad" FilterControlAltText="nombreCiudad" UniqueName="nombreCiudad" AutoPostBackOnFilter="true" />
                        <telerik:GridBoundColumn DataField="celular" HeaderText="Celular" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="celular" FilterControlAltText="celular" UniqueName="celular" AutoPostBackOnFilter="true" />
                        <telerik:GridBoundColumn DataField="telefonoFijo" HeaderText="Teléfono" HeaderStyle-Width="250px" ItemStyle-Width="250px" SortExpression="telefonoFijo" FilterControlAltText="telefonoFijo" UniqueName="telefonoFijo" AutoPostBackOnFilter="true" />
                        <telerik:GridBoundColumn DataField="correo" HeaderText="Correo Electronico" HeaderStyle-Width="200px" ItemStyle-Width="200px" FooterStyle-Width="200px" FilterControlAltText="correoElectronico" UniqueName="correoElectronico" AutoPostBackOnFilter="true" />                        
                        <%--<telerik:GridBoundColumn DataField="nombreRol" HeaderText="Rol" HeaderStyle-Width="100px" ItemStyle-Width="100px" FooterStyle-Width="300px" FilterControlAltText="nombreRol" UniqueName="nombreRol" AutoPostBackOnFilter="true" />                                    --%>
                        <%--<telerik:GridCheckBoxColumn DataField="Estado" HeaderText="Estado" UniqueName="estado" AutoPostBackOnFilter="true" />--%>
                        
                    </Columns>
                    <EditFormSettings>
                        <FormTableItemStyle Wrap="False" />
                        <FormCaptionStyle CssClass="EditFormHeader" />
                        <FormMainTableStyle BackColor="White" CellPadding="3" CellSpacing="0" GridLines="None" Width="100%" />
                        <FormTableStyle BackColor="white" ForeColor="Black" Font-Bold="true" CellPadding="2" CellSpacing="0" Height="110px" />
                        <FormTableAlternatingItemStyle Wrap="false" BackColor="#5499CD" />
                        <FormTableItemStyle Wrap="false" BackColor="#5499CD" />
                        <EditColumn ButtonType="PushButton" CancelText="Cancelar" UpdateText="Actualizar" InsertText="Guardar" UniqueName="EditCommandColumn1">
                        </EditColumn>
                        <FormTableButtonRowStyle HorizontalAlign="Left" />
                    </EditFormSettings>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>
        <asp:Panel ID="panelComboUsuario" runat="server" Visible="false">
            <h3 class="encabezado">BUSCAR USUARIO EXISTENTE EN EL DIRECTORIO ACTIVO</h3>
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="cboPersonalMedico" runat="server" Filter="Contains" DropDownAutoWidth="Enabled" Width="100%" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" EmptyMessage="Digite para buscar..." OnItemDataBound="cboPersonalMedico_ItemDataBound"
                    OnItemsRequested="cboPersonalMedico_ItemsRequested" LoadingMessage="Cargando personal médico" OnSelectedIndexChanged="cboPersonalMedico_SelectedIndexChanged">
                </telerik:RadComboBox>
                <div style="padding-top:15px">
                    <asp:Button ID="btnAgregarManual" runat="server" Text="Agregar usuarios manual" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnAgregarManual_Click" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="panelAgregarUsuario" runat="server" Visible="false">
            <h3 class="encabezado">INFORMACIÓN USUARIO</h3>
            <div class="row">
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblPrimerNombre" runat="server" CssClass="text-left" Text="Primer nombre" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtPrimerNombre" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblSegundoNombre" runat="server" CssClass="text-left" Text="Segundo nombre" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtSegundoNombre" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblPrimerApellido" runat="server" CssClass="text-left" Text="Primer apellido" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtPrimerApellido" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblSegundoApellido" runat="server" CssClass="text-left" Text="Segundo apellido" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtSegundoApellido" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblbTipoID" runat="server" CssClass="text-left" Text="Tipo identificación" Width="100%"></asp:Label>
                    <telerik:RadComboBox ID="cboTipoIdentificacion" runat="server" Width="100%" EnableLoadOnDemand="true" 
                        OnItemsRequested="cboTipoIdentificacion_ItemsRequested" EmptyMessage="Digite para buscar..." />
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblNumeroID" runat="server" CssClass="text-left" Text="Numero identificación" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtNumeroIdentificacion" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblFechaNac" runat="server" CssClass="text-left" Text="Fecha nacimiento" Width="100%"></asp:Label>
                    <telerik:RadDatePicker ID="rdpFechaNacimiento" runat="server" Width="100%" Culture="es-ES" MinDate="1/1/1900" DateInput-DateFormat="dd/MM/yyyy"></telerik:RadDatePicker>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblCiudad" runat="server" CssClass="text-left" Text="Ciudad" Width="100%"></asp:Label>
                    <telerik:RadComboBox ID="cboCiudad" runat="server" Width="100%" EnableLoadOnDemand="true" OnItemsRequested="cboCiudad_ItemsRequested" 
                        EmptyMessage="Digite para buscar..."/>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblCelular" runat="server" CssClass="text-left" Text="Celular" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtCelular" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblTelefonoFijo" runat="server" CssClass="text-left" Text="Teléfono" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtTelefonoFijo" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblCorreo" runat="server" CssClass="text-left" Text="Correo Electronico" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtCorreo" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblEmpresa" runat="server" CssClass="text-left" Text="Empresa" Width="100%"></asp:Label>
                    <telerik:RadComboBox ID="cboEmpresa" runat="server" Width="100%" EnableLoadOnDemand="true" OnItemsRequested="cboEmpresa_ItemsRequested" 
                        EmptyMessage="Digite para buscar..."/>                    
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblChkMedicoTratante" runat="server" CssClass="text-left" Text="¿Medico tratante?" Width="100%"></asp:Label>
                    <asp:CheckBox ID="chkMedicoTratante" runat="server" Width="100%" CssClass="form-control"></asp:CheckBox>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 col-md-3">                    
                    <asp:Label ID="lblUSuario" runat="server" CssClass="text-left" Text="Usuario" Width="100%"></asp:Label>
                    <asp:TextBox ID="txtUsuario" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Label ID="lblRol" runat="server" CssClass="text-left" Text="Rol " Width="100%"></asp:Label>
                    <telerik:RadComboBox ID="cboRol" runat="server" Width="100%" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" 
                        Localization-AllItemsCheckedString="Todos" Localization-CheckAllString="Seleccionar todos"/>

                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Panel ID="panelLblContrasena" runat="server">
                        <asp:Label ID="lblContrasena" runat="server" CssClass="text-left" Text="Contraseña" Width="100%"></asp:Label>
                        <asp:TextBox ID="txtContrasena" TextMode="Password" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                    </asp:Panel>
                </div>
                <div class="col-sm-6 col-md-3">
                    <asp:Panel ID="panelLblEstado" runat="server">
                        <asp:Label ID="lblEstado" runat="server" CssClass="text-left" Text="Estado" Width="100%"></asp:Label>
                        <telerik:RadComboBox ID="cboEstado" runat="server" AppendDataBoundItems="true" Width="100%">
                            <Items>
                                <telerik:RadComboBoxItem Text="Seleccione..." Value="0" Selected="true" />
                                <telerik:RadComboBoxItem Text="Activo" Value="1" Selected="false" />
                                <telerik:RadComboBoxItem Text="Inactivo" Value="2" Selected="false" />
                            </Items>
                        </telerik:RadComboBox>
                    </asp:Panel>
                </div>
            </div>
            <div class="row text-center" style="float:none; padding-top:15px">
                <div class="col-md-3">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnAgregarUsuario" runat="server" Text="Guardar" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnAgregarUsuario_Click" />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnActualizar_Click" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </asp:Panel>
        <asp:PlaceHolder ID="placeHolderControl" runat="server"></asp:PlaceHolder>
    </div>

</asp:Content>
