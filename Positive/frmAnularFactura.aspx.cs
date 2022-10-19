using InventarioBusiness;
using InventarioItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;

namespace Positive
{
    public partial class frmAnularFactura : System.Web.UI.Page
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private long idDocumento = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            long.TryParse(Request.QueryString["idDocumento"], out idDocumento);
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.NotaCreditoVenta.GetHashCode().ToString()));
                if (oRolPagI.Insertar)
                {
                    AnularFactura();
                }
                else
                {
                    Response.Redirect("frmMantenimientos.aspx");
                }
            }
        }

        private bool ValidarCajaAbierta()
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                oCuaI.Estado = true;
                oCuaI = oCuaB.ObtenerCuadreCajaListaPorUsuario(oCuaI);
                if (oCuaI.idCuadreCaja == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void AnularFactura() {
            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
            tblDocumentoItem oDocItem = new tblDocumentoItem();
            tblDocumentoItem oDocItemSource = new tblDocumentoItem();
            List<tblDetalleDocumentoItem> oListDet = new List<tblDetalleDocumentoItem>();
            oDocItemSource = oDocB.traerDocumentoPorId(tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode(), idDocumento);
            if (oDocItemSource != null && oDocItemSource.idDocumento > 0 && oDocItemSource.idEmpresa == oUsuarioI.idEmpresa)
            {
                oDocItem.Referencia = oDocItemSource.NumeroDocumento.ToString();
                oDocItem.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                oDocItem.idTercero = oDocItemSource.idTercero;
                oDocItem.Telefono = oDocItemSource.Telefono;
                oDocItem.Direccion = oDocItemSource.Direccion;
                oDocItem.idCiudad = oDocItemSource.idCiudad;
                oDocItem.NombreTercero = oDocItemSource.NombreTercero;
                oDocItem.Observaciones = $"Documento creado para anular la factura {oDocItemSource.NumeroDocumento}";
                oDocItem.idEmpresa = oUsuarioI.idEmpresa;
                if (ValidarCajaAbierta())
                {
                    oDocItem.idUsuario = oUsuarioI.idUsuario;
                }
                else
                {
                    oDocItem.idUsuario = oDocItemSource.idUsuario;
                }
                oDocItem.TotalDocumento = oDocItemSource.TotalDocumento;
                oDocItem.TotalIVA = oDocItemSource.TotalIVA;
                oDocItem.saldo = 0;
                oDocItem.IdTipoDocumento = tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode();
                oDocItem.EnCuadre = false;
                oDocItem.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                oDocItem.Devuelta = 0;
                oDocItem.TotalDescuento = oDocItemSource.TotalDescuento;
                oDocItem.TotalAntesIVA = oDocItemSource.TotalAntesIVA;
                oDocItem.FechaVencimiento = DateTime.Now;
                oDocItem.Propina = oDocItemSource.Propina;
                oDocItem.Impoconsumo = oDocItemSource.Impoconsumo;
                oDocItem.Resolucion = oDocItemSource.Resolucion;
                oDocItem.TotalRetenciones = oDocItemSource.TotalRetenciones;
                oDocItem.Retenciones = new List<tblDocumentoRetencionItem>();
                foreach (var item in oDocItemSource.DocumentoLineas)
                {
                    tblDetalleDocumentoItem oDetI = new tblDetalleDocumentoItem();
                    oDetI.NumeroLinea = item.NumeroLinea;
                    oDetI.idArticulo = item.idArticulo;
                    oDetI.Articulo = item.Articulo;
                    oDetI.ValorUnitario = item.ValorUnitario;
                    oDetI.IVA = item.IVA;
                    oDetI.Cantidad = item.Cantidad;
                    oDetI.Descuento = item.Descuento;
                    oDetI.CostoPonderado = item.CostoPonderado;
                    oDetI.ValorDescuento = item.ValorDescuento;
                    oDetI.idBodega = item.idBodega;
                    oListDet.Add(oDetI);
                }
                if (oDocItemSource.Retenciones != null)
                {
                    foreach (var item in oDocItemSource.Retenciones)
                    {
                        tblDocumentoRetencionItem oRItem = new tblDocumentoRetencionItem();
                        oRItem.IdRetencion = item.IdRetencion;
                        oRItem.TipoDocumento = item.TipoDocumento;
                        oRItem.Porcentaje = item.Porcentaje;
                        oRItem.Base = item.Base;
                        oRItem.Valor = item.Valor;
                        oDocItem.Retenciones.Add(oRItem);
                    }
                }
                List<tblTipoPagoItem> oTipPagLis = new List<tblTipoPagoItem>();
                tblPagoItem oPagoI = new tblPagoItem();
                if (oDocB.GuardarTodo(oDocItem, oListDet, oPagoI, oTipPagLis))
                {
                    oDocItem = oDocB.traerDocumentoPorId(tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode(), oDocItem.idDocumento);
                    if(oDocItemSource.FormasPago.Count() > 0)
                    {
                        tblConciliacionBusiness oConB = new tblConciliacionBusiness(CadenaConexion);
                        tblConciliacionItem oConI = new tblConciliacionItem();
                        List<tblConciliacionDetalleItem> oListDetCon = new List<tblConciliacionDetalleItem>();
                        tblMovimientosDiariosItem oMovI = new tblMovimientosDiariosItem();
                        oConI = CargarDatosConciliacion(oDocItemSource);
                        oMovI = CargarDatosRetiro(oDocItemSource);

                        oListDetCon.Add(new tblConciliacionDetalleItem()
                        {
                            TipoDocumento = "NC",
                            NumeroDocumento = oDocItem.NumeroDocumento,
                            Valor = oDocItem.TotalDocumento
                        });

                        if (oConB.Guardar(oConI, oListDetCon, oMovI))
                        {
                            tblEmpresaItem oEmpI = new tblEmpresaItem();
                            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                            oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                            string Documentos = "";
                            Documentos = "<table border='1' style='width:100%'><tr><td align='center'>Documento</td><td align='center'>Valor</td></tr>";
                            Documentos = Documentos + "<tr><td>" + string.Format("Factura Venta-{0}", oDocItemSource.NumeroDocumento) + "</td>";
                            Documentos = Documentos + "<td align='right'>" + oDocItemSource.TotalDocumento.ToString(Util.ObtenerFormatoEntero()) + "</td></tr>";
                            Documentos = Documentos + "<tr><td>" + string.Format("Nota Credito Venta-{0}", oDocItem.NumeroDocumento) + "</td>";
                            Documentos = Documentos + "<td align='right'>" + oDocItemSource.TotalDocumento.ToString(Util.ObtenerFormatoEntero()) + "</td></tr>";
                            Documentos = Documentos + "</table>";
                            string Mensaje = "";
                            Mensaje = string.Format("<div style='position:relative;font-family:arial;'>" +
                                "<div style='font-size: 12px; font-weight: bold; width: 300px; padding-top: 20px; text-align: center;'>{0}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Nit: {1}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Direcci&oacute;n: {2}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align: center;'>Telefono: {3}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>{4}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px; text-align:center;'>Reintegro de Dinero</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Número: {5}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Tercero: {6}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px;'>Identificaci&oacute;n: {7}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; text-align:center; width:300px;'>Documentos</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>{11}</div>" +
                                "<div style='font-size: 14px;font-weight: bold; padding-top: 5px; width: 300px; text-align: right;'>Valor: {8}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Vende: {9}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 5px; width: 300px;'>Observaciones: {10}</div>" +
                                "<div style='font-size: 12px;font-weight: bold; padding-top: 15px; width: 300px;'>Firma: ____________________________</div>" +
                                "</div>", oEmpI.Nombre, oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, oConI.Fecha, oConI.IdConciliacion, oDocItemSource.NombreTercero,
                                oTerB.ObtenerTercero(oDocItemSource.idTercero, oUsuarioI.idEmpresa).Identificacion, oDocItemSource.TotalDocumento,
                                oUsuarioI.Usuario, oConI.Observaciones, Documentos);
                            string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirConciliacionLocal(\"{0}\");}});", Mensaje);
                            if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                            {
                                Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect($"frmVerDocumentos.aspx?opcionDocumento=1&Titulo=Exito&Mensaje=Se genero la devolución con exito.&TipoMensaje=1");
                    }
                }
                else
                {
                    Response.Redirect($"frmVerDocumentos.aspx?opcionDocumento=1&Titulo=Error&Mensaje=Error al anular el documento, contacte al administrador del sistema!&TipoMensaje=0");
                }
            }
            else {
                Response.Redirect($"frmVerDocumentos.aspx?opcionDocumento=1&Titulo=Error&Mensaje=Documento no valido!&TipoMensaje=0");
            }
        }
        private tblConciliacionItem CargarDatosConciliacion(tblDocumentoItem oDocItemSource)
        {
            try
            {
                tblConciliacionItem oConI = new tblConciliacionItem();
                oConI.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                oConI.IdTercero = oDocItemSource.idTercero;
                if (ValidarCajaAbierta())
                {
                    oConI.IdUsuario = oUsuarioI.idUsuario;
                }
                else
                {
                    oConI.IdUsuario = oDocItemSource.idUsuario;
                }
                oConI.Observaciones = $"Conciliación creada para anular la factura {oDocItemSource.NumeroDocumento}";
                oConI.Total = oDocItemSource.TotalDocumento;
                oConI.IdEmpresa = oUsuarioI.idEmpresa;
                return oConI;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private tblMovimientosDiariosItem CargarDatosRetiro(tblDocumentoItem oDocItemSource)
        {
            try
            {
                tblMovimientosDiariosItem oMovI = new tblMovimientosDiariosItem();
                oMovI.idTipoMovimiento = 1;
                oMovI.fechaMovimiento = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                if (ValidarCajaAbierta())
                {
                    oMovI.idUsuario = oUsuarioI.idUsuario;
                }
                else
                {
                    oMovI.idUsuario = oDocItemSource.idUsuario;
                }
                oMovI.valor = oDocItemSource.TotalDocumento;
                oMovI.observaciones = string.Format("Devolución de dinero al cliente por medio de conciliación.");
                oMovI.idEmpresa = oUsuarioI.idEmpresa;
                oMovI.EnCuadre = false;
                return oMovI;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}