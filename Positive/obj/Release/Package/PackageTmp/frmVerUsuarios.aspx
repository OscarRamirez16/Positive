<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmVerUsuarios.aspx.cs" Inherits="Inventario.frmVerUsuarios" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgUsuarios");
        });
    </script>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <asp:Label ID="lblTitulo" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-1"><asp:Label ID="lblPrimerNombre" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Input/Nombre.png" title="Primer Nombre" /></span>
                            </div>
                        </div>
                        <div class="col-md-1"><asp:Label ID="lblSegundoNombre" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Input/Nombre.png" title="Segundo Nombre" /></span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-1"><asp:Label ID="lblPrimerApellido" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Input/Nombre.png" title="Primer Apellido" /></span>
                            </div>
                        </div>
                        <div class="col-md-1"><asp:Label ID="lblSegundoApellido" runat="server"></asp:Label>:</div>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon">
                                <img src="Images/Input/Nombre.png" title="Segundo Apellido" /></span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align:center">
                            <asp:ImageButton ImageUrl="~/Images/btnbuscar.png" ToolTip="Buscar" runat="server" ID="Button1" Height="40" Width="40"  OnClick="btnBuscar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="Button2" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:DataGrid ID="dgUsuarios" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" OnEditCommand="dgUsuarios_EditCommand">
                <Columns>
                    <asp:BoundColumn DataField="idUsuario" HeaderText="ID Usuario"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Identificacion" HeaderText="Identificación"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NombreCompleto" HeaderText="Usuario"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Usuario" HeaderText="Login"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Contrasena" HeaderText="Contraseña"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Empresa" HeaderText="Empresa"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Telefonos" HeaderText="Telefono o Celular"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Direccion" HeaderText="Dirección"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Correo" HeaderText="E-mail"></asp:BoundColumn>
                    <asp:EditCommandColumn CancelText="Cancelar" EditText="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />" UpdateText="Actualizar"></asp:EditCommandColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            </asp:DataGrid>
            <div id="divAlerta"></div>
        </div>
    </div>
</asp:Content>
