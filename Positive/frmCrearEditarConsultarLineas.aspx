<%@ Page Title="Lineas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarConsultarLineas.aspx.cs" Inherits="Inventario.frmCrearEditarLineas" %>
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
                        <td style="width: 50%">ID:&nbsp;&nbsp;
                            <asp:Label ID="lblID" runat="server"></asp:Label></td>
                        <td style="width: 50%">
                            <asp:TextBox ID="txtNombre" CssClass="Box" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="2">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr style="text-align: center;" class="tr">
                        <td colspan="2"><b><asp:Label ID="lblTituloGrilla" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DataGrid ID="dgLineas" BorderColor="#70B52B" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgLineas_EditCommand">
                                <Columns>
                                    <asp:BoundColumn DataField="idLinea" HeaderText="ID"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                                    <asp:EditCommandColumn EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />" ItemStyle-HorizontalAlign="Center"></asp:EditCommandColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; height: 50%; text-align: center;">
                <img src="Images/lineas.png" />
            </td>
        </tr>
    </table>
</asp:Content>
