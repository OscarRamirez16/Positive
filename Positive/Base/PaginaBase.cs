using System.Web.UI;

namespace HQSFramework.Base
{
    public class PaginaBase : System.Web.UI.Page
    {
        public void MostrarMensaje(string Titulo, string Mensaje) {
            if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarMensaje"))
            {
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarMensaje", string.Format("$(document).ready(function(){{MostrarAlerta('{0}', '{1}', '80%');}});", Titulo, Mensaje), true);
            }
        }

        public void MostrarAlerta(int Tipo, string Titulo, string Mensaje)
        {
            if(Tipo == 0)
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarMensaje"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarMensaje", string.Format("$(document).ready(function(){{MostrarAlertaError('{0}', '{1}');}});", Titulo, Mensaje), true);
                }
            }
            else if (Tipo == 1)
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarMensaje"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarMensaje", string.Format("$(document).ready(function(){{MostrarAlertaSuccess('{0}', '{1}');}});", Titulo, Mensaje), true);
                }
            }
            else
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("scriptRegistrarMensaje"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "scriptRegistrarMensaje", string.Format("$(document).ready(function(){{MostrarAlertaInfo('{0}');}});", Titulo), true);
                }
            }
        }

        public void SeleccionarTab(string IdTab,int Index)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("SeleccionarTabScript"))
            {
                string strJS = string.Format("$(document).ready(function(){{seleccionarTab('{0}',{1});}});", IdTab, Index);
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "SeleccionarTabScript", strJS, true);
            }
        }
        public void ShowTab(string IdTab, string ContentName)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("SeleccionarTabScript"))
            {
                string strJS = string.Format("$(document).ready(function(){{ShowTab('{0}','{1}');}});", IdTab, ContentName);
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "SeleccionarTabScript", strJS, true);
            }
        }
        public void OcultarControlClass(string className)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(string.Format("OcultarControlClassScript_{0}", className)))
            {
                string strJS = string.Format("$(document).ready(function(){{OcultarControlClass('{0}');}});", className);
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), string.Format("OcultarControlClassScript_{0}", className), strJS, true);
            }
        }
        public void MostrarControlClass(string className)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(string.Format("MostrarControlClassScript_{0}", className)))
            {
                string strJS = string.Format("$(document).ready(function(){{MostrarControlClass('{0}');}});", className);
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), string.Format("MostrarControlClassScript_{0}", className), strJS, true);
            }
        }

        public void OcultarControl(string IdControl)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(string.Format("{0}ScriptOcultarControl", IdControl)))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), string.Format("{0}ScriptOcultarControl", IdControl), string.Format("$(document).ready(function(){{$('#{0}').hide();}});", IdControl), true);
            }
        }

        public void MostrarControl(string IdControl)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(string.Format("{0}ScriptMostrarControl", IdControl)))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), string.Format("{0}ScriptMostrarControl", IdControl), string.Format("$(document).ready(function(){{$('#{0}').show();}});", IdControl), true);
            }
        }
    }
}