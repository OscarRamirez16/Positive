<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCreacionMasivaArticulos.aspx.cs" Inherits="Inventario.frmCreacionMasivaArticulos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>Creación Masiva de Artículos</b>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-1">Delimitador:</div>
                        <div class="col-md-2"><asp:DropDownList ID="ddlDelimitador" runat="server" CssClass="form-control" ></asp:DropDownList></div>
                        <div class="col-md-3"><asp:FileUpload ID="fulArticulos" runat="server" /></div>
                        <div class="col-md-2"><asp:ImageButton ImageUrl="~/Images/btnactualizar.png" ToolTip="Cargar Archivo" runat="server" ID="btnCargar" Height="40" Width="40" CausesValidation="false" OnClick="btnCargar_Click" /></div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align:center">
                            <asp:DataGrid ID="dgArticulosMasivo" Width="100%" runat="server" AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" OnItemDataBound="dgArticulosMasivo_ItemDataBound1">
                                <Columns>
                                    <asp:BoundColumn DataField="CodigoArticulo" HeaderText="Codigo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Nombre" HeaderText="Descripcion"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Presentacion" HeaderText="Presentacion" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdLinea" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Linea" HeaderText="Linea"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IVAVenta" HeaderText="IVA Venta" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CodigoBarra" HeaderText="CodigoBarra" Visible="false" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdTercero" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Tercero" HeaderText="Tercero"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdBodega" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Bodega" HeaderText="Bodega" ItemStyle-HorizontalAlign="Right" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="EsInventario" HeaderText="EsInventario" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="StockMinimo" HeaderText="StockMinimo" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IVACompra" HeaderText="IVA Compra" Visible="false" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla Decimal"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Ubicacion" HeaderText="Ubicacion" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="EsCompuesto" HeaderText="EsCompuesto" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="EsArticuloFinal" HeaderText="EsFinal" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="EsHijo" HeaderText="EsHijo" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="IdArticuloPadre" HeaderText="IdPadre" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="CantidadPadre" HeaderText="Cant. Padre" Visible="false" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Costo" HeaderText="Costo" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Precio" HeaderText="Precio" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="BoxValorGrilla"></asp:BoundColumn>
                                    <asp:BoundColumn HeaderText="Errores"></asp:BoundColumn>
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
