<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmConciliacion.aspx.cs" Inherits="Inventario.frmConciliacion" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-md-12" style="text-align:center">
            <h2><b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b></h2>
            <input type="hidden" runat="server" id="hddTipoDocumento" value="1" />
            <input type="hidden" runat="server" id="hddIdEmpresa" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblTercero" runat="server"></asp:Label>:
                    <div class="input-group">
                        <asp:TextBox ID="txtTercero" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="input-group-addon">
                            <img src="Images/Input/SocioNegocio.png" title="Socio de Negocio" /></span>
                    </div>
                    <input type="hidden" id="hddIdCliente" runat="server" />
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblIdentificacion" runat="server"></asp:Label>:
                    <div class="input-group">
                        <asp:TextBox ID="txtIdentificacion" CssClass="form-control" runat="server" OnTextChanged="txtIdentificacion_TextChanged"></asp:TextBox>
                        <span class="input-group-addon">
                            <img src="Images/Input/Identificacion.png" title="Identificacion" /></span>
                    </div>
                </div>
                <div class="col-md-3"></div>
            </div>
            <div class="row" style="text-align:center">
                <div class="col-md-12"><h3>Documentos</h3></div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:DataGrid ID="dgSaldos" runat="server" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Seleccionar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionar" runat="server" AutoPostBack="true" OnCheckedChanged="AdicionarPago" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="Tipo" HeaderText="Tipo"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Id" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NumeroDocumento" HeaderText="Numero"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Saldo" HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8"></div>
                <div class="col-md-1">Total:</div>
                <div class="col-md-3">
                    <div class="input-group">
                        <asp:TextBox ID="txtTotal" CssClass="form-control BoxValor Entero" runat="server"></asp:TextBox>
                        <span class="input-group-addon">
                            <img src="Images/Input/DolarAmarillo.png" title="Total" /></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <br />
                    <asp:Label ID="lblObservaciones" runat="server"></asp:Label>:
                    <asp:TextBox ID="txtObservaciones" CssClass="form-control" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align:center">
                    <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click"  />
                    <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" />
                </div>
            </div>
        </div>
        <div class="col-md-4" style="text-align:center">
            <img src="Images/⁫listaprecio.png" />
        </div>
    </div>
</asp:Content>
