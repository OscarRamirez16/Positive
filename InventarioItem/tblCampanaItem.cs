using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class tblCampanaItem
    {
        public long idCampana { get; set; }
        public string Nombre { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime HoraInicial { get; set; }
        public DateTime HoraFinal { get; set; }
        public long idUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public long idEmpresa { get; set; }
        public bool Activo { get; set; }
    }
}
