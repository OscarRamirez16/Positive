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
    public partial class frmEntradaMasivaMercancia : PaginaBase
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
            IVA = 7,
            CostoPonderado = 8
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.EntradaMasivaMercancia.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            OcultarControl(divBotones.ClientID);
                            OcultarControl(divGrilla.ClientID);
                            OcultarControl(divObservaciones.ClientID);
                            CargarDelimitadores();
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
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgArticulosMasivo.Columns[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Codigo);
                dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Descripcion);
                dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Bodega.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
                dgArticulosMasivo.Columns[dgArticulosColumnsEnum.Precio.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Precio);
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void btnCancelarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        private string ValidarInventarioSuficiente()
        {
            string Errores = "";
            try
            {
                if (rbSalida.Checked)
                {
                    tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                    foreach (DataGridItem Item in dgArticulosMasivo.Items)
                    {
                        long IdArticulo = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text);
                        long IdBodega = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text);
                        decimal Cantidad = 0;
                        if (oArtB.ObtenerArticuloPorID(IdArticulo, oUsuarioI.idEmpresa).EsInventario)
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
                                    Errores = string.Format("El artículo {0} no tiene suficientes existencias. Disponibilidad {1}", Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoEntero()).Replace("$", ""));
                                }
                                else
                                {
                                    Errores = string.Format("{0}. El artículo {1} no tiene suficientes existencias. Disponibilidad {2}", Errores, Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text, Disponibles.ToString(Util.ObtenerFormatoEntero()).Replace("$", ""));
                                }
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

        protected void btnGuardarMasivo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (oRolPagI.Insertar)
                {
                    if (rbEntrada.Checked == false && rbSalida.Checked == false)
                    {
                        MostrarAlerta(0, "Error", "Por favor seleccione la operación que desea realizar");
                    }
                    else
                    {
                        string Errores = ValidarInventarioSuficiente();
                        if (string.IsNullOrEmpty(Errores))
                        {
                            tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                            tblTerceroItem oTerI = new tblTerceroItem();
                            oTerI = oTerB.ObtenerClienteProveedorGenerico(oUsuarioI.idEmpresa, "P");
                            tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                            tblDocumentoItem oDocI = new tblDocumentoItem();
                            List<tblDetalleDocumentoItem> oListDet = new List<tblDetalleDocumentoItem>();
                            oDocI.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                            oDocI.idTercero = oTerI.IdTercero;
                            oDocI.Telefono = oTerI.Telefono;
                            oDocI.Direccion = oTerI.Direccion;
                            oDocI.idCiudad = oTerI.idCiudad;
                            oDocI.NombreTercero = oTerI.Nombre;
                            oDocI.Observaciones = txtObservaciones.Text.Replace(Environment.NewLine, " ");
                            oDocI.idEmpresa = oUsuarioI.idEmpresa;
                            oDocI.idUsuario = oUsuarioI.idUsuario;
                            //oDocI.TotalDocumento = decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency);
                            //oDocI.TotalIVA = decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency);
                            //oDocI.saldo = oDocItem.TotalDocumento;
                            oDocI.EnCuadre = false;
                            oDocI.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                            oDocI.FechaVencimiento = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                            //oDocI.TotalDescuento = decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency);
                            //oDocI.TotalAntesIVA = decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency);
                            if (rbEntrada.Checked)
                            {
                                oDocI.IdTipoDocumento = 5;
                            }
                            if (rbSalida.Checked)
                            {
                                oDocI.IdTipoDocumento = 6;
                            }
                            short NumeroLinea = 1;
                            foreach (DataGridItem Item in dgArticulosMasivo.Items)
                            {
                                tblDetalleDocumentoItem oDetI = new tblDetalleDocumentoItem();
                                oDetI.NumeroLinea = NumeroLinea;
                                oDetI.idArticulo = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdArticulo.GetHashCode()].Text);
                                oDetI.Codigo = Item.Cells[dgArticulosColumnsEnum.CodigoArticulo.GetHashCode()].Text;
                                oDetI.Articulo = Item.Cells[dgArticulosColumnsEnum.Nombre.GetHashCode()].Text;
                                oDetI.ValorUnitario = (decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Precio.GetHashCode()].Text, NumberStyles.Currency) / ( 1 + (decimal.Parse(Item.Cells[dgArticulosColumnsEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100)));
                                oDetI.IVA = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.IVA.GetHashCode()].Text, NumberStyles.Currency);
                                oDetI.Cantidad = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                                oDetI.Descuento = 0;
                                oDetI.idBodega = long.Parse(Item.Cells[dgArticulosColumnsEnum.IdBodega.GetHashCode()].Text);
                                oDetI.CostoPonderado = decimal.Parse(Item.Cells[dgArticulosColumnsEnum.CostoPonderado.GetHashCode()].Text, NumberStyles.Currency);
                                oListDet.Add(oDetI);
                                NumeroLinea++;
                            }
                            if(oListDet.Count > 0)
                            {
                                string Error = oDocB.GuardarSoloDocumento(oDocI, oListDet);
                                if (Error == "Exito")
                                {
                                    MostrarAlerta(1, "Exito", "El documento se creó con exito.");
                                    LimpiarControles();
                                }
                                else
                                {
                                    MostrarAlerta(0, "Error", Error);
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
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }

        private void LimpiarControles()
        {
            try
            {
                CargarDelimitadores();
                OcultarControl(divBotones.ClientID);
                OcultarControl(divGrilla.ClientID);
                OcultarControl(divObservaciones.ClientID);
                dgArticulosMasivo.DataSource = null;
                dgArticulosMasivo.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
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
                    string error = oABiz.LeerArticulosParaEntradaSalidaMasiva(fulArticulos.PostedFile.InputStream, Articulos, char.Parse(ddlDelimitador.SelectedValue), oUsuarioI.idEmpresa);
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
    }
}