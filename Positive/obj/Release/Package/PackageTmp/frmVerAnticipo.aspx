<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerAnticipo.aspx.cs" Inherits="Inventario.frmVerAnticipo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <br />
        <div class="col-md-12" style="text-align:center">
            <b>Consultar Anticipos</b>
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
        <div class="col-md-1">ID Anticipo:</div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox ID="txtIdAnticipo" runat="server" CssClass="form-control"></asp:TextBox>
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
        <div class="col-md-2"></div>
        <div class="col-md-1">Cliente:</div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/SocioNegocio.png" title="ItemCode" /></span>
            </div>
        </div>
        <div class="col-md-6"></div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <asp:DataGrid ID="dgAnticipos" CssClass="table-responsive" runat="server" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="idPago" HeaderText="ID"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Identificacion" HeaderText="Identificacion"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Tercero" HeaderText="Tercero"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ValorPago" HeaderText="Valor Pago" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Fecha" HeaderText="Fecha"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Usuario" HeaderText="Usuario"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Estado" HeaderText="Estado"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Observaciones" HeaderText="Obs Documento." ItemStyle-Width="20%"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Saldo" HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ValorAplicado" HeaderText="Valor Aplicado" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NumeroDocumento" HeaderText="Nro. Documento"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalDocumento" HeaderText="Total Documento" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="Anular" Text="Anular"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="Imprimir" Text="Imprimir"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="ImprimirFormal" Text="<img src='Images/btnimprimir.png' Width='18' Height='18' title='Impresión Formal' />"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </div>
</asp:Content>
