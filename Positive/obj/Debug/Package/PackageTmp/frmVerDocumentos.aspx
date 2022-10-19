<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerDocumentos.aspx.cs" Inherits="Inventario.frmVerDocumentos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgDocumentos");
        });
    </script>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <asp:Label ID="lblTipoDocumento" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-1"><asp:Label ID="lblFechaInical" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Date.png" title="Fecha Inicial" /></span>
                            </div>
                        </div>
                        <div class="col-md-1"><asp:Label ID="lblFechaFinal" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Date.png" title="Fecha Final" /></span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-1"><asp:Label ID="lblCliente" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Input/SocioNegocio.png" title="Cliente" /></span>
                            </div>
                        </div>
                        <div class="col-md-1"><asp:Label ID="lblIdentificacion" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Input/Identificacion.png" title="Identificación" /></span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-1"><asp:Label ID="lblDocumento" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Input/Identificacion.png" title="Dirección" /></span>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Cancelar"  Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ID="btnCancelar" runat="server" CausesValidation="false" ToolTip="Cancelar"  Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:DataGrid ID="dgDocumentos" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgDocumentos_EditCommand" OnItemCommand="dgDocumentos_ItemCommand">
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                <Columns>
                    <asp:BoundColumn DataField="idDocumento" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NumeroDocumento" HeaderText="Numero Documento"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Fecha" HeaderText="Fecha"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NombreTercero" HeaderText="Tercero"></asp:BoundColumn>
                    <asp:BoundColumn DataField="DatosTercero" HeaderText="Datos Tercero"></asp:BoundColumn>
                    <asp:BoundColumn DataField="IdEstado" Visible="false"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Estado" HeaderText="Estado"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TotalDocumento" HeaderText="Valor" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Observaciones" HeaderText="Obs Documento." ItemStyle-Width="20%"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Observación">
                        <ItemTemplate>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:EditCommandColumn CancelText="Cancelar" EditText="<img src='Images/btnbuscar.png' Width='18' Height='18' title='Consultar' />" UpdateText="Actualizar"></asp:EditCommandColumn>
                    <%--<asp:ButtonColumn CommandName="Anular" Text="Anular"></asp:ButtonColumn>--%>
                    <asp:ButtonColumn CommandName="Imprimir" Text="Imprimir"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="ImprimirFormal" Text="<img src='Images/btnimprimir.png' Width='18' Height='18' title='Impresión Formal' />"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="AnularFactura" Text="Anular"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
            <div id="divAlerta"></div>
            <input type="hidden" id="hddTipoDocumento" runat="server"/>
            <input type="hidden" id="hddIdUsuario" runat="server" />
            <input type="hidden" id="hddIdEmpresa" runat="server" />
            <input type="hidden" id="hddIdDocumento" />
        </div>
    </div>
</asp:Content>
