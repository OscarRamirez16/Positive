<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteMovimientosDiarios.aspx.cs" Inherits="Inventario.frmReporteMovimientosDiarios" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table class="table" style="width: 100%">
        <tr style="text-align: center;">
            <td colspan="4"><h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2></td>
        </tr>
        <tr>
            <td style="width: 200px;"><asp:Label ID="lblFechaInical" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtFechaInicial" runat="server" CssClass="Box"></asp:TextBox></td>
            <td style="width: 200px"><asp:Label ID="lblFechaFinal" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtFechaFinal" runat="server" CssClass="Box"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 200px"><asp:Label ID="lblUsuario" runat="server"></asp:Label>:</td>
            <td><asp:DropDownList ID="ddlUsuario" runat="server" CssClass="DropDownList" DataTextField="NombreCompleto" DataValueField="idUsuario"></asp:DropDownList></td>
            <td style="width: 200px"><asp:Label ID="lblTipoDocumento" runat="server"></asp:Label>:</td>
            <td><asp:DropDownList ID="ddlTipoMovimiento" runat="server" CssClass="DropDownList" DataTextField="Nombre" DataValueField="idTipoMovimiento"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center">
                <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar" CausesValidation="false" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                <input type="image" src="Images/btnimprimir.png" height='40' id="btnImprimir" value="Imprimir Reporte" onclick="imprimirDataGrid('Movimientos Diarios', 'dgMovimientosDiarios', false)" />
                <asp:ImageButton ID="btnExportarExcel" Visible="false" Text="Exportar Excel" runat="server" Height="40" Width="40" ImageUrl="~/Images/btnlimpiar.png" OnClick="btnExportarExcel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4"><asp:DataGrid ID="dgMovimientosDiarios" runat="server" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnItemDataBound="dgMovimientosDiarios_ItemDataBound">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="idMovimiento" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="fechaMovimiento" HeaderText="Fecha Mvto"></asp:BoundColumn>
                        <asp:BoundColumn DataField="observaciones" HeaderText="Observaciones"></asp:BoundColumn>
                        <asp:BoundColumn DataField="idTipoMovimiento" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TipoMovimiento" HeaderText="Tipo Movimiento"></asp:BoundColumn>
                        <asp:BoundColumn DataField="idUsuario" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Usuario" HeaderText="Usuario"></asp:BoundColumn>
                        <asp:BoundColumn DataField="valor" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="Valor"></asp:BoundColumn>
                        <asp:BoundColumn DataField="idEmpresa" Visible="false"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td colspan="3"></td>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right"><asp:Label ID="lbltotal" runat="server"></asp:Label>:</td>
                        <td style="text-align: right"><asp:TextBox ID="txtTotal" Text="0" CssClass="BoxValor" runat="server" Enabled="false"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
