using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Inventario
{
    public partial class frmFacturacionMasiva : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgFacturaEnum
        {
            IdArticulo = 0,
            Codigo = 1,
            Articulo = 2,
            IdBodega = 3,
            Bodega = 4,
            Cantidad = 5,
            ValorUnitario = 6,
            Descuento = 7,
            ValorUnitarioConDescuento = 8,
            IVA = 9,
            ValorUnitarioConIVA = 10,
            TotalLinea = 11,
            Eliminar = 12,
            CostoPonderado = 13,
            ValorDescuento = 14,
            PrecioCosto = 15
        }

        private enum dgClientesEnum
        {
            IdTercero = 0,
            Identificacion = 1,
            Nombre = 2,
            Direccion = 3,
            Telefono = 4,
            Celular = 5,
            IdCiudad = 6,
            Seleccionar = 7
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    txtAntesIVA.Attributes.Add("readonly", "readonly");
                    txtTotalIVA.Attributes.Add("readonly", "readonly");
                    txtTotalFactura.Attributes.Add("readonly", "readonly");
                    txtTotalDescuento.Attributes.Add("readonly", "readonly");
                    OcultarControl(btnAdicionar.ClientID);
                    OcultarControl(divValidador.ClientID);
                    OcultarControl(divDescuento.ClientID);
                    hddIdUsuario.Value = oUsuarioI.idUsuario.ToString();
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    hddBodegaPorDefectoUsuario.Value = oUsuarioI.idBodega.ToString();
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Ventas.GetHashCode().ToString()));
                    if (oRolPagI.Insertar)
                    {
                        if (!IsPostBack)
                        {
                            txtDescuento.Text = "0";
                            txtPrecio.Text = "0";
                            txtTotalFactura.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            txtAntesIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalDescuento.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            CargarGrupoCliente();
                            CargarClientes();
                        }
                        txtCodigo.Attributes.Add("onblur", string.Format("traerArticuloPorCodigoOCodigoBarra('{0}','Ashx/Articulo.ashx','{1}','{2}','{3}','{4}',2,'{5}','{6}','{7}','{8}','{9}','{10}')", txtCodigo.ClientID, hddIdArticulo.ClientID, txtDescripcion.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID));
                        txtPrecio.Attributes.Add("onblur", "AdicionarArticulo()");
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} ConfigurarEnter();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteArticulo('{1}','Ashx/Articulo.ashx','{2}','{3}','{4}','{5}',1,'{6}','{7}','{8}','{9}','{10}','{11}');", strScript, txtDescripcion.ClientID, hddIdArticulo.ClientID, txtCodigo.ClientID, txtPrecio.ClientID, hddIVA.ClientID, hddCantidad.ClientID, hddIdBodega.ClientID, txtBodega.ClientID, hddEsInventario.ClientID, hddCostoPonderado.ClientID, hddPrecioCosto.ClientID);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                    else
                    {
                        Response.Redirect("frmMantenimientos.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        private void CargarGrupoCliente()
        {
            if (!IsPostBack)
            {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                ddlGrupoCliente.DataTextField = "Nombre";
                ddlGrupoCliente.DataValueField = "idGrupoCliente";
                ddlGrupoCliente.DataSource = oTBiz.ObtenerGrupoClienteLista(oUsuarioI.idEmpresa, "");
                ddlGrupoCliente.DataBind();
                ddlGrupoCliente.Items.Insert(0, new ListItem("Todos", "0"));
                ddlGrupoCliente.SelectedIndex = 0;
            }
        }

        private void CargarClientes()
        {
            try
            {
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                dgClientes.DataSource = oTerB.ObtenerClientesListaActivos(oUsuarioI.idEmpresa, int.Parse(ddlGrupoCliente.SelectedValue));
                dgClientes.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                if (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) > 0)
                {
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Descuentos.GetHashCode().ToString()));
                    if (oRolPagI.Insertar)
                    {
                        if (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) > oUsuarioI.PorcentajeDescuento)
                        {
                            MostrarMensaje("Error", "El descuento otorgado sobre pasa el valor permitido por el usuario.");
                        }
                        else
                        {
                            OcultarControl(divDescuento.ClientID);
                            decimal Descuento = (decimal.Parse(txtDescuento.Text, NumberStyles.Currency) / 100);
                            if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                            {
                                if (oUsuarioI.ManejaPrecioConIVA)
                                {
                                    if (oUsuarioI.ManejaDescuentoConIVA)
                                    {
                                        hddValorDescuento.Value = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                    else
                                    {
                                        hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                else
                                {
                                    hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * Descuento)).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            else
                            {
                                if (oUsuarioI.ManejaCostoConIVA)
                                {
                                    hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * (1 - (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))) * Descuento).ToString(Util.ObtenerFormatoDecimal());
                                }
                                else
                                {
                                    hddValorDescuento.Value = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) * Descuento)).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            txtCodigo.Focus();
                            AdicionarDetalle();
                        }
                    }
                    else
                    {
                        MostrarControl(divDescuento.ClientID);
                        string Titulo = "Requiere Autorización.";
                        if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarValidador"))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarValidador", string.Format("$(document).ready(function(){{MostrarDescuento('{0}', 600);}});", Titulo), true);
                        }
                    }
                }
                else
                {
                    hddValorDescuento.Value = "0";
                    AdicionarDetalle();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error", string.Format("No se pudo adicionar la materia prima. {0}", ex.Message));
            }
        }

        private void CargarColumnasFactura(ref DataTable dt)
        {
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdArticulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Codigo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Articulo";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IdBodega";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Bodega";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Cantidad";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Descuento";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IVA";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorUnitario";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorUnitarioConDescuento";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorUnitarioConIVA";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "TotalLinea";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CostoPonderado";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ValorDescuento";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PrecioCosto";
            dt.Columns.Add(column);
        }

        private void AdicionarDetalle()
        {
            hddIdEliminar.Value = string.Empty;
            OcultarControl(divValidador.ClientID);
            bool Validador = true;
            decimal Cantidad = 0;
            if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.SalidaMercancia.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoCompra.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode().ToString())
            {
                if (hddEsInventario.Value == "1" && (decimal.Parse(hddCantidad.Value, NumberStyles.Currency) < decimal.Parse(txtCantidad.Text, NumberStyles.Currency)))
                {
                    Validador = false;
                    MostrarMensaje("Error", "No hay existencias suficientes del artículo para la venta.");
                }
            }
            if (Validador)
            {
                DataTable dt = new DataTable();
                bool Existe = false;
                if (dgFactura.Items.Count > 0)
                {
                    CargarColumnasFactura(ref dt);
                    foreach (DataGridItem Item in dgFactura.Items)
                    {
                        if (Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text == hddIdArticulo.Value && hddIdBodega.Value == Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text)
                        {
                            Existe = true;
                            Cantidad = Cantidad + decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                        }
                        DataRow copia;
                        copia = dt.NewRow();
                        copia["IdArticulo"] = Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text;
                        copia["Codigo"] = Item.Cells[dgFacturaEnum.Codigo.GetHashCode()].Text;
                        copia["Articulo"] = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                        copia["IdBodega"] = Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text;
                        copia["Bodega"] = Item.Cells[dgFacturaEnum.Bodega.GetHashCode()].Text;
                        copia["Cantidad"] = Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text;
                        copia["ValorUnitario"] = Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text;
                        copia["Descuento"] = Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text;
                        copia["ValorUnitarioConDescuento"] = Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text;
                        copia["IVA"] = Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text;
                        copia["ValorUnitarioConIVA"] = Item.Cells[dgFacturaEnum.ValorUnitarioConIVA.GetHashCode()].Text;
                        copia["TotalLinea"] = Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text;
                        copia["CostoPonderado"] = Item.Cells[dgFacturaEnum.CostoPonderado.GetHashCode()].Text;
                        copia["ValorDescuento"] = Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text;
                        copia["PrecioCosto"] = Item.Cells[dgFacturaEnum.PrecioCosto.GetHashCode()].Text;
                        dt.Rows.Add(copia);
                    }
                }
                else
                {
                    CargarColumnasFactura(ref dt);
                }
                if (!Existe)
                {
                    DataRow row;
                    row = dt.NewRow();
                    row["IdArticulo"] = hddIdArticulo.Value;
                    row["Codigo"] = txtCodigo.Text;
                    row["Articulo"] = txtDescripcion.Text;
                    row["IdBodega"] = hddIdBodega.Value;
                    row["Bodega"] = txtBodega.Text;
                    row["Cantidad"] = decimal.Parse(txtCantidad.Text, NumberStyles.Currency).ToString();
                    row["Descuento"] = decimal.Parse(txtDescuento.Text, NumberStyles.Currency).ToString();
                    if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                    {
                        if (oUsuarioI.ManejaPrecioConIVA)
                        {
                            if (oUsuarioI.ManejaDescuentoConIVA)
                            {
                                row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            }
                            else
                            {
                                row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            }
                        }
                        else
                        {
                            row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                            row["ValorUnitarioConIVA"] = Math.Round(((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                            row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                        }
                    }
                    else
                    {
                        if (oUsuarioI.ManejaCostoConIVA)
                        {
                            if (oUsuarioI.ManejaDescuentoConIVA)
                            {
                                row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            }
                            else
                            {
                                row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            }
                        }
                        else
                        {
                            row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                            row["ValorUnitarioConIVA"] = Math.Round(((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                            row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                        }
                    }
                    row["IVA"] = decimal.Parse(hddIVA.Value, NumberStyles.Currency).ToString();
                    row["CostoPonderado"] = decimal.Parse(hddCostoPonderado.Value, NumberStyles.Currency).ToString();
                    row["ValorDescuento"] = decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency).ToString();
                    row["PrecioCosto"] = decimal.Parse(hddPrecioCosto.Value, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                    dt.Rows.Add(row);
                    txtCodigo.Focus();
                }
                else
                {
                    if ((hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.SalidaMercancia.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoCompra.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Remision.GetHashCode().ToString()))
                    {
                        if (hddEsInventario.Value == "1" && (decimal.Parse(hddCantidad.Value, NumberStyles.Currency) < (decimal.Parse(txtCantidad.Text, NumberStyles.Currency) + Cantidad)))
                        {
                            MostrarMensaje("Error", "No hay existencias suficientes del artículo para la venta.");
                        }
                        else
                        {
                            DataRow row;
                            row = dt.NewRow();
                            row["IdArticulo"] = hddIdArticulo.Value;
                            row["Codigo"] = txtCodigo.Text;
                            row["Articulo"] = txtDescripcion.Text;
                            row["IdBodega"] = hddIdBodega.Value;
                            row["Bodega"] = txtBodega.Text;
                            row["Cantidad"] = decimal.Parse(txtCantidad.Text, NumberStyles.Currency).ToString();
                            row["Descuento"] = decimal.Parse(txtDescuento.Text, NumberStyles.Currency).ToString();
                            if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                            {
                                if (oUsuarioI.ManejaPrecioConIVA)
                                {
                                    if (oUsuarioI.ManejaDescuentoConIVA)
                                    {
                                        row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                    else
                                    {
                                        row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                else
                                {
                                    row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = Math.Round(((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            else
                            {
                                if (oUsuarioI.ManejaCostoConIVA)
                                {
                                    if (oUsuarioI.ManejaDescuentoConIVA)
                                    {
                                        row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                    else
                                    {
                                        row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                        row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                        row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    }
                                }
                                else
                                {
                                    row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = Math.Round(((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            row["IVA"] = decimal.Parse(hddIVA.Value, NumberStyles.Currency).ToString();
                            row["CostoPonderado"] = decimal.Parse(hddCostoPonderado.Value, NumberStyles.Currency).ToString();
                            row["ValorDescuento"] = decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency).ToString();
                            row["PrecioCosto"] = decimal.Parse(hddPrecioCosto.Value, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                            dt.Rows.Add(row);
                            txtCodigo.Focus();
                        }
                    }
                    else
                    {
                        DataRow row;
                        row = dt.NewRow();
                        row["IdArticulo"] = hddIdArticulo.Value;
                        row["Codigo"] = txtCodigo.Text;
                        row["Articulo"] = txtDescripcion.Text;
                        row["IdBodega"] = hddIdBodega.Value;
                        row["Bodega"] = txtBodega.Text;
                        row["Cantidad"] = decimal.Parse(txtCantidad.Text, NumberStyles.Currency).ToString();
                        row["Descuento"] = decimal.Parse(txtDescuento.Text, NumberStyles.Currency).ToString();
                        if (hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Venta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.NotaCreditoVenta.GetHashCode().ToString() || hddTipoDocumento.Value == tblDocumentoBusiness.TipoDocumentoEnum.Cotizaciones.GetHashCode().ToString())
                        {
                            if (oUsuarioI.ManejaPrecioConIVA)
                            {
                                if (oUsuarioI.ManejaDescuentoConIVA)
                                {
                                    row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                }
                                else
                                {
                                    row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            else
                            {
                                row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = Math.Round(((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            }
                        }
                        else
                        {
                            if (oUsuarioI.ManejaCostoConIVA)
                            {
                                if (oUsuarioI.ManejaDescuentoConIVA)
                                {
                                    row["ValorUnitario"] = ((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                }
                                else
                                {
                                    row["ValorUnitario"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) / (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConDescuento"] = (decimal.Parse(row["ValorUnitario"].ToString(), NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                    row["ValorUnitarioConIVA"] = Math.Round((decimal.Parse(row["ValorUnitarioConDescuento"].ToString(), NumberStyles.Currency) * (1 + (decimal.Parse(hddIVA.Value, NumberStyles.Currency) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                    row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                                }
                            }
                            else
                            {
                                row["ValorUnitario"] = decimal.Parse(txtPrecio.Text, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConDescuento"] = (decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)).ToString(Util.ObtenerFormatoDecimal());
                                row["ValorUnitarioConIVA"] = Math.Round(((decimal.Parse(txtPrecio.Text, NumberStyles.Currency) - decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency)) * (1 + (decimal.Parse(hddIVA.Value) / 100))), 0).ToString(Util.ObtenerFormatoDecimal());
                                row["TotalLinea"] = Math.Round((decimal.Parse(txtCantidad.Text, NumberStyles.Currency) * (decimal.Parse(row["ValorUnitarioConIVA"].ToString(), NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            }
                        }
                        row["IVA"] = decimal.Parse(hddIVA.Value, NumberStyles.Currency).ToString();
                        row["CostoPonderado"] = decimal.Parse(hddCostoPonderado.Value, NumberStyles.Currency).ToString();
                        row["ValorDescuento"] = decimal.Parse(hddValorDescuento.Value, NumberStyles.Currency).ToString();
                        row["PrecioCosto"] = decimal.Parse(hddPrecioCosto.Value, NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                        dt.Rows.Add(row);
                        txtCodigo.Focus();
                    }
                }
                dgFactura.DataSource = dt;
                dgFactura.DataBind();
                hddIdArticulo.Value = "0";
                txtDescripcion.Text = "";
                hddIdBodega.Value = "0";
                txtBodega.Text = "";
                txtCantidad.Text = "";
                if (string.IsNullOrEmpty(hddDescuento.Value))
                {
                    txtDescuento.Text = "0";
                }
                else
                {
                    txtDescuento.Text = hddDescuento.Value;
                }
                txtDescuento.Text = "0";
                txtCodigo.Text = "";
                txtPrecio.Text = "0";
                hddValorDescuento.Value = "0";
                CalcularTotalesDocumento();
            }
        }

        private void CalcularTotalesDocumento()
        {
            try
            {
                txtAntesIVA.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalDescuento.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalIVA.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                txtTotalFactura.Text = decimal.Parse("0").ToString(Util.ObtenerFormatoDecimal());
                foreach (DataGridItem Item in dgFactura.Items)
                {
                    if (oUsuarioI.ManejaPrecioConIVA)
                    {
                        if (oUsuarioI.ManejaDescuentoConIVA)
                        {
                            txtAntesIVA.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalDescuento.Text = Math.Round((decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalIVA.Text = Math.Round((decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency) + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalFactura.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                        }
                        else
                        {
                            txtAntesIVA.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalDescuento.Text = Math.Round((decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalIVA.Text = Math.Round((decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency) + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                            txtTotalFactura.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                        }
                    }
                    else
                    {
                        txtAntesIVA.Text = Math.Round((decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalDescuento.Text = Math.Round((decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency) + (decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency))), 0).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalIVA.Text = Math.Round((decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency) + ((decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitarioConDescuento.GetHashCode()].Text, NumberStyles.Currency) * (decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency) / 100))) * decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                        txtTotalFactura.Text = Math.Round((decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency) + decimal.Parse(Item.Cells[dgFacturaEnum.TotalLinea.GetHashCode()].Text, NumberStyles.Currency)), 0).ToString(Util.ObtenerFormatoDecimal());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                tblDocumentoBusiness oDocB = new tblDocumentoBusiness(CadenaConexion);
                List<tblDetalleDocumentoItem> oListDet = new List<tblDetalleDocumentoItem>();
                List<tblTipoPagoItem> oTipPagLis = new List<tblTipoPagoItem>();
                tblPagoItem oPagoI = new tblPagoItem();
                string Errores = string.Empty;
                short NumeroLinea = 1;
                foreach (DataGridItem Item in dgFactura.Items)
                {
                    tblDetalleDocumentoItem oDetI = new tblDetalleDocumentoItem();
                    oDetI.NumeroLinea = NumeroLinea;
                    oDetI.idArticulo = long.Parse(Item.Cells[dgFacturaEnum.IdArticulo.GetHashCode()].Text);
                    oDetI.Articulo = Item.Cells[dgFacturaEnum.Articulo.GetHashCode()].Text;
                    oDetI.ValorUnitario = decimal.Parse(Item.Cells[dgFacturaEnum.ValorUnitario.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.IVA = decimal.Parse(Item.Cells[dgFacturaEnum.IVA.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.Cantidad = decimal.Parse(Item.Cells[dgFacturaEnum.Cantidad.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.Descuento = decimal.Parse(Item.Cells[dgFacturaEnum.Descuento.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.CostoPonderado = decimal.Parse(Item.Cells[dgFacturaEnum.CostoPonderado.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.ValorDescuento = decimal.Parse(Item.Cells[dgFacturaEnum.ValorDescuento.GetHashCode()].Text, NumberStyles.Currency);
                    oDetI.idBodega = long.Parse(Item.Cells[dgFacturaEnum.IdBodega.GetHashCode()].Text);
                    oListDet.Add(oDetI);
                    NumeroLinea++;
                }
                foreach (DataGridItem Cliente in dgClientes.Items)
                {
                    if (((CheckBox)(Cliente.Cells[dgClientesEnum.Seleccionar.GetHashCode()].FindControl("chkSeleccionar"))).Checked)
                    {
                        tblDocumentoItem oDocItem = new tblDocumentoItem();
                        oDocItem.Fecha = Util.ObtenerFecha(oUsuarioI.idEmpresa);
                        oDocItem.idTercero = long.Parse(Cliente.Cells[dgClientesEnum.IdTercero.GetHashCode()].Text);
                        oDocItem.Telefono = Cliente.Cells[dgClientesEnum.Telefono.GetHashCode()].Text;
                        oDocItem.Direccion = Cliente.Cells[dgClientesEnum.Direccion.GetHashCode()].Text;
                        oDocItem.idCiudad = short.Parse(Cliente.Cells[dgClientesEnum.IdCiudad.GetHashCode()].Text);
                        oDocItem.NombreTercero = Cliente.Cells[dgClientesEnum.Nombre.GetHashCode()].Text;
                        oDocItem.Observaciones = txtObservaciones.Text.Replace(Environment.NewLine, " ");
                        oDocItem.idEmpresa = oUsuarioI.idEmpresa;
                        oDocItem.idUsuario = oUsuarioI.idUsuario;
                        oDocItem.TotalDocumento = decimal.Parse(txtTotalFactura.Text, NumberStyles.Currency);
                        oDocItem.TotalIVA = decimal.Parse(txtTotalIVA.Text, NumberStyles.Currency);
                        oDocItem.saldo = oDocItem.TotalDocumento;
                        oDocItem.IdTipoDocumento = int.Parse(hddTipoDocumento.Value);
                        oDocItem.EnCuadre = false;
                        oDocItem.IdEstado = tblDocumentoBusiness.EstadoDocumentoEnum.Abierto.GetHashCode();
                        oDocItem.TotalDescuento = decimal.Parse(txtTotalDescuento.Text, NumberStyles.Currency);
                        oDocItem.TotalAntesIVA = decimal.Parse(txtAntesIVA.Text, NumberStyles.Currency);
                        oDocItem.FechaVencimiento = DateTime.Now;
                        if (!oDocB.GuardarTodo(oDocItem, oListDet, oPagoI, oTipPagLis))
                        {
                            Errores = string.Format("No se pudo realizar la factura del cliente {0}. {1}", oDocItem.NombreTercero, Errores);
                        }
                    }
                }
                if (string.IsNullOrEmpty(Errores))
                {
                    MostrarMensaje("Exito", "La facturación masiva se realizó con exito.");
                }
                else
                {
                    MostrarMensaje("Error", Errores);
                }
                dgFactura.DataSource = null;
                dgFactura.DataBind();
                txtTotalFactura.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                txtTotalIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                txtAntesIVA.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                txtTotalDescuento.Text = decimal.Parse("0", NumberStyles.Currency).ToString(Util.ObtenerFormatoDecimal());
                txtObservaciones.Text = "";
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }

        protected void ddlGrupoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarClientes();
            }
            catch(Exception ex)
            {
                MostrarMensaje("Error", ex.Message);
            }
        }
    }
}