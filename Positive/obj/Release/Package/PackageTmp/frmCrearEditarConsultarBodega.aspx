<%@ Page Title="Bodegas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarConsultarBodega.aspx.cs" Inherits="Inventario.frmCrearEditarBodega" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;">
        <tr style="text-align: center;">
            <td colspan="2">
                <h2><b>
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
                    <input type="hidden" runat="server" id="hddIdBodega" value="0" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width:50%">
                            <asp:TextBox ID="txtNombre" CssClass="Box" runat="server"></asp:TextBox></td>
                        <td style="width:50%">
                            <asp:TextBox ID="txtDireccion" CssClass="Box" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><br /></td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="2">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td><br /></td>
                    </tr>
                    <tr style="text-align: center;" class="tr">
                        <td colspan="2"><b><asp:Label ID="lblTituloGrilla" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:DataGrid ID="dgBodegas" BorderColor="#70B52B" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgBodegas_EditCommand">
                                <Columns>
                                    <asp:BoundColumn DataField="idBodega" HeaderText="ID"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Descripcion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Direccion"></asp:BoundColumn>
                                    <asp:EditCommandColumn ItemStyle-HorizontalAlign="Center" EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:EditCommandColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:DataGrid>
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
