<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmListaMateriales.aspx.cs" Inherits="Inventario.frmListaMateriales" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-lg-6 col-lg-offset-3" >
          <div class="panel panel-default">
            <div class="panel-heading">
                <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                <input type="hidden" runat="server" id="hddIdListaMateriales" />
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-4"><asp:Label ID="lblArticulo" ForeColor="White" class="label label-warning" runat="server"></asp:Label></div>
                    <div class="col-lg-8">
                        <div class = "input-group">
                            <asp:TextBox ID="txtArticulo" CssClass="form-control" runat="server"></asp:TextBox>
                            <input type="hidden" runat="server" id="hddIdArticulo" />
                            <span class = "input-group-addon"><img src="Images/Input/Articulo.png" title="Articulo" /></span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4"><asp:Label ID="lblCantidadMaestro" ForeColor="White" class="label label-warning" runat="server"></asp:Label></div>
                    <div class="col-lg-4">
                        <div class = "input-group">
                            <asp:TextBox ID="txtCantidadMaestro" CssClass="form-control Decimal" Text="0" runat="server"></asp:TextBox>
                            <span class = "input-group-addon"><img src="Images/Input/Cantidad.png" title="Cantidad" /></span>
                        </div>
                    </div>
                    <div class="col-lg-2" style="padding-top:5px;">
                        <asp:CheckBox ID="chkActivo" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <hr />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <span class="label label-success" style="color:white;">Adicionar Materia Prima</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="lblArticuloMat" ForeColor="White" class="label label-warning" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-8">
                        <div class = "input-group">
                            <asp:TextBox ID="txtArticuloMat" CssClass="form-control" runat="server"></asp:TextBox>
                            <input type="hidden" runat="server" id="hddIdArticuloMat" />
                            <span class = "input-group-addon"><img src="Images/Input/Articulo.png" title="Articulo" /></span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:Label ID="lblCantidad" class="label label-warning" ForeColor="White" runat="server"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <div class = "input-group">
                            <asp:TextBox ID="txtCantidad" CssClass="form-control Decimal" runat="server"></asp:TextBox>
                            <span class = "input-group-addon"><img src="Images/Input/Cantidad.png" title="Cantidad" /></span>
                        </div>
                    </div>
                    <div class="col-lg-1" style="padding-top:4px;">
                        <asp:ImageButton ImageUrl="~/Images/adicionar.png" style="cursor:pointer;" ToolTip="Adicionar" runat="server" ID="btnAdicionar" Width="24px" OnClick="btnAdicionar_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <br />
                        <asp:DataGrid ID="dgMateriales" BorderColor="#70B52B" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgMateriales_EditCommand" >
                            <Columns>
                                <asp:BoundColumn DataField="IdArticulo" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Articulo"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Cantidad" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                <asp:EditCommandColumn ItemStyle-HorizontalAlign="Center" EditText="<img src='Images/eliminar.png' Width='18' Height='18' title='Eliminar' />"></asp:EditCommandColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:DataGrid>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <hr />
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                        <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                    </div>
                </div>
            </div>
        </div>  
        </div>
    </div>
</asp:Content>
