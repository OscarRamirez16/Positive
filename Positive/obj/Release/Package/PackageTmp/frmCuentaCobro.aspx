<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCuentaCobro.aspx.cs" Inherits="Inventario.frmCuentaCobro" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script type="text/javascript">
        EstablecerMascaras();
    </script>
    <div class="row"><div class="col-lg-12"><br /><br /></div></div>
    <div class="row" runat="server" id="tblPrincipal">
        <div class="col-lg-8 col-lg-offset-2">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Cuenta de Cobro
                    <input type="hidden" id="hddIdUsuario" runat="server" />
                    <input type="hidden" runat="server" id="hddIdEmpresa" value="0" />
                    <input type="hidden" id="hddTipoDocumento" runat="server" value="1"/>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <span class="label label-warning" style="color:white">Numero</span>
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-lg-6">
                            <span class="label label-warning" style="color:white">Fecha</span>
                            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-9">
                            <span class="label label-warning" style="color:white">Cliente</span>
                            <asp:TextBox ID="txtTercero" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                            <input type="hidden" id="hddIdCliente" runat="server" />
                        </div>
                        <div class="col-lg-3">
                            <span class="label label-warning" style="color:white">Identificacion</span>
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-6">
                            <span class="label label-warning" style="color:white">Direccion</span>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            <span class="label label-warning" style="color:white">Telefono</span>
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                        
                        <div class="col-lg-3">
                            <span class="label label-warning" style="color:white">Ciudad</span>
                            <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                            <input type="hidden" id="hddIdCiudad" runat="server" />
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <span class="label label-warning" style="color:white">Concepto</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <textarea id="txtConcepto" runat="server" class="form-control" rows="3"></textarea>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-6">
                            <span class="label label-warning" style="color:white">Total</span>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control Decimal BoxValor" Width="100%"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" OnClientClick="return validarObligatorio();" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
