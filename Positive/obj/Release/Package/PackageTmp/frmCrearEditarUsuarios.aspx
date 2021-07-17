<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarUsuarios.aspx.cs" Inherits="Inventario.frmCrearUsuarios" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        var pintar = false;
        $(document).ready(function () {
            PintarTabla("cphContenido_dgRolesUsuario");
            $("#pestana2").unbind("click");
            $("#pestana2").click(function () {
                if (pintar) {
                    pintar = true;
                    PintarTabla("cphContenido_dgRolesUsuario");
                }
            });
        });
    </script>
    <div id="contenido" style="width: 100%">
        <ul id="lista">
            <li id="pestana1"><a href='#CrearUsuarios'>Crear Usuarios</a></li>
            <li id="pestana2"><a href='#AsignacionRoles'>Asignación de Rol</a></li>
        </ul>
        <div id="CrearUsuarios" style="width: 100%">
            <div class="row">
                <div class="col-lg-10 col-lg-offset-1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblTipoIdentificacion" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:DropDownList ID="ddlTipoIdentificacion" CssClass="form-control" runat="server" DataTextField="Nombre" DataValueField="idTipoIdentificacion"></asp:DropDownList>
                                    <input type="hidden" id="hddIdUsuario" runat="server" />
                                </div>
                                <div class="col-lg-2"><asp:Label ID="lblIdentificacion" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblLogin" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtLogin" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-lg-2"><asp:Label ID="lblPassword" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtContrasena" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblPrimerNombre" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtPrimerNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-2"><asp:Label ID="lblSegundoNombre" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtSegundoNombre" runat="server" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblPrimerApellido" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtPrimerApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-2"><asp:Label ID="lblSegundoApellido" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtSegundoApellido" runat="server" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblCorreo" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-2"><asp:Label ID="lblTelefono" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblCelular" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-2"><asp:Label ID="lblDireccion" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4"><asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblCiudad" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control"></asp:TextBox>
                                    <input type="hidden" id="hddIdCiudad" runat="server" />
                                </div>
                                <div class="col-lg-2"><asp:Label ID="lblBodega" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtBodega" runat="server" CssClass="form-control"></asp:TextBox>
                                    <input type="hidden" id="hddIdBodega" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-2"><asp:Label ID="lblPorcentajeDescuento" runat="server"></asp:Label>:</div>
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtPorcentajeDescuento" runat="server" CssClass="form-control Decimal"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6"><asp:CheckBox ID="chkActivo" runat="server" /></div>
                                <div class="col-lg-6"><asp:CheckBox ID="chkModificaPrecio" runat="server" /></div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6"><asp:CheckBox ID="chkAdmin" runat="server" /></div>
                                <div class="col-lg-6"><asp:CheckBox ID="chkVerCuadreCaja" runat="server" Text="Ver Cuadre Caja" /></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="AsignacionRoles" style="width: 100%">
            <div class="row">
                <div class="col-lg-10 col-lg-offset-1">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:Label ID="lblTituloGrilla" runat="server"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <asp:DataGrid ID="dgRolesUsuario" runat="server"  Width="100%" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center" OnItemDataBound="dgRolesUsuario_ItemDataBound">
                                        <Columns>
                                            <asp:BoundColumn DataField="idRol" HeaderText="ID"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRol" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
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
        </div>
    </div>
</asp:Content>
