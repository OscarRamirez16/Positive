<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRetiros.aspx.cs" Inherits="Inventario.frmRetiros" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <script type="text/javascript">
        EstablecerMascaras();
    </script>
    <div class="row"><div class="col-lg-12"><br /><br /></div></div>
    <div class="row" runat="server" id="tblPrincipal">
        <div class="col-lg-6 col-lg-offset-3">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                <input type="hidden" runat="server" id="hddIdEmpresa" value="0" />
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblUsuario" ForeColor="White" CssClass="label label-warning" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="form-control" DataTextField="NombreCompleto" DataValueField="idUsuario"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblValor" ForeColor="White" CssClass="label label-warning" runat="server"></asp:Label>
                            <asp:TextBox ID="txtValor" CssClass="Decimal Obligatorio form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblObservaciones" ForeColor="White" CssClass="label label-warning" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <textarea id="txtObser" runat="server" class="form-control" rows="5"></textarea>
                        </div>
                    </div>
                    <div class="row"><div class="col-lg-12"><br /></div></div>
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" OnClientClick="return validarObligatorio();" Height="40" Width="40" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
