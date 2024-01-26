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

namespace Positive
{
    public partial class frmCreacionMasivaTerceros : PaginaBase
    {

        private string CadenaConexion;
        private tblRol_PaginaItem oRolPagI = new tblRol_PaginaItem();
        private tblUsuarioItem oUsuarioI = new tblUsuarioItem();
        private enum dgTercerosColumnsEnum
        {
            TipoIdentificacion = 0,
            Identificacion = 1,
            Nombre = 2,
            Telefono = 3,
            Celular = 4,
            Mail = 5,
            Direccion = 6,
            Ciudad = 7,
            TipoTercero = 8,
            FechaNacimiento = 9,
            ListaPrecio = 10,
            GrupoCliente = 11,
            Observaciones = 12,
            TipoIdentificacionDIAN = 13,
            MatriculaMercantil = 14,
            TipoContribuyente = 15,
            RegimenFiscal = 16,
            ResponsabilidadFiscal = 17,
            CodigoZip = 18,
            Errores = 19
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
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
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
                MostrarAlerta(0, "Error", ex.Message.Replace(Environment.NewLine, " "));
            }
        }
        protected void btnCargar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (fulTerceros.HasFile)
                {
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("InicializarControles"))
                    {
                        string strScript = "$(document).ready(function(){";
                        strScript = string.Format("{0} seleccionarTab('contenido','2');", strScript);
                        strScript = string.Format("{0}}});", strScript);
                        this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "InicializarControles", strScript, true);
                    }
                    btnGuardar.Visible = true;
                    List<tblTerceroItem> oListTerceros = new List<tblTerceroItem>();
                    tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                    string error = oTerB.LeerTercerosArchivo(fulTerceros.PostedFile.InputStream, oListTerceros, char.Parse(ddlDelimitador.SelectedValue));
                    if (error != "")
                    {
                        MostrarAlerta(0, "Error", error);
                    }
                    else
                    {
                        dgTerceros.DataSource = oListTerceros;
                        dgTerceros.DataBind();
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
        private void LimpiarControles()
        {
            dgTerceros.DataSource = null;
            dgTerceros.DataBind();
            CargarDelimitadores();
        }
        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (oRolPagI.Insertar)
                {
                    string Error = "";
                    tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);
                    List<tblTerceroItem> oListTerI = new List<tblTerceroItem>();
                    foreach (DataGridItem Item in dgTerceros.Items)
                    {
                        tblTerceroItem oTerI = new tblTerceroItem();
                        oTerI.idTipoIdentificacion = short.Parse(Item.Cells[dgTercerosColumnsEnum.TipoIdentificacion.GetHashCode()].Text);
                        oTerI.Identificacion = Item.Cells[dgTercerosColumnsEnum.Identificacion.GetHashCode()].Text.Trim();
                        oTerI.Nombre = Item.Cells[dgTercerosColumnsEnum.Nombre.GetHashCode()].Text.Trim();
                        if(Item.Cells[dgTercerosColumnsEnum.Telefono.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.Telefono.GetHashCode()].Text != "")
                        {
                            oTerI.Telefono = Item.Cells[dgTercerosColumnsEnum.Telefono.GetHashCode()].Text.Trim();
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.Celular.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.Celular.GetHashCode()].Text != "")
                        {
                            oTerI.Celular = Item.Cells[dgTercerosColumnsEnum.Celular.GetHashCode()].Text.Trim();
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.Mail.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.Mail.GetHashCode()].Text != "")
                        {
                            oTerI.Mail = Item.Cells[dgTercerosColumnsEnum.Mail.GetHashCode()].Text.Trim();
                        }
                        oTerI.Direccion = Item.Cells[dgTercerosColumnsEnum.Direccion.GetHashCode()].Text.Trim();
                        if (Item.Cells[dgTercerosColumnsEnum.CodigoZip.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.CodigoZip.GetHashCode()].Text != "")
                        {
                            oTerI.CodigoZip = Item.Cells[dgTercerosColumnsEnum.CodigoZip.GetHashCode()].Text.Trim();
                        }
                        oTerI.idCiudad = short.Parse(Item.Cells[dgTercerosColumnsEnum.Ciudad.GetHashCode()].Text);
                        oTerI.idEmpresa = oUsuarioI.idEmpresa;
                        oTerI.TipoTercero = Item.Cells[dgTercerosColumnsEnum.TipoTercero.GetHashCode()].Text;
                        if (oTerI.TipoTercero == "C")
                        {
                            if (Item.Cells[dgTercerosColumnsEnum.FechaNacimiento.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.FechaNacimiento.GetHashCode()].Text != "")
                            {
                                oTerI.FechaNacimiento = DateTime.Parse(Item.Cells[dgTercerosColumnsEnum.FechaNacimiento.GetHashCode()].Text);
                            }
                            if (Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text != "")
                            {
                                oTerI.IdListaPrecio = long.Parse(Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text);
                            }
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text != "")
                        {
                            oTerI.idGrupoCliente = long.Parse(Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text);
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.Observaciones.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.Observaciones.GetHashCode()].Text != "")
                        {
                            oTerI.Observaciones = Item.Cells[dgTercerosColumnsEnum.Observaciones.GetHashCode()].Text;
                        }
                        oTerI.Activo = true;
                        oTerI.Generico = false;
                        if (Item.Cells[dgTercerosColumnsEnum.TipoIdentificacionDIAN.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.TipoIdentificacionDIAN.GetHashCode()].Text != "")
                        {
                            oTerI.TipoIdentificacionDIAN = Item.Cells[dgTercerosColumnsEnum.TipoIdentificacionDIAN.GetHashCode()].Text;
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.MatriculaMercantil.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.MatriculaMercantil.GetHashCode()].Text != "")
                        {
                            oTerI.MatriculaMercantil = Item.Cells[dgTercerosColumnsEnum.MatriculaMercantil.GetHashCode()].Text;
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.TipoContribuyente.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.TipoContribuyente.GetHashCode()].Text != "")
                        {
                            oTerI.TipoContribuyente = Item.Cells[dgTercerosColumnsEnum.TipoContribuyente.GetHashCode()].Text;
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.RegimenFiscal.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.RegimenFiscal.GetHashCode()].Text != "")
                        {
                            oTerI.RegimenFiscal = Item.Cells[dgTercerosColumnsEnum.RegimenFiscal.GetHashCode()].Text;
                        }
                        if (Item.Cells[dgTercerosColumnsEnum.ResponsabilidadFiscal.GetHashCode()].Text == "&nbsp;" && Item.Cells[dgTercerosColumnsEnum.ResponsabilidadFiscal.GetHashCode()].Text != "")
                        {
                            oTerI.ResponsabilidadFiscal = Item.Cells[dgTercerosColumnsEnum.ResponsabilidadFiscal.GetHashCode()].Text;
                        }
                        oListTerI.Add(oTerI);
                    }
                    Error = oTerB.CreacionMasivaTerceros(oListTerI);
                    if (string.IsNullOrEmpty(Error))
                    {
                        MostrarAlerta(1, "Creación de Terceros", "Los terceros se crearon con exito.");
                        LimpiarControles();
                    }
                    else
                    {
                        MostrarAlerta(0, "Error", Error);
                    }
                }
                else
                {
                    MostrarAlerta(0, "Error", "El usuario no posee permisos para esta operación.");
                }
            }
            catch (Exception ex)
            {
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmMantenimientos.aspx");
        }
        protected void dgTerceros_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    string Error = "";
                    tblTerceroBusiness oTerB = new tblTerceroBusiness(CadenaConexion);

                    //Verifica que el tipo de identificación, el nombre y el tipo tercero sean obligatorios
                    if (e.Item.Cells[dgTercerosColumnsEnum.TipoIdentificacion.GetHashCode()].Text == "" && e.Item.Cells[dgTercerosColumnsEnum.Nombre.GetHashCode()].Text == "&nbsp;" && e.Item.Cells[dgTercerosColumnsEnum.Nombre.GetHashCode()].Text == "" && e.Item.Cells[dgTercerosColumnsEnum.Nombre.GetHashCode()].Text == "&nbsp;" && e.Item.Cells[dgTercerosColumnsEnum.TipoTercero.GetHashCode()].Text == "" && e.Item.Cells[dgTercerosColumnsEnum.TipoTercero.GetHashCode()].Text == "&nbsp;")
                    {
                        Error = string.Format("{0} * Error. El Tipo Identificacion, el Nombre y el Tipo Tercero son campos obligatorios.<br> ", Error);
                    }

                    //Validar que el tercero no exista por identificación y tipo tercero
                    if (oTerB.ObtenerTerceroPorIdentificacion(e.Item.Cells[dgTercerosColumnsEnum.Identificacion.GetHashCode()].Text, oUsuarioI.idEmpresa).TipoTercero == e.Item.Cells[dgTercerosColumnsEnum.TipoTercero.GetHashCode()].Text)
                    {
                        Error = string.Format("{0} * Error. Ya existe en el sistema un tercero con la misma identificación y tipo tercero.<br> ", Error);
                    }

                    //Realiza la busqueda de la lista de precio por ID
                    if (e.Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text != "" && e.Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text != "&nbsp;" && e.Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text != "0")
                    {
                        tblListaPrecioBusiness oLisPrecioB = new tblListaPrecioBusiness(CadenaConexion);
                        tblListaPrecioItem oLisPrecioI = new tblListaPrecioItem();
                        oLisPrecioI = oLisPrecioB.ObtenerListaPrecioPorID(long.Parse(e.Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text));
                        if (oLisPrecioI.IdListaPrecio == 0)
                        {
                            Error = string.Format("{0} * Error. No se registra en el sistema una lista de precio con ese ID.<br> ", Error);
                        }
                        else
                        {
                            e.Item.Cells[dgTercerosColumnsEnum.ListaPrecio.GetHashCode()].Text = oLisPrecioI.IdListaPrecio.ToString();
                        }
                    }

                    //Realiza la busqueda del grupo por ID
                    if (e.Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text != "" && e.Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text != "&nbsp;" && e.Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text != "0")
                    {
                        tblGrupoClienteItem oGrupoI = new tblGrupoClienteItem();
                        oGrupoI = oTerB.ObtenerGrupoCliente(long.Parse(e.Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text), oUsuarioI.idEmpresa);
                        if (oGrupoI.idGrupoCliente == 0)
                        {
                            Error = string.Format("{0} * Error. No se registra en el sistema un grupo de cliente con ese ID.<br> ", Error);
                        }
                        else
                        {
                            e.Item.Cells[dgTercerosColumnsEnum.GrupoCliente.GetHashCode()].Text = oGrupoI.idGrupoCliente.ToString();
                        }
                    }

                    //Verifica si hay un error en la linea
                    if (Error != "")
                    {
                        e.Item.Cells[dgTercerosColumnsEnum.Errores.GetHashCode()].Text = Error;
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
                MostrarAlerta(0, "Error", ex.Message.Replace("'", "").Replace(Environment.NewLine, " "));
            }
        }
    }
}