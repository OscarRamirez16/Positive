<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReportePagosPorProveedor.aspx.cs" Inherits="Inventario.frmReportePagosPorProveedor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table class="table" style="width: 100%">
        <tr style="text-align: center;">
            <td colspan="4"><h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
                <input type="hidden" runat="server" id="hddIdEmpresa" />
                <input type="hidden" runat="server" id="hddTipoDocumento" value="2" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px;"><asp:Label ID="lblFechaInical" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtFechaInicial" runat="server" CssClass="Box"></asp:TextBox></td>
            <td style="width: 200px"><asp:Label ID="lblFechaFinal" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtFechaFinal" runat="server" CssClass="Box"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 200px"><asp:Label ID="lblTercero" runat="server"></asp:Label>:</td>
            <td>
                <asp:TextBox ID="txtProveedorBus" CssClass="Box" runat="server"></asp:TextBox><input type="hidden" value="0" id="hddIdProveedorBus" runat="server" />
                <asp:RequiredFieldValidator ID="rfvProveedor" ControlToValidate="txtProveedorBus" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
            </td>
            <td colspan="2" style="text-align:center">
                <asp:ImageButton ID="btnBuscar" runat="server" CausesValidation="true" ToolTip="Buscar"  Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar"  Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                <input type="image" src="Images/btnimprimir.png" height='40' id="btnImprimir" value="Imprimir Reporte" onclick="imprimirDataGrid('Pagos Por Cliente', 'dgPagos')" />
            </td>
        </tr>
        <tr>
            <td colspan="4"><asp:DataGrid ID="dgPagos" runat="server" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnItemCommand="dgPagos_ItemCommand" >
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="idPago" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="idTercero" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Tercero" HeaderText="Proveedor"></asp:BoundColumn>
                    <asp:BoundColumn DataField="fechaPago" HeaderText="Fecha"></asp:BoundColumn>
                    <asp:BoundColumn DataField="totalPago" HeaderText="Valor Pago" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="Ver" ItemStyle-HorizontalAlign="Center" Text="<img src='Images/btnbuscar.png' Width='18' Height='18' title='Ver Detalles' />"></asp:ButtonColumn>
                </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
