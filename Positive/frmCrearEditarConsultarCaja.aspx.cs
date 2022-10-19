using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using Idioma;
using HQSFramework.Base;

namespace Inventario
{
    public partial class frmCrearEditarConsultarCaja : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgCajasColumnsEnum
        {
            idCaja = 0,
            nombreCaja = 1,
            idBodega = 2,
            Bodega = 3,
            ValorInicial = 4,
            ValorFinal = 5,
            ProximoValor = 6,
            Resolucion = 7,
            Activo = 8
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                oUsuarioI = (tblUsuarioItem)(Session["Usuario"]);
                if (oUsuarioI != null)
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["inventario"].ConnectionString;
                    SeguridadBusiness oSegB = new SeguridadBusiness(cadenaConexion);
                    hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarConsultarCajas.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        ConfiguracionIdioma();
                        if (!IsPostBack)
                        {
                            CargarCajas();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerDataPicker('{1}');", strScript, txtFechaVencimiento.ClientID);
                            strScript = string.Format("{0} EstablecerAutoCompleteBodega('{1}','Ashx/Bodega.ashx','{2}','{3}','1');", strScript, txtBodega.ClientID, hddIdBodega.ClientID, oUsuarioI.idEmpresa);
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
                MostrarAlerta(0, "Error", string.Format("No se pudo cargar la pagina. {0}", ex.Message));
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
            lblTitulo.Text = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Crear), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Caja));
            lblTituloGrilla.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cajas));
            txtFechaVencimiento.Attributes.Add("placeholder", "DD/MM/AAAA");
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgCajas.Columns[dgCajasColumnsEnum.nombreCaja.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                dgCajas.Columns[dgCajasColumnsEnum.Bodega.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Bodega);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void limpiarControles()
        {
            CargarCajas();
            txtNombre.Text = "";
            hddIdCaja.Value = "";
            txtBodega.Text = "";
            hddIdBodega.Value = "";
            txtProximoValor.Text = "";
            txtValorInicial.Text = "";
            txtValorFinal.Text = "";
            txtResolucion.Text = "";
            txtFechaVencimiento.Text = "";
            chkActivo.Checked = false;
        }

        private void CargarDatosGuardar(tblCajaItem Caja)
        {
            try
            {
                if (!string.IsNullOrEmpty(hddIdCaja.Value) && hddIdCaja.Value != "0")
                {
                    Caja.idCaja = long.Parse(hddIdCaja.Value);
                }
                Caja.nombre = txtNombre.Text;
                Caja.idEmpresa = oUsuarioI.idEmpresa;
                Caja.idBodega = long.Parse(hddIdBodega.Value);
                Caja.ValorInicial = txtValorInicial.Text;
                Caja.ValorFinal = txtValorFinal.Text;
                Caja.ProximoValor = txtProximoValor.Text;
                Caja.Resolucion = txtResolucion.Text;
                Caja.Activo = chkActivo.Checked;
                if (!string.IsNullOrEmpty(txtFechaVencimiento.Text))
                {
                    Caja.FechaVencimiento = DateTime.Parse(txtFechaVencimiento.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarCajas()
        {
            try
            {
                tblCajaBusiness oCajaB = new tblCajaBusiness(cadenaConexion);
                dgCajas.DataSource = oCajaB.ObtenerCajaLista(oUsuarioI.idEmpresa);
                dgCajas.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void dgCajas_EditCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (oRolPagI.Actualizar)
                {
                    tblCajaBusiness oCajaB = new tblCajaBusiness(cadenaConexion);
                    tblCajaItem oCajaI = new tblCajaItem();
                    oCajaI = oCajaB.ObtenerCajaPorID(long.Parse(e.Item.Cells[dgCajasColumnsEnum.idCaja.GetHashCode()].Text));
                    if(oCajaI.idCaja > 0)
                    {
                        txtNombre.Text = oCajaI.nombre;
                        hddIdCaja.Value = oCajaI.idCaja.ToString();
                        hddIdBodega.Value = oCajaI.idBodega.ToString();
                        txtBodega.Text = oCajaI.Bodega;
                        txtValorInicial.Text = oCajaI.ValorInicial;
                        txtValorFinal.Text = oCajaI.ValorFinal;
                        txtProximoValor.Text = oCajaI.ProximoValor;
                        txtResolucion.Text = oCajaI.Resolucion;
                        chkActivo.Checked = oCajaI.Activo;
                        if(oCajaI.FechaVencimiento != DateTime.MinValue)
                        {
                            txtFechaVencimiento.Text = oCajaI.FechaVencimiento.ToShortDateString();
                        }
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", "El ID de la caja no valido.");
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "El usuario no posee permisos para esta operación");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }
        private string ValidarDatos()
        {
            try
            {
                string Errores = string.Empty;
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    Errores = string.Format("{0} Por favor digite un nombre valido.", Errores);
                }
                if (string.IsNullOrEmpty(txtProximoValor.Text))
                {
                    Errores = string.Format("{0} Por favor digite un proximo valor valido.", Errores);
                }
                if (string.IsNullOrEmpty(hddIdBodega.Value))
                {
                    Errores = string.Format("{0} Por favor seleccione una bodega valida.", Errores);
                }
                if (string.IsNullOrEmpty(txtValorInicial.Text))
                {
                    Errores = string.Format("{0} Por favor digite un valor inicial valido.", Errores);
                }
                if (string.IsNullOrEmpty(txtValorFinal.Text))
                {
                    Errores = string.Format("{0} Por favor digite un valor final valido.", Errores);
                }
                return Errores;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string Errores = ValidarDatos();
                if (string.IsNullOrEmpty(Errores))
                {
                    tblCajaBusiness oCajaB = new tblCajaBusiness(cadenaConexion);
                    tblCajaItem oCajaI = new tblCajaItem();
                    CargarDatosGuardar(oCajaI);
                    if (oRolPagI.Insertar && oCajaI.idCaja == 0)
                    {
                        if (oCajaB.Guardar(oCajaI))
                        {
                            MostrarAlerta(1, "Exitoso", "La caja se creo con exito");
                            limpiarControles();
                        }
                        else
                        {
                            MostrarAlerta(0, "Error", "Error al crear la caja.");
                        }
                    }
                    else
                    {
                        if (oRolPagI.Actualizar && oCajaI.idCaja > 0)
                        {
                            if (oCajaB.Guardar(oCajaI))
                            {
                                MostrarAlerta(1, "Exitoso", "La caja se actualizo con exito");
                                limpiarControles();
                            }
                            else
                            {
                                MostrarAlerta(0, "Error", "La caja no se actualizo con exito");
                            }
                        }
                        else
                        {
                            MostrarAlerta(2, "El usuario no posee permisos para esta operación", string.Empty);
                        }
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", Errores);
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", string.Format("No se pudo crear la caja. {0}", ex.Message));
            }
        }
    }
}