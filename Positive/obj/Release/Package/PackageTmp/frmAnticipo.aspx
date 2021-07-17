<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAnticipo.aspx.cs" Inherits="Inventario.frmAnticipo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgPagos");
        });
    </script>
    <div class="panel panel-success">
        <div class="panel-heading">
            <b><asp:Label ID="lblTitulo" runat="server" Text="Anticipo Cliente"></asp:Label></b>
            <input type="hidden" runat="server" id="hddTipoDocumento" value="1" />
            <input type="hidden" runat="server" id="hddIdEmpresa" />
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-1">Cliente:</div>
                <div class="col-md-5">
                    <div class="input-group">
                        <asp:TextBox ID="txtTercero" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="input-group-addon">
                            <img src="Images/Input/SocioNegocio.png" title="Socio de Negocio" /></span>
                    </div>
                    <input type="hidden" id="hddIdCliente" runat="server" />
                </div>
                <div class="col-md-1">Identificación:</div>
                <div class="col-md-5">
                    <div class="input-group">
                        <asp:TextBox ID="txtIdentificacion" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="input-group-addon">
                            <img src="Images/Input/Identificacion.png" title="Identificacion" /></span>
                    </div>
                </div>
            </div>
            <div class="row"><div class="col-md-12"><br /></div></div>
            <div class="row">
                <div class="col-md-12" style="text-align:center">
                    <asp:DataGrid ID="dgPagos" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnItemDataBound="dgPagos_ItemDataBound">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="idFormaPago" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn ItemStyle-HorizontalAlign="Left" DataField="nombre" HeaderText="Forma Pago"></asp:BoundColumn>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Valor Pago">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValorPago" OnTextChanged="CalcularTotalPago" AutoPostBack="true" runat="server" CssClass="Decimal"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Num. Cheque">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNumeroCheque" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10" style="text-align:right;position:relative;top:5px">
                    <b>Total Anticipo:</b>
                </div>
                <div class="col-md-2" style="text-align:right">
                    <div class="input-group">
                        <asp:TextBox ID="txtTotalPago" runat="server" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-addon">
                            <img src="Images/Input/DolarAzul.png" title="Total Anticipo" /></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    Observaciones:
                    <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row" style="padding-top:10px;">
                <div class="col-md-12" style="text-align:center">
                    <asp:ImageButton ImageUrl="~/Images/btnguardar.png" CssClass="btnGuardar" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClientClick="EsconderBoton();" OnClick="btnGuardar_Click"  />
                    <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>