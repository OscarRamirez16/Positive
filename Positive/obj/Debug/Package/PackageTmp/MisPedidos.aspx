<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MisPedidos.aspx.cs" Inherits="Positive.MisPedidos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgDocumentos");
        });
    </script>
    <div class="row">
        <div class="col-md-12"><h1 style="color:#337AB7">Mis Pedidos</h1></div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <a href="frmVentaRapida.aspx?IdTipoDocumento=3"><img src="Images/adicionar.png" title="Crear Pedido" alt="Crear Pedido" width="32px" style="cursor:pointer" /></a></div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:DataGrid ID="dgDocumentos" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgDocumentos_EditCommand">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="idDocumento" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NumeroDocumento" HeaderText="Numero Documento"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Fecha" HeaderText="Fecha"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Nombre" HeaderText="Vendedor"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Observaciones" HeaderText="Obs Documento." ItemStyle-Width="20%"></asp:BoundColumn>
                    <asp:EditCommandColumn CancelText="Cancelar" EditText="<img src='Images/btnbuscar.png' Width='18' Height='18' title='Consultar' />" UpdateText="Actualizar"></asp:EditCommandColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </div>
</asp:Content>