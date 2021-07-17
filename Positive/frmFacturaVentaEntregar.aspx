<%@  Page Title="Entregar Pedidos" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="frmFacturaVentaEntregar.aspx.cs" Inherits="Inventario.frmFacturaVentaEntregar" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width:100%;margin-left:10px;margin-right:10px;">
        <tr>
            <td>
                <table><tr><td><h3 style="color:#1D70B7;font-weight:bold;">&nbsp;&nbsp;Pedidos</h3></td><td>&nbsp;&nbsp;&nbsp;<img src="Images/refresh.png" title="Actualizar" style="cursor:pointer" onclick="document.getElementById('<% = btnActualizar.ClientID %>').click();" /></td></tr></table>
            </td>
        </tr>
        <tr>
            <td id="tdFacturas" runat="server" style="width:100%"></td>
        </tr>
    </table>
    <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnActualizar" runat="server" OnClick="btnActualizar_Click" />
    <input type="hidden" id="hddPedido" value="0" runat="server" />
    <script>
        setTimeout(function () {
            document.getElementById("<% = btnActualizar.ClientID %>").click();
        },30000);
        function EntregarPedido(btnGuardarID,hddPedidoID, Pedido) {
            document.getElementById(hddPedidoID).value = Pedido;
            document.getElementById(btnGuardarID).click();
        }
    </script>
</asp:Content>