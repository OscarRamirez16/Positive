<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVendedor.aspx.cs" Inherits="Inventario.frmVendedor" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Label ID="lblTitulo" Font-Size="Large" ForeColor="White" class="label label-info" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <span>Datos del Vendedor</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" Font-Bold="true" ID="lblidVendedor"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtidVendedor" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Identificacion.png" title="Id" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" Font-Bold="true" ID="lblNombre"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Nombre.png" title="Id" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" Font-Bold="true" ID="lblComision"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtComision" CssClass="form-control decimal" runat="server"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/DolarAzulClaro.png" title="Comision" /></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" Font-Bold="true" ID="lblBodega"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtBodega" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Tienda.png" title="Almacén" /></span>
                            </div>
                            <input type="hidden" id="hddIdBodega" runat="server" value="0" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label runat="server" Font-Bold="true" ID="lblActivo"></asp:Label>
                        </div>
                        <div class="col-md-7">
                            <div class = "input-group">
                                <asp:CheckBox ID="chkActivo" Checked="true" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col-md-12">
                        <asp:ImageButton ImageUrl="~/Images/Documento/Guardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click"/>
                        <asp:ImageButton ImageUrl="~/Images/Documento/Cancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" CausesValidation="false" OnClick="btnCancelar_Click"/>
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
                    <span>Vendedores Creados</span>
                </div>
                <div class="panel-body">
                    <asp:DataGrid ID="dgVendedores" runat="server" CssClass="grdViewTable" BorderColor="#70B52B" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" OnItemCommand="dgVendedores_ItemCommand">
                        <Columns>
                            <asp:BoundColumn DataField="idVendedor" HeaderText="Codigo" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Comision" HeaderText="Comisión"></asp:BoundColumn>
                            <asp:BoundColumn DataField="idBodega" HeaderText="Bodega" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Activo" HeaderText="Activo" ItemStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"></asp:BoundColumn>
                            <asp:ButtonColumn ButtonType="LinkButton" CausesValidation="false" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                        </Columns>
                        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
                    </asp:DataGrid>
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
