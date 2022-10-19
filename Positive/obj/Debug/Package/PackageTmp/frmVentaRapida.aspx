<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVentaRapida.aspx.cs" Inherits="Inventario.frmVentaRapida" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />--%>
    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>
    <style>
        .active {
            background-color:white;
        }
        ::-webkit-scrollbar {
            width: 10px;
        }

        ::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3); 
            border-radius: 5px;
        }
        ::-webkit-scrollbar-thumb {
            border-radius: 5px;
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5); 
        }

        /* Three columns side by side */
        .column {
          float: left;
          width: 12.5%;
          margin-bottom: 16px;
          padding: 0 8px;
        }

        /* Display the columns below each other instead of side by side on small screens */
        @media screen and (max-width: 650px) {
          .column {
            width: 50%;
            display: block;
          }

          p {
            margin: 0 0 0px !important;
            font-size:70%;
        }
        }

        /* Add some shadows to create a card effect */
        .card {
          box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
        }

        .card > img {
          width: 100%;
        }

        /* Some left and right padding inside the container */
        .containerCard {
          padding: 0 16px;
        }

        /* Clear floats */
        .containerCard::after, .row::after {
          content: "";
          clear: both;
          display: table;
        }

        .title {
          color: grey;
        }

        .button {
          border: none;
          outline: 0;
          display: inline-block;
          padding: 8px;
          color: white;
          background-color: #000;
          text-align: center;
          cursor: pointer;
          width: 100%;
        }

        .button:hover {
          background-color: #555;
        }

        p {
            margin: 0 0 0px !important;
            font-size:70%;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .nav-pills>li {
            margin-left: 2px;
            background-color: #DFF0D8;
            text-transform: uppercase;
        }

        .vrFacturar {
            height: 32px;
            background-color: #5CB85C;
            cursor:pointer;
            text-align: center;
            align-content:center;
        }

        .vrFacturar p {
            font-size: 16px;
            position: relative;
            top: 5px;
            font-weight: bold;
            color: #337AB7;
        }

        .vrFacturar p:hover {
            color: #FFFFFF;
        }

        .divFacturaVentaRapida {
        }

    </style>
    <script type="text/javascript">
        function CalcularDevueltaVentaRapida() {
            var string = "";
            if ($('#' + string + 'txtValorPago').val() != "0" && $('#' + string + 'txtValorPago').val() != "") {
                $('#' + string + 'txtValorPago').val($('#' + string + 'txtValorPago').val().replace("$", ""));
                $('#' + string + 'tdValorTotal').html($('#' + string + 'tdValorTotal').html().replace("$", ""));
                $('#' + string + 'txtDevuelta').val(formatNumber.new((parseFloat($('#' + string + 'txtValorPago').val().replace(",", "")).toFixed(2) - parseFloat($('#' +
                    string + 'tdValorTotal').html().replace(",", "")).toFixed(2)).toFixed(2)));
            }
            else {
                $('#' + string + 'txtDevuelta').val("0");
                $('#' + string + 'txtValorPago').val("0");
            }
        }
    </script>
    <input type="hidden" runat="server" id="hddItems" value="" />
    <input type="hidden" runat="server" id="hddIdEmpresa" value="" />
    <input type="hidden" id="hddValorTotalAntesImpoConsumo" value="0" />
    <input type="hidden" runat="server" id="hddTipoDocumento" value="1" />
    <div class="container-fluid">
        <div>
            <div class="row">
                <div class="col-md-4">
                    <div class = "input-group">
                        <asp:TextBox ID="txtTercero" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class = "input-group-addon"><img src="Images/Input/SocioNegocio.png" title="Socio de Negocio" /></span>
                    </div>
                    <input type="hidden" id="hddIdCliente" runat="server" /><asp:Button ID="btnActualizarPrecios" runat="server" OnClick="btnActualizarPrecios_Click" />
                </div>
                <div class="col-md-3">
                    <asp:RadioButton ID="rdbFacturaVenta" GroupName="TipoDocumento" runat="server" Text="Factura de Venta" />&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdbRemision" GroupName="TipoDocumento" runat="server" Text="Remision" />&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdbCotizacion" GroupName="TipoDocumento" runat="server" Text="Cotizacion" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-9">
                    <div>
                        <br />
                        <div id="divItems" runat="server">
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div id="demo" class="divFacturaVentaRapida">
                        <div class="row" id="div_Factura">
                            <div class="col-md-12">
                                <div class = "panel panel-default" style="font-size:xx-small;height:450px;overflow-y: scroll;width:auto">
                                    <div class = "panel-heading">
                                        <h3 class = "panel-title">
                                            <asp:Label ID="lblFacturaVenta" runat="server"></asp:Label>
                                        </h3>
                                    </div>
   
                                    <div class = "panel-body">
                                        <table style="width:100%">
                                            <thead>
                                                <tr>
                                                    <th><asp:Label runat="server" ID="lblCodigo"></asp:Label></th>
                                                    <th><asp:Label runat="server" ID="lblDescripcion"></asp:Label></th>
                                                    <th><asp:Label runat="server" ID="lblCantidad"></asp:Label></th>
                                                    <th><asp:Label runat="server" ID="lblValor"></asp:Label></th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody class="FacturaRapidaBody">
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="5"><hr /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3"><asp:Label runat="server" ID="lblValorAntesIVA"></asp:Label></td>
                                                    <td style="text-align:right;font-weight:bold;font-size:small;" id="tdValorAntesIVA">0</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3"><asp:Label runat="server" ID="lblIVA"></asp:Label></td>
                                                    <td style="text-align:right;font-weight:bold;font-size:small;" id="tdValorIVA">0</td>
                                                    <td><input type="hidden" id="hddValorIVA" runat="server" value="0" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3"><asp:Label runat="server" ID="lblImpoconsumo">Impoconsumo</asp:Label></td>
                                                    <td style="text-align:right;font-weight:bold;font-size:small;" id="tdValorImpoconsumo">0</td>
                                                    <td><input type="hidden" id="hddValorImpoconsumo" runat="server" value="0" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3"><asp:Label runat="server" ID="lblValorTotal"></asp:Label></td>
                                                    <td style="text-align:right;font-weight:bold;font-size:small;" id="tdValorTotal">0</td>
                                                    <td><input type="hidden" runat="server" id="hddValorTotal" value="0" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:center;">
                                                        <asp:ImageButton ImageUrl="~/Images/Documento/Guardar.png" CssClass="GuardarVentaRapida" ToolTip="Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click"/>
                                                        <asp:ImageButton ImageUrl="~/Images/Documento/Cancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" CausesValidation="false" OnClick="btnCancelar_Click"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:center;"><br /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:left; padding-left:10px">
                                                        <b> Observaciones</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:center;">
                                                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:left; padding-left:10px">
                                                        <b> Valor Pago</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:center;">
                                                        <div class = "input-group">
                                                            <input type="text" id="txtValorPago" style="font-family:Courier New;font-size:medium" class="Decimal form-control BoxValor" onblur="CalcularDevueltaVentaRapida();" />
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Total IVA" /></span>
                                                    </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:center;"><br /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:left; padding-left:10px">
                                                        <b>Devuelta</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" style="text-align:center;">
                                                        <div class = "input-group">
                                                            <input type="text" id="txtDevuelta" style="font-family:Courier New;font-size:medium;color:blue" class="form-control BoxValor" disabled="disabled" />
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Total IVA" /></span>
                                                    </div>
                                                    </td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>










