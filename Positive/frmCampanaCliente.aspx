<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCampanaCliente.aspx.cs" Inherits="Inventario.frmCampanaCliente" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div id="contenido" style="width: 100%">
            <ul id="lista">
                <li id="pestana1"><a href='#CampanaCliente'>
                    <asp:Label ID="liCampanaArticulo" runat="server"></asp:Label></a></li>
            </ul>
            <div id="CampanaCliente">
                <table style="width:100%">
                    <tr style="text-align:center" class="tr">
                        <td colspan="2"><b><asp:Label ID="lblTitulo" ForeColor="White" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td colspan="2"><b><asp:Label ID="lblCampana" runat="server"></asp:Label></b></td>
                    </tr>
                    <tr>
                        <td style="width:20%"><label id="IdCampanaClienteLabel" for="<% =lblIdCampana.ClientID %>" runat="server">ID:</label></td>
                        <td><asp:Label runat="server" ID="lblIdCampanaCliente">0</asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width:20%"><label id="lblExcluir" for="<% =chkExcluir.ClientID %>" runat="server"></label></td>
                        <td><asp:CheckBox ID="chkExcluir" runat="server" Checked="false" /></td>
                    </tr>
                    <tr>
                        <td style="width:20%"><label id="lblTipo" for="<% =ddlTipo.ClientID %>" runat="server"></label></td>
                        <td><asp:DropDownList ID="ddlTipo" runat="server">
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Grupo Cliente</asp:ListItem>
                                <asp:ListItem Value="2">Cliente</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trGrupoCliente" class="cssGrupoCliente" runat="server" style="display:none">
                        <td><label id="lblGrupoCliente" for="<% =ddlGrupoCliente.ClientID %>" runat="server"></label></td>
                        <td>
                            <asp:DropDownList ID="ddlGrupoCliente" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trCliente" runat="server" class="cssCliente" style="display:none">
                        <td>
                            <label id="lblCliente" for="<% = txtTercero.ClientID %>" runat="server">Cliente:</label>
                        </td>
                        <td><input id="hddIdTercero" type="hidden" runat="server" />
                            <asp:TextBox ID="txtTercero" runat="server" ></asp:TextBox></td>
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
    <input type="hidden" id="hddIdEmpresa" runat="server" />
    <script>
        $(document).ready(function () {
            MostrarTipoCampanaCliente($("#<%=ddlTipo.ClientID%>").get(0));
        });
        function MostrarTipoCampanaCliente(source) {
            if ($(source).val() == "0") {
                $("#<%=trGrupoCliente.ClientID%>").hide();
                $("#<%=trCliente.ClientID%>").hide();
            }
            else if ($(source).val() == "1") {
                $("#<%=trGrupoCliente.ClientID%>").show();
                $("#<%=trCliente.ClientID%>").hide();
            }
            else {
                $("#<%=trGrupoCliente.ClientID%>").hide();
                $("#<%=trCliente.ClientID%>").show();
            }
        }
    </script>
</asp:Content>
