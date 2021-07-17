using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblMovimientosPorDocumentoItem
    {

        public tblMovimientosPorDocumentoItem()
        {
            idDocumento = 0;
        }

        public long idDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get; set; }
        public decimal TotalIVA { get; set; }
        public decimal TotalDocumento { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal ValorTotalLinea { get; set; }
        public decimal Costo { get; set; }
        public decimal Ganancia { get; set; }

    }
}
