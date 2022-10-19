<%@ Page Title="Terceros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarTerceros.aspx.cs" Inherits="Inventario.frmCrearEditarTerceros" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgRetenciones");
        });
    </script>
    <div class="row">
        <div class="col-md-12">
            <b><asp:Label ID="lblTitulo" Font-Size="Large" ForeColor="White" class="label label-info" runat="server"></asp:Label></b>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <span>Datos Basicos</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblTipoTercero" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:DropDownList ID="ddlTipoTercero" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoTercero_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <span><b>Activo</b></span>
                        </div>
                        <div class="col-lg-7">
                            <asp:CheckBox ID="chkActivo" Checked="true" runat="server" Text="" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblTipoIdentificacion" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:DropDownList ID="ddlTipoIdentificacion" CssClass="form-control" runat="server" AutoPostBack="false" DataTextField="Nombre" DataValueField="idTipoIdentificacion"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblIdentificacion" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtIdentificacion" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Identificacion.png" title="Tipo Identificación" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:RequiredFieldValidator ControlToValidate="txtIdentificacion" ID="rfvIdentificacion" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblNombre" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/SocioNegocio.png" title="Nombre" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:RequiredFieldValidator ControlToValidate="txtNombre" ID="rfvNombre" ForeColor="Red" runat="server" Text="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblFechaNacimiento" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtFechaNac" CssClass="Fecha form-control" runat="server"></asp:TextBox>
                                <span class="input-group-addon"><img src="Images/Date.png" title="Fecha de Nacimiento" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            Tipo ID DIAN
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:DropDownList ID="ddlTipoIdDIAN" runat="server" CssClass="form-control" DataTextField="Nombre" DataValueField="Id"></asp:DropDownList>
                                <span class="input-group-addon"><img src="Images/Input/Identificacion.png" title="Tipo ID DIAN" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            Matricula Mercantil
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtMatriculaMercantil" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-addon"><img src="Images/Input/Identificacion.png" title="Tipo ID DIAN" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            Tipo Contribuyente
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:DropDownList ID="ddlTipoContribuyente" CssClass="form-control" runat="server" DataTextField="Nombre" DataValueField="Id"></asp:DropDownList>
                                <span class="input-group-addon"><img src="Images/Input/Identificacion.png" title="Tipo Contribuyente" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            Regimen Fiscal:
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:DropDownList ID="ddlRegimenFiscal" CssClass="form-control" runat="server" DataTextField="Nombre" DataValueField="Id"></asp:DropDownList>
                                <span class="input-group-addon"><img src="Images/Input/Identificacion.png" title="Regimen Fiscal" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            Responsabilidad Fiscal:
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:DropDownList ID="ddlResponsabilidadFiscal" CssClass="form-control" runat="server" DataTextField="Nombre" DataValueField="Id"></asp:DropDownList>
                                <span class="input-group-addon"><img src="Images/Input/Identificacion.png" title="Responsabilidad Fiscal" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <span>Datos de Contacto</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblDireccion" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Direccion.png" title="Dirección" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <asp:RequiredFieldValidator ControlToValidate="txtDireccion" ID="rfvDireccion" runat="server" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblCiudad" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtCiudad" CssClass="form-control" runat="server"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Mapa.png" title="Ciudad" /></span>
                                <input type="hidden" id="hddIdCiudad" value="0" runat="server" />
                            </div>
                        </div>
                        <div class="col-lg-1">

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblCorreo" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Correo.png" title="Mail" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1">

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblTelefono" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Telefono.png" title="Teléfono" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1">

                        </div>
                    </div>
                    <div class="row" style="padding-bottom:22px">
                        <div class="col-lg-4">
                            <asp:Label ID="lblCelular" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <div class = "input-group">
                                <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class = "input-group-addon"><img src="Images/Input/Celular.png" title="Celular" /></span>
                            </div>
                        </div>
                        <div class="col-lg-1">

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <span>Información Comercial</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblListaPrecio" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:DropDownList ID="ddlListaPrecio" CssClass="form-control" runat="server" DataValueField="IdListaPrecio" DataTextField="Nombre" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1">

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <asp:Label ID="lblGrupoCliente" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-lg-7">
                            <asp:DropDownList ID="ddlGrupoCliente" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1">

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    <span>Observaciones</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" style="padding-bottom:7px">
                            <asp:TextBox ID="txtObservaciones" runat="server" Rows="2" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div><div class="row">
        <div class="col-md-6">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <span>Retenciones</span>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12" style="text-align:center">
                            <div class="table-responsive">
                                <asp:DataGrid ID="dgRetenciones" runat="server" CssClass="table table-responsive table-striped table-hover" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:BoundColumn DataField="Id" HeaderText="ID"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Codigo" HeaderText="Codigo"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Descripcion" HeaderText="Descripcion"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Activo" HeaderText="Activo"></asp:BoundColumn>
                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:ImageButton ImageUrl="~/Images/Documento/Guardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" />
            <asp:ImageButton ImageUrl="~/Images/Documento/Cancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" CausesValidation="false" OnClick="btnCancelar_Click" />
        </div>
    </div>
</asp:Content>
