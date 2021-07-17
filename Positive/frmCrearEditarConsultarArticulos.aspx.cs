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

namespace Inventario
{
    public partial class frmCrearEditarArticulos : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgBodegasColumnsEnum
        {
            IdBodega = 0,
            Descripcion = 1,
            Cantidad = 2,
            TipoManejoPrecio = 3,
            Precio = 4,
            Costo = 5,
            Seleccionar = 6
        }

        enum dgArticulosColumnsEnum
        {
            Numero = 0,
            IdArticulo = 1,
            CodigoArticulo = 2,
            Nombre = 3,
            Presentacion = 4,
            IdLinea = 5,
            Linea = 6,
            IVACompra = 7,
            IVAVenta = 8,
            CodigoBarra = 9,
            IdTercero = 10,
            Tercero = 11,
            IdEmpresa = 12,
            Empresa = 13,
            IdBodega = 14,
            Bodega = 15,
            EsInventario = 16,
            StockMinimo = 17,
            Existencias = 18,
            Activo = 19,
            PrecioAutomatico = 20,
            Editar = 21
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
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarArticulos.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        string UltimoCodigo = (new tblArticuloBusiness(CadenaConexion)).ArticuloObtenerUltimoCodigo(oUsuarioI.idEmpresa);
                        imgUltimoCodigo.Attributes.Add("title", $"Ultimo código de articulo creado: {UltimoCodigo}");
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarBodegas();
                            txtIVACompra.Text = "0.00";
                            txtIVAVenta.Text = "0.00";
                            txtStockMinimo.Text = "0.00";
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} pestañas();", strScript);
                            strScript = string.Format("{0} ConfigurarEnter();", strScript);
                            strScript = string.Format("{0} seleccionarTab('contenido','1');", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteTercero('{1}','Ashx/Tercero.ashx','{2}');", strScript, txtTercero.ClientID, hddIdTercero.ClientID);
                            strScript = string.Format("{0} EstablecerAutoCompleteTercero('{1}','Ashx/Tercero.ashx','{2}');", strScript, txtProveedorBus.ClientID, hddIdProBus.ClientID);
                            strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}','{3}','3');", strScript, txtPadre.ClientID, hddIdPadre.ClientID, oUsuarioI.idEmpresa);
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','1');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                        if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["texto"])) {
                            txtNombreBus.Text = Request.QueryString["texto"];
                            CargarArticulos();
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
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
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
            liCrearArticulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo));
            liArticulosCreados.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulos));
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo));
            lblTituloGrilla.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Asignacion), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodegas));
            lblNombre.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
            txtNombre.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            lblPresentacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Presentacion);
            txtPresentacion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Presentacion));
            lblCodigo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo);
            txtCodigo.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo));
            lblCodigoBarra.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoBarra);
            txtCodigoBarra.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoBarra));
            lblTercero.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio);
            txtTercero.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio));
            lblIVACompra.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVACompra);
            txtIVACompra.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVA));
            lblIVAVenta.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVAVenta);
            txtIVAVenta.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVA));
            lblStock.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.StockMinimo);
            txtStockMinimo.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.StockMinimo));
            lblBodega.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
            txtBodega.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega));
            chkInventario.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Es), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Inventario));
            chkPrecioAuto.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.PrecioAutomatico);
            chkActivo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Activo);
            txtNombreBus.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            txtProveedorBus.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio));
            lblListaArticulos.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulos));
            lblLinea.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea);
            lblUbicacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ubicacion);
            CargarLineas(oCIdioma, Idioma);
            ConfigurarIdiomaGrillaArticulo(oCIdioma, Idioma);
            ConfigurarIdiomaGrillaBodegas(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrillaArticulo(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgArticulos.Columns[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo);
                dgArticulos.Columns[dgArticulosColumnsEnum.Nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                dgArticulos.Columns[dgArticulosColumnsEnum.Presentacion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Presentacion);
                dgArticulos.Columns[dgArticulosColumnsEnum.Linea.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea);
                dgArticulos.Columns[dgArticulosColumnsEnum.IVACompra.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVACompra);
                dgArticulos.Columns[dgArticulosColumnsEnum.IVAVenta.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVAVenta);
                dgArticulos.Columns[dgArticulosColumnsEnum.Tercero.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
                dgArticulos.Columns[dgArticulosColumnsEnum.EsInventario.GetHashCode()].HeaderText = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Es), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Inventario));
                dgArticulos.Columns[dgArticulosColumnsEnum.StockMinimo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.StockMinimo);
                dgArticulos.Columns[dgArticulosColumnsEnum.Existencias.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConfigurarIdiomaGrillaBodegas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgBodegas.Columns[dgBodegasColumnsEnum.Descripcion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion);
                dgBodegas.Columns[dgBodegasColumnsEnum.Cantidad.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad);
                dgBodegas.Columns[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TipoManejo);
                dgBodegas.Columns[dgBodegasColumnsEnum.Costo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Costo);
                dgBodegas.Columns[dgBodegasColumnsEnum.Precio.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio);
                dgBodegas.Columns[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarLineas(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                tblLineaBussines oLineaB = new tblLineaBussines(CadenaConexion);
                string Opcion = ddlLinea.SelectedValue;
                string OpcionBus = ddlLineaBus.SelectedValue;
                ddlLinea.Items.Clear();
                ddlLineaBus.Items.Clear();
                ddlLinea.SelectedValue = null;
                ddlLineaBus.SelectedValue = null;
                ddlLinea.DataSource = oLineaB.ObtenerLineaLista(oUsuarioI.idEmpresa);
                ddlLinea.DataBind();
                ddlLinea.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea)), "0"));
                ddlLineaBus.DataSource = oLineaB.ObtenerLineaLista(oUsuarioI.idEmpresa);
                ddlLineaBus.DataBind();
                ddlLineaBus.Items.Add(new ListItem(string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea)), "0"));
                if (!IsPostBack)
                {
                    ddlLinea.SelectedValue = "0";
                    ddlLineaBus.SelectedValue = "0";
                }
                else
                {
                    ddlLinea.SelectedValue = Opcion;
                    ddlLineaBus.SelectedValue = OpcionBus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarBodegas()
        {
            try
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                dgBodegas.DataSource = oBodB.ObtenerBodegaLista(oUsuarioI.idEmpresa);
                dgBodegas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblArticuloBusiness oArticuloB = new tblArticuloBusiness(CadenaConexion);
                tblArticuloItem oArticuloI = new tblArticuloItem();
                CargarDatosArticulo(ref oArticuloI);
                if (oRolPagI.Insertar && oArticuloI.IdArticulo == 0 && validarSeleccionarBodegas())
                {
                    if (oArticuloB.insertar(oArticuloI))
                    {
                        tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                        oBodB.EliminarBodegasArticulos(oArticuloI.IdArticulo);
                        foreach (DataGridItem item in dgBodegas.Items)
                        {
                            if (((CheckBox)(item.Cells[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkBodega"))).Checked == true)
                            {
                                tblArticulo_BodegaItem oArtBodI = new tblArticulo_BodegaItem();
                                oArtBodI.IdBodega = short.Parse(item.Cells[dgBodegasColumnsEnum.IdBodega.GetHashCode()].Text);
                                oArtBodI.IdArticulo = oArticuloI.IdArticulo;
                                oArtBodI.Cantidad = decimal.Parse(((TextBox)(item.Cells[dgBodegasColumnsEnum.Cantidad.GetHashCode()].FindControl("txtCantidad"))).Text, NumberStyles.Currency);
                                oArtBodI.Precio = decimal.Parse(((TextBox)(item.Cells[dgBodegasColumnsEnum.Precio.GetHashCode()].FindControl("txtPrecio"))).Text, NumberStyles.Currency);
                                oArtBodI.Costo = decimal.Parse(((TextBox)(item.Cells[dgBodegasColumnsEnum.Costo.GetHashCode()].FindControl("txtCosto"))).Text, NumberStyles.Currency);
                                oArtBodI.IdTipoManejoPrecio = short.Parse(((DropDownList)(item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).SelectedValue);
                                oBodB.GuardarArticuloPorBodega(oArtBodI);
                            }
                        }
                        MostrarMensaje("Artículo", "El Articulo se guardo con exito");
                        limpiarControles();
                    }
                    else
                    {
                        MostrarMensaje("Error", "El Articulo no se pudo crear");
                    }
                }
                else
                {
                    if (oRolPagI.Actualizar && oArticuloI.IdArticulo > 0 && validarSeleccionarBodegas())
                    {
                        if (oArticuloB.insertar(oArticuloI))
                        {
                            tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                            oBodB.EliminarBodegasArticulos(oArticuloI.IdArticulo);
                            foreach (DataGridItem item in dgBodegas.Items)
                            {
                                if (((CheckBox)(item.Cells[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkBodega"))).Checked == true)
                                {
                                    tblArticulo_BodegaItem oArtBodI = new tblArticulo_BodegaItem();
                                    oArtBodI.IdBodega = short.Parse(item.Cells[dgBodegasColumnsEnum.IdBodega.GetHashCode()].Text);
                                    oArtBodI.IdArticulo = oArticuloI.IdArticulo;
                                    oArtBodI.Cantidad = decimal.Parse(((TextBox)(item.Cells[dgBodegasColumnsEnum.Cantidad.GetHashCode()].FindControl("txtCantidad"))).Text, NumberStyles.Currency);
                                    oArtBodI.Precio = decimal.Parse(((TextBox)(item.Cells[dgBodegasColumnsEnum.Precio.GetHashCode()].FindControl("txtPrecio"))).Text, NumberStyles.Currency);
                                    oArtBodI.Costo = decimal.Parse(((TextBox)(item.Cells[dgBodegasColumnsEnum.Costo.GetHashCode()].FindControl("txtCosto"))).Text, NumberStyles.Currency);
                                    oArtBodI.IdTipoManejoPrecio = short.Parse(((DropDownList)(item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).SelectedValue);
                                    oBodB.GuardarArticuloPorBodega(oArtBodI);
                                }
                            }
                            MostrarMensaje("Artículo", "El Articulo se actualizo con exito");
                            limpiarControles();
                        }
                        else
                        {
                            MostrarMensaje("Error", "El Articulo no se pudo actualizar");
                        }
                    }
                    else
                    {
                        MostrarMensaje("Error", "El usuario no posee permisos para esta operación o no hay bodegas seleccionadas");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo guardar el articulo. {0}", ex.Message));
            }
        }

        private bool validarSeleccionarBodegas()
        {
            try
            {
                bool validador = false;
                foreach (DataGridItem item in dgBodegas.Items)
                {
                    if (((CheckBox)(item.Cells[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkBodega"))).Checked == true)
                    {
                        validador = true;
                    }
                }
                return validador;
            }
            catch
            {
                return false;
            }
        }

        private void CargarDatosArticulo(ref tblArticuloItem articulo)
        {
            try
            {
                if (lblIdArticulo.Text != "")
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    articulo = oArtB.ObtenerArticuloPorID(long.Parse(lblIdArticulo.Text), oUsuarioI.idEmpresa);
                }
                else
                {
                    articulo.IdEmpresa = oUsuarioI.idEmpresa;
                }
                articulo.IdTercero = long.Parse(hddIdTercero.Value);
                articulo.CodigoArticulo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Presentacion = txtPresentacion.Text;
                articulo.IdLinea = long.Parse(ddlLinea.SelectedValue);
                if (txtIVACompra.Text != "")
                {
                    articulo.IVACompra = decimal.Parse(txtIVACompra.Text, NumberStyles.Currency);
                }
                if (txtIVAVenta.Text != "")
                {
                    articulo.IVAVenta = decimal.Parse(txtIVAVenta.Text, NumberStyles.Currency);
                }
                articulo.CodigoBarra = txtCodigoBarra.Text;
                articulo.IdBodega = long.Parse(hddIdBodega.Value);
                articulo.EsInventario = chkInventario.Checked;
                if(txtStockMinimo.Text != "")
                {
                    articulo.StockMinimo = decimal.Parse(txtStockMinimo.Text, NumberStyles.Currency);
                }
                articulo.Activo = chkActivo.Checked;
                articulo.PrecioAutomatico = chkPrecioAuto.Checked;
                articulo.Ubicacion = txtUbicacion.Text;
                if (txtCostoPonderado.Text != "")
                {
                    articulo.CostoPonderado = decimal.Parse(txtCostoPonderado.Text, NumberStyles.Currency);
                }
                articulo.EsCompuesto = chkEsCompuesto.Checked;
                articulo.EsArticuloFinal = chkEsArticuloFinal.Checked;
                articulo.EsHijo = chkEsHijo.Checked;
                if (chkEsHijo.Checked)
                {
                    articulo.IdArticuloPadre = long.Parse(hddIdPadre.Value);
                    articulo.CantidadPadre = decimal.Parse(txtCantidadPadre.Text, NumberStyles.Currency);
                }
                //if (txtImpoconsumo.Text != "")
                //{
                //    articulo.Impoconsumo = decimal.Parse(txtImpoconsumo.Text, NumberStyles.Currency);
                //}
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo realizar la operación. {0}", ex.Message));
            }
        }

        private void limpiarControles()
        {
            lblIdArticulo.Text = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtPresentacion.Text = "";
            txtBodega.Text = "";
            txtIVACompra.Text = "";
            txtIVAVenta.Text = "";
            txtCodigoBarra.Text = "";
            txtTercero.Text = "";
            chkInventario.Checked = false;
            txtStockMinimo.Text = "";
            hddIdTercero.Value = "";
            hddIdBodega.Value = "";
            chkActivo.Checked = false;
            chkPrecioAuto.Checked = false;
            ConfiguracionIdioma();
            CargarBodegas();
            ddlLinea.SelectedValue = "0";
            ddlLineaBus.SelectedValue = "0";
            txtUbicacion.Text = "";
            txtCostoPonderado.Text = "";
            chkEsCompuesto.Checked = false;
            chkEsArticuloFinal.Checked = false;
            chkEsHijo.Checked = false;
            hddIdPadre.Value = "";
            txtPadre.Text = "";
            txtCantidadPadre.Text = "";
            //txtImpoconsumo.Text = "";
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void dgBodegas_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                ((DropDownList)(e.Item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).DataSource = oBodB.ObtenerTipoManejoPrecioArticulo();
                ((DropDownList)(e.Item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).DataBind();
                if (lblIdArticulo.Text != "")
                {
                    tblArticulo_BodegaItem oArtBodI = new tblArticulo_BodegaItem();
                    oArtBodI = oBodB.ConsultarArticulosBodegaPorID(long.Parse(lblIdArticulo.Text), long.Parse(e.Item.Cells[dgBodegasColumnsEnum.IdBodega.GetHashCode()].Text));
                    if (oArtBodI.IdArticulo > 0)
                    {
                        ((TextBox)(e.Item.Cells[dgBodegasColumnsEnum.Cantidad.GetHashCode()].FindControl("txtCantidad"))).Text = oArtBodI.Cantidad.ToString();
                        ((DropDownList)(e.Item.Cells[dgBodegasColumnsEnum.TipoManejoPrecio.GetHashCode()].FindControl("ddlTipoManejo"))).SelectedValue = oArtBodI.IdTipoManejoPrecio.ToString();
                        ((TextBox)(e.Item.Cells[dgBodegasColumnsEnum.Precio.GetHashCode()].FindControl("txtPrecio"))).Text = oArtBodI.Precio.ToString();
                        ((TextBox)(e.Item.Cells[dgBodegasColumnsEnum.Costo.GetHashCode()].FindControl("txtCosto"))).Text = oArtBodI.Costo.ToString();
                        ((CheckBox)(e.Item.Cells[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkBodega"))).Checked = true;
                    }
                    else
                    {
                        ((TextBox)(e.Item.Cells[dgBodegasColumnsEnum.Cantidad.GetHashCode()].FindControl("txtCantidad"))).Text = "0";
                    }
                }
                else
                {
                    ((TextBox)(e.Item.Cells[dgBodegasColumnsEnum.Cantidad.GetHashCode()].FindControl("txtCantidad"))).Text = "0";
                }
            }
        }

        private void CargarArticulos()
        {
            try
            {
                tblArticuloBusiness oArticuloB = new tblArticuloBusiness(CadenaConexion);
                dgArticulos.DataSource = oArticuloB.ObtenerArticuloListaPorFiltrosNombreLineaProveedorEmpresa(txtNombreBus.Text, short.Parse(ddlLineaBus.SelectedValue), long.Parse(hddIdProBus.Value), oUsuarioI.idEmpresa);
                dgArticulos.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void dgArticulos_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (oRolPagI.Actualizar)
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    tblArticuloItem oArtI = new tblArticuloItem();
                    oArtI = oArtB.ObtenerArticuloPorID(long.Parse(e.Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text), oUsuarioI.idEmpresa);
                    lblIdArticulo.Text = oArtI.IdArticulo.ToString();
                    txtNombre.Text = oArtI.Nombre;
                    txtPresentacion.Text = oArtI.Presentacion;
                    txtCodigo.Text = oArtI.CodigoArticulo;
                    chkInventario.Checked = oArtI.EsInventario;
                    ddlLinea.SelectedValue = oArtI.IdLinea.ToString();
                    txtCodigoBarra.Text = oArtI.CodigoBarra;
                    txtTercero.Text = oArtI.Tercero;
                    hddIdTercero.Value = oArtI.IdTercero.ToString();
                    txtIVACompra.Text = oArtI.IVACompra.ToString();
                    txtIVAVenta.Text = oArtI.IVAVenta.ToString();
                    txtBodega.Text = oArtI.Bodega ;
                    hddIdBodega.Value = oArtI.IdBodega.ToString();
                    txtStockMinimo.Text = oArtI.StockMinimo.ToString();
                    chkActivo.Checked = oArtI.Activo;
                    chkPrecioAuto.Checked = oArtI.PrecioAutomatico;
                    txtUbicacion.Text = oArtI.Ubicacion;
                    txtCostoPonderado.Text = oArtI.CostoPonderado.ToString();
                    chkEsCompuesto.Checked = oArtI.EsCompuesto;
                    chkEsArticuloFinal.Checked = oArtI.EsArticuloFinal;
                    chkEsHijo.Checked = oArtI.EsHijo;
                    if (oArtI.EsHijo)
                    {
                        txtPadre.Enabled = true;
                        txtCantidadPadre.Enabled = true;
                    }
                    txtPadre.Text = oArtI.NombrePadre;
                    hddIdPadre.Value = oArtI.IdArticuloPadre.ToString();
                    txtCantidadPadre.Text = oArtI.CantidadPadre.ToString();
                    //txtImpoconsumo.Text = oArtI.Impoconsumo.ToString();
                    CargarBodegas();
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                    }
                }
                else
                {
                    MostrarMensaje("Permisos", "El usuario no posee permisos para esta operación.");
                }
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo editar el artículo. {0}", ex.Message));
            }
        }

        protected void dgArticulos_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                List<tblArticulo_BodegaItem> oArtBodI = new List<tblArticulo_BodegaItem>();
                tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                oArtBodI = oBodB.ConsultarArticulosPorBodega(long.Parse(e.Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text));
                decimal cantidadTotal = 0;
                foreach (tblArticulo_BodegaItem Item in oArtBodI)
                {
                    cantidadTotal = cantidadTotal + Item.Cantidad;
                }
                if(cantidadTotal == 0 && e.Item.Cells[dgArticulosColumnsEnum.EsInventario.GetHashCode()].Text == "True")
                {
                    e.Item.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (cantidadTotal <= decimal.Parse(e.Item.Cells[dgArticulosColumnsEnum.StockMinimo.GetHashCode()].Text.Replace(".", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator)) && e.Item.Cells[dgArticulosColumnsEnum.EsInventario.GetHashCode()].Text == "True")
                    {
                        e.Item.ForeColor = System.Drawing.Color.Green;
                    }
                }
                e.Item.Cells[dgArticulosColumnsEnum.Numero.GetHashCode()].Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CargarArticulos();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo realizar la busqueda de los articulos. {0}", ex.Message));
            }
        }

        protected void dgBodegas_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("frmPreciosPorBodega.aspx?IdBodega={0}&IdArticulo={1}", e.Item.Cells[dgBodegasColumnsEnum.IdBodega.GetHashCode()].Text, lblIdArticulo.Text));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo abrir la configuración de precios. {0}", ex.Message));
            }
        }

        protected void dgArticulos_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CodigoBarra")
                {
                    Response.Redirect(string.Format("frmCodigoBarra.aspx?IdArticulo={0}", e.Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text));
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo abrir la configuración de código de barras. {0}", ex.Message));
            }
        }

        protected void ValidarCantidadBodega(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblIdArticulo.Text))
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                    List<tblArticulo_BodegaItem> oListArtBod = new List<tblArticulo_BodegaItem>();
                    oListArtBod = oBodB.ConsultarArticulosPorBodega(long.Parse(lblIdArticulo.Text));
                    foreach (tblArticulo_BodegaItem Item in oListArtBod)
                    {
                        bool BodSel = false;
                        foreach (DataGridItem Row in dgBodegas.Items)
                        {
                            if (((CheckBox)(Row.Cells[dgBodegasColumnsEnum.Seleccionar.GetHashCode()].FindControl("chkBodega"))).Checked == true)
                            {
                                if (Item.IdBodega.ToString() == Row.Cells[dgBodegasColumnsEnum.IdBodega.GetHashCode()].Text)
                                {
                                    BodSel = true;
                                }
                            }
                        }
                        if (!BodSel)
                        {
                            if (Item.Cantidad > 0)
                            {
                                MostrarMensaje("Error", "No puede deseleccionar esta bodega, porque tiene unidades cargadas en ella.");
                                CargarBodegas();
                                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                                {
                                    string strScript = "$(document).ready(function(){";
                                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                                    strScript = string.Format("{0}}});", strScript);
                                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                                }
                            }
                            else
                            {
                                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                                {
                                    string strScript = "$(document).ready(function(){";
                                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                                    strScript = string.Format("{0}}});", strScript);
                                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                                }
                            }
                        }
                        else
                        {
                            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                            {
                                string strScript = "$(document).ready(function(){";
                                strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                                strScript = string.Format("{0}}});", strScript);
                                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                            }
                        }
                    }
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                    }
                }
                else
                {
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo seleccionar la bodega. {0}", ex.Message));
            }
        }

        protected void chkEsCompuesto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEsCompuesto.Checked)
                {
                    chkInventario.Checked = false;
                }
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                {
                    string strScript = "$(document).ready(function(){";
                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                    strScript = string.Format("{0}}});", strScript);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                }
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                {
                    string strScript = "$(document).ready(function(){";
                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                    strScript = string.Format("{0}}});", strScript);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                }
            }
        }

        protected void chkEsArticuloFinal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEsArticuloFinal.Checked)
                {
                    chkInventario.Checked = true;
                }
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                {
                    string strScript = "$(document).ready(function(){";
                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                    strScript = string.Format("{0}}});", strScript);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                {
                    string strScript = "$(document).ready(function(){";
                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                    strScript = string.Format("{0}}});", strScript);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                }
            }
        }

        protected void chkEsHijo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEsHijo.Checked)
                {
                    txtPadre.Enabled = true;
                    txtCantidadPadre.Enabled = true;
                    chkInventario.Checked = false;
                }
                else
                {
                    txtPadre.Enabled = false;
                    txtCantidadPadre.Enabled = false;
                }
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                {
                    string strScript = "$(document).ready(function(){";
                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                    strScript = string.Format("{0}}});", strScript);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                {
                    string strScript = "$(document).ready(function(){";
                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                    strScript = string.Format("{0}}});", strScript);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                }
            }
        }

        protected void chkInventario_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkInventario.Checked)
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                    List<tblArticulo_BodegaItem> oListArtBod = new List<tblArticulo_BodegaItem>();
                    oListArtBod = oBodB.ConsultarArticulosPorBodega(long.Parse(lblIdArticulo.Text));
                    bool Validador = false;
                    foreach(tblArticulo_BodegaItem Item in oListArtBod)
                    {
                        if(Item.Cantidad > 0)
                        {
                            Validador = true;
                        }
                    }
                    if (Validador)
                    {
                        MostrarMensaje("Error", "No se puede cambiar el atributo al artículo mientras tenga existencias en una bodega.");
                        chkInventario.Checked = true;
                    }
                }
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesBusqueda"))
                {
                    string strScript = "$(document).ready(function(){";
                    strScript = string.Format("{0} seleccionarTab('contenido','0');", strScript);
                    strScript = string.Format("{0}}});", strScript);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesBusqueda", strScript, true);
                }
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", ex.Message.Replace("'",""));
            }
        }
    }
}