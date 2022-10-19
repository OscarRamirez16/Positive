<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmArticulos.aspx.cs" Inherits="Inventario.frmArticulos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager runat="server" ID="smArticulos"></asp:ScriptManager>
    <div class="page-header">
        <h1>Mantenimiento de Artículos: <small>En este modulo puedes crear o editar la información de tus artículos.</small></h1>
    </div>
    <div class="row">
        <div class="col-md-1"><b>Descripción:</b></div>
        <div class="col-md-5">
            <div class="input-group">
                <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control"></asp:TextBox>
                <input type="hidden" id="hddIdEmpresa" runat="server" />
                <span class="input-group-addon">
                <img src="Images/Input/Nombre.png" title="Dirección" /></span>
            </div>
        </div>
        <div class="col-md-6">
            <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar Artículo" Height="40" Width="40" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click" />
            <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
            <asp:ImageButton ID="btnCrear" runat="server" ToolTip="Crear Artículo" Height="40" Width="40" ImageUrl="~/Images/btncrear.png" OnClick="btnCrear_Click" />
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
                        AutoGenerateColumns="False" DataKeyNames="IdArticulo" AllowPaging="True"
                        OnPageIndexChanging="gvBodegas_PageIndexChanging"
                        OnRowCommand="gvDatos_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="IdArticulo" Visible="false" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="CodigoArticulo" HeaderText="Código"/>
                            <asp:BoundField DataField="Nombre" HeaderText="Descripción"/>
                            <asp:BoundField DataField="Presentacion" HeaderText="Presentación"/>
                            <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación"/>
                            <asp:BoundField DataField="Activo" HeaderText="Activo"/>
                            <asp:ButtonField Text="<img src='Images/btneditar.png' Width='18' Height='18' title='Editar datos principales' />" CommandName="Editar" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField Text="<img src='Images/btneditar.png' Width='18' Height='18' title='Configuración bodegas' />" CommandName="Bodegas" ItemStyle-HorizontalAlign="Center" />
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
                 <div class="modal-dialog" style="width:65%">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h4 class="modal-title"><b>Crear o Editar Artículo</b></h4><br />
                             <asp:Label ID="lblErrores" Font-Size="XX-Small" ForeColor="Red" runat="server"></asp:Label>
                         </div>
                         <div class="modal-body">
                             <div class="row">
                                 <div class="col-md-3"><label for="lblID" class="control-label">ID Artículo:</label></div>
                                 <div class="col-md-9">
                                     <asp:Label ID="lblID" runat="server"></asp:Label>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="txtNombre" class="control-label">Nombre:</label></div>
                                 <div class="col-md-9">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtNombre" Width="100%" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon"></span>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="txtCodigo" class="control-label">Codigo:</label></div>
                                 <div class="col-md-3">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3"><label for="txtPresentacion" class="control-label">Presentación:</label></div>
                                 <div class="col-md-3">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtPresentacion" runat="server" CssClass="form-control"></asp:TextBox>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="ddlGrupo" class="control-label">Grupo:</label></div>
                                 <div class="col-md-9">
                                     <div class="input-group">
                                         <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="form-control" AutoPostBack="false" DataTextField="Nombre" DataValueField="idLinea"></asp:DropDownList>
                                         <span class="input-group-addon"></span>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="txtIVAVenta" class="control-label">IVA Venta:</label></div>
                                 <div class="col-md-3">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtIVAVenta" runat="server" CssClass="form-control Entero BoxValor"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3"><label for="txtIVACompra" class="control-label">IVA Compra:</label></div>
                                 <div class="col-md-3">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtIVACompra" runat="server" CssClass="form-control Entero BoxValor"></asp:TextBox>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="txtCodigoBarra" class="control-label">Codigo Barras:</label></div>
                                 <div class="col-md-9">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtCodigoBarra" runat="server" CssClass="form-control"></asp:TextBox>
                                         <span class="input-group-addon"></span>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="txtTercero" class="control-label">Proveedor:</label></div>
                                 <div class="col-md-9">
                                     <div class="input-group">
                                         <asp:DropDownList ID="ddlTercero" runat="server" CssClass="form-control" AutoPostBack="false" DataTextField="Nombre" DataValueField="idTercero"></asp:DropDownList>
                                         <span class="input-group-addon"></span>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="ddlBodega" class="control-label">Bodega:</label></div>
                                 <div class="col-md-9">
                                     <div class="input-group">
                                         <asp:DropDownList ID="ddlBodega" runat="server" CssClass="form-control" AutoPostBack="false" DataTextField="Descripcion" DataValueField="IdBodega"></asp:DropDownList>
                                         <span class="input-group-addon"></span>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="txtStockMinimo" class="control-label">Stock Minimo:</label></div>
                                 <div class="col-md-3">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control Decimal BoxValor"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="col-md-3"><label for="txtCostoPonderado" class="control-label">Costo Pond:</label></div>
                                 <div class="col-md-3">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtCostoPonderado" runat="server" CssClass="form-control Decimal BoxValor"></asp:TextBox>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="txtUbicacion" class="control-label">Ubicación:</label></div>
                                 <div class="col-md-9">
                                     <div class="input-group">
                                         <asp:TextBox ID="txtUbicacion" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                         <span class="input-group-addon"></span>
                                     </div>
                                 </div>
                             </div>
                             <div class="row">
                                 <div class="col-md-3"><label for="chkActivo" class="control-label">Activo:</label></div>
                                 <div class="col-md-3">
                                     <asp:CheckBox ID="chkActivo" runat="server" />
                                 </div>
                                 <div class="col-md-3"><label for="chkEsInventario" class="control-label">Es Inventario:</label></div>
                                 <div class="col-md-3">
                                     <asp:CheckBox ID="chkEsInventario" runat="server" />
                                 </div>
                             </div>
                         </div>
                         <div class="modal-footer">
                             <div class="row">
                                 <div class="col-md-12">
                                     <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Buscar" Height="40" Width="40" ImageUrl="~/Images/btnguardar.png" OnClick="btnGuardar_Click" />
                                     <asp:ImageButton ID="btnCerrar" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCerrar_Click" />
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
             </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:ModalPopupExtender ID="mpeBodega" runat="server" 
        PopupControlID="panelBodega" TargetControlID="HiddenField2"
         BackgroundCssClass="backgroundColor" >
    </asp:ModalPopupExtender>
    <asp:Panel ID="panelBodega" runat="server" BackColor="White">
         <asp:UpdatePanel ID="upBodega" runat="server">
             <ContentTemplate>
                 <div class="modal-dialog" style="width:100%">
                     <div class="modal-content">
                         <div class="modal-header">
                             <h4 class="modal-title"><b>Configuración de bodegas</b></h4><br />
                             <asp:Label ID="lblErroresBodegas" Font-Size="XX-Small" ForeColor="Red" runat="server"></asp:Label>
                             <input type="hidden" runat="server" id="hddIdArticulo" />
                         </div>
                         <div class="modal-body">
                             <div class="row">
                                 <div class="col-md-12 table-responsive">
                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                         </Triggers>
                                         <ContentTemplate>
                                             <asp:GridView ID="gvBodegas" runat="server" CssClass="table table-hover"
                                                 AutoGenerateColumns="False" DataKeyNames="idBodega" AllowPaging="True"
                                                 OnPageIndexChanging="gvBodegas_PageIndexChanging"
                                                 OnRowCommand="gvDatos_RowCommand" OnRowUpdating="gvBodegas_RowUpdating" 
                                                 OnRowCancelingEdit="gvBodegas_RowCancelingEdit" OnRowEditing="gvBodegas_RowEditing"
                                                 OnRowDeleting="gvBodegas_RowDeleting">
                                                 <Columns>
                                                     <asp:BoundField DataField="idBodega" HeaderText="ID" ReadOnly="true" ItemStyle-HorizontalAlign="Center" >
                                                     <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ReadOnly="true" />
                                                     <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right" ReadOnly="true" >
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Costo" HeaderText="Costo" ReadOnly="false" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" >
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     </asp:BoundField>
                                                     <asp:BoundField DataField="Precio" HeaderText="Precio" ReadOnly="false" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" >
                                                     <ItemStyle HorizontalAlign="Right" />
                                                     </asp:BoundField>
                                                     <asp:CommandField ShowEditButton="True" />
                                                     <asp:CommandField ShowDeleteButton="True" />
                                                 </Columns>
                                             </asp:GridView>
                                         </ContentTemplate>
                                     </asp:UpdatePanel>
                                 </div>
                             </div>
                         </div>
                         <div class="modal-footer">
                             <div class="row">
                                 <div class="col-md-12">
                                     <asp:ImageButton ID="btnCancelarBodega" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelarBodega_Click" />
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
             </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
