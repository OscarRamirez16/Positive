using System;

namespace InventarioItem
{
    public class Servidor
    {
        public int IdServidor { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public string CadenaConexion { get; set; }
        public bool Disponible { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    public class EmpresaUsuario
    {
        public string prefijoUsuario { get; set; }
        public string PrimeNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
    }
    public class tblEmpresaItem
    {
        public long idEmpresa { get; set; }
        public string Nombre { get; set; }
        public short idTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public short idCiudad { get; set; }
        public string Ciudad { get; set; }
        public string TextoEncabezadoFactura { get; set; }
        public string TextoPieFactura { get; set; }
        public bool InventarioNegativo { get; set; }
        public decimal MargenUtilidad { get; set; }
        public bool Activo { get; set; }
        public short NumeroUsuarios { get; set; }
        public string ZonaHoraria { get; set; }
        public bool FacturacionCaja { get; set; }
        public DateTime FechaInicialEntrega { get; set; }
        public bool ManejaPrecioConIVA { get; set; }
        public bool ManejaCostoConIVA { get; set; }
        public bool ManejaDescuentoConIVA { get; set; }
        public bool MultiBodega { get; set; }
        public bool MostrarRemisiones { get; set; }
        public decimal Propina { get; set; }
        public byte[] Logo { get; set; }
        public decimal Impoconsumo { get; set; }
        public bool MostrarComisionesArticulo { get; set; }

        //Factura electrónica
        public string Correo { get; set; }
        public string SoftwareID { get; set; }
        public string SoftwarePIN { get; set; }
        public string CodigoDepartamento { get; set; }
        public string Departamento { get; set; }
        public string CodigoCiudad { get; set; }
        public string MatriculaMercantil { get; set; }
        public string TipoContribuyente { get; set; }
        public string RegimenFiscal { get; set; }
        public string CodigoResponsabilidadFiscal { get; set; }
        public string ResponsabilidadFiscal { get; set; }
        public string TestSetId { get; set; }
        public string ClaveTecnica { get; set; }
        public string ClaveCertificado { get; set; }
        public int Consecutivo { get; set; }
        public byte[] CertificadoFE { get; set; }
        public int IdServidor { get; set; }
        public string Prefijo { get; set; }
    }
}
