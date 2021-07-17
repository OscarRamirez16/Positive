<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmCrearTercero.aspx.cs" Inherits="Inventario.frmCrearTercero" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="Script/jquery-1.10.2.min.js"></script>
    <script src="Script/bootstrap.min.js"></script>
    <script src="Script/PositivoScript.js?version=4"></script>
    <script src="Script/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/js/bootstrap-datetimepicker.min.js"></script>
    <title>Crear Tercero</title>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <div class="form-horizontal">
            <div class="row">
                <div class="col-md-12" style="text-align:center"><h2><b>Crear Tercero</b></h2></div>
            </div>
            <div class="row">
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblTipoID" runat="server" CssClass="control-label" Text="Tipo ID:"></asp:Label>
                        <div class="col-md-12">
                            <asp:DropDownList ID="ddlTipoID" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblIdentificacion" runat="server" CssClass="control-label" Text="Identificación:"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtIdentificacion" ID="rfvIdentificacion" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblTipoTercero" runat="server" CssClass="control-label" Text="Tipo Tercero:"></asp:Label>
                        <div class="col-md-12">
                            <asp:DropDownList ID="ddlTipoTercero" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblNombre" runat="server" CssClass="control-label" Text="Nombre:"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtNombre" ID="rfvNombre" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblTelefono" runat="server" CssClass="control-label" Text="Telefono:"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblDireccion" runat="server" CssClass="control-label" Text="Dirección:"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtDireccion" ID="rfvDireccion" ForeColor="Red" Text="*" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblFechaNac" runat="server" CssClass="control-label" Text="Fecha Nacimiento:"></asp:Label>
                        <div class="col-md-12 input-group date" id="datetimepicker1">
                            <asp:TextBox ID="txtFechaNac" runat="server" CssClass="form-control"></asp:TextBox>
                            <span class="input-group-addon">
                                <span class="glyphicon glyphicon-calendar"></span>
                            </span>
                        </div>
                        <script type="text/javascript">
                            $(function () {
                                $('#datetimepicker1').datetimepicker();
                            });
                        </script>
                    </div>
                </div>
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblCiudad" runat="server" CssClass="control-label" Text="Ciudad:"></asp:Label>
                        <div class="col-md-12">
                            <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblCorreo" runat="server" CssClass="control-label" Text="Correo:"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblCelular" runat="server" CssClass="control-label" Text="Celular:"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblListaPrecio" runat="server" CssClass="control-label" Text="Lista Precio:"></asp:Label>
                        <div class="col-md-12">
                            <asp:DropDownList ID="ddlListaPrecio" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblGrupo" runat="server" CssClass="control-label" Text="Grupo:"></asp:Label>
                        <div class="col-md-12">
                            <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6" style="text-align:center">
                    <div class="form-group">
                        <asp:Label ID="lblObservaciones" runat="server" CssClass="control-label" Text="Observaciones:"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-16">
                    <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
