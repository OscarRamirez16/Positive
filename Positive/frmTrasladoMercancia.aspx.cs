using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;
using System.Data;
using System.Globalization;

namespace Inventario
{
    public partial class frmTrasladoMercancia : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgTrasladosEnum
        {
            IdArticulo = 0,
            Articulo = 1,
            Cantidad = 2,
            IdBodegaOrigen = 3,
            BodegaOrigen = 4,
            IdBodegaDestino = 5,
            BodegaDestino = 6,
            Eliminar = 7
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.TrasladarMercancia.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteArticuloSencillo('{1}','Ashx/Articulo.ashx','{2}', '{3}','3');", strScript, txtArticulo.ClientID, hddIdArticulo.ClientID, oUsuarioI.idEmpresa);
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','2','{4}','{5}');", strScript, txtBodegaOrigen.ClientID, hddIdBodegaOrigen.ClientID, oUsuarioI.idEmpresa, hddIdArticulo.ClientID, hddCantidad.ClientID);
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','1');", strScript, txtBodegaDestino.ClientID, hddIdBodegaDestino.ClientID, oUsuarioI.idEmpresa);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                        if (!IsPostBack)
                        {
                            txtCantidad.Text = "0.00";
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
            lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.TrasladoMercancia);
            txtArticulo.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Articulo));
            txtCantidad.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cantidad));
            txtBodegaOrigen.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.BodegaOrigen));
            txtBodegaDestino.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.BodegaDestino));
            lblObservaciones.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Observaciones);
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (dgTraslados.Items.Count > 0)
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
                        foreach (DataGridItem Item in dgTraslados.Items)
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
                            dgTraslados.DataSource = null;
                            dgTraslados.DataBind();
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
                tblArticuloItem oArtI = new tblArticuloItem();
                tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                foreach (DataGridItem Item in dgTraslados.Items)
                {
                    decimal Disponibles = oArtB.DisponibilidadArticuloEnBodega(long.Parse(Item.Cells[dgTrasladosEnum.IdArticulo.GetHashCode()].Text), long.Parse(Item.Cells[dgTrasladosEnum.IdBodegaOrigen.GetHashCode()].Text));
                    if (decimal.Parse(Item.Cells[dgTrasladosEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency) > Disponibles)
                    {
                        if (string.IsNullOrEmpty(Errores))
                        {
                            Errores = string.Format("El artículo {0} no tiene suficientes existencias. Disponibilidad {1}", oArtI.CodigoArticulo, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
                        }
                        else
                        {
                            Errores = string.Format("{0}. El artículo {1} no tiene suficientes existencias . Disponibilidad {2}", Errores, oArtI.CodigoArticulo, Disponibles.ToString(Util.ObtenerFormatoDecimal()).Replace("$", ""));
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
        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string Errores = ValidacionesTraslado();
                if (string.IsNullOrEmpty(Errores))
                {
                    DataTable dt = new DataTable();
                    if (dgTraslados.Items.Count > 0)
                    {
                        CargarColumnasTraslados(ref dt);
                        foreach (DataGridItem Item in dgTraslados.Items)
                        {
                            DataRow copia;
                            copia = dt.NewRow();
                            copia["IdArticulo"] = Item.Cells[dgTrasladosEnum.IdArticulo.GetHashCode()].Text;
                            copia["Articulo"] = Item.Cells[dgTrasladosEnum.Articulo.GetHashCode()].Text;
                            copia["Cantidad"] = Item.Cells[dgTrasladosEnum.Cantidad.GetHashCode()].Text;
                            copia["IdBodegaOrigen"] = Item.Cells[dgTrasladosEnum.IdBodegaOrigen.GetHashCode()].Text;
                            copia["BodegaOrigen"] = Item.Cells[dgTrasladosEnum.BodegaOrigen.GetHashCode()].Text;
                            copia["IdBodegaDestino"] = Item.Cells[dgTrasladosEnum.IdBodegaDestino.GetHashCode()].Text;
                            copia["BodegaDestino"] = Item.Cells[dgTrasladosEnum.BodegaDestino.GetHashCode()].Text;
                            dt.Rows.Add(copia);
                        }
                    }
                    else
                    {
                        CargarColumnasTraslados(ref dt);
                    }
                    DataRow row;
                    row = dt.NewRow();
                    row["IdArticulo"] = hddIdArticulo.Value;
                    row["Articulo"] = txtArticulo.Text;
                    row["Cantidad"] = txtCantidad.Text;
                    row["IdBodegaOrigen"] = hddIdBodegaOrigen.Value;
                    row["BodegaOrigen"] = txtBodegaOrigen.Text;
                    row["IdBodegaDestino"] = hddIdBodegaDestino.Value;
                    row["BodegaDestino"] = txtBodegaDestino.Text;
                    dt.Rows.Add(row);
                    dgTraslados.DataSource = dt;
                    dgTraslados.DataBind();
                    LimpiarControles();
                }
                else
                {
                    MostrarAlerta(0, "Error", Errores);
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        private void LimpiarControles()
        {
            hddIdArticulo.Value = "0";
            txtArticulo.Text = "";
            txtCantidad.Text = "0.00";
            hddCantidad.Value = "0";
            hddIdBodegaDestino.Value = "0";
            txtBodegaDestino.Text = "";
            hddIdBodegaOrigen.Value = "0";
            txtBodegaOrigen.Text = "";
        }
        private string ValidacionesTraslado()
        {
            string Errores = "";
            if (string.IsNullOrEmpty(hddIdArticulo.Value) || hddIdArticulo.Value == "0")
            {
                Errores = string.Format("{0} * Por favor seleccione un artículo. <br>", Errores);
            }
            if (string.IsNullOrEmpty(hddIdBodegaOrigen.Value) || hddIdBodegaOrigen.Value == "0")
            {
                Errores = string.Format("{0} * Por favor seleccione la bodega de origen. <br>", Errores);
            }
            if (string.IsNullOrEmpty(hddIdBodegaDestino.Value) || hddIdBodegaDestino.Value == "0")
            {
                Errores = string.Format("{0} * Por favor seleccione la bodega de destino. <br>", Errores);
            }
            if (string.IsNullOrEmpty(txtCantidad.Text) || txtCantidad.Text == "0.00")
            {
                Errores = string.Format("{0} * Por favor ingrese una cantidad valida. <br>", Errores);
            }
            if (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) > decimal.Parse(hddCantidad.Value, NumberStyles.Currency))
            {
                Errores = string.Format("{0} * La cantidad a trasladar no puede ser mayor a la disponible en la bodega. <br>", Errores);
            }
            return Errores;
        }
        private void CargarColumnasTraslados(ref DataTable dt)
        {
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdArticulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Articulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cantidad";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdBodegaOrigen";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "BodegaOrigen";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdBodegaDestino";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "BodegaDestino";
            dt.Columns.Add(column);
        }
        protected void dgTraslados_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                CargarColumnasTraslados(ref dt);
                foreach (DataGridItem Item in dgTraslados.Items)
                {
                    if (Item.ItemIndex != e.Item.ItemIndex)
                    {
                        DataRow row;
                        row = dt.NewRow();
                        row["IdArticulo"] = Item.Cells[dgTrasladosEnum.IdArticulo.GetHashCode()].Text;
                        row["Articulo"] = Item.Cells[dgTrasladosEnum.Articulo.GetHashCode()].Text;
                        row["Cantidad"] = Item.Cells[dgTrasladosEnum.Cantidad.GetHashCode()].Text;
                        row["IdBodegaOrigen"] = Item.Cells[dgTrasladosEnum.IdBodegaOrigen.GetHashCode()].Text;
                        row["BodegaOrigen"] = Item.Cells[dgTrasladosEnum.BodegaOrigen.GetHashCode()].Text;
                        row["IdBodegaDestino"] = Item.Cells[dgTrasladosEnum.IdBodegaDestino.GetHashCode()].Text;
                        row["BodegaDestino"] = Item.Cells[dgTrasladosEnum.BodegaDestino.GetHashCode()].Text;
                        dt.Rows.Add(row);
                    }
                }
                dgTraslados.DataSource = dt;
                dgTraslados.DataBind();
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hddIdArticulo.Value))
                {
                    if(oUsuarioI.idEmpresa == 142)
                    {
                        long BodegaPrincipal = 217;
                        tblArticuloBusiness oArtB = new tblArticuloBusiness(CadenaConexion);
                        decimal Disponibles = oArtB.DisponibilidadArticuloEnBodega(long.Parse(hddIdArticulo.Value), BodegaPrincipal);
                        hddIdBodegaOrigen.Value = BodegaPrincipal.ToString();
                        txtBodegaOrigen.Text = string.Format("BODEGA PRINCIPAL({0} UND)", Disponibles.ToString());
                        hddCantidad.Value = Disponibles.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
    }
}