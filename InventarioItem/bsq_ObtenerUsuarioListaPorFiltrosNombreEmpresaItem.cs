using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioItem
{
    public class bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem
    {

        public long idUsuario { get; set; }
        public string Identificacion { get; set; }
        public string Usuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefonos { get; set; }
        public string Empresa { get; set; }
        public string Direccion { get; set; }
        public string Contrasena { get; set; }

        public bsq_ObtenerUsuarioListaPorFiltrosNombreEmpresaItem()
        {
            idUsuario = 0;
        }

    }
}
