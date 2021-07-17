<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReporteFacturaCompra.aspx.cs" Inherits="Inventario.frmReporteFacturaCompra" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager ID="smMovimientos" runat="server"></asp:ScriptManager>
    <div class="row">
        <br />
        <div class="col-md-12" style="text-align:center">
            <b>Reporte Facturas de Compra</b>
        </div>
        <br />
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-1">Fecha Inicial:</div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Date.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-1">Fecha Final:</div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Date.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-2"></div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <rsweb:ReportViewer ID="rvPos" runat="server" Width="100%" ShowPrintButton="true"></rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
