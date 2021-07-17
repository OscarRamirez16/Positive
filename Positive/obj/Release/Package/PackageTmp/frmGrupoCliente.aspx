<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmGrupoCliente.aspx.cs" Inherits="Inventario.frmGrupoCliente" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <br />
    <div id="contenido" style="width: 100%">
        <ul class="tab">
            <li id="pestana1"><a id="aGrupoCliente" href='javascript:void(0)' class="tablinks active" onclick="ShowTab('aGrupoCliente', 'GrupoCliente')">
                <asp:Label ID="liGrupoCliente" runat="server"></asp:Label></a></li>
            <li id="pestana2"><a id="aBuscarGrupoCliente" href='javascript:void(0)' class="tablinks" onclick="ShowTab('aBuscarGrupoCliente', 'BuscarGrupoCliente')">
                    <asp:Label ID="liBuscarGrupoCliente" runat="server"></asp:Label></a></li>
        </ul>
        <div id="GrupoCliente" class="tabcontent" style="display:block">
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <b><label id="lblIdGrupoCliente" for="<% =txtIdGrupoCliente.ClientID %>" runat="server">Codigo</label></b>
                                </div>
                                <div class="col-lg-7">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtIdGrupoCliente" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/Identificacion.png" title="Nombre" /></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <b><label id="lblNombre" for="<% =txtNombre.ClientID %>" runat="server">Nombre</label></b>
                                </div>
                                <div class="col-lg-7">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/SocioNegocio.png" title="Nombre" /></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <b><label id="lblCuentaContable" for="<% =txtCuentaContable.ClientID %>" runat="server">Cuenta Contable</label></b>
                                </div>
                                <div class="col-lg-7">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtCuentaContable" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/DolarAzul.png" title="Nombre" /></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <asp:ImageButton ImageUrl="~/Images/Documento/Guardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click"/>
                    <asp:ImageButton ImageUrl="~/Images/Documento/Cancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" CausesValidation="false" OnClick="btnCancelar_Click"/>
                </div>
            </div>
        </div>
        <div id="BuscarGrupoCliente" class="tabcontent">
            <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <asp:Label ID="lblTituloBuscar" Text="Filtros" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <asp:Label ID="lblTexto" Font-Bold="true" Text="Texto" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-7">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtTexto" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/SocioNegocio.png" title="Nombre" /></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" CausesValidation="false" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
                                    <asp:ImageButton ID="btnCancelarBus" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelarBus_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            Resultados
                        </div>
                        <div class="panel-body">
                            <asp:DataGrid ID="dgGrupoCliente" CssClass="grdViewTable"  runat="server" BorderColor="#70B52B" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" OnItemCommand="dgGrupoCliente_ItemCommand">
                                <Columns>
                                    <asp:BoundColumn DataField="IdGrupoCliente" HeaderText="Codigo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CuentaContable" HeaderText="Cuenta Contable" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="LinkButton" CausesValidation="false" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                                </Columns>
                                <HeaderStyle CssClass="headerStyle"></HeaderStyle>
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
