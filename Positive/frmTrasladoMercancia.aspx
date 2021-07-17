<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTrasladoMercancia.aspx.cs" Inherits="Inventario.frmTrasladoMercancia" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;">
        <tr style="text-align: center;">
            <td colspan="2">
                <h2><b>
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtArticulo" runat="server" CssClass="Box"></asp:TextBox>
                            <input type="hidden" id="hddIdArticulo" runat="server" value="0" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtBodegaOrigen" runat="server" CssClass="Box"></asp:TextBox>
                            <input type="hidden" id="hddIdBodegaOrigen" runat="server" value="0" />
                            <input type="hidden" id="hddCantidad" runat="server" value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="BoxValor Decimal"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtBodegaDestino" runat="server" CssClass="Box"></asp:TextBox>
                            <input type="hidden" id="hddIdBodegaDestino" runat="server" value="0" />
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="2">
                            <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" runat="server" ID="btnAdicionar" Height="40" Width="40" OnClick="btnAdicionar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
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
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblObservaciones" runat="server"></asp:Label>
                            <asp:TextBox TextMode="MultiLine" Width="90%" ID="txtObservaciones" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:50%; height:50%; text-align:center;">
                <img src="Images/bodega.png" />
            </td>
        </tr>
    </table>
</asp:Content>
