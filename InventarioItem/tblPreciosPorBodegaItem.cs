using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblPreciosPorBodegaItem
    {
        public long IdBodega { get; set; }
        public long IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public short IdTipoManejoPrecio { get; set; }
        public decimal Valor { get; set; }
    }
}
