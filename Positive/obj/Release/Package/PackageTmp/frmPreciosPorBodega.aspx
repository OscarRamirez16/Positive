<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPreciosPorBodega.aspx.cs" Inherits="Inventario.frmPreciosPorBodega" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;">
        <tr style="text-align: center;">
            <td colspan="2">
                <h2><b>
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
            </td>
        </tr>
        <tr>
            <td style="width: 70%">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center" colspan="2">
                            <h3><b>
                                <asp:Label ID="lblArticulo" runat="server"></asp:Label></b></h3>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><hr /></td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="2">
                            <asp:DataGrid ID="dgBodegas" runat="server" BorderColor="#70B52B" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" OnItemDataBound="dgBodegas_ItemDataBound">
                                <Columns>
                                    <asp:BoundColumn DataField="Descripcion" Visible="false"></asp:BoundColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="Box" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlTipoManejo" runat="server" CssClass="DropDownListGrilla" DataValueField="IdTipoManejoPrecio" DataTextField="Nombre" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="BoxValorGrilla Decimal" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td colspan="2">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click"/>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:30%; text-align:center;">
                <img src="Images/precios.png" />
            </td>
        </tr>
    </table>
</asp:Content>
