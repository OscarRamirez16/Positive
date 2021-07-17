<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCrearEditarConsultarVentaRapida.aspx.cs" Inherits="Inventario.frmCrearEditarConsultarVentaRapida" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgVentaRapida");
        });
    </script>
    <div id="contenido" style="width: 100%">
        <ul class="tab">
            <li id="pestana1"><a id="aVentaRapida" href='javascript:void(0)' class="tablinks active" onclick="ShowTab('aVentaRapida', 'divVentaRapida')">
                <asp:Label ID="liVentaRapida" runat="server"></asp:Label></a></li>
            <li id="pestana2"><a id="aBuscarVentaRapida" href='javascript:void(0)' class="tablinks" onclick="ShowTab('aBuscarVentaRapida', 'divBuscarVentaRapida')">
                    <asp:Label ID="liBuscarVentaRapida" runat="server"></asp:Label></a></li>
        </ul>
        <div id="divVentaRapida" class="tabcontent" style="display:block">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b><asp:Label ID="lblTitulo" runat="server"></asp:Label></b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-2"><asp:Label ID="lblidVentaRapidaLabel" class="label label-warning" ForeColor="White"  runat="server"></asp:Label></div>
                                        <div class="col-md-10"><asp:Label ID="lblidVentaRapida" ForeColor="White" class="label label-info" runat="server">0</asp:Label></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblArticulo" runat="server"></asp:Label></b></div>
                                        <div class="col-md-10">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtArticulo" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/Articulo.png" title="Articulo" /></span>
                                                <input type="hidden" runat="server" id="hddIdArticulo" value="0" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblNombre" runat="server"></asp:Label></b></div>
                                        <div class="col-md-10">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/Nombre.png" title="Nombre" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblCantidad" runat="server"></asp:Label></b></div>
                                        <div class="col-md-10">
                                            <div class = "input-group">
                                                <asp:TextBox ID="txtCantidad" CssClass="form-control" runat="server"></asp:TextBox>
                                                <span class = "input-group-addon"><img src="Images/Input/Cantidad.png" title="Cantidad" /></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2"><b><asp:Label ID="lblActivo" runat="server"></asp:Label></b></div>
                                        <div class="col-md-10"><asp:CheckBox ID="chkActivo" runat="server" /></div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="lblImagen" runat="server" ForeColor="White" Visible="false" class="label label-success" ></asp:Label>
                                            <label for="<%=fluImagen.ClientID %>" class="label label-info">Seleccionar Imagen</label>
                                            <input id="fluImagen" runat="server" style="display:none !important" type="file" accept="image/*" onchange="loadFile(event)" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <img id="imgOutput" class="Output" src="Images/Form/Imagen.png" height="128" runat="server" width="128"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row"><div class="col-md-12"><br /></div></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:ImageButton ImageUrl="~/Images/Documento/Guardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click"/>
                                    <asp:ImageButton ImageUrl="~/Images/Documento/Cancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" CausesValidation="false" OnClick="btnCancelar_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divBuscarVentaRapida" class="tabcontent">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <b><asp:Label ID="lblTituloBusqueda" runat="server"></asp:Label></b>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-2"><b><asp:Label ID="lblArticuloBusqueda" runat="server"></asp:Label></b></div>
                                <div class="col-md-4">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtArticuloBusqeuda" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/Articulo.png" title="Articulo" /></span>
                                        <input type="hidden" runat="server" id="hddIdArticuloBusqueda" value="0" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"><b><asp:Label ID="lblNombreBusqueda" runat="server"></asp:Label></b></div>
                                <div class="col-md-4">
                                    <div class = "input-group">
                                        <asp:TextBox ID="txtNombreBusqueda" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class = "input-group-addon"><img src="Images/Input/Nombre.png" title="Nombre" /></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:ImageButton ImageUrl="~/Images/Documento/Buscar.png" ToolTip="Buscar" runat="server" ID="btnBuscar" OnClick="btnBuscar_Click"/>
                                    <asp:ImageButton ImageUrl="~/Images/Documento/Cancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelarBusqueda" CausesValidation="false" OnClick="btnCancelarBusqueda_Click"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            <b>Articulos Venta Rapida</b>
                                        </div>
                                        <div class="panel-body">
                                            <asp:DataGrid ID="dgVentaRapida" runat="server" Width="100%" AutoGenerateColumns="false" OnItemCommand="dgVentaRapida_ItemCommand">
                                                <Columns>
                                                    <asp:BoundColumn DataField="idVentaRapida" HeaderText="Codigo"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="idArticulo" HeaderText="Articulo"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Articulo" HeaderText="Descripción"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Linea" HeaderText="Grupo de Articulos"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Cantidad" HeaderText="Cantidad"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Activo" HeaderText="Activo"></asp:BoundColumn>
                                                    <asp:ButtonColumn ButtonType="LinkButton" CausesValidation="false" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        //$("#output").hide();
      var loadFile = function(event) {
        var output = document.getElementById('<% = imgOutput.ClientID%>');
        output.src = URL.createObjectURL(event.target.files[0]);
        //$("#output").show();
      };
    </script>
</asp:Content>
