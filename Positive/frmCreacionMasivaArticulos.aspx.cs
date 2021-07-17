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
    public partial class frmCreacionMasivaArticulos : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgArticulosColumnsEnum
        {
            CodigoArticulo = 0,
            Nombre = 1,
            Presentacion = 2,
            IdLinea = 3,
            Linea = 4,
            IVAVenta = 5,
            CodigoBarra = 6,
            IdTercero = 7,
            Tercero = 8,
            IdBodega = 9,
            Bodega = 10,
            EsInventario = 11,
            StockMinimo = 12,
            IVACompra = 13,
            Ubicacion = 14,
            EsCompuesto = 15,
            EsArticuloFinal = 16,
            EsHijo = 17,
            IdPadre = 18,
            CantidadPadre = 19,
            Costo = 20,
            Precio = 21,
            Errores = 22
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CreacionMasivaArticulos.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarDelimitadores();
                            OcultarControl(btnGuardar.ClientID);
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
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina de creación masiva de articulos. {0}", ex.Message));
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
            //ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        //private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        //{
        //    try
        //    {
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Presentacion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Presentacion);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Linea.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Linea);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.IVAVenta.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVAVenta);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.CodigoBarra.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.CodigoBarra);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Tercero.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Bodega.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.EsInventario.GetHashCode()].HeaderText = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Es), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Inventario));
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.StockMinimo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.StockMinimo);
        //        //dgArticulosMasivo.Columns[dgArticulosColumnsEnum.PrecioAutomatico.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.PrecioAutomatico);
        //        dgArticulosMasivo.Columns[dgArticulosColumnsEnum.IVACompra.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.IVACompra);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void btnCargar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (fulArticulos.HasFile)
                {
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControles"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} seleccionarTab('contenido','2');", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControles", strScript, true);
                    }
                    btnGuardar.Visible = true;
                    List<tblArticuloItem> articulos = new List<tblArticuloItem>();
                    tblArticuloBusiness oABiz = new tblArticuloBusiness(CadenaConexion);
                    string error = oABiz.LeerArticulosArchivo(fulArticulos.PostedFile.InputStream, articulos, oUsuarioI.idEmpresa, char.Parse(ddlDelimitador.SelectedValue));
                    if (error != "")
                    {
                        MostrarMensaje("Error", error);
                    }
                    else
                    {
                        dgArticulosMasivo.DataSource = articulos;
                        dgArticulosMasivo.DataBind();
                    }
                }
                else
                {
                    Response.Write("<script>alert('No hay un archivo seleccionado');</script>");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar el archivo. {0}", ex.Message));
            }
        }

        private void CargarDelimitadores()
        {
            try
            {
                ddlDelimitador.Items.Add(new ListItem("Coma (,)", ","));
                ddlDelimitador.Items.Add(new ListItem("Punto y coma (;)", ";"));
                ddlDelimitador.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los delimitadores. {0}", ex.Message));
            }
        }

        private void LimpiarControles()
        {
            dgArticulosMasivo.DataSource = null;
            dgArticulosMasivo.DataBind();
            CargarDelimitadores();
        }

        protected void dgArticulosMasivo_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    string Error = "";
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);

                    //Verifica que el codigo del articulo y la descripción sean obligatorios
                    if (e.Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text == "" && e.Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text == "&nbsp;" && e.Item.Cells[dgArticulosColumnsEnum.Nombre.GetHashCode()].Text == "" && e.Item.Cells[dgArticulosColumnsEnum.Nombre.GetHashCode()].Text == "&nbsp;" && e.Item.Cells[dgArticulosColumnsEnum.Presentacion.GetHashCode()].Text == "" && e.Item.Cells[dgArticulosColumnsEnum.Presentacion.GetHashCode()].Text == "&nbsp;")
                    {
                        Error = string.Format("{0} * Error. El codigo del articulo, el nombre y la presentación son campos obligatorios.<br> ", Error);
                    }

                    //Validar que el codigo del artículo no exista
                    if (oArtB.ObtenerArticuloPorCodigo(e.Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text, oUsuarioI.idEmpresa).IdArticulo > 0)
                    {
                        Error = string.Format("{0} * Error. Ya existe en el sistema un artículo con el mismo código.<br> ", Error);
                    }

                    //Realiza la busqueda de la linea por su ID
                    if (e.Item.Cells[dgArticulosColumnsEnum.IdLinea.GetHashCode()].Text == "" && e.Item.Cells[dgArticulosColumnsEnum.IdLinea.GetHashCode()].Text == "&nbsp;")
                    {
                        Error = string.Format("{0} * Error. Se debe asignar una linea al articulo.<br> ", Error);
                    }
                    else
                    {
                        tblLineaBussines oLinB = new tblLineaBussines(CadenaConexion);
                        tblLineaItem oLinI = new tblLineaItem();
                        oLinI = oLinB.ObtenerLinea(long.Parse(e.Item.Cells[dgArticulosColumnsEnum.IdLinea.GetHashCode()].Text), oUsuarioI.idEmpresa);
                        if (oLinI.IdLinea == 0)
                        {
                            Error = string.Format("{0} * Error. No se registra en el sistema una linea con ese ID.<br> ", Error);
                        }
                        else
                        {
                            e.Item.Cells[dgArticulosColumnsEnum.IdLinea.GetHashCode()].Text = oLinI.IdLinea.ToString();
                            e.Item.Cells[dgArticulosColumnsEnum.Linea.GetHashCode()].Text = oLinI.Nombre;
                        }
                    }

                    //Realiza la busqueda de la bodega por su ID
                    if (e.Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text == "" && e.Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text == "&nbsp;")
                    {
                        Error = string.Format("{0} * Error. Se debe asignar una bodega al articulo.<br> ", Error);
                    }
                    else
                    {
                        tblBodegaBusiness oBodB = new tblBodegaBusiness(CadenaConexion);
                        tblBodegaItem oBodI = new tblBodegaItem();
                        oBodI = oBodB.ObtenerBodega(long.Parse(e.Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text), oUsuarioI.idEmpresa);
                        if (oBodI.IdBodega == 0)
                        {
                            Error = string.Format("{0} * Error. No se registra en el sistema una bodega con ese ID.<br> ", Error);
                        }
                        else
                        {
                            e.Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text = oBodI.IdBodega.ToString();
                            e.Item.Cells[dgArticulosColumnsEnum.Bodega.GetHashCode()].Text = oBodI.Descripcion;
                        }
                    }

                    //Realiza la busqueda del tercero por su ID
                    if (e.Item.Cells[dgArticulosColumnsEnum.IdTercero.GetHashCode()].Text == "" && e.Item.Cells[dgArticulosColumnsEnum.IdTercero.GetHashCode()].Text == "&nbsp;")
                    {
                        Error = string.Format("{0} * Error. Se debe asignar un proveedor al articulo.<br> ", Error);
                    }
                    else
                    {
                        tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                        tblTerceroItem oTerI = new tblTerceroItem();
                        oTerI = oTerB.ObtenerTercero(long.Parse(e.Item.Cells[dgArticulosColumnsEnum.IdTercero.GetHashCode()].Text), oUsuarioI.idEmpresa);
                        if (oTerI.IdTercero == 0)
                        {
                            Error = string.Format("{0} * Error. No se registra en el sistema un proveedor con ese ID.<br> ", Error);
                        }
                        else
                        {
                            e.Item.Cells[dgArticulosColumnsEnum.IdTercero.GetHashCode()].Text = oTerI.IdTercero.ToString();
                            e.Item.Cells[dgArticulosColumnsEnum.Tercero.GetHashCode()].Text = oTerI.Nombre;
                        }
                    }

                    e.Item.Cells[dgArticulosColumnsEnum.Costo.GetHashCode()].Text = decimal.Parse(e.Item.Cells[dgArticulosColumnsEnum.Costo.GetHashCode()].Text).ToString(Util.ObtenerFormatoDecimal());
                    e.Item.Cells[dgArticulosColumnsEnum.Precio.GetHashCode()].Text = decimal.Parse(e.Item.Cells[dgArticulosColumnsEnum.Precio.GetHashCode()].Text).ToString(Util.ObtenerFormatoDecimal());

                    //Verifica si hay un error en la linea
                    if (Error != "")
                    {
                        e.Item.Cells[dgArticulosColumnsEnum.Errores.GetHashCode()].Text = Error;
                        e.Item.BackColor = System.Drawing.Color.Yellow;
                        OcultarControl(btnGuardar.ClientID);
                    }
                    else
                    {
                        MostrarControl(btnGuardar.ClientID);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo cargar los articulos. {0}", ex.Message));
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (oRolPagI.Insertar)
                {
                    string Error = "";
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    List<tblArticuloItem> oListArtI = new List<tblArticuloItem>();
                    foreach (DataGridItem Item in dgArticulosMasivo.Items)
                    {
                        tblArticuloItem oArtI = new tblArticuloItem();
                        oArtI.CodigoArticulo = Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text;
                        oArtI.Nombre = Item.Cells[dgArticulosColumnsEnum.Nombre.GetHashCode()].Text;
                        oArtI.Presentacion = Item.Cells[dgArticulosColumnsEnum.Presentacion.GetHashCode()].Text;
                        oArtI.IdLinea = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdLinea.GetHashCode()].Text);
                        oArtI.IVAVenta = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.IVAVenta.GetHashCode()].Text, NumberStyles.Currency);
                        oArtI.CodigoBarra = Item.Cells[dgArticulosColumnsEnum.CodigoBarra.GetHashCode()].Text;
                        oArtI.IdTercero = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdTercero.GetHashCode()].Text);
                        oArtI.IdEmpresa = oUsuarioI.idEmpresa;
                        oArtI.IdBodega = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text);
                        oArtI.EsInventario = Item.Cells[dgArticulosColumnsEnum.EsInventario.GetHashCode()].Text == "True" ? true : false;
                        oArtI.StockMinimo = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.StockMinimo.GetHashCode()].Text, NumberStyles.Currency);
                        oArtI.Activo = true;
                        oArtI.IVACompra = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.IVACompra.GetHashCode()].Text, NumberStyles.Currency);
                        oArtI.Ubicacion = Item.Cells[dgArticulosColumnsEnum.Ubicacion.GetHashCode()].Text == "&nbsp;" ? string.Empty : Item.Cells[dgArticulosColumnsEnum.Ubicacion.GetHashCode()].Text;
                        oArtI.EsCompuesto = Item.Cells[dgArticulosColumnsEnum.EsCompuesto.GetHashCode()].Text == "True" ? true : false;
                        oArtI.EsArticuloFinal = Item.Cells[dgArticulosColumnsEnum.EsArticuloFinal.GetHashCode()].Text == "True" ? true : false;
                        oArtI.EsHijo = Item.Cells[dgArticulosColumnsEnum.EsHijo.GetHashCode()].Text == "True" ? true : false;
                        oArtI.IdArticuloPadre = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdPadre.GetHashCode()].Text);
                        oArtI.CantidadPadre = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.CantidadPadre.GetHashCode()].Text, NumberStyles.Currency);
                        oArtI.Costo = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Costo.GetHashCode()].Text, NumberStyles.Currency);
                        oArtI.Precio = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Precio.GetHashCode()].Text, NumberStyles.Currency);
                        oListArtI.Add(oArtI);
                    }
                    Error = oArtB.CreacionMasivaArticulos(oListArtI);
                    if (string.IsNullOrEmpty(Error))
                    {
                        MostrarMensaje("Creación de Artículos", "Los articulos se crearon con exito");
                        LimpiarControles();
                    }
                    else
                    {
                        MostrarMensaje("Error", Error);
                    }
                }
                else
                {
                    MostrarMensaje("Error", "El usuario no posee permisos para esta operación.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo guardar los artículos. {0}", ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
    }
}