using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblDocumentoItem
    {

        public tblDocumentoItem()
        {
            idDocumento = 0;
        }

        public enum AccionEnum
        {
            Insertar = 1,
            Actualizar = 2,
            Eliminar = 3
        }

        public long idDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public long idTercero { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public short idCiudad { get; set; }
        public string NombreTercero { get; set; }
        public string DatosTercero { get; set; }
        public string Observaciones { get; set; }
        public long idEmpresa { get; set; }
        public long idUsuario { get; set; }
        public decimal TotalDocumento { get; set; }
        public decimal TotalIVA { get; set; }
        public decimal saldo { get; set; }
        public long IdEstado { get; set; }
        public string Estado { get; set; }
        public List<tblDetalleDocumentoItem> DocumentoLineas { get; set; }
        public tblTipoDocumentoItem.TipoDocumentoEnum TipoDocumento { get; set; }
        public List<tblTipoPagoItem> FormasPago { get; set; }
        public int IdTipoDocumento { get; set; }
        public bool EnCuadre { get; set; }
        public decimal Devuelta { get; set; }
        public long IdVendedor { get; set; }
        public decimal TotalDescuento { get; set; }
        public decimal TotalAntesIVA { get; set; }
        public string Referencia { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public short BaseIdTipoDocumento { get; set; }
        public decimal Propina { get; set; }
        public string Resolucion { get; set; }
        public decimal Impoconsumo { get; set; }
        public decimal TotalRetenciones { get; set; }
        public List<tblDocumentoRetencionItem> Retenciones { get; set; }
        public string ZipKey { get; set; }
    }
}