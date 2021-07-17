using System;

namespace InventarioItem
{
    public class tblCuadreCajaItem
    {
        public long idCuadreCaja { get; set; }
        public DateTime Fecha { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal TotalRetiros { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal Efectivo { get; set; }
        public decimal TarjetaCredito { get; set; }
        public decimal TarjetaDebito { get; set; }
        public decimal Cheques { get; set; }
        public decimal Bonos { get; set; }
        public decimal Consignaciones { get; set; }
        public decimal DescuentosNomina { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalCompras { get; set; }
        public decimal TotalCuadre { get; set; }
        public long idUsuario { get; set; }
        public string Usuario { get; set; }
        public long idEmpresa { get; set; }
        public string Observaciones { get; set; }
        public long idCaja { get; set; }
        public string Caja { get; set; }
        public long idUsuarioCaja { get; set; }
        public string UsuarioCaja { get; set; }
        public long idUsuarioCierre { get; set; }
        public string UsuarioCierre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal TotalRemisiones { get; set; }
        public decimal TotalCreditos { get; set; }
        public decimal PagoCreditos { get; set; }
    }
}
