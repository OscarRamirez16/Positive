<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerPagoCliente.aspx.cs" Inherits="Inventario.VerPagoCliente" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table class="table" style="width: 100%">
        <tr class="tr" style="text-align: center;">
            <td colspan="2"><b>Pagos Por Cliente</b></td>
        </tr>
        <tr>
            <td colspan="2"><asp:DataGrid ID="dgPagos" runat="server" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" >
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="idPagoDetalle" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="idPago" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="idDocumento" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NumeroDocumento" HeaderText="No. Documento"></asp:BoundColumn>
                    <asp:BoundColumn DataField="valorAbono" HeaderText="Valor Abono"></asp:BoundColumn>
                    <asp:BoundColumn DataField="formaPago" HeaderText="Forma Pago"></asp:BoundColumn>
                </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar"  Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                <input type="image" src="Images/btnimprimir.png" height="40" id="btnImprimir" value="Imprimir Reporte" onclick="imprimirDataGrid('Pago Cliente', 'dgPagos')" />
            </td>
        </tr>
    </table>
</asp:Content>
