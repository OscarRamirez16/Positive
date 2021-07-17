using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblTipoTarjetaCreditoItem
    {
        public short idTipoTarjetaCredito { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string CuentaContable { get; set; }
        public bool Activo { get; set; }
        public DateTime Fecha { get; set; }
        public long idUsuario { get; set; }
        public long idEmpresa { get; set; }
    }
}
