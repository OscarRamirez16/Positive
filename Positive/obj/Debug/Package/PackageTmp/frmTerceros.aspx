<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTerceros.aspx.cs" Inherits="Positive.frmTerceros" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Creación Masiva de Terceros</b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-1">Delimitador:</div>
                        <div class="col-md-2"><asp:DropDownList ID="ddlDelimitador" runat="server" CssClass="form-control" ></asp:DropDownList></div>
                        <div class="col-md-3"><asp:FileUpload ID="fulTerceros" runat="server" /></div>
                        <div class="col-md-2"><asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Cargar Archivo" runat="server" ID="btnCargar" Height="40" Width="40" CausesValidation="false" OnClick="btnCargar_Click" /></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align:center">
                            <asp:DataGrid ID="dgTerceros" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnItemDataBound="dgTerceros_ItemDataBound">
                                <Columns>
                                    <asp:BoundColumn DataField="IdTercero" HeaderText="Id Tercero"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TipoTercero" HeaderText="Tipo Tercero"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idTipoIdentificacion" Visible="false" HeaderText="TipoIdentificacion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idTipoIdentificacion" HeaderText="Tipo Identificacion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Identificacion" HeaderText="Identificación"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idGrupoCliente" HeaderText="IdGrupo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idGrupoCliente" HeaderText="Grupo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Direccion" HeaderText="Dirección" ></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Telefono" HeaderText="Telefono"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Mail" HeaderText="Mail"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idCiudad" HeaderText="IdCiudad" Visible="false" ></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idCiudad" HeaderText="Ciudad"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idCiudad" HeaderText="Nuevo Tercero"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idCiudad" HeaderText="Errores"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align:center">
                            <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Buscar" Height="40" Width="40" ImageUrl="~/Images/btnguardar.png" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ID="btnCancelar" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
