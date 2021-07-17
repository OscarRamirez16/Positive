<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Positive.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
<%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>--%>
    <title>Inicio Sesion</title>
    <!--<link href="Content/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="Content/vendor/bootstrap/js/bootstrap.min.js"></script>-->
    <!-- Bootstrap Core CSS -->
    <link href="Content/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- Bootstrap Core JavaScript -->
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <div class="form-horizontal">
            <div class="row">
                <div class="form-group">
                    <label class="control-label col-md-4 col-sm-4">Usuario</label>
                    <asp:TextBox ID="txtLogin" placeholder="Usuario" runat="server" CssClass="required form-control" TextMode="SingleLine"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="control-label col-md-4 col-sm-4">Contraseña</label>
                    <asp:TextBox ID="txtPassword" placeholder="Contraseña" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="cold-md-3 col-sm-3 col-lg-offset-4">
                    <asp:Button ID="btnAceptar" CssClass="btn-danger" runat="server" Text="Aceptar" Font-Bold="true" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
