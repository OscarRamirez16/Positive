using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblCampanaClienteItem
    {
        public long idCampanaCliente { get; set; }
        public long idCampana { get; set; }
        public bool Excluir { get; set; }
        public short TipoCampanaCliente { get; set; }
        public string TipoCampanaClienteNombre { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public long idUsuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
