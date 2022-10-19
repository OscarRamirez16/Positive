<%@ Page Title="Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarRoles.aspx.cs" Inherits="Inventario.frmCrearEditarRoles" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <table style="width: 100%;">
        <tr style="text-align: center;">
            <td colspan="2">
                <h2><b>
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;"><b><asp:Label ID="lblInformacion" runat="server"></asp:Label></b></td>
            <td style="text-align: center;"><b><asp:Label ID="lblAsigacion" runat="server"></asp:Label></b></td>
        </tr>
        <tr>
            <td style="width: 50%; vertical-align: top;"">
                <table style="width: 100%;">
                    <tr>
                        <td>ID: <asp:Label runat="server" ID="lblIdRol"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: super;"><asp:TextBox ID="txtNombreRol" runat="server" CssClass="Box"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <asp:DataGrid ID="dgRolPaginas" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnItemDataBound="dgRolPaginas_ItemDataBound">
                    <Columns>
                        <asp:BoundColumn DataField="idPagina" HeaderText="ID"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:CheckBox ID="chkLeer" runat="server" CssClass="chkLeer" /></ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:CheckBox ID="chkInsertar" runat="server" CssClass="chkInsertar" /></ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:CheckBox ID="chkActualizar" runat="server" CssClass="chkActulizar" /></ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate><asp:CheckBox ID="chkEliminar" runat="server" CssClass="chkEliminar" /></ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:DataGrid>
            </td>
        </tr>
        <tr style="text-align: center;">
            <td colspan="2">
               <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40"  OnClick="btnGuardar_Click" />
               <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr style="text-align: center;" class="tr">
            <td colspan="2"><b><asp:Label ID="lblTituloGrilla" runat="server"></asp:Label></b></td>
        </tr>
        <tr>
            <td colspan="2"><asp:DataGrid ID="dgRoles" runat="server" Width="100%" CssClass="table" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" BorderColor="#70B52B" HeaderStyle-BackColor="#9D9C9C" OnEditCommand="dgRoles_EditCommand">
                    <Columns>
                        <asp:BoundColumn DataField="idRol" HeaderText="ID"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Empresa"></asp:BoundColumn>
                        <asp:BoundColumn DataField="idEmpresa" Visible="false"></asp:BoundColumn>
                         <asp:EditCommandColumn EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />" ItemStyle-HorizontalAlign="Center"></asp:EditCommandColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
