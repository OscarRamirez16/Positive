<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCambiarContrasena.aspx.cs" Inherits="Inventario.frmCambiarContrasena" %>

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
                            <asp:TextBox ID="txtNuevaCon" TextMode="Password" runat="server" CssClass="Box"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtConfirmarCon" TextMode="Password" runat="server" CssClass="Box"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtActualCon" runat="server" TextMode="Password" CssClass="Box"></asp:TextBox></td>
                        <td>
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; height: 50%; text-align: center;">
                <img src="Images/llave.png" />
            </td>
        </tr>
    </table>
</asp:Content>
