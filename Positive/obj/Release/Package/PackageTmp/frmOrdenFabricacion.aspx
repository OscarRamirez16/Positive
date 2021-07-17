<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmOrdenFabricacion.aspx.cs" Inherits="Inventario.frmOrdenFabricacion" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;">
        <tr style="text-align: center;">
            <td colspan="2">
                <h2><b>
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
                <input type="hidden" runat="server" id="hddIdOrden" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 15%">
                            <asp:Label ID="lblArticulo" runat="server"></asp:Label>:</td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtArticulo" CssClass="Box" runat="server"></asp:TextBox>
                            <input type="hidden" runat="server" id="hddIdListaMateriales" />
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="lblCantidadOrden" runat="server"></asp:Label>:</td>
                        <td style="width:35%">
                            <asp:TextBox ID="txtCantidadOrden" CssClass="BoxValor" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEstado" runat="server"></asp:Label>:</td>
                        <td>
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="DropDownList" DataTextField="Nombre" DataValueField="IdEstado"></asp:DropDownList>
                        </td>
                        <td colspan="2" style="text-align:center" id="tdBuscar" runat="server">
                            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar"  Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar"  Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"><hr /></td>
                    </tr>
                    <tr>
                        <td style="width:15%"><asp:Label ID="lblArticuloMat" runat="server"></asp:Label>:</td>
                        <td style="width:35%">
                            <asp:TextBox ID="txtArticuloMat" CssClass="Box" runat="server"></asp:TextBox>
                            <input type="hidden" runat="server" id="hddIdArticuloMat" />
                        </td>
                        <td style="width:15%"><asp:Label ID="lblCantidad" runat="server"></asp:Label>:</td>
                        <td style="width:35%">
                            <asp:TextBox ID="txtCantidad" CssClass="BoxValor" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="4">
                            <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Adicionar" runat="server" ID="btnAdicionar" Height="40" Width="40" OnClick="btnAdicionar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid ID="dgMateriales" BorderColor="#70B52B" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" OnItemCommand="dgMateriales_ItemCommand" OnItemDataBound="dgMateriales_ItemDataBound" >
                                <Columns>
                                    <asp:BoundColumn DataField="IdArticulo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Articulo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Cantidad" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlBodega" DataTextField="Descripcion" DataValueField="IdBodega" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:ButtonColumn Text="<img src='Images/eliminar.png' Width='18' Height='18' title='Eliminar' />" CommandName="Eliminar" ItemStyle-HorizontalAlign="Center"></asp:ButtonColumn>
                                </Columns>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="4">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click"/>
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="ImageButton1" Height="40" Width="40" CausesValidation="false" OnClick="ImageButton1_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:50%; height:50%; text-align:center;">
                <img src="Images/ordenfabricacion.png" />
            </td>
        </tr>
    </table>
</asp:Content>
