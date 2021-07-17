<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmProduccion.aspx.cs" Inherits="Inventario.frmProduccion" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row"><div class="col-lg-12"><br /></div></div>
    <div class="row">
        <div class="col-lg-4 col-lg-offset-4">
            <div class = "list-group">
                <a href="#" style="text-align:center" class="list-group-item active"><b style="font-size:medium;"><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></a>
                <a href="frmListaMateriales.aspx" class="list-group-item"><img src="Images/menulistamateriales.png" width="24px" title="Lista de Materiales" />&nbsp;<span>Lista de Materiales</span></a>
                <a href="frmOrdenFabricacion.aspx" class="list-group-item"><img src="Images/menuordenfabricacion.png" width="24px" title="Orden de Fabricación" />&nbsp;<span>Orden de Fabricación</span></a>
                <a href="frmVerListaMateriales.aspx" class="list-group-item"><img src="Images/searchusers.png" width="24px" title="Consultar Listas de Materiales" />&nbsp;<span>Consultar Listas de Materiales</span></a>
                <a href="frmVerOrdenesFabricacion.aspx" class="list-group-item"><img src="Images/almacen.png" width="24px" title="Consultar Ordenes de Fabricación" />&nbsp;<span>Consultar Ordenes de Fabricación</span></a>
            </div>
        </div>
    </div>
</asp:Content>
