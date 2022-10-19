<%@ Page Title="Articulos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarConsultarArticulos.aspx.cs" Inherits="Inventario.frmCrearEditarArticulos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        var pintarBodegas = true;
        $(document).ready(function () {
            PintarTabla("cphContenido_dgArticulos");
            $("#pestana1").unbind("click");
            $("#pestana1").click(function () {
                if (pintarBodegas) {
                    pintarBodegas = false;
                    PintarTabla("cphContenido_dgBodegas");
                }
                else {
                    PintarTabla("cphContenido_dgBodegas");
                }
            });
        });
    </script>
    <div id="contenido" style="width: 100%">
        <ul id="lista">
            <li id="pestana1"><a href='#crearArticulos'>
                <asp:Label ID="liCrearArticulo" runat="server"></asp:Label></a></li>
            <li id="pestana2"><a href='#articulosCreados'>
                <asp:Label ID="liArticulosCreados" runat="server"></asp:Label></a></li>
        </ul>
        <div id="crearArticulos" style="width: 100%">
            <div class="row">
                <div class="col-lg-10 col-lg-offset-1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                            <input type="hidden" runat="server" id="hddIdEmpresa" />
                            <input type="hidden" runat="server" id="hddTipoDocumento" value="2" />
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    ID
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label ID="lblIdArticulo" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                                </div>
                                <div class="col-lg-2"><asp:CheckBox ID="chkInventario" runat="server" AutoPostBack="true" OnCheckedChanged="chkInventario_CheckedChanged" /></div>
                                <div class="col-lg-2"><asp:CheckBox ID="chkPrecioAuto" runat="server" Visible="false" /></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Label ID="lblNombre" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtNombre" ID="rfvNombre" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblPresentacion" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtPresentacion" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtPresentacion" ID="rfvPresentacion" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/Articulo.png" id="imgUltimoCodigo" runat="server" /></span>
                                    </div>
                                    <asp:RequiredFieldValidator ControlToValidate="txtCodigo" ForeColor="Red" ID="rfvCodigo" Text="*" runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblLinea" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="ddlLinea" runat="server" CssClass="form-control" AutoPostBack="false" DataTextField="Nombre" DataValueField="idLinea"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Label ID="lblCodigoBarra" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtCodigoBarra" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblTercero" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtTercero" CssClass="form-control" runat="server"></asp:TextBox><input type="hidden" id="hddIdTercero" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtTercero" ID="rfvTercero" Text="*" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Label ID="lblIVACompra" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtIVACompra" CssClass="form-control Decimal" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblIVAVenta" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtIVAVenta" CssClass="form-control Decimal" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <asp:Label ID="lblStock" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtStockMinimo" CssClass="form-control Decimal" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Label ID="lblBodega" runat="server"></asp:Label>

                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtBodega" CssClass="form-control" runat="server"></asp:TextBox><input type="hidden" id="hddIdBodega" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtBodega" ID="rfvBodega" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblUbicacion" runat="server"></asp:Label></div>
                                <div class="col-lg-4"><asp:TextBox ID="txtUbicacion" CssClass="form-control" runat="server"></asp:TextBox></div>
                                <div class="col-lg-2">Costo Ponderado</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtCostoPonderado" CssClass="form-control Decimal" runat="server"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">Marca</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtMarca" CssClass="form-control" runat="server"></asp:TextBox></div>
                                <div class="col-lg-2">Porcentaje Comisión</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtPorcentajeComision" CssClass="form-control Decimal" runat="server"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            Información de Articulo Compuesto
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-3"><asp:CheckBox ID="chkEsCompuesto" AutoPostBack="true" runat="server" Text="Es Compuesto" OnCheckedChanged="chkEsCompuesto_CheckedChanged"/></div>
                                                <div class="col-lg-3"><asp:CheckBox ID="chkEsArticuloFinal" AutoPostBack="true" runat="server" Text="Es Articulo Final" OnCheckedChanged="chkEsArticuloFinal_CheckedChanged"/></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            Información de Padre-Hijo
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <asp:CheckBox ID="chkEsHijo" runat="server" AutoPostBack="true" Text="Es Hijo" OnCheckedChanged="chkEsHijo_CheckedChanged"/>
                                                </div>
                                                <div class="col-lg-1">Padre</div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtPadre" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    <input type="hidden" id="hddIdPadre" runat="server" />
                                                </div>
                                                <div class="col-lg-2">Cantidad Padre</div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtCantidadPadre" CssClass="form-control Decimal3" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <asp:Label ID="lblTituloGrilla" runat="server"></asp:Label>
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:DataGrid ID="dgBodegas" runat="server" Width="100%" AutoGenerateColumns="False" OnItemDataBound="dgBodegas_ItemDataBound" OnEditCommand="dgBodegas_EditCommand">
                                                        <Columns>
                                                            <asp:BoundColumn DataField="idBodega" HeaderText="ID" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Descripcion" HeaderText="Bodega"></asp:BoundColumn>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" ReadOnly="true" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlTipoManejo" runat="server" CssClass="form-control" DataValueField="IdTipoManejoPrecio" DataTextField="Nombre" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control Decimal" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control Decimal" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkBodega" runat="server"  AutoPostBack="true" OnCheckedChanged="ValidarCantidadBodega" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:EditCommandColumn ItemStyle-HorizontalAlign="Center" EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Fijar Precios Alternos' />" HeaderText="Editar"></asp:EditCommandColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                                    <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="articulosCreados">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblListaArticulos" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtNombreBus" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-4">
                            <asp:DropDownList ID="ddlLineaBus" runat="server" CssClass="form-control" AutoPostBack="false" DataTextField="Nombre" DataValueField="idLinea"></asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtProveedorBus" CssClass="form-control" runat="server"></asp:TextBox><input type="hidden" value="0" id="hddIdProBus" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <br />
                            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" CausesValidation="false" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ID="btnCancelarBus" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:DataGrid ID="dgArticulos" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" OnEditCommand="dgArticulos_EditCommand" OnItemDataBound="dgArticulos_ItemDataBound" OnItemCommand="dgArticulos_ItemCommand">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn HeaderText="No."></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idArticulo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CodigoArticulo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Presentacion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idLinea" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Linea"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IVACompra" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IVAVenta" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CodigoBarra" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idTercero" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Tercero"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idEmpresa" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Empresa" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idBodega" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Bodega" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="EsInventario"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="StockMinimo" ItemStyle-CssClass="BoxValorGrilla Decimal" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Cantidad" ItemStyle-CssClass="BoxValorGrilla Decimal3" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Activo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PrecioAutomatico" Visible="false"></asp:BoundColumn>
                                    <asp:EditCommandColumn ItemStyle-HorizontalAlign="Center" EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />" ></asp:EditCommandColumn>
                                    <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" CommandName="CodigoBarra" Text="<img src='Images/barcode.png' Width='18' Height='18' title='Codigos de barras' />"></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

