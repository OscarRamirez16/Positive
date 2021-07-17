using System;
using InventarioBusiness;
using InventarioItem;
using Idioma;
using HQSFramework.Base;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;

namespace Inventario
{
    public partial class frmConciliacion : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgSaldosColumnsEnum
        {
            Seleccionar = 0,
            Tipo = 1,
            Id = 2,
            NumeroDocumento = 3,
            Saldo = 4
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
            oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
            if (oUsuarioI != null)
            {
                hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                ConfiguracionIdioma();
                SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Conciliacion.GetHashCode().ToString()));
                if (oRolPagI.Insertar && ValidarCajaAbiertaUsuario())
                {
                    txtIdentificacion.Attributes.Add("onblur", string.Format("EstablecerAutoCompleteClientePorIdentificacion('{0}','Ashx/Tercero.ashx','{1}','{2}')", txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID));
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} menu();", strScript);
                        strScript = string.Format("{0} EstablecerAutoCompleteCliente('{1}','Ashx/Tercero.ashx','{2}','{3}');", strScript, txtTercero.ClientID, hddIdCliente.ClientID, txtIdentificacion.ClientID);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                    }
                }
                else
                {
                    Response.Redirect("frmMantenimientos.aspx?Error=El usuario no tiene permisos o no tiene una caja abierta.");
                }
            }
        }

        private bool ValidarCajaAbiertaUsuario()
        {
            try
            {
                tblCuadreCajaBusiness oCuaB = new tblCuadreCajaBusiness(CadenaConexion);
                tblCuadreCajaItem oCuaI = new tblCuadreCajaItem();
                oCuaI.idEmpresa = oUsuarioI.idEmpresa;
                oCuaI.idUsuarioCaja = oUsuarioI.idUsuario;
                oCuaI.Estado = true;
                if (oCuaB.ValidarCajaAbierta(oCuaI) == 0)
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
            lblTitulo.Text = "Conciliacion";
            lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente);
            lblIdentificacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
            lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
        }

        protected void AdicionarPago(object sender, EventArgs e)
        {
            txtTotal.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoEntero());
            foreach (DataGridItem Item in dgSaldos.Items)
            {
                if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                {
                    txtTotal.Text = Math.Round((decimal.Parse(txtTotal.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString();
                }
            }
        }

        protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hddIdCliente.Value))
                {
                    tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                    dgSaldos.DataSource = oDocB.ObtenerSaldoDocumentoAFavorClienteTodos(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa);
                    dgSaldos.DataBind();
                }
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los documentos. {0}", ex.Message));
            }
        }

        protected void btnGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                tblConciliacionBusiness oConB = new tblConciliacionBusiness(CadenaConexion);
                tblConciliacionItem oConI = new tblConciliacionItem();
                List<tblConciliacionDetalleItem> oListDet = new List<tblConciliacionDetalleItem>();
                tblMovimientosDiariosItem oMovI = new tblMovimientosDiariosItem();
                oConI = CargarDatosConciliacion();
                oMovI = CargarDatosRetiro();
                foreach(DataGridItem Item in dgSaldos.Items)
                {
                    if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                    {
                        tblConciliacionDetalleItem oCotD = new tblConciliacionDetalleItem();
                        oCotD.TipoDocumento = Item.Cells[dgSaldosColumnsEnum.Tipo.GetHashCode()].Text.Trim();
                        oCotD.NumeroDocumento = Item.Cells[dgSaldosColumnsEnum.NumeroDocumento.GetHashCode()].Text;
                        oCotD.Valor = decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency);
                        oListDet.Add(oCotD);
                    }
                }
                if(oListDet.Count > 0)
                {
                    if (oConB.Guardar(oConI, oListDet, oMovI))
                    {
                        tblEmpresaItem oEmpI = new tblEmpresaItem();
                        tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                        tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                        oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                        string Documentos = "";
                        Documentos = "<table border='1' style='width:100%'><tr><td align='center'>Documento</td><td align='center'>Valor</td></tr>";
                        foreach (DataGridItem Item in dgSaldos.Items)
                        {
                            if (((CheckBox)(Item.Cells[dgSaldosColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                            {
                                Documentos = Documentos + "<tr><td>" + string.Format("{0}-{1}", Item.Cells[dgSaldosColumnsEnum.Tipo.GetHashCode()].Text, Item.Cells[dgSaldosColumnsEnum.NumeroDocumento.GetHashCode()].Text) + "</td>";
                                Documentos = Documentos + "<td align='right'>" + decimal.Parse(Item.Cells[dgSaldosColumnsEnum.Saldo.GetHashCode()].Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoEntero()) + "</td></tr>";
                            }
                        }
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
                            "</div>", oEmpI.Nombre, oEmpI.Identificacion, oEmpI.Direccion, oEmpI.Telefono, oConI.Fecha, oConI.IdConciliacion, txtTercero.Text,
                            oTerB.ObtenerTercero(long.Parse(hddIdCliente.Value), oUsuarioI.idEmpresa).Identificacion, txtTotal.Text,
                            oUsuarioI.Usuario, oConI.Observaciones, Documentos);
                        string strScript = string.Format("jQuery(document).ready(function(){{ ImprimirConciliacion(\"{0}\");}});", Mensaje);
                        if (!Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScriptImprimir"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(GetType(), "InicializarControlesScriptImprimir", strScript, true);
                        }
                    }
                    else
                    {
                        MostrarMensaje("Error", "No se pudo realizar la conciliación.");
                    }
                }
                else
                {
                    MostrarMensaje("Error", "Debe seleccionar al menos un documento.");
                }
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo guardar la operación. {0}", ex.Message));
            }
        }

        private void LimpiarControles()
        {
            txtTercero.Text = "";
            txtIdentificacion.Text = "";
            hddIdCliente.Value = "";
            dgSaldos.DataSource = null;
            dgSaldos.DataBind();
            txtTotal.Text = "";
            txtObservaciones.Text = "";
            txtTercero.Focus();
        }

        private tblConciliacionItem CargarDatosConciliacion()
        {
            try
            {
                tblConciliacionItem oConI = new tblConciliacionItem();
                oConI.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                oConI.IdTercero = long.Parse(hddIdCliente.Value);
                oConI.IdUsuario = oUsuarioI.idUsuario;
                oConI.Observaciones = txtObservaciones.Text;
                oConI.Total = decimal.Parse(txtTotal.Text, NumberStyles.Currency);
                oConI.IdEmpresa = oUsuarioI.idEmpresa;
                return oConI;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private tblMovimientosDiariosItem CargarDatosRetiro()
        {
            try
            {
                tblMovimientosDiariosItem oMovI = new tblMovimientosDiariosItem();
                oMovI.idTipoMovimiento = 1;
                oMovI.fechaMovimiento = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                oMovI.idUsuario = oUsuarioI.idUsuario;
                oMovI.valor = decimal.Parse(txtTotal.Text, NumberStyles.Currency);
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