<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCuentasPorCobrarPagar.aspx.cs" Inherits="Inventario.frmPagos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            //PintarTabla("cphContenido_dgDocumentos");
            PintarTabla("cphContenido_dgFormasPagos");
            PintarTabla("cphContenido_dgSaldos");
        });
    </script>
    <div class="row" id="divPrincipal" runat="server">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblFecha" runat="server"></asp:Label></b></div>
                                        <div class="col-md-10"><asp:Label ID="lblFechaActual" runat="server"></asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblTercero" runat="server"></asp:Label></b></div>
                                        <div class="col-md-9">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTercero" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtTercero_TextChanged"></asp:TextBox>
                                                <input type="hidden" id="hddIdCliente" runat="server" />
                                                <span class="input-group-addon">
                                                    <img src="Images/Input/SocioNegocio.png" title="Socio de Negocio" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblIdentificacion" runat="server"></asp:Label></b></div>
                                        <div class="col-md-9">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtIdentificacion" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <img src="Images/Input/Identificacion.png" title="Identificación" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblTelefono" runat="server"></asp:Label></b></div>
                                        <div class="col-md-9">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <img src="Images/Input/Telefono.png" title="Teléfono" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblDireccion" runat="server"></asp:Label></b></div>
                                        <div class="col-md-9">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <img src="Images/Input/Direccion.png" title="Dirección" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b>Devuelta</b></div>
                                        <div class="col-md-9"><asp:TextBox style="height:50px; font-size:25px;" ID="txtDevuelta" Width="100%" Height="100%" Text="Crédito" Font-Size="Large" runat="server" TextMode="MultiLine"></asp:TextBox></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><asp:CheckBox ID="chkVerTodos" runat="server" Text="&nbsp;&nbsp;Ver Todos" AutoPostBack="true" OnCheckedChanged="chkVerTodos_CheckedChanged" /></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="contenido" style="width: 100%">
                                        <ul id="lista">
                                            <li id="pestana1"><a href='#Div1'>Formas de pago</a></li>
                                            <li id="pestana2"><a href='#Div2'>Saldos a favor</a></li>
                                        </ul>
                                        <div id="Div1">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:DropDownList ID="ddlFormaPago" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="nombre" DataValueField="idFormaPago" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row" id="divEfectivo" runat="server">
                                                <div class="col-md-6">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtValorE" runat="server" CssClass="form-control Decimal" placeholder="Valor"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <img src="Images/Input/DolarAzul.png" title="Valor" /></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnAdicionarPago" Text="Adicionar" runat="server" ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" OnClick="btnAdicionarPago_Click"></asp:Button>
                                                </div>
                                            </div>
                                            <div class="row" id="divTarjetaCredito" runat="server" visible="false">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlTipoTarjeta" CssClass="form-control" runat="server" AutoPostBack="true" DataTextField="Nombre" DataValueField="idTipoTarjetaCredito" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtValorTJ" runat="server" CssClass="BoxValorGrilla Decimal form-control" placeholder="Valor"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <img src="Images/Input/DolarAzul.png" title="Valor" /></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtVoucherTJ" runat="server" CssClass="BoxValorGrilla form-control" placeholder="Voucher"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <img src="Images/Input/DolarAzul.png" title="Voucher" /></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnAdicionarTJ" runat="server" Text="Adicionar" ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" OnClick="btnAdicionarPago_Click"></asp:Button>
                                                </div>
                                            </div>
                                            <div class="row" id="divOtros" runat="server" visible="false">
                                                <div class="col-md-5">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtValorO" runat="server" CssClass="BoxValorGrilla Decimal form-control" placeholder="Valor"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <img src="Images/Input/DolarAzul.png" title="Valor" /></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="txtVoucherO" runat="server" CssClass="BoxValorGrilla form-control" placeholder="Voucher"></asp:TextBox>
                                                                <span class="input-group-addon">
                                                                    <img src="Images/Input/DolarAzul.png" title="Voucher" /></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnAdicionarO" runat="server" Text="Adicionar" ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" OnClick="btnAdicionarPago_Click"></asp:Button>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:DataGrid ID="dgFormasPagos" runat="server" AutoGenerateColumns="false" OnItemCommand="dgFormasPagos_ItemCommand">
                                                        <Columns>
                                                            <asp:BoundColumn DataField="IdFormaPago" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="FormaPago" HeaderText="Forma Pago"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Valor" HeaderText="Valor" ItemStyle-CssClass="Decimal"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Voucher" HeaderText="Voucher"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="IdTipoTarjetaCredito" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="TarjetaCredito" HeaderText="TarjetaCredito"></asp:BoundColumn>
                                                            <asp:ButtonColumn CommandName="Eliminar" ItemStyle-HorizontalAlign="Center" Text="<img src='Images/eliminar.png' Width='18' Height='18' title='Eliminar' />"></asp:ButtonColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="Div2">
                                            <asp:DataGrid ID="dgSaldos" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Sel" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="AdicionarPago" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="Tipo" HeaderText="Tipo"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IdFormaPago" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Id" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NumeroDocumento" HeaderText="Numero"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Saldo" HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="Decimal"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </div>
                                </div>
                                <div class="row"><div class="col-md-12"><br /></div></div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <b>Total Pago</b>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtTotalPago" runat="server" CssClass="BoxValor form-control"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <img src="Images/Input/DolarAzul.png" title="Teléfono" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <b>Restante</b>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtRestante" runat="server" CssClass="BoxValor form-control"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <img src="Images/Input/DolarAzul.png" title="Teléfono" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <b><asp:Label ID="lblTituloGrilla" runat="server"></asp:Label></b>
                                </div>
                                <div class="panel-body">
                                    <asp:DataGrid ID="dgDocumentos" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"  OnItemDataBound="dgDocumentos_ItemDataBound">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Sel" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="CalcularPagoTotal" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Tipo" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="idDocumento" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NumeroDocumento"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Fecha"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FechaVencimiento" HeaderText ="Fecha Vencimiento"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NombreTercero"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Referencia" HeaderText="Referencia"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Observaciones" HeaderText="Observaciones"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TotalDocumento" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="saldo" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                            <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-1">
                                    <b><asp:Label ID="lblObservaciones" runat="server"></asp:Label></b>
                                </div>
                                <div class="col-md-11">
                                    <asp:TextBox ID="txtObser" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row"><div class="col-md-12"><br /></div></div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-1">
                                    <b><asp:Label ID="lblTotal" runat="server"></asp:Label></b>
                                </div>
                                <div class="col-md-11">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtTotal" runat="server" Text="0,00" CssClass="BoxValor form-control"></asp:TextBox>
                                        <span class="input-group-addon">
                                            <img src="Images/Input/DolarAzul.png" title="Total" /></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row"><div class="col-md-12"><br /></div></div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:ImageButton ID="btnAplicar" Visible="false" runat="server" ToolTip="Aplicar Cambios"  Height="40" Width="40" ImageUrl="~/Images/btnlimpiar.png" OnClick="btnAplicar_Click" />
                            <asp:ImageButton ID="btnPagar" CssClass="btnGuardar" runat="server" ToolTip="Pagar" Height="40" Width="40" ImageUrl="~/Images/btnguardar.png" OnClientClick="EsconderBoton();" OnClick="btnPagar_Click" />
                            <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar"  Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                            <input type="hidden" id="hddIdEmpresa" runat="server"/>
                            <input type="hidden" id="hddTipoDocumento" runat="server"/>
                            <input type="hidden" id="hddValorPago" runat="server" value="0"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
