<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmArticuloActualizarMasivo.aspx.cs" Inherits="Inventario.frmArticuloActualizarMasivo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <br />
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Actualización Masiva de Campos Artículos
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-1">
                            Delimitador:
                        </div>
                        <div class="col-lg-3">
                            <asp:DropDownList ID="ddlDelimitador" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-lg-4">
                            <asp:FileUpload ID="fulArticulos" runat="server" />
                        </div>
                        <div class="col-lg-2">
                            <asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Cargar Archivo" runat="server" ID="btnCargar" Height="40" Width="40" CausesValidation="false" OnClick="btnCargar_Click" />
                        </div>
                    </div>
                    <div class="row" id="divGrilla" runat="server">
                        <div class="col-lg-12" style="text-align: center">
                            <asp:DataGrid ID="dgArticulosMasivo" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8">
                                <Columns>
                                    <asp:BoundColumn DataField="IdArticulo" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CodigoArticulo" HeaderText="Código"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre" HeaderText="Descripción"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdBodega" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Bodega" HeaderText="Bodega"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Precio" HeaderText="Precio" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Costo" HeaderText="Costo" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Marca" HeaderText="Marca"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Ubicacion" HeaderText="Ubicacion"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div class="row" id="divBotones" runat="server">
                        <div class="col-lg-12" style="text-align: center">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardarMasivo" Height="40" Width="40" CausesValidation="false" OnClick="btnGuardarMasivo_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelarMasivo" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelarMasivo_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
