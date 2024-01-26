using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Positive
{
    public partial class frmTrasladoMasivoMercancia : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private enum dgTrasladosEnum
        {
            IdArticulo = 0,
            CodigoArticulo = 1,
            Nombre = 2,
            IdBodegaOrigen = 3,
            BodegaOrigen = 4,
            Cantidad = 5,
            IdBodegaDestino = 6,
            BodegaDestino = 7
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.TrasladoMasivoTemplate.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            OcultarControl(divBotones.ClientID);
                            OcultarControl(divGrilla.ClientID);
                            OcultarControl(divObservaciones.ClientID);
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} ConfigurarEnter();", strScript);
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
                    string error = oABiz.LeerArticulosParaTrasladoMasivoTemplate(fulArticulos.PostedFile.InputStream, Articulos, oUsuarioI.idEmpresa);
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
        protected void btnCancelarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
        protected void btnGuardarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (dgArticulosMasivo.Items.Count > 0)
                {
                    string Errores = ValidarInventarioSuficiente();
                    if (string.IsNullOrEmpty(Errores))
                    {
                        tblTrasladoMercanciaBusiness oTraB = new tblTrasladoMercanciaBusiness(CadenaConexion);
                        tblTrasladoMercanciaItem oTraI = new tblTrasladoMercanciaItem();
                        List<tblTrasladoMercanciaDetalle> oListDetI = new List<tblTrasladoMercanciaDetalle>();
                        oTraI.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                        oTraI.IdEmpresa = oUsuarioI.idEmpresa;
                        oTraI.IdUsuario = oUsuarioI.idUsuario;
                        oTraI.Obervaciones = txtObservaciones.Text;
                        foreach (DataGridItem Item in dgArticulosMasivo.Items)
                        {
                            tblTrasladoMercanciaDetalle Detalle = new tblTrasladoMercanciaDetalle();
                            Detalle.IdArticulo = long.Parse(Item.Cells[dgTrasladosEnum.IdArticulo.GetHashCode()].Text);
                            Detalle.Cantidad = decimal.Parse(Item.Cells[dgTrasladosEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                            Detalle.IdBodegaOrigen = long.Parse(Item.Cells[dgTrasladosEnum.IdBodegaOrigen.GetHashCode()].Text);
                            Detalle.IdBodegaDestino = long.Parse(Item.Cells[dgTrasladosEnum.IdBodegaDestino.GetHashCode()].Text);
                            oListDetI.Add(Detalle);
                        }
                        if (oTraB.Guardar(oTraI, oListDetI))
                        {
                            MostrarAlerta(1, "Exito", "El traslado de mercancía se realizó con exito.");
                            LimpiarControles();
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "No se pudo realizar el traslado de mercancía.");
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", Errores);
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "No hay artículos para realizar el traslado.");
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
                    decimal Disponibles = oArtB.DisponibilidadArticuloEnBodega(long.Parse(Item.Cells[dgTrasladosEnum.IdArticulo.GetHashCode()].Text), long.Parse(Item.Cells[dgTrasladosEnum.IdBodegaOrigen.GetHashCode()].Text));
                    if (decimal.Parse(Item.Cells[dgTrasladosEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency) > Disponibles)
                    {
                        if (string.IsNullOrEmpty(Errores))
                        {
                            Errores = string.Format("El artículo {0} - {1} no tiene suficientes existencias. Disponibilidad {2}", Item.Cells[dgTrasladosEnum.CodigoArticulo.GetHashCode()].Text, Item.Cells[dgTrasladosEnum.Nombre.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
                        }
                        else
                        {
                            Errores = string.Format("{0}. El artículo {1} - {2} no tiene suficientes existencias . Disponibilidad {3}", Errores, Item.Cells[dgTrasladosEnum.CodigoArticulo.GetHashCode()].Text, Item.Cells[dgTrasladosEnum.Nombre.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
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
        private void LimpiarControles()
        {
            try
            {
                OcultarControl(divBotones.ClientID);
                OcultarControl(divGrilla.ClientID);
                OcultarControl(divObservaciones.ClientID);
                dgArticulosMasivo.DataSource = null;
                dgArticulosMasivo.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}