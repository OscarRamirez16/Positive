using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblNumeracionFacturaVentaItem
    {

        public tblNumeracionFacturaVentaItem()
        {
            idNumeracionFacturaVenta = 0;
        }

        public long idNumeracionFacturaVenta { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public string ProximoValor { get; set; }
        public long idEmpresa { get; set; }
    }
}
