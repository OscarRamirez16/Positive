<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCodigoBarra.aspx.cs" Inherits="Inventario.frmCodigoBarra" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;">
        <tr style="text-align: center;">
            <td colspan="2">
                <h2><b>
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
                    <input type="hidden" runat="server" id="hddIdArticulo" value="0" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table style="width: 100%;">
                    <tr class="Tr">
                        <td style="text-align:center" colspan="2"><asp:Label ID="lblArticulo" Width="100%" runat="server"></asp:Label></td>
                    </tr>
                    <tr class="Tr">
                        <td>
                            <asp:TextBox ID="txtCodigo" CssClass="Box" runat="server"></asp:TextBox>
                            <input type="hidden" runat="server" id="hddIdCodigo" value="0" />
                        </td>
                        <td><asp:TextBox ID="txtDescripcion" CssClass="Box" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="Tr">
                        <td><asp:CheckBox ID="chkActivo" runat="server" /></td>
                        <td></td>
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
                            <asp:DataGrid ID="dgCodigos" runat="server" Width="100%" CssClass="table" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnItemCommand="dgCodigos_ItemCommand" >
                                <Columns>
                                    <asp:BoundColumn DataField="IdCodigo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CodigoBarra" HeaderText="CodigoBarra"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Descripcion" HeaderText="Descripción"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Activo" HeaderText="Activo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdArticulo" Visible="false"></asp:BoundColumn>
                                    <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" CommandName="Editar" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%; height: 50%; text-align: center;">
                <img src="Images/⁫listaprecio.png" />
            </td>   
        </tr>
    </table>
</asp:Content>
