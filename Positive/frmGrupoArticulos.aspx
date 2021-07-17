<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmGrupoArticulos.aspx.cs" Inherits="Inventario.frmGrupoArticulos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager runat="server" ID="smArticulos"></asp:ScriptManager>
    <div class="page-header">
        <h1>Grupos de Artículos: <small>En este modulo puedes crear o editar la información de tus grupos de artículos.</small></h1>
    </div>
    <div class="row">
        <div class="col-md-1"><b>Descripción:</b></div>
        <div class="col-md-5">
            <div class="input-group">
                <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control"></asp:TextBox>
                <span class="input-group-addon">
                <img src="Images/Input/Nombre.png" title="Descripción" /></span>
            </div>
        </div>
        <div class="col-md-6">
            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar Grupo" Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
            <asp:ImageButton ID="btnCrear" runat="server" ToolTip="Crear Grupo" Height="40" Width="40" ImageUrl="~/Images/btncrear.png" OnClick="btnCrear_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:UpdatePanel ID="upDatos" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:GridView ID="gvDatos" runat="server" CssClass="table table-hover"
                        AutoGenerateColumns="False" DataKeyNames="idLinea" AllowPaging="True"
                        OnPageIndexChanging="gvDatos_PageIndexChanging"
                        OnSelectedIndexChanging="gvDatos_SelectedIndexChanging" >
                        <Columns>
                            <asp:BoundField DataField="idLinea" HeaderText="ID Grupo" ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="Nombre" HeaderText="Descripcion"/>
                            <asp:BoundField DataField="Activo" HeaderText="Activo"/>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/btneditar.png"
                                         CommandName="Select" Height="20" Width="20" ToolTip="Editar"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ModalPopupExtender ID="mpeEdit" runat="server" 
        PopupControlID="panelEdit" TargetControlID="HiddenField1"
         BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelEdit" runat="server" BackColor="White">
         <asp:UpdatePanel ID="upEdit" runat="server">
             <ContentTemplate>
                 <div class="modal-dialog modal-lg">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h4 class="modal-title"><b>Crear o Editar Grupo de Artículo</b></h4>
                         </div>
                         <div class="modal-body">
                             <div class="row">
                                 <div class="col-md-3">ID Grupo:</div>
                                 <div class="col-md-9">
                                     <asp:Label ID="lblID" runat="server"></asp:Label>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3">Descripción:</div>
                                 <div class="col-md-9">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                         <span class="input-group-addon">
                                             <img src="Images/Input/Nombre.png" title="Descripción" /></span>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3">Activo:</div>
                                 <div class="col-md-9">
                                     <asp:CheckBox ID="chkActivo" runat="server" />
                                 </div>
                             </div>
                         </div>
                         <div class="modal-footer">
                             <div class="row">
                                 <div class="col-md-12">
                                     <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Guardar" Height="40" Width="40" ImageUrl="~/Images/btnguardar.png" OnClick="btnGuardar_Click" />
                                     <asp:ImageButton ID="btnCerrar" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCerrar_Click" />
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
             </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>