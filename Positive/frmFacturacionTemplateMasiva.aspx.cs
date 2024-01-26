using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;
using System.Globalization;
using System.Linq;

namespace Positive
{
    public partial class frmFacturacionTemplateMasiva : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        enum dgArticulosColumnsEnum
        {
            IdArticulo = 0,
            CodigoArticulo = 1,
            Nombre = 2,
            IdBodega = 3,
            Bodega = 4,
            Cantidad = 5,
            Precio = 6,
            Costo = 7,
            IVA = 8,
            CostoPonderado = 9
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.FacturacionMasivaTemplate.GetHashCode().ToString()));
                    if (oRolPagI.Leer && ValidarCajaAbierta())
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            OcultarControl(divBotones.ClientID);
                            OcultarControl(divGrilla.ClientID);
                            OcultarControl(divObservaciones.ClientID);
                            tblTerceroItem oTerI = new tblTerceroItem();
                            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                            oTerI = oTerB.ObtenerClienteProveedorGenerico(oUsuarioI.idEmpresa, "C");
                            if(oTerI.IdTercero > 0)
                            {
                                txtTercero.Text = oTerI.Nombre;
                                hddIdCliente.Value = oTerI.IdTercero.ToString();
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "No tiene un cliente generico configurado");
                                btnGuardarMasivo.Enabled = false;
                            }
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} ConfigurarEnter();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                    else
                    {
                        Response.Redirect("frmMantenimientos.aspx");
                    }
                }
                else
                {
                    Response.Redirect("frmInicioSesion.aspx");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        private bool ValidarCajaAbierta()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["consulta"]) || Request.QueryString["TipoDocumento"] == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString())
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
                        tblCajaItem oCajaI = new tblCajaItem();
                        tblCajaBusiness oCajaB = new tblCajaBusiness(CadenaConexion);
                        oCajaI = oCajaB.ObtenerCajaPorID(oCuaI.idCaja);
                        hddResolucion.Value = oCajaI.Resolucion;
                        List<tblCajaItem> oListCajaI = new List<tblCajaItem>();
                        oListCajaI = oCajaB.ObtenerCajaProximaVencer(oUsuarioI.idEmpresa);
                        if (oListCajaI.Count > 0)
                        {
                            if (oListCajaI.Where(x => x.idCaja == oCajaI.idCaja && ((x.FechaVencimiento - DateTime.Now).Days <= 0)).ToList().Count() > 0)
                            {
                                Response.Redirect(string.Format("frmMantenimientos.aspx?Error=La caja {0} venció, fecha de vencimiento {1}", oCajaI.nombre, oCajaI.FechaVencimiento.ToShortDateString()));
                            }
                        }
                        return true;
                    }
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
        private void ConfiguracionIdioma()
        {
            Traductor oCIdioma = new Traductor();
            short Opcion;
            Idioma.Traductor.IdiomaEnum Idioma = Traductor.IdiomaEnum.Espanol;
            if (string.IsNullOrEmpty(Request.Form["ctl00$ddlIdiomas"]))
            {
                Opcion = oUsuarioI.IdIdioma;
            }
            else
            {
                Opcion = short.Parse(Request.Form["ctl00$ddlIdiomas"]);
            }
            if (Opcion == Traductor.IdiomaEnum.Espanol.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Espanol;
            }
            if (Opcion == Traductor.IdiomaEnum.Ingles.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Ingles;
            }
            if (Opcion == Traductor.IdiomaEnum.Aleman.GetHashCode())
            {
                Idioma = Traductor.IdiomaEnum.Aleman;
            }
        }
        protected void btnCargar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (fulArticulos.HasFile)
                {
                    btnGuardarMasivo.Visible = true;
                    List<tblArticuloItem> Articulos = new List<tblArticuloItem>();
                    tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                    string error = oABiz.LeerArticulosParaFacturacionMasivaTemplate(fulArticulos.PostedFile.InputStream, Articulos, oUsuarioI.idEmpresa);
                    if (error != "")
                    {
                        MostrarAlerta(0, "Error", error);
                    }
                    else
                    {
                        dgArticulosMasivo.DataSource = Articulos;
                        dgArticulosMasivo.DataBind();
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "No hay un archivo seleccionado");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        private string ValidarInventarioSuficiente()
        {
            string Errores = "";
            try
            {
                tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                foreach (DataGridItem Item in dgArticulosMasivo.Items)
                {
                    long IdArticulo = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text);
                    long IdBodega = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text);
                    decimal Cantidad = 0;
                    tblArticuloItem oArtI = new tblArticuloItem();
                    oArtI = oArtB.ObtenerArticuloPorID(IdArticulo, oUsuarioI.idEmpresa);
                    if (oArtI.EsInventario)
                    {
                        foreach (DataGridItem Item1 in dgArticulosMasivo.Items)
                        {
                            if (IdArticulo.ToString() == Item1.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text)
                            {
                                Cantidad = Cantidad + decimal.Parse(Item1.Cells[dgArticulosColumnsEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                            }
                        }
                        decimal Disponibles = oArtB.DisponibilidadArticuloEnBodega(IdArticulo, IdBodega);
                        if (Cantidad > Disponibles)
                        {
                            if (string.IsNullOrEmpty(Errores))
                            {
                                Errores = string.Format("El artículo {0} - {1} no tiene suficientes existencias. Disponibilidad {2}", oArtI.CodigoArticulo, oArtI.Nombre, Disponibles.ToString(Util.ObtenerFormatoEntero()).Replace("$", ""));
                            }
                            else
                            {
                                Errores = string.Format("{0}. El artículo {1} - {2} no tiene suficientes existencias. Disponibilidad {3}", Errores, oArtI.CodigoArticulo, oArtI.Nombre, Disponibles.ToString(Util.ObtenerFormatoEntero()).Replace("$", ""));
                            }
                        }
                    }
                }
                return Errores;
            }
            catch (Exception ex)
            {
                Errores = ex.Message;
                return Errores;
            }
        }
        protected void btnCancelarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
        private void LimpiarControles()
        {
            try
            {
                OcultarControl(divBotones.ClientID);
                OcultarControl(divGrilla.ClientID);
                OcultarControl(divObservaciones.ClientID);
                dgArticulosMasivo.DataSource = null;
                dgArticulosMasivo.DataBind();
                txtTercero.Text = "";
                hddIdCliente.Value = "";
                txtObservaciones.Text = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (oRolPagI.Insertar)
                {
                    string Errores = ValidarInventarioSuficiente();
                    if (string.IsNullOrEmpty(Errores))
                    {
                        tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                        tblTerceroItem oTerI = new tblTerceroItem();
                        tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                        tblDocumentoItem oDocI = new tblDocumentoItem();
                        List<tblDetalleDocumentoItem> oListDet = new List<tblDetalleDocumentoItem>();
                        oDocI.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                        if (string.IsNullOrEmpty(hddIdCliente.ClientID))
                        {
                            oTerI = oTerB.ObtenerClienteProveedorGenerico(oUsuarioI.idEmpresa, "C");
                        }
                        else
                        {
                            oTerI = oTerB.ObtenerTercero(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa);
                        }
                        oDocI.idTercero = oTerI.IdTercero;
                        oDocI.Telefono = oTerI.Telefono;
                        oDocI.Direccion = oTerI.Direccion;
                        oDocI.idCiudad = oTerI.idCiudad;
                        oDocI.NombreTercero = oTerI.Nombre;
                        oDocI.Observaciones = txtObservaciones.Text.Replace(Environment.NewLine, " ");
                        oDocI.idEmpresa = oUsuarioI.idEmpresa;
                        oDocI.idUsuario = oUsuarioI.idUsuario;
                        oDocI.EnCuadre = false;
                        oDocI.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                        oDocI.FechaVencimiento = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                        oDocI.IdTipoDocumento = int.Parse(hddTipoDocumento.Value);
                        oDocI.Resolucion = hddResolucion.Value;
                        short NumeroLinea = 1;
                        decimal TotalDocumento = 0, TotalIVA = 0;
                        foreach (DataGridItem Item in dgArticulosMasivo.Items)
                        {
                            tblDetalleDocumentoItem oDetI = new tblDetalleDocumentoItem();
                            oDetI.NumeroLinea = NumeroLinea;
                            oDetI.idArticulo = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text);
                            oDetI.Codigo = Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text;
                            oDetI.Articulo = Item.Cells[dgArticulosColumnsEnum.Nombre.GetHashCode()].Text;
                            oDetI.ValorUnitario = (decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Precio.GetHashCode()].Text, NumberStyles.Currency) / (1 + (decimal.Parse(Item.Cells[dgArticulosColumnsEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100)));
                            oDetI.IVA = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.IVA.GetHashCode()].Text, NumberStyles.Currency);
                            oDetI.Cantidad = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                            oDetI.idBodega = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text);
                            oDetI.CostoPonderado = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Costo.GetHashCode()].Text, NumberStyles.Currency);
                            oListDet.Add(oDetI);
                            TotalDocumento = TotalDocumento + (oDetI.ValorUnitario * oDetI.Cantidad);
                            TotalIVA = TotalIVA + (decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Precio.GetHashCode()].Text, NumberStyles.Currency) * (decimal.Parse(Item.Cells[dgArticulosColumnsEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100));
                            NumeroLinea++;
                        }
                        oDocI.TotalDescuento = 0;
                        oDocI.TotalDocumento = TotalDocumento;
                        oDocI.TotalIVA = TotalIVA;
                        oDocI.TotalAntesIVA = oDocI.TotalDocumento - oDocI.TotalIVA;
                        oDocI.saldo = oDocI.TotalDocumento;
                        //Cargar datos pagos
                        List<tblTipoPagoItem> oTipPagLis = new List<tblTipoPagoItem>();
                        tblPagoItem oPagoI = new tblPagoItem();
                        if (!chkCredito.Checked)
                        {
                            oPagoI.idTercero = long.Parse(hddIdCliente.Value);
                            oPagoI.idEmpresa = oUsuarioI.idEmpresa;
                            oPagoI.fechaPago = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                            oPagoI.idUsuario = oUsuarioI.idUsuario;
                            oPagoI.idEstado = short.Parse(tblPagoBusiness.EstadoPago.Definitivo.GetHashCode().ToString());
                            oPagoI.EnCuadre = false;
                            tblTipoPagoItem oTipPagI = new tblTipoPagoItem();
                            oTipPagI.idFormaPago = short.Parse(tblPagoBusiness.FormaPagoEnum.Efectivo.GetHashCode().ToString());
                            oTipPagI.ValorPago = TotalDocumento;
                            oPagoI.totalPago = TotalDocumento;
                            oDocI.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Pagado.GetHashCode();
                            oTipPagLis.Add(oTipPagI);
                        }
                        if (oListDet.Count > 0)
                        {
                            if (oDocB.GuardarTodo(oDocI, oListDet, oPagoI, oTipPagLis))
                            {
                                MostrarAlerta(1, "Exito", "El documento se creó con exito.");
                                LimpiarControles();
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "El documento no se pudo guardar");
                            }
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "No hay artículos para realizar la operación.");
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", Errores);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
    }
}