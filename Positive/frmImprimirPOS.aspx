<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmImprimirPOS.aspx.cs" Inherits="Positive.frmImprimirPOS" %>

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
        function ImprimirDocumentoVentaRapida(CuerpoImpresion) {
            var IdTipoDocumento = getParameterByName('IdTipoDocumento');
            if (IdTipoDocumento == null) {
                IdTipoDocumento = 1;
            }
            //var innerHTML = "<html>" +
            //    "<head>" +
            //    "<title>" +
            //    "</title>" +
            //    "<style type='text/css'>" +
            //    "</style>" +
            //    "</head>" +
            //    "<body onload='window.print()' onafterprint='window.location.href = `frmVentaRapida.aspx?IdTipoDocumento=" + IdTipoDocumento + "`;' style='margin:0px;'>" + CuerpoImpresion +
            //    "</body></html>";

            //document.open("text/html", "replace");
            //document.write(innerHTML);
            //document.close();
            //window.stop();

            //var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html>" +
                "<head>" +
                "<title>" +
                "</title>" +
                "<style type='text/css'>" +
                "</style>" +
                "</head>" +
                "<body style='margin:0px;'>" + CuerpoImpresion +
                "</body></html>";
            window.print();
            window.setTimeout(function () {
                if (IdTipoDocumento == null || IdTipoDocumento == 1) {
                    window.location.href = "frmVentaRapida.aspx";
                }
                else if (IdTipoDocumento == 3) {
                    window.location.href = "frmVentaRapida.aspx?IdTipoDocumento=3";
                }
                else {
                    window.location.href = "frmVentaRapida.aspx?IdTipoDocumento=8";
                }
            }, 1000);
        }
    </script>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
