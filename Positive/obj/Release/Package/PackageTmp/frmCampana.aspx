<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCampana.aspx.cs" Inherits="Inventario.frmCampana" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        var pintarBodegas = true;
        $(document).ready(function () {
            PintarTabla("cphContenido_dgCampanas");
            $("#pestana1").unbind("click");
            $("#pestana1").click(function () {
                if (pintarBodegas) {
                    pintarBodegas = false;
                    PintarTabla("cphContenido_dgCampanaArticulo");
                    PintarTabla("cphContenido_dgCampanaCliente");
                }
            });
        });
    </script>
    <div id="contenido" style="width: 100%">
        <ul id="lista">
            <li id="pestana1"><a href='#crearCampana'>
                <asp:Label ID="liCrearCampana" runat="server"></asp:Label></a></li>
            <li id="pestana2"><a href='#campanaCreadas'>
                <asp:Label ID="liCampanaCreadas" runat="server"></asp:Label></a></li>
        </ul>
        <div id="crearCampana">
            <div class="row">
                <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <b>Nro Campaña:</b>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Label runat="server" ID="lblIdCampana"></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" Checked="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <label id="lblNombre" for="<% = txtNombre.ClientID %>" runat="server">Nombre:</label>
                                </div>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <label id="lblFechaInicial" for="<% = txtFechaInicial.ClientID %>" runat="server">Fecha Inicial:</label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="form-control Fecha"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <label id="lblFechaFinal" for="<% = txtFechaFinal.ClientID %>" runat="server">Fecha Final:</label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="form-control Fecha"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <label id="lblHoraInicial" for="<% = txtHoraInicial.ClientID %>" runat="server">Hora Inicial:</label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtHoraInicial" runat="server" CssClass="Hora form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <label id="lblHoraFinal" for="<% = txtHoraFinal.ClientID %>" runat="server">Hora Final:</label>
                                </div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtHoraFinal" runat="server" CssClass="Hora form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2">
                                    <label id="lblObservacion" for="<% = txtObservacion.ClientID %>" runat="server">Observación:</label>
                                </div>
                                <div class="col-lg-10">
                                    <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" style="text-align:center">
                                    <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                                    <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label runat="server" ID="lblCampanaArticuloTitulo"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <asp:ImageButton ImageUrl="~/Images/btncrear.png" ToolTip="Adicionar Articulo" runat="server" ID="imgAdicionarArticulo" Height="40" Width="40" OnClick="imgAdicionarArticulo_Click"/><br />
                                </div>
                                <div class="col-lg-12">
                                    <asp:DataGrid ID="dgCampanaArticulo" runat="server" BorderColor="#70B52B" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" OnItemCommand="dgCampanaArticulo_ItemCommand">
                                        <Columns>
                                            <asp:BoundColumn DataField="idCampanaArticulo" HeaderText="Id"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Excluir" HeaderText="Excluir"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TipoCampanaArticuloNombre" HeaderText="Tipo"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Id" HeaderText="Código"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TipoDescuentoNombre" HeaderText="Tipo Descuento"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ValorDescuento" HeaderText="Valor"></asp:BoundColumn>
                                            <asp:ButtonColumn ButtonType="LinkButton" CausesValidation="false" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:Label runat="server" ID="lblCampanaClienteTitulo"></asp:Label>
                                </div>
                                <div class="col-lg-12">
                                    <asp:ImageButton ImageUrl="~/Images/btncrear.png" ToolTip="Adicionar Cliente" runat="server" ID="imgAdicionarCliente" Height="40" Width="40" OnClick="imgAdicionarCliente_Click"/><br />
                                </div>
                                <div class="col-lg-12">
                                    <asp:DataGrid ID="dgCampanaCliente" runat="server" BorderColor="#70B52B" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" OnItemCommand="dgCampabaCliente_ItemCommand">
                                        <Columns>
                                            <asp:BoundColumn DataField="idCampanaCliente" HeaderText="Id"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Excluir" HeaderText="Excluir"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TipoCampanaClienteNombre" HeaderText="Tipo"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Codigo" HeaderText="Código"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                                            <asp:ButtonColumn ButtonType="LinkButton" CausesValidation="false" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
        <div id="campanaCreadas">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblTituloBuscar" runat="server"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2">
                            <b>Nro Campaña:</b>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtIdCampana" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-2">
                            <label id="lblNombreBuscar" for="<% = txtNombreBuscar.ClientID %>" runat="server">Nombre:</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtNombreBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <label id="lblArticulo" for="<% = txtArticulo.ClientID %>" runat="server">Articulo:</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox ID="txtArticulo" runat="server" CssClass="form-control"></asp:TextBox>
                            <input type="hidden" runat="server" id="hddIdArticulo" />
                        </div>
                        <div class="col-lg-2">
                            <label id="Label1" for="<% = txtNombreBuscar.ClientID %>" runat="server">Estado:</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" CausesValidation="false" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                            <asp:ImageButton ID="btnCancelarBus" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:DataGrid ID="dgCampanas" runat="server" BorderColor="#70B52B" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" OnItemCommand="dgCampanas_ItemCommand">
                                <Columns>
                                    <asp:BoundColumn DataField="idCampana"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FechaInicial"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FechaFinal"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Activo"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="LinkButton" CausesValidation="false" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
