using System;

namespace InventarioItem
{
    public class tblEmpresaItem
    {
        public long idEmpresa { get; set; }
        public string Nombre { get; set; }
        public short idTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public short idCiudad { get; set; }
        public string Ciudad { get; set; }
        public string TextoEncabezadoFactura { get; set; }
        public string TextoPieFactura { get; set; }
        public bool InventarioNegativo { get; set; }
        public decimal MargenUtilidad { get; set; }
        public bool Activo { get; set; }
        public short NumeroUsuarios { get; set; }
        public string ZonaHoraria { get; set; }
        public bool FacturacionCaja { get; set; }
        public DateTime FechaInicialEntrega { get; set; }
        public bool ManejaPrecioConIVA { get; set; }
        public bool ManejaCostoConIVA { get; set; }
        public bool ManejaDescuentoConIVA { get; set; }
        public bool MultiBodega { get; set; }
        public bool MostrarRemisiones { get; set; }
        public decimal Propina { get; set; }
        public byte[] Logo { get; set; }
        public decimal Impoconsumo { get; set; }
    }
}
