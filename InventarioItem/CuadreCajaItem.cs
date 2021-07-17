using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class CuadreCajaItem
    {
        public decimal Total { get; set; }
        public decimal Devoluciones { get; set; }
        public short idFormaPago { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
    }
}
