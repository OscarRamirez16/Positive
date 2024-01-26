<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTrasladoMercancia.aspx.cs" Inherits="Inventario.frmTrasladoMercancia" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-lg-6 col-lg-offset-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-5">
                            <asp:TextBox ID="txtArticulo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtArticulo_TextChanged"></asp:TextBox>
                            <input type="hidden" id="hddIdArticulo" runat="server" value="0" />
                        </div>
                        <div class="col-lg-5">
                            <asp:TextBox ID="txtBodegaOrigen" runat="server" CssClass="form-control"></asp:TextBox>
                            <input type="hidden" id="hddIdBodegaOrigen" runat="server" value="0" />
                            <input type="hidden" id="hddCantidad" runat="server" value="0" />
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-5">
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control Decimal"></asp:TextBox>
                        </div>
                        <div class="col-lg-5">
                            <asp:TextBox ID="txtBodegaDestino" runat="server" CssClass="form-control"></asp:TextBox>
                            <input type="hidden" id="hddIdBodegaDestino" runat="server" value="0" />
                        </div>
                        <div class="col-lg-2" style="text-align:center">
                            <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" runat="server" ID="btnAdicionar" Height="40" Width="40" OnClick="btnAdicionar_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12"><br />
                            <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                            <asp:TextBox TextMode="MultiLine" Width="100%" ID="txtObservaciones" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:DataGrid ID="dgTraslados" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnEditCommand="dgTraslados_EditCommand">
                                <Columns>
                                    <asp:BoundColumn DataField="IdArticulo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Articulo" HeaderText="Descripción"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Cantidad" HeaderText="Cantidad" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdBodegaOrigen" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="BodegaOrigen" HeaderText="Bod. Origen"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdBodegaDestino" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="BodegaDestino" HeaderText="Bod. Destino"></asp:BoundColumn>
                                    <asp:EditCommandColumn ItemStyle-HorizontalAlign="Center" EditText="<img src='Images/eliminar.png' Width='18' Height='18' title='Eliminar' />"></asp:EditCommandColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
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
