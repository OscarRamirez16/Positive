<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCampanaArticulo.aspx.cs" Inherits="Inventario.frmCampanaArticulo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div id="contenido" style="width: 100%">
        <ul id="lista">
            <li id="pestana1"><a href='#CampanaArticulo'>
                <asp:Label ID="liCampanaArticulo" runat="server"></asp:Label></a></li>
        </ul>
        <div id="CampanaArticulo">
            <table style="width:100%">
                <tr style="text-align:center" class="tr">
                    <td colspan="2"><b><asp:Label ID="lblTitulo" ForeColor="White" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td colspan="2"><b><asp:Label ID="lblCampana" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="IdCampanaArticuloLabel" for="<% =lblIdCampana.ClientID %>" runat="server">ID:</label></td>
                    <td><asp:Label runat="server" ID="lblIdCampanaArticulo">0</asp:Label></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblExcluir" for="<% =chkExcluir.ClientID %>" runat="server"></label></td>
                    <td><asp:CheckBox ID="chkExcluir" runat="server" Checked="false" /></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblTipo" for="<% =ddlTipo.ClientID %>" runat="server"></label></td>
                    <td><asp:DropDownList ID="ddlTipo" runat="server">
                            <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            <asp:ListItem Value="1">Grupo Articulo</asp:ListItem>
                            <asp:ListItem Value="2">Articulo</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr id="trGrupoArticulo" class="cssGrupoArticulo" runat="server" style="display:none">
                    <td nowrap="nowrap"><label id="lblGrupoArticulo" for="<% =ddlGrupoArticulo.ClientID %>" runat="server"></label></td>
                    <td>
                        <asp:DropDownList ID="ddlGrupoArticulo" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trArticulo" runat="server" class="cssArticulo" style="display:none">
                    <td nowrap="nowrap">
                        <label id="lblArticulo" for="<% = txtArticulo.ClientID %>" runat="server">Articulo:</label>
                    </td>
                    <td><input id="hddIdArticulo" type="hidden" runat="server" />
                        <asp:TextBox ID="txtArticulo" runat="server" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblTipoDescuento" for="<% =ddlTipo.ClientID %>" runat="server"></label></td>
                    <td><asp:DropDownList ID="ddlTipoDescuento" runat="server">
                            <asp:ListItem Value="0" Selected="True">Descuento Porcentaje</asp:ListItem>
                            <%--<asp:ListItem Value="1">Descuento Valor</asp:ListItem>--%>
                            <asp:ListItem Value="2">Valor Fijo</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblValorDescuento" for="<% =ddlTipo.ClientID %>" runat="server"></label></td>
                    <td><asp:TextBox ID="txtValorDescuento" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ControlToValidate="txtValorDescuento" ID="rfvValorDescuento" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click"/>
                        <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            MostrarTipoCampanaArticulo($("#<%=ddlTipo.ClientID%>").get(0));
        });
        function MostrarTipoCampanaArticulo(source) {
            if ($(source).val() == "0") {
                $("#<%=trGrupoArticulo.ClientID%>").hide();
                $("#<%=trArticulo.ClientID%>").hide();
            }
            else if ($(source).val() == "1") {
                $("#<%=trGrupoArticulo.ClientID%>").show();
                $("#<%=trArticulo.ClientID%>").hide();
            }
            else {
                $("#<%=trGrupoArticulo.ClientID%>").hide();
                $("#<%=trArticulo.ClientID%>").show();
            }
        }
    </script>
</asp:Content>
