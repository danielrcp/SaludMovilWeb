<%@ Page Title="Cálculo de índice de Framingham" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Framingham.aspx.cs" Inherits="SaludMovil.Portal.ModPacientes.Framingham" %>
<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <script>
        function calcular() {
            var peso = parseFloat(document.getElementById("peso").value);
            var altura = parseFloat(document.getElementById("altura").value);
            altura = altura / 100;
            var imc = (peso / (altura * altura)).toFixed(2);
            document.getElementById("imc").value = imc;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container">
        <div class="col-sm-4">
            <div class="form-group">
                <label class="control-label col-sm-6" for="peso">Paciente </label>
                <asp:Label ID="lblPaciente" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="form-group">
                <label class="control-label col-sm-6" for="peso">Género </label>
                <asp:DropDownList ID="ddlGenero" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="form-group">
                <label class="control-label col-sm-6" for="altura">Edad </label>
                <asp:Label ID="lblEdad" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
        <div class="col-sm-4">
            <div class="form-group">
                <label class="control-label col-sm-12" for="altura">Colesterol total (mg/dL) </label>
                <asp:TextBox ID="txtColesterolTotal" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-sm-offset-2">
            <div class="form-group">
                <label class="control-label col-sm-12" for="altura">Colesterol HDL (mg/dL) </label>
                <asp:TextBox ID="txtColesterolHDL" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 ">
            <div class="form-group">
                <label class="control-label col-sm-12" for="altura">Presión sistólica (mmHg) </label>
                <asp:TextBox ID="txtPresion" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-sm-offset-2">
            <div class="form-group">
                <label class="control-label col-sm-12" for="altura">Fumador(a) </label>
                <asp:RadioButton ID="rblFumador" runat="server" Text="Fumador"></asp:RadioButton>
            </div>
        </div>
        <div class="col-sm-4 ">
            <div class="form-group">
                <label class="control-label col-sm-12" for="altura">Diabetes </label>
               <asp:RadioButtonList ID="rblDiabetes" runat="server"></asp:RadioButtonList>
            </div>
        </div>

        <div class="form-group"> 
            <div class="col-sm-offset-2 col-sm-4" style="text-align:center">
                <button type="button" class="btn-primary" onclick="calcular()">Calcular</button>
            </div>
            <div class="col-sm-4"> 
                <input type="number" class="form-control" id="imc" readonly></input>
            </div>
        </div>
    </div>
</asp:Content>
