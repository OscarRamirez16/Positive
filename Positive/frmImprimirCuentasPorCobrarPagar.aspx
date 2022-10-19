<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmImprimirCuentasPorCobrarPagar.aspx.cs" Inherits="Positive.frmImprimirCuentasPorCobrarPagar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Script/PositivoScript.js?version=6"></script>
    <script>
        function ImprimirComprobantePago(CuerpoImpresion) {
            var TipoPago = getParameterByName('TipoPago');
            if (TipoPago == null) {
                TipoPago = 1;
            }
            var innerHTML = "<html>" +
                "<head>" +
                "<title>" +
                "</title>" +
                "<style type='text/css'>" +
                "</style>" +
                "</head>" +
                "<body onload='window.print()' onafterprint='window.location.href = `frmCuentasPorCobrarPagar.aspx?TipoPago=" + TipoPago + "`;' style='margin:0px;'>" + CuerpoImpresion +
                "</body></html>";

            document.open("text/html", "replace");
            document.write(innerHTML);
            document.close();
        }
    </script>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
