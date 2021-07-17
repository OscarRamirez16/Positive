<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerConciliacion.aspx.cs" Inherits="Inventario.frmVerConciliacion" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <br />
        <div class="col-md-12" style="text-align:center">
            <b>Consultar Conciliaciones</b>
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
        <div class="col-md-2"></div>
        <div class="col-md-1">Cliente:</div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Identificacion.png" title="ItemCode" /></span>
            </div>
        </div>
        <div class="col-md-1">Identificación:</div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Identificacion.png" title="Bodega"/></span>
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
            <rsweb:ReportViewer ID="rvPos" runat="server" Width="100%"></rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
