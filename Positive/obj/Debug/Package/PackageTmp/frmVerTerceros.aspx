<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerTerceros.aspx.cs" Inherits="Inventario.frmVerClientes" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgClientes");
        });
    </script>
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblTitulo" Font-Size="Large" ForeColor="White" class="label label-info" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <span>Filtros</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <span><b>Nombre</b></span>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox><input type="hidden" runat="server" id="hddIdTercero" value="-1" />
                                <span class = "input-group-addon"><img src="Images/Input/SocioNegocio.png" title="Nombre" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><b>Identificación</b></span>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtCedula" CssClass="form-control" runat="server"></asp:TextBox><input type="hidden" id="hddIdEmpresa" runat="server" />
                                <span class = "input-group-addon"><img src="Images/Input/Identificacion.png" title="Nombre" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><b>Tipo</b></span>
                        </div>
                        <div class="col-lg-7">
                            <asp:DropDownList ID="ddlTipoTercero" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:ImageButton ImageUrl="~/Images/btnbuscar.png" ToolTip="Buscar" runat="server" ID="btnBuscar" Height="40" Width="40"  OnClick="btnBuscar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <span>Resultados</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:DataGrid ID="dgClientes" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgClientes_EditCommand" OnItemDataBound="dgClientes_ItemDataBound">
                            <Columns>
                                <asp:BoundColumn HeaderText="No"></asp:BoundColumn>
                                <asp:BoundColumn DataField="idTercero" HeaderText="ID" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Identificacion"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TipoTercero" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="GrupoCliente"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Telefono" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Mail" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Direccion" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Ciudad" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Empresa" Visible="false"></asp:BoundColumn>
                                <asp:EditCommandColumn EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />" ItemStyle-HorizontalAlign="Center"></asp:EditCommandColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" CssClass="headerStyle"></HeaderStyle>
                        </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(
            function () {
                setGridResponsive();
            }
        );
    </script>
</asp:Content>
