<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListaPrecio.aspx.cs" Inherits="Inventario.frmListaPrecio" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;">
        <tr style="text-align: center;">
            <td colspan="2">
                <h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
                <input type="hidden" runat="server" id="hddIdEmpresa" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <table style="width: 100%;">
                    <tr class="Tr">
                        <td style="width: 200px">ID:<asp:Label ID="lblID" Width="300" runat="server"></asp:Label></td>
                        <td><asp:CheckBox ID="chkActivo" runat="server" /></td>
                    </tr>
                    <tr class="Tr">
                        <td><asp:TextBox ID="txtNombre" CssClass="Box" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtFactor" CssClass="Decimal BoxValor" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="Tr">
                        <td><asp:CheckBox ID="chkAumento" runat="server" /></td>
                        <td colspan="2"></td>
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
                            <asp:DataGrid ID="dgListas" runat="server" Width="100%" CssClass="table" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnItemCommand="dgListas_ItemCommand">
                                <Columns>
                                    <asp:BoundColumn DataField="IdListaPrecio" HeaderText="ID Lista"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Factor" HeaderText="Factor"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Activo" HeaderText="Activo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Aumento" HeaderText="Aumento"></asp:BoundColumn>
                                    <asp:ButtonColumn CommandName="Editar" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
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
