using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;
using System.IO;
using System.Globalization;

namespace Inventario
{
    public partial class frmEmpresa : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        public string srcLogo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    CadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(CadenaConexion);
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Empresa.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        InicializarControles();
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarTipoContribuyente();
                            CargarRegimenFiscal();
                            CargarResponsabilidadFiscal();
                            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                            tblEmpresaItem oEmpI = new tblEmpresaItem();
                            oEmpI = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                            CargarDatosEmpresa(oEmpI);
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaInicialPedido.ClientID);
                            strScript = string.Format("{0} EstablecerAutoCompleteCiudad('{1}','Ashx/Ciudad.ashx','{2}');", strScript, txtCiudad.ClientID, hddIdCiudad.ClientID);
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

        private void InicializarControles() {
            fluImage.Style.Add("display", "none");
            fluImage.Style.Add("visibility", "hidden");
        }

        private void ConfiguracionIdioma()
        {
            try
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
                lblTitulo.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Empresa);
                lblIdentificacion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
                lblTipoIdentificacion.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion));
                lblNombre.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                txtNombre.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
                lblTelefono.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Telefono);
                txtTelefono.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Telefono));
                lblDireccion.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion);
                txtDireccion.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion));
                lblCiudad.Text = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ciudad);
                txtCiudad.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ciudad));
                txtEncabezado.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Encabezado));
                txtPiePagina.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.PiePagina));
                CargarTipoIdentificacion(oCIdioma, Idioma);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void CargarTipoContribuyente()
        {
            try
            {
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                ddlTipoContribuyente.DataSource = oTerB.ObtenerTipoContribuyente();
                ddlTipoContribuyente.DataBind();
                ddlTipoIdentificacion.Items.Add(new ListItem("Seleccione opción", ""));
                ddlTipoContribuyente.SelectedValue = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarRegimenFiscal()
        {
            try
            {
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                ddlRegimenFiscal.DataSource = oTerB.ObtenerRegimenFiscal();
                ddlRegimenFiscal.DataBind();
                ddlRegimenFiscal.Items.Add(new ListItem("Seleccione opción", ""));
                ddlRegimenFiscal.SelectedValue = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CargarResponsabilidadFiscal()
        {
            try
            {
                tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                ddlResponsabilidadFiscal.DataSource = oTerB.ObtenerResponsabilidadFiscal();
                ddlResponsabilidadFiscal.DataBind();
                ddlResponsabilidadFiscal.Items.Add(new ListItem("Seleccione opción", ""));
                ddlResponsabilidadFiscal.SelectedValue = "";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void CargarTipoIdentificacion(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                string Opcion = ddlTipoIdentificacion.SelectedValue;
                ddlTipoIdentificacion.Items.Clear();
                ddlTipoIdentificacion.SelectedValue = null;
                ddlTipoIdentificacion.Items.Add(new ListItem(string.Format("{0} {1} {2} {3}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion)), "0"));
                ddlTipoIdentificacion.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nit), tblTerceroBusiness.TipoIdentificacion.Nit.GetHashCode().ToString()));
                ddlTipoIdentificacion.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cedula), tblTerceroBusiness.TipoIdentificacion.Cedula.GetHashCode().ToString()));
                ddlTipoIdentificacion.DataBind();
                if (!IsPostBack)
                {
                    ddlTipoIdentificacion.SelectedValue = "0";
                }
                else
                {
                    ddlTipoIdentificacion.SelectedValue = Opcion;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDatosGuardar(ref tblEmpresaItem empresa)
        {
            try
            {
                tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                empresa = oEmpB.ObtenerEmpresa(oUsuarioI.idEmpresa);
                empresa.idEmpresa = oUsuarioI.idEmpresa;
                empresa.Nombre = txtNombre.Text.Replace(Environment.NewLine, " ");
                empresa.idTipoIdentificacion = short.Parse(ddlTipoIdentificacion.SelectedValue);
                empresa.Identificacion = txtIdentificacion.Text;
                empresa.Direccion = txtDireccion.Text;
                empresa.idCiudad = short.Parse(hddIdCiudad.Value);
                empresa.Telefono = txtTelefono.Text;
                empresa.MultiBodega = chkMultiBodega.Checked;
                empresa.FechaInicialEntrega = DateTime.Parse(txtFechaInicialPedido.Text);
                //empresa.ManejaPrecioConIVA = chkPrecioConIVA.Checked;
                //empresa.ManejaCostoConIVA = chkCostoConIVA.Checked;
                //empresa.ManejaDescuentoConIVA = chkDescuentoConIVA.Checked;
                empresa.Consecutivo = int.Parse(txtConsecutivoDIAN.Text.Trim());
                if (fluCertificado.HasFile)
                {
                    empresa.CertificadoFE = fluCertificado.FileBytes;
                }
                empresa.TextoEncabezadoFactura = txtEncabezado.Text.Replace(Environment.NewLine, " ");
                empresa.TextoPieFactura = txtPiePagina.Text.Replace(Environment.NewLine, " ");
                empresa.Impoconsumo = decimal.Parse(txtImpoconsumo.Text, NumberStyles.Currency);
                tblDocumentoBusiness oDBiz = new tblDocumentoBusiness(CadenaConexion);
                if (fluImage.HasFile)
                {
                    System.Drawing.Bitmap oBitmap = new System.Drawing.Bitmap(fluImage.PostedFile.InputStream);
                    System.Drawing.Bitmap ImagenPequeña = oDBiz.CambiarTamanoImagen((System.Drawing.Image)oBitmap, Util.ImagenAncho(), Util.ImagenAlto()); ;
                    using (var stream = new MemoryStream())
                    {
                        oBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        empresa.Logo = stream.ToArray();
                    }
                }
                empresa.SoftwareID = txtSoftwareID.Text.Trim();
                empresa.SoftwarePIN = txtSoftwarePIN.Text.Trim();
                empresa.MatriculaMercantil = txtMatriculaMercantil.Text.Trim();
                empresa.TipoContribuyente = ddlTipoContribuyente.SelectedValue;
                empresa.RegimenFiscal = ddlRegimenFiscal.SelectedValue;
                empresa.ResponsabilidadFiscal = ddlResponsabilidadFiscal.SelectedValue;
                empresa.TestSetId = txtTestSetId.Text.Trim();
                empresa.ClaveTecnica = txtClaveTecnica.Text.Trim();
                empresa.ClaveCertificado = txtClaveCertificado.Text.Trim();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void CargarDatosEmpresa(tblEmpresaItem empresa)
        {
            try
            {
                txtNombre.Text = empresa.Nombre;
                ddlTipoIdentificacion.SelectedValue = empresa.idTipoIdentificacion.ToString();
                txtIdentificacion.Text = empresa.Identificacion;
                txtTelefono.Text = empresa.Telefono;
                txtCiudad.Text = empresa.Ciudad;
                hddIdCiudad.Value = empresa.idCiudad.ToString();
                txtDireccion.Text = empresa.Direccion;
                chkMultiBodega.Checked = empresa.MultiBodega;
                txtFechaInicialPedido.Text = empresa.FechaInicialEntrega.ToShortDateString();
                txtConsecutivoDIAN.Text = empresa.Consecutivo.ToString();
                //chkPrecioConIVA.Checked = empresa.ManejaPrecioConIVA;
                //chkCostoConIVA.Checked = empresa.ManejaCostoConIVA;
                //chkDescuentoConIVA.Checked = empresa.ManejaDescuentoConIVA;
                txtEncabezado.Text = empresa.TextoEncabezadoFactura;
                txtPiePagina.Text = empresa.TextoPieFactura;
                txtImpoconsumo.Text = empresa.Impoconsumo.ToString();
                txtSoftwareID.Text = empresa.SoftwareID;
                txtSoftwarePIN.Text = empresa.SoftwarePIN;
                txtMatriculaMercantil.Text = empresa.MatriculaMercantil;
                ddlTipoContribuyente.SelectedValue = empresa.TipoContribuyente;
                ddlRegimenFiscal.SelectedValue = empresa.RegimenFiscal;
                ddlResponsabilidadFiscal.SelectedValue = empresa.CodigoResponsabilidadFiscal;
                txtTestSetId.Text = empresa.TestSetId;
                txtClaveTecnica.Text = empresa.ClaveTecnica;
                txtClaveCertificado.Text = empresa.ClaveCertificado;
                if (empresa.Logo != null && empresa.Logo.Length > 0)
                {
                    srcLogo = "frmObtenerLogo.aspx";
                }
                else
                {
                    srcLogo = "Images/Default.png";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(CadenaConexion);
                tblEmpresaItem oEmpI = new tblEmpresaItem();
                CargarDatosGuardar(ref oEmpI);
                if (oRolPagI.Actualizar)
                {
                    if (oEmpB.Guardar(oEmpI, new EmpresaUsuario()))
                    {
                        MostrarAlerta(1, "Exito", "Los datos de la empresa se guardaron con exito.");
                        if (oEmpI.Logo != null && oEmpI.Logo.Length > 0)
                        {
                            srcLogo = "frmObtenerLogo.aspx";
                        }
                        else
                        {
                            srcLogo = "Images/Default.png";
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "Los datos de la empresa no se pudieron actualizar con exito.");
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