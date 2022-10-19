<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PedidosPendientes.aspx.cs" Inherits="Positive.PedidosPendientes" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <style>
        
        .card-header:first-child {
            border-radius: calc(.25rem - 1px) calc(.25rem - 1px) 0 0;
        }
        .card-header {
            padding: .75rem 1.25rem;
            margin-bottom: 0;
            background-color: rgba(0,0,0,.03);
            border-bottom: 1px solid rgba(0,0,0,.125);
        }
        .card-footer:last-child {
            border-radius: 0 0 calc(.25rem - 1px) calc(.25rem - 1px);
        }
        .liFooter {
            background-color: rgba(0,0,0,.03);
        }
        .liHeader {
            background-color: rgba(0,0,0,.03);
        }
        .delivery-order {
            cursor: pointer;
            padding-left:20px;
            float: right;
        }

        .delivery-order-item {
            cursor: pointer;
            padding-left:10px;
            width: 32px;
            float: right;
        }
    </style>
    <div class="row">
        <div class="col-md-12"><h1 style="color:#337AB7">Pedidos Pendientes</h1></div>
    </div>
    <div class="row">
        <div class="col-md-12" id="divPedidos" runat="server">
        </div>
    </div>
    <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnActualizar" runat="server" OnClick="btnActualizar_Click" />
    <input type="hidden" id="hddPedido" value="0" runat="server" />
    <script>
        setTimeout(function () {
            document.getElementById("<% = btnActualizar.ClientID %>").click();
        }, 30000);
        function EntregarCotizacionCocina(items) {
            document.getElementById("<% = hddPedido.ClientID %>").value = items;
            document.getElementById("<% = btnGuardar.ClientID %>").click();
        }
    </script>
</asp:Content>