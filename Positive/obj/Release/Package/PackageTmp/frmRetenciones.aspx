<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRetenciones.aspx.cs" Inherits="Positive.frmRetenciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgRetenciones");
        });
    </script>
    <div class="col-lg-10 col-lg-offset-1">
        <div class="panel panel-default">
            <div class="panel-heading">
                RETENCIONES
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-2">ID:</div>
                    <div class="col-lg-4"><asp:Label ID="lblID" runat="server"></asp:Label></div>
                    <div class="col-lg-6"><asp:CheckBox ID="chkActivo" runat="server" Text="Activo" /></div>
                </div>
                <div class="row">
                    <div class="col-lg-2">Codigo:</div>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-2">Descripcion:</div>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2">Porcentaje:</div>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control Decimal"></asp:TextBox>
                    </div>
                    <div class="col-lg-2">Base:</div>
                    <div class="col-lg-4">
                        <asp:TextBox ID="txtBase" runat="server" CssClass="form-control Decimal"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12" style="text-align:center">
                        <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click" />
                        <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                    </div>
                </div>
                <div class="row"><div class="col-lg-12"><br /></div></div>
                <div class="row">
                    <div class="col-lg-12" >
                        <asp:Label ID="lblTituloGrilla" runat="server" ForeColor="White" CssClass="label label-warning">Lista de retenciones</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                                <asp:DataGrid ID="dgRetenciones" runat="server" CssClass="table table-responsive table-striped table-hover" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgRetenciones_EditCommand">
                                <Columns>
                                    <asp:BoundColumn DataField="Id" HeaderText="ID"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Codigo" HeaderText="Codigo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Descripcion" HeaderText="Descripcion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Porcentaje" HeaderText="Porcentaje" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:F}"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Base" HeaderText="Base" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}"></asp:BoundColumn>
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
</asp:Content>
