<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteDocumentosRangoFecha.aspx.cs" Inherits="Inventario.frmReporteDocumentosRangoFecha" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager ID="smMovimientos" runat="server"></asp:ScriptManager>
    <table class="table" style="width: 100%">
        <tr style="text-align: center;">
            <td colspan="4"><h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2></td>
        </tr>
        <tr>
            <td style="width: 200px;"><asp:Label ID="lblFechaInical" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtFechaInicial" runat="server" CssClass="Box"></asp:TextBox></td>
            <td style="width: 200px"><asp:Label ID="lblFechaFinal" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtFechaFinal" runat="server" CssClass="Box"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 200px"><asp:Label ID="lblTipoDocumento" runat="server"></asp:Label>:</td>
            <td><asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="DropDownList" DataTextField="Nombre" DataValueField="idTipoDocumento"></asp:DropDownList></td>
            <td style="width: 200px"><asp:Label ID="lblBodega" runat="server"></asp:Label>:</td>
            <td><asp:TextBox ID="txtBodega" CssClass="Box" runat="server"></asp:TextBox><input type="hidden" id="hddIdBodega" value="0" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center">
                <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar" CausesValidation="false" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <rsweb:ReportViewer ID="rvPos" runat="server" Width="100%"></rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
