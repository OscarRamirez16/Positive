<%@ Page Title="Tarjetas de Credito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmTipoTarjetaCredito.aspx.cs" Inherits="Inventario.frmTipoTarjetaCredito" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div id="contenido" style="width: 100%">
        <ul id="lista">
            <li id="pestana1"><a href='#TipoTarjetaCredito'>
                <asp:Label ID="liTipoTarjetaCredito" runat="server"></asp:Label></a></li>
            <li id="pestana2"><a href='#BuscarTipoTarjetaCredito'>
                    <asp:Label ID="liBuscarTipoTarjetaCredito" runat="server"></asp:Label></a></li>
        </ul>
        <div id="TipoTarjetaCredito">
            <table style="width:100%">
                <tr style="text-align:center" class="tr">
                    <td colspan="2"><b><asp:Label ID="lblTitulo" ForeColor="White" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblIdTipoTarjetaCredito" for="<% =txtIdTipoTarjetaCredito.ClientID %>" runat="server">Codigo:</label></td>
                    <td><asp:TextBox ID="txtIdTipoTarjetaCredito" runat="server" Width="10%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblNombre" for="<% =txtNombre.ClientID %>" runat="server">Nombre:</label></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" Width="35%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblCuentaContable" for="<% =txtCuentaContable.ClientID %>" runat="server">Cuenta Contable:</label></td>
                    <td><asp:TextBox ID="txtCuentaContable" runat="server" Width="35%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblActivo" for="<% =chkActivo.ClientID %>" runat="server">Activo:</label></td>
                    <td><asp:CheckBox ID="chkActivo" runat="server" Checked="true" /></td>
                </tr>
                <tr>
                    <td style="width:20%"><label id="lblDescripcion" for="<% =txtDescripcion.ClientID %>" runat="server">Descripción:</label></td>
                    <td><asp:TextBox ID="txtDescripcion" TextMode="MultiLine" Rows="4" runat="server" Width="35%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ImageUrl="~/Images/btnguardar.png" ToolTip="Guardar" runat="server" ID="btnGuardar" Height="40" Width="40" OnClick="btnGuardar_Click"/>
                        <asp:ImageButton ImageUrl="~/Images/btncancelar.png" ToolTip="Cancelar" runat="server" ID="btnCancelar" Height="40" Width="40" CausesValidation="false" OnClick="btnCancelar_Click"/>
                    </td>
                </tr>
            </table>
        </div>
        <div id="BuscarTipoTarjetaCredito">
            <table style="width:100%">
                <tr style="text-align:center">
                    <td colspan="2"><h2><asp:Label ID="lblTituloBuscar" runat="server"></asp:Label></h2></td>
                </tr>
                <tr>
                    <td style="width:20%"><asp:Label ID="lblTexto" runat="server"></asp:Label></td>
                    <td><asp:TextBox ID="txtTexto" Width="35%" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" Height="40" Width="40" CausesValidation="false" ImageUrl="~/Images/btnbuscar.png" OnClick="btnBuscar_Click"/>
                        <asp:ImageButton ID="btnCancelarBus" runat="server" ToolTip="Cancelar" Height="40" Width="40" ImageUrl="~/Images/btncancelar.png" OnClick="btnCancelarBus_Click"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DataGrid ID="dgTipoTarjetaCredito" runat="server" BorderColor="#70B52B" Width="100%" HeaderStyle-BackColor="#E5E5E5" AlternatingItemStyle-BackColor="#F8F8F8" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" OnItemCommand="dgTipoTarjetaCredito_ItemCommand">
                            <Columns>
                                <asp:BoundColumn DataField="IdTipoTarjetaCredito" HeaderText="Codigo"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Nombre"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CuentaContable" HeaderText="Cuenta Contable"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Activo" HeaderText="Activo"></asp:BoundColumn>
                                <asp:ButtonColumn ButtonType="LinkButton" CausesValidation="false" Text="<img src='Images/editar.jpg' Width='18' Height='18' title='Editar' />"></asp:ButtonColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
