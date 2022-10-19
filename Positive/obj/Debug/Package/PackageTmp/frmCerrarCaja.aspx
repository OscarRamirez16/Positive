<%@ Page Title="Cerrar Caja" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCerrarCaja.aspx.cs" Inherits="Inventario.frmCerrarCaja" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script type="text/javascript">
        EstablecerMascaras();
    </script>
    <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                    <input type="hidden" runat="server" id="hddIdEmpresa" value="0" />
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-1">
                            <asp:Label ID="lblCaja" runat="server" AssociatedControlID="ddlCaja"></asp:Label>
                        </div>
                        <div class="col-lg-5">
                            <asp:DropDownList ID="ddlCaja" runat="server" CssClass="form-control" DataTextField="nombre" DataValueField="idCaja"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1">
                            <asp:ImageButton ImageUrl="~/Images/btnbuscar.png" ToolTip="Buscar" runat="server" ID="btnBuscar" Height="30" Width="30" CausesValidation="false" OnClick="btnBuscar_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-success">
                                <div class="panel-heading">
                                    Entradas
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblSaldoInicial" runat="server" AssociatedControlID="txtSaldoInicial"></asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtSaldoInicial" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Saldo inicial" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblIngresos" runat="server" AssociatedControlID="txtIngresos"></asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtIngresos" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Ingresos" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblEfectivo" runat="server" AssociatedControlID="txtEfectivo">Efectivo</asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtEfectivo" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Efectivo" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblTarjetaCredito" runat="server" AssociatedControlID="txtTarjetaCredito">Tarjeta Credito</asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtTarjetaCredito" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Tarjeta Credito" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblTarjetaDebito" runat="server" AssociatedControlID="txtTarjetaDebito">Tarjeta Debito</asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtTarjetaDebito" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Tarjeta Debito" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblCheque" runat="server" AssociatedControlID="txtCheque">Cheque</asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtCheque" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Cheque" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblBonos" runat="server" AssociatedControlID="txtBonos">Bonos</asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtBonos" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Cheque" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblConsignacion" runat="server" AssociatedControlID="txtConsignacion">Consignación</asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtConsignacion" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Cheque" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-4"><asp:Label ID="lblNomina" runat="server" AssociatedControlID="txtNomina">Descuento Nomina</asp:Label></div>
                                        <div class="col-lg-8">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtNomina" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Nomina" /></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                </div>
                            </div>
                            <div class="row" id="divComisionesPorArticulos" runat="server" visible="false">
                                <div class="col-lg-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Comisiones Por Articulos
                                        </div>
                                        <div class="panel-body">
                                            <asp:DataGrid ID="dgComisiones" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:BoundColumn DataField="Vendedor" HeaderText="Vendedor"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Comision" HeaderText="Comision" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="Entero"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="Entero"></asp:BoundColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-danger">
                                        <div class="panel-heading">
                                            Salidas
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblRetiros" runat="server" AssociatedControlID="txtRetiros"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtRetiros" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Saldo inicial" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblCompras" runat="server" AssociatedControlID="txtCompras"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtCompras" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Compras" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Informativo
                                        </div>
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblPagosCreditos" runat="server" AssociatedControlID="txtPagoCreditos"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtPagoCreditos" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Compras" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblPropina" runat="server" AssociatedControlID="txtPropina"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtPropina" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Compras" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblRemision" runat="server" AssociatedControlID="txtRemision"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtRemision" Enabled="false" runat="server" CssClass="form-control BoxValor"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Remision" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblCreditos" runat="server" AssociatedControlID="txtCreditos"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtCreditos" Enabled="false" runat="server" CssClass="form-control BoxValor"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Creditos" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblPagoEfectivo" runat="server" AssociatedControlID="txtCreditos"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtPagoEfectivo" Enabled="false" runat="server" CssClass="form-control BoxValor"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Creditos" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-4"><asp:Label ID="lblOtrasFormasPago" runat="server" AssociatedControlID="txtCreditos"></asp:Label></div>
                                                <div class="col-lg-8">
                                                    <div class = "input-group">
                                                        <asp:TextBox ID="txtOtrasFormasPago" Enabled="false" runat="server" CssClass="form-control BoxValor"></asp:TextBox>
                                                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Creditos" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12"><asp:Label ID="lblTotalCuadre" ForeColor="White" CssClass="label label-info" runat="server" AssociatedControlID="txtTotalCuadre">Total Cuadre</asp:Label></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtTotalCuadre" CssClass="form-control BoxValor" Enabled="false" runat="server"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/DolarAmarillo.png" title="Total Cuadre" /></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12"><asp:Label ID="lblObservaciones" ForeColor="White" CssClass="label label-warning" runat="server" AssociatedControlID="txtObser"></asp:Label></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <textarea id="txtObser" runat="server" class="form-control" rows="2"></textarea>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12"><br /></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
