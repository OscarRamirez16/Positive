<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteArticulosPorBodega.aspx.cs" Inherits="Inventario.frmReporteArticulosPorBodega" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager ID="smMovimientos" runat="server"></asp:ScriptManager>
    <table class="table" style="width: 100%">
        <tr style="text-align: center;">
            <td colspan="4"><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b>
                <input type="hidden" runat="server" id="hddIdEmpresa" />
                <input type="hidden" runat="server" id="hddTipoDocumento" value="2" />
            </td>
        </tr>
        <tr>
            <td style="width: 200px"><asp:Label ID="lblBodega" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtBodega" CssClass="Box" runat="server"></asp:TextBox><input type="hidden" id="hddIdBodega" value="0" runat="server" /></td>
            <td style="width: 200px"><asp:Label ID="lblArticulo" runat="server"></asp:Label>:</td>
            <td>
                <asp:TextBox ID="txtNombreBus" runat="server" CssClass="Box"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 200px"><asp:Label ID="lblLinea" runat="server"></asp:Label>:</td>
            <td>
                <asp:DropDownList ID="ddlLineaBus" runat="server" CssClass="DropDownList" AutoPostBack="false" DataTextField="Nombre" DataValueField="idLinea"></asp:DropDownList></td>
            <td style="width: 200px"><asp:Label ID="lblTercero" runat="server"></asp:Label>:</td>
            <td>
                <asp:TextBox ID="txtProveedorBus" CssClass="Box" runat="server"></asp:TextBox><input type="hidden" value="0" id="hddIdProBus" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center">
                <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar"  Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar"  Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <rsweb:ReportViewer ID="rvPos" runat="server" Width="100%"></rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
