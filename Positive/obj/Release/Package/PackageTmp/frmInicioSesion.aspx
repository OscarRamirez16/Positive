<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmInicioSesion.aspx.cs" Inherits="Inventario.frmInicioSesion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, text/html, charset=utf-8" />
    <title>Inicio Sesion</title>
    <link href="~/Images/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="Script/estilos.css" rel="stylesheet" />

    <!-- Bootstrap Core CSS -->
    <link href="Content/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- MetisMenu CSS -->
    <link href="Content/vendor/metisMenu/metisMenu.min.css" rel="stylesheet" />
    <link href="CSS/Positivo.css" rel="stylesheet" />
    <link href="Styles/jquery.ptTimeSelect.css" rel="stylesheet" />
    <link href="Styles/jquery-ui.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="Content/dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- DataTables CSS -->
    <link href="Content/vendor/datatables-plugins/dataTables.bootstrap.css" rel="stylesheet" />

    <!-- DataTables Responsive CSS -->
    <link href="Content/vendor/datatables-responsive/dataTables.responsive.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="Content/dist/css/sb-admin-2.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="Content/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/vendor/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Script/jquery.mask.js"></script>
    <script src="Script/jquery.ptTimeSelect.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="Content/vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="Content/vendor/metisMenu/metisMenu.min.js"></script>

    <!-- DataTables JavaScript -->
    <script src="Content/vendor/datatables/js/jquery.dataTables.min.js"></script>
    <script src="Content/vendor/datatables-plugins/dataTables.bootstrap.min.js"></script>
    <script src="Content/vendor/datatables-responsive/dataTables.responsive.js"></script>
    <!-- Custom Theme JavaScript -->
    <script src="Content/dist/js/sb-admin-2.js"></script>

    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    <%--<script src="Scripts/jquery-ui.js"></script>--%>
    <script src="Content/vendor/datatables/js/dataTables.buttons.min.js"></script>
    <script src="Content/vendor/datatables/js/buttons.html5.min.js"></script>
    <script src="Script/PositivoScript.js?version=4"></script>
    <style>
        /*Login Page*/
        body {
            font-family: "Lato", sans-serif;
        }

        .main-head {
            height: 150px;
            background: #FFF;
        }

        .sidenav {
            height: 100%;
            background-color: #575757;
            overflow-x: hidden;
            padding-top: 20px;
            background-image: url(/Images/bg/bg1.jpg);
            background-repeat: no-repeat;
            background-position: left;
        }

        .btn-info, .btn-info:hover {
            color: #fff;
            background-color: #206AB2;
            border-color: #206AB2;
        }

        .main {
            padding: 0px 10px;
        }

        @media screen and (max-height: 450px) {
            .sidenav {
                padding-top: 15px;
                background-size: cover !important;
            }

            .login-main-text {
                margin-top: 50% !important;
            }

                .login-main-text > img {
                    display: none !important;
                }
        }

        @media screen and (max-width: 450px) {

            .sidenav {
                padding-top: 15px;
                background-size: cover !important;
            }

            .login-form {
                margin-top: 10%;
            }

            .register-form {
                margin-top: 10%;
            }

            .login-main-text {
                margin-top: 50% !important;
            }

                .login-main-text > img {
                    display: none !important;
                }
        }

        @media screen and (min-width: 768px) {
            .main {
                margin-left: 30%;
            }

            .sidenav {
                width: 30%;
                position: fixed;
                z-index: 1;
                top: 0;
                left: 0;
            }

            .login-form {
                margin-top: 40%;
            }

            .register-form {
                margin-top: 20%;
            }

            .login-main-text {
                margin-top: unset !important;
            }
        }


        .login-main-text {
            margin-top: 10%;
            color: #fff;
            text-align: center;
            align-content: center;
        }

            .login-main-text h2 {
                font-weight: 300;
            }

        .btn-black {
            background-color: #000 !important;
            color: #fff;
        }

        body {
            background-color: #FFFFFF;
        }
        /*End Login Page*/
    </style>
</head>
<body>
    <div class="sidenav">
        <div class="login-main-text">
            <img src="Images/bg.png" alt="Positive" width="50%" />
        </div>
    </div>
    <div class="main">
        <div class="col-md-6 col-sm-12">
            <div class="login-form">
                <form id="form1" runat="server">
                    <div class="form-group">
                        <label>Usuario</label>
                        <asp:TextBox ID="txtLogin" placeholder="Usuario" runat="server" CssClass="required form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Contrasena</label>
                        <asp:TextBox ID="txtPassword" placeholder="Contrasena" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnAceptar" CssClass="btn btn-info btn-sm btn-block" runat="server" Text="Aceptar" Font-Bold="true" OnClick="btnAceptar_Click" />
                </form>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="myModalLabel"></h4>
                </div>
                <div class="modal-body" id="myModalBody">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div style="position: absolute; right: 22px; bottom: 5px;">
        <a href="www.hqs-spftware.com" style="cursor: pointer">
            <img src="Images/logo hqs.png" alt="" /></a>
    </div>
</body>
</html>
