using System;

namespace InventarioItem
{
    public class tblPagoItem
    {

        public tblPagoItem()
        {
            idPago = -1;
        }

        public long idPago { get; set; }
        public long idTercero { get; set; }
        public string Tercero { get; set; }
        public decimal totalPago { get; set; }
        public long idEmpresa { get; set; }
        public DateTime fechaPago { get; set; }
        public long idUsuario { get; set; }
        public short idEstado { get; set; }
        public string Observaciones { get; set; }
        public bool EnCuadre { get; set; }
        public short IdTipoPago { get; set; }
        public decimal Saldo { get; set; }
    }
}
