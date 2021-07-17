using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblGrupoClienteItem
    {
        public long idGrupoCliente { get; set; }
        public string Nombre { get; set; }
        public string CuentaContable { get; set; }
        public long idUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public long idEmpresa { get; set; }
    }
}
