using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblRolItem
    {

        public tblRolItem()
        {
            idRol = 0;
        }

        public short idRol { get; set; }
        public string Nombre { get; set; }
        public long idEmpresa { get; set; }
    }
}
