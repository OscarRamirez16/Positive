<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmImprimirCuadreDiario.aspx.cs" Inherits="Inventario.frmImprimirCuadreDiario" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <br />
        <div class="col-md-12" style="text-align:center">
            <b>Cuadre Diario de Caja</b>
            <input type="hidden" runat="server" id="hddTipoDocumento" value="1" />
            <input type="hidden" runat="server" id="hddIdEmpresa" />
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
        <div class="col-md-1"><asp:Label ID="lblUsuario" runat="server"></asp:Label></div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-control" DataTextField="NombreCompleto" DataValueField="idUsuario"></asp:DropDownList>
                <span class="input-group-addon">
                <img src="Images/Date.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-1"><asp:Label ID="lblCaja" runat="server"></asp:Label></div>
        <div class="col-md-3">
            <div class="input-group">
                <asp:DropDownList ID="ddlCaja" runat="server" CssClass="form-control" DataTextField="nombre" DataValueField="idCaja"></asp:DropDownList>
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
            <asp:DataGrid ID="dgCuadreDiario" runat="server" Width="100%" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" OnItemCommand="dgCuadreDiario_ItemCommand">
                <Columns>
                    <asp:BoundColumn DataField="idCuadreCaja" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="UsuarioCaja" HeaderText="Usuario"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Fecha" HeaderText="Fecha Apertura"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FechaCierre" HeaderText="Fecha Cierre"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SaldoInicial" HeaderText="Saldo Inicial" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Observaciones" HeaderText="Observaciones"></asp:BoundColumn>
                    <asp:ButtonColumn Text="Imprimir" CommandName="Imprimir"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </div>
</asp:Content>
