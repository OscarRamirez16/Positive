function EjecutarClick(_Btn) {
    _Btn.click();
}

function EstablecerMascaras() {
    $('.Decimal').mask('000,000,000,000,000.00', { reverse: true });
    $('.Entero').mask('000,000,000,000,000', { reverse: true });
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

function EstablecerTituloPagina(Titulo) {
    document.title = Titulo;
}

function EstablecerAutoCompleteTercero(_ControlID, _Url, _hddID, _identificacionID, _TipoFactura, _IdEmpresa) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&tipoFactura=' + _TipoFactura + '&i_e=' + _IdEmpresa,
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
            if ($("#" + _ControlID).val() == "") {
                $("#" + _hddID).val('0');
                $('#ContentPlaceHolder1_divDetalles').hide();
            }
        },
        select: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            if (_identificacionID) {
                $("#" + _identificacionID).val(datos[1]);
            }
            $('#ContentPlaceHolder1_divDetalles').show();
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            if (_identificacionID) {
                $("#" + _identificacionID).val(datos[1]);
            }
            return false;
        }
    });
}

function EstablecerAutoCompleteArticulo(_ControlID, _Url, _hddID, _Opcion, _TipoFactura, _IdEmpresa, _BodegaUsuario, _IdTercero) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val() + '&opcion=' + _Opcion + '&i_e=' + _IdEmpresa + '&tipoFactura=' + _TipoFactura + '&i_b=' + _BodegaUsuario + '&i_t=' + _IdTercero,
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
            if ($("#" + _ControlID).val() == "") {
                $("#" + _hddID).val('0');
            }
        },
        select: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            var PRECIO = parseFloat(datos[2]);
            $("#" + _Precio).val(formatNumber.new(PRECIO));
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            var PRECIO = parseFloat(datos[2]);
            $("#" + _Precio).val(formatNumber.new(PRECIO));
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