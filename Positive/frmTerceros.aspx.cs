using HQSFramework.Base;
using Idioma;
using InventarioBusiness;
using InventarioItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Positive
{
    public partial class frmTerceros : PaginaBase
    {
        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();

        private enum dgTercerosColumn {
            IdTercero = 0,
            TipoTercero = 1,
            TipoIdentificacion = 2,
            TipoIdentificacionNombre = 3,
            Identificacion = 4,
            IdGrupo = 5,
            Grupo = 6,
            Nombre = 7,
            Direccion = 8,
            Telefono = 9,
            Mail = 10,
            IdCiudad = 11,
            Ciudad = 12,
            Nuevo = 13,
            Errores = 14
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
                    oRolPagI = oSegB.TraerPermisosPaginasPorUsuario(oUsuarioI.idUsuario, oUsuarioI.idEmpresa, short.Parse(SeguridadBusiness.paginasEnum.CrearEditarTerceros.GetHashCode().ToString()));
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
                MostrarMensaje("Error", string.Format("No se pudo cargar la pagina de creación masiva de terceros. {0}", ex.Message));
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

        private void CargarDelimitadores()
        {
            try
            {
                ddlDelimitador.Items.Clear();
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
            dgTerceros.DataSource = null;
            dgTerceros.DataBind();
            CargarDelimitadores();
        }

        protected void dgTerceros_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                string Error = "";
                if (string.IsNullOrEmpty(e.Item.Cells[dgTercerosColumn.TipoTercero.GetHashCode()].Text))
                {
                    Error = $"{Error}<br/>El campo <b>Tipo Tercero</b> es obligatorio";
                }
                else {
                    if (e.Item.Cells[dgTercerosColumn.TipoTercero.GetHashCode()].Text.ToUpper() != "C" && e.Item.Cells[dgTercerosColumn.TipoTercero.GetHashCode()].Text.ToUpper() != "P") {
                        Error = $"{Error}<br/>El campo <b>Tipo Tercero</b> no es correcto";
                    }
                }

                if (string.IsNullOrEmpty(e.Item.Cells[dgTercerosColumn.Identificacion.GetHashCode()].Text))
                {
                    Error = $"{Error}<br/>El campo <b>Identificación</b> es obligatorio";
                }
                else
                {
                    tblTerceroItem oTItem = oTBiz.ObtenerTerceroPorIdentificacion(e.Item.Cells[dgTercerosColumn.Identificacion.GetHashCode()].Text, oUsuarioI.idEmpresa);
                    if (oTItem != null && oTItem.IdTercero > 0 && oTItem.TipoTercero.ToUpper() == e.Item.Cells[dgTercerosColumn.TipoTercero.GetHashCode()].Text.ToUpper())
                    {
                        e.Item.Cells[dgTercerosColumn.IdTercero.GetHashCode()].Text = oTItem.IdTercero.ToString();
                        e.Item.Cells[dgTercerosColumn.Nuevo.GetHashCode()].Text = "No";
                    }
                    else{
                        e.Item.Cells[dgTercerosColumn.IdTercero.GetHashCode()].Text = "0";
                        e.Item.Cells[dgTercerosColumn.Nuevo.GetHashCode()].Text = "Si";
                    }
                }

                short.TryParse(e.Item.Cells[dgTercerosColumn.TipoIdentificacion.GetHashCode()].Text, out short TipoIdentificacion);
                if (TipoIdentificacion == 0)
                {
                    Error = $"{Error}<br/>El campo <b>Tipo Identificacion</b> no tiene el formato correcto";
                }
                else {
                    tblTipoIdentificacionBusiness oTIBiz = new tblTipoIdentificacionBusiness(CadenaConexion);
                    var oTIItem = oTIBiz.ObtenerTipoIdentificacionLista().FirstOrDefault(c => c.idTipoIdentificacion == TipoIdentificacion);
                    if (oTIItem != null && oTIItem.idTipoIdentificacion > 0)
                    {
                        e.Item.Cells[dgTercerosColumn.TipoIdentificacionNombre.GetHashCode()].Text = oTIItem.Nombre;
                    }
                    else {
                        Error = $"{Error}<br/>El campo <b>Tipo Identificacion</b> no tiene el formato correcto";
                    }
                }

                int.TryParse(e.Item.Cells[dgTercerosColumn.IdGrupo.GetHashCode()].Text, out int IdGrupo);
                if (IdGrupo == 0)
                {
                    Error = $"{Error}<br/>El campo <b>Grupo</b> no tiene el formato correcto";
                }
                else
                {
                    var oGItem = oTBiz.ObtenerGrupoCliente(IdGrupo, oUsuarioI.idEmpresa);
                    if (oGItem != null && oGItem.idGrupoCliente > 0)
                    {
                        e.Item.Cells[dgTercerosColumn.Grupo.GetHashCode()].Text = oGItem.Nombre;
                    }
                    else
                    {
                        Error = $"{Error}<br/>El campo <b>Grupo</b> no tiene el formato correcto";
                    }
                }

                int.TryParse(e.Item.Cells[dgTercerosColumn.IdCiudad.GetHashCode()].Text, out int IdCiudad);
                if (IdGrupo == 0)
                {
                    Error = $"{Error}<br/>El campo <b>Ciudad</b> no tiene el formato correcto";
                }
                else
                {
                    tblCiudadBusiness oCiudadB = new tblCiudadBusiness(CadenaConexion);
                    var oCItem = oCiudadB.ObtenerCiudad(IdCiudad);
                    if (oCItem != null && oCItem.idCiudad > 0)
                    {
                        e.Item.Cells[dgTercerosColumn.Ciudad.GetHashCode()].Text = oCItem.Nombre;
                    }
                    else
                    {
                        Error = $"{Error}<br/>El campo <b>Ciudad</b> no tiene el formato correcto";
                    }
                }

                e.Item.Cells[dgTercerosColumn.Errores.GetHashCode()].Text = Error;
                if (Error != "")
                {
                    e.Item.BackColor = System.Drawing.Color.Yellow;
                    OcultarControl(btnGuardar.ClientID);
                }
            }
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (oRolPagI.Insertar)
                {
                    string Error = "";
                    tblTerceroBusiness oTBiz = new tblTerceroBusiness(CadenaConexion);
                    foreach (DataGridItem Item in dgTerceros.Items)
                    {
                        tblTerceroItem oTItem = new tblTerceroItem();
                        oTItem.IdTercero = long.Parse(Item.Cells[dgTercerosColumn.IdTercero.GetHashCode()].Text);
                        if (oTItem.IdTercero > 0)
                        {
                            oTItem = oTBiz.ObtenerTercero(oTItem.IdTercero, oUsuarioI.idEmpresa);
                        }
                        else { 
                            oTItem.TipoTercero = Item.Cells[dgTercerosColumn.TipoTercero.GetHashCode()].Text;
                        }
                        oTItem.idTipoIdentificacion = Item.Cells[dgTercerosColumn.TipoIdentificacion.GetHashCode()].Text;
                        oTItem.Identificacion = Item.Cells[dgTercerosColumn.Identificacion.GetHashCode()].Text;
                        oTItem.idGrupoCliente = long.Parse(Item.Cells[dgTercerosColumn.IdGrupo.GetHashCode()].Text);
                        oTItem.Nombre = Item.Cells[dgTercerosColumn.Nombre.GetHashCode()].Text;
                        oTItem.Direccion = Item.Cells[dgTercerosColumn.Direccion.GetHashCode()].Text == "&nbsp;" ? string.Empty : Item.Cells[dgTercerosColumn.Direccion.GetHashCode()].Text;
                        oTItem.Telefono = Item.Cells[dgTercerosColumn.Telefono.GetHashCode()].Text == "&nbsp;" ? string.Empty : Item.Cells[dgTercerosColumn.Telefono.GetHashCode()].Text;
                        oTItem.Mail = Item.Cells[dgTercerosColumn.Mail.GetHashCode()].Text == "&nbsp;" ? string.Empty : Item.Cells[dgTercerosColumn.Mail.GetHashCode()].Text;
                        oTItem.idCiudad = short.Parse(Item.Cells[dgTercerosColumn.IdCiudad.GetHashCode()].Text);
                        oTItem.idEmpresa = oUsuarioI.idEmpresa;
                        var result = oTBiz.insertar(oTItem);
                        if (!string.IsNullOrEmpty(result)) {
                            Error = $"{Error}<br/>{result}";
                        }
                    }
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

        protected void btnCargar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (fulTerceros.HasFile)
                {
                    btnGuardar.Visible = true;
                    List<tblTerceroItem> terceros = new List<tblTerceroItem>();
                    tblTerceroBusiness oABiz = new tblTerceroBusiness(CadenaConexion);
                    string error = oABiz.LeerTercerosArchivo(fulTerceros.PostedFile.InputStream, terceros, oUsuarioI.idEmpresa, char.Parse(ddlDelimitador.SelectedValue));
                    if (error != "")
                    {
                        MostrarMensaje("Error", error);
                    }
                    else
                    {
                        dgTerceros.DataSource = terceros;
                        dgTerceros.DataBind();
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
    }
}