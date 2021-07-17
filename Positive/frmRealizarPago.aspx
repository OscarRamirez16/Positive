<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRealizarPago.aspx.cs" Inherits="Positive.frmRealizarPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphContenido" runat="server">
    <script>
        $(document).ready(function () {
            PintarTabla("cphContenido_dgFacturas");
        });
    </script>
    <div class="panel panel-default">
        <div class="panel-heading">
            Facturas Pendientes de Pago
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <asp:DataGrid ID="dgFacturas" runat="server" Width="100%" AutoGenerateColumns="False" HeaderStyle-BackColor="#E5E5E5">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="Fecha" HeaderText="Fecha"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NumeroDocumento" HeaderText="Nro."></asp:BoundColumn>
                            <asp:BoundColumn DataField="Nombre" HeaderText="Dirección"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Telefono" HeaderText="Telefono"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Observaciones" HeaderText="Observaciones"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalDocumento" HeaderText="TotalDocumento" DataFormatString="{0:C}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="saldo" HeaderText="Saldo" DataFormatString="{0:C}"></asp:BoundColumn>
                            <asp:ButtonColumn ItemStyle-HorizontalAlign="Center" CommandName="Pagar" Text="<img src='Images/Input/DolarAmarillo.png' Width='18' Height='18' title='Realizar Pago' />"></asp:ButtonColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
    <!--PayPal
    <div id="smart-button-container">
    <div style="text-align: center"><label for="description">Descripción </label><input type="text" name="descriptionInput" id="description" maxlength="127" value="" class="form-control"></div>
      <p id="descriptionError" style="visibility: hidden; color:red; text-align: center;">Please enter a description</p>
    <div style="text-align: center"><label for="amount">Valor Pago </label><input name="amountInput" type="number" id="amount" value="" class="form-control"><span> USD</span></div>
      <p id="priceLabelError" style="visibility: hidden; color:red; text-align: center;">Please enter a price</p>
    <div id="invoiceidDiv" style="text-align: center; display: none;"><label for="invoiceid">Observaciones </label><input name="invoiceid" maxlength="127" type="text" id="invoiceid" value="" class="form-control"></div>
      <p id="invoiceidError" style="visibility: hidden; color:red; text-align: center;">Please enter an Invoice ID</p>
    <div style="text-align: center; margin-top: 0.625rem;" id="paypal-button-container"></div>
  </div>
  <script src="https://www.paypal.com/sdk/js?client-id=sb&currency=USD" data-sdk-integration-source="button-factory"></script>
  <script>
      function initPayPalButton() {
          var description = document.querySelector('#smart-button-container #description');
          var amount = document.querySelector('#smart-button-container #amount');
          var descriptionError = document.querySelector('#smart-button-container #descriptionError');
          var priceError = document.querySelector('#smart-button-container #priceLabelError');
          var invoiceid = document.querySelector('#smart-button-container #invoiceid');
          var invoiceidError = document.querySelector('#smart-button-container #invoiceidError');
          var invoiceidDiv = document.querySelector('#smart-button-container #invoiceidDiv');

          var elArr = [description, amount];

          if (invoiceidDiv.firstChild.innerHTML.length > 1) {
              invoiceidDiv.style.display = "block";
          }

          var purchase_units = [];
          purchase_units[0] = {};
          purchase_units[0].amount = {};

          function validate(event) {
              return event.value.length > 0;
          }

          paypal.Buttons({
              style: {
                  color: 'gold',
                  shape: 'rect',
                  label: 'pay',
                  layout: 'vertical',

              },

              onInit: function (data, actions) {
                  actions.disable();

                  if (invoiceidDiv.style.display === "block") {
                      elArr.push(invoiceid);
                  }

                  elArr.forEach(function (item) {
                      item.addEventListener('keyup', function (event) {
                          var result = elArr.every(validate);
                          if (result) {
                              actions.enable();
                          } else {
                              actions.disable();
                          }
                      });
                  });
              },

              onClick: function () {
                  if (description.value.length < 1) {
                      descriptionError.style.visibility = "visible";
                  } else {
                      descriptionError.style.visibility = "hidden";
                  }

                  if (amount.value.length < 1) {
                      priceError.style.visibility = "visible";
                  } else {
                      priceError.style.visibility = "hidden";
                  }

                  if (invoiceid.value.length < 1 && invoiceidDiv.style.display === "block") {
                      invoiceidError.style.visibility = "visible";
                  } else {
                      invoiceidError.style.visibility = "hidden";
                  }

                  purchase_units[0].description = description.value;
                  purchase_units[0].amount.value = amount.value;

                  if (invoiceid.value !== '') {
                      purchase_units[0].invoice_id = invoiceid.value;
                  }
              },

              createOrder: function (data, actions) {
                  return actions.order.create({
                      purchase_units: purchase_units,
                  });
              },

              onApprove: function (data, actions) {
                  return actions.order.capture().then(function (details) {
                      alert('Transaction completed by ' + details.payer.name.given_name + '!');
                  });
              },

              onError: function (err) {
                  console.log(err);
              }
          }).render('#paypal-button-container');
      }
      initPayPalButton();
  </script>-->
</asp:Content>