using System;

namespace InventarioItem
{
    public class tblCuentaCobroItem
    {
        public long Id { get; set; }
        public long Numero { get; set; }
        public DateTime Fecha { get; set; }
        public long IdTercero { get; set; }
        public long IdUsuario { get; set; }
        public string Concepto { get; set; }
        public decimal Total { get; set; }
        public short IdEstado { get; set; }
        public long IdEmpresa { get; set; }
        public decimal Saldo { get; set; }
    }
}
