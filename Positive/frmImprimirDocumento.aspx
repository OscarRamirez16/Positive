<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmImprimirDocumento.aspx.cs" Inherits="Inventario.frmImprimirDocumento" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager ID="smMovimientos" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <rsweb:ReportViewer ID="rvPos" ProcessingMode="Local" runat="server" Width="100%"></rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>