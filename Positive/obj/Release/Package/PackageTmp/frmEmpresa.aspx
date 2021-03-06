﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmEmpresa.aspx.cs" Inherits="Inventario.frmEmpresa" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
    function showpreview(input) {
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').css('visibility', 'visible');
                    $('#imgpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4 col-lg-offset-4" style="text-align:center">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Logo
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4" style="align-content:center">
                            <input type="button" value="Buscar Logo" class="btn btn-success" onclick="document.getElementById('<%=fluImage.ClientID %>').click();" />
                            <asp:FileUpload runat="server" ID="fluImage"  onchange="showpreview(this);" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <img id="imgpreview" src="<%=srcLogo %>" style="border-width: 0px;max-width:300px;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <asp:Label ID="lblNombre" runat="server"></asp:Label>:
            <div class="input-group">
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Nombre.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblTipoIdentificacion" runat="server"></asp:Label>:
            <div class="input-group">
                <asp:DropDownList ID="ddlTipoIdentificacion" CssClass="form-control" runat="server" AutoPostBack="false" DataTextField="Nombre" DataValueField="idTipoIdentificacion"></asp:DropDownList>
                <span class="input-group-addon">
                <img src="Images/Input/Identificacion.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblIdentificacion" runat="server" CssClass="Box"></asp:Label>
            <div class="input-group">
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Identificacion.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblDireccion" runat="server"></asp:Label>:
            <div class="input-group">
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Direccion.png" title="Dirección" /></span>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <asp:Label ID="lblCiudad" runat="server"></asp:Label>:
            <div class="input-group">
                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control"></asp:TextBox>
                <input type="hidden" id="hddIdCiudad" runat="server" />
                <span class="input-group-addon">
                <img src="Images/Input/Mapa.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblTelefono" runat="server"></asp:Label>:
            <div class="input-group">
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Telefono.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-3">
            Fecha Inicial Pedidos:
            <div class="input-group">
                <asp:TextBox ID="txtFechaInicialPedido" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Date.png" title="Fecha Inicial" /></span>
            </div>
        </div>
        <div class="col-md-3">
            Multi Bodega:
            <div class="input-group">
                <asp:CheckBox ID="chkMultiBodega" runat="server" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            Impoconsumo:
            <div class="input-group">
                <asp:TextBox ID="txtImpoconsumo" runat="server" CssClass="form-control Decimal"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Price.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-3">
            Manejo Precio Con IVA:
            <div class="input-group">
                <asp:CheckBox ID="chkPrecioConIVA" runat="server"></asp:CheckBox>
            </div>
        </div>
        <div class="col-md-3">
            Manejo Costo Con IVA:
            <div class="input-group">
                <asp:CheckBox ID="chkCostoConIVA" runat="server" />
            </div>
        </div>
        <div class="col-md-3">
            Manejo Descuento Con IVA:
            <div class="input-group">
                <asp:CheckBox ID="chkDescuentoConIVA" runat="server" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            Texto Encabezado Factura:
                <asp:TextBox ID="txtEncabezado" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="col-md-6">
            Texto Pie Factura:
                <asp:TextBox ID="txtPiePagina" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
        </div>
    </div>
    <div>
        <div class="col-md-12" style="text-align: center">
            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
        </div>
    </div>
</asp:Content>
