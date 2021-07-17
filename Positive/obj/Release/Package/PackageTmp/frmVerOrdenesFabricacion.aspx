<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerOrdenesFabricacion.aspx.cs" Inherits="Inventario.frmVerOrdenesFabricacion" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;" class="table">
        <tr style="text-align: center;">
            <td><h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2></td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 200px;"><asp:Label ID="lblFechaInical" runat="server"></asp:Label>:</td>
                        <td><asp:TextBox ID="txtFechaInicial" runat="server" CssClass="Box"></asp:TextBox></td>
                        <td style="width: 200px"><asp:Label ID="lblFechaFinal" runat="server"></asp:Label>:</td>
                        <td><asp:TextBox ID="txtFechaFinal" runat="server" CssClass="Box"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width:15%">ID:</td>
                        <td style="width:35%"><asp:TextBox ID="txtIdOrden" runat="server" CssClass="Box"></asp:TextBox><input type="hidden" runat="server" id="hddIdArticulo" value="0"/></td>
                        <td style="width:15%"><asp:Label ID="lblUsuario" runat="server"></asp:Label>:</td>
                        <td style="width:35%"><asp:DropDownList ID="ddlUsuario" runat="server" CssClass="DropDownList" DataTextField="NombreCompleto" DataValueField="idUsuario"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align:center">
                            <asp:ImageButton ImageUrl="~/Images/btnbuscar.png" ToolTip="Buscar" runat="server" ID="btnBuscar" Height="40" Width="40" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><asp:DataGrid ID="dgOrdenes" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnItemCommand="dgOrdenes_ItemCommand" OnItemDataBound="dgOrdenes_ItemDataBound">
                    <Columns>
                        <asp:BoundColumn DataField="IdOrdenFabricacion" HeaderText="ID"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IdListaMateriales" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="FechaCreacion" HeaderText="Fecha Creación"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IdUsuario" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Usuario" HeaderText="Usuario"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IdEstado" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Estado" HeaderText="Estado"></asp:BoundColumn>
                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Cambiar Estado">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlEstado" AutoPostBack="false" DataTextField="Nombre" DataValueField="IdEstado" runat="server"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" Text="CambiarEstado" CommandName="CambiarEstado"></asp:ButtonColumn>
                        <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />" CommandName="Editar"></asp:ButtonColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
