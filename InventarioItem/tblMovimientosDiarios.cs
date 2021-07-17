using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblMovimientosDiariosItem
    {
        public long idMovimiento { get; set; }
        public DateTime fechaMovimiento { get; set; }
        public decimal valor { get; set; }
        public string observaciones { get; set; }
        public short idTipoMovimiento { get; set; }
        public string TipoMovimiento { get; set; }
        public long idUsuario { get; set; }
        public string Usuario { get; set; }
        public long idEmpresa { get; set; }
        public bool EnCuadre { get; set; }
    }
}
