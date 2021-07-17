<%@ Page Title="Cajas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarConsultarCaja.aspx.cs" Inherits="Inventario.frmCrearEditarConsultarCaja" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgCajas");
        });
    </script>
    <div class="row"><div class="col-lg-12"><br /><br /></div></div>
    <div class="row">
        <div class="col-lg-8 col-lg-offset-2">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                    <input type="hidden" runat="server" id="hddIdCaja" value="0" />
                    <input type="hidden" runat="server" id="hddIdEmpresa" value="0"/>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtProximoValor" CssClass="form-control" runat="server" ToolTip="Proximo Valor"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtBodega" CssClass="form-control" runat="server"></asp:TextBox><input type="hidden" id="hddIdBodega" runat="server" />
                        </div>
                        <div class="col-lg-6">
                            <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtValorInicial" CssClass="form-control" runat="server" ToolTip="Valor Inicial"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtValorFinal" CssClass="form-control" runat="server" ToolTip="Valor Final"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:TextBox ID="txtResolucion" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblTituloGrilla" ForeColor="White" CssClass="label label-warning" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                    <asp:DataGrid ID="dgCajas" runat="server" CssClass="table table-responsive table-striped table-hover" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgCajas_EditCommand">
                                    <Columns>
                                        <asp:BoundColumn DataField="idCaja" HeaderText="ID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="nombre"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="idBodega" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Bodega"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ValorInicial" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ValorFinal" HeaderText="Valor Final"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ProximoValor" HeaderText="Proximo Valor"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Resolucion" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Activo" HeaderText="Activo"></asp:BoundColumn>
                                        <asp:EditCommandColumn ItemStyle-HorizontalAlign="Center" EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:EditCommandColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
