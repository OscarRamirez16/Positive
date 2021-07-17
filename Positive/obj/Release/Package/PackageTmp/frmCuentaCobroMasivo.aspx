<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCuentaCobroMasivo.aspx.cs" Inherits="Inventario.frmCuentaCobroMasivo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <br />
        <div class="col-md-12" style="text-align:center">
            <h3><b>Cuenta de cobro masiva de clientes</b></h3>
            <input type="hidden" id="hddIdUsuario" runat="server" />
            <input type="hidden" id="hddIdEmpresa" runat="server" />
            <input type="hidden" id="hddTipoDocumento" runat="server" value="1"/>
            <input type="hidden" id="hddBodegaPorDefectoUsuario" runat="server" />
        </div>
        <br />
    </div>
    <div class="row">
        <div class="col-lg-8 col-lg-offset-2">
            <div class="col-lg-12">
                <span class="label label-warning" style="color:white">Concepto</span>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <textarea id="txtConcepto" runat="server" class="form-control" rows="3"></textarea>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <br />
        <div class="col-md-7" style="text-align:center">
            <b>Selección de clientes</b>
        </div>
        <div class="col-md-5" style="text-align:center">
            <b>Datos</b>
        </div>
        <br />
    </div>
    <div class="row">
        <div class="col-md-8" style="text-align:center">
            <div class="row">
                <div class="col-md-2">
                    Grupo Cliente:
                </div>
                <div class="col-md-10">
                    <asp:DropDownList ID="ddlGrupoCliente" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlGrupoCliente_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" style="text-align:center">
                    <asp:DataGrid ID="dgClientes" BorderColor="#70B52B" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" >
                        <Columns>
                            <asp:BoundColumn DataField="IdTercero" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Identificacion" HeaderText="Identificacion"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nombre" HeaderText="Tercero"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Direccion" HeaderText="Direccion"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Telefono" HeaderText="Telefono"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Celular" HeaderText="Celular"></asp:BoundColumn>
                            <asp:BoundColumn DataField="idCiudad" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderText="<input type='checkbox' id='chkSelTodSel' data-elementos='chkSeleccionar' class='_selecionar' checked='true' />">
                                <ItemTemplate><asp:CheckBox ID="chkSeleccionar" runat="server" CssClass="chkSeleccionar" Checked="true" /></ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="row">
                <div class="col-md-6">
                    Total a pagar:
                </div>
                <div class="col-md-6">
                    <div class = "input-group">
                        <asp:TextBox ID="txtTotalFactura" runat="server" class="form-control BoxValor Decimal"></asp:TextBox>
                        <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Antes de IVA" /></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align:center">
                    <asp:ImageButton ID="btnGuardar" CssClass="btnGuardar" runat="server" ImageUrl="~/Images/Documento/Guardar.png" OnClientClick="EsconderBoton();" OnClick="btnGuardar_Click" />
                    <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/Documento/Cancelar.png" OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
