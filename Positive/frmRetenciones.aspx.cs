using HQSFramework.Base;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Configuration;
using System.Web.UI;

namespace Positive
{
    public partial class frmRetenciones : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private enum dgRetencionesNum
        {
            Id = 0,
            Codigo = 1,
            Descripcion = 2,
            Porcentaje = 3,
            Base = 4,
            Activo = 5,
            Editar = 6
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.Retenciones.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        if (!IsPostBack)
                        {
                            CargarRetenciones();
                        }
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} pestañas();", strScript);
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
                MostrarAlerta(0, "Error", ex.Message);
            }
        }
        private void CargarRetenciones()
        {
            try
            {
                tblRetencionBusiness oRetB = new tblRetencionBusiness(CadenaConexion);
                dgRetenciones.DataSource = oRetB.ObtenerRetencionesTodas(oUsuarioI.idEmpresa);
                dgRetenciones.DataBind();
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
                string Errores = ValidarInformacion();
                if (string.IsNullOrEmpty(Errores))
                {
                    tblRetencionItem Item = new tblRetencionItem();
                    if (!string.IsNullOrEmpty(lblID.Text))
                    {
                        Item.Id = long.Parse(lblID.Text);
                    }
                    Item.Codigo = txtCodigo.Text;
                    Item.Descripcion = txtDescripcion.Text;
                    Item.Porcentaje = decimal.Parse(txtPorcentaje.Text, System.Globalization.NumberStyles.Currency);
                    Item.Base = decimal.Parse(txtBase.Text, System.Globalization.NumberStyles.Currency);
                    Item.Activo = chkActivo.Checked;
                    Item.IdEmpresa = oUsuarioI.idEmpresa;
                    tblRetencionBusiness oRetB = new tblRetencionBusiness(CadenaConexion);
                    Errores = oRetB.Insertar(Item);
                    if (string.IsNullOrEmpty(Errores))
                    {
                        MostrarAlerta(1, "Exito", "El registro se realizó con éxito.");
                        LimpiarControles();
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", Errores);
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", Errores);
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }
        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
        private string ValidarInformacion()
        {
            try
            {
                string Errores = string.Empty;
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    Errores = string.Format("{0} Por favor digitar un código valido.", Errores);
                }
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    Errores = string.Format("{0} Por favor digitar una descripción valida.", Errores);
                }
                if (string.IsNullOrEmpty(txtPorcentaje.Text))
                {
                    Errores = string.Format("{0} Por favor digitar un porcentaje valido.", Errores);
                }
                if (string.IsNullOrEmpty(txtBase.Text))
                {
                    Errores = string.Format("{0} Por favor digitar una base valida.", Errores);
                }
                return Errores;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void dgRetenciones_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                tblRetencionBusiness oRetB = new tblRetencionBusiness(CadenaConexion);
                tblRetencionItem oRetI = new tblRetencionItem();
                oRetI = oRetB.ObtenerRetencionPorID(long.Parse(e.Item.Cells[dgRetencionesNum.Id.GetHashCode()].Text));
                if(oRetI.Id > 0)
                {
                    lblID.Text = oRetI.Id.ToString();
                    txtCodigo.Text = oRetI.Codigo;
                    txtDescripcion.Text = oRetI.Descripcion;
                    txtPorcentaje.Text = oRetI.Porcentaje.ToString(Util.ObtenerFormatoDecimal());
                    txtBase.Text = oRetI.Base.ToString(Util.ObtenerFormatoDecimal());
                    chkActivo.Checked = oRetI.Activo;
                }
                else
                {
                    MostrarAlerta(0, "Error", "No se pudo encontrar un registro valido.");
                }
            }
            catch(Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message);
            }
        }
        private void LimpiarControles()
        {
            try
            {
                lblID.Text = "";
                txtCodigo.Text = "";
                txtDescripcion.Text = "";
                txtPorcentaje.Text = "";
                txtBase.Text = "";
                chkActivo.Checked = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}