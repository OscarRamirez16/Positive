<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerListaMateriales.aspx.cs" Inherits="Inventario.frmVerListaMateriales" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;" class="table">
        <tr style="text-align: center;">
            <td><h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2></td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width:15%"><asp:Label ID="lblArticulo" runat="server"></asp:Label>:</td>
                        <td style="width:35%"><asp:TextBox ID="txtArticulo" runat="server" CssClass="Box"></asp:TextBox><input type="hidden" runat="server" id="hddIdArticulo" value="0"/></td>
                        <td style="width:15%"><asp:Label ID="lblUsuario" runat="server"></asp:Label>:</td>
                        <td style="width:35%"><asp:DropDownList ID="ddlUsuario" runat="server" CssClass="DropDownList" DataTextField="NombreCompleto" DataValueField="idUsuario"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:center">
                            <asp:ImageButton ImageUrl="~/Images/btnbuscar.png" ToolTip="Buscar" runat="server" ID="btnBuscar" Height="40" Width="40"  OnClick="btnBuscar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><asp:DataGrid ID="dgListaMateriales" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnEditCommand="dgListaMateriales_EditCommand">
                    <Columns>
                        <asp:BoundColumn DataField="IdListaMateriales" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IdArticulo" HeaderText="ID"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Articulo"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Fecha"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IdUsuario" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Usuario"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IdEmpresa" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Activo"></asp:BoundColumn>
                        <asp:EditCommandColumn EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />" ItemStyle-HorizontalAlign="Center"></asp:EditCommandColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
