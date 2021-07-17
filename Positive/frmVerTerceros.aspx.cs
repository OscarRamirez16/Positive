using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using InventarioBusiness;
using InventarioItem;
using HQSFramework.Base;
using Idioma;

namespace Inventario
{
    public partial class frmVerClientes : PaginaBase
    {

        private string cadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        enum dgClientesColumnsEnum
        {
            Numero = 0,
            idTercero = 1,
            identificacion = 2,
            nombre = 3,
            tipoTercero = 4,
            telefono = 5,
            correo = 6,
            direccion = 7,
            ciudad = 8,
            empresa = 9
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.VerTerceros.GetHashCode().ToString()));
                    if (oRolPagI.Leer)
                    {
                        hddIdEmpresa.Value = oUsuarioI.idEmpresa.ToString();
                        ConfiguracionIdioma();
                        if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControlesScript"))
                        {
                            string strScript = "$(document).ready(function(){";
                            strScript = string.Format("{0} menu();", strScript);
                            strScript = string.Format("{0} EstablecerAutoCompleteTercero('{1}','Ashx/Tercero.ashx','{2}');", strScript, txtNombre.ClientID, hddIdTercero.ClientID);
                            strScript = string.Format("{0}}});", strScript);
                            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControlesScript", strScript, true);
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('El usuario no posee permisos en la pagina');</script>");
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
                Response.Write("<script>alert('Error al cargar la pagina de ver terceros. " + ex.ToString() + "');</script>");
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
            lblTitulo.Text = string.Format("{0} {1} {2}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Lista), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SociosNegocio));
            txtNombre.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre));
            txtCedula.Attributes.Add("placeholder", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion));
            CargarTipoTercero(oCIdioma, Idioma);
            ConfigurarIdiomaGrilla(oCIdioma, Idioma);
        }

        private void ConfigurarIdiomaGrilla(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            try
            {
                dgClientes.Columns[dgClientesColumnsEnum.identificacion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Identificacion);
                dgClientes.Columns[dgClientesColumnsEnum.nombre.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Nombre);
                dgClientes.Columns[dgClientesColumnsEnum.tipoTercero.GetHashCode()].HeaderText = string.Format("{0} {1}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio));
                dgClientes.Columns[dgClientesColumnsEnum.telefono.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Telefono);
                dgClientes.Columns[dgClientesColumnsEnum.correo.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Correo);
                dgClientes.Columns[dgClientesColumnsEnum.direccion.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Direccion);
                dgClientes.Columns[dgClientesColumnsEnum.ciudad.GetHashCode()].HeaderText = oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Ciudad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CargarTipoTercero(Traductor oCIdioma, Idioma.Traductor.IdiomaEnum Idioma)
        {
            string Opcion = ddlTipoTercero.SelectedValue;
            ddlTipoTercero.Items.Clear();
            ddlTipoTercero.Items.Add(new ListItem(string.Format("{0} {1} {2} {3}", oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Seleccione), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Tipo), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.de), oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.SocioNegocio)), "-1"));
            ddlTipoTercero.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Cliente), "C"));
            ddlTipoTercero.Items.Add(new ListItem(oCIdioma.TraducirPalabra(Idioma, Traductor.IdiomaPalabraEnum.Proveedor), "P"));
            ddlTipoTercero.DataBind();
            if (!IsPostBack)
            {
                ddlTipoTercero.SelectedValue = "-1";
            }
            else
            {
                ddlTipoTercero.SelectedValue = Opcion;
            }
        }

        protected void dgClientes_EditCommand(object source, DataGridCommandEventArgs e)
        {
            if (oRolPagI.Actualizar)
            {
                Response.Redirect("frmCrearEditarTerceros.aspx?idTercero=" + e.Item.Cells[dgClientesColumnsEnum.idTercero.GetHashCode()].Text);
            }
            else
            {
                Response.Write("<script>alert('El usuario no posee permisos para esta operación');</script>");
            }
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                tblTerceroBusiness oTerceroB = new tblTerceroBusiness(cadenaConexion);
                dgClientes.DataSource = oTerceroB.ObtenerTerceroListaPorFiltrosNombreCiudadEmpresa(long.Parse(hddIdTercero.Value), txtCedula.Text, oUsuarioI.idEmpresa, ddlTipoTercero.SelectedValue);
                dgClientes.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar los terceros. " + ex.ToString() + "');</script>");
            }
        }

        protected void dgClientes_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                e.Item.Cells[dgClientesColumnsEnum.Numero.GetHashCode()].Text = (e.Item.ItemIndex + 1).ToString();
            }
        }

        
    }
}