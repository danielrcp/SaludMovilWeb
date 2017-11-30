<%@ Page Title="Cálculo de índice de masa corporal" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CalculoIMC.aspx.cs" Inherits="SaludMovil.Portal.ModPacientes.CalculoIMC" %>
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
    <div class="container col-md-6">
        <div class="form-group">
            <label class="control-label col-sm-4" for="peso">Peso (kg): </label>
            <input type="number" class="form-control" id="peso" placeholder="Inserte su peso en kg">
        </div>
        <div class="form-group">
            <label class="control-label col-sm-4" for="altura">Altura (cm): </label>
            <input type="number" class="form-control" id="altura" placeholder="Inserte su altura en cm">
        </div>
        <div class="form-group"> 
            <div class="col-sm-offset-2 col-sm-4">
                <button type="button" class="btn-primary" onclick="calcular()">Calcular IMC</button>
            </div>
            <div class="col-sm-4"> 
                <input type="number" class="form-control" id="imc" readonly></input>
            </div>
        </div>
    </div>
    
    <div>
        <div class="col-md-6">
            <div class="table-responsive">
                <table class="table-bordered">
                    <thead>
                        <tr>
                            <th style="width:250px; text-align:center">Índice de masa corporal</th>
                            <th style="width:250px; text-align:center">Clasificación</th>
                        </tr>                            
                    </thead>
                    <tbody>
                        <tr>
                            <td align="center"><16.00</td>
                            <td align="center">Infrapeso: delgadez severa</td>
                        </tr>                            
                        <tr>
                            <td align="center">16.00 - 16.99</td>
                            <td align="center">Infrapeso: delgadez moderada</td>
                        </tr>                            
                        <tr>
                            <td align="center">17.00 - 18.49</td>
                            <td align="center">Infrapeso: delgadez aceptable</td>
                        </tr>                            
                        <tr>
                            <td align="center">18.50 - 24.99</td>
                            <td align="center">Peso normal</td>
                        </tr>
                        <tr>
                            <td align="center">25.00 - 29.99</td>
                            <td align="center">Sobrepeso</td>
                        </tr>                            
                        <tr>
                            <td align="center">30.00 - 34.99</td>
                            <td align="center">Obeso: tipo I</td>
                        </tr>                            
                        <tr>
                            <td align="center">35.00 - 40.00</td>
                            <td align="center">Obeso: tipo II</td>
                        </tr>                            
                        <tr>
                            <td align="center">>40.00</td>
                            <td align="center">Obeso: tipo III</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
