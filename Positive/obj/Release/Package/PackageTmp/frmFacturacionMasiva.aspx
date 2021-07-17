<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmFacturacionMasiva.aspx.cs" Inherits="Inventario.frmFacturacionMasiva" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <br />
        <div class="col-md-12" style="text-align:center">
            <h3><b>Facturación masiva de clientes</b></h3>
            <input type="hidden" id="hddIdUsuario" runat="server" />
            <input type="hidden" id="hddIdEmpresa" runat="server" />
            <input type="hidden" id="hddTipoDocumento" runat="server" value="1"/>
            <input type="hidden" id="hddBodegaPorDefectoUsuario" runat="server" />
        </div>
        <br />
    </div>
    <div class="row">
        <div class="col-sm-1">
            <div class="input-group">
                Código:
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4">
            Articulo:
            <div class="input-group">
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                <input type="hidden" id="hddIdArticulo" runat="server" value="0" />
                <input type="hidden" id="hddIVA" runat="server" value="0" />
                <input type="hidden" id="hddEsInventario" runat="server" value="0" />
                <input type="hidden" id="hddPrecioSinIVA" runat="server" value="0" />
                <input type="hidden" id="hddValorDescuento" runat="server" value="0" />
                <input type="hidden" id="hddCostoPonderado" runat="server" value="0"/>
                <input type="hidden" id="hddPrecioCosto" runat="server" value="0"/>
                <span class="input-group-addon">
                <img src="Images/Input/Nombre.png" title="Artículo" /></span>
            </div>
        </div>
        <div class="col-sm-3">
            Bodega:
            <div class="input-group">
                <asp:TextBox ID="txtBodega" runat="server" CssClass="form-control"></asp:TextBox>
                <input type="hidden" id="hddIdBodega" runat="server" value="0" />
                <input type="hidden" id="hddCantidad" runat="server" value="0" />
                <span class="input-group-addon">
                <img src="Images/Input/Direccion.png" title="Bodega" /></span>
            </div>
        </div>
        <div class="col-sm-1">
            Cantidad:
            <div class="input-group">
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control BoxValorGrilla Decimal"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="row">
                <div class="col-sm-4">
                    <div class="input-group">
                        <asp:Label ID="lblDescuento" Text="Desc" runat="server"></asp:Label>:
                        <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control BoxValorGrilla Entero"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-8">
                    <div class="input-group">
                        <asp:Label ID="lblPrecio" Text="Valor" runat="server"></asp:Label>:
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control BoxValor Decimal"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" runat="server" ID="btnAdicionar" Height="40" Width="40" OnClick="btnAdicionar_Click" />
        <input type="hidden" id="hddIdEliminar" runat="server" />
        <input type="hidden" id="hddLogin" runat="server" />
        <input type="hidden" id="hddPassword" runat="server" />
        <input type="hidden" id="hddDescuento" runat="server" />
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
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <asp:DataGrid ID="dgFactura" BorderColor="#70B52B" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" >
                <Columns>
                    <asp:BoundColumn DataField="IdArticulo" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Codigo" HeaderText="Codigo"  ItemStyle-Width="5%"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Articulo" HeaderText="Articulo"  ItemStyle-Width="42%"></asp:BoundColumn>
                    <asp:BoundColumn DataField="IdBodega" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Bodega" HeaderText="Bodega" ItemStyle-Width="14%"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Cantidad" HeaderText="Cant" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ValorUnitario" HeaderText="V/U" ItemStyle-Width="9%" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Descuento" HeaderText="% Desc" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ValorUnitarioConDescuento" HeaderText="V/U Con Desc" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                    <asp:BoundColumn DataField="IVA" HeaderText="IVA" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ValorUnitarioConIVA" ItemStyle-Width="9%" HeaderText="V/U Con IVA" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalLinea" HeaderText="Total Linea" ItemStyle-Width="9%" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" Text="<img src='Images/eliminar.png' Width='18' Height='18' title='Eliminar' />"></asp:ButtonColumn>
                    <asp:BoundColumn DataField="CostoPonderado" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ValorDescuento" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="PrecioCosto" Visible="false" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </asp:DataGrid>
        </div>
    </div>
    <div class="row">
        <br />
        <div class="col-md-7" style="text-align:center">
            <b>Selección de clientes</b>
        </div>
        <div class="col-md-5" style="text-align:center">
            <b>Datos factura</b>
        </div>
        <br />
    </div>
    <div class="row">
        <div class="col-md-8" style="text-align:center">
            <div class="row">
                <div class="col-md-2">
                    Grupo Cliente:
                </div>
                <div class="col-md-10">
                    <asp:DropDownList ID="ddlGrupoCliente" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlGrupoCliente_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align:center">
                    <asp:DataGrid ID="dgClientes" BorderColor="#70B52B" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" >
                        <Columns>
                            <asp:BoundColumn DataField="IdTercero" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Identificacion" HeaderText="Identificacion"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nombre" HeaderText="Tercero"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Direccion" HeaderText="Direccion"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Telefono" HeaderText="Telefono"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Celular" HeaderText="Celular"></asp:BoundColumn>
                            <asp:BoundColumn DataField="idCiudad" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="<input type='checkbox' id='chkSelTodSel' data-elementos='chkSeleccionar' class='_selecionar' checked='true' />">
                                <ItemTemplate><asp:CheckBox ID="chkSeleccionar" runat="server" CssClass="chkSeleccionar" Checked="true" /></ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="row">
                <div class="col-md-6">
                    Antes de IVA:
                </div>
                <div class="col-md-6">
                    <div class = "input-group">
                        <asp:TextBox ID="txtAntesIVA" runat="server" class="form-control BoxValor"></asp:TextBox>
                        <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Antes de IVA" /></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    Descuento Por Artículos:
                </div>
                <div class="col-md-6">
                    <div class = "input-group">
                        <asp:TextBox ID="txtTotalDescuento" runat="server" class="form-control BoxValor"></asp:TextBox>
                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Antes de IVA" /></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    Valor IVA:
                </div>
                <div class="col-md-6">
                    <div class = "input-group">
                        <asp:TextBox ID="txtTotalIVA" runat="server" class="form-control BoxValor"></asp:TextBox>
                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Antes de IVA" /></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    Total a pagar:
                </div>
                <div class="col-md-6">
                    <div class = "input-group">
                        <asp:TextBox ID="txtTotalFactura" runat="server" class="form-control BoxValor"></asp:TextBox>
                        <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Antes de IVA" /></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align:center">
                    <asp:ImageButton ID="btnGuardar" CssClass="btnGuardar" runat="server" ImageUrl="~/Images/Documento/Guardar.png" OnClientClick="EsconderBoton();" OnClick="btnGuardar_Click" />
                    <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/Documento/Cancelar.png" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="input-group" style="width:100%">
                        Observaciones:
                        <asp:TextBox ID="txtObservaciones" Width="100%" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
