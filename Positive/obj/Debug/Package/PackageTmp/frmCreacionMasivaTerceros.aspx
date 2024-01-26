<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCreacionMasivaTerceros.aspx.cs" Inherits="Positive.frmCreacionMasivaTerceros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Creación Masiva de Terceros
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2">
                            Delimitador:
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="ddlDelimitador" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-lg-5">
                            <asp:FileUpload ID="fulTerceros" runat="server" />
                        </div>
                        <div class="col-lg-2">
                            <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Cargar Archivo" runat="server" ID="btnCargar" Height="40" Width="40" CausesValidation="false" OnClick="btnCargar_Click" />
                        </div>
                    </div>
                    <div class="row" id="divGrilla" runat="server">
                        <div class="col-lg-12" style="text-align: center">
                            <asp:DataGrid ID="dgTerceros" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnItemDataBound="dgTerceros_ItemDataBound">
                                <Columns>
                                    <asp:BoundColumn DataField="idTipoIdentificacion" HeaderText="Tipo ID"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Identificacion" HeaderText="Identificacion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Telefono" HeaderText="Telefono"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Celular" HeaderText="Celular"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Mail" HeaderText="Correo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Direccion" HeaderText="Direccion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idCiudad" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TipoTercero" HeaderText="Tipo Tercero"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FechaNacimiento" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdListaPrecio" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="idGrupoCliente" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Observaciones" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TipoIdentificacionDIAN" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MatriculaMercantil" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TipoContribuyente" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="RegimenFiscal" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ResponsabilidadFiscal" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CodigoZip" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Errores"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="row" id="divBotones" runat="server">
                        <div class="col-lg-12" style="text-align: center">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" OnClientClick="EsconderBoton();" runat="server" ID="btnGuardar" Height="40" Width="40" CausesValidation="false" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
