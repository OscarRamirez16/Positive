<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDocumentos.aspx.cs" Inherits="Inventario.frmDocumentos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgFactura");
        });
    </script>
    <div id="contenido" style="width: 100%">
        <div class="row">
            <div class="col-md-10">
                <ul class="tab">
                    <li id="pestana1"><a id="aDocumento" href='javascript:void(0)' class="tablinks active" onclick="ShowTab('aDocumento', 'divDocumento')">
                        <asp:Label ID="lblTipoDocumento" ForeColor="#1D70B7" runat="server"></asp:Label></a></li>
                    <li id="pestana2"><a id="aFormaPago" href='javascript:void(0)' class="tablinks" onclick="ShowTab('aFormaPago', 'divFormaPago')">
                        <span style="color:#1D70B7">Formas de Pago</span></a></li>
                    <li id="pestana3"><a id="aReteciones" href='javascript:void(0)' class="tablinks" onclick="ShowTab('aReteciones', 'divRetenciones')">
                        <span style="color:#1D70B7">Retenciones</span></a></li>
                </ul>
            </div>
            <div class="col-md-2" style="position:relative;top:7px">
                <asp:Label ID="lblNumero" class="label label-warning" Font-Size="Large" ForeColor="White" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblNumeroFactura" ForeColor="White" Font-Size="Large" class="label label-info" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div id="divDocumento" onkeydown="ConfigurarTeclas(event);" class="tabcontent" style="display:block">
        <div class="row" runat="server" id="divPrincipal">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-1">
                        <asp:Label ID="lblTercero" runat="server"></asp:Label>:
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                                <asp:TextBox ID="txtTercero" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtTercero_TextChanged"></asp:TextBox>
                                <span class="input-group-addon">
                                    <img src="Images/Input/SocioNegocio.png" title="Socio de Negocio" onclick="AbrirVentanaTerceroNuevo()" /></span>
                            </div>
                            <input type="hidden" id="hddIdCliente" runat="server" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblIdentificacion" runat="server"></asp:Label>:
                    </div>
                    <div class="col-md-2">
                        <div class="input-group">
                                <asp:TextBox ID="txtIdentificacion" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon">
                                    <img src="Images/Input/Identificacion.png" title="Identificación" /></span>
                            </div>
                    </div>
                    <div class="col-md-1">
                        Vendedor:
                    </div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlVendedor" runat="server" CssClass="form-control" DataTextField="Nombre" DataValueField="idVendedor"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <div class="input-group">
                                <asp:TextBox ID="txtReferencia" placeholder="Referencia" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtReferencia_TextChanged"></asp:TextBox>
                                <span class="input-group-addon">
                                    <img src="Images/Input/Identificacion.png" title="Referencia" /></span>
                            </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblEstado" runat="server" Visible="false" CssClass="label label-warning"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <asp:Label ID="lblDireccion" runat="server"></asp:Label>:
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                                <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon">
                                    <img src="Images/Input/Direccion.png" title="Dirección" /></span>
                            </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblCiudad" runat="server"></asp:Label>:
                    </div>
                    <div class="col-md-2">
                        <input type="hidden" id="hddIdCiudad" runat="server" />
                            <div class="input-group">
                                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon"><img src="Images/Input/Mapa.png" title="Ciudad" /></span>
                            </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblTelefono" runat="server"></asp:Label>:
                    </div>
                    <div class="col-md-2">
                        <div class="input-group">
                                <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon">
                                    <img src="Images/Input/Telefono.png" title="Teléfono" /></span>
                            </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblListaPrecio" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row tr">
                    <div class="col-md-12">
                        <b><i style="color:#FFFFFF" class="fa fa-plus-circle fa-fw"></i>&nbsp;<asp:Label ID="lblDetalles" ForeColor="White" runat="server"></asp:Label></b>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-1">
                        <div class="input-group">
                            <asp:Label ID="lblCodigo" Text="Codigo" runat="server"></asp:Label>:
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <asp:Label ID="lblArticulo" Text="Articulo" runat="server"></asp:Label>:
                        <div class="input-group">
                            <asp:TextBox ID="txtArticulo" runat="server" CssClass="form-control"></asp:TextBox>
                            <input type="hidden" id="hddIdArticulo" runat="server" value="0" />
                            <input type="hidden" id="hddIVA" runat="server" value="0" />
                            <input type="hidden" id="hddEsInventario" runat="server" value="0" />
                            <input type="hidden" id="hddPrecioSinIVA" runat="server" value="0" />
                            <input type="hidden" id="hddValorDescuento" runat="server" value="0" />
                            <span class="input-group-addon">
                            <img src="Images/Input/Articulo.png" title="Dirección" /></span>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lblBodega" Text="Bodega" runat="server"></asp:Label>:
                        <div class="input-group">
                            <asp:TextBox ID="txtBodega" runat="server" CssClass="form-control"></asp:TextBox>
                            <input type="hidden" id="hddIdBodega" runat="server" value="0" />
                            <input type="hidden" id="hddCantidad" runat="server" value="0" />
                            <span class="input-group-addon">
                            <img src="Images/Input/Direccion.png" title="Dirección" /></span>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <div class="input-group">
                            <asp:Label ID="lblCantidad" Text="Cantidad" runat="server"></asp:Label>:
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control BoxValorGrilla Decimal"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="input-group">
                                    <asp:Label ID="lblDescuento" Text="Desc" runat="server"></asp:Label>:
                                    <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control BoxValorGrilla Entero"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-7">
                                <div class="input-group">
                                    <asp:Label ID="lblPrecio" Text="Valor" runat="server"></asp:Label>:
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control BoxValor Decimal"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <img src="Images/Input/DolarAmarillo.png" style="position: relative;top: 25px;" id="imgPrecios" onclick="MostrarPrecios();" runat="server"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align:center;padding-top:10px;">
                        <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" runat="server" ID="btnAdicionar" Height="40" Width="40" OnClick="btnAdicionar_Click" />
                        <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Login" runat="server" ID="btnLogin" Height="40" Width="40" OnClick="btnIngresar_Click" />
                        <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Descuento" runat="server" ID="btnDescuento" Height="40" Width="40" OnClick="btnDescuento_Click" />
                        <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Autorizar" runat="server" ID="btnCalcularDescuento" Height="40" Width="40" OnClick="btnCalcularDescuento_Click" />
                        <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Crear Tercero" runat="server" ID="btnCrearTercero" Height="40" Width="40" OnClick="btnCrearTercero_Click" />
                    </div>
                </div>
                <div class="row tr">
                    <div class="col-md-12">
                        <b style="color:#FFFFFF"><i style="color:#FFFFFF" class="fa fa-cubes fa-fw"></i>&nbsp;Detalle</b>
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <asp:DataGrid ID="dgFactura" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnItemCommand="dgFactura_ItemCommand" OnItemDataBound="dgFactura_ItemDataBound" >
                        <Columns>
                            <asp:BoundColumn DataField="IdArticulo" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Codigo" HeaderText="Codigo" ></asp:BoundColumn>
                            <asp:BoundColumn DataField="Articulo" HeaderText="Articulo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IdBodega" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Bodega" HeaderText="Bodega"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Cantidad" HeaderText="Cant"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ValorUnitario" HeaderText="V.U."></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Precio Venta" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPrecioVenta" runat="server" CssClass="BoxValorGrilla" Text="0.00" AutoPostBack="true" OnTextChanged="GuardarPrecioVenta"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Descuento" HeaderText="Desc"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ValorUnitarioConDescuento" HeaderText="V.U. Con Desc"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IVA" HeaderText="IVA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ValorUnitarioConIVA" HeaderText="V.U. Con IVA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalLinea" HeaderText="Total Linea"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Eliminar" HeaderText="Eliminar" Text="<img src='Images/eliminar.png' Width='18' Height='18' title='Eliminar' />"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="CostoPonderado" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ValorDescuento" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PrecioCosto" Visible="false"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:DataGrid>
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-8">
                        <div class="row">
                            <div class="col-md-2"><asp:Label ID="lblObservaciones" runat="server"></asp:Label>:</div>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtObservaciones" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row"><div class="col-md-12"><br /></div></div>
                        <div class="row">
                            <div class="col-md-2">Valor Pago:</div>
                            <div class="col-md-4">
                                <input style="height:50px; font-size:20px;" type="text" id="txtValorPago" value="0" runat="server" onblur="CalcularDevuelta();" class="Decimal form-control BoxValor" maxlength="22" autocomplete="off"/>
                            </div>
                            <div class="col-md-2">Devuelta:</div>
                            <div class="col-md-4">
                                <input style="height:50px; font-size:20px" type="text" id="txtDevuelta1" value="0" runat="server" readonly="True" class="Decimal form-control BoxValor" maxlength="22" autocomplete="off"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%;">Antes de IVA:</td>
                                        <td>
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtAntesIVA" runat="server" class="form-control BoxValor"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Antes de IVA" /></span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Descuento Por Artículos:</td>
                                        <td>
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtTotalDescuento" runat="server" class="form-control BoxValor" ></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total IVA" /></span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Valor IVA:</td>
                                        <td>
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtTotalIVA" runat="server" class="form-control BoxValor"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total IVA" /></span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="trImpoconsumo" runat="server">
                                        <td>Impoconsumo:</td>
                                        <td>
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtImpoconsumo" runat="server" class="form-control BoxValor"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total Impoconsumo" /></span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="trPropina" runat="server">
                                        <td style="width: 50%;">Propina: <asp:CheckBox ID="chkPropina" runat="server" AutoPostBack="true" OnCheckedChanged="chkPropina_CheckedChanged" /></td>
                                        <td>
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtPropina" runat="server" class="form-control BoxValor"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Propina" /></span>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Total a pagar:</td>
                                        <td>
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtTotalFactura" runat="server" class="form-control BoxValor"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Total IVA" /></span>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" style="text-align: center;">
                        <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/Documento/Cancelar.png" />
                        <asp:ImageButton ID="btnGuardar" CssClass="btnGuardar" runat="server" ImageUrl="~/Images/Documento/Guardar.png" OnClientClick="EsconderBoton();" OnClick="btnGuardar_Click" />
                        <asp:ImageButton ID="btnFacturar" Visible="false" runat="server" class="btnFacturar" ImageUrl="~/Images/Documento/Pasar.png" OnClick="btnFacturar_Click" />
                        <asp:ImageButton ID="btnPasar" Visible="false" runat="server" ToolTip="Pasar a Factura" class="btnFacturar" ImageUrl="~/Images/Documento/Pasar.png" OnClick="btnPasar_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <input type="hidden" id="hddTipoDocumento" runat="server"/>
                        <input type="hidden" id="hddCostoPonderado" runat="server" value="0"/>
                        <input type="hidden" id="hddPrecioCosto" runat="server" value="0"/>
                        <input type="hidden" id="hddIdDocumento" runat="server"/>
                        <input type="hidden" id="hddIdUsuario" runat="server" />
                        <input type="hidden" id="hddIdEmpresa" runat="server" />
                        <input type="hidden" id="hddBodegaPorDefectoUsuario" runat="server" />
                        <input type="hidden" id="hddIdEliminar" runat="server" />
                        <input type="hidden" id="hddLogin" runat="server" />
                        <input type="hidden" id="hddPassword" runat="server" />
                        <input type="hidden" id="hddDescuento" runat="server" />
                        <input type="hidden" id="hddResolucion" runat="server" />
                        <div class="Validador" id="divValidador" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <input type="text" id="txtLogin" name="txtLogin" class="form-control" placeholder="Login"/>
                                </div>
                                <div class="col-md-6">
                                    <input type="password" id="txtPassword" name="txtPassword" class="form-control" placeholder="Password"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <input type="button" onclick="EliminarDetalle()" class="btn btn-lg btn-primary btn-block" value="Validar" />
                                </div>
                            </div>
                        </div>
                        <div class="Descuento" id="divDescuento" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <input type="text" id="txtLoginDes" name="txtLogin" class="form-control" placeholder="Login"/>
                                </div>
                                <div class="col-md-6">
                                    <input type="password" id="txtPasswordDes" name="txtPassword" class="form-control" placeholder="Password"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="text-align:center">
                                    <input type="button" onclick="Descuento()" class="btn btn-lg btn-primary" value="Validar" />
                                    <input type="button" onclick="CancelarDescuento()" class="btn btn-lg btn-primary" value="Cancelar" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divFormaPago" class="tabcontent">
        <table style="width: 100%;" id="tblFormasPagos" runat="server">
            <tr>
                <td colspan="3">
                    <div id="Div1" style="border-color:gainsboro; border-style:solid;border-width:1px;padding:4px;">
                        <div class="row">
                            <div class="col-md-12" style="text-align:left">
                                <span style="font-size:medium;color:#1D70B7"><b>Saldos a Favor</b></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="text-align: left">
                                <asp:DataGrid ID="dgSaldos" runat="server" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="AdicionarPago" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Tipo" HeaderText="Tipo"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="IdFormaPago" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Id" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NumeroDocumento" HeaderText="Numero"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Saldo" HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="text-align:left">
                                <span style="font-size:medium;color:#1D70B7"><b>Formas de Pago</b></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlFormaPago" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="nombre" DataValueField="idFormaPago" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-4" style="text-align:center">
                                <asp:CheckBox ID="chkCredito" runat="server" Text="Documento a Crédito" />
                            </div>
                            <div class="col-md-1">
                                Fecha Vencimiento:
                            </div>
                            <div class="col-md-3">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtFechaVen" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Date.png" title="Fecha de vencimiento" /></span>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divEfectivo" runat="server">
                            <div class="col-md-1">
                                Valor:
                            </div>
                            <div class="col-md-3">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtValorE" runat="server" CssClass="BoxValor form-control Decimal"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total IVA" /></span>
                                </div>
                            </div>
                            <div class="col-md-3" style="text-align:center">
                                <asp:Button ID="btnAdicionarPago" CssClass="btn btn-info btn-sm btn-block" Text="Adicionar" runat="server" ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" OnClick="btnAdicionarPago_Click"></asp:Button>
                            </div>
                        </div>
                        <div class="row" id="divTarjetaCredito" runat="server" visible="false">
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlTipoTarjeta" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="Nombre" DataValueField="idTipoTarjetaCredito" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-md-1">Valor:</div>
                            <div class="col-md-3">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtValorTJ" runat="server" CssClass=" form-control BoxValorGrilla Decimal"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total IVA" /></span>
                                </div>
                            </div>
                            <div class="col-md-1">Voucher:</div>
                            <div class="col-md-2">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtVoucherTJ" runat="server" CssClass="form-control BoxValorGrilla"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Input/Nombre.png" title="Total IVA" /></span>
                                </div>
                            </div>
                            <div class="col-md-2" style="text-align:center">
                                <asp:Button ID="btnAdicionarTJ" runat="server" CssClass="btn btn-info btn-sm btn-block" Text="Adicionar" ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" OnClick="btnAdicionarPago_Click"></asp:Button>
                            </div>
                        </div>
                        <div class="row" id="divOtros" runat="server" visible="false">
                            <div class="col-md-1">Valor:</div>
                            <div class="col-md-3">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtValorO" runat="server" CssClass="form-control BoxValorGrilla Decimal"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total IVA" /></span>
                                </div>
                            </div>
                            <div class="col-md-1">Voucher:</div>
                            <div class="col-md-3">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtVoucherO" runat="server" CssClass="form-control BoxValorGrilla"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Input/Nombre.png" title="Total IVA" /></span>
                                </div>
                            </div>
                            <div class="col-md-2" style="text-align:center">
                                <asp:Button ID="btnAdicionarO" runat="server" CssClass="btn btn-info btn-sm btn-block" Text="Adicionar" ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" OnClick="btnAdicionarPago_Click"></asp:Button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:DataGrid ID="dgFormasPagos" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" OnItemCommand="dgFormasPagos_ItemCommand">
                                    <Columns>
                                        <asp:BoundColumn DataField="IdFormaPago" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FormaPago" HeaderText="Forma Pago"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Valor" HeaderText="Valor" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Voucher" HeaderText="Voucher"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="IdTipoTarjetaCredito" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TarjetaCredito" HeaderText="TarjetaCredito"></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" Text="<img src='Images/eliminar.png' Width='18' Height='18' title='Eliminar' />"></asp:ButtonColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                    <div style="padding-top:10px;padding-bottom:10px">
                        <div class="row">
                            <div class="col-md-1">Vr. Pago</div>
                            <div class="col-md-3">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtTotalPago" runat="server" CssClass="form-control BoxValor"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total IVA" /></span>
                                </div>
                            </div>
                            <div class="col-md-1">Restante</div>
                            <div class="col-md-3">
                                <div class = "input-group">
                                    <asp:TextBox ID="txtRestante" runat="server" CssClass="form-control BoxValor"></asp:TextBox>
                                    <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Total IVA" /></span>
                                </div>
                            </div>
                            <div class="col-md-1"><asp:Label ID="lblDevuelta" runat="server"></asp:Label></div>
                            <div class="col-md-3">
                                <asp:TextBox style="background-color:red" ForeColor="White" Font-Bold="true" ID="txtDevuelta" Text="Crédito" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="text-align:center">
                                <asp:ImageButton ID="imgCancelar" runat="server" ImageUrl="~/Images/Documento/Cancelar.png" />
                                <asp:ImageButton ID="imgGuardar" CssClass="btnGuardar" runat="server" ImageUrl="~/Images/Documento/Guardar.png" OnClientClick="EsconderBoton();" OnClick="btnGuardar_Click" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divRetenciones" class="tabcontent">
        <div class="row">
            <div class="col-lg-12">
                <div class="table-responsive">
                        <asp:DataGrid ID="dgRetenciones" runat="server" CssClass="table table-responsive table-striped table-hover" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:BoundColumn DataField="Id" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Codigo" HeaderText="Codigo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Descripcion" HeaderText="Descripcion"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Porcentaje" HeaderText="Porcentaje" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Base" HeaderText="Base" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
