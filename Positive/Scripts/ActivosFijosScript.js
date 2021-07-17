//Variale que guarda el DataTale mas reciemte
var dtCurrent;
/*
    0-->Seguridad
    1-->Parametrización
    2-->Declarante
    3-->Campaña
*/
function SeleccionarMenu(Opcion) {
    var BackColorSel = "#008299";
    var BackColorNoSel = "#00A0B1";
    GuardarOpcionMenu(Opcion);
    if (Opcion == 0) {
        $("#trSeguridad").show();
        $("#trParametros").hide();
        $("#trDeclarante").hide();
        $("#trCampana").hide();
        $("#trReportes").hide();

        $("#tdSeguridad").css("background-color", BackColorSel);
        $("#tdParametros").css("background-color", BackColorNoSel);
        $("#tdCampana").css("background-color", BackColorNoSel);
        $("#tdDeclarante").css("background-color", BackColorNoSel);
        $("#tdReportes").css("background-color", BackColorNoSel);
    }
    if (Opcion == 1) {
        $("#trParametros").show();
        $("#trSeguridad").hide();
        $("#trDeclarante").hide();
        $("#trCampana").hide();
        $("#trReportes").hide();

        $("#tdSeguridad").css("background-color", BackColorNoSel);
        $("#tdParametros").css("background-color", BackColorSel);
        $("#tdCampana").css("background-color", BackColorNoSel);
        $("#tdDeclarante").css("background-color", BackColorNoSel);
        $("#tdReportes").css("background-color", BackColorNoSel);
    }
    if (Opcion == 2) {
        $("#trDeclarante").show();
        $("#trSeguridad").hide();
        $("#trParametros").hide();
        $("#trCampana").hide();
        $("#trReportes").hide();

        $("#tdSeguridad").css("background-color", BackColorNoSel);
        $("#tdParametros").css("background-color", BackColorNoSel);
        $("#tdCampana").css("background-color", BackColorNoSel);
        $("#tdDeclarante").css("background-color", BackColorSel);
        $("#tdReportes").css("background-color", BackColorNoSel);
    }
    if (Opcion == 3) {
        $("#trCampana").show();
        $("#trSeguridad").hide();
        $("#trParametros").hide();
        $("#trDeclarante").hide();
        $("#trReportes").hide();

        $("#tdSeguridad").css("background-color", BackColorNoSel);
        $("#tdParametros").css("background-color", BackColorNoSel);
        $("#tdCampana").css("background-color", BackColorSel);
        $("#tdDeclarante").css("background-color", BackColorNoSel);
        $("#tdReportes").css("background-color", BackColorNoSel);
    }
    if (Opcion == 4) {
        $("#trReportes").show();
        $("#trSeguridad").hide();
        $("#trParametros").hide();
        $("#trDeclarante").hide();
        $("#trCampana").hide();

        $("#tdSeguridad").css("background-color", BackColorNoSel);
        $("#tdParametros").css("background-color", BackColorNoSel);
        $("#tdCampana").css("background-color", BackColorNoSel);
        $("#tdDeclarante").css("background-color", BackColorNoSel);
        $("#tdReportes").css("background-color", BackColorSel);
    }
}

function PasarMayuscula(InputControl) {
    InputControl.value = InputControl.value.toUpperCase().replace(/\s/g, "");
}

function CopiarValor(ClientIDOrigen, CLienteIDDestino) {
    $("#" + CLienteIDDestino).val($("#" + ClientIDOrigen).val());
}
function PintarTabla(ClientID) {
    dtCurrent = $('#' + ClientID).DataTable({
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'JSON',
                action: function (e, dt, button, config) {
                    var data = dt.buttons.exportData();

                    $.fn.dataTable.fileSave(
                        new Blob([JSON.stringify(data)]),
                        'Export.json'
                    );
                }
            }
        ]
    });
    $(".dt-button").hide();
}

function MostrarMensaje(Titulo, Cuerpo) {
    $("#myModalLabel").html(Titulo);
    $("#myModalBody").html(Cuerpo);
    $('#myModal').modal();
}

function CrearPopUpFormulario(Titulo) {
    $(".ppForm").get(0).title = Titulo;
    $(".ppForm").dialog({ "autoOpen": false, "width": 700 });
}

function MostrarPopUpFormulario() {
    $(".ppForm").dialog("open");
}

function OcultarPopUpFormulario() {
    $(".ppForm").dialog("close");
}

function AplicarEstilos() {
    var BackColor = "#00A0B1";
    var fontcolor = "White";
    $(".ui-corner-all").removeClass("ui-corner-all");
    $(".ui-corner-top").css("background-color", BackColor);
    $(".ui-widget-header").css("background-color", BackColor);
    $(".ui-tabs-anchor").css("color", fontcolor);
    $(".ui-accordion-header").css("background-color", BackColor);
    $(".body-content").css("font-size", "small");
    EstablecerDataPicker();
}
function EstilosMenu() {
    $(".BotonMenu").css("cursor", "pointer");
    $(".BotonMenu").css("border", "solid 1px #008299");
    $(".BotonMenu").css("padding", "5px");
    $(".BotonMenu").css("text-align", "center");
    $(".Menu").css("vertical-align", "middle");
    $(".Menu").css("cursor", "pointer");
    $(".Menu").css("border", "solid 1px #008299");
    $(".Menu").css("padding", "5px");
    $(".Menu").css("text-align", "center");
    $(".Menu").css("width", "180px");
    $(".MenuInicio").css("vertical-align", "middle");
    $(".MenuInicio").css("cursor", "pointer");
    $(".MenuInicio").css("border", "solid 1px #008299");
    $(".MenuInicio").css("padding", "5px");
    $(".MenuInicio").css("text-align", "center");
}

function SeleccionarTab(IdTab) {
    $('#' + IdTab).trigger('click');
}

function EstablecerDataPicker() {
    $(".Fecha").blur(function () {
        FormatoFecha($(".Fecha").get(0));
    });
    $(".Fecha").datepicker({
        buttonImage: "Images/date.png",
        showOtherMonths: true,
        selectOtherMonths: true,
        dayNames: ["Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado"],
        dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
        monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
        dateFormat: "dd/mm/yy"
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

function EstablecerAutoCompleteActividad(_ControlID, _Url, _hddID, _Tarifa) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?ValorBusqueda=' + $("#" + _ControlID).val(),
                dataType: "json",
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Nombre,
                            value: item.Id
                        }
                    }))
                },
                error: function (a, b, c) {
                    debugger;
                }
            });

        },
        change: function (event, ui) {
            if (ui.item == null) {
                $("#" + _ControlID).val('');
                $("#" + _hddID).val('');
                $("#" + _Tarifa).val('');
                return false;
            }
        },
        select: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            $("#" + _Tarifa).val(datos[1]);
            return false;
        },
        focus: function (event, ui) {
            var datos = ui.item.value.split('@');
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(datos[0]);
            $("#" + _Tarifa).val(datos[1]);
            return false;
        }
    });
}
function AbrirNuevaVentana(UrlString) {
    window.open(UrlString);
}
function OcultarControlClass(className) {
    $("." + className).css("display", "none");
}
function PresionarBoton(ClientID) {
    $("#" + ClientID).click();
}



function CalcularDigitoVerificacion(txtIdentificacion, DVClientID, HDDClientID) {
    var vpri, x, y, z, i, nit1, dv1;
    nit1 = $(txtIdentificacion).val();
    if (isNaN(nit1)) {
        $("#" + DVClientID).val("");
        $("#" + HDDClientID).val("");
    } else {
        vpri = new Array(16);
        x = 0; y = 0; z = nit1.length;
        vpri[1] = 3;
        vpri[2] = 7;
        vpri[3] = 13;
        vpri[4] = 17;
        vpri[5] = 19;
        vpri[6] = 23;
        vpri[7] = 29;
        vpri[8] = 37;
        vpri[9] = 41;
        vpri[10] = 43;
        vpri[11] = 47;
        vpri[12] = 53;
        vpri[13] = 59;
        vpri[14] = 67;
        vpri[15] = 71;
        for (i = 0 ; i < z ; i++) {
            y = (nit1.substr(i, 1));
            //document.write(y+"x"+ vpri[z-i] +":");
            x += (y * vpri[z - i]);
            //document.write(x+"<br>");     
        }
        y = x % 11
        //document.write(y+"<br>");
        if (y > 1) {
            dv1 = 11 - y;
        } else {
            dv1 = y;
        }
        $("#" + DVClientID).val(dv1);
        $("#" + HDDClientID).val(dv1);
    }
}

function imprimirDocumento(strHTML,Url) {
    var oldPage = document.body.innerHTML;
    document.body.innerHTML =
              "<html>" +
                "<head>" +
                "<title>" +
                "</title>" +
                "<style type='text/css'>" +
                "</style>" +
                "</head>" +
                "<body style='margin:0px;'>" + strHTML +
                "</body>";
    window.print();
    document.body.innerHTML = oldPage;
    window.location.href = Url;
}

function EstablecerAutoCompleteArticulo(_ControlID, _Url, _hddID, CostoControlID,TipoDocumento) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?text=' + $("#" + _ControlID).val(),
                dataType: "json",
                type: "GET",
                success: function (data) {
                    response($.map(data, function (item) {
                        var Costo = item.UltimoCostoEntrada;
                        if (TipoDocumento == 1)//Salida
                        {
                            Costo = item.CostoPonderado;
                        }
                        return {
                            label: item.Nombre,
                            value: item.Codigo + '||' + Costo
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
            $("#" + _hddID).val(ui.item.value.split('||')[0]);
            $("#" + CostoControlID).val(ui.item.value.split('||')[1]);
            return false;
        },
        focus: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value.split('||')[0]);
            $("#" + CostoControlID).val(ui.item.value.split('||')[1]);
            return false;
        }
    });
}

function EstablecerAutoCompleteActivoFijo(_ControlID, _Url, _hddID) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?text=' + $("#" + _ControlID).val(),
                dataType: "json",
                type: "GET",
                success: function (data) {
                    if (data != "") {
                        response($.map(data, function (item) {
                            return {
                                label: item.NombreCompleto,
                                value: item.IdActivoFijo
                            }
                        }));
                    }
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
function EstablecerAutoCompleteActivoFijoEntrada(_ControlID, _Url, _hddID,CostoID,PlacaID) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?text=' + $("#" + _ControlID).val(),
                dataType: "json",
                type: "GET",
                success: function (data) {
                    if (data != "") {
                        response($.map(data, function (item) {
                            return {
                                label: item.NombreCompleto,
                                value: item.IdActivoFijo + "||" + item.ValorRazonable + "||" + item.NumeroPlaca
                            }
                        }));
                    }
                },
                error: function (a, b, c) {
                    debugger;
                }
            });
        },
        select: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value.split('||')[0]);
            $("#" + CostoID).val(ui.item.value.split('||')[1]);
            $("#" + PlacaID).val(ui.item.value.split('||')[2]);
            return false;
        },
        focus: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value.split('||')[0]);
            $("#" + CostoID).val(ui.item.value.split('||')[1]);
            $("#" + PlacaID).val(ui.item.value.split('||')[2]);
            return false;
        }
    });
}
function EstablecerAutoCompleteActivoFijoSalida(_ControlID, _Url, _hddID, CostoID, PlacaID) {
    $("#" + _ControlID).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: _Url + '?text=' + $("#" + _ControlID).val(),
                dataType: "json",
                type: "GET",
                success: function (data) {
                    if (data != "") {
                        response($.map(data, function (item) {
                            return {
                                label: item.ActivoFijo,
                                value: item.IdActivoFijoEmpleado + "||" + item.ValorRazonable + "||" + item.NumeroPlaca
                            }
                        }));
                    }
                },
                error: function (a, b, c) {
                    debugger;
                }
            });
        },
        select: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value.split('||')[0]);
            $("#" + CostoID).val(ui.item.value.split('||')[1]);
            $("#" + PlacaID).val(ui.item.value.split('||')[2]);
            return false;
        },
        focus: function (event, ui) {
            $("#" + _ControlID).val(ui.item.label);
            $("#" + _hddID).val(ui.item.value.split('||')[0]);
            $("#" + CostoID).val(ui.item.value.split('||')[1]);
            $("#" + PlacaID).val(ui.item.value.split('||')[2]);
            return false;
        }
    });
}
function DeleteDocumentItem(deleteObject, TotalLine) {
    dtCurrent
        .row($(deleteObject).parents('tr'))
        .remove()
        .draw();
    CalcDocumentTotal(TotalLine);
}

function AddDocumentItem(tblname, hddCodigoID, txtNombreID, txtCantidadID, txtCostoID, txtObs) {
    if ($("#" + hddCodigoID).val() != "" && $("#" + txtCantidadID).val() != "" && $("#" + txtCantidadID).val() > "0" && $("#" + txtCostoID).val() != "") {
        var TotalLine = (parseFloat($("#" + txtCostoID).val()) * parseFloat($("#" + txtCantidadID).val()));
        dtCurrent.row.add(
            [
                $("#" + hddCodigoID).val(),
                $("#" + txtNombreID).val(),
                $("#" + txtObs).val(),
                $("#" + txtCantidadID).val(),
                $("#" + txtCostoID).val(),
                TotalLine,
                "<span class='fa fa-times-circle fa-fw' onclick='DeleteDocumentItem(this," + (TotalLine * -1) + ");'></span>"
            ]
        ).draw(false);
        $("#" + hddCodigoID).val("");
        $("#" + txtNombreID).val("");
        $("#" + txtCantidadID).val("");
        $("#" + txtObs).val("");
        $("#" + txtCostoID).val("");
        $("#" + txtCantidadID).val("");
        setFocusClient(txtNombreID);
        CalcDocumentTotal(TotalLine);
        //$('#' + tblname).DataTable().draw();
    }
    else {
        MostrarMensaje("Campos Obligatorios", "Los campos Articulo, Cantidad y Costo son obligatorios, Cantidad debe ser mayor a cero");
    }
}
/* T
Entrada = 0,
Salida = 1,
Traslado = 2
*/

function checkDocumentSubmit(tipoDocumento, hddControlID, BodegaID, BodegaDestinoID) {
    if (confirm('Está seguro que desea guardar el documento?') ) {
        var dataSource = dtCurrent.buttons.exportData();
        if (dataSource.body != undefined && dataSource.body != null && dataSource.body != "") {
            $("#" + hddControlID).val(JSON.stringify(dataSource));
            if (tipoDocumento == 2) {
                if ($("#" + BodegaDestinoID).val() == "" || $("#" + BodegaID).val() == "") {
                    MostrarMensaje("Campos Obligatorios", "Los campos Bodegas son obligatoios para el tipo de documento Traslado");
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                if ($("#" + BodegaID).val() == "") {
                    MostrarMensaje("Campos Obligatorios", "El campo Bodega es obligatoio");
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        else {
            MostrarMensaje("Campos Obligatorios", "Debe seleccionar al menos un articulo");
            return false;
        }
    }
    else {
        return false;
        
        }
   }
function checkDocumentAFSubmit(hddControlID) {
    if (confirm('Está seguro que desea guardar el documento?')) {
        var dataSource = dtCurrent.buttons.exportData();
        $("#" + hddControlID).val(JSON.stringify(dataSource));
        if (dataSource.body != undefined && dataSource.body != null && dataSource.body != "") {
            return true;
        }
        else {
            MostrarMensaje("Campos Obligatorios", "Debe seleccionar al menos un Activo Fijo");
            return false;
        }
    }
    else {
        return false;

    }
}
function AddDocumentAFItem(tblname, hddActivoFIjoID, txtNombreID, txtPlacaID, txtCostoID) {
    if ($("#" + hddActivoFIjoID).val() != "" && $("#" + txtPlacaID).val() != "" && $("#" + txtNombreID).val() > "0" && $("#" + txtCostoID).val() != "") {
        dtCurrent.row.add(
            [
                $("#" + hddActivoFIjoID).val(),
                $("#" + txtPlacaID).val(),
                $("#" + txtNombreID).val(),
                $("#" + txtCostoID).val(),
                "<span class='fa fa-times-circle fa-fw' onclick='DeleteDocumentItem(this," + (parseFloat($("#" + txtCostoID).val()) * -1) + ");'></span>"
            ]
        ).draw(false);
        CalcDocumentTotal(parseFloat($("#" + txtCostoID).val()));
        $("#" + hddActivoFIjoID).val("");
        $("#" + txtPlacaID).val("");
        $("#" + txtNombreID).val("");
        $("#" + txtCostoID).val("");
        setFocusClient(txtNombreID);
        //$('#' + tblname).DataTable().draw();
    }
    else {
        MostrarMensaje("Campos Obligatorios", "Los campos Codigo, Nombre y Costo son obligatorios");
    }
}
function setFocusClient(ControlID){
    document.getElementById(ControlID).focus();
}
function CalcDocumentTotal(valor) {
  var total = 0;
  //valor = parseInt(valor); // Convertir el valor a un entero (número).
  // total = document.getElementById('spTotal').innerHTML;

  total = parseFloat(document.getElementById('spTotal').innerHTML);

  // Aquí valido si hay un valor previo, si no hay datos, le pongo un cero "0".
  total = (total == null || total == undefined || total == "") ? 0 : total;

  /* Esta es la suma. */
  // total = (parseInt(total) + parseInt(valor));
  total += parseFloat(valor);

  // Colocar el resultado de la suma en el control "span".
  document.getElementById('spTotal').innerHTML = total;
}

function BusquedaPrincipal() {
    window.location.href = "FixedAssetList.aspx?all=true&texto=" + $("#textoPrincipal").val();
}