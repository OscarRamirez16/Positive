function setGridResponsive() {
    var tableID = $(".grdViewTable").attr("id");
    if (tableID != undefined) {
        if ($(".grdViewTable").find("thead").length == 0) {
            $(".grdViewTable").prepend("<thead></thead>").find("thead").addClass("headerStyle" + tableID)
            $("#" + tableID + " tr:first").prependTo("thead.headerStyle" + tableID);
            $("#" + tableID).unwrap();
        }
    }
}

function EjecutarCrearTercero() {
    document.getElementById('cphContenido_btnCrearTercero').click();
    $('#cphContenido_txtCodigo').focus();
}

function ConfigurarTeclas(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == "114") {
        var Respuesta = window.open("frmCrearTercero.aspx", this.target, 'width=1300,height=600', false);
    }
    if (tecla == "121") {
        document.getElementById('cphContenido_btnGuardar').click();
    }
    if (tecla == "27") {
        window.close();
    }
}

function ImprimirConciliacion(CuerpoImpresion) {
    var oldPage = document.body.innerHTML;
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
        RedireccionarConciliacion(oldPage);
    }, 1000);
}

function RedireccionarConciliacion(oldPage) {
    document.body.innerHTML = oldPage;
    window.location = "frmConciliacion.aspx";
}

$(document).ready(function () {
    $('._selecionar').click(function () {
        var elementos = $(this).data('elementos');
        if ($('.' + elementos + ' :first').is(':checked')) {
            var nuevoEstado = false;
        } else {
            var nuevoEstado = true;
        }
        $("." + elementos + " input").prop('checked', nuevoEstado);

    });
});

function EstablecerTituloPagina(Titulo) {
    document.title = Titulo;
}

function ConfigurarEnter() {
    $(".form-control").keypress(function (e) {
        if (e.which == "13") {
            return false;
        }
    });
    $(".Entero").keypress(function (e) {
        if (e.which == "13") {
            return false;
        }
    });
    $(".Decimal").keypress(function (e) {
        if (e.which == "13") {
            return false;
        }
    });
    $(".BoxValor").keypress(function (e) {
        if (e.which == "13") {
            return false;
        }
    });
    $(".BoxValorGrilla").keypress(function (e) {
        if (e.which == "13") {
            return false;
        }
    });
    $(".Box").keypress(function (e) {
        if (e.which == "13") {
            return false;
        }
    });
}

function CalcularTotalPago() {
    __doPostBack('CalcularTotalPago');
}

function AdicionarArticulo() {
    document.getElementById('cphContenido_btnAdicionar').click();
    $('#cphContenido_txtCodigo').focus();
}

function CalcularDescuento() {
    if ($('#cphContenido_txtDescuento').val() != "0") {
        document.getElementById('cphContenido_btnCalcularDescuento').click();
    }
}

function CalcularDevuelta() {
    var string = "cphContenido_";
    if ($('#' + string + 'txtValorPago').val() != "0" && $('#' + string + 'txtValorPago').val() != "") {
        $('#' + string + 'txtValorPago').val($('#' + string + 'txtValorPago').val().replace("$", ""));
        $('#' + string + 'txtTotalFactura').val($('#' + string + 'txtTotalFactura').val().replace("$", ""));
        $('#' + string + 'txtDevuelta1').val(formatNumber.new(parseFloat($('#' + string + 'txtValorPago').val().replace(",", "")).toFixed(2) - parseFloat($('#' +
        string + 'txtTotalFactura').val().replace(",", "")).toFixed(2)));
    }
    else {
        $('#' + string + 'txtDevuelta1').val("0");
        $('#' + string + 'txtValorPago').val("0");
    }
}

function ImprimirComprobanteCuadreCaja(CuerpoImpresion, Opcion) {
    var oldPage = document.body.innerHTML;
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
        if (Opcion == "1") {
            RedireccionarCuadreCaja(oldPage);
        }
        else {
            RedireccionarCuadreCajaImprimir(oldPage);
        }
    }, 1000);
}

function RedireccionarCuadreCaja(oldPage) {
    document.body.innerHTML = oldPage;
    window.location = "frmCerrarCaja.aspx";
}

function RedireccionarCuadreCajaImprimir(oldPage) {
    document.body.innerHTML = oldPage;
    window.location = "frmImprimirCuadreDiario.aspx";
}

function ReImprimirDocumentoServidor(CuerpoImpresion) {
    var oldPage = document.body.innerHTML;
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
        RedireccionarBusqueda(oldPage);
    }, 1000);
}

function RedireccionarBusqueda(oldPage) {
    document.body.innerHTML = oldPage;
    window.location = "frmVerDocumentos.aspx?opcionDocumento=" + $('#cphContenido_hddTipoDocumento').val();
}

function ImprimirDocumentoServidor(CuerpoImpresion) {
    var oldPage = document.body.innerHTML;
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
        Redireccionar(oldPage);
    }, 1000);
}

function Redireccionar(oldPage) {
    document.body.innerHTML = oldPage;
    window.location = "frmDocumentos.aspx?opcionDocumento=" + $('#cphContenido_hddTipoDocumento').val();
}

//function RedireccionarPagos(oldPage) {
//    document.body.innerHTML = oldPage;
//    window.location = "frmCuentasPorCobrarPagar.aspx?TipoPago=" + $('#cphContenido_hddTipoDocumento').val();
//}

//function RedireccionarAnticipo(oldPage) {
//    document.body.innerHTML = oldPage;
//    window.location = "frmAnticipo.aspx";
//}

//function ImprimirComprobanteAnticipo(CuerpoImpresion) {
//    var oldPage = document.body.innerHTML;
//    document.body.innerHTML = "<html>" +
//        "<head>" +
//        "<title>" +
//        "</title>" +
//        "<style type='text/css'>" +
//        "</style>" +
//        "</head>" +
//        "<body style='margin:0px;'>" + CuerpoImpresion +
//        "</body></html>";
//    window.print();
//    window.setTimeout(function () {
//        RedireccionarAnticipo(oldPage);
//    }, 1000);
//}

//function ImprimirComprobantePago(CuerpoImpresion) {
//    var oldPage = document.body.innerHTML;
//    document.body.innerHTML = "<html>" +
//                "<head>" +
//                "<title>" +
//                "</title>" +
//                "<style type='text/css'>" +
//                "</style>" +
//                "</head>" +
//                "<body style='margin:0px;'>" + CuerpoImpresion +
//                "</body></html>";
//    window.print();
//    window.setTimeout(function () {
//        RedireccionarPagos(oldPage);
//    }, 1000);
//}

function MostrarPrecios() {
    var string = 'cphContenido_';
    if ($("#" + string + "hddTipoDocumento").val() == '1' || $("#" + string + "hddTipoDocumento").val() == '3') {
        var _Url = "Ashx/Bodega.ashx";
        var Parametros = '?opcion=3&IdArticulo=' + $('#' + string + 'hddIdArticulo').val() + '&IdBodega=' + $('#' + string + 'hddIdBodega').val();
        $.ajax({
            url: _Url + Parametros,
            type: "POST",
            success: function (data) {
                MostrarAlerta("Precios disponibles para el artículo en la bodega", data, "600");
            },
            error: function (a, b, c) {
                debugger;
            }
        });
    }
    else {
        MostrarAlerta("Error", "Opción no valida para este documento.", "600");
    }
}

function SeleccionarPrecio(Precio) {
    cerrarAlerta();
    $('#cphContenido_txtPrecio').val(parseFloat(Precio).toFixed(2));
    $('#cphContenido_txtPrecio').focus();
}

function EstablecerMascaras() {
    EstablecerDataPicker();
    EstablecerDataHour();
    $('.Decimal').mask('000,000,000,000,000.00', { reverse: true });
    $('.Decimal3').mask('000,000,000,000,000.000', { reverse: true });
    $('.Entero').mask('000,000,000,000,000', { reverse: true });
    $('.Fecha').mask("00/00/0000", { placeholder: "__/__/____" });
}

function EstablecerDataHour() {
    $(".Hora").ptTimeSelect({
        popupImage: "<img alt='Establecer hora' src='Images/Hour.png'/>",
        hoursLabel: 'Horas',
        minutesLabel: 'Minutos',
        setButtonLabel: "<img alt='Establecer hora' src='Images/Ok.png'/>"
    });
}

function validarObligatorio() {
    var Error = '';
    $('.Obligatorio').each(function () {
        if ($(this).val() == '') {
            Error = Error + 'El campo <b style="color:red">' + $(this).attr('nombreCampo') + '</b> es obligatorio<br>';
        }
    });
    if (Error == '') {
        return true;
    }
    else {
        MostrarAlerta('Campos Obligatorios', Error, 600);
        return false;
    }
}

function imprimirDataGrid(strTitulo, IdTabla, ContieneCadena) {
    var oldPage = document.body.innerHTML;
    var string = 'cphContenido_';
    var table;
    if (ContieneCadena) {
        table = document.getElementById(IdTabla);
    }
    else {
        table = document.getElementById(string + IdTabla);
    }

    document.body.innerHTML =
              "<html>" +
                "<head>" +
                "<title>" +
                "</title>" + strTitulo +
                "<link href='Script/estilos.css' rel='stylesheet' />" +
                "</head>" +
                "<body style='margin:0px;'>" +
                "<div style='position:relative;font-family:arial;'>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; text-align: center; background: #1D70B7;'><img src='Images/logo avanti.jpg' alt='Avanti'/></div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; text-align: center; background: #1D70B7;'>" + strTitulo +
                "</div>" +
                "<div style='font-size: 10px;" +
                "padding-top: 5px;'>" + table.outerHTML +
                "</div>" +
                "</div>" +
                "</body>";
    window.print();
    document.body.innerHTML = oldPage;
}

function menu() {
    //$('.nav li').hover(
    //function () { 
    //    $('ul', this).fadeIn();
    //},
    //function () { 
    //    $('ul', this).fadeOut();
    //}
    //);
}

function pestañas() {
    $("#contenido").tabs();
}

function ShowTab(liName, tabName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
        tablinks[i].style.borderWidth = "0";
    }

    // Show the current tab, and add an "active" class to the link that opened the tab
    document.getElementById(tabName).style.display = "block";
    
    if ($("#" + liName).currentTarget != undefined) {
        $("#" + liName).currentTarget.className += " active";
    }
    if (tabName == "iProfile") {
        borderWidth
        if ($("#" + liName).currentTarget != undefined) {
            $("#" + liName).currentTarget.style.borderColor = "#f08d30";
            $("#" + liName).currentTarget.style.borderWidth = "thin";
        }
    }
    else {
        if ($("#" + liName).currentTarget != undefined) {
            $("#" + liName).currentTarget.style.borderColor = "#00b08d";
            $("#" + liName).currentTarget.style.borderWidth = "thin";
        }
    }
}

function seleccionarTab(TabId, TabIndex) {
    $("#" + TabId).tabs("option", "active", TabIndex);
}

function EstablecerDataPicker(txtControlID) {
    $(".Fecha").blur(function () {
        FormatoFecha($(".Fecha").get(0));
    });
    $("#" + txtControlID).datepicker({
        //showOn: "button",
        //buttonImage: "Images/date.png",
        buttonImageOnly: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        dayNames: ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"],
        dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
        monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        dateFormat: "dd/mm/yy",
        popupImage: "<span class = 'input-group-addon'><img alt='Establecer hora' src='Images/Date.png'/></span>"
    });
}

function FormatoFecha(Control) {
    var Value = $(Control).val();
    var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
    if (!date_regex.test(Value)) {
        Value = "";
    }
    $(Control).val(Value);
}

function ocultarBotonFacturar() {
    $(".btnFacturar").hide();
}

function mostrarBotonFacturar() {
    $(".btnFacturar").show();
}

function crearEncabezado(observaciones, totalDocumento, totalIVA, idDocumento) {
    $('#txtTotalFactura').val(parseFloat(totalDocumento).toFixed(2));
    $('#txtTotalIVA').val(parseFloat(totalIVA).toFixed(2));
    $('#txtObser').val(observaciones);
    $('#txtAntesIVA').val(parseFloat(totalDocumento) - parseFloat(totalIVA));
	$('#txtAntesIVA').val(parseFloat($('#txtAntesIVA').val()).toFixed(2));
    $('#hddIdDocumento').val(idDocumento)
}

function limpiarTabla() {
    var table = document.getElementById('detallesFactura');
    var rowCount = table.rows.length;
    for (rowCount; rowCount > 1; rowCount--) {
        table.deleteRow(rowCount - 1);
    }
}

function llenarTablaDetalles(numLinea, codigoArticulo, idArticulo, DescripcionArticulo, valorUniArticulo, cantidadArticulo, IVA, idDetalle, idBodega, txtBodega, Accion){
    var string = 'cphContenido_';
    var table = document.getElementById("detallesFactura");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    generarColumnasTablaFactura(row, numLinea);
    EstablecerAutoCompleteArticulo('txtNombreArticulo_' + numLinea, 'Ashx/Articulo.ashx', 'hddIdArticulo_' + numLinea, 'txtCodigoArticulo_' + numLinea, 'txtPrecioArticulo_' + numLinea, 'txtIva_' + numLinea, 'hddAccion_' + numLinea, 1, 'hddCantBodega_' + numLinea, 'hddIdBodega_' + numLinea, 'txtBodega_' + numLinea, 'hddEsInventario_' + numLinea);
    EstablecerAutoCompleteBodega('txtBodega_' + numLinea, 'Ashx/Bodega.ashx', 'hddIdBodega_' + numLinea, $('#' + string + 'hddIdEmpresa').val(), '2', 'hddIdArticulo_' + numLinea, 'hddCantBodega_' + numLinea);
    $('#imgOpcionArticulo_' + numLinea).attr('src', 'Images/eliminar.png');
    $('#imgOpcionArticulo_' + numLinea).attr('onclick', 'ocultarFilaTablaFactura("detallesFactura",' + numLinea + ');');
    $('#hddIdArticulo_' + numLinea).val(idArticulo);
    $('#txtCodigoArticulo_' + numLinea).val(codigoArticulo);
    $('#txtNombreArticulo_' + numLinea).val(DescripcionArticulo);
    $('#txtPrecioArticulo_' + numLinea).val(valorUniArticulo.replace(",", "."));
    $('#txtCantidadArticulo_' + numLinea).val(cantidadArticulo.replace(",", "."));
    $('#txtIva_' + numLinea).val(parseFloat(IVA));
    var ValorUni = parseFloat(valorUniArticulo.replace(",", "."));
    var Can = parseFloat(cantidadArticulo.replace(",", "."));
    $('#txtValorTotal_' + numLinea).val(ValorUni * Can);
	$('#txtValorTotal_' + numLinea).val(parseFloat($('#txtValorTotal_' + numLinea).val()).toFixed(2));
    $('#hddIdDetalle_' + numLinea).val(idDetalle);
    $('#hddAccion_' + numLinea).val(Accion);
    $('#hddIdBodega_' + numLinea).val(idBodega);
    $('#txtBodega_' + numLinea).val(txtBodega);
    if (ModificaPrecio == 0) {
        $('#txtPrecioArticulo_' + numLinea).prop('disabled', true);
    }
}

function adicionarDetalleBlancoTabla(numLinea) {
    var string = 'cphContenido_';
    var table = document.getElementById("detallesFactura");
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    generarColumnasTablaFactura(row, numLinea);
    EstablecerAutoCompleteArticulo('txtNombreArticulo_' + numLinea, 'Ashx/Articulo.ashx', 'hddIdArticulo_' + numLinea, 'txtCodigoArticulo_' + numLinea, 'txtPrecioArticulo_' + numLinea, 'txtIva_' + numLinea, 'hddAccion_' + numLinea, 1, 'hddCantBodega_' + numLinea, 'hddIdBodega_' + numLinea, 'txtBodega_' + numLinea, 'hddEsInventario_' + numLinea);
    EstablecerAutoCompleteBodega('txtBodega_' + numLinea, 'Ashx/Bodega.ashx', 'hddIdBodega_' + numLinea, $('#' + string + 'hddIdEmpresa').val(), '2', 'hddIdArticulo_' + numLinea, 'hddCantBodega_' + numLinea);
    if (ModificaPrecio == 0) {
        $('#txtPrecioArticulo_' + numLinea).prop('disabled', true);
    }
}

function EsconderBoton() {
    $(".btnGuardar").hide();
}

function guardarDocumento(_Url, tipoDocumento) {
    $(".btnFacturar").hide();
    var string = 'cphContenido_';
    if (validarDatos()) {
        guardarEncabezadoDocumento(_Url, tipoDocumento);
    }
}

function calcularDevuelta(totalFactura) {
    if ($("#txtEfectivo").val() != '0' && $("#txtEfectivo").val() != '') {
        $("#txtDevuelta").val(parseFloat($("#txtEfectivo").val()) - totalFactura);
    }
}

function validarPagoCompra(totalFactura, numeroDocumento) {
    $(".btnPago").hide();
    var TotalPago = 0;
    if ($("#txtEfectivo").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtEfectivo").val());
    }
    if ($("#txtTarjetaCredito").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtTarjetaCredito").val());
    }
    if ($("#txtTarjetaDebito").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtTarjetaDebito").val());
    }
    if ($("#txtCheque").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtCheque").val());
    }
    if (TotalPago < totalFactura) {
        if (confirm('El valor a pagar es menor que el valor de la factura, desea realizar el abono?')) {
            if ($("#txtEfectivo").val() != '0' && $("#txtEfectivo").val() != '') {
                guardarPagoCompra($("#txtEfectivo").val(), totalFactura, 1, numeroDocumento, 0);
            }
            if ($("#txtTarjetaCredito").val() != '0' && $("#txtTarjetaCredito").val() != '') {
                guardarPagoCompra($("#txtTarjetaCredito").val(), totalFactura, 2, numeroDocumento, $("#txtVoucherTarjetaCredito").val());
            }
            if ($("#txtTarjetaDebito").val() != '0' && $("#txtTarjetaDebito").val() != '') {
                guardarPagoCompra($("#txtTarjetaDebito").val(), totalFactura, 3, numeroDocumento, $("#txtVoucherTarjetaDebito").val());
            }
            if ($("#txtCheque").val() != '0' && $("#txtCheque").val() != '') {
                guardarPagoCompra($("#txtCheque").val(), totalFactura, 4, numeroDocumento, $("#txtNumeroCheque").val());
            }
            else {
                cerrarAlerta();
                limpiarControlesDocumento();
            }
        }
    }
    else {
        if ($("#txtEfectivo").val() != '0' && $("#txtEfectivo").val() != '') {
            guardarPagoCompra(totalFactura, totalFactura, 1, numeroDocumento, 0);
        }
        if ($("#txtTarjetaCredito").val() != '0' && $("#txtTarjetaCredito").val() != '') {
            guardarPagoCompra(totalFactura, totalFactura, 2, numeroDocumento, $("#txtVoucherTarjetaCredito").val());
        }
        if ($("#txtTarjetaDebito").val() != '0' && $("#txtTarjetaDebito").val() != '') {
            guardarPagoCompra(totalFactura, totalFactura, 3, numeroDocumento, $("#txtVoucherTarjetaDebito").val());
        }
        if ($("#txtCheque").val() != '0' && $("#txtCheque").val() != '') {
            guardarPagoCompra(totalFactura, totalFactura, 4, numeroDocumento, $("#txtNumeroCheque").val());
        }
        //var valorPago = parseFloat($("#txtEfectivo").val()) - parseFloat($("#txtDevuelta").val());
        //guardarPagoCompra(valorPago, totalFactura, 1, numeroDocumento);
    }
}

function validarPago(totalFactura, numeroDocumento) {
    $(".btnPago").hide();
    var TotalPago = 0;
    if ($("#txtEfectivo").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtEfectivo").val());
    }
    if ($("#txtTarjetaCredito").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtTarjetaCredito").val());
    }
    if ($("#txtTarjetaDebito").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtTarjetaDebito").val());
    }
    if ($("#txtCheque").val() != "") {
        TotalPago = TotalPago + parseFloat($("#txtCheque").val());
    }
    if (TotalPago < totalFactura) {
        if (confirm('El valor a pagar es menor que el valor de la factura, desea realizar el abono?')) {
            if ($("#txtEfectivo").val() != '0' && $("#txtEfectivo").val() != '') {
                guardarPago($("#txtEfectivo").val(), totalFactura, 1, numeroDocumento, 0);
            }
            if ($("#txtTarjetaCredito").val() != '0' && $("#txtTarjetaCredito").val() != '') {
                guardarPago($("#txtTarjetaCredito").val(), totalFactura, 2, numeroDocumento, $("#txtVoucherTarjetaCredito").val());
            }
            if ($("#txtTarjetaDebito").val() != '0' && $("#txtTarjetaDebito").val() != '') {
                guardarPago($("#txtTarjetaDebito").val(), totalFactura, 3, numeroDocumento, $("#txtVoucherTarjetaDebito").val());
            }
            if ($("#txtCheque").val() != '0' && $("#txtCheque").val() != '') {
                guardarPago($("#txtCheque").val(), totalFactura, 4, numeroDocumento, $("#txtNumeroCheque").val());
            }
            else {
                cerrarAlerta();
                imprimirDocumento(numeroDocumento);
            }
        }
    }
    else {
        if ($("#txtEfectivo").val() != '0' && $("#txtEfectivo").val() != '') {
            guardarPago(totalFactura, totalFactura, 1, numeroDocumento, 0);
        }
        if ($("#txtTarjetaCredito").val() != '0' && $("#txtTarjetaCredito").val() != '') {
            guardarPago(totalFactura, totalFactura, 2, numeroDocumento, $("#txtVoucherTarjetaCredito").val());
        }
        if ($("#txtTarjetaDebito").val() != '0' && $("#txtTarjetaDebito").val() != '') {
            guardarPago(totalFactura, totalFactura, 3, numeroDocumento, $("#txtVoucherTarjetaDebito").val());
        }
        if ($("#txtCheque").val() != '0' && $("#txtCheque").val() != '') {
            guardarPago(totalFactura, totalFactura, 4, numeroDocumento, $("#txtNumeroCheque").val());
        }
        //var valorPago = parseFloat($("#txtEfectivo").val()) - parseFloat($("#txtDevuelta").val());
        //guardarPago(valorPago, totalFactura, 1, numeroDocumento);
    }
}

function guardarPagoCompra(valorPago, totalFactura, formaPago, numeroDocumento, voucher) {
    var _Url = "Ashx/Documento.ashx";
    var string = 'cphContenido_';
    var Parametros = '?idTercero=' + $('#' + string + 'hddIdCliente').val() + '&valorPago=' + valorPago + '&totalPago=' + totalFactura + '&idEmpresa=' + $('#' + string + 'hddIdEmpresa').val() + '&idUsuario=' + $('#' + string + 'hddIdUsuario').val() + '&idEstado=1&formaPago=' + formaPago + '&voucher=' + voucher + '&opcion=10&idDocumento=' + $('#hddIdDocumento').val() + '&tipoDocumento=' + $('#' + string + 'hddTipoDocumento').val();
    $.ajax({
        url: _Url + Parametros,
        type: "POST",
        success: function (data) {
            if (data == "-1") {
                guardarPagoCompra(valorPago, totalFactura, formaPago, numeroDocumento, voucher);
            }
            else {
                alert("El pago se realizo satisfactoriamente.");
                mostrarBotonFacturar();
                cerrarAlerta();
                limpiarControlesDocumento();
            }
        },
        error: function (a, b, c) {
            debugger;
        }
    });
}

function guardarPago(valorPago, totalFactura, formaPago, numeroDocumento, voucher) {
    var _Url = "Ashx/Documento.ashx";
    var string = 'cphContenido_';
    var Parametros = '?idTercero=' + $('#' + string + 'hddIdCliente').val() + '&valorPago=' + valorPago + '&totalPago=' + totalFactura + '&idEmpresa=' + $('#' + string + 'hddIdEmpresa').val() + '&idUsuario=' + $('#' + string + 'hddIdUsuario').val() + '&idEstado=1&formaPago=' + formaPago + '&voucher=' + voucher + '&opcion=4&idDocumento=' + $('#hddIdDocumento').val() + '&tipoDocumento=' + $('#' + string + 'hddTipoDocumento').val();
    $.ajax({
        url: _Url + Parametros,
        type: "POST",
        success: function (data) {
            if (data == "-1") {
                guardarPago(valorPago, totalFactura, formaPago, numeroDocumento, voucher);
            }
            else {
                alert("El pago se realizo satisfactoriamente.");
                cerrarAlerta();
                imprimirDocumento(numeroDocumento);
            }
        },
        error: function (a, b, c) {
            debugger;
        }
    });
}

function guardarEncabezadoDocumento(_Url, tipoDocumento) {
    var string = 'cphContenido_';
    var direccion = $('#' + string + 'txtDireccion').val();
    var dir = direccion.replace("#", "numero");
    var Parametros = '';
    var numDoc;
    if (parseFloat($('#hddIdDocumento').val()) > 0) {
        Parametros = '?numeroDocumento=' + $('#' + string + 'lblNumeroFactura').text() + '&fecha=' + $('#' + string + 'lblFechaActual').text() + '&idTercero=' + $('#' + string + 'hddIdCliente').val() + '&telefono=' + $('#' + string + 'txtTelefono').val() + '&direccion=' + dir + '&idCiudad=' + $('#' + string + 'hddIdCiudad').val() + '&nombreTercero=' + $('#' + string + 'txtTercero').val() + '&observaciones=' + $('#txtObser').val() + '&opcion=5&tipoDocumento=' + $('#' + string + 'hddTipoDocumento').val() + '&_i_u=' + $('#' + string + 'hddIdUsuario').val() + '&_i_e=' + $('#' + string + 'hddIdEmpresa').val() + '&TotalDocumento=' + $('#txtTotalFactura').val() + '&TotalIVA=' + $('#txtTotalIVA').val() + '&idDocumento=' + $('#hddIdDocumento').val();
    }
    else{
        Parametros = '?fecha=' + $('#' + string + 'lblFechaActual').text() + '&idTercero=' + $('#' + string + 'hddIdCliente').val() + '&telefono=' + $('#' + string + 'txtTelefono').val() + '&direccion=' + dir + '&idCiudad=' + $('#' + string + 'hddIdCiudad').val() + '&nombreTercero=' + $('#' + string + 'txtTercero').val() + '&observaciones=' + $('#txtObser').val() + '&opcion=1&tipoDocumento=' + $('#' + string + 'hddTipoDocumento').val() + '&_i_u=' + $('#' + string + 'hddIdUsuario').val() + '&_i_e=' + $('#' + string + 'hddIdEmpresa').val() + '&TotalDocumento=' + $('#txtTotalFactura').val() + '&TotalIVA=' + $('#txtTotalIVA').val();
    }
    $.ajax({
        url: _Url + Parametros,
        type: "POST",
        success: function (data) {
            if (data == "-1") {
                alert("Error al guardar el documento, contacte el administrador del sistema.");
                limpiarControlesDocumento();
            }
            else {
                $('#' + string + 'lblNumeroFactura').text(parseInt($('#' + string + 'lblNumeroFactura').text()) + 1);
                guardarDetallesDocumento(_Url, data.split(',')[0], data.split(',')[1]);
            }
        },
        error: function (a, b, c) {
            debugger;
        }
    });
    if ($('#' + string + 'hddTipoDocumento').val() == 1) {
        var cuerpo = "<table style='width:100%'><tr><td colspan='4' style='text-align: center;'>Total a pagar: <b>" + $('#txtTotalFactura').val() + "</b>" +
                    "</td></tr><tr><td>Efectivo:</td><td><input type='text' value=" + $('#txtTotalFactura').val() + " onblur='calcularDevuelta(" + $('#txtTotalFactura').val() + ");' id='txtEfectivo' /></td><td colspan='2'></td></tr><tr>" +
                    "<td>Tarjeta credito:</td><td><input type='text' id='txtTarjetaCredito' /></td><td>Voucher:</td><td><input type='text' id='txtVoucherTarjetaCredito' /></td></tr><tr>" +
		            "<td>Tarjeta debito:</td><td><input type='text' id='txtTarjetaDebito' /></td><td>Voucher:</td><td><input type='text' id='txtVoucherTarjetaDebito' /></td></tr><tr>" +
		            "<td>Cheque:</td><td><input type='text' id='txtCheque' /></td><td>Numero:</td><td><input type='text' id='txtNumeroCheque' /></td></tr><tr>" +
                    "<td>Devuelta:</td><td><input type='text' id='txtDevuelta' disabled='disabled' value='0' /></td></tr><tr><td colspan='2' style='text-align: center;'>" +
                    "<input type='button' id='btnPago' class='btnPago' value='Aceptar' onclick='validarPago(" + $('#txtTotalFactura').val() + "," + $('#' + string + 'lblNumeroFactura').text() + ");' />" +
                    "<input type='button' id='btnCancelarPago' class='btnPago' value='Cancelar' onclick='eliminarDocumento(" + -1 + "," + $('#cphContenido_hddTipoDocumento').val() + ");'  /></td></tr></table>";
        MostrarAlerta("FORMAS DE PAGO", cuerpo, "600");
    }
    if ($('#' + string + 'hddTipoDocumento').val() == 2) {
        var cuerpo = "<table style='width:100%'><tr><td colspan='4' style='text-align: center;'>Total a pagar: <b>" + $('#txtTotalFactura').val() + "</b>" +
                    "</td></tr><tr><td>Efectivo:</td><td><input type='text' value=" + $('#txtTotalFactura').val() + " onblur='calcularDevuelta(" + $('#txtTotalFactura').val() + ");' id='txtEfectivo' /></td><td colspan='2'></td></tr><tr>" +
                    "<td>Tarjeta credito:</td><td><input type='text' id='txtTarjetaCredito' /></td><td>Voucher:</td><td><input type='text' id='txtVoucherTarjetaCredito' /></td></tr><tr>" +
		            "<td>Tarjeta debito:</td><td><input type='text' id='txtTarjetaDebito' /></td><td>Voucher:</td><td><input type='text' id='txtVoucherTarjetaDebito' /></td></tr><tr>" +
		            "<td>Cheque:</td><td><input type='text' id='txtCheque' /></td><td>Numero:</td><td><input type='text' id='txtNumeroCheque' /></td></tr><tr>" +
                    "<td>Devuelta:</td><td><input type='text' id='txtDevuelta' disabled='disabled' value='0' /></td></tr><tr><td colspan='2' style='text-align: center;'>" +
                    "<input type='button' id='btnPago' class='btnPago' value='Aceptar' onclick='validarPagoCompra(" + $('#txtTotalFactura').val() + "," + $('#' + string + 'lblNumeroFactura').text() + ");' />" +
                    "<input type='button' id='btnCancelarPago' class='btnPago' value='Cancelar' onclick='eliminarDocumento(" + -1 + "," + $('#cphContenido_hddTipoDocumento').val() + ");'  /></td></tr></table>";
        MostrarAlerta("FORMAS DE PAGO", cuerpo, "600");
    }
}

function guardarDetallesDocumento(_Url, idDocumento, numeroDocumento) {
    $('#hddIdDocumento').val(idDocumento);
    var table = document.getElementById("detallesFactura");
    var rowCount = table.rows.length;
    var string = 'cphContenido_';
    for (var i = 1; i < rowCount; i++) {
        if ($('#hddAccion_' + (i - 1)).val() != '2') {
            if ($('#hddAccion_' + (i - 1)).val() == '3') {
                Parametros = '?idDocumento=' + idDocumento + '&opcion=7&tipoDocumento=' + $('#' + string + 'hddTipoDocumento').val() + '&idDetalleDocumento=' + $('#hddIdDetalle_' + (i - 1)).val();
                $.ajax({
                    url: _Url + Parametros,
                    type: "POST",
                    success: function (data) {
                        if (data == "-1") {
                            alert("Error al eliminar el detalle " + $('#txtNombreArticulo_' + (i - 1)).val() + ", contacte el administrador del sistema.");
                            i = rowCount;
                            limpiarControlesDocumento();
                        }
                        else {

                        }
                    },
                    error: function (a, b, c) {
                        debugger;
                    }
                });
            }
            else {
                if ($('#hddIdArticulo_' + (i - 1)).val() != '-1' && $('#hddIdArticulo_' + (i - 1)).val() != '') {
                    var valor = $('#txtValorTotal_' + (i - 1)).val();
                    var IVA = $('#txtIva_' + (i - 1)).val();
                    var Parametros = '';
                    if ($('#hddAccion_' + (i - 1)).val() == '1') {
                        Parametros = '?idDocumento=' + idDocumento + '&numeroLinea=' + i + '&idArticulo=' + $('#hddIdArticulo_' + (i - 1)).val() + '&descripcion=' + $('#txtNombreArticulo_' + (i - 1)).val() + '&precio=' + $('#txtPrecioArticulo_' + (i - 1)).val() + '&impuesto=' + (valor * (IVA / 100)).toFixed(0) + '&cantidad=' + $('#txtCantidadArticulo_' + (i - 1)).val() + '&opcion=2&tipoDocumento=' + $('#' + string + 'hddTipoDocumento').val() + '&_i_e=' + $('#' + string + 'hddIdEmpresa').val() + '&_i_b=' + $('#hddIdBodega_' + (i - 1)).val();
                    }
                    if ($('#hddAccion_' + (i - 1)).val() == '4') {
                        Parametros = '?idDocumento=' + idDocumento + '&numeroLinea=' + i + '&idArticulo=' + $('#hddIdArticulo_' + (i - 1)).val() + '&descripcion=' + $('#txtNombreArticulo_' + (i - 1)).val() + '&precio=' + $('#txtPrecioArticulo_' + (i - 1)).val() + '&impuesto=' + (valor * (IVA / 100)).toFixed(0) + '&cantidad=' + $('#txtCantidadArticulo_' + (i - 1)).val() + '&opcion=6&tipoDocumento=' + $('#' + string + 'hddTipoDocumento').val() + '&_i_e=' + $('#' + string + 'hddIdEmpresa').val() + '&idDetalleDocumento=' + $('#hddIdDetalle_' + (i - 1)).val() + '&_i_b=' + $('#hddIdBodega_' + (i - 1)).val();
                    }
                    $.ajax({
                        url: _Url + Parametros,
                        type: "POST",
                        success: function (data) {
                            if (data == "-1") {
                                alert("Error al guardar el detalle " + $('#txtNombreArticulo_' + (i - 1)).val() + ", contacte el administrador del sistema.");
                                eliminarDocumento(idDocumento, $('#' + string + 'hddTipoDocumento').val());
                                i = rowCount;
                            }
                            else {
                                
                            }
                        },
                        error: function (a, b, c) {
                            debugger;
                        }
                    });
                }
            }
        }
    }
    if ($('#' + string + 'hddTipoDocumento').val() != 1 && $('#' + string + 'hddTipoDocumento').val() != 2 && $('#' + string + 'hddTipoDocumento').val() != 5 && $('#' + string + 'hddTipoDocumento').val() != 6) {
        imprimirDocumento(numeroDocumento);
    }
    if ($('#' + string + 'hddTipoDocumento').val() == 5 || $('#' + string + 'hddTipoDocumento').val() == 6)
    {
        limpiarControlesDocumento();
    }
}

function eliminarDocumento(idDocumento, tipoDocumento) {
    var _Url = "Ashx/Documento.ashx";
    if (idDocumento == -1) {
        idDocumento = $('#hddIdDocumento').val();
        cerrarAlerta();
    }
    var Parametros = '?idDocumento=' + idDocumento + '&tipoDocumento=' + tipoDocumento + '&opcion=3';
    $.ajax({
        url: _Url + Parametros,
        type: "POST",
        success: function (data) {
            if (data == "-1") {
                alert("Error al eliminar el documento, contacte el administrador del sistema.");
            }
            else {
                alert("El Documento no se registro.");
                limpiarControlesDocumento();
            }
        },
        error: function (a, b, c) {
            debugger;
        }
    });
}

function imprimirReporteMovimientosPorDocumento() {
    var oldPage = document.body.innerHTML;
    var tableDetalles = "";
    var string = 'cphContenido_';
    var table = document.getElementById(string + "dgMovimientosDocumentos");
    tableDetalles = tableDetalles + "<table border='1'><tr><td align='center'>Numero Documento</td><td align='center'>Fecha</td><td align='center'>Tercero</td><td align='center'>Usuario</td><td align='center'>IVA</td><td align='center'>Total Pago</td><td align='center'>Articulo</td><td align='center'>Cantidad</td><td align='center'>Valor Unitario</td><td align='center'>Total Linea</td></tr>"
    for (var i = 1; i < table.rows.length; i++) {
            tableDetalles = tableDetalles + "<tr><td>" + table.rows[i].cells[0].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[1].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[2].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[3].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[4].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[5].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[6].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[7].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[8].innerHTML + "</td>";
            tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[9].innerHTML + "</td></tr>";
    }
    tableDetalles = tableDetalles + "</table>";
    document.body.innerHTML =
              "<html>" +
                "<head>" +
                "<title>" +
                "</title>" +
                "<style type='text/css'>" +
                "</style>" +
                "</head>" +
                "<body style='margin:0px;'>" +
                "<div style='position:relative;font-family:arial;'>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; text-align: center; background: #1D70B7;'>MOVIMIENTOS POR DOCUMENTO" +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "font-weight: bold; padding-top: 20px; text-align: left;'> " + $("#lblNombreEmpresa").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: left;'>Nit: " + $("#lblNitEmpresa").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: left;'>Direcci&oacute;n: " + $("#lblDireccion").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: left;'>Telefono: " + $("#lblTelefono").text() +
                "</div>" +
                "<div style='font-size: 10px;" +
                "font-weight: bold; padding-top: 5px; text-align: left;'>Usuario: " + $("#" + string + "ddlUsuario option:selected").text() +
                "</div>" +
                "<div style='font-size: 10px;" +
                "font-weight: bold; padding-top: 5px; text-align: left;'>Tipo Documento: " + $("#" + string + "ddlTipoDocumento option:selected").text() +
                "</div>" +
                "<div style='font-size: 10px;" +
                "font-weight: bold; padding-top: 5px; text-align: left;'>Fecha Desde: " + $("#" + string + "txtFechaInicial").val() +
                "</div>" +
                "<div style='font-size: 10px;" +
                "font-weight: bold; padding-top: 5px; text-align: left;'>Fecha Hasta: " + $("#" + string + "txtFechaFinal").val() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: center'>" + tableDetalles +
                "</div>" +
                "<div style='font-size: 10px;" +
                "font-weight: bold; padding-top: 5px; text-align: left;'>Total: " + $("#" + string + "txtTotal").val() +
                "</div>" +
                "</div>" +
                "</body>";
    window.print();
    document.body.innerHTML = oldPage;
    window.location = "frmReporteMovimientosPorDocumento.aspx";
}

function imprimirReporteArticulosPorBodega() {
    var oldPage = document.body.innerHTML;
    var tableDetalles = "";
    var string = 'cphContenido_';
    var table = document.getElementById(string + "dgArticulos");
    tableDetalles = tableDetalles + "<table border='1' style='width: 100%'><tr><td align='center'>Codigo</td><td align='center'>Nombre</td><td align='center'>Presentación</td><td align='center'>Cantidad</td><td align='center'>Bodega</td></tr>"
    for (var i = 1; i < table.rows.length; i++) {
        tableDetalles = tableDetalles + "<tr><td>" + table.rows[i].cells[0].innerHTML + "</td>";
        tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[1].innerHTML + "</td>";
        tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[2].innerHTML + "</td>";
        tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[8].innerHTML + "</td>";
        tableDetalles = tableDetalles + "<td>" + table.rows[i].cells[7].innerHTML + "</td></tr>";
    }
    tableDetalles = tableDetalles + "</table>";
    document.body.innerHTML =
              "<html>" +
                "<head>" +
                "<title>" +
                "</title>" +
                "<style type='text/css'>" +
                "</style>" +
                "</head>" +
                "<body style='margin:0px;'>" +
                "<div style='position:relative;font-family:arial;'>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; text-align: center; background: #1D70B7;'>ARTICULOS POR BODEGA" +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "font-weight: bold; padding-top: 20px; text-align: left;'> " + $("#lblNombreEmpresa").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: left;'>Nit: " + $("#lblNitEmpresa").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: left;'>Direcci&oacute;n: " + $("#lblDireccion").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: left;'>Telefono: " + $("#lblTelefono").text() +
                "</div>" +
                "<div style='font-size: 10px;" +
                "font-weight: bold; padding-top: 5px; text-align: left;'>Bodega: " + $("#" + string + "txtBodega").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; text-align: center'>" + tableDetalles +
                "</div>" +
                "</div>" +
                "</body>";
    window.print();
    document.body.innerHTML = oldPage;
    window.location = "frmReporteArticulosPorBodega.aspx";
}

function imprimirDocumento(numeroDocumento) {
    var oldPage = document.body.innerHTML;
    var tableDetalles = "";
    var string = 'cphContenido_';
    var table = document.getElementById("detallesFactura");
    tableDetalles = tableDetalles + "<table border='1'><tr><td align='center'>Cantidad</td><td align='center'>Descripcion</td><td align='center'>Valor</td></tr>"
    for (var i = 1; i < table.rows.length; i++) {
        if ($('#txtNombreArticulo_' + (i - 1)).val() != "" && $('#hddIdArticulo_' + (i - 1)).val() != "-1") {
            tableDetalles = tableDetalles + "<tr><td>" + $('#txtCantidadArticulo_' + (i - 1)).val() + "</td>";
            tableDetalles = tableDetalles + "<td>" + $('#txtNombreArticulo_' + (i - 1)).val() + "</td>";
            tableDetalles = tableDetalles + "<td>" + $('#txtValorTotal_' + (i - 1)).val() + "</td></tr>";
        }
    }
    tableDetalles = tableDetalles + "</table>";
    document.body.innerHTML =
              "<html>" +
                "<head>" +
                "<title>" +
                "</title>" +
                "<style type='text/css'>" +
                "</style>" +
                "</head>" +
                "<body style='margin:0px;'>" +
                "<div style='position:relative;font-family:arial;'>" +
                "<div style='font-size: 12px;" +
                "font-weight: bold; width: 300px; padding-top: 20px; text-align: left;'> " + $("#lblNombreEmpresa").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; width: 300px; text-align: left;'>Nit: " + $("#lblNitEmpresa").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; width: 300px; text-align: left;'>Direcci&oacute;n: " + $("#lblDireccion").text() +
                "</div>" +
                "<div style='font-size: 10px;font-weight: bold;" +
                "padding-top: 5px; width: 300px; text-align: left;'>Telefono: " + $("#lblTelefono").text() +
                "</div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'> " + $('#' + string + 'hddEncabezadoFactura').val() +
                "</div>" +
                "<div style='font-size: 16px;font-weight: bold;" +
                "padding-top: 25px; width: 300px;'>Numero Documento: " + numeroDocumento +
                "</div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 25px; width: 300px;'>" + $('#' + string + 'lblFechaActual').text() +
                "</div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 25px; width: 300px;'>Tercero: " + $('#' + string + 'txtTercero').val() +
                "</div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px;'>Identificaci&oacute;n: " + $('#' + string + 'txtIdentificacion').val() +
                "</div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; text-align: center; width: 300px;'>DETALLES" +
                "</div>" +
                "<div style='font-size: 13px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'>" + tableDetalles +
                "</div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'>Antes de IVA: " + $("#txtAntesIVA").val() +
                "</div>" +
                "<div style='font-size: 12px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'>Valor IVA: " + $("#txtTotalIVA").val() +
                "</div>" +
                "<div style='font-size: 11px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'>Total a pagar: " + $("#txtTotalFactura").val() +
                "</div>" +
                "<div style='font-size: 11px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'>Vende: " + $('#lblUsuario').text() +
                "</div>" +
                "<div style='font-size: 11px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'>Observaciones: " + $('#txtObser').val() +
                "</div>" +
                "<div style='font-size: 11px;font-weight: bold;" +
                "padding-top: 5px; width: 300px;'>" + $('#' + string + 'hddPieFactura').val() +
                "</div>" +
                "</div>" +
                "</body>";
                window.print();
                document.body.innerHTML = oldPage;
                window.location = "frmDocumento.aspx?opcionDocumento=" + $('#' + string + 'hddTipoDocumento').val();
}

function limpiarControlesDocumento() {
    var string = 'cphContenido_';
    var table = document.getElementById('detallesFactura');
    var rowCount = table.rows.length;
    for (rowCount; rowCount > 1; rowCount--) {
        table.deleteRow(rowCount-1);
    }
    var row = table.insertRow(rowCount);
    generarColumnasTablaFactura(row, 0);
    EstablecerAutoCompleteArticulo('txtNombreArticulo_0', 'Ashx/Articulo.ashx', 'hddIdArticulo_0', 'txtCodigoArticulo_0', 'txtPrecioArticulo_0', 'txtIva_0', 'hddAccion_0', 1, 'hddCantBodega_0', 'hddIdBodega_0', 'txtBodega_0', 'hddEsInventario_0');
    EstablecerAutoCompleteBodega('txtBodega_0', 'Ashx/Bodega.ashx', 'hddIdBodega_0', $('#' + string + 'hddIdEmpresa').val(), '2', 'hddIdArticulo_0', 'hddCantBodega_0');
    $('#' + string + 'txtCiudad').val('');
    $('#' + string + 'txtTercero').val('');
    $('#' + string + 'txtIdentificacion').val('');
    $('#' + string + 'txtTelefono').val('');
    $('#' + string + 'txtDireccion').val('');
    $('#txtObser').val('');
    $('#txtTotalFactura').val('0');
    $('#txtTotalIVA').val('0');
    $('#txtAntesIVA').val('0');
    var _Url = "Ashx/Documento.ashx";
    if ($('#' + string + 'hddTipoDocumento').val() != "1") {
        var Parametros = '?ValorBusqueda=' + $('#' + string + 'hddIdEmpresa').val() + '&opcion=8' + '&tipoFactura=' + $('#' + string + 'hddTipoDocumento').val();
        $.ajax({
            url: _Url + Parametros,
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data == '') {
                    alert('Error al traer el numero de la factura.');
                }
                else {
                    $("#" + string +"lblNumeroFactura").text(data);
                }
            },
            error: function (a, b, c) {
                debugger;
            }
        });
    }
    else {
        var Parametros = '?ValorBusqueda=' + $('#' + string + 'hddIdEmpresa').val() + '&opcion=9';
        $.ajax({
            url: _Url + Parametros,
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data == '') {
                    alert('Error al traer el numero de la factura.');
                }
                else {
                    $("#" + string + "lblNumeroFactura").text(data);
                }
            },
            error: function (a, b, c) {
                debugger;
            }
        });
    }
}

function validarDatos() {
    var string = 'cphContenido_';
    var ErrorMsg = '';
    if ($('#' + string + 'txtCiudad').val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Ciudad</b> es obligatorio";
    }
    if ($('#' + string + 'txtTercero').val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>" + $('#' + string + 'lblTercero').text() + "</b> es obligatorio";
    }
    if ($('#' + string + 'txtIdentificacion').val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Identificación</b> es obligatorio";
    }
    if ($('#' + string + 'txtTelefono').val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Telefono</b> es obligatorio";
    }
    if ($('#' + string + 'txtDireccion').val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Dirección</b> es obligatorio";
    }
    var table = document.getElementById("detallesFactura");
    var rowCount = table.rows.length;
    if (rowCount == 2) {
        ErrorMsg = ErrorMsg + "<br/>No hay <b>Articulos</b> para registrar";
    }
    if (ErrorMsg != '') {
        MostrarAlerta("Falta información", ErrorMsg, 600);
        return false;
    }
    else {
        return true;
    }
}

function validarCamposFaturaDetalle(numLinea) {
    var string = 'cphContenido_';
    var ErrorMsg = '';
    if ($('#txtCodigoArticulo_'+numLinea).val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Codigo</b> es obligatorio";
    }
    if ($('#txtNombreArticulo_'+numLinea).val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Descripcion</b> es obligatorio";
    }
    if ($('#txtPrecioArticulo_'+numLinea).val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Valor Unitario</b> es obligatorio";
    }
    if ($('#txtCantidadArticulo_'+numLinea).val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Cantidad</b> es obligatorio";
    }
    if ($('#txtIva_'+numLinea).val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>IVA</b> es obligatorio";
    }
    if ($('#txtValorTotal_'+numLinea).val() == '') {
        ErrorMsg = ErrorMsg + "<br/>El campo <b>Valor Total</b> es obligatorio";
    }
    else {
        calcularTotalFactura();
    }
    if (ErrorMsg != '') {
        MostrarAlerta("Falta información", ErrorMsg, 600);
    }
    else {
        if ($('#' + string + 'hddTipoDocumento').val() == "1" && $('#' + string + 'hddInventarioNegativo').val() == "False" && parseFloat($('#txtCantidadArticulo_' + numLinea).val()) > parseFloat($('#hddCantBodega_' + numLinea).val()) && $('#hddEsInventario_' + numLinea).val() != '0') {
            $('#txtCantidadArticulo_' + numLinea).val("");
            $('#txtBodega_' + numLinea).focus();
            $('#hddIndicador').val('1');
            ErrorMsg = ErrorMsg + "<br/>No hay suficiente existencias en bodega";
            MostrarAlerta("Inventario", ErrorMsg, 600);
        }
        else {
            if ($('#hddAccion_' + numLinea).val() == "2" || $('#hddAccion_' + numLinea).val() == "4") {
                $('#hddAccion_' + numLinea).val('4');
            }
            else {
                $("#hddAccion_" + numLinea).val('1');
            }
            var table = document.getElementById("detallesFactura");
            var rowCount = table.rows.length;
            if (numLinea >= (rowCount - 2)) {
                var row = table.insertRow(rowCount);
                generarColumnasTablaFactura(row, parseInt(numLinea) + 1);
                EstablecerAutoCompleteArticulo('txtNombreArticulo_' + (parseInt(numLinea) + 1), 'Ashx/Articulo.ashx', 'hddIdArticulo_' + (parseInt(numLinea) + 1), 'txtCodigoArticulo_' + (parseInt(numLinea) + 1), 'txtPrecioArticulo_' + (parseInt(numLinea) + 1), 'txtIva_' + (parseInt(numLinea) + 1), 'hddAccion_' + (parseInt(numLinea) + 1), 1, 'hddCantBodega_' + (parseInt(numLinea) + 1), 'hddIdBodega_' + (parseInt(numLinea) + 1), 'txtBodega_' + (parseInt(numLinea) + 1), 'hddEsInventario_' + (parseInt(numLinea) + 1));
                EstablecerAutoCompleteBodega('txtBodega_' + (parseInt(numLinea) + 1), 'Ashx/Bodega.ashx', 'hddIdBodega_' + (parseInt(numLinea) + 1), $('#' + string + 'hddIdEmpresa').val(), '2', 'hddIdArticulo_' + (parseInt(numLinea) + 1), 'hddCantBodega_' + (parseInt(numLinea) + 1));
                $('#imgOpcionArticulo_' + numLinea).attr('src', 'Images/eliminar.png');
                $('#imgOpcionArticulo_' + numLinea).attr('onclick', 'ocultarFilaTablaFactura("detallesFactura",' + numLinea + ');');
                $('#txtCodigoArticulo_' + (parseInt(numLinea) + 1)).focus();
            }
        }
    }
}

function calcularTotalFactura() {
    var table = document.getElementById('detallesFactura');
    var rowCount = table.rows.length;
    $('#txtTotalFactura').val('0.00');
    $('#txtTotalIVA').val('0.00');
    for (var i = 1; i < rowCount; i++) {
		if($('#txtValorTotal_' + (i - 1)).val() != ''){
			var valor = parseFloat($('#txtValorTotal_' + (i - 1)).val());
			var IVA = $('#txtIva_' + (i - 1)).val();
			if (valor != '' && $('#hddIdArticulo_' + (i - 1)).val() != '-1') {
				$('#txtTotalFactura').val(parseFloat($('#txtTotalFactura').val()) + parseFloat(valor));
				$('#txtTotalIVA').val(parseFloat($('#txtTotalIVA').val()) + (parseFloat(valor) * (parseFloat(IVA) / 100)));
			}
		}
    }
    $('#txtAntesIVA').val(parseFloat($('#txtTotalFactura').val()) - parseFloat($('#txtTotalIVA').val()));
    var antesIVA = parseFloat($('#txtTotalIVA').val());
    antesIVA = antesIVA.toFixed(2);
    $('#txtTotalIVA').val(antesIVA);
	$('#txtTotalFactura').val(parseFloat($('#txtTotalFactura').val()).toFixed(2));
	$('#txtTotalIVA').val(parseFloat($('#txtTotalIVA').val()).toFixed(2));
	$('#txtAntesIVA').val(parseFloat($('#txtAntesIVA').val()).toFixed(2));
}

function ocultarFilaTablaFactura(table, numLinea) {
    var tabla = document.getElementById(table);
    var row = tabla.rows[parseInt(numLinea)+1];
    row.style.display = 'none';
    $('#hddIdArticulo_' + numLinea).val('-1');
    if(parseFloat($('#hddIdDetalle_' + numLinea).val()) > 0){
        $('#hddAccion_' + numLinea).val('3');
    }
    calcularTotalFactura();
}

function generarColumnasTablaFactura(row, numLinea) {
    var cell1 = row.insertCell(0);
    var txtCodigo = document.createElement("input");
    txtCodigo.setAttribute('type', 'text');
    txtCodigo.setAttribute('name', "txtCodigoArticulo_" + numLinea);
    txtCodigo.setAttribute('id', "txtCodigoArticulo_" + numLinea);
    txtCodigo.style.width = '100px';
    txtCodigo.setAttribute('class', 'txtFactura');
    txtCodigo.setAttribute("onblur", "traerArticuloPorCodigoOCodigoBarra('txtCodigoArticulo_" + numLinea + "', 'Ashx/Articulo.ashx', 'hddIdArticulo_" + numLinea + "', 'txtNombreArticulo_" + numLinea + "', 'txtPrecioArticulo_" + numLinea + "', 'txtIva_" + numLinea + "', 'hddAccion_" + numLinea + "', 2, 'hddCantBodega_" + numLinea + "', 'hddIdBodega_" + numLinea + "', 'txtBodega_" + numLinea + "', 'hddEsInventario_" + numLinea + "');");
    cell1.appendChild(txtCodigo);
    var hddID = document.createElement("input");
    hddID.setAttribute('type', 'hidden');
    hddID.setAttribute('name', "hddIdArticulo_" + numLinea);
    hddID.setAttribute('id', "hddIdArticulo_" + numLinea);
    cell1.appendChild(hddID);
    cell1.setAttribute("align", "center");
    var hddIdDetalle = document.createElement("input");
    hddIdDetalle.setAttribute('type', 'hidden');
    hddIdDetalle.setAttribute('name', "hddIdDetalle_" + numLinea);
    hddIdDetalle.setAttribute('id', "hddIdDetalle_" + numLinea);
    cell1.appendChild(hddIdDetalle);
    var hddAccion = document.createElement("input");
    hddAccion.setAttribute('type', 'hidden');
    hddAccion.setAttribute('name', "hddAccion_" + numLinea);
    hddAccion.setAttribute('id', "hddAccion_" + numLinea);
    cell1.appendChild(hddAccion);
    cell1.setAttribute("align", "center");
    var cell2 = row.insertCell(1);
    var txtNombre = document.createElement("input");
    txtNombre.setAttribute('type', 'text');
    txtNombre.setAttribute('name', "txtNombreArticulo_" + numLinea);
    txtNombre.setAttribute('id', "txtNombreArticulo_" + numLinea);
    txtNombre.style.width = '300px';
    cell2.setAttribute("align", "center");
    cell2.appendChild(txtNombre);
    var hddEsInventario = document.createElement("input");
    hddEsInventario.setAttribute('type', 'hidden');
    hddEsInventario.setAttribute('name', "hddEsInventario_" + numLinea);
    hddEsInventario.setAttribute('id', "hddEsInventario_" + numLinea);
    cell2.appendChild(hddEsInventario);
    var cell3 = row.insertCell(2);
    var txtBodega = document.createElement("input");
    txtBodega.setAttribute('type', 'text');
    txtBodega.setAttribute('name', "txtBodega_" + numLinea);
    txtBodega.setAttribute('id', "txtBodega_" + numLinea);
    txtBodega.style.width = '300px';
    cell3.setAttribute("align", "center");
    cell3.appendChild(txtBodega);
    var hddIdBodega = document.createElement("input");
    hddIdBodega.setAttribute('type', 'hidden');
    hddIdBodega.setAttribute('name', "hddIdBodega_" + numLinea);
    hddIdBodega.setAttribute('id', "hddIdBodega_" + numLinea);
    cell3.appendChild(hddIdBodega);
    var hddCantBodega = document.createElement("input");
    hddCantBodega.setAttribute('type', 'hidden');
    hddCantBodega.setAttribute('name', "hddCantBodega_" + numLinea);
    hddCantBodega.setAttribute('id', "hddCantBodega_" + numLinea);
    cell3.appendChild(hddCantBodega);
    var cell4 = row.insertCell(3);
    var txtValorUni = document.createElement("input");
    txtValorUni.setAttribute('type', 'text');
    txtValorUni.setAttribute('name', "txtPrecioArticulo_" + numLinea);
    txtValorUni.setAttribute('id', "txtPrecioArticulo_" + numLinea);
    txtValorUni.style.width = '100px';
    txtValorUni.setAttribute('class', 'txtFactura');
    txtValorUni.setAttribute('onblur','calcularValorTotalDetalle('+numLinea+')');
    cell4.setAttribute("align", "center");
    cell4.appendChild(txtValorUni);
    var cell5 = row.insertCell(4);
    var txtCantidad = document.createElement("input");
    txtCantidad.setAttribute('type','text');
    txtCantidad.setAttribute('name',"txtCantidadArticulo_" + numLinea);
    txtCantidad.setAttribute('id',"txtCantidadArticulo_" + numLinea);
    txtCantidad.style.width = '50px';
    txtCantidad.setAttribute('class','txtFactura');
    txtCantidad.setAttribute('onblur', 'calcularValorTotalDetalle(' + numLinea + ');');
    cell5.setAttribute("align", "center");
    cell5.appendChild(txtCantidad);
    var cell6 = row.insertCell(5);
    var txtIVA = document.createElement("input");
    txtIVA.setAttribute('type', 'text');
    txtIVA.setAttribute('name', 'txtIva_' + numLinea);
    txtIVA.setAttribute('id', 'txtIva_' + numLinea);
    txtIVA.style.width = '100px';
    txtIVA.setAttribute('class', 'txtFactura');
    txtIVA.disabled = true;
    cell6.setAttribute("align", "center");
    cell6.appendChild(txtIVA);
    var cell7 = row.insertCell(6);
    var txtTotal = document.createElement("input");
    txtTotal.setAttribute('type', 'text');
    txtTotal.setAttribute('name', "txtValorTotal_" + numLinea);
    txtTotal.setAttribute('id', "txtValorTotal_" + numLinea);
    txtTotal.style.width = '100px';
    txtTotal.setAttribute('class', 'txtFactura');
    txtTotal.disabled = true;
    cell7.setAttribute("align", "center");
    cell7.appendChild(txtTotal);
    var cell8 = row.insertCell(7);
    var img = document.createElement("img");
    img.src = 'Images/adicionar.png';
    img.setAttribute('onclick','validarCamposFaturaDetalle(' + numLinea + ');');
    img.setAttribute('id', 'imgOpcionArticulo_' + numLinea);
    cell8.appendChild(img);
    var cell9 = row.insertCell(8);
    var imgPrecios = document.createElement("img");
    imgPrecios.src = 'Images/editar.jpg';
    imgPrecios.setAttribute('onclick', 'MostrarPrecios(' + numLinea + ');');
    imgPrecios.setAttribute('id', 'imgPrecios_' + numLinea);
    if (ModificaPrecio == 0) {
        $('#txtPrecioArticulo_' + numLinea).prop('disabled', true);
    }
    cell9.appendChild(imgPrecios);
}

function calcularValorTotalDetalle(numLinea) {
    if (validarNumero($('#txtPrecioArticulo_' + numLinea).attr("id"))) {
        if (validarNumero($('#txtCantidadArticulo_' + numLinea).attr("id"))) {
            $('#txtValorTotal_' + numLinea).val(parseFloat($('#txtPrecioArticulo_' + numLinea).val()).toFixed(2) * parseFloat($('#txtCantidadArticulo_' + numLinea).val()));
            validarCamposFaturaDetalle(numLinea);
			$('#txtValorTotal_' + numLinea).val(parseFloat($('#txtValorTotal_' + numLinea).val()).toFixed(2));
        }
        else {
            if ($('#txtCantidadArticulo_' + numLinea).val() != '') {
                var ErrorMsg = 'El <b>valor unitario</b> o la <b>cantidad</b> deben ser numericos';
                MostrarAlerta("Falta información", ErrorMsg, 600);
            }
            else {
                if($('#hddIndicador').val() != '1'){
                    $('#txtCantidadArticulo_' + numLinea).focus();
                }
            }
        }
    }
    else {
        var ErrorMsg = 'El <b>valor unitario</b> o la <b>cantidad</b> deben ser numericos y mayores a cero';
        MostrarAlerta("Falta información", ErrorMsg, 600);
    }
}

function validarNumero(ControlID) {
    var Value = $("#" + ControlID).val();
    if (isNaN(Value) || Value == '' || parseFloat(Value) <= 0) {
        if (Value != '') {
            $("#" + ControlID).focus();
        }
        $("#" + ControlID).val('');
        return false;
    }
    else {
        return true;
    }
}

function EliminarDetalle() {
    $('#cphContenido_hddLogin').val($('#txtLogin').val());
    $('#cphContenido_hddPassword').val($('#txtPassword').val());
    document.getElementById('cphContenido_btnLogin').click();
}

function CancelarDescuento() {
    CerrarDescuento();
    $('#cphContenido_txtDescuento').val('0');
    $('#cphContenido_txtDescuento').focus();
}

function CerrarDescuento() {
    $(".Descuento").dialog("close");
}

function MostrarDescuento(Titulo, Width) {
    $(".Descuento").get(0).title = Titulo;
    $(".Descuento").dialog({
        width: function () {
            if (Width) {
                return Width;
            }
            else {
                return auto;
            }
        },
        closeOnEscape: false,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        }
    });
}

function CerrarValidador() {
    $(".Validador").dialog("close");
}

function MostrarValidador(Titulo, Width) {
    $(".Validador").get(0).title = Titulo;
    $(".Validador").dialog({
        width: function () {
            if (Width) {
                return Width;
            }
            else {
                return auto;
            }
        }
    });
}

function cerrarAlerta() {
    $(".Mensaje").dialog("close");
}

function MostrarAlerta(Titulo, Cuerpo, Width) {
    $(".Mensaje").get(0).title = Titulo;
    $(".Mensaje").html(Cuerpo);
    $(".Mensaje").dialog({
        width: function () {
            if (Width) {
                return Width;
            }
            else {
                return auto;
            }
        }
    });
}

function EstablecerAutoCompleteListaMateriales(_ControlID, _Url, _hddID, _IdEmpresa) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&i_e=' + _IdEmpresa,
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });

        },
        select: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        },
        focus: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        }
    });
}

function EstablecerAutoCompleteCiudad(_ControlID, _Url, _hddID) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val(),
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });

        },
        select: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        },
        focus: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        }
    });
}

function EstablecerAutoCompleteClientePorIdentificacion(_ControlID, _Url, _hddID, _identificacionID, _telefonoID, _direccionID, _CiudadID, _hddIdCiudad) {
    if ($("#" + _identificacionID).val() != "") {
        $.ajax({
            url: _Url + '?ValorBusqueda=' + $("#" + _identificacionID).val() + '&tipoFactura=' + $("#cphContenido_hddTipoDocumento").val() + '&i_e=' + $("#cphContenido_hddIdEmpresa").val(),
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data == '') {
                    alert('No hay terceros con la identificación ingresada.');
                    $("#" + _ControlID).focus();
                }
                else {
                    $.map(data, function (item) {
                        var datos = item.id.split('@');
                        $("#" + _ControlID).val(item.nombre);
                        $("#" + _hddID).val(datos[0]);
                        if (_identificacionID) {
                            $("#" + _identificacionID).val(datos[1]);
                        }
                        if (_telefonoID) {
                            $("#" + _telefonoID).val(datos[2]);
                        }
                        if (_direccionID) {
                            $("#" + _direccionID).val(datos[3]);
                        }
                        __doPostBack('CargarFacturasPendientesPagos');
                    })
                }
            },
            error: function (a, b, c) {
                debugger;
            }
        });
    }
}

function EstablecerAutoCompleteClientePorIdentificacionDocumentos(_ControlID, _Url, _hddID, _identificacionID, _telefonoID, _direccionID, _CiudadID, _hddIdCiudad) {
    if ($("#" + _identificacionID).val() != "") {
        $.ajax({
            url: _Url + '?ValorBusqueda=' + $("#" + _identificacionID).val() + '&tipoFactura=' + $("#cphContenido_hddTipoDocumento").val() + '&i_e=' + $("#cphContenido_hddIdEmpresa").val(),
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data == '') {
                    alert('No hay terceros con la identificación ingresada.');
                    $("#" + _ControlID).focus();
                }
                else {
                    $.map(data, function (item) {
                        var datos = item.id.split('@');
                        $("#" + _ControlID).val(item.nombre);
                        $("#" + _hddID).val(datos[0]);
                        if (_identificacionID) {
                            $("#" + _identificacionID).val(datos[1]);
                        }
                        if (_telefonoID) {
                            $("#" + _telefonoID).val(datos[2]);
                        }
                        if (_direccionID) {
                            $("#" + _direccionID).val(datos[3]);
                        }
                        if (_CiudadID) {
                            $("#" + _CiudadID).val(datos[4]);
                        }
                        if (_hddIdCiudad) {
                            $("#" + _hddIdCiudad).val(datos[5]);
                        }
                        __doPostBack('CargarSaldosPendientesCliente');
                    })
                }
            },
            error: function (a, b, c) {
                debugger;
            }
        });
    }
}

function EstablecerAutoCompleteTercero(_ControlID, _Url, _hddID) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&tipoFactura=0&i_e=' + $("#cphContenido_hddIdEmpresa").val(),
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });

        },
        select: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            return false;
        }
    });
}

function EstablecerAutoCompleteCliente(_ControlID, _Url, _hddID, _identificacionID, _telefonoID, _direccionID, _CiudadID, _hddIdCiudad) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&tipoFactura=' + $("#cphContenido_hddTipoDocumento").val() + '&i_e=' + $("#cphContenido_hddIdEmpresa").val(),
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });

        },
        select: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            if (_identificacionID) {
                $("#" + _identificacionID).val(datos[1]);
            }
            if (_telefonoID) {
                $("#" + _telefonoID).val(datos[2]);
            }
            if (_direccionID) {
                $("#" + _direccionID).val(datos[3]);
            }
            if (_CiudadID) {
                $("#" + _CiudadID).val(datos[4]);
            }
            if (_hddIdCiudad) {
                $("#" + _hddIdCiudad).val(datos[5]);
            }
            __doPostBack('CargarSaldosPendientesCliente');
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            if (_identificacionID) {
                $("#" + _identificacionID).val(datos[1]);
            }
            if (_telefonoID) {
                $("#" + _telefonoID).val(datos[2]);
            }
            if (_direccionID) {
                $("#" + _direccionID).val(datos[3]);
            }
            if (_CiudadID) {
                $("#" + _CiudadID).val(datos[4]);
            }
            if (_hddIdCiudad) {
                $("#" + _hddIdCiudad).val(datos[5]);
            }
            return false;
        }
    });
}

var formatNumber = {
    separador: ",", // separador para los miles
    sepDecimal: '.', // separador para los decimales
    formatear: function (num) {
        num += '';
        var splitStr = num.split('.');
        var splitLeft = splitStr[0];
        var splitRight = splitStr.length > 1 ? this.sepDecimal + splitStr[1] : '';
        var regx = /(\d+)(\d{3})/;
        while (regx.test(splitLeft)) {
            splitLeft = splitLeft.replace(regx, '$1' + this.separador + '$2');
        }
        return this.simbol + splitLeft + splitRight;
    },
    new: function (num, simbol) {
        this.simbol = simbol || '';
        return this.formatear(num);
    }
}

function traerArticuloPorCodigoOCodigoBarra(_ControlID, _Url, _hddID, _codigoID, _precioID, _ivaID, _opcion, _cantBodega, _idBodega, _txtBodega, _EsInventario, _hddCostoPonderado, _PrecioCosto, _PosCod, _LonCod, _PosCan, _LonCan, _txtCantidad, _txtDescuento) {
    if ($("#" + _ControlID).val() != "") {
        $.ajax({
            url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&opcion=' + _opcion + '&i_e=' + $("#cphContenido_hddIdEmpresa").val() + '&tipoFactura=' + $("#cphContenido_hddTipoDocumento").val() + '&i_b=' + $('#cphContenido_hddBodegaPorDefectoUsuario').val() + '&i_t=' + $("#cphContenido_hddIdCliente").val(),
            dataType: "json",
            type: "POST",
            success: function (data) {
                if (data == '') {
                    alert('No hay productos con el codigo ingresado.');
                    $("#" + _hddID).focus();
                }
                else {
                    $.map(data, function (item) {
                        var datos = item.id.split('@');
                        $("#" + _ControlID).val(item.nombre);
                        $("#" + _hddID).val(datos[0]);
                        $("#" + _codigoID).val(datos[1]);
                        var PRECIO = parseFloat(datos[2]);
                        $("#" + _precioID).val(formatNumber.new(PRECIO));
                        var IVA = parseFloat(datos[3]);
                        $("#" + _ivaID).val(formatNumber.new(IVA));
                        $("#" + _txtBodega).val(datos[4]);
                        $("#" + _idBodega).val(datos[5]);
                        $("#" + _EsInventario).val(datos[6]);
                        $("#" + _hddCostoPonderado).val(datos[7]);
                        $("#" + _PrecioCosto).val(datos[8]);
                        var bodega = datos[4].split('(');
                        if (_cantBodega) {
                            $("#" + _cantBodega).val(parseFloat(bodega[1].split(' ')[0]));
                        }
                        if (parseFloat(datos[9]) != 0) {
                            $("#" + _txtCantidad).val(datos[9]);
                        }
                        if (datos[10] != undefined && parseFloat(datos[10]) != 0) {
                            $("#" + _txtDescuento).val(datos[10]);
                        }
                        $("#" + _txtCantidad).focus();
                    })
                }
            },
            error: function (a, b, c) {
                debugger;
            }
        });
    }
}

function EstablecerAutoCompleteArticulo(_ControlID, _Url, _hddID, _codigoID, _precioID, _ivaID, _opcion, _cantBodega, _idBodega, _txtBodega, _EsInventario, _hddCostoPonderado, _PrecioCosto) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&opcion=' + _opcion + '&i_e=' + $("#cphContenido_hddIdEmpresa").val() + '&tipoFactura=' + $("#cphContenido_hddTipoDocumento").val() + '&i_b=' + $('#cphContenido_hddBodegaPorDefectoUsuario').val() + '&i_t=' + $("#cphContenido_hddIdCliente").val(),
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });
        },
        select: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            $("#" + _codigoID).val(datos[1]);
            var PRECIO = parseFloat(datos[2]);
            $("#" + _precioID).val(formatNumber.new(PRECIO));
            var IVA = parseFloat(datos[3]);
            $("#" + _ivaID).val(formatNumber.new(IVA));
            $("#" + _txtBodega).val(datos[4]);
            $("#" + _idBodega).val(datos[5]);
            $("#" + _EsInventario).val(datos[6]);
            $("#" + _hddCostoPonderado).val(datos[7]);
            $("#" + _PrecioCosto).val(datos[8]);
            var bodega = datos[4].split('(');
            if (_cantBodega) {
                $("#" + _cantBodega).val(parseFloat(bodega[1].split(' ')[0]));
            }
            $("#" + _txtBodega).focus();
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            $("#" + _codigoID).val(datos[1]);
            var PRECIO = parseFloat(datos[2]);
            $("#" + _precioID).val(formatNumber.new(PRECIO));
            var IVA = parseFloat(datos[3]);
            $("#" + _ivaID).val(formatNumber.new(IVA));
            $("#" + _txtBodega).val(datos[4]);
            $("#" + _idBodega).val(datos[5]);
            $("#" + _EsInventario).val(datos[6]);
            $("#" + _hddCostoPonderado).val(datos[7]);
            $("#" + _PrecioCosto).val(datos[8]);
            var bodega = datos[4].split('(');
            if (_cantBodega) {
                $("#" + _cantBodega).val(parseFloat(bodega[1].split(' ')[0]));
            }
            return false;
        }
    });
}

function EstablecerAutoCompleteArticuloSencillo(_ControlID, _Url, _hddID, _IdEmpresa, _opcion) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&opcion=' + _opcion + '&i_e=' + _IdEmpresa,
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });
        },
        change: function (event, ui) {
            if (ui.item == null && $("#" + _ControlID).val() == "") {
                $("#" + _ControlID).val('0');
                $("#" + _hddID).val('0');
                return false;
            }
        },
        select: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        },
        focus: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        }
    });
}

function EstablecerAutoCompleteBodega(_ControlID, _Url, _hddID, idEmpresa, opcion, idArticulo, _CantBodega, _EsInventario) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&i_e=' + idEmpresa + '&opcion=' + opcion + '&i_a=' + $('#'+idArticulo).val(),
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });
        },
        change: function (event, ui) {
            if (ui.item == null && $("#" + _ControlID).val() == "") {
                $("#" + _ControlID).val('0');
                $("#" + _hddID).val('0');
                $("#" + _CantBodega).val('0');
                return false;
            }
        },
        select: function (event, ui) {
            var datos = ui.item.label.split('(');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            if (_CantBodega) {
                $("#" + _CantBodega).val(parseFloat(datos[1].split(' ')[0]));
            }
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.label.split('(');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            if (_CantBodega) {
                $("#" + _CantBodega).val(parseFloat(datos[1].split(' ')[0]));
            }
            return false;
        }
    });
}
function EstablecerAutoCompleteBodegaSimple(_ControlID, _Url, _hddID, idEmpresa, opcion) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&i_e=' + idEmpresa + '&opcion=' + opcion,
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.nombre,
                            value: item.id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });
        },
        select: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.label.split('(');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value);
            return false;
        }
    });
}
function AdicionarVentaRapida(idVentaRapida, Articulo, Descripcion, Cantidad, ValorIVA, Precio, Stock, hddItemsID, Impoconsumo, hideDelete) {
    var numDecimales = 2;
    if ((parseFloat($("#hdd" + idVentaRapida + "Stock").val()) - parseFloat(Cantidad)) >= 0) {
        var ValorIVATotal = parseFloat($("#tdValorIVA").html()) + parseFloat(ValorIVA);
        var ValorTotalAntesImpoconsumo = parseFloat($("#hddValorTotalAntesImpoConsumo").val()) + parseFloat(Precio);
        $("#hddValorTotalAntesImpoConsumo").val(ValorTotalAntesImpoconsumo);
        $("#tdValorIVA").html(ValorIVATotal.toFixed(numDecimales));
        $("#cphContenido_hddValorIVA").val(ValorIVATotal.toFixed(numDecimales));
        var ValorTotal = parseFloat($("#tdValorTotal").html()) + parseFloat(Precio);
        $("#hddValorTotalAntesImpoConsumo").val()
        $("#tdValorAntesIVA").html((ValorTotalAntesImpoconsumo - ValorIVATotal).toFixed(numDecimales));
        var ValorImpoconsumoTotal = 0;
        if (parseFloat(Impoconsumo) > 0) {
            ValorImpoconsumoTotal = parseFloat($("#tdValorAntesIVA").html()) * (parseFloat(Impoconsumo) / 100);
        }
        $("#tdValorImpoconsumo").html(ValorImpoconsumoTotal.toFixed(numDecimales));
        $("#cphContenido_hddValorImpoconsumo").val(ValorImpoconsumoTotal.toFixed(numDecimales));
        ValorTotal = ValorTotalAntesImpoconsumo + ValorImpoconsumoTotal;
        $("#tdValorTotal").html(ValorTotal.toFixed(numDecimales));
        $("#cphContenido_hddValorTotal").val(ValorTotal.toFixed(numDecimales));
        if ($("#" + hddItemsID).val() == "") {
            $("#" + hddItemsID).val(idVentaRapida);
        }
        else {
            $("#" + hddItemsID).val($("#" + hddItemsID).val() + "," + idVentaRapida);
        }
        $("#hdd" + idVentaRapida + "Stock").val(parseFloat($("#hdd" + idVentaRapida + "Stock").val()) - parseFloat(Cantidad));
        var deleteHTML = "<td></td>";
        if (hideDelete == false || hideDelete == undefined || hideDelete == "") {
            deleteHTML = "<td style=\"cursor:pointer;\" onclick=\"EliminarVentaRapida(" + idVentaRapida + ",this,'" + hddItemsID + "','" + ValorIVA + "','" + Precio + "','" + Cantidad + "','" + Impoconsumo + "');\"><img src='Images/Input/Eliminar.png' title='" + Descripcion + "'/></td>";
        }
        var trVentaRapida = "<tr><td>" + Articulo + "</td><td>" + Descripcion + "</td><td>" + Cantidad + "</td><td style=\"text-align:right;\">" + Precio + "</td>" + deleteHTML + "</tr>"
        $(".FacturaRapidaBody").append(trVentaRapida);
    }
    else {
        alert("El articulo " + Descripcion + " no tiene stock...");
    }
}
function EliminarVentaRapida(idVentaRapida, Control, hddItemsID, ValorIVA, Precio, Cantidad, Impoconsumo) {
    var numDecimales = 2;
    $(Control).closest('tr').remove();
    var NewItems = "";
    var Eliminar = true;
    for (var i = 0; i < $("#" + hddItemsID).val().split(",").length; i++) {
        if (idVentaRapida != $("#" + hddItemsID).val().split(",")[i] || !Eliminar) {
            if (NewItems == "") {
                NewItems = $("#" + hddItemsID).val().split(",")[i];
            }
            else {
                NewItems = NewItems + "," + $("#" + hddItemsID).val().split(",")[i];
            }
        }
        else {
            Eliminar = false;
        }
    }
    var ValorTotalAntesImpoconsumo = parseFloat($("#hddValorTotalAntesImpoConsumo").val()) - parseFloat(Precio);
    $("#hddValorTotalAntesImpoConsumo").val(ValorTotalAntesImpoconsumo.toFixed(numDecimales));
    $("#" + hddItemsID).val(NewItems);
    var ValorIVATotal = parseFloat($("#tdValorIVA").html()) - parseFloat(ValorIVA);
    $("#tdValorIVA").html(ValorIVATotal.toFixed(numDecimales));
    $("#cphContenido_hddValorIVA").val(ValorIVATotal.toFixed(numDecimales));
    var ValorTotal = ValorTotalAntesImpoconsumo;
    $("#tdValorTotal").html(ValorTotal.toFixed(numDecimales));
    $("#cphContenido_hddValorTotal").val(ValorTotal.toFixed(numDecimales));
    $("#tdValorAntesIVA").html((ValorTotal - ValorIVATotal).toFixed(numDecimales));
    var ValorImpoconsumoTotal = 0;
    if (parseFloat(Impoconsumo) > 0) {
        ValorImpoconsumoTotal = parseFloat($("#tdValorAntesIVA").html()) * (parseFloat(Impoconsumo) / 100);
    }
    $("#tdValorImpoconsumo").html(ValorImpoconsumoTotal.toFixed(numDecimales));
    $("#cphContenido_hddValorImpoconsumo").val(ValorImpoconsumoTotal.toFixed(numDecimales)); ValorTotal = ValorTotalAntesImpoconsumo + ValorImpoconsumoTotal;
    ValorTotal = ValorTotalAntesImpoconsumo + ValorImpoconsumoTotal;
    $("#tdValorTotal").html(ValorTotal.toFixed(numDecimales));
    $("#cphContenido_hddValorTotal").val(ValorTotal.toFixed(numDecimales));
    $("#hdd" + idVentaRapida + "Stock").val(parseFloat($("#hdd" + idVentaRapida + "Stock").val()) + parseFloat(Cantidad));
}

function LimpiarFacturaVentaRapida(hddItemsID) {
    $("#" + hddItemsID).val("");
    $(".FacturaRapidaBody").html("");
    $("#tdValorAntesIVA").html("0");
    $("#tdValorIVA").html("0");
    $("#tdValorImpoconsumo").html("0");
    $("#tdValorTotal").html("0");
    $("#cphContenido_hddValorTotal").html(ValorTotal);
}

function ActualizarPreciosVentaRapida(tnActulizarPreciosID, hddIdClienteID) {
    if ($("#" + hddIdClienteID).val() != "") {
        $("#" + tnActulizarPreciosID).click();
    }
}

//function ImprimirDocumentoVentaRapida(CuerpoImpresion) {
//    var oldPage = document.body.innerHTML;
//    document.body.innerHTML = "<html>" +
//        "<head>" +
//        "<title>" +
//        "</title>" +
//        "<style type='text/css'>" +
//        "</style>" +
//        "</head>" +
//        "<body style='margin:0px;'>" + CuerpoImpresion +
//        "</body></html>";
//    window.print();
//    window.setTimeout(function () {
//        RedireccionarBusqueda(oldPage);
//    }, 1000);
//}

//function ImprimirDocumentoVentaRapida(CuerpoImpresion) {
//    var IdTipoDocumento = getParameterByName('IdTipoDocumento');
//    if (IdTipoDocumento == null) {
//        IdTipoDocumento = 1;
//    }
//    var innerHTML = "<html>" +
//        "<head>" +
//        "<title>" +
//        "</title>" +
//        "<style type='text/css'>" +
//        "</style>" +
//        "</head>" +
//        "<body onload='window.print()' onafterprint='window.location.href = `frmVentaRapida.aspx?IdTipoDocumento=" + IdTipoDocumento + "`;' style='margin:0px;'>" + CuerpoImpresion +
//        "</body></html>";
//    document.open("text/html", "replace");
//    document.write(innerHTML);
//    document.close();

//}

function CargarVentaRapida(oldPage) {
    document.body.innerHTML = oldPage;
    var IdTipoDocumento = getParameterByName('IdTipoDocumento');
    if (IdTipoDocumento == null || IdTipoDocumento == 1) {
        window.location.href = "frmVentaRapida.aspx";
    }
    else if (IdTipoDocumento == 3) {
        window.location.href = "frmVentaRapida.aspx?IdTipoDocumento=3";
    }
    else {
        window.location.href = "frmVentaRapida.aspx?IdTipoDocumento=8";
    }
}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}