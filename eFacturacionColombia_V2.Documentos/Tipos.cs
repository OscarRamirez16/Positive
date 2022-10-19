using System;
using System.Collections.ObjectModel;

namespace eFacturacionColombia_V2.Documentos
{
    public class AmbienteDestino
    {
        public string Codigo { get; set; }

        public static AmbienteDestino PRODUCCION => new AmbienteDestino { Codigo = "1" };
        public static AmbienteDestino PRUEBAS => new AmbienteDestino { Codigo = "2" };
    }

    public class RangoNumeracion
    {
        public long NumeroResolucion { get; set; }
        public DateTime FechaResolucion { get; set; }
        public string Prefijo { get; set; }
        public long Desde { get; set; }
        public long Hasta { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime VigenciaHasta { get; set; }
        public string ClaveTecnica { get; set; }
    }

    public class ExtensionDian
    {
        public RangoNumeracion RangoNumeracion { get; set; }
        public Pais PaisOrigen { get; set; } = Pais.COLOMBIA;
        public string SoftwareProveedorNit { get; set; }
        public string SoftwareIdentificador { get; set; }
        public string SoftwarePin { get; set; }
    }

    public class AlgoritmoSeguridadUUID
    {
        public string Codigo { get; set; }

        public static AlgoritmoSeguridadUUID CUFE_SHA384 => new AlgoritmoSeguridadUUID { Codigo = "CUFE-SHA384" };
        public static AlgoritmoSeguridadUUID CUDE_SHA384 => new AlgoritmoSeguridadUUID { Codigo = "CUDE-SHA384" };
    }

    public class TipoFactura
    {
        public string Codigo { get; set; }

        public static TipoFactura VENTA => new TipoFactura { Codigo = "01" };
        public static TipoFactura EXPORTACION => new TipoFactura { Codigo = "02" };
        public static TipoFactura CONTINGENCIA_FACTURADOR => new TipoFactura { Codigo = "03" };
        public static TipoFactura CONTINGENCIA_DIAN => new TipoFactura { Codigo = "04" };
    }

    public class OperacionFactura
    {
        public string Codigo { get; set; }

        public static OperacionFactura ESTANDAR => new OperacionFactura { Codigo = "10" };
        public static OperacionFactura AIU => new OperacionFactura { Codigo = "09" };
        public static OperacionFactura MANDATOS => new OperacionFactura { Codigo = "11" };
    }

    public class OperacionNotaCredito
    {
        public string Codigo { get; set; }

        /// <summary>
        /// Nota Crédito que referencia una factura electrónica.
        /// </summary>
        public static OperacionNotaCredito CON_REFERENCIA_FE => new OperacionNotaCredito { Codigo = "20" };
        /// <summary>
        /// Nota Crédito sin referencia a facturas*.
        /// </summary>
        public static OperacionNotaCredito SIN_REFERENCIA_FACTURA => new OperacionNotaCredito { Codigo = "22" };
        /// <summary>
        /// Nota Crédito para facturación electrónica V1 (Decreto 2242).
        /// </summary>
        public static OperacionNotaCredito PARA_FE_V1 => new OperacionNotaCredito { Codigo = "23" };
    }

    public class OperacionNotaDebito
    {
        public string Codigo { get; set; }

        /// <summary>
        /// Nota Débito que referencia una factura electrónica.
        /// </summary>
        public static OperacionNotaDebito CON_REFERENCIA_FE => new OperacionNotaDebito { Codigo = "30" };
        /// <summary>
        /// Nota Débito sin referencia a facturas*.
        /// </summary>
        public static OperacionNotaDebito SIN_REFERENCIA_FACTURA => new OperacionNotaDebito { Codigo = "32" };
        /// <summary>
        /// Nota Débito para facturación electrónica V1 (Decreto 2242).
        /// </summary>
        public static OperacionNotaDebito PARA_FE_V1 => new OperacionNotaDebito { Codigo = "33" };
    }

    public class TipoImpuesto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public static TipoImpuesto IVA => new TipoImpuesto
        {
            Codigo = "01",
            Nombre = "IVA",
            Descripcion = "Impuesto de Valor Agregado"
        };
        public static TipoImpuesto IC => new TipoImpuesto
        {
            Codigo = "02",
            Nombre = "IC",
            Descripcion = "Impuesto al Consumo"
        };
        public static TipoImpuesto ICA => new TipoImpuesto
        {
            Codigo = "03",
            Nombre = "ICA",
            Descripcion = "Impuesto de Industria, Comercio y Aviso"
        };
        public static TipoImpuesto INC => new TipoImpuesto
        {
            Codigo = "04",
            Nombre = "INC",
            Descripcion = "Impuesto Nacional al Consumo "
        };
        public static TipoImpuesto RETE_IVA => new TipoImpuesto
        {
            Codigo = "05",
            Nombre = "ReteIVA",
            Descripcion = "Retención sobre el IVA"
        };
        public static TipoImpuesto RETE_FUENTE => new TipoImpuesto
        {
            Codigo = "06",
            Nombre = "ReteFuente",
            Descripcion = "Retención sobre Renta"
        };
        public static TipoImpuesto RETE_ICA => new TipoImpuesto
        {
            Codigo = "07",
            Nombre = "ReteICA",
            Descripcion = "Retención sobre el ICA"
        };
        public static TipoImpuesto FTO_HORTIFRUTICULA => new TipoImpuesto
        {
            Codigo = "20",
            Nombre = "FtoHorticultura",
            Descripcion = "Cuota de Fomento Hortifrutícula"
        };
        public static TipoImpuesto TIMBRE => new TipoImpuesto
        {
            Codigo = "21",
            Nombre = "Timbre",
            Descripcion = "Impuesto de Timbre"
        };
        public static TipoImpuesto BOLSAS => new TipoImpuesto
        {
            Codigo = "22",
            Nombre = "Bolsas",
            Descripcion = "Impuesto al Consumo de Bolsa Plástica"
        };
        public static TipoImpuesto IN_CARBONO => new TipoImpuesto
        {
            Codigo = "23",
            Nombre = "INCarbono",
            Descripcion = "Impuesto Nacional al Carbono"
        };
        public static TipoImpuesto IN_COMBUSTIBLES => new TipoImpuesto
        {
            Codigo = "24",
            Nombre = "INCombustibles",
            Descripcion = "Impuesto Nacional a los Combustibles"
        };
        public static TipoImpuesto SOBRETASA_COMBUSTIBLES => new TipoImpuesto
        {
            Codigo = "25",
            Nombre = "Sobretasa Combustibles",
            Descripcion = "Sobretasa a los combustibles"
        };
        public static TipoImpuesto SORDICOM => new TipoImpuesto
        {
            Codigo = "26",
            Nombre = "Sordicom",
            Descripcion = "Contribución minoristas (Combustibles)"
        };
    }

    public class TipoContribuyente
    {
        public string Codigo { get; set; }

        public bool EsPersonaJuridica => Codigo == PERSONA_JURIDICA.Codigo;

        public bool EsPersonaNatural => Codigo == PERSONA_NATURAL.Codigo;

        /// <summary>
        /// Persona Jurídica y asimiladas.
        /// </summary>
        public static TipoContribuyente PERSONA_JURIDICA => new TipoContribuyente { Codigo = "1" };

        /// <summary>
        /// Persona Natural y asimiladas.
        /// </summary>
        public static TipoContribuyente PERSONA_NATURAL => new TipoContribuyente { Codigo = "2" };
    }

    public class RegimenFiscal
    {
        public string Codigo { get; set; }

        [Obsolete("Esta opción ha quedado obsoleta.", false)]
        public static RegimenFiscal REGIMEN_SIMPLE => new RegimenFiscal { Codigo = "04" };
        [Obsolete("Esta opción ha quedado obsoleta.", false)]
        public static RegimenFiscal REGIMEN_ORDINARIO => new RegimenFiscal { Codigo = "05" };

        public static RegimenFiscal RESPONSABLE_DE_IVA => new RegimenFiscal { Codigo = "48" };
        public static RegimenFiscal NO_RESPONSABLE_DE_IVA => new RegimenFiscal { Codigo = "49" };
    }

    public class ConceptoNotaCredito
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        /// <summary>
        /// Devolución de parte de los bienes; no aceptación de partes del servicio
        /// </summary>
        public static ConceptoNotaCredito DEVOLUCION_PARCIAL => new ConceptoNotaCredito
        {
            Codigo = "1",
            Descripcion = "Devolución de parte de los bienes; no aceptación de partes del servicio"
        };

        /// <summary>
        /// Anulación de factura electrónica
        /// </summary>
        public static ConceptoNotaCredito ANULACION_TOTAL => new ConceptoNotaCredito
        {
            Codigo = "2",
            Descripcion = "Anulación de factura electrónica"
        };

        /// <summary>
        /// Rebaja o descuento parcial o total
        /// </summary>
        public static ConceptoNotaCredito REBAJA_O_DESCUENTO => new ConceptoNotaCredito
        {
            Codigo = "3",
            Descripcion = "Rebaja o descuento parcial o total"
        };

        /// <summary>
        /// Ajuste de precio
        /// </summary>
        public static ConceptoNotaCredito AJUSTE_PRECIO => new ConceptoNotaCredito
        {
            Codigo = "4",
            Descripcion = "Ajuste de precio"
        };

        /// <summary>
        /// Otros
        /// </summary>
        public static ConceptoNotaCredito OTROS => new ConceptoNotaCredito
        {
            Codigo = "5", // 6
            Descripcion = "Otros"
        };
    }

    public class ConceptoNotaDebito
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        /// <summary>
        /// Intereses
        /// </summary>
        public static ConceptoNotaDebito INTERESES => new ConceptoNotaDebito
        {
            Codigo = "1",
            Descripcion = "Intereses"
        };

        /// <summary>
        /// Gastos por cobrar
        /// </summary>
        public static ConceptoNotaDebito GASTOS_COBRAR => new ConceptoNotaDebito
        {
            Codigo = "2",
            Descripcion = "Gastos por cobrar"
        };

        /// <summary>
        /// Cambio de valor
        /// </summary>
        public static ConceptoNotaDebito CAMBIO_VALOR => new ConceptoNotaDebito
        {
            Codigo = "3",
            Descripcion = "Cambio del valor"
        };

        /// <summary>
        /// Otros
        /// </summary>
        public static ConceptoNotaDebito OTROS => new ConceptoNotaDebito
        {
            Codigo = "4",
            Descripcion = "Otros"
        };
    }

    public class Moneda
    {
        public string Codigo { get; set; }

        public static Moneda PESO_COLOMBIANO => new Moneda { Codigo = "COP" };
        public static Moneda DOLAR_ESTADOUNIDENSE => new Moneda { Codigo = "USD" };
        public static Moneda EURO => new Moneda { Codigo = "EUR" };
    }

    public class Pais
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public static Pais COLOMBIA => new Pais { Codigo = "CO", Nombre = "Colombia" };
    }

    public class Departamento
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public static Departamento AMAZONAS => new Departamento { Codigo = "91", Nombre = "Amazonas" };
        public static Departamento ANTIOQUIA => new Departamento { Codigo = "05", Nombre = "Antioquia" };
        public static Departamento ARAUCA => new Departamento { Codigo = "81", Nombre = "Arauca" };
        public static Departamento ATLANTICO => new Departamento { Codigo = "08", Nombre = "Atlántico" };
        public static Departamento BOGOTA => new Departamento { Codigo = "11", Nombre = "Bogotá" };
        public static Departamento BOLIVAR => new Departamento { Codigo = "13", Nombre = "Bolívar" };
        public static Departamento BOYACA => new Departamento { Codigo = "15", Nombre = "Boyacá" };
        public static Departamento CALDAS => new Departamento { Codigo = "17", Nombre = "Caldas" };
        public static Departamento CAQUETA => new Departamento { Codigo = "18", Nombre = "Caquetá" };
        public static Departamento CASANARE => new Departamento { Codigo = "85", Nombre = "Casanare" };
        public static Departamento CAUCA => new Departamento { Codigo = "19", Nombre = "Cauca" };
        public static Departamento CESAR => new Departamento { Codigo = "20", Nombre = "Cesar" };
        public static Departamento CHOCO => new Departamento { Codigo = "27", Nombre = "Chocó" };
        public static Departamento CORDOBA => new Departamento { Codigo = "23", Nombre = "Córdoba" };
        public static Departamento CUNDINAMARCA => new Departamento { Codigo = "25", Nombre = "Cundinamarca" };
        public static Departamento GUAINIA => new Departamento { Codigo = "94", Nombre = "Guainía" };
        public static Departamento GUAVIARE => new Departamento { Codigo = "95", Nombre = "Guaviare" };
        public static Departamento HUILA => new Departamento { Codigo = "41", Nombre = "Huila" };
        public static Departamento LA_GUAJIRA => new Departamento { Codigo = "44", Nombre = "La Guajira" };
        public static Departamento MAGDALENA => new Departamento { Codigo = "47", Nombre = "Magdalena" };
        public static Departamento META => new Departamento { Codigo = "50", Nombre = "Meta" };
        public static Departamento NARIÑO => new Departamento { Codigo = "52", Nombre = "Nariño" };
        public static Departamento NORTE_DE_SANTANDER => new Departamento { Codigo = "54", Nombre = "Norte de Santander" };
        public static Departamento PUTUMAYO => new Departamento { Codigo = "86", Nombre = "Putumayo" };
        public static Departamento QUINDIO => new Departamento { Codigo = "63", Nombre = "Quindío" };
        public static Departamento RISARALDA => new Departamento { Codigo = "66", Nombre = "Risaralda" };
        public static Departamento SAN_ANDRES_Y_PROVIDENCIA => new Departamento { Codigo = "88", Nombre = "San Andrés y Providencia" };
        public static Departamento SANTANDER => new Departamento { Codigo = "68", Nombre = "Santander" };
        public static Departamento SUCRE => new Departamento { Codigo = "70", Nombre = "Sucre" };
        public static Departamento TOLIMA => new Departamento { Codigo = "73", Nombre = "Tolima" };
        public static Departamento VALLE_DEL_CAUCA => new Departamento { Codigo = "76", Nombre = "Valle del Cauca" };
        public static Departamento VAUPES => new Departamento { Codigo = "97", Nombre = "Vaupés" };
        public static Departamento VICHADA => new Departamento { Codigo = "99", Nombre = "Vichada" };
    }

    public class Municipio
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public static Municipio ANTIOQUIA_MEDELLIN => new Municipio { Codigo = "05001", Nombre = "MEDELLÍN" };
        public static Municipio ANTIOQUIA_ABEJORRAL => new Municipio { Codigo = "05002", Nombre = "ABEJORRAL" };
        public static Municipio ANTIOQUIA_ABRIAQUI => new Municipio { Codigo = "05004", Nombre = "ABRIAQUÍ" };
        public static Municipio ANTIOQUIA_ALEJANDRIA => new Municipio { Codigo = "05021", Nombre = "ALEJANDRÍA" };
        public static Municipio ANTIOQUIA_AMAGA => new Municipio { Codigo = "05030", Nombre = "AMAGÁ" };
        public static Municipio ANTIOQUIA_AMALFI => new Municipio { Codigo = "05031", Nombre = "AMALFI" };
        public static Municipio ANTIOQUIA_ANDES => new Municipio { Codigo = "05034", Nombre = "ANDES" };
        public static Municipio ANTIOQUIA_ANGELOPOLIS => new Municipio { Codigo = "05036", Nombre = "ANGELÓPOLIS" };
        public static Municipio ANTIOQUIA_ANGOSTURA => new Municipio { Codigo = "05038", Nombre = "ANGOSTURA" };
        public static Municipio ANTIOQUIA_ANORI => new Municipio { Codigo = "05040", Nombre = "ANORÍ" };
        public static Municipio ANTIOQUIA_SANTA_FE_DE_ANTIOQUIA => new Municipio { Codigo = "05042", Nombre = "SANTA FÉ DE ANTIOQUIA" };
        public static Municipio ANTIOQUIA_ANZA => new Municipio { Codigo = "05044", Nombre = "ANZÁ" };
        public static Municipio ANTIOQUIA_APARTADO => new Municipio { Codigo = "05045", Nombre = "APARTADÓ" };
        public static Municipio ANTIOQUIA_ARBOLETES => new Municipio { Codigo = "05051", Nombre = "ARBOLETES" };
        public static Municipio ANTIOQUIA_ARGELIA => new Municipio { Codigo = "05055", Nombre = "ARGELIA" };
        public static Municipio ANTIOQUIA_ARMENIA => new Municipio { Codigo = "05059", Nombre = "ARMENIA" };
        public static Municipio ANTIOQUIA_BARBOSA => new Municipio { Codigo = "05079", Nombre = "BARBOSA" };
        public static Municipio ANTIOQUIA_BELMIRA => new Municipio { Codigo = "05086", Nombre = "BELMIRA" };
        public static Municipio ANTIOQUIA_BELLO => new Municipio { Codigo = "05088", Nombre = "BELLO" };
        public static Municipio ANTIOQUIA_BETANIA => new Municipio { Codigo = "05091", Nombre = "BETANIA" };
        public static Municipio ANTIOQUIA_BETULIA => new Municipio { Codigo = "05093", Nombre = "BETULIA" };
        public static Municipio ANTIOQUIA_CIUDAD_BOLIVAR => new Municipio { Codigo = "05101", Nombre = "CIUDAD BOLÍVAR" };
        public static Municipio ANTIOQUIA_BRICEÑO => new Municipio { Codigo = "05107", Nombre = "BRICEÑO" };
        public static Municipio ANTIOQUIA_BURITICA => new Municipio { Codigo = "05113", Nombre = "BURITICÁ" };
        public static Municipio ANTIOQUIA_CACERES => new Municipio { Codigo = "05120", Nombre = "CÁCERES" };
        public static Municipio ANTIOQUIA_CAICEDO => new Municipio { Codigo = "05125", Nombre = "CAICEDO" };
        public static Municipio ANTIOQUIA_CALDAS => new Municipio { Codigo = "05129", Nombre = "CALDAS" };
        public static Municipio ANTIOQUIA_CAMPAMENTO => new Municipio { Codigo = "05134", Nombre = "CAMPAMENTO" };
        public static Municipio ANTIOQUIA_CAÑASGORDAS => new Municipio { Codigo = "05138", Nombre = "CAÑASGORDAS" };
        public static Municipio ANTIOQUIA_CARACOLI => new Municipio { Codigo = "05142", Nombre = "CARACOLÍ" };
        public static Municipio ANTIOQUIA_CARAMANTA => new Municipio { Codigo = "05145", Nombre = "CARAMANTA" };
        public static Municipio ANTIOQUIA_CAREPA => new Municipio { Codigo = "05147", Nombre = "CAREPA" };
        public static Municipio ANTIOQUIA_EL_CARMEN_DE_VIBORAL => new Municipio { Codigo = "05148", Nombre = "EL CARMEN DE VIBORAL" };
        public static Municipio ANTIOQUIA_CAROLINA => new Municipio { Codigo = "05150", Nombre = "CAROLINA" };
        public static Municipio ANTIOQUIA_CAUCASIA => new Municipio { Codigo = "05154", Nombre = "CAUCASIA" };
        public static Municipio ANTIOQUIA_CHIGORODO => new Municipio { Codigo = "05172", Nombre = "CHIGORODÓ" };
        public static Municipio ANTIOQUIA_CISNEROS => new Municipio { Codigo = "05190", Nombre = "CISNEROS" };
        public static Municipio ANTIOQUIA_COCORNA => new Municipio { Codigo = "05197", Nombre = "COCORNÁ" };
        public static Municipio ANTIOQUIA_CONCEPCION => new Municipio { Codigo = "05206", Nombre = "CONCEPCIÓN" };
        public static Municipio ANTIOQUIA_CONCORDIA => new Municipio { Codigo = "05209", Nombre = "CONCORDIA" };
        public static Municipio ANTIOQUIA_COPACABANA => new Municipio { Codigo = "05212", Nombre = "COPACABANA" };
        public static Municipio ANTIOQUIA_DABEIBA => new Municipio { Codigo = "05234", Nombre = "DABEIBA" };
        public static Municipio ANTIOQUIA_DONMATIAS => new Municipio { Codigo = "05237", Nombre = "DONMATÍAS" };
        public static Municipio ANTIOQUIA_EBEJICO => new Municipio { Codigo = "05240", Nombre = "EBÉJICO" };
        public static Municipio ANTIOQUIA_EL_BAGRE => new Municipio { Codigo = "05250", Nombre = "EL BAGRE" };
        public static Municipio ANTIOQUIA_ENTRERRIOS => new Municipio { Codigo = "05264", Nombre = "ENTRERRÍOS" };
        public static Municipio ANTIOQUIA_ENVIGADO => new Municipio { Codigo = "05266", Nombre = "ENVIGADO" };
        public static Municipio ANTIOQUIA_FREDONIA => new Municipio { Codigo = "05282", Nombre = "FREDONIA" };
        public static Municipio ANTIOQUIA_FRONTINO => new Municipio { Codigo = "05284", Nombre = "FRONTINO" };
        public static Municipio ANTIOQUIA_GIRALDO => new Municipio { Codigo = "05306", Nombre = "GIRALDO" };
        public static Municipio ANTIOQUIA_GIRARDOTA => new Municipio { Codigo = "05308", Nombre = "GIRARDOTA" };
        public static Municipio ANTIOQUIA_GOMEZ_PLATA => new Municipio { Codigo = "05310", Nombre = "GÓMEZ PLATA" };
        public static Municipio ANTIOQUIA_GRANADA => new Municipio { Codigo = "05313", Nombre = "GRANADA" };
        public static Municipio ANTIOQUIA_GUADALUPE => new Municipio { Codigo = "05315", Nombre = "GUADALUPE" };
        public static Municipio ANTIOQUIA_GUARNE => new Municipio { Codigo = "05318", Nombre = "GUARNE" };
        public static Municipio ANTIOQUIA_GUATAPE => new Municipio { Codigo = "05321", Nombre = "GUATAPÉ" };
        public static Municipio ANTIOQUIA_HELICONIA => new Municipio { Codigo = "05347", Nombre = "HELICONIA" };
        public static Municipio ANTIOQUIA_HISPANIA => new Municipio { Codigo = "05353", Nombre = "HISPANIA" };
        public static Municipio ANTIOQUIA_ITAGÜI => new Municipio { Codigo = "05360", Nombre = "ITAGÜÍ" };
        public static Municipio ANTIOQUIA_ITUANGO => new Municipio { Codigo = "05361", Nombre = "ITUANGO" };
        public static Municipio ANTIOQUIA_JARDIN => new Municipio { Codigo = "05364", Nombre = "JARDÍN" };
        public static Municipio ANTIOQUIA_JERICO => new Municipio { Codigo = "05368", Nombre = "JERICÓ" };
        public static Municipio ANTIOQUIA_LA_CEJA => new Municipio { Codigo = "05376", Nombre = "LA CEJA" };
        public static Municipio ANTIOQUIA_LA_ESTRELLA => new Municipio { Codigo = "05380", Nombre = "LA ESTRELLA" };
        public static Municipio ANTIOQUIA_LA_PINTADA => new Municipio { Codigo = "05390", Nombre = "LA PINTADA" };
        public static Municipio ANTIOQUIA_LA_UNION => new Municipio { Codigo = "05400", Nombre = "LA UNIÓN" };
        public static Municipio ANTIOQUIA_LIBORINA => new Municipio { Codigo = "05411", Nombre = "LIBORINA" };
        public static Municipio ANTIOQUIA_MACEO => new Municipio { Codigo = "05425", Nombre = "MACEO" };
        public static Municipio ANTIOQUIA_MARINILLA => new Municipio { Codigo = "05440", Nombre = "MARINILLA" };
        public static Municipio ANTIOQUIA_MONTEBELLO => new Municipio { Codigo = "05467", Nombre = "MONTEBELLO" };
        public static Municipio ANTIOQUIA_MURINDO => new Municipio { Codigo = "05475", Nombre = "MURINDÓ" };
        public static Municipio ANTIOQUIA_MUTATA => new Municipio { Codigo = "05480", Nombre = "MUTATÁ" };
        public static Municipio ANTIOQUIA_NARIÑO => new Municipio { Codigo = "05483", Nombre = "NARIÑO" };
        public static Municipio ANTIOQUIA_NECOCLI => new Municipio { Codigo = "05490", Nombre = "NECOCLÍ" };
        public static Municipio ANTIOQUIA_NECHI => new Municipio { Codigo = "05495", Nombre = "NECHÍ" };
        public static Municipio ANTIOQUIA_OLAYA => new Municipio { Codigo = "05501", Nombre = "OLAYA" };
        public static Municipio ANTIOQUIA_PEÑOL => new Municipio { Codigo = "05541", Nombre = "PEÑOL" };
        public static Municipio ANTIOQUIA_PEQUE => new Municipio { Codigo = "05543", Nombre = "PEQUE" };
        public static Municipio ANTIOQUIA_PUEBLORRICO => new Municipio { Codigo = "05576", Nombre = "PUEBLORRICO" };
        public static Municipio ANTIOQUIA_PUERTO_BERRIO => new Municipio { Codigo = "05579", Nombre = "PUERTO BERRÍO" };
        public static Municipio ANTIOQUIA_PUERTO_NARE => new Municipio { Codigo = "05585", Nombre = "PUERTO NARE" };
        public static Municipio ANTIOQUIA_PUERTO_TRIUNFO => new Municipio { Codigo = "05591", Nombre = "PUERTO TRIUNFO" };
        public static Municipio ANTIOQUIA_REMEDIOS => new Municipio { Codigo = "05604", Nombre = "REMEDIOS" };
        public static Municipio ANTIOQUIA_RETIRO => new Municipio { Codigo = "05607", Nombre = "RETIRO" };
        public static Municipio ANTIOQUIA_RIONEGRO => new Municipio { Codigo = "05615", Nombre = "RIONEGRO" };
        public static Municipio ANTIOQUIA_SABANALARGA => new Municipio { Codigo = "05628", Nombre = "SABANALARGA" };
        public static Municipio ANTIOQUIA_SABANETA => new Municipio { Codigo = "05631", Nombre = "SABANETA" };
        public static Municipio ANTIOQUIA_SALGAR => new Municipio { Codigo = "05642", Nombre = "SALGAR" };
        public static Municipio ANTIOQUIA_SAN_ANDRES_DE_CUERQUIA => new Municipio { Codigo = "05647", Nombre = "SAN ANDRÉS DE CUERQUÍA" };
        public static Municipio ANTIOQUIA_SAN_CARLOS => new Municipio { Codigo = "05649", Nombre = "SAN CARLOS" };
        public static Municipio ANTIOQUIA_SAN_FRANCISCO => new Municipio { Codigo = "05652", Nombre = "SAN FRANCISCO" };
        public static Municipio ANTIOQUIA_SAN_JERONIMO => new Municipio { Codigo = "05656", Nombre = "SAN JERÓNIMO" };
        public static Municipio ANTIOQUIA_SAN_JOSE_DE_LA_MONTAÑA => new Municipio { Codigo = "05658", Nombre = "SAN JOSÉ DE LA MONTAÑA" };
        public static Municipio ANTIOQUIA_SAN_JUAN_DE_URABA => new Municipio { Codigo = "05659", Nombre = "SAN JUAN DE URABÁ" };
        public static Municipio ANTIOQUIA_SAN_LUIS => new Municipio { Codigo = "05660", Nombre = "SAN LUIS" };
        public static Municipio ANTIOQUIA_SAN_PEDRO_DE_LOS_MILAGROS => new Municipio { Codigo = "05664", Nombre = "SAN PEDRO DE LOS MILAGROS" };
        public static Municipio ANTIOQUIA_SAN_PEDRO_DE_URABA => new Municipio { Codigo = "05665", Nombre = "SAN PEDRO DE URABÁ" };
        public static Municipio ANTIOQUIA_SAN_RAFAEL => new Municipio { Codigo = "05667", Nombre = "SAN RAFAEL" };
        public static Municipio ANTIOQUIA_SAN_ROQUE => new Municipio { Codigo = "05670", Nombre = "SAN ROQUE" };
        public static Municipio ANTIOQUIA_SAN_VICENTE_FERRER => new Municipio { Codigo = "05674", Nombre = "SAN VICENTE FERRER" };
        public static Municipio ANTIOQUIA_SANTA_BARBARA => new Municipio { Codigo = "05679", Nombre = "SANTA BÁRBARA" };
        public static Municipio ANTIOQUIA_SANTA_ROSA_DE_OSOS => new Municipio { Codigo = "05686", Nombre = "SANTA ROSA DE OSOS" };
        public static Municipio ANTIOQUIA_SANTO_DOMINGO => new Municipio { Codigo = "05690", Nombre = "SANTO DOMINGO" };
        public static Municipio ANTIOQUIA_EL_SANTUARIO => new Municipio { Codigo = "05697", Nombre = "EL SANTUARIO" };
        public static Municipio ANTIOQUIA_SEGOVIA => new Municipio { Codigo = "05736", Nombre = "SEGOVIA" };
        public static Municipio ANTIOQUIA_SONSON => new Municipio { Codigo = "05756", Nombre = "SONSÓN" };
        public static Municipio ANTIOQUIA_SOPETRAN => new Municipio { Codigo = "05761", Nombre = "SOPETRÁN" };
        public static Municipio ANTIOQUIA_TAMESIS => new Municipio { Codigo = "05789", Nombre = "TÁMESIS" };
        public static Municipio ANTIOQUIA_TARAZA => new Municipio { Codigo = "05790", Nombre = "TARAZÁ" };
        public static Municipio ANTIOQUIA_TARSO => new Municipio { Codigo = "05792", Nombre = "TARSO" };
        public static Municipio ANTIOQUIA_TITIRIBI => new Municipio { Codigo = "05809", Nombre = "TITIRIBÍ" };
        public static Municipio ANTIOQUIA_TOLEDO => new Municipio { Codigo = "05819", Nombre = "TOLEDO" };
        public static Municipio ANTIOQUIA_TURBO => new Municipio { Codigo = "05837", Nombre = "TURBO" };
        public static Municipio ANTIOQUIA_URAMITA => new Municipio { Codigo = "05842", Nombre = "URAMITA" };
        public static Municipio ANTIOQUIA_URRAO => new Municipio { Codigo = "05847", Nombre = "URRAO" };
        public static Municipio ANTIOQUIA_VALDIVIA => new Municipio { Codigo = "05854", Nombre = "VALDIVIA" };
        public static Municipio ANTIOQUIA_VALPARAISO => new Municipio { Codigo = "05856", Nombre = "VALPARAÍSO" };
        public static Municipio ANTIOQUIA_VEGACHI => new Municipio { Codigo = "05858", Nombre = "VEGACHÍ" };
        public static Municipio ANTIOQUIA_VENECIA => new Municipio { Codigo = "05861", Nombre = "VENECIA" };
        public static Municipio ANTIOQUIA_VIGIA_DEL_FUERTE => new Municipio { Codigo = "05873", Nombre = "VIGÍA DEL FUERTE" };
        public static Municipio ANTIOQUIA_YALI => new Municipio { Codigo = "05885", Nombre = "YALÍ" };
        public static Municipio ANTIOQUIA_YARUMAL => new Municipio { Codigo = "05887", Nombre = "YARUMAL" };
        public static Municipio ANTIOQUIA_YOLOMBO => new Municipio { Codigo = "05890", Nombre = "YOLOMBÓ" };
        public static Municipio ANTIOQUIA_YONDO => new Municipio { Codigo = "05893", Nombre = "YONDÓ" };
        public static Municipio ANTIOQUIA_ZARAGOZA => new Municipio { Codigo = "05895", Nombre = "ZARAGOZA" };
        public static Municipio ATLANTICO_BARRANQUILLA => new Municipio { Codigo = "08001", Nombre = "BARRANQUILLA" };
        public static Municipio ATLANTICO_BARANOA => new Municipio { Codigo = "08078", Nombre = "BARANOA" };
        public static Municipio ATLANTICO_CAMPO_DE_LA_CRUZ => new Municipio { Codigo = "08137", Nombre = "CAMPO DE LA CRUZ" };
        public static Municipio ATLANTICO_CANDELARIA => new Municipio { Codigo = "08141", Nombre = "CANDELARIA" };
        public static Municipio ATLANTICO_GALAPA => new Municipio { Codigo = "08296", Nombre = "GALAPA" };
        public static Municipio ATLANTICO_JUAN_DE_ACOSTA => new Municipio { Codigo = "08372", Nombre = "JUAN DE ACOSTA" };
        public static Municipio ATLANTICO_LURUACO => new Municipio { Codigo = "08421", Nombre = "LURUACO" };
        public static Municipio ATLANTICO_MALAMBO => new Municipio { Codigo = "08433", Nombre = "MALAMBO" };
        public static Municipio ATLANTICO_MANATI => new Municipio { Codigo = "08436", Nombre = "MANATÍ" };
        public static Municipio ATLANTICO_PALMAR_DE_VARELA => new Municipio { Codigo = "08520", Nombre = "PALMAR DE VARELA" };
        public static Municipio ATLANTICO_PIOJO => new Municipio { Codigo = "08549", Nombre = "PIOJÓ" };
        public static Municipio ATLANTICO_POLONUEVO => new Municipio { Codigo = "08558", Nombre = "POLONUEVO" };
        public static Municipio ATLANTICO_PONEDERA => new Municipio { Codigo = "08560", Nombre = "PONEDERA" };
        public static Municipio ATLANTICO_PUERTO_COLOMBIA => new Municipio { Codigo = "08573", Nombre = "PUERTO COLOMBIA" };
        public static Municipio ATLANTICO_REPELON => new Municipio { Codigo = "08606", Nombre = "REPELÓN" };
        public static Municipio ATLANTICO_SABANAGRANDE => new Municipio { Codigo = "08634", Nombre = "SABANAGRANDE" };
        public static Municipio ATLANTICO_SABANALARGA => new Municipio { Codigo = "08638", Nombre = "SABANALARGA" };
        public static Municipio ATLANTICO_SANTA_LUCIA => new Municipio { Codigo = "08675", Nombre = "SANTA LUCÍA" };
        public static Municipio ATLANTICO_SANTO_TOMAS => new Municipio { Codigo = "08685", Nombre = "SANTO TOMÁS" };
        public static Municipio ATLANTICO_SOLEDAD => new Municipio { Codigo = "08758", Nombre = "SOLEDAD" };
        public static Municipio ATLANTICO_SUAN => new Municipio { Codigo = "08770", Nombre = "SUAN" };
        public static Municipio ATLANTICO_TUBARA => new Municipio { Codigo = "08832", Nombre = "TUBARÁ" };
        public static Municipio ATLANTICO_USIACURI => new Municipio { Codigo = "08849", Nombre = "USIACURÍ" };
        public static Municipio BOGOTA_BOGOTA_DC => new Municipio { Codigo = "11001", Nombre = "BOGOTÁ, D.C." };
        public static Municipio BOLIVAR_CARTAGENA_DE_INDIAS => new Municipio { Codigo = "13001", Nombre = "CARTAGENA DE INDIAS" };
        public static Municipio BOLIVAR_ACHI => new Municipio { Codigo = "13006", Nombre = "ACHÍ" };
        public static Municipio BOLIVAR_ALTOS_DEL_ROSARIO => new Municipio { Codigo = "13030", Nombre = "ALTOS DEL ROSARIO" };
        public static Municipio BOLIVAR_ARENAL => new Municipio { Codigo = "13042", Nombre = "ARENAL" };
        public static Municipio BOLIVAR_ARJONA => new Municipio { Codigo = "13052", Nombre = "ARJONA" };
        public static Municipio BOLIVAR_ARROYOHONDO => new Municipio { Codigo = "13062", Nombre = "ARROYOHONDO" };
        public static Municipio BOLIVAR_BARRANCO_DE_LOBA => new Municipio { Codigo = "13074", Nombre = "BARRANCO DE LOBA" };
        public static Municipio BOLIVAR_CALAMAR => new Municipio { Codigo = "13140", Nombre = "CALAMAR" };
        public static Municipio BOLIVAR_CANTAGALLO => new Municipio { Codigo = "13160", Nombre = "CANTAGALLO" };
        public static Municipio BOLIVAR_CICUCO => new Municipio { Codigo = "13188", Nombre = "CICUCO" };
        public static Municipio BOLIVAR_CORDOBA => new Municipio { Codigo = "13212", Nombre = "CÓRDOBA" };
        public static Municipio BOLIVAR_CLEMENCIA => new Municipio { Codigo = "13222", Nombre = "CLEMENCIA" };
        public static Municipio BOLIVAR_EL_CARMEN_DE_BOLIVAR => new Municipio { Codigo = "13244", Nombre = "EL CARMEN DE BOLÍVAR" };
        public static Municipio BOLIVAR_EL_GUAMO => new Municipio { Codigo = "13248", Nombre = "EL GUAMO" };
        public static Municipio BOLIVAR_EL_PEÑON => new Municipio { Codigo = "13268", Nombre = "EL PEÑÓN" };
        public static Municipio BOLIVAR_HATILLO_DE_LOBA => new Municipio { Codigo = "13300", Nombre = "HATILLO DE LOBA" };
        public static Municipio BOLIVAR_MAGANGUE => new Municipio { Codigo = "13430", Nombre = "MAGANGUÉ" };
        public static Municipio BOLIVAR_MAHATES => new Municipio { Codigo = "13433", Nombre = "MAHATES" };
        public static Municipio BOLIVAR_MARGARITA => new Municipio { Codigo = "13440", Nombre = "MARGARITA" };
        public static Municipio BOLIVAR_MARIA_LA_BAJA => new Municipio { Codigo = "13442", Nombre = "MARÍA LA BAJA" };
        public static Municipio BOLIVAR_MONTECRISTO => new Municipio { Codigo = "13458", Nombre = "MONTECRISTO" };
        public static Municipio BOLIVAR_MOMPOS => new Municipio { Codigo = "13468", Nombre = "MOMPÓS" };
        public static Municipio BOLIVAR_MORALES => new Municipio { Codigo = "13473", Nombre = "MORALES" };
        public static Municipio BOLIVAR_NOROSI => new Municipio { Codigo = "13490", Nombre = "NOROSÍ" };
        public static Municipio BOLIVAR_PINILLOS => new Municipio { Codigo = "13549", Nombre = "PINILLOS" };
        public static Municipio BOLIVAR_REGIDOR => new Municipio { Codigo = "13580", Nombre = "REGIDOR" };
        public static Municipio BOLIVAR_RIO_VIEJO => new Municipio { Codigo = "13600", Nombre = "RÍO VIEJO" };
        public static Municipio BOLIVAR_SAN_CRISTOBAL => new Municipio { Codigo = "13620", Nombre = "SAN CRISTÓBAL" };
        public static Municipio BOLIVAR_SAN_ESTANISLAO => new Municipio { Codigo = "13647", Nombre = "SAN ESTANISLAO" };
        public static Municipio BOLIVAR_SAN_FERNANDO => new Municipio { Codigo = "13650", Nombre = "SAN FERNANDO" };
        public static Municipio BOLIVAR_SAN_JACINTO => new Municipio { Codigo = "13654", Nombre = "SAN JACINTO" };
        public static Municipio BOLIVAR_SAN_JACINTO_DEL_CAUCA => new Municipio { Codigo = "13655", Nombre = "SAN JACINTO DEL CAUCA" };
        public static Municipio BOLIVAR_SAN_JUAN_NEPOMUCENO => new Municipio { Codigo = "13657", Nombre = "SAN JUAN NEPOMUCENO" };
        public static Municipio BOLIVAR_SAN_MARTIN_DE_LOBA => new Municipio { Codigo = "13667", Nombre = "SAN MARTÍN DE LOBA" };
        public static Municipio BOLIVAR_SAN_PABLO => new Municipio { Codigo = "13670", Nombre = "SAN PABLO" };
        public static Municipio BOLIVAR_SANTA_CATALINA => new Municipio { Codigo = "13673", Nombre = "SANTA CATALINA" };
        public static Municipio BOLIVAR_SANTA_ROSA => new Municipio { Codigo = "13683", Nombre = "SANTA ROSA" };
        public static Municipio BOLIVAR_SANTA_ROSA_DEL_SUR => new Municipio { Codigo = "13688", Nombre = "SANTA ROSA DEL SUR" };
        public static Municipio BOLIVAR_SIMITI => new Municipio { Codigo = "13744", Nombre = "SIMITÍ" };
        public static Municipio BOLIVAR_SOPLAVIENTO => new Municipio { Codigo = "13760", Nombre = "SOPLAVIENTO" };
        public static Municipio BOLIVAR_TALAIGUA_NUEVO => new Municipio { Codigo = "13780", Nombre = "TALAIGUA NUEVO" };
        public static Municipio BOLIVAR_TIQUISIO => new Municipio { Codigo = "13810", Nombre = "TIQUISIO" };
        public static Municipio BOLIVAR_TURBACO => new Municipio { Codigo = "13836", Nombre = "TURBACO" };
        public static Municipio BOLIVAR_TURBANA => new Municipio { Codigo = "13838", Nombre = "TURBANÁ" };
        public static Municipio BOLIVAR_VILLANUEVA => new Municipio { Codigo = "13873", Nombre = "VILLANUEVA" };
        public static Municipio BOLIVAR_ZAMBRANO => new Municipio { Codigo = "13894", Nombre = "ZAMBRANO" };
        public static Municipio BOYACA_TUNJA => new Municipio { Codigo = "15001", Nombre = "TUNJA" };
        public static Municipio BOYACA_ALMEIDA => new Municipio { Codigo = "15022", Nombre = "ALMEIDA" };
        public static Municipio BOYACA_AQUITANIA => new Municipio { Codigo = "15047", Nombre = "AQUITANIA" };
        public static Municipio BOYACA_ARCABUCO => new Municipio { Codigo = "15051", Nombre = "ARCABUCO" };
        public static Municipio BOYACA_BELEN => new Municipio { Codigo = "15087", Nombre = "BELÉN" };
        public static Municipio BOYACA_BERBEO => new Municipio { Codigo = "15090", Nombre = "BERBEO" };
        public static Municipio BOYACA_BETEITIVA => new Municipio { Codigo = "15092", Nombre = "BETÉITIVA" };
        public static Municipio BOYACA_BOAVITA => new Municipio { Codigo = "15097", Nombre = "BOAVITA" };
        public static Municipio BOYACA_BOYACA => new Municipio { Codigo = "15104", Nombre = "BOYACÁ" };
        public static Municipio BOYACA_BRICEÑO => new Municipio { Codigo = "15106", Nombre = "BRICEÑO" };
        public static Municipio BOYACA_BUENAVISTA => new Municipio { Codigo = "15109", Nombre = "BUENAVISTA" };
        public static Municipio BOYACA_BUSBANZA => new Municipio { Codigo = "15114", Nombre = "BUSBANZÁ" };
        public static Municipio BOYACA_CALDAS => new Municipio { Codigo = "15131", Nombre = "CALDAS" };
        public static Municipio BOYACA_CAMPOHERMOSO => new Municipio { Codigo = "15135", Nombre = "CAMPOHERMOSO" };
        public static Municipio BOYACA_CERINZA => new Municipio { Codigo = "15162", Nombre = "CERINZA" };
        public static Municipio BOYACA_CHINAVITA => new Municipio { Codigo = "15172", Nombre = "CHINAVITA" };
        public static Municipio BOYACA_CHIQUINQUIRA => new Municipio { Codigo = "15176", Nombre = "CHIQUINQUIRÁ" };
        public static Municipio BOYACA_CHISCAS => new Municipio { Codigo = "15180", Nombre = "CHISCAS" };
        public static Municipio BOYACA_CHITA => new Municipio { Codigo = "15183", Nombre = "CHITA" };
        public static Municipio BOYACA_CHITARAQUE => new Municipio { Codigo = "15185", Nombre = "CHITARAQUE" };
        public static Municipio BOYACA_CHIVATA => new Municipio { Codigo = "15187", Nombre = "CHIVATÁ" };
        public static Municipio BOYACA_CIENEGA => new Municipio { Codigo = "15189", Nombre = "CIÉNEGA" };
        public static Municipio BOYACA_COMBITA => new Municipio { Codigo = "15204", Nombre = "CÓMBITA" };
        public static Municipio BOYACA_COPER => new Municipio { Codigo = "15212", Nombre = "COPER" };
        public static Municipio BOYACA_CORRALES => new Municipio { Codigo = "15215", Nombre = "CORRALES" };
        public static Municipio BOYACA_COVARACHIA => new Municipio { Codigo = "15218", Nombre = "COVARACHÍA" };
        public static Municipio BOYACA_CUBARA => new Municipio { Codigo = "15223", Nombre = "CUBARÁ" };
        public static Municipio BOYACA_CUCAITA => new Municipio { Codigo = "15224", Nombre = "CUCAITA" };
        public static Municipio BOYACA_CUITIVA => new Municipio { Codigo = "15226", Nombre = "CUÍTIVA" };
        public static Municipio BOYACA_CHIQUIZA => new Municipio { Codigo = "15232", Nombre = "CHÍQUIZA" };
        public static Municipio BOYACA_CHIVOR => new Municipio { Codigo = "15236", Nombre = "CHIVOR" };
        public static Municipio BOYACA_DUITAMA => new Municipio { Codigo = "15238", Nombre = "DUITAMA" };
        public static Municipio BOYACA_EL_COCUY => new Municipio { Codigo = "15244", Nombre = "EL COCUY" };
        public static Municipio BOYACA_EL_ESPINO => new Municipio { Codigo = "15248", Nombre = "EL ESPINO" };
        public static Municipio BOYACA_FIRAVITOBA => new Municipio { Codigo = "15272", Nombre = "FIRAVITOBA" };
        public static Municipio BOYACA_FLORESTA => new Municipio { Codigo = "15276", Nombre = "FLORESTA" };
        public static Municipio BOYACA_GACHANTIVA => new Municipio { Codigo = "15293", Nombre = "GACHANTIVÁ" };
        public static Municipio BOYACA_GAMEZA => new Municipio { Codigo = "15296", Nombre = "GÁMEZA" };
        public static Municipio BOYACA_GARAGOA => new Municipio { Codigo = "15299", Nombre = "GARAGOA" };
        public static Municipio BOYACA_GUACAMAYAS => new Municipio { Codigo = "15317", Nombre = "GUACAMAYAS" };
        public static Municipio BOYACA_GUATEQUE => new Municipio { Codigo = "15322", Nombre = "GUATEQUE" };
        public static Municipio BOYACA_GUAYATA => new Municipio { Codigo = "15325", Nombre = "GUAYATÁ" };
        public static Municipio BOYACA_GÜICAN_DE_LA_SIERRA => new Municipio { Codigo = "15332", Nombre = "GÜICÁN DE LA SIERRA" };
        public static Municipio BOYACA_IZA => new Municipio { Codigo = "15362", Nombre = "IZA" };
        public static Municipio BOYACA_JENESANO => new Municipio { Codigo = "15367", Nombre = "JENESANO" };
        public static Municipio BOYACA_JERICO => new Municipio { Codigo = "15368", Nombre = "JERICÓ" };
        public static Municipio BOYACA_LABRANZAGRANDE => new Municipio { Codigo = "15377", Nombre = "LABRANZAGRANDE" };
        public static Municipio BOYACA_LA_CAPILLA => new Municipio { Codigo = "15380", Nombre = "LA CAPILLA" };
        public static Municipio BOYACA_LA_VICTORIA => new Municipio { Codigo = "15401", Nombre = "LA VICTORIA" };
        public static Municipio BOYACA_LA_UVITA => new Municipio { Codigo = "15403", Nombre = "LA UVITA" };
        public static Municipio BOYACA_VILLA_DE_LEYVA => new Municipio { Codigo = "15407", Nombre = "VILLA DE LEYVA" };
        public static Municipio BOYACA_MACANAL => new Municipio { Codigo = "15425", Nombre = "MACANAL" };
        public static Municipio BOYACA_MARIPI => new Municipio { Codigo = "15442", Nombre = "MARIPÍ" };
        public static Municipio BOYACA_MIRAFLORES => new Municipio { Codigo = "15455", Nombre = "MIRAFLORES" };
        public static Municipio BOYACA_MONGUA => new Municipio { Codigo = "15464", Nombre = "MONGUA" };
        public static Municipio BOYACA_MONGUI => new Municipio { Codigo = "15466", Nombre = "MONGUÍ" };
        public static Municipio BOYACA_MONIQUIRA => new Municipio { Codigo = "15469", Nombre = "MONIQUIRÁ" };
        public static Municipio BOYACA_MOTAVITA => new Municipio { Codigo = "15476", Nombre = "MOTAVITA" };
        public static Municipio BOYACA_MUZO => new Municipio { Codigo = "15480", Nombre = "MUZO" };
        public static Municipio BOYACA_NOBSA => new Municipio { Codigo = "15491", Nombre = "NOBSA" };
        public static Municipio BOYACA_NUEVO_COLON => new Municipio { Codigo = "15494", Nombre = "NUEVO COLÓN" };
        public static Municipio BOYACA_OICATA => new Municipio { Codigo = "15500", Nombre = "OICATÁ" };
        public static Municipio BOYACA_OTANCHE => new Municipio { Codigo = "15507", Nombre = "OTANCHE" };
        public static Municipio BOYACA_PACHAVITA => new Municipio { Codigo = "15511", Nombre = "PACHAVITA" };
        public static Municipio BOYACA_PAEZ => new Municipio { Codigo = "15514", Nombre = "PÁEZ" };
        public static Municipio BOYACA_PAIPA => new Municipio { Codigo = "15516", Nombre = "PAIPA" };
        public static Municipio BOYACA_PAJARITO => new Municipio { Codigo = "15518", Nombre = "PAJARITO" };
        public static Municipio BOYACA_PANQUEBA => new Municipio { Codigo = "15522", Nombre = "PANQUEBA" };
        public static Municipio BOYACA_PAUNA => new Municipio { Codigo = "15531", Nombre = "PAUNA" };
        public static Municipio BOYACA_PAYA => new Municipio { Codigo = "15533", Nombre = "PAYA" };
        public static Municipio BOYACA_PAZ_DE_RIO => new Municipio { Codigo = "15537", Nombre = "PAZ DE RÍO" };
        public static Municipio BOYACA_PESCA => new Municipio { Codigo = "15542", Nombre = "PESCA" };
        public static Municipio BOYACA_PISBA => new Municipio { Codigo = "15550", Nombre = "PISBA" };
        public static Municipio BOYACA_PUERTO_BOYACA => new Municipio { Codigo = "15572", Nombre = "PUERTO BOYACÁ" };
        public static Municipio BOYACA_QUIPAMA => new Municipio { Codigo = "15580", Nombre = "QUÍPAMA" };
        public static Municipio BOYACA_RAMIRIQUI => new Municipio { Codigo = "15599", Nombre = "RAMIRIQUÍ" };
        public static Municipio BOYACA_RAQUIRA => new Municipio { Codigo = "15600", Nombre = "RÁQUIRA" };
        public static Municipio BOYACA_RONDON => new Municipio { Codigo = "15621", Nombre = "RONDÓN" };
        public static Municipio BOYACA_SABOYA => new Municipio { Codigo = "15632", Nombre = "SABOYÁ" };
        public static Municipio BOYACA_SACHICA => new Municipio { Codigo = "15638", Nombre = "SÁCHICA" };
        public static Municipio BOYACA_SAMACA => new Municipio { Codigo = "15646", Nombre = "SAMACÁ" };
        public static Municipio BOYACA_SAN_EDUARDO => new Municipio { Codigo = "15660", Nombre = "SAN EDUARDO" };
        public static Municipio BOYACA_SAN_JOSE_DE_PARE => new Municipio { Codigo = "15664", Nombre = "SAN JOSÉ DE PARE" };
        public static Municipio BOYACA_SAN_LUIS_DE_GACENO => new Municipio { Codigo = "15667", Nombre = "SAN LUIS DE GACENO" };
        public static Municipio BOYACA_SAN_MATEO => new Municipio { Codigo = "15673", Nombre = "SAN MATEO" };
        public static Municipio BOYACA_SAN_MIGUEL_DE_SEMA => new Municipio { Codigo = "15676", Nombre = "SAN MIGUEL DE SEMA" };
        public static Municipio BOYACA_SAN_PABLO_DE_BORBUR => new Municipio { Codigo = "15681", Nombre = "SAN PABLO DE BORBUR" };
        public static Municipio BOYACA_SANTANA => new Municipio { Codigo = "15686", Nombre = "SANTANA" };
        public static Municipio BOYACA_SANTA_MARIA => new Municipio { Codigo = "15690", Nombre = "SANTA MARÍA" };
        public static Municipio BOYACA_SANTA_ROSA_DE_VITERBO => new Municipio { Codigo = "15693", Nombre = "SANTA ROSA DE VITERBO" };
        public static Municipio BOYACA_SANTA_SOFIA => new Municipio { Codigo = "15696", Nombre = "SANTA SOFÍA" };
        public static Municipio BOYACA_SATIVANORTE => new Municipio { Codigo = "15720", Nombre = "SATIVANORTE" };
        public static Municipio BOYACA_SATIVASUR => new Municipio { Codigo = "15723", Nombre = "SATIVASUR" };
        public static Municipio BOYACA_SIACHOQUE => new Municipio { Codigo = "15740", Nombre = "SIACHOQUE" };
        public static Municipio BOYACA_SOATA => new Municipio { Codigo = "15753", Nombre = "SOATÁ" };
        public static Municipio BOYACA_SOCOTA => new Municipio { Codigo = "15755", Nombre = "SOCOTÁ" };
        public static Municipio BOYACA_SOCHA => new Municipio { Codigo = "15757", Nombre = "SOCHA" };
        public static Municipio BOYACA_SOGAMOSO => new Municipio { Codigo = "15759", Nombre = "SOGAMOSO" };
        public static Municipio BOYACA_SOMONDOCO => new Municipio { Codigo = "15761", Nombre = "SOMONDOCO" };
        public static Municipio BOYACA_SORA => new Municipio { Codigo = "15762", Nombre = "SORA" };
        public static Municipio BOYACA_SOTAQUIRA => new Municipio { Codigo = "15763", Nombre = "SOTAQUIRÁ" };
        public static Municipio BOYACA_SORACA => new Municipio { Codigo = "15764", Nombre = "SORACÁ" };
        public static Municipio BOYACA_SUSACON => new Municipio { Codigo = "15774", Nombre = "SUSACÓN" };
        public static Municipio BOYACA_SUTAMARCHAN => new Municipio { Codigo = "15776", Nombre = "SUTAMARCHÁN" };
        public static Municipio BOYACA_SUTATENZA => new Municipio { Codigo = "15778", Nombre = "SUTATENZA" };
        public static Municipio BOYACA_TASCO => new Municipio { Codigo = "15790", Nombre = "TASCO" };
        public static Municipio BOYACA_TENZA => new Municipio { Codigo = "15798", Nombre = "TENZA" };
        public static Municipio BOYACA_TIBANA => new Municipio { Codigo = "15804", Nombre = "TIBANÁ" };
        public static Municipio BOYACA_TIBASOSA => new Municipio { Codigo = "15806", Nombre = "TIBASOSA" };
        public static Municipio BOYACA_TINJACA => new Municipio { Codigo = "15808", Nombre = "TINJACÁ" };
        public static Municipio BOYACA_TIPACOQUE => new Municipio { Codigo = "15810", Nombre = "TIPACOQUE" };
        public static Municipio BOYACA_TOCA => new Municipio { Codigo = "15814", Nombre = "TOCA" };
        public static Municipio BOYACA_TOGÜI => new Municipio { Codigo = "15816", Nombre = "TOGÜÍ" };
        public static Municipio BOYACA_TOPAGA => new Municipio { Codigo = "15820", Nombre = "TÓPAGA" };
        public static Municipio BOYACA_TOTA => new Municipio { Codigo = "15822", Nombre = "TOTA" };
        public static Municipio BOYACA_TUNUNGUA => new Municipio { Codigo = "15832", Nombre = "TUNUNGUÁ" };
        public static Municipio BOYACA_TURMEQUE => new Municipio { Codigo = "15835", Nombre = "TURMEQUÉ" };
        public static Municipio BOYACA_TUTA => new Municipio { Codigo = "15837", Nombre = "TUTA" };
        public static Municipio BOYACA_TUTAZA => new Municipio { Codigo = "15839", Nombre = "TUTAZÁ" };
        public static Municipio BOYACA_UMBITA => new Municipio { Codigo = "15842", Nombre = "ÚMBITA" };
        public static Municipio BOYACA_VENTAQUEMADA => new Municipio { Codigo = "15861", Nombre = "VENTAQUEMADA" };
        public static Municipio BOYACA_VIRACACHA => new Municipio { Codigo = "15879", Nombre = "VIRACACHÁ" };
        public static Municipio BOYACA_ZETAQUIRA => new Municipio { Codigo = "15897", Nombre = "ZETAQUIRA" };
        public static Municipio CALDAS_MANIZALES => new Municipio { Codigo = "17001", Nombre = "MANIZALES" };
        public static Municipio CALDAS_AGUADAS => new Municipio { Codigo = "17013", Nombre = "AGUADAS" };
        public static Municipio CALDAS_ANSERMA => new Municipio { Codigo = "17042", Nombre = "ANSERMA" };
        public static Municipio CALDAS_ARANZAZU => new Municipio { Codigo = "17050", Nombre = "ARANZAZU" };
        public static Municipio CALDAS_BELALCAZAR => new Municipio { Codigo = "17088", Nombre = "BELALCÁZAR" };
        public static Municipio CALDAS_CHINCHINA => new Municipio { Codigo = "17174", Nombre = "CHINCHINÁ" };
        public static Municipio CALDAS_FILADELFIA => new Municipio { Codigo = "17272", Nombre = "FILADELFIA" };
        public static Municipio CALDAS_LA_DORADA => new Municipio { Codigo = "17380", Nombre = "LA DORADA" };
        public static Municipio CALDAS_LA_MERCED => new Municipio { Codigo = "17388", Nombre = "LA MERCED" };
        public static Municipio CALDAS_MANZANARES => new Municipio { Codigo = "17433", Nombre = "MANZANARES" };
        public static Municipio CALDAS_MARMATO => new Municipio { Codigo = "17442", Nombre = "MARMATO" };
        public static Municipio CALDAS_MARQUETALIA => new Municipio { Codigo = "17444", Nombre = "MARQUETALIA" };
        public static Municipio CALDAS_MARULANDA => new Municipio { Codigo = "17446", Nombre = "MARULANDA" };
        public static Municipio CALDAS_NEIRA => new Municipio { Codigo = "17486", Nombre = "NEIRA" };
        public static Municipio CALDAS_NORCASIA => new Municipio { Codigo = "17495", Nombre = "NORCASIA" };
        public static Municipio CALDAS_PACORA => new Municipio { Codigo = "17513", Nombre = "PÁCORA" };
        public static Municipio CALDAS_PALESTINA => new Municipio { Codigo = "17524", Nombre = "PALESTINA" };
        public static Municipio CALDAS_PENSILVANIA => new Municipio { Codigo = "17541", Nombre = "PENSILVANIA" };
        public static Municipio CALDAS_RIOSUCIO => new Municipio { Codigo = "17614", Nombre = "RIOSUCIO" };
        public static Municipio CALDAS_RISARALDA => new Municipio { Codigo = "17616", Nombre = "RISARALDA" };
        public static Municipio CALDAS_SALAMINA => new Municipio { Codigo = "17653", Nombre = "SALAMINA" };
        public static Municipio CALDAS_SAMANA => new Municipio { Codigo = "17662", Nombre = "SAMANÁ" };
        public static Municipio CALDAS_SAN_JOSE => new Municipio { Codigo = "17665", Nombre = "SAN JOSÉ" };
        public static Municipio CALDAS_SUPIA => new Municipio { Codigo = "17777", Nombre = "SUPÍA" };
        public static Municipio CALDAS_VICTORIA => new Municipio { Codigo = "17867", Nombre = "VICTORIA" };
        public static Municipio CALDAS_VILLAMARIA => new Municipio { Codigo = "17873", Nombre = "VILLAMARÍA" };
        public static Municipio CALDAS_VITERBO => new Municipio { Codigo = "17877", Nombre = "VITERBO" };
        public static Municipio CAQUETA_FLORENCIA => new Municipio { Codigo = "18001", Nombre = "FLORENCIA" };
        public static Municipio CAQUETA_ALBANIA => new Municipio { Codigo = "18029", Nombre = "ALBANIA" };
        public static Municipio CAQUETA_BELEN_DE_LOS_ANDAQUIES => new Municipio { Codigo = "18094", Nombre = "BELÉN DE LOS ANDAQUÍES" };
        public static Municipio CAQUETA_CARTAGENA_DEL_CHAIRA => new Municipio { Codigo = "18150", Nombre = "CARTAGENA DEL CHAIRÁ" };
        public static Municipio CAQUETA_CURILLO => new Municipio { Codigo = "18205", Nombre = "CURILLO" };
        public static Municipio CAQUETA_EL_DONCELLO => new Municipio { Codigo = "18247", Nombre = "EL DONCELLO" };
        public static Municipio CAQUETA_EL_PAUJIL => new Municipio { Codigo = "18256", Nombre = "EL PAUJÍL" };
        public static Municipio CAQUETA_LA_MONTAÑITA => new Municipio { Codigo = "18410", Nombre = "LA MONTAÑITA" };
        public static Municipio CAQUETA_MILAN => new Municipio { Codigo = "18460", Nombre = "MILÁN" };
        public static Municipio CAQUETA_MORELIA => new Municipio { Codigo = "18479", Nombre = "MORELIA" };
        public static Municipio CAQUETA_PUERTO_RICO => new Municipio { Codigo = "18592", Nombre = "PUERTO RICO" };
        public static Municipio CAQUETA_SAN_JOSE_DEL_FRAGUA => new Municipio { Codigo = "18610", Nombre = "SAN JOSÉ DEL FRAGUA" };
        public static Municipio CAQUETA_SAN_VICENTE_DEL_CAGUAN => new Municipio { Codigo = "18753", Nombre = "SAN VICENTE DEL CAGUÁN" };
        public static Municipio CAQUETA_SOLANO => new Municipio { Codigo = "18756", Nombre = "SOLANO" };
        public static Municipio CAQUETA_SOLITA => new Municipio { Codigo = "18785", Nombre = "SOLITA" };
        public static Municipio CAQUETA_VALPARAISO => new Municipio { Codigo = "18860", Nombre = "VALPARAÍSO" };
        public static Municipio CAUCA_POPAYAN => new Municipio { Codigo = "19001", Nombre = "POPAYÁN" };
        public static Municipio CAUCA_ALMAGUER => new Municipio { Codigo = "19022", Nombre = "ALMAGUER" };
        public static Municipio CAUCA_ARGELIA => new Municipio { Codigo = "19050", Nombre = "ARGELIA" };
        public static Municipio CAUCA_BALBOA => new Municipio { Codigo = "19075", Nombre = "BALBOA" };
        public static Municipio CAUCA_BOLIVAR => new Municipio { Codigo = "19100", Nombre = "BOLÍVAR" };
        public static Municipio CAUCA_BUENOS_AIRES => new Municipio { Codigo = "19110", Nombre = "BUENOS AIRES" };
        public static Municipio CAUCA_CAJIBIO => new Municipio { Codigo = "19130", Nombre = "CAJIBÍO" };
        public static Municipio CAUCA_CALDONO => new Municipio { Codigo = "19137", Nombre = "CALDONO" };
        public static Municipio CAUCA_CALOTO => new Municipio { Codigo = "19142", Nombre = "CALOTO" };
        public static Municipio CAUCA_CORINTO => new Municipio { Codigo = "19212", Nombre = "CORINTO" };
        public static Municipio CAUCA_EL_TAMBO => new Municipio { Codigo = "19256", Nombre = "EL TAMBO" };
        public static Municipio CAUCA_FLORENCIA => new Municipio { Codigo = "19290", Nombre = "FLORENCIA" };
        public static Municipio CAUCA_GUACHENE => new Municipio { Codigo = "19300", Nombre = "GUACHENÉ" };
        public static Municipio CAUCA_GUAPI => new Municipio { Codigo = "19318", Nombre = "GUAPÍ" };
        public static Municipio CAUCA_INZA => new Municipio { Codigo = "19355", Nombre = "INZÁ" };
        public static Municipio CAUCA_JAMBALO => new Municipio { Codigo = "19364", Nombre = "JAMBALÓ" };
        public static Municipio CAUCA_LA_SIERRA => new Municipio { Codigo = "19392", Nombre = "LA SIERRA" };
        public static Municipio CAUCA_LA_VEGA => new Municipio { Codigo = "19397", Nombre = "LA VEGA" };
        public static Municipio CAUCA_LOPEZ_DE_MICAY => new Municipio { Codigo = "19418", Nombre = "LÓPEZ DE MICAY" };
        public static Municipio CAUCA_MERCADERES => new Municipio { Codigo = "19450", Nombre = "MERCADERES" };
        public static Municipio CAUCA_MIRANDA => new Municipio { Codigo = "19455", Nombre = "MIRANDA" };
        public static Municipio CAUCA_MORALES => new Municipio { Codigo = "19473", Nombre = "MORALES" };
        public static Municipio CAUCA_PADILLA => new Municipio { Codigo = "19513", Nombre = "PADILLA" };
        public static Municipio CAUCA_PAEZ => new Municipio { Codigo = "19517", Nombre = "PÁEZ" };
        public static Municipio CAUCA_PATIA => new Municipio { Codigo = "19532", Nombre = "PATÍA" };
        public static Municipio CAUCA_PIAMONTE => new Municipio { Codigo = "19533", Nombre = "PIAMONTE" };
        public static Municipio CAUCA_PIENDAMO__TUNIA => new Municipio { Codigo = "19548", Nombre = "PIENDAMÓ – TUNÍA" };
        public static Municipio CAUCA_PUERTO_TEJADA => new Municipio { Codigo = "19573", Nombre = "PUERTO TEJADA" };
        public static Municipio CAUCA_PURACE => new Municipio { Codigo = "19585", Nombre = "PURACÉ" };
        public static Municipio CAUCA_ROSAS => new Municipio { Codigo = "19622", Nombre = "ROSAS" };
        public static Municipio CAUCA_SAN_SEBASTIAN => new Municipio { Codigo = "19693", Nombre = "SAN SEBASTIÁN" };
        public static Municipio CAUCA_SANTANDER_DE_QUILICHAO => new Municipio { Codigo = "19698", Nombre = "SANTANDER DE QUILICHAO" };
        public static Municipio CAUCA_SANTA_ROSA => new Municipio { Codigo = "19701", Nombre = "SANTA ROSA" };
        public static Municipio CAUCA_SILVIA => new Municipio { Codigo = "19743", Nombre = "SILVIA" };
        public static Municipio CAUCA_SOTARA => new Municipio { Codigo = "19760", Nombre = "SOTARA" };
        public static Municipio CAUCA_SUAREZ => new Municipio { Codigo = "19780", Nombre = "SUÁREZ" };
        public static Municipio CAUCA_SUCRE => new Municipio { Codigo = "19785", Nombre = "SUCRE" };
        public static Municipio CAUCA_TIMBIO => new Municipio { Codigo = "19807", Nombre = "TIMBÍO" };
        public static Municipio CAUCA_TIMBIQUI => new Municipio { Codigo = "19809", Nombre = "TIMBIQUÍ" };
        public static Municipio CAUCA_TORIBIO => new Municipio { Codigo = "19821", Nombre = "TORIBÍO" };
        public static Municipio CAUCA_TOTORO => new Municipio { Codigo = "19824", Nombre = "TOTORÓ" };
        public static Municipio CAUCA_VILLA_RICA => new Municipio { Codigo = "19845", Nombre = "VILLA RICA" };
        public static Municipio CESAR_VALLEDUPAR => new Municipio { Codigo = "20001", Nombre = "VALLEDUPAR" };
        public static Municipio CESAR_AGUACHICA => new Municipio { Codigo = "20011", Nombre = "AGUACHICA" };
        public static Municipio CESAR_AGUSTIN_CODAZZI => new Municipio { Codigo = "20013", Nombre = "AGUSTÍN CODAZZI" };
        public static Municipio CESAR_ASTREA => new Municipio { Codigo = "20032", Nombre = "ASTREA" };
        public static Municipio CESAR_BECERRIL => new Municipio { Codigo = "20045", Nombre = "BECERRIL" };
        public static Municipio CESAR_BOSCONIA => new Municipio { Codigo = "20060", Nombre = "BOSCONIA" };
        public static Municipio CESAR_CHIMICHAGUA => new Municipio { Codigo = "20175", Nombre = "CHIMICHAGUA" };
        public static Municipio CESAR_CHIRIGUANA => new Municipio { Codigo = "20178", Nombre = "CHIRIGUANÁ" };
        public static Municipio CESAR_CURUMANI => new Municipio { Codigo = "20228", Nombre = "CURUMANÍ" };
        public static Municipio CESAR_EL_COPEY => new Municipio { Codigo = "20238", Nombre = "EL COPEY" };
        public static Municipio CESAR_EL_PASO => new Municipio { Codigo = "20250", Nombre = "EL PASO" };
        public static Municipio CESAR_GAMARRA => new Municipio { Codigo = "20295", Nombre = "GAMARRA" };
        public static Municipio CESAR_GONZALEZ => new Municipio { Codigo = "20310", Nombre = "GONZÁLEZ" };
        public static Municipio CESAR_LA_GLORIA => new Municipio { Codigo = "20383", Nombre = "LA GLORIA" };
        public static Municipio CESAR_LA_JAGUA_DE_IBIRICO => new Municipio { Codigo = "20400", Nombre = "LA JAGUA DE IBIRICO" };
        public static Municipio CESAR_MANAURE_BALCON_DEL_CESAR => new Municipio { Codigo = "20443", Nombre = "MANAURE BALCÓN DEL CESAR" };
        public static Municipio CESAR_PAILITAS => new Municipio { Codigo = "20517", Nombre = "PAILITAS" };
        public static Municipio CESAR_PELAYA => new Municipio { Codigo = "20550", Nombre = "PELAYA" };
        public static Municipio CESAR_PUEBLO_BELLO => new Municipio { Codigo = "20570", Nombre = "PUEBLO BELLO" };
        public static Municipio CESAR_RIO_DE_ORO => new Municipio { Codigo = "20614", Nombre = "RÍO DE ORO" };
        public static Municipio CESAR_LA_PAZ => new Municipio { Codigo = "20621", Nombre = "LA PAZ" };
        public static Municipio CESAR_SAN_ALBERTO => new Municipio { Codigo = "20710", Nombre = "SAN ALBERTO" };
        public static Municipio CESAR_SAN_DIEGO => new Municipio { Codigo = "20750", Nombre = "SAN DIEGO" };
        public static Municipio CESAR_SAN_MARTIN => new Municipio { Codigo = "20770", Nombre = "SAN MARTÍN" };
        public static Municipio CESAR_TAMALAMEQUE => new Municipio { Codigo = "20787", Nombre = "TAMALAMEQUE" };
        public static Municipio CORDOBA_MONTERIA => new Municipio { Codigo = "23001", Nombre = "MONTERÍA" };
        public static Municipio CORDOBA_AYAPEL => new Municipio { Codigo = "23068", Nombre = "AYAPEL" };
        public static Municipio CORDOBA_BUENAVISTA => new Municipio { Codigo = "23079", Nombre = "BUENAVISTA" };
        public static Municipio CORDOBA_CANALETE => new Municipio { Codigo = "23090", Nombre = "CANALETE" };
        public static Municipio CORDOBA_CERETE => new Municipio { Codigo = "23162", Nombre = "CERETÉ" };
        public static Municipio CORDOBA_CHIMA => new Municipio { Codigo = "23168", Nombre = "CHIMÁ" };
        public static Municipio CORDOBA_CHINU => new Municipio { Codigo = "23182", Nombre = "CHINÚ" };
        public static Municipio CORDOBA_CIENAGA_DE_ORO => new Municipio { Codigo = "23189", Nombre = "CIÉNAGA DE ORO" };
        public static Municipio CORDOBA_COTORRA => new Municipio { Codigo = "23300", Nombre = "COTORRA" };
        public static Municipio CORDOBA_LA_APARTADA => new Municipio { Codigo = "23350", Nombre = "LA APARTADA" };
        public static Municipio CORDOBA_LORICA => new Municipio { Codigo = "23417", Nombre = "LORICA" };
        public static Municipio CORDOBA_LOS_CORDOBAS => new Municipio { Codigo = "23419", Nombre = "LOS CÓRDOBAS" };
        public static Municipio CORDOBA_MOMIL => new Municipio { Codigo = "23464", Nombre = "MOMIL" };
        public static Municipio CORDOBA_MONTELIBANO => new Municipio { Codigo = "23466", Nombre = "MONTELÍBANO" };
        public static Municipio CORDOBA_MOÑITOS => new Municipio { Codigo = "23500", Nombre = "MOÑITOS" };
        public static Municipio CORDOBA_PLANETA_RICA => new Municipio { Codigo = "23555", Nombre = "PLANETA RICA" };
        public static Municipio CORDOBA_PUEBLO_NUEVO => new Municipio { Codigo = "23570", Nombre = "PUEBLO NUEVO" };
        public static Municipio CORDOBA_PUERTO_ESCONDIDO => new Municipio { Codigo = "23574", Nombre = "PUERTO ESCONDIDO" };
        public static Municipio CORDOBA_PUERTO_LIBERTADOR => new Municipio { Codigo = "23580", Nombre = "PUERTO LIBERTADOR" };
        public static Municipio CORDOBA_PURISIMA_DE_LA_CONCEPCION => new Municipio { Codigo = "23586", Nombre = "PURÍSIMA DE LA CONCEPCIÓN" };
        public static Municipio CORDOBA_SAHAGUN => new Municipio { Codigo = "23660", Nombre = "SAHAGÚN" };
        public static Municipio CORDOBA_SAN_ANDRES_DE_SOTAVENTO => new Municipio { Codigo = "23670", Nombre = "SAN ANDRÉS DE SOTAVENTO" };
        public static Municipio CORDOBA_SAN_ANTERO => new Municipio { Codigo = "23672", Nombre = "SAN ANTERO" };
        public static Municipio CORDOBA_SAN_BERNARDO_DEL_VIENTO => new Municipio { Codigo = "23675", Nombre = "SAN BERNARDO DEL VIENTO" };
        public static Municipio CORDOBA_SAN_CARLOS => new Municipio { Codigo = "23678", Nombre = "SAN CARLOS" };
        public static Municipio CORDOBA_SAN_JOSE_DE_URE => new Municipio { Codigo = "23682", Nombre = "SAN JOSÉ DE URÉ" };
        public static Municipio CORDOBA_SAN_PELAYO => new Municipio { Codigo = "23686", Nombre = "SAN PELAYO" };
        public static Municipio CORDOBA_TIERRALTA => new Municipio { Codigo = "23807", Nombre = "TIERRALTA" };
        public static Municipio CORDOBA_TUCHIN => new Municipio { Codigo = "23815", Nombre = "TUCHÍN" };
        public static Municipio CORDOBA_VALENCIA => new Municipio { Codigo = "23855", Nombre = "VALENCIA" };
        public static Municipio CUNDINAMARCA_AGUA_DE_DIOS => new Municipio { Codigo = "25001", Nombre = "AGUA DE DIOS" };
        public static Municipio CUNDINAMARCA_ALBAN => new Municipio { Codigo = "25019", Nombre = "ALBÁN" };
        public static Municipio CUNDINAMARCA_ANAPOIMA => new Municipio { Codigo = "25035", Nombre = "ANAPOIMA" };
        public static Municipio CUNDINAMARCA_ANOLAIMA => new Municipio { Codigo = "25040", Nombre = "ANOLAIMA" };
        public static Municipio CUNDINAMARCA_ARBELAEZ => new Municipio { Codigo = "25053", Nombre = "ARBELÁEZ" };
        public static Municipio CUNDINAMARCA_BELTRAN => new Municipio { Codigo = "25086", Nombre = "BELTRÁN" };
        public static Municipio CUNDINAMARCA_BITUIMA => new Municipio { Codigo = "25095", Nombre = "BITUIMA" };
        public static Municipio CUNDINAMARCA_BOJACA => new Municipio { Codigo = "25099", Nombre = "BOJACÁ" };
        public static Municipio CUNDINAMARCA_CABRERA => new Municipio { Codigo = "25120", Nombre = "CABRERA" };
        public static Municipio CUNDINAMARCA_CACHIPAY => new Municipio { Codigo = "25123", Nombre = "CACHIPAY" };
        public static Municipio CUNDINAMARCA_CAJICA => new Municipio { Codigo = "25126", Nombre = "CAJICÁ" };
        public static Municipio CUNDINAMARCA_CAPARRAPI => new Municipio { Codigo = "25148", Nombre = "CAPARRAPÍ" };
        public static Municipio CUNDINAMARCA_CAQUEZA => new Municipio { Codigo = "25151", Nombre = "CÁQUEZA" };
        public static Municipio CUNDINAMARCA_CARMEN_DE_CARUPA => new Municipio { Codigo = "25154", Nombre = "CARMEN DE CARUPA" };
        public static Municipio CUNDINAMARCA_CHAGUANI => new Municipio { Codigo = "25168", Nombre = "CHAGUANÍ" };
        public static Municipio CUNDINAMARCA_CHIA => new Municipio { Codigo = "25175", Nombre = "CHÍA" };
        public static Municipio CUNDINAMARCA_CHIPAQUE => new Municipio { Codigo = "25178", Nombre = "CHIPAQUE" };
        public static Municipio CUNDINAMARCA_CHOACHI => new Municipio { Codigo = "25181", Nombre = "CHOACHÍ" };
        public static Municipio CUNDINAMARCA_CHOCONTA => new Municipio { Codigo = "25183", Nombre = "CHOCONTÁ" };
        public static Municipio CUNDINAMARCA_COGUA => new Municipio { Codigo = "25200", Nombre = "COGUA" };
        public static Municipio CUNDINAMARCA_COTA => new Municipio { Codigo = "25214", Nombre = "COTA" };
        public static Municipio CUNDINAMARCA_CUCUNUBA => new Municipio { Codigo = "25224", Nombre = "CUCUNUBÁ" };
        public static Municipio CUNDINAMARCA_EL_COLEGIO => new Municipio { Codigo = "25245", Nombre = "EL COLEGIO" };
        public static Municipio CUNDINAMARCA_EL_PEÑON => new Municipio { Codigo = "25258", Nombre = "EL PEÑÓN" };
        public static Municipio CUNDINAMARCA_EL_ROSAL => new Municipio { Codigo = "25260", Nombre = "EL ROSAL" };
        public static Municipio CUNDINAMARCA_FACATATIVA => new Municipio { Codigo = "25269", Nombre = "FACATATIVÁ" };
        public static Municipio CUNDINAMARCA_FOMEQUE => new Municipio { Codigo = "25279", Nombre = "FÓMEQUE" };
        public static Municipio CUNDINAMARCA_FOSCA => new Municipio { Codigo = "25281", Nombre = "FOSCA" };
        public static Municipio CUNDINAMARCA_FUNZA => new Municipio { Codigo = "25286", Nombre = "FUNZA" };
        public static Municipio CUNDINAMARCA_FUQUENE => new Municipio { Codigo = "25288", Nombre = "FÚQUENE" };
        public static Municipio CUNDINAMARCA_FUSAGASUGA => new Municipio { Codigo = "25290", Nombre = "FUSAGASUGÁ" };
        public static Municipio CUNDINAMARCA_GACHALA => new Municipio { Codigo = "25293", Nombre = "GACHALÁ" };
        public static Municipio CUNDINAMARCA_GACHANCIPA => new Municipio { Codigo = "25295", Nombre = "GACHANCIPÁ" };
        public static Municipio CUNDINAMARCA_GACHETA => new Municipio { Codigo = "25297", Nombre = "GACHETÁ" };
        public static Municipio CUNDINAMARCA_GAMA => new Municipio { Codigo = "25299", Nombre = "GAMA" };
        public static Municipio CUNDINAMARCA_GIRARDOT => new Municipio { Codigo = "25307", Nombre = "GIRARDOT" };
        public static Municipio CUNDINAMARCA_GRANADA => new Municipio { Codigo = "25312", Nombre = "GRANADA" };
        public static Municipio CUNDINAMARCA_GUACHETA => new Municipio { Codigo = "25317", Nombre = "GUACHETÁ" };
        public static Municipio CUNDINAMARCA_GUADUAS => new Municipio { Codigo = "25320", Nombre = "GUADUAS" };
        public static Municipio CUNDINAMARCA_GUASCA => new Municipio { Codigo = "25322", Nombre = "GUASCA" };
        public static Municipio CUNDINAMARCA_GUATAQUI => new Municipio { Codigo = "25324", Nombre = "GUATAQUÍ" };
        public static Municipio CUNDINAMARCA_GUATAVITA => new Municipio { Codigo = "25326", Nombre = "GUATAVITA" };
        public static Municipio CUNDINAMARCA_GUAYABAL_DE_SIQUIMA => new Municipio { Codigo = "25328", Nombre = "GUAYABAL DE SÍQUIMA" };
        public static Municipio CUNDINAMARCA_GUAYABETAL => new Municipio { Codigo = "25335", Nombre = "GUAYABETAL" };
        public static Municipio CUNDINAMARCA_GUTIERREZ => new Municipio { Codigo = "25339", Nombre = "GUTIÉRREZ" };
        public static Municipio CUNDINAMARCA_JERUSALEN => new Municipio { Codigo = "25368", Nombre = "JERUSALÉN" };
        public static Municipio CUNDINAMARCA_JUNIN => new Municipio { Codigo = "25372", Nombre = "JUNÍN" };
        public static Municipio CUNDINAMARCA_LA_CALERA => new Municipio { Codigo = "25377", Nombre = "LA CALERA" };
        public static Municipio CUNDINAMARCA_LA_MESA => new Municipio { Codigo = "25386", Nombre = "LA MESA" };
        public static Municipio CUNDINAMARCA_LA_PALMA => new Municipio { Codigo = "25394", Nombre = "LA PALMA" };
        public static Municipio CUNDINAMARCA_LA_PEÑA => new Municipio { Codigo = "25398", Nombre = "LA PEÑA" };
        public static Municipio CUNDINAMARCA_LA_VEGA => new Municipio { Codigo = "25402", Nombre = "LA VEGA" };
        public static Municipio CUNDINAMARCA_LENGUAZAQUE => new Municipio { Codigo = "25407", Nombre = "LENGUAZAQUE" };
        public static Municipio CUNDINAMARCA_MACHETA => new Municipio { Codigo = "25426", Nombre = "MACHETÁ" };
        public static Municipio CUNDINAMARCA_MADRID => new Municipio { Codigo = "25430", Nombre = "MADRID" };
        public static Municipio CUNDINAMARCA_MANTA => new Municipio { Codigo = "25436", Nombre = "MANTA" };
        public static Municipio CUNDINAMARCA_MEDINA => new Municipio { Codigo = "25438", Nombre = "MEDINA" };
        public static Municipio CUNDINAMARCA_MOSQUERA => new Municipio { Codigo = "25473", Nombre = "MOSQUERA" };
        public static Municipio CUNDINAMARCA_NARIÑO => new Municipio { Codigo = "25483", Nombre = "NARIÑO" };
        public static Municipio CUNDINAMARCA_NEMOCON => new Municipio { Codigo = "25486", Nombre = "NEMOCÓN" };
        public static Municipio CUNDINAMARCA_NILO => new Municipio { Codigo = "25488", Nombre = "NILO" };
        public static Municipio CUNDINAMARCA_NIMAIMA => new Municipio { Codigo = "25489", Nombre = "NIMAIMA" };
        public static Municipio CUNDINAMARCA_NOCAIMA => new Municipio { Codigo = "25491", Nombre = "NOCAIMA" };
        public static Municipio CUNDINAMARCA_VENECIA => new Municipio { Codigo = "25506", Nombre = "VENECIA" };
        public static Municipio CUNDINAMARCA_PACHO => new Municipio { Codigo = "25513", Nombre = "PACHO" };
        public static Municipio CUNDINAMARCA_PAIME => new Municipio { Codigo = "25518", Nombre = "PAIME" };
        public static Municipio CUNDINAMARCA_PANDI => new Municipio { Codigo = "25524", Nombre = "PANDI" };
        public static Municipio CUNDINAMARCA_PARATEBUENO => new Municipio { Codigo = "25530", Nombre = "PARATEBUENO" };
        public static Municipio CUNDINAMARCA_PASCA => new Municipio { Codigo = "25535", Nombre = "PASCA" };
        public static Municipio CUNDINAMARCA_PUERTO_SALGAR => new Municipio { Codigo = "25572", Nombre = "PUERTO SALGAR" };
        public static Municipio CUNDINAMARCA_PULI => new Municipio { Codigo = "25580", Nombre = "PULÍ" };
        public static Municipio CUNDINAMARCA_QUEBRADANEGRA => new Municipio { Codigo = "25592", Nombre = "QUEBRADANEGRA" };
        public static Municipio CUNDINAMARCA_QUETAME => new Municipio { Codigo = "25594", Nombre = "QUETAME" };
        public static Municipio CUNDINAMARCA_QUIPILE => new Municipio { Codigo = "25596", Nombre = "QUIPILE" };
        public static Municipio CUNDINAMARCA_APULO => new Municipio { Codigo = "25599", Nombre = "APULO" };
        public static Municipio CUNDINAMARCA_RICAURTE => new Municipio { Codigo = "25612", Nombre = "RICAURTE" };
        public static Municipio _DEL => new Municipio { Codigo = "ANTONIO", Nombre = "DEL" };
        public static Municipio CUNDINAMARCA_SAN_BERNARDO => new Municipio { Codigo = "25649", Nombre = "SAN BERNARDO" };
        public static Municipio CUNDINAMARCA_SAN_CAYETANO => new Municipio { Codigo = "25653", Nombre = "SAN CAYETANO" };
        public static Municipio CUNDINAMARCA_SAN_FRANCISCO => new Municipio { Codigo = "25658", Nombre = "SAN FRANCISCO" };
        public static Municipio CUNDINAMARCA_SAN_JUAN_DE_RIOSECO => new Municipio { Codigo = "25662", Nombre = "SAN JUAN DE RIOSECO" };
        public static Municipio CUNDINAMARCA_SASAIMA => new Municipio { Codigo = "25718", Nombre = "SASAIMA" };
        public static Municipio CUNDINAMARCA_SESQUILE => new Municipio { Codigo = "25736", Nombre = "SESQUILÉ" };
        public static Municipio CUNDINAMARCA_SIBATE => new Municipio { Codigo = "25740", Nombre = "SIBATÉ" };
        public static Municipio CUNDINAMARCA_SILVANIA => new Municipio { Codigo = "25743", Nombre = "SILVANIA" };
        public static Municipio CUNDINAMARCA_SIMIJACA => new Municipio { Codigo = "25745", Nombre = "SIMIJACA" };
        public static Municipio CUNDINAMARCA_SOACHA => new Municipio { Codigo = "25754", Nombre = "SOACHA" };
        public static Municipio CUNDINAMARCA_SOPO => new Municipio { Codigo = "25758", Nombre = "SOPÓ" };
        public static Municipio CUNDINAMARCA_SUBACHOQUE => new Municipio { Codigo = "25769", Nombre = "SUBACHOQUE" };
        public static Municipio CUNDINAMARCA_SUESCA => new Municipio { Codigo = "25772", Nombre = "SUESCA" };
        public static Municipio CUNDINAMARCA_SUPATA => new Municipio { Codigo = "25777", Nombre = "SUPATÁ" };
        public static Municipio CUNDINAMARCA_SUSA => new Municipio { Codigo = "25779", Nombre = "SUSA" };
        public static Municipio CUNDINAMARCA_SUTATAUSA => new Municipio { Codigo = "25781", Nombre = "SUTATAUSA" };
        public static Municipio CUNDINAMARCA_TABIO => new Municipio { Codigo = "25785", Nombre = "TABIO" };
        public static Municipio CUNDINAMARCA_TAUSA => new Municipio { Codigo = "25793", Nombre = "TAUSA" };
        public static Municipio CUNDINAMARCA_TENA => new Municipio { Codigo = "25797", Nombre = "TENA" };
        public static Municipio CUNDINAMARCA_TENJO => new Municipio { Codigo = "25799", Nombre = "TENJO" };
        public static Municipio CUNDINAMARCA_TIBACUY => new Municipio { Codigo = "25805", Nombre = "TIBACUY" };
        public static Municipio CUNDINAMARCA_TIBIRITA => new Municipio { Codigo = "25807", Nombre = "TIBIRITA" };
        public static Municipio CUNDINAMARCA_TOCAIMA => new Municipio { Codigo = "25815", Nombre = "TOCAIMA" };
        public static Municipio CUNDINAMARCA_TOCANCIPA => new Municipio { Codigo = "25817", Nombre = "TOCANCIPÁ" };
        public static Municipio CUNDINAMARCA_TOPAIPI => new Municipio { Codigo = "25823", Nombre = "TOPAIPÍ" };
        public static Municipio CUNDINAMARCA_UBALA => new Municipio { Codigo = "25839", Nombre = "UBALÁ" };
        public static Municipio CUNDINAMARCA_UBAQUE => new Municipio { Codigo = "25841", Nombre = "UBAQUE" };
        public static Municipio CUNDINAMARCA_VILLA_DE_SAN_DIEGO_DE_UBATE => new Municipio { Codigo = "25843", Nombre = "VILLA DE SAN DIEGO DE UBATÉ" };
        public static Municipio CUNDINAMARCA_UNE => new Municipio { Codigo = "25845", Nombre = "UNE" };
        public static Municipio CUNDINAMARCA_UTICA => new Municipio { Codigo = "25851", Nombre = "ÚTICA" };
        public static Municipio CUNDINAMARCA_VERGARA => new Municipio { Codigo = "25862", Nombre = "VERGARA" };
        public static Municipio CUNDINAMARCA_VIANI => new Municipio { Codigo = "25867", Nombre = "VIANÍ" };
        public static Municipio CUNDINAMARCA_VILLAGOMEZ => new Municipio { Codigo = "25871", Nombre = "VILLAGÓMEZ" };
        public static Municipio CUNDINAMARCA_VILLAPINZON => new Municipio { Codigo = "25873", Nombre = "VILLAPINZÓN" };
        public static Municipio CUNDINAMARCA_VILLETA => new Municipio { Codigo = "25875", Nombre = "VILLETA" };
        public static Municipio CUNDINAMARCA_VIOTA => new Municipio { Codigo = "25878", Nombre = "VIOTÁ" };
        public static Municipio CUNDINAMARCA_YACOPI => new Municipio { Codigo = "25885", Nombre = "YACOPÍ" };
        public static Municipio CUNDINAMARCA_ZIPACON => new Municipio { Codigo = "25898", Nombre = "ZIPACÓN" };
        public static Municipio CUNDINAMARCA_ZIPAQUIRA => new Municipio { Codigo = "25899", Nombre = "ZIPAQUIRÁ" };
        public static Municipio CHOCO_QUIBDO => new Municipio { Codigo = "27001", Nombre = "QUIBDÓ" };
        public static Municipio CHOCO_ACANDI => new Municipio { Codigo = "27006", Nombre = "ACANDÍ" };
        public static Municipio CHOCO_ALTO_BAUDO => new Municipio { Codigo = "27025", Nombre = "ALTO BAUDÓ" };
        public static Municipio CHOCO_ATRATO => new Municipio { Codigo = "27050", Nombre = "ATRATO" };
        public static Municipio CHOCO_BAGADO => new Municipio { Codigo = "27073", Nombre = "BAGADÓ" };
        public static Municipio CHOCO_BAHIA_SOLANO => new Municipio { Codigo = "27075", Nombre = "BAHÍA SOLANO" };
        public static Municipio CHOCO_BAJO_BAUDO => new Municipio { Codigo = "27077", Nombre = "BAJO BAUDÓ" };
        public static Municipio CHOCO_BOJAYA => new Municipio { Codigo = "27099", Nombre = "BOJAYÁ" };
        public static Municipio CHOCO_EL_CANTON_DEL_SAN_PABLO => new Municipio { Codigo = "27135", Nombre = "EL CANTÓN DEL SAN PABLO" };
        public static Municipio CHOCO_CARMEN_DEL_DARIEN => new Municipio { Codigo = "27150", Nombre = "CARMEN DEL DARIÉN" };
        public static Municipio CHOCO_CERTEGUI => new Municipio { Codigo = "27160", Nombre = "CÉRTEGUI" };
        public static Municipio CHOCO_CONDOTO => new Municipio { Codigo = "27205", Nombre = "CONDOTO" };
        public static Municipio CHOCO_EL_CARMEN_DE_ATRATO => new Municipio { Codigo = "27245", Nombre = "EL CARMEN DE ATRATO" };
        public static Municipio CHOCO_EL_LITORAL_DEL_SAN_JUAN => new Municipio { Codigo = "27250", Nombre = "EL LITORAL DEL SAN JUAN" };
        public static Municipio CHOCO_ISTMINA => new Municipio { Codigo = "27361", Nombre = "ISTMINA" };
        public static Municipio CHOCO_JURADO => new Municipio { Codigo = "27372", Nombre = "JURADÓ" };
        public static Municipio CHOCO_LLORO => new Municipio { Codigo = "27413", Nombre = "LLORÓ" };
        public static Municipio CHOCO_MEDIO_ATRATO => new Municipio { Codigo = "27425", Nombre = "MEDIO ATRATO" };
        public static Municipio CHOCO_MEDIO_BAUDO => new Municipio { Codigo = "27430", Nombre = "MEDIO BAUDÓ" };
        public static Municipio CHOCO_MEDIO_SAN_JUAN => new Municipio { Codigo = "27450", Nombre = "MEDIO SAN JUAN" };
        public static Municipio CHOCO_NOVITA => new Municipio { Codigo = "27491", Nombre = "NÓVITA" };
        public static Municipio CHOCO_NUQUI => new Municipio { Codigo = "27495", Nombre = "NUQUÍ" };
        public static Municipio CHOCO_RIO_IRO => new Municipio { Codigo = "27580", Nombre = "RÍO IRÓ" };
        public static Municipio CHOCO_RIO_QUITO => new Municipio { Codigo = "27600", Nombre = "RÍO QUITO" };
        public static Municipio CHOCO_RIOSUCIO => new Municipio { Codigo = "27615", Nombre = "RIOSUCIO" };
        public static Municipio CHOCO_SAN_JOSE_DEL_PALMAR => new Municipio { Codigo = "27660", Nombre = "SAN JOSÉ DEL PALMAR" };
        public static Municipio CHOCO_SIPI => new Municipio { Codigo = "27745", Nombre = "SIPÍ" };
        public static Municipio CHOCO_TADO => new Municipio { Codigo = "27787", Nombre = "TADÓ" };
        public static Municipio CHOCO_UNGUIA => new Municipio { Codigo = "27800", Nombre = "UNGUÍA" };
        public static Municipio CHOCO_UNION_PANAMERICANA => new Municipio { Codigo = "27810", Nombre = "UNIÓN PANAMERICANA" };
        public static Municipio HUILA_NEIVA => new Municipio { Codigo = "41001", Nombre = "NEIVA" };
        public static Municipio HUILA_ACEVEDO => new Municipio { Codigo = "41006", Nombre = "ACEVEDO" };
        public static Municipio HUILA_AGRADO => new Municipio { Codigo = "41013", Nombre = "AGRADO" };
        public static Municipio HUILA_AIPE => new Municipio { Codigo = "41016", Nombre = "AIPE" };
        public static Municipio HUILA_ALGECIRAS => new Municipio { Codigo = "41020", Nombre = "ALGECIRAS" };
        public static Municipio HUILA_ALTAMIRA => new Municipio { Codigo = "41026", Nombre = "ALTAMIRA" };
        public static Municipio HUILA_BARAYA => new Municipio { Codigo = "41078", Nombre = "BARAYA" };
        public static Municipio HUILA_CAMPOALEGRE => new Municipio { Codigo = "41132", Nombre = "CAMPOALEGRE" };
        public static Municipio HUILA_COLOMBIA => new Municipio { Codigo = "41206", Nombre = "COLOMBIA" };
        public static Municipio HUILA_ELIAS => new Municipio { Codigo = "41244", Nombre = "ELÍAS" };
        public static Municipio HUILA_GARZON => new Municipio { Codigo = "41298", Nombre = "GARZÓN" };
        public static Municipio HUILA_GIGANTE => new Municipio { Codigo = "41306", Nombre = "GIGANTE" };
        public static Municipio HUILA_GUADALUPE => new Municipio { Codigo = "41319", Nombre = "GUADALUPE" };
        public static Municipio HUILA_HOBO => new Municipio { Codigo = "41349", Nombre = "HOBO" };
        public static Municipio HUILA_IQUIRA => new Municipio { Codigo = "41357", Nombre = "ÍQUIRA" };
        public static Municipio HUILA_ISNOS => new Municipio { Codigo = "41359", Nombre = "ISNOS" };
        public static Municipio HUILA_LA_ARGENTINA => new Municipio { Codigo = "41378", Nombre = "LA ARGENTINA" };
        public static Municipio HUILA_LA_PLATA => new Municipio { Codigo = "41396", Nombre = "LA PLATA" };
        public static Municipio HUILA_NATAGA => new Municipio { Codigo = "41483", Nombre = "NÁTAGA" };
        public static Municipio HUILA_OPORAPA => new Municipio { Codigo = "41503", Nombre = "OPORAPA" };
        public static Municipio HUILA_PAICOL => new Municipio { Codigo = "41518", Nombre = "PAICOL" };
        public static Municipio HUILA_PALERMO => new Municipio { Codigo = "41524", Nombre = "PALERMO" };
        public static Municipio HUILA_PALESTINA => new Municipio { Codigo = "41530", Nombre = "PALESTINA" };
        public static Municipio HUILA_PITAL => new Municipio { Codigo = "41548", Nombre = "PITAL" };
        public static Municipio HUILA_PITALITO => new Municipio { Codigo = "41551", Nombre = "PITALITO" };
        public static Municipio HUILA_RIVERA => new Municipio { Codigo = "41615", Nombre = "RIVERA" };
        public static Municipio HUILA_SALADOBLANCO => new Municipio { Codigo = "41660", Nombre = "SALADOBLANCO" };
        public static Municipio HUILA_SAN_AGUSTIN => new Municipio { Codigo = "41668", Nombre = "SAN AGUSTÍN" };
        public static Municipio HUILA_SANTA_MARIA => new Municipio { Codigo = "41676", Nombre = "SANTA MARÍA" };
        public static Municipio HUILA_SUAZA => new Municipio { Codigo = "41770", Nombre = "SUAZA" };
        public static Municipio HUILA_TARQUI => new Municipio { Codigo = "41791", Nombre = "TARQUI" };
        public static Municipio HUILA_TESALIA => new Municipio { Codigo = "41797", Nombre = "TESALIA" };
        public static Municipio HUILA_TELLO => new Municipio { Codigo = "41799", Nombre = "TELLO" };
        public static Municipio HUILA_TERUEL => new Municipio { Codigo = "41801", Nombre = "TERUEL" };
        public static Municipio HUILA_TIMANA => new Municipio { Codigo = "41807", Nombre = "TIMANÁ" };
        public static Municipio HUILA_VILLAVIEJA => new Municipio { Codigo = "41872", Nombre = "VILLAVIEJA" };
        public static Municipio HUILA_YAGUARA => new Municipio { Codigo = "41885", Nombre = "YAGUARÁ" };
        public static Municipio LA_GUAJIRA_RIOHACHA => new Municipio { Codigo = "44001", Nombre = "RIOHACHA" };
        public static Municipio LA_GUAJIRA_ALBANIA => new Municipio { Codigo = "44035", Nombre = "ALBANIA" };
        public static Municipio LA_GUAJIRA_BARRANCAS => new Municipio { Codigo = "44078", Nombre = "BARRANCAS" };
        public static Municipio LA_GUAJIRA_DIBULLA => new Municipio { Codigo = "44090", Nombre = "DIBULLA" };
        public static Municipio LA_GUAJIRA_DISTRACCION => new Municipio { Codigo = "44098", Nombre = "DISTRACCIÓN" };
        public static Municipio LA_GUAJIRA_EL_MOLINO => new Municipio { Codigo = "44110", Nombre = "EL MOLINO" };
        public static Municipio LA_GUAJIRA_FONSECA => new Municipio { Codigo = "44279", Nombre = "FONSECA" };
        public static Municipio LA_GUAJIRA_HATONUEVO => new Municipio { Codigo = "44378", Nombre = "HATONUEVO" };
        public static Municipio LA_GUAJIRA_LA_JAGUA_DEL_PILAR => new Municipio { Codigo = "44420", Nombre = "LA JAGUA DEL PILAR" };
        public static Municipio LA_GUAJIRA_MAICAO => new Municipio { Codigo = "44430", Nombre = "MAICAO" };
        public static Municipio LA_GUAJIRA_MANAURE => new Municipio { Codigo = "44560", Nombre = "MANAURE" };
        public static Municipio LA_GUAJIRA_SAN_JUAN_DEL_CESAR => new Municipio { Codigo = "44650", Nombre = "SAN JUAN DEL CESAR" };
        public static Municipio LA_GUAJIRA_URIBIA => new Municipio { Codigo = "44847", Nombre = "URIBIA" };
        public static Municipio LA_GUAJIRA_URUMITA => new Municipio { Codigo = "44855", Nombre = "URUMITA" };
        public static Municipio LA_GUAJIRA_VILLANUEVA => new Municipio { Codigo = "44874", Nombre = "VILLANUEVA" };
        public static Municipio MAGDALENA_SANTA_MARTA => new Municipio { Codigo = "47001", Nombre = "SANTA MARTA" };
        public static Municipio MAGDALENA_ALGARROBO => new Municipio { Codigo = "47030", Nombre = "ALGARROBO" };
        public static Municipio MAGDALENA_ARACATACA => new Municipio { Codigo = "47053", Nombre = "ARACATACA" };
        public static Municipio MAGDALENA_ARIGUANI => new Municipio { Codigo = "47058", Nombre = "ARIGUANÍ" };
        public static Municipio MAGDALENA_CERRO_DE_SAN_ANTONIO => new Municipio { Codigo = "47161", Nombre = "CERRO DE SAN ANTONIO" };
        public static Municipio MAGDALENA_CHIVOLO => new Municipio { Codigo = "47170", Nombre = "CHIVOLO" };
        public static Municipio MAGDALENA_CIENAGA => new Municipio { Codigo = "47189", Nombre = "CIÉNAGA" };
        public static Municipio MAGDALENA_CONCORDIA => new Municipio { Codigo = "47205", Nombre = "CONCORDIA" };
        public static Municipio MAGDALENA_EL_BANCO => new Municipio { Codigo = "47245", Nombre = "EL BANCO" };
        public static Municipio MAGDALENA_EL_PIÑON => new Municipio { Codigo = "47258", Nombre = "EL PIÑÓN" };
        public static Municipio MAGDALENA_EL_RETEN => new Municipio { Codigo = "47268", Nombre = "EL RETÉN" };
        public static Municipio MAGDALENA_FUNDACION => new Municipio { Codigo = "47288", Nombre = "FUNDACIÓN" };
        public static Municipio MAGDALENA_GUAMAL => new Municipio { Codigo = "47318", Nombre = "GUAMAL" };
        public static Municipio MAGDALENA_NUEVA_GRANADA => new Municipio { Codigo = "47460", Nombre = "NUEVA GRANADA" };
        public static Municipio MAGDALENA_PEDRAZA => new Municipio { Codigo = "47541", Nombre = "PEDRAZA" };
        public static Municipio MAGDALENA_PIJIÑO_DEL_CARMEN => new Municipio { Codigo = "47545", Nombre = "PIJIÑO DEL CARMEN" };
        public static Municipio MAGDALENA_PIVIJAY => new Municipio { Codigo = "47551", Nombre = "PIVIJAY" };
        public static Municipio MAGDALENA_PLATO => new Municipio { Codigo = "47555", Nombre = "PLATO" };
        public static Municipio MAGDALENA_PUEBLOVIEJO => new Municipio { Codigo = "47570", Nombre = "PUEBLOVIEJO" };
        public static Municipio MAGDALENA_REMOLINO => new Municipio { Codigo = "47605", Nombre = "REMOLINO" };
        public static Municipio MAGDALENA_SABANAS_DE_SAN_ANGEL => new Municipio { Codigo = "47660", Nombre = "SABANAS DE SAN ÁNGEL" };
        public static Municipio MAGDALENA_SALAMINA => new Municipio { Codigo = "47675", Nombre = "SALAMINA" };
        public static Municipio MAGDALENA_SAN_SEBASTIAN_DE_BUENAVISTA => new Municipio { Codigo = "47692", Nombre = "SAN SEBASTIÁN DE BUENAVISTA" };
        public static Municipio MAGDALENA_SAN_ZENON => new Municipio { Codigo = "47703", Nombre = "SAN ZENÓN" };
        public static Municipio MAGDALENA_SANTA_ANA => new Municipio { Codigo = "47707", Nombre = "SANTA ANA" };
        public static Municipio MAGDALENA_SANTA_BARBARA_DE_PINTO => new Municipio { Codigo = "47720", Nombre = "SANTA BÁRBARA DE PINTO" };
        public static Municipio MAGDALENA_SITIONUEVO => new Municipio { Codigo = "47745", Nombre = "SITIONUEVO" };
        public static Municipio MAGDALENA_TENERIFE => new Municipio { Codigo = "47798", Nombre = "TENERIFE" };
        public static Municipio MAGDALENA_ZAPAYAN => new Municipio { Codigo = "47960", Nombre = "ZAPAYÁN" };
        public static Municipio MAGDALENA_ZONA_BANANERA => new Municipio { Codigo = "47980", Nombre = "ZONA BANANERA" };
        public static Municipio META_VILLAVICENCIO => new Municipio { Codigo = "50001", Nombre = "VILLAVICENCIO" };
        public static Municipio META_ACACIAS => new Municipio { Codigo = "50006", Nombre = "ACACÍAS" };
        public static Municipio META_BARRANCA_DE_UPIA => new Municipio { Codigo = "50110", Nombre = "BARRANCA DE UPÍA" };
        public static Municipio META_CABUYARO => new Municipio { Codigo = "50124", Nombre = "CABUYARO" };
        public static Municipio META_CASTILLA_LA_NUEVA => new Municipio { Codigo = "50150", Nombre = "CASTILLA LA NUEVA" };
        public static Municipio META_CUBARRAL => new Municipio { Codigo = "50223", Nombre = "CUBARRAL" };
        public static Municipio META_CUMARAL => new Municipio { Codigo = "50226", Nombre = "CUMARAL" };
        public static Municipio META_EL_CALVARIO => new Municipio { Codigo = "50245", Nombre = "EL CALVARIO" };
        public static Municipio META_EL_CASTILLO => new Municipio { Codigo = "50251", Nombre = "EL CASTILLO" };
        public static Municipio META_EL_DORADO => new Municipio { Codigo = "50270", Nombre = "EL DORADO" };
        public static Municipio META_FUENTEDEORO => new Municipio { Codigo = "50287", Nombre = "FUENTEDEORO" };
        public static Municipio META_GRANADA => new Municipio { Codigo = "50313", Nombre = "GRANADA" };
        public static Municipio META_GUAMAL => new Municipio { Codigo = "50318", Nombre = "GUAMAL" };
        public static Municipio META_MAPIRIPAN => new Municipio { Codigo = "50325", Nombre = "MAPIRIPÁN" };
        public static Municipio META_MESETAS => new Municipio { Codigo = "50330", Nombre = "MESETAS" };
        public static Municipio META_LA_MACARENA => new Municipio { Codigo = "50350", Nombre = "LA MACARENA" };
        public static Municipio META_URIBE => new Municipio { Codigo = "50370", Nombre = "URIBE" };
        public static Municipio META_LEJANIAS => new Municipio { Codigo = "50400", Nombre = "LEJANÍAS" };
        public static Municipio META_PUERTO_CONCORDIA => new Municipio { Codigo = "50450", Nombre = "PUERTO CONCORDIA" };
        public static Municipio META_PUERTO_GAITAN => new Municipio { Codigo = "50568", Nombre = "PUERTO GAITÁN" };
        public static Municipio META_PUERTO_LOPEZ => new Municipio { Codigo = "50573", Nombre = "PUERTO LÓPEZ" };
        public static Municipio META_PUERTO_LLERAS => new Municipio { Codigo = "50577", Nombre = "PUERTO LLERAS" };
        public static Municipio META_PUERTO_RICO => new Municipio { Codigo = "50590", Nombre = "PUERTO RICO" };
        public static Municipio META_RESTREPO => new Municipio { Codigo = "50606", Nombre = "RESTREPO" };
        public static Municipio META_SAN_CARLOS_DE_GUAROA => new Municipio { Codigo = "50680", Nombre = "SAN CARLOS DE GUAROA" };
        public static Municipio META_SAN_JUAN_DE_ARAMA => new Municipio { Codigo = "50683", Nombre = "SAN JUAN DE ARAMA" };
        public static Municipio META_SAN_JUANITO => new Municipio { Codigo = "50686", Nombre = "SAN JUANITO" };
        public static Municipio META_SAN_MARTIN => new Municipio { Codigo = "50689", Nombre = "SAN MARTÍN" };
        public static Municipio META_VISTAHERMOSA => new Municipio { Codigo = "50711", Nombre = "VISTAHERMOSA" };
        public static Municipio NARIÑO_PASTO => new Municipio { Codigo = "52001", Nombre = "PASTO" };
        public static Municipio NARIÑO_ALBAN => new Municipio { Codigo = "52019", Nombre = "ALBÁN" };
        public static Municipio NARIÑO_ALDANA => new Municipio { Codigo = "52022", Nombre = "ALDANA" };
        public static Municipio NARIÑO_ANCUYA => new Municipio { Codigo = "52036", Nombre = "ANCUYÁ" };
        public static Municipio NARIÑO_ARBOLEDA => new Municipio { Codigo = "52051", Nombre = "ARBOLEDA" };
        public static Municipio NARIÑO_BARBACOAS => new Municipio { Codigo = "52079", Nombre = "BARBACOAS" };
        public static Municipio NARIÑO_BELEN => new Municipio { Codigo = "52083", Nombre = "BELÉN" };
        public static Municipio NARIÑO_BUESACO => new Municipio { Codigo = "52110", Nombre = "BUESACO" };
        public static Municipio NARIÑO_COLON => new Municipio { Codigo = "52203", Nombre = "COLÓN" };
        public static Municipio NARIÑO_CONSACA => new Municipio { Codigo = "52207", Nombre = "CONSACÁ" };
        public static Municipio NARIÑO_CONTADERO => new Municipio { Codigo = "52210", Nombre = "CONTADERO" };
        public static Municipio NARIÑO_CORDOBA => new Municipio { Codigo = "52215", Nombre = "CÓRDOBA" };
        public static Municipio NARIÑO_CUASPUD => new Municipio { Codigo = "52224", Nombre = "CUASPÚD" };
        public static Municipio NARIÑO_CUMBAL => new Municipio { Codigo = "52227", Nombre = "CUMBAL" };
        public static Municipio NARIÑO_CUMBITARA => new Municipio { Codigo = "52233", Nombre = "CUMBITARA" };
        public static Municipio NARIÑO_CHACHAGÜI => new Municipio { Codigo = "52240", Nombre = "CHACHAGÜÍ" };
        public static Municipio NARIÑO_EL_CHARCO => new Municipio { Codigo = "52250", Nombre = "EL CHARCO" };
        public static Municipio NARIÑO_EL_PEÑOL => new Municipio { Codigo = "52254", Nombre = "EL PEÑOL" };
        public static Municipio NARIÑO_EL_ROSARIO => new Municipio { Codigo = "52256", Nombre = "EL ROSARIO" };
        public static Municipio NARIÑO_EL_TABLON_DE_GOMEZ => new Municipio { Codigo = "52258", Nombre = "EL TABLÓN DE GÓMEZ" };
        public static Municipio NARIÑO_EL_TAMBO => new Municipio { Codigo = "52260", Nombre = "EL TAMBO" };
        public static Municipio NARIÑO_FUNES => new Municipio { Codigo = "52287", Nombre = "FUNES" };
        public static Municipio NARIÑO_GUACHUCAL => new Municipio { Codigo = "52317", Nombre = "GUACHUCAL" };
        public static Municipio NARIÑO_GUAITARILLA => new Municipio { Codigo = "52320", Nombre = "GUAITARILLA" };
        public static Municipio NARIÑO_GUALMATAN => new Municipio { Codigo = "52323", Nombre = "GUALMATÁN" };
        public static Municipio NARIÑO_ILES => new Municipio { Codigo = "52352", Nombre = "ILES" };
        public static Municipio NARIÑO_IMUES => new Municipio { Codigo = "52354", Nombre = "IMUÉS" };
        public static Municipio NARIÑO_IPIALES => new Municipio { Codigo = "52356", Nombre = "IPIALES" };
        public static Municipio NARIÑO_LA_CRUZ => new Municipio { Codigo = "52378", Nombre = "LA CRUZ" };
        public static Municipio NARIÑO_LA_FLORIDA => new Municipio { Codigo = "52381", Nombre = "LA FLORIDA" };
        public static Municipio NARIÑO_LA_LLANADA => new Municipio { Codigo = "52385", Nombre = "LA LLANADA" };
        public static Municipio NARIÑO_LA_TOLA => new Municipio { Codigo = "52390", Nombre = "LA TOLA" };
        public static Municipio NARIÑO_LA_UNION => new Municipio { Codigo = "52399", Nombre = "LA UNIÓN" };
        public static Municipio NARIÑO_LEIVA => new Municipio { Codigo = "52405", Nombre = "LEIVA" };
        public static Municipio NARIÑO_LINARES => new Municipio { Codigo = "52411", Nombre = "LINARES" };
        public static Municipio NARIÑO_LOS_ANDES => new Municipio { Codigo = "52418", Nombre = "LOS ANDES" };
        public static Municipio NARIÑO_MAGÜI => new Municipio { Codigo = "52427", Nombre = "MAGÜÍ" };
        public static Municipio NARIÑO_MALLAMA => new Municipio { Codigo = "52435", Nombre = "MALLAMA" };
        public static Municipio NARIÑO_MOSQUERA => new Municipio { Codigo = "52473", Nombre = "MOSQUERA" };
        public static Municipio NARIÑO_NARIÑO => new Municipio { Codigo = "52480", Nombre = "NARIÑO" };
        public static Municipio NARIÑO_OLAYA_HERRERA => new Municipio { Codigo = "52490", Nombre = "OLAYA HERRERA" };
        public static Municipio NARIÑO_OSPINA => new Municipio { Codigo = "52506", Nombre = "OSPINA" };
        public static Municipio NARIÑO_FRANCISCO_PIZARRO => new Municipio { Codigo = "52520", Nombre = "FRANCISCO PIZARRO" };
        public static Municipio NARIÑO_POLICARPA => new Municipio { Codigo = "52540", Nombre = "POLICARPA" };
        public static Municipio NARIÑO_POTOSI => new Municipio { Codigo = "52560", Nombre = "POTOSÍ" };
        public static Municipio NARIÑO_PROVIDENCIA => new Municipio { Codigo = "52565", Nombre = "PROVIDENCIA" };
        public static Municipio NARIÑO_PUERRES => new Municipio { Codigo = "52573", Nombre = "PUERRES" };
        public static Municipio NARIÑO_PUPIALES => new Municipio { Codigo = "52585", Nombre = "PUPIALES" };
        public static Municipio NARIÑO_RICAURTE => new Municipio { Codigo = "52612", Nombre = "RICAURTE" };
        public static Municipio NARIÑO_ROBERTO_PAYAN => new Municipio { Codigo = "52621", Nombre = "ROBERTO PAYÁN" };
        public static Municipio NARIÑO_SAMANIEGO => new Municipio { Codigo = "52678", Nombre = "SAMANIEGO" };
        public static Municipio NARIÑO_SANDONA => new Municipio { Codigo = "52683", Nombre = "SANDONÁ" };
        public static Municipio NARIÑO_SAN_BERNARDO => new Municipio { Codigo = "52685", Nombre = "SAN BERNARDO" };
        public static Municipio NARIÑO_SAN_LORENZO => new Municipio { Codigo = "52687", Nombre = "SAN LORENZO" };
        public static Municipio NARIÑO_SAN_PABLO => new Municipio { Codigo = "52693", Nombre = "SAN PABLO" };
        public static Municipio NARIÑO_SAN_PEDRO_DE_CARTAGO => new Municipio { Codigo = "52694", Nombre = "SAN PEDRO DE CARTAGO" };
        public static Municipio NARIÑO_SANTA_BARBARA => new Municipio { Codigo = "52696", Nombre = "SANTA BÁRBARA" };
        public static Municipio NARIÑO_SANTACRUZ => new Municipio { Codigo = "52699", Nombre = "SANTACRUZ" };
        public static Municipio NARIÑO_SAPUYES => new Municipio { Codigo = "52720", Nombre = "SAPUYES" };
        public static Municipio NARIÑO_TAMINANGO => new Municipio { Codigo = "52786", Nombre = "TAMINANGO" };
        public static Municipio NARIÑO_TANGUA => new Municipio { Codigo = "52788", Nombre = "TANGUA" };
        public static Municipio NARIÑO_SAN_ANDRES_DE_TUMACO => new Municipio { Codigo = "52835", Nombre = "SAN ANDRÉS DE TUMACO" };
        public static Municipio NARIÑO_TUQUERRES => new Municipio { Codigo = "52838", Nombre = "TÚQUERRES" };
        public static Municipio NARIÑO_YACUANQUER => new Municipio { Codigo = "52885", Nombre = "YACUANQUER" };
        public static Municipio NORTE_DE_SANTANDER_CUCUTA => new Municipio { Codigo = "54001", Nombre = "CÚCUTA" };
        public static Municipio NORTE_DE_SANTANDER_ABREGO => new Municipio { Codigo = "54003", Nombre = "ÁBREGO" };
        public static Municipio NORTE_DE_SANTANDER_ARBOLEDAS => new Municipio { Codigo = "54051", Nombre = "ARBOLEDAS" };
        public static Municipio NORTE_DE_SANTANDER_BOCHALEMA => new Municipio { Codigo = "54099", Nombre = "BOCHALEMA" };
        public static Municipio NORTE_DE_SANTANDER_BUCARASICA => new Municipio { Codigo = "54109", Nombre = "BUCARASICA" };
        public static Municipio NORTE_DE_SANTANDER_CACOTA => new Municipio { Codigo = "54125", Nombre = "CÁCOTA" };
        public static Municipio NORTE_DE_SANTANDER_CACHIRA => new Municipio { Codigo = "54128", Nombre = "CÁCHIRA" };
        public static Municipio NORTE_DE_SANTANDER_CHINACOTA => new Municipio { Codigo = "54172", Nombre = "CHINÁCOTA" };
        public static Municipio NORTE_DE_SANTANDER_CHITAGA => new Municipio { Codigo = "54174", Nombre = "CHITAGÁ" };
        public static Municipio NORTE_DE_SANTANDER_CONVENCION => new Municipio { Codigo = "54206", Nombre = "CONVENCIÓN" };
        public static Municipio NORTE_DE_SANTANDER_CUCUTILLA => new Municipio { Codigo = "54223", Nombre = "CUCUTILLA" };
        public static Municipio NORTE_DE_SANTANDER_DURANIA => new Municipio { Codigo = "54239", Nombre = "DURANIA" };
        public static Municipio NORTE_DE_SANTANDER_EL_CARMEN => new Municipio { Codigo = "54245", Nombre = "EL CARMEN" };
        public static Municipio NORTE_DE_SANTANDER_EL_TARRA => new Municipio { Codigo = "54250", Nombre = "EL TARRA" };
        public static Municipio NORTE_DE_SANTANDER_EL_ZULIA => new Municipio { Codigo = "54261", Nombre = "EL ZULIA" };
        public static Municipio NORTE_DE_SANTANDER_GRAMALOTE => new Municipio { Codigo = "54313", Nombre = "GRAMALOTE" };
        public static Municipio NORTE_DE_SANTANDER_HACARI => new Municipio { Codigo = "54344", Nombre = "HACARÍ" };
        public static Municipio NORTE_DE_SANTANDER_HERRAN => new Municipio { Codigo = "54347", Nombre = "HERRÁN" };
        public static Municipio NORTE_DE_SANTANDER_LABATECA => new Municipio { Codigo = "54377", Nombre = "LABATECA" };
        public static Municipio NORTE_DE_SANTANDER_LA_ESPERANZA => new Municipio { Codigo = "54385", Nombre = "LA ESPERANZA" };
        public static Municipio NORTE_DE_SANTANDER_LA_PLAYA => new Municipio { Codigo = "54398", Nombre = "LA PLAYA" };
        public static Municipio NORTE_DE_SANTANDER_LOS_PATIOS => new Municipio { Codigo = "54405", Nombre = "LOS PATIOS" };
        public static Municipio NORTE_DE_SANTANDER_LOURDES => new Municipio { Codigo = "54418", Nombre = "LOURDES" };
        public static Municipio NORTE_DE_SANTANDER_MUTISCUA => new Municipio { Codigo = "54480", Nombre = "MUTISCUA" };
        public static Municipio NORTE_DE_SANTANDER_OCAÑA => new Municipio { Codigo = "54498", Nombre = "OCAÑA" };
        public static Municipio NORTE_DE_SANTANDER_PAMPLONA => new Municipio { Codigo = "54518", Nombre = "PAMPLONA" };
        public static Municipio NORTE_DE_SANTANDER_PAMPLONITA => new Municipio { Codigo = "54520", Nombre = "PAMPLONITA" };
        public static Municipio NORTE_DE_SANTANDER_PUERTO_SANTANDER => new Municipio { Codigo = "54553", Nombre = "PUERTO SANTANDER" };
        public static Municipio NORTE_DE_SANTANDER_RAGONVALIA => new Municipio { Codigo = "54599", Nombre = "RAGONVALIA" };
        public static Municipio NORTE_DE_SANTANDER_SALAZAR => new Municipio { Codigo = "54660", Nombre = "SALAZAR" };
        public static Municipio NORTE_DE_SANTANDER_SAN_CALIXTO => new Municipio { Codigo = "54670", Nombre = "SAN CALIXTO" };
        public static Municipio NORTE_DE_SANTANDER_SAN_CAYETANO => new Municipio { Codigo = "54673", Nombre = "SAN CAYETANO" };
        public static Municipio NORTE_DE_SANTANDER_SANTIAGO => new Municipio { Codigo = "54680", Nombre = "SANTIAGO" };
        public static Municipio NORTE_DE_SANTANDER_SARDINATA => new Municipio { Codigo = "54720", Nombre = "SARDINATA" };
        public static Municipio NORTE_DE_SANTANDER_SILOS => new Municipio { Codigo = "54743", Nombre = "SILOS" };
        public static Municipio NORTE_DE_SANTANDER_TEORAMA => new Municipio { Codigo = "54800", Nombre = "TEORAMA" };
        public static Municipio NORTE_DE_SANTANDER_TIBU => new Municipio { Codigo = "54810", Nombre = "TIBÚ" };
        public static Municipio NORTE_DE_SANTANDER_TOLEDO => new Municipio { Codigo = "54820", Nombre = "TOLEDO" };
        public static Municipio NORTE_DE_SANTANDER_VILLA_CARO => new Municipio { Codigo = "54871", Nombre = "VILLA CARO" };
        public static Municipio NORTE_DE_SANTANDER_VILLA_DEL_ROSARIO => new Municipio { Codigo = "54874", Nombre = "VILLA DEL ROSARIO" };
        public static Municipio QUINDIO_ARMENIA => new Municipio { Codigo = "63001", Nombre = "ARMENIA" };
        public static Municipio QUINDIO_BUENAVISTA => new Municipio { Codigo = "63111", Nombre = "BUENAVISTA" };
        public static Municipio QUINDIO_CALARCA => new Municipio { Codigo = "63130", Nombre = "CALARCÁ" };
        public static Municipio QUINDIO_CIRCASIA => new Municipio { Codigo = "63190", Nombre = "CIRCASIA" };
        public static Municipio QUINDIO_CORDOBA => new Municipio { Codigo = "63212", Nombre = "CÓRDOBA" };
        public static Municipio QUINDIO_FILANDIA => new Municipio { Codigo = "63272", Nombre = "FILANDIA" };
        public static Municipio QUINDIO_GENOVA => new Municipio { Codigo = "63302", Nombre = "GÉNOVA" };
        public static Municipio QUINDIO_LA_TEBAIDA => new Municipio { Codigo = "63401", Nombre = "LA TEBAIDA" };
        public static Municipio QUINDIO_MONTENEGRO => new Municipio { Codigo = "63470", Nombre = "MONTENEGRO" };
        public static Municipio QUINDIO_PIJAO => new Municipio { Codigo = "63548", Nombre = "PIJAO" };
        public static Municipio QUINDIO_QUIMBAYA => new Municipio { Codigo = "63594", Nombre = "QUIMBAYA" };
        public static Municipio QUINDIO_SALENTO => new Municipio { Codigo = "63690", Nombre = "SALENTO" };
        public static Municipio RISARALDA_PEREIRA => new Municipio { Codigo = "66001", Nombre = "PEREIRA" };
        public static Municipio RISARALDA_APIA => new Municipio { Codigo = "66045", Nombre = "APÍA" };
        public static Municipio RISARALDA_BALBOA => new Municipio { Codigo = "66075", Nombre = "BALBOA" };
        public static Municipio RISARALDA_BELEN_DE_UMBRIA => new Municipio { Codigo = "66088", Nombre = "BELÉN DE UMBRÍA" };
        public static Municipio RISARALDA_DOSQUEBRADAS => new Municipio { Codigo = "66170", Nombre = "DOSQUEBRADAS" };
        public static Municipio RISARALDA_GUATICA => new Municipio { Codigo = "66318", Nombre = "GUÁTICA" };
        public static Municipio RISARALDA_LA_CELIA => new Municipio { Codigo = "66383", Nombre = "LA CELIA" };
        public static Municipio RISARALDA_LA_VIRGINIA => new Municipio { Codigo = "66400", Nombre = "LA VIRGINIA" };
        public static Municipio RISARALDA_MARSELLA => new Municipio { Codigo = "66440", Nombre = "MARSELLA" };
        public static Municipio RISARALDA_MISTRATO => new Municipio { Codigo = "66456", Nombre = "MISTRATÓ" };
        public static Municipio RISARALDA_PUEBLO_RICO => new Municipio { Codigo = "66572", Nombre = "PUEBLO RICO" };
        public static Municipio RISARALDA_QUINCHIA => new Municipio { Codigo = "66594", Nombre = "QUINCHÍA" };
        public static Municipio RISARALDA_SANTA_ROSA_DE_CABAL => new Municipio { Codigo = "66682", Nombre = "SANTA ROSA DE CABAL" };
        public static Municipio RISARALDA_SANTUARIO => new Municipio { Codigo = "66687", Nombre = "SANTUARIO" };
        public static Municipio SANTANDER_BUCARAMANGA => new Municipio { Codigo = "68001", Nombre = "BUCARAMANGA" };
        public static Municipio SANTANDER_AGUADA => new Municipio { Codigo = "68013", Nombre = "AGUADA" };
        public static Municipio SANTANDER_ALBANIA => new Municipio { Codigo = "68020", Nombre = "ALBANIA" };
        public static Municipio SANTANDER_ARATOCA => new Municipio { Codigo = "68051", Nombre = "ARATOCA" };
        public static Municipio SANTANDER_BARBOSA => new Municipio { Codigo = "68077", Nombre = "BARBOSA" };
        public static Municipio SANTANDER_BARICHARA => new Municipio { Codigo = "68079", Nombre = "BARICHARA" };
        public static Municipio SANTANDER_BARRANCABERMEJA => new Municipio { Codigo = "68081", Nombre = "BARRANCABERMEJA" };
        public static Municipio SANTANDER_BETULIA => new Municipio { Codigo = "68092", Nombre = "BETULIA" };
        public static Municipio SANTANDER_BOLIVAR => new Municipio { Codigo = "68101", Nombre = "BOLÍVAR" };
        public static Municipio SANTANDER_CABRERA => new Municipio { Codigo = "68121", Nombre = "CABRERA" };
        public static Municipio SANTANDER_CALIFORNIA => new Municipio { Codigo = "68132", Nombre = "CALIFORNIA" };
        public static Municipio SANTANDER_CAPITANEJO => new Municipio { Codigo = "68147", Nombre = "CAPITANEJO" };
        public static Municipio SANTANDER_CARCASI => new Municipio { Codigo = "68152", Nombre = "CARCASÍ" };
        public static Municipio SANTANDER_CEPITA => new Municipio { Codigo = "68160", Nombre = "CEPITÁ" };
        public static Municipio SANTANDER_CERRITO => new Municipio { Codigo = "68162", Nombre = "CERRITO" };
        public static Municipio SANTANDER_CHARALA => new Municipio { Codigo = "68167", Nombre = "CHARALÁ" };
        public static Municipio SANTANDER_CHARTA => new Municipio { Codigo = "68169", Nombre = "CHARTA" };
        public static Municipio SANTANDER_CHIMA => new Municipio { Codigo = "68176", Nombre = "CHIMA" };
        public static Municipio SANTANDER_CHIPATA => new Municipio { Codigo = "68179", Nombre = "CHIPATÁ" };
        public static Municipio SANTANDER_CIMITARRA => new Municipio { Codigo = "68190", Nombre = "CIMITARRA" };
        public static Municipio SANTANDER_CONCEPCION => new Municipio { Codigo = "68207", Nombre = "CONCEPCIÓN" };
        public static Municipio SANTANDER_CONFINES => new Municipio { Codigo = "68209", Nombre = "CONFINES" };
        public static Municipio SANTANDER_CONTRATACION => new Municipio { Codigo = "68211", Nombre = "CONTRATACIÓN" };
        public static Municipio SANTANDER_COROMORO => new Municipio { Codigo = "68217", Nombre = "COROMORO" };
        public static Municipio SANTANDER_CURITI => new Municipio { Codigo = "68229", Nombre = "CURITÍ" };
        public static Municipio SANTANDER_EL_CARMEN_DE_CHUCURI => new Municipio { Codigo = "68235", Nombre = "EL CARMEN DE CHUCURÍ" };
        public static Municipio SANTANDER_EL_GUACAMAYO => new Municipio { Codigo = "68245", Nombre = "EL GUACAMAYO" };
        public static Municipio SANTANDER_EL_PEÑON => new Municipio { Codigo = "68250", Nombre = "EL PEÑÓN" };
        public static Municipio SANTANDER_EL_PLAYON => new Municipio { Codigo = "68255", Nombre = "EL PLAYÓN" };
        public static Municipio SANTANDER_ENCINO => new Municipio { Codigo = "68264", Nombre = "ENCINO" };
        public static Municipio SANTANDER_ENCISO => new Municipio { Codigo = "68266", Nombre = "ENCISO" };
        public static Municipio SANTANDER_FLORIAN => new Municipio { Codigo = "68271", Nombre = "FLORIÁN" };
        public static Municipio SANTANDER_FLORIDABLANCA => new Municipio { Codigo = "68276", Nombre = "FLORIDABLANCA" };
        public static Municipio SANTANDER_GALAN => new Municipio { Codigo = "68296", Nombre = "GALÁN" };
        public static Municipio SANTANDER_GAMBITA => new Municipio { Codigo = "68298", Nombre = "GÁMBITA" };
        public static Municipio SANTANDER_GIRON => new Municipio { Codigo = "68307", Nombre = "GIRÓN" };
        public static Municipio SANTANDER_GUACA => new Municipio { Codigo = "68318", Nombre = "GUACA" };
        public static Municipio SANTANDER_GUADALUPE => new Municipio { Codigo = "68320", Nombre = "GUADALUPE" };
        public static Municipio SANTANDER_GUAPOTA => new Municipio { Codigo = "68322", Nombre = "GUAPOTÁ" };
        public static Municipio SANTANDER_GUAVATA => new Municipio { Codigo = "68324", Nombre = "GUAVATÁ" };
        public static Municipio SANTANDER_GÜEPSA => new Municipio { Codigo = "68327", Nombre = "GÜEPSA" };
        public static Municipio SANTANDER_HATO => new Municipio { Codigo = "68344", Nombre = "HATO" };
        public static Municipio SANTANDER_JESUS_MARIA => new Municipio { Codigo = "68368", Nombre = "JESÚS MARÍA" };
        public static Municipio SANTANDER_JORDAN => new Municipio { Codigo = "68370", Nombre = "JORDÁN" };
        public static Municipio SANTANDER_LA_BELLEZA => new Municipio { Codigo = "68377", Nombre = "LA BELLEZA" };
        public static Municipio SANTANDER_LANDAZURI => new Municipio { Codigo = "68385", Nombre = "LANDÁZURI" };
        public static Municipio SANTANDER_LA_PAZ => new Municipio { Codigo = "68397", Nombre = "LA PAZ" };
        public static Municipio SANTANDER_LEBRIJA => new Municipio { Codigo = "68406", Nombre = "LEBRIJA" };
        public static Municipio SANTANDER_LOS_SANTOS => new Municipio { Codigo = "68418", Nombre = "LOS SANTOS" };
        public static Municipio SANTANDER_MACARAVITA => new Municipio { Codigo = "68425", Nombre = "MACARAVITA" };
        public static Municipio SANTANDER_MALAGA => new Municipio { Codigo = "68432", Nombre = "MÁLAGA" };
        public static Municipio SANTANDER_MATANZA => new Municipio { Codigo = "68444", Nombre = "MATANZA" };
        public static Municipio SANTANDER_MOGOTES => new Municipio { Codigo = "68464", Nombre = "MOGOTES" };
        public static Municipio SANTANDER_MOLAGAVITA => new Municipio { Codigo = "68468", Nombre = "MOLAGAVITA" };
        public static Municipio SANTANDER_OCAMONTE => new Municipio { Codigo = "68498", Nombre = "OCAMONTE" };
        public static Municipio SANTANDER_OIBA => new Municipio { Codigo = "68500", Nombre = "OIBA" };
        public static Municipio SANTANDER_ONZAGA => new Municipio { Codigo = "68502", Nombre = "ONZAGA" };
        public static Municipio SANTANDER_PALMAR => new Municipio { Codigo = "68522", Nombre = "PALMAR" };
        public static Municipio SANTANDER_PALMAS_DEL_SOCORRO => new Municipio { Codigo = "68524", Nombre = "PALMAS DEL SOCORRO" };
        public static Municipio SANTANDER_PARAMO => new Municipio { Codigo = "68533", Nombre = "PÁRAMO" };
        public static Municipio SANTANDER_PIEDECUESTA => new Municipio { Codigo = "68547", Nombre = "PIEDECUESTA" };
        public static Municipio SANTANDER_PINCHOTE => new Municipio { Codigo = "68549", Nombre = "PINCHOTE" };
        public static Municipio SANTANDER_PUENTE_NACIONAL => new Municipio { Codigo = "68572", Nombre = "PUENTE NACIONAL" };
        public static Municipio SANTANDER_PUERTO_PARRA => new Municipio { Codigo = "68573", Nombre = "PUERTO PARRA" };
        public static Municipio SANTANDER_PUERTO_WILCHES => new Municipio { Codigo = "68575", Nombre = "PUERTO WILCHES" };
        public static Municipio SANTANDER_RIONEGRO => new Municipio { Codigo = "68615", Nombre = "RIONEGRO" };
        public static Municipio SANTANDER_SABANA_DE_TORRES => new Municipio { Codigo = "68655", Nombre = "SABANA DE TORRES" };
        public static Municipio SANTANDER_SAN_ANDRES => new Municipio { Codigo = "68669", Nombre = "SAN ANDRÉS" };
        public static Municipio SANTANDER_SAN_BENITO => new Municipio { Codigo = "68673", Nombre = "SAN BENITO" };
        public static Municipio SANTANDER_SAN_GIL => new Municipio { Codigo = "68679", Nombre = "SAN GIL" };
        public static Municipio SANTANDER_SAN_JOAQUIN => new Municipio { Codigo = "68682", Nombre = "SAN JOAQUÍN" };
        public static Municipio SANTANDER_SAN_JOSE_DE_MIRANDA => new Municipio { Codigo = "68684", Nombre = "SAN JOSÉ DE MIRANDA" };
        public static Municipio SANTANDER_SAN_MIGUEL => new Municipio { Codigo = "68686", Nombre = "SAN MIGUEL" };
        public static Municipio SANTANDER_SAN_VICENTE_DE_CHUCURI => new Municipio { Codigo = "68689", Nombre = "SAN VICENTE DE CHUCURÍ" };
        public static Municipio SANTANDER_SANTA_BARBARA => new Municipio { Codigo = "68705", Nombre = "SANTA BÁRBARA" };
        public static Municipio SANTANDER_SANTA_HELENA_DEL_OPON => new Municipio { Codigo = "68720", Nombre = "SANTA HELENA DEL OPÓN" };
        public static Municipio SANTANDER_SIMACOTA => new Municipio { Codigo = "68745", Nombre = "SIMACOTA" };
        public static Municipio SANTANDER_SOCORRO => new Municipio { Codigo = "68755", Nombre = "SOCORRO" };
        public static Municipio SANTANDER_SUAITA => new Municipio { Codigo = "68770", Nombre = "SUAITA" };
        public static Municipio SANTANDER_SUCRE => new Municipio { Codigo = "68773", Nombre = "SUCRE" };
        public static Municipio SANTANDER_SURATA => new Municipio { Codigo = "68780", Nombre = "SURATÁ" };
        public static Municipio SANTANDER_TONA => new Municipio { Codigo = "68820", Nombre = "TONA" };
        public static Municipio SANTANDER_VALLE_DE_SAN_JOSE => new Municipio { Codigo = "68855", Nombre = "VALLE DE SAN JOSÉ" };
        public static Municipio SANTANDER_VELEZ => new Municipio { Codigo = "68861", Nombre = "VÉLEZ" };
        public static Municipio SANTANDER_VETAS => new Municipio { Codigo = "68867", Nombre = "VETAS" };
        public static Municipio SANTANDER_VILLANUEVA => new Municipio { Codigo = "68872", Nombre = "VILLANUEVA" };
        public static Municipio SANTANDER_ZAPATOCA => new Municipio { Codigo = "68895", Nombre = "ZAPATOCA" };
        public static Municipio SUCRE_SINCELEJO => new Municipio { Codigo = "70001", Nombre = "SINCELEJO" };
        public static Municipio SUCRE_BUENAVISTA => new Municipio { Codigo = "70110", Nombre = "BUENAVISTA" };
        public static Municipio SUCRE_CAIMITO => new Municipio { Codigo = "70124", Nombre = "CAIMITO" };
        public static Municipio SUCRE_COLOSO => new Municipio { Codigo = "70204", Nombre = "COLOSÓ" };
        public static Municipio SUCRE_COROZAL => new Municipio { Codigo = "70215", Nombre = "COROZAL" };
        public static Municipio SUCRE_COVEÑAS => new Municipio { Codigo = "70221", Nombre = "COVEÑAS" };
        public static Municipio SUCRE_CHALAN => new Municipio { Codigo = "70230", Nombre = "CHALÁN" };
        public static Municipio SUCRE_EL_ROBLE => new Municipio { Codigo = "70233", Nombre = "EL ROBLE" };
        public static Municipio SUCRE_GALERAS => new Municipio { Codigo = "70235", Nombre = "GALERAS" };
        public static Municipio SUCRE_GUARANDA => new Municipio { Codigo = "70265", Nombre = "GUARANDA" };
        public static Municipio SUCRE_LA_UNION => new Municipio { Codigo = "70400", Nombre = "LA UNIÓN" };
        public static Municipio SUCRE_LOS_PALMITOS => new Municipio { Codigo = "70418", Nombre = "LOS PALMITOS" };
        public static Municipio SUCRE_MAJAGUAL => new Municipio { Codigo = "70429", Nombre = "MAJAGUAL" };
        public static Municipio SUCRE_MORROA => new Municipio { Codigo = "70473", Nombre = "MORROA" };
        public static Municipio SUCRE_OVEJAS => new Municipio { Codigo = "70508", Nombre = "OVEJAS" };
        public static Municipio SUCRE_PALMITO => new Municipio { Codigo = "70523", Nombre = "PALMITO" };
        public static Municipio SUCRE_SAMPUES => new Municipio { Codigo = "70670", Nombre = "SAMPUÉS" };
        public static Municipio SUCRE_SAN_BENITO_ABAD => new Municipio { Codigo = "70678", Nombre = "SAN BENITO ABAD" };
        public static Municipio SUCRE_SAN_JUAN_DE_BETULIA => new Municipio { Codigo = "70702", Nombre = "SAN JUAN DE BETULIA" };
        public static Municipio SUCRE_SAN_MARCOS => new Municipio { Codigo = "70708", Nombre = "SAN MARCOS" };
        public static Municipio SUCRE_SAN_ONOFRE => new Municipio { Codigo = "70713", Nombre = "SAN ONOFRE" };
        public static Municipio SUCRE_SAN_PEDRO => new Municipio { Codigo = "70717", Nombre = "SAN PEDRO" };
        public static Municipio SUCRE_SAN_LUIS_DE_SINCE => new Municipio { Codigo = "70742", Nombre = "SAN LUIS DE SINCÉ" };
        public static Municipio SUCRE_SUCRE => new Municipio { Codigo = "70771", Nombre = "SUCRE" };
        public static Municipio SUCRE_SANTIAGO_DE_TOLU => new Municipio { Codigo = "70820", Nombre = "SANTIAGO DE TOLÚ" };
        public static Municipio SUCRE_TOLU_VIEJO => new Municipio { Codigo = "70823", Nombre = "TOLÚ VIEJO" };
        public static Municipio TOLIMA_IBAGUE => new Municipio { Codigo = "73001", Nombre = "IBAGUÉ" };
        public static Municipio TOLIMA_ALPUJARRA => new Municipio { Codigo = "73024", Nombre = "ALPUJARRA" };
        public static Municipio TOLIMA_ALVARADO => new Municipio { Codigo = "73026", Nombre = "ALVARADO" };
        public static Municipio TOLIMA_AMBALEMA => new Municipio { Codigo = "73030", Nombre = "AMBALEMA" };
        public static Municipio TOLIMA_ANZOATEGUI => new Municipio { Codigo = "73043", Nombre = "ANZOÁTEGUI" };
        public static Municipio TOLIMA_ARMERO => new Municipio { Codigo = "73055", Nombre = "ARMERO" };
        public static Municipio TOLIMA_ATACO => new Municipio { Codigo = "73067", Nombre = "ATACO" };
        public static Municipio TOLIMA_CAJAMARCA => new Municipio { Codigo = "73124", Nombre = "CAJAMARCA" };
        public static Municipio TOLIMA_CARMEN_DE_APICALA => new Municipio { Codigo = "73148", Nombre = "CARMEN DE APICALÁ" };
        public static Municipio TOLIMA_CASABIANCA => new Municipio { Codigo = "73152", Nombre = "CASABIANCA" };
        public static Municipio TOLIMA_CHAPARRAL => new Municipio { Codigo = "73168", Nombre = "CHAPARRAL" };
        public static Municipio TOLIMA_COELLO => new Municipio { Codigo = "73200", Nombre = "COELLO" };
        public static Municipio TOLIMA_COYAIMA => new Municipio { Codigo = "73217", Nombre = "COYAIMA" };
        public static Municipio TOLIMA_CUNDAY => new Municipio { Codigo = "73226", Nombre = "CUNDAY" };
        public static Municipio TOLIMA_DOLORES => new Municipio { Codigo = "73236", Nombre = "DOLORES" };
        public static Municipio TOLIMA_ESPINAL => new Municipio { Codigo = "73268", Nombre = "ESPINAL" };
        public static Municipio TOLIMA_FALAN => new Municipio { Codigo = "73270", Nombre = "FALAN" };
        public static Municipio TOLIMA_FLANDES => new Municipio { Codigo = "73275", Nombre = "FLANDES" };
        public static Municipio TOLIMA_FRESNO => new Municipio { Codigo = "73283", Nombre = "FRESNO" };
        public static Municipio TOLIMA_GUAMO => new Municipio { Codigo = "73319", Nombre = "GUAMO" };
        public static Municipio TOLIMA_HERVEO => new Municipio { Codigo = "73347", Nombre = "HERVEO" };
        public static Municipio TOLIMA_HONDA => new Municipio { Codigo = "73349", Nombre = "HONDA" };
        public static Municipio TOLIMA_ICONONZO => new Municipio { Codigo = "73352", Nombre = "ICONONZO" };
        public static Municipio TOLIMA_LERIDA => new Municipio { Codigo = "73408", Nombre = "LÉRIDA" };
        public static Municipio TOLIMA_LIBANO => new Municipio { Codigo = "73411", Nombre = "LÍBANO" };
        public static Municipio TOLIMA_SAN_SEBASTIAN_DE_MARIQUITA => new Municipio { Codigo = "73443", Nombre = "SAN SEBASTIÁN DE MARIQUITA" };
        public static Municipio TOLIMA_MELGAR => new Municipio { Codigo = "73449", Nombre = "MELGAR" };
        public static Municipio TOLIMA_MURILLO => new Municipio { Codigo = "73461", Nombre = "MURILLO" };
        public static Municipio TOLIMA_NATAGAIMA => new Municipio { Codigo = "73483", Nombre = "NATAGAIMA" };
        public static Municipio TOLIMA_ORTEGA => new Municipio { Codigo = "73504", Nombre = "ORTEGA" };
        public static Municipio TOLIMA_PALOCABILDO => new Municipio { Codigo = "73520", Nombre = "PALOCABILDO" };
        public static Municipio TOLIMA_PIEDRAS => new Municipio { Codigo = "73547", Nombre = "PIEDRAS" };
        public static Municipio TOLIMA_PLANADAS => new Municipio { Codigo = "73555", Nombre = "PLANADAS" };
        public static Municipio TOLIMA_PRADO => new Municipio { Codigo = "73563", Nombre = "PRADO" };
        public static Municipio TOLIMA_PURIFICACION => new Municipio { Codigo = "73585", Nombre = "PURIFICACIÓN" };
        public static Municipio TOLIMA_RIOBLANCO => new Municipio { Codigo = "73616", Nombre = "RIOBLANCO" };
        public static Municipio TOLIMA_RONCESVALLES => new Municipio { Codigo = "73622", Nombre = "RONCESVALLES" };
        public static Municipio TOLIMA_ROVIRA => new Municipio { Codigo = "73624", Nombre = "ROVIRA" };
        public static Municipio TOLIMA_SALDAÑA => new Municipio { Codigo = "73671", Nombre = "SALDAÑA" };
        public static Municipio TOLIMA_SAN_ANTONIO => new Municipio { Codigo = "73675", Nombre = "SAN ANTONIO" };
        public static Municipio TOLIMA_SAN_LUIS => new Municipio { Codigo = "73678", Nombre = "SAN LUIS" };
        public static Municipio TOLIMA_SANTA_ISABEL => new Municipio { Codigo = "73686", Nombre = "SANTA ISABEL" };
        public static Municipio TOLIMA_SUAREZ => new Municipio { Codigo = "73770", Nombre = "SUÁREZ" };
        public static Municipio TOLIMA_VALLE_DE_SAN_JUAN => new Municipio { Codigo = "73854", Nombre = "VALLE DE SAN JUAN" };
        public static Municipio TOLIMA_VENADILLO => new Municipio { Codigo = "73861", Nombre = "VENADILLO" };
        public static Municipio TOLIMA_VILLAHERMOSA => new Municipio { Codigo = "73870", Nombre = "VILLAHERMOSA" };
        public static Municipio TOLIMA_VILLARRICA => new Municipio { Codigo = "73873", Nombre = "VILLARRICA" };
        public static Municipio VALLE_DEL_CAUCA_CALI => new Municipio { Codigo = "76001", Nombre = "CALI" };
        public static Municipio VALLE_DEL_CAUCA_ALCALA => new Municipio { Codigo = "76020", Nombre = "ALCALÁ" };
        public static Municipio VALLE_DEL_CAUCA_ANDALUCIA => new Municipio { Codigo = "76036", Nombre = "ANDALUCÍA" };
        public static Municipio VALLE_DE_CAUCA_ANDALUCIA => new Municipio { Codigo = "76036", Nombre = "ANDALUCÍA" };
        public static Municipio VALLE_DEL_CAUCA_ANSERMANUEVO => new Municipio { Codigo = "76041", Nombre = "ANSERMANUEVO" };
        public static Municipio VALLE_DEL_CAUCA_ARGELIA => new Municipio { Codigo = "76054", Nombre = "ARGELIA" };
        public static Municipio VALLE_DEL_CAUCA_BOLIVAR => new Municipio { Codigo = "76100", Nombre = "BOLÍVAR" };
        public static Municipio VALLE_DEL_CAUCA_BUENAVENTURA => new Municipio { Codigo = "76109", Nombre = "BUENAVENTURA" };
        public static Municipio VALLE_DEL_CAUCA_GUADALAJARA_DE_BUGA => new Municipio { Codigo = "76111", Nombre = "GUADALAJARA DE BUGA" };
        public static Municipio VALLE_DEL_CAUCA_BUGALAGRANDE => new Municipio { Codigo = "76113", Nombre = "BUGALAGRANDE" };
        public static Municipio VALLE_DEL_CAUCA_CAICEDONIA => new Municipio { Codigo = "76122", Nombre = "CAICEDONIA" };
        public static Municipio VALLE_DEL_CAUCA_CALIMA => new Municipio { Codigo = "76126", Nombre = "CALIMA" };
        public static Municipio VALLE_DEL_CAUCA_CANDELARIA => new Municipio { Codigo = "76130", Nombre = "CANDELARIA" };
        public static Municipio VALLE_DEL_CAUCA_CARTAGO => new Municipio { Codigo = "76147", Nombre = "CARTAGO" };
        public static Municipio VALLE_DEL_CAUCA_DAGUA => new Municipio { Codigo = "76233", Nombre = "DAGUA" };
        public static Municipio VALLE_DEL_CAUCA_EL_AGUILA => new Municipio { Codigo = "76243", Nombre = "EL ÁGUILA" };
        public static Municipio VALLE_DEL_CAUCA_EL_CAIRO => new Municipio { Codigo = "76246", Nombre = "EL CAIRO" };
        public static Municipio VALLE_DEL_CAUCA_EL_CERRITO => new Municipio { Codigo = "76248", Nombre = "EL CERRITO" };
        public static Municipio VALLE_DEL_CAUCA_EL_DOVIO => new Municipio { Codigo = "76250", Nombre = "EL DOVIO" };
        public static Municipio VALLE_DEL_CAUCA_FLORIDA => new Municipio { Codigo = "76275", Nombre = "FLORIDA" };
        public static Municipio VALLE_DEL_CAUCA_GINEBRA => new Municipio { Codigo = "76306", Nombre = "GINEBRA" };
        public static Municipio VALLE_DEL_CAUCA_GUACARI => new Municipio { Codigo = "76318", Nombre = "GUACARÍ" };
        public static Municipio VALLE_DEL_CAUCA_JAMUNDI => new Municipio { Codigo = "76364", Nombre = "JAMUNDÍ" };
        public static Municipio VALLE_DE_CAUCA_JAMUNDI => new Municipio { Codigo = "76364", Nombre = "JAMUNDÍ" };
        public static Municipio VALLE_DEL_CAUCA_LA_CUMBRE => new Municipio { Codigo = "76377", Nombre = "LA CUMBRE" };
        public static Municipio VALLE_DEL_CAUCA_LA_UNION => new Municipio { Codigo = "76400", Nombre = "LA UNIÓN" };
        public static Municipio VALLE_DEL_CAUCA_LA_VICTORIA => new Municipio { Codigo = "76403", Nombre = "LA VICTORIA" };
        public static Municipio VALLE_DEL_CAUCA_OBANDO => new Municipio { Codigo = "76497", Nombre = "OBANDO" };
        public static Municipio VALLE_DEL_CAUCA_PALMIRA => new Municipio { Codigo = "76520", Nombre = "PALMIRA" };
        public static Municipio VALLE_DEL_CAUCA_PRADERA => new Municipio { Codigo = "76563", Nombre = "PRADERA" };
        public static Municipio VALLE_DEL_CAUCA_RESTREPO => new Municipio { Codigo = "76606", Nombre = "RESTREPO" };
        public static Municipio VALLE_DEL_CAUCA_RIOFRIO => new Municipio { Codigo = "76616", Nombre = "RIOFRÍO" };
        public static Municipio VALLE_DEL_CAUCA_ROLDANILLO => new Municipio { Codigo = "76622", Nombre = "ROLDANILLO" };
        public static Municipio VALLE_DEL_CAUCA_SAN_PEDRO => new Municipio { Codigo = "76670", Nombre = "SAN PEDRO" };
        public static Municipio VALLE_DEL_CAUCA_SEVILLA => new Municipio { Codigo = "76736", Nombre = "SEVILLA" };
        public static Municipio VALLE_DEL_CAUCA_TORO => new Municipio { Codigo = "76823", Nombre = "TORO" };
        public static Municipio VALLE_DEL_CAUCA_TRUJILLO => new Municipio { Codigo = "76828", Nombre = "TRUJILLO" };
        public static Municipio VALLE_DEL_CAUCA_TULUA => new Municipio { Codigo = "76834", Nombre = "TULUÁ" };
        public static Municipio VALLE_DEL_CAUCA_ULLOA => new Municipio { Codigo = "76845", Nombre = "ULLOA" };
        public static Municipio VALLE_DEL_CAUCA_VERSALLES => new Municipio { Codigo = "76863", Nombre = "VERSALLES" };
        public static Municipio VALLE_DEL_CAUCA_VIJES => new Municipio { Codigo = "76869", Nombre = "VIJES" };
        public static Municipio VALLE_DEL_CAUCA_YOTOCO => new Municipio { Codigo = "76890", Nombre = "YOTOCO" };
        public static Municipio VALLE_DEL_CAUCA_YUMBO => new Municipio { Codigo = "76892", Nombre = "YUMBO" };
        public static Municipio VALLE_DEL_CAUCA_ZARZAL => new Municipio { Codigo = "76895", Nombre = "ZARZAL" };
        public static Municipio ARAUCA_ARAUCA => new Municipio { Codigo = "81001", Nombre = "ARAUCA" };
        public static Municipio ARAUCA_ARAUQUITA => new Municipio { Codigo = "81065", Nombre = "ARAUQUITA" };
        public static Municipio ARAUCA_CRAVO_NORTE => new Municipio { Codigo = "81220", Nombre = "CRAVO NORTE" };
        public static Municipio ARAUCA_FORTUL => new Municipio { Codigo = "81300", Nombre = "FORTUL" };
        public static Municipio ARAUCA_PUERTO_RONDON => new Municipio { Codigo = "81591", Nombre = "PUERTO RONDÓN" };
        public static Municipio ARAUCA_SARAVENA => new Municipio { Codigo = "81736", Nombre = "SARAVENA" };
        public static Municipio ARAUCA_TAME => new Municipio { Codigo = "81794", Nombre = "TAME" };
        public static Municipio CASANARE_YOPAL => new Municipio { Codigo = "85001", Nombre = "YOPAL" };
        public static Municipio CASANARE_AGUAZUL => new Municipio { Codigo = "85010", Nombre = "AGUAZUL" };
        public static Municipio CASANARE_CHAMEZA => new Municipio { Codigo = "85015", Nombre = "CHÁMEZA" };
        public static Municipio CASANARE_HATO_COROZAL => new Municipio { Codigo = "85125", Nombre = "HATO COROZAL" };
        public static Municipio CASANARE_LA_SALINA => new Municipio { Codigo = "85136", Nombre = "LA SALINA" };
        public static Municipio CASANARE_MANI => new Municipio { Codigo = "85139", Nombre = "MANÍ" };
        public static Municipio CASANARE_MONTERREY => new Municipio { Codigo = "85162", Nombre = "MONTERREY" };
        public static Municipio CASANARE_NUNCHIA => new Municipio { Codigo = "85225", Nombre = "NUNCHÍA" };
        public static Municipio CASANARE_OROCUE => new Municipio { Codigo = "85230", Nombre = "OROCUÉ" };
        public static Municipio CASANARE_PAZ_DE_ARIPORO => new Municipio { Codigo = "85250", Nombre = "PAZ DE ARIPORO" };
        public static Municipio CASANARE_PORE => new Municipio { Codigo = "85263", Nombre = "PORE" };
        public static Municipio CASANARE_RECETOR => new Municipio { Codigo = "85279", Nombre = "RECETOR" };
        public static Municipio CASANARE_SABANALARGA => new Municipio { Codigo = "85300", Nombre = "SABANALARGA" };
        public static Municipio CASANARE_SACAMA => new Municipio { Codigo = "85315", Nombre = "SÁCAMA" };
        public static Municipio CASANARE_SAN_LUIS_DE_PALENQUE => new Municipio { Codigo = "85325", Nombre = "SAN LUIS DE PALENQUE" };
        public static Municipio CASANARE_TAMARA => new Municipio { Codigo = "85400", Nombre = "TÁMARA" };
        public static Municipio CASANARE_TAURAMENA => new Municipio { Codigo = "85410", Nombre = "TAURAMENA" };
        public static Municipio CASANARE_TRINIDAD => new Municipio { Codigo = "85430", Nombre = "TRINIDAD" };
        public static Municipio CASANARE_VILLANUEVA => new Municipio { Codigo = "85440", Nombre = "VILLANUEVA" };
        public static Municipio PUTUMAYO_MOCOA => new Municipio { Codigo = "86001", Nombre = "MOCOA" };
        public static Municipio PUTUMAYO_COLON => new Municipio { Codigo = "86219", Nombre = "COLÓN" };
        public static Municipio PUTUMAYO_ORITO => new Municipio { Codigo = "86320", Nombre = "ORITO" };
        public static Municipio PUTUMAYO_PUERTO_ASIS => new Municipio { Codigo = "86568", Nombre = "PUERTO ASÍS" };
        public static Municipio PUTUMAYO_PUERTO_CAICEDO => new Municipio { Codigo = "86569", Nombre = "PUERTO CAICEDO" };
        public static Municipio PUTUMAYO_PUERTO_GUZMAN => new Municipio { Codigo = "86571", Nombre = "PUERTO GUZMÁN" };
        public static Municipio PUTUMAYO_PUERTO_LEGUIZAMO => new Municipio { Codigo = "86573", Nombre = "PUERTO LEGUÍZAMO" };
        public static Municipio PUTUMAYO_SIBUNDOY => new Municipio { Codigo = "86749", Nombre = "SIBUNDOY" };
        public static Municipio PUTUMAYO_SAN_FRANCISCO => new Municipio { Codigo = "86755", Nombre = "SAN FRANCISCO" };
        public static Municipio PUTUMAYO_SAN_MIGUEL => new Municipio { Codigo = "86757", Nombre = "SAN MIGUEL" };
        public static Municipio PUTUMAYO_SANTIAGO => new Municipio { Codigo = "86760", Nombre = "SANTIAGO" };
        public static Municipio PUTUMAYO_VALLE_DEL_GUAMUEZ => new Municipio { Codigo = "86865", Nombre = "VALLE DEL GUAMUEZ" };
        public static Municipio PUTUMAYO_VILLAGARZON => new Municipio { Codigo = "86885", Nombre = "VILLAGARZÓN" };
        public static Municipio ARCHIPIELAGO_DE_SAN_ANDRES_PROVIDENCIA_Y_SANTA_CATALINA_SAN_ANDRES => new Municipio { Codigo = "88001", Nombre = "SAN ANDRÉS" };
        public static Municipio ARCHIPIELAGO_DE_SAN_ANDRES_PROVIDENCIA_Y_SANTA_CATALINA_PROVIDENCIA => new Municipio { Codigo = "88564", Nombre = "PROVIDENCIA" };
        public static Municipio AMAZONAS_LETICIA => new Municipio { Codigo = "91001", Nombre = "LETICIA" };
        public static Municipio AMAZONAS_EL_ENCANTO => new Municipio { Codigo = "91263", Nombre = "EL ENCANTO" };
        public static Municipio AMAZONAS_LA_CHORRERA => new Municipio { Codigo = "91405", Nombre = "LA CHORRERA" };
        public static Municipio AMAZONAS_LA_PEDRERA => new Municipio { Codigo = "91407", Nombre = "LA PEDRERA" };
        public static Municipio AMAZONAS_LA_VICTORIA => new Municipio { Codigo = "91430", Nombre = "LA VICTORIA" };
        public static Municipio AMAZONAS_MIRITI__PARANA => new Municipio { Codigo = "91460", Nombre = "MIRITÍ – PARANÁ" };
        public static Municipio AMAZONAS_PUERTO_ALEGRIA => new Municipio { Codigo = "91530", Nombre = "PUERTO ALEGRÍA" };
        public static Municipio AMAZONAS_PUERTO_ARICA => new Municipio { Codigo = "91536", Nombre = "PUERTO ARICA" };
        public static Municipio AMAZONAS_PUERTO_NARIÑO => new Municipio { Codigo = "91540", Nombre = "PUERTO NARIÑO" };
        public static Municipio AMAZONAS_PUERTO_SANTANDER => new Municipio { Codigo = "91669", Nombre = "PUERTO SANTANDER" };
        public static Municipio AMAZONAS_TARAPACA => new Municipio { Codigo = "91798", Nombre = "TARAPACÁ" };
        public static Municipio GUAINIA_INIRIDA => new Municipio { Codigo = "94001", Nombre = "INÍRIDA" };
        public static Municipio GUAINIA_BARRANCO_MINAS => new Municipio { Codigo = "94343", Nombre = "BARRANCO MINAS" };
        public static Municipio GUAINIA_MAPIRIPANA => new Municipio { Codigo = "94663", Nombre = "MAPIRIPANA" };
        public static Municipio GUAINIA_SAN_FELIPE => new Municipio { Codigo = "94883", Nombre = "SAN FELIPE" };
        public static Municipio GUAINIA_PUERTO_COLOMBIA => new Municipio { Codigo = "94884", Nombre = "PUERTO COLOMBIA" };
        public static Municipio GUAINIA_LA_GUADALUPE => new Municipio { Codigo = "94885", Nombre = "LA GUADALUPE" };
        public static Municipio GUAINIA_CACAHUAL => new Municipio { Codigo = "94886", Nombre = "CACAHUAL" };
        public static Municipio GUAINIA_PANA_PANA => new Municipio { Codigo = "94887", Nombre = "PANA PANA" };
        public static Municipio GUAINIA_MORICHAL => new Municipio { Codigo = "94888", Nombre = "MORICHAL" };
        public static Municipio GUAVIARE_SAN_JOSE_DEL_GUAVIARE => new Municipio { Codigo = "95001", Nombre = "SAN JOSÉ DEL GUAVIARE" };
        public static Municipio GUAVIARE_CALAMAR => new Municipio { Codigo = "95015", Nombre = "CALAMAR" };
        public static Municipio GUAVIARE_EL_RETORNO => new Municipio { Codigo = "95025", Nombre = "EL RETORNO" };
        public static Municipio GUAVIARE_MIRAFLORES => new Municipio { Codigo = "95200", Nombre = "MIRAFLORES" };
        public static Municipio VAUPES_MITU => new Municipio { Codigo = "97001", Nombre = "MITÚ" };
        public static Municipio VAUPES_CARURU => new Municipio { Codigo = "97161", Nombre = "CARURÚ" };
        public static Municipio VAUPES_PACOA => new Municipio { Codigo = "97511", Nombre = "PACOA" };
        public static Municipio VAUPES_TARAIRA => new Municipio { Codigo = "97666", Nombre = "TARAIRA" };
        public static Municipio VAUPES_PAPUNAHUA => new Municipio { Codigo = "97777", Nombre = "PAPUNAHUA" };
        public static Municipio VAUPES_YAVARATE => new Municipio { Codigo = "97889", Nombre = "YAVARATÉ" };
        public static Municipio VICHADA_PUERTO_CARREÑO => new Municipio { Codigo = "99001", Nombre = "PUERTO CARREÑO" };
        public static Municipio VICHADA_LA_PRIMAVERA => new Municipio { Codigo = "99524", Nombre = "LA PRIMAVERA" };
        public static Municipio VICHADA_SANTA_ROSALIA => new Municipio { Codigo = "99624", Nombre = "SANTA ROSALÍA" };
        public static Municipio VICHADA_CUMARIBO => new Municipio { Codigo = "99773", Nombre = "CUMARIBO" };
    }

    public class DireccionFisica
    {
        public Departamento Departamento { get; set; }
        public Municipio Municipio { get; set; }
        public string Linea { get; set; }
        public Pais Pais { get; set; } = Pais.COLOMBIA;
    }

    public class TipoDocumento
    {
        public string Codigo { get; set; }

        public static TipoDocumento FACTURA_VENTA_NACIONAL => new TipoDocumento { Codigo = "01" };
        public static TipoDocumento FACTURA_EXPORTACION => new TipoDocumento { Codigo = "02" };
        public static TipoDocumento FACTURA_CONTINGENCIA_FACTURADOR => new TipoDocumento { Codigo = "03" };
        public static TipoDocumento FACTURA_CONTINGENCIA_DIAN => new TipoDocumento { Codigo = "04" };
        public static TipoDocumento NOTA_CREDITO => new TipoDocumento { Codigo = "91" };
        public static TipoDocumento NOTA_DEBITO => new TipoDocumento { Codigo = "92" };
    }

    public class ReferenciaDocumento
    {
        public string Numero { get; set; }
        public string Cufe { get; set; }
        public AlgoritmoSeguridadUUID AlgoritmoCufe { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Indica si se refiere a una factura.
        /// </summary>
        public bool EsFactura
        {
            get
            {
                return (TipoDocumento.Codigo == TipoDocumento.FACTURA_VENTA_NACIONAL.Codigo ||
                    TipoDocumento.Codigo == TipoDocumento.FACTURA_EXPORTACION.Codigo ||
                    TipoDocumento.Codigo == TipoDocumento.FACTURA_CONTINGENCIA_FACTURADOR.Codigo ||
                    TipoDocumento.Codigo == TipoDocumento.FACTURA_CONTINGENCIA_DIAN.Codigo);
            }
        }

        /// <summary>
        /// Indica si se refiere a una nota de crédito.
        /// </summary>
        public bool EsNotaCredito
        {
            get
            {
                return (TipoDocumento.Codigo == TipoDocumento.NOTA_CREDITO.Codigo);
            }
        }

        /// <summary>
        /// Indica si se refiere a una nota de débito.
        /// </summary>
        public bool EsNotaDebito
        {
            get
            {
                return (TipoDocumento.Codigo == TipoDocumento.NOTA_DEBITO.Codigo);
            }
        }
    }

    public class ResponsabilidadFiscal
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        [Obsolete("Responsabilidad fiscal no disponible.", true)]
        public static ResponsabilidadFiscal OTROS_OBLIGADO => new ResponsabilidadFiscal
        {
            Codigo = "O-99",
            Descripcion = "Otro tipo de obligado"
        };

        public static ResponsabilidadFiscal GRAN_CONTRIBUYENTE => new ResponsabilidadFiscal
        {
            Codigo = "O-13",
            Descripcion = "Gran contribuyente"
        };
        public static ResponsabilidadFiscal AUTORRETENEDOR => new ResponsabilidadFiscal
        {
            Codigo = "O-15",
            Descripcion = "Autorretenedor"
        };
        public static ResponsabilidadFiscal AGENTE_RETENCION_IVA => new ResponsabilidadFiscal
        {
            Codigo = "O-23",
            Descripcion = "Agente de retención IVA"
        };
        public static ResponsabilidadFiscal REGIMEN_SIMPLE_TRIBUTACION => new ResponsabilidadFiscal
        {
            Codigo = "O-47",
            Descripcion = "Régimen simple de tributación"
        };
        [Obsolete("Responsabilidad fiscal no disponible", true)]
        public static ResponsabilidadFiscal NO_APLICA => new ResponsabilidadFiscal
        {
            Codigo = "ZZ",
            Descripcion = "No aplica"
        };
        public static ResponsabilidadFiscal NO_RESPONSABLE => new ResponsabilidadFiscal
        {
            Codigo = "R-99-PN",
            Descripcion = "No responsable"
        };
    }

    public class Emisor
    {
        public string Nit { get; set; }
        public string Nombre { get; set; }
        public DireccionFisica DireccionFisica { get; set; }
        public TipoContribuyente TipoContribuyente { get; set; }
        public RegimenFiscal RegimenFiscal { get; set; }
        public ResponsabilidadFiscal ResponsabilidadFiscal { get; set; } = ResponsabilidadFiscal.REGIMEN_SIMPLE_TRIBUTACION;
        public string PrefijoRangoNumeracion { get; set; }
        public string MatriculaMercantil { get; set; } = "00000";
        public string NumeroTelefonico { get; set; }
        public string CorreoElectronico { get; set; }
    }

    public class TipoIdentificacion
    {
        public string Codigo { get; set; }

        public static TipoIdentificacion NO_OBLIGADO => new TipoIdentificacion { Codigo = "R-00-PN" };
        public static TipoIdentificacion REGISTRO_CIVIL => new TipoIdentificacion { Codigo = "11" };
        public static TipoIdentificacion TARJETA_IDENTIDAD => new TipoIdentificacion { Codigo = "12" };
        public static TipoIdentificacion CEDULA_CIUDADANIA => new TipoIdentificacion { Codigo = "13" };
        public static TipoIdentificacion TARJETA_EXTRANJERIA => new TipoIdentificacion { Codigo = "21" };
        public static TipoIdentificacion CEDULA_EXTRANJERIA => new TipoIdentificacion { Codigo = "22" };
        public static TipoIdentificacion NIT => new TipoIdentificacion { Codigo = "31" };
        public static TipoIdentificacion PASAPORTE => new TipoIdentificacion { Codigo = "41" };
        public static TipoIdentificacion DOCUMENTO_IDENTIFICACION_EXTRANJERO => new TipoIdentificacion { Codigo = "42" };
        public static TipoIdentificacion NIT_EXTRANJERO => new TipoIdentificacion { Codigo = "50" };
        public static TipoIdentificacion NUIP => new TipoIdentificacion { Codigo = "91" };
    }

    public class Adquiriente
    {
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public DireccionFisica DireccionFisica { get; set; }
        public TipoContribuyente TipoContribuyente { get; set; }
        public RegimenFiscal RegimenFiscal { get; set; }
        public ResponsabilidadFiscal ResponsabilidadFiscal { get; set; } = ResponsabilidadFiscal.REGIMEN_SIMPLE_TRIBUTACION;
        public string MatriculaMercantil { get; set; } = "00000";
        public string NumeroTelefonico { get; set; }
        public string CorreoElectronico { get; set; }
    }

    public class FormaPago
    {
        public string Codigo { get; set; }

        public static FormaPago CONTADO => new FormaPago { Codigo = "1" };
        public static FormaPago CREDITO => new FormaPago { Codigo = "2" };
    }

    public class MetodoPago
    {
        public string Codigo { get; set; }

        public static MetodoPago NO_DEFINIDO => new MetodoPago { Codigo = "1" };
        public static MetodoPago EFECTIVO => new MetodoPago { Codigo = "10" };
        public static MetodoPago CHEQUE => new MetodoPago { Codigo = "20" };
        public static MetodoPago TRANSFERENCIA_CREDITO => new MetodoPago { Codigo = "30" };
        public static MetodoPago TRANSFERENCIA_DEBITO => new MetodoPago { Codigo = "31" };
        public static MetodoPago PAGO_DEPOSITO_PREACORDADO => new MetodoPago { Codigo = "34" };
        public static MetodoPago CONSIGNACION_BANCARIA => new MetodoPago { Codigo = "42" };
        public static MetodoPago TARJETA_CREDITO => new MetodoPago { Codigo = "48" };
        public static MetodoPago TARJETA_DEBITO => new MetodoPago { Codigo = "49" };
        public static MetodoPago POSTGIRO => new MetodoPago { Codigo = "50" };
        public static MetodoPago ACUERDO_MUTUO => new MetodoPago { Codigo = "97" };
        public static MetodoPago OTRO => new MetodoPago { Codigo = "ZZZ" };
    }
    
    public class DetallePago
    {
        public FormaPago Forma { get; set; }
        public MetodoPago Metodo { get; set; }

        /// <summary>
        /// Si es a crédito, debe ser la fecha de vencimiento.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Identificador (interno) del pago.
        /// </summary>
        public string Identificador { get; set; } = "NP" + DateTime.Now.Ticks.ToString();
    }

    public class CondicionEntrega
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public static CondicionEntrega COSTO_Y_FLETE => new CondicionEntrega
        {
            Codigo = "CFR",
            Descripcion = "Costo y flete"
        };
        public static CondicionEntrega COSTO_FLETE_Y_SEGURO => new CondicionEntrega
        {
            Codigo = "CIF",
            Descripcion = "Costo, flete y seguro"
        };
        public static CondicionEntrega TRANSPORTE_Y_SEGURO_PAGADO => new CondicionEntrega
        {
            Codigo = "CIP",
            Descripcion = "Transporte y seguro pagado"
        };
        public static CondicionEntrega TRANSPORTE_PAGADO => new CondicionEntrega
        {
            Codigo = "CPT",
            Descripcion = "Transporte pagado"
        };
        public static CondicionEntrega ENTREGADO_EN_DETERMINADO_LUGAR => new CondicionEntrega
        {
            Codigo = "DAP",
            Descripcion = "Entregado en determinado lugar"
        };
        public static CondicionEntrega ENTREGADO_EN_TERMINAL => new CondicionEntrega
        {
            Codigo = "DAT",
            Descripcion = "Entregado en terminal"
        };
        public static CondicionEntrega ENTREGADO_CON_ARANCELES_PAGADOS => new CondicionEntrega
        {
            Codigo = "DDP",
            Descripcion = "Entregado con aranceles pagados"
        };
        public static CondicionEntrega EN_TIENDA => new CondicionEntrega
        {
            Codigo = "EXW",
            Descripcion = "En tienda"
        };
        public static CondicionEntrega GRATIS_JUNTO_AL_BARCO => new CondicionEntrega
        {
            Codigo = "FAS",
            Descripcion = "Gratis junto al barco"
        };
        public static CondicionEntrega TRANSPORTISTA_GRATUITO => new CondicionEntrega
        {
            Codigo = "FCA",
            Descripcion = "Transportista gratuito"
        };
        public static CondicionEntrega GRATUITO_A_BORDO => new CondicionEntrega
        {
            Codigo = "FOB",
            Descripcion = "Gratuito a bordo"
        };
    }

    public class TerminosEntrega
    {
        public string TerminosEspeciales { get; set; }
        public CondicionEntrega Condicion { get; set; }        
    }

    public class Cargo
    {
        public string Razon { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Monto { get; set; }
        public decimal Base { get; set; }
    }

    public class TipoDescuento
    {
        public string Codigo { get; set; }

        public static TipoDescuento IMPUESTO_ASUMIDO => new TipoDescuento { Codigo = "00" };
        public static TipoDescuento PAGUE_UNO_LLEVE_OTRO => new TipoDescuento { Codigo = "01" };
        public static TipoDescuento CONTRACTUAL => new TipoDescuento { Codigo = "02" };
        public static TipoDescuento PRONTO_PAGO => new TipoDescuento { Codigo = "03" };
        public static TipoDescuento ENVIO_GRATIS => new TipoDescuento { Codigo = "04" };
        public static TipoDescuento ESPECIFICO_POR_INVENTARIO => new TipoDescuento { Codigo = "05" };
        public static TipoDescuento MONTO_DE_COMPRAS => new TipoDescuento { Codigo = "06" };
        public static TipoDescuento TEMPORADA => new TipoDescuento { Codigo = "07" };
        public static TipoDescuento ACTUALIZACION_PRODUCTOS_SERVICIOS => new TipoDescuento { Codigo = "08" };
        public static TipoDescuento GENERAL => new TipoDescuento { Codigo = "09" };
        public static TipoDescuento VOLUMEN => new TipoDescuento { Codigo = "10" };
        public static TipoDescuento OTROS => new TipoDescuento { Codigo = "11" };
    }

    public class Descuento
    {
        public string Razon { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal Monto { get; set; }
        public decimal Base { get; set; }
        public TipoDescuento Tipo { get; set; } = TipoDescuento.OTROS;
    }

    public class TasaCambio
    {
        /// <summary>
        /// Moneda del documento.
        /// </summary>
        public Moneda Origen { get; set; }

        /// <summary>
        /// Debe ser peso colombiano.
        /// </summary>
        public Moneda Destino { get; set; } = Moneda.PESO_COLOMBIANO;

        /// <summary>
        /// Tasa de cambio aplicada.
        /// </summary>
        public decimal Cambio { get; set; }

        /// <summary>
        /// Fecha de la tasa.
        /// </summary>
        public DateTime Fecha { get; set; }
    }

    public class Impuesto
    {
        /// <summary>
        /// Tipo de impuesto.
        /// </summary>
        public TipoImpuesto Tipo { get; set; }

        /// <summary>
        /// Porcentaje del impuesto.
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// Indicar cuando Porcentaje y Base imponible sean igual a 0.
        /// </summary>
        public decimal PorUnidad { get; set; }

        /// <summary>
        /// Base imponible del impuesto.
        /// </summary>
        public decimal BaseImponible { get; set; }

        /// <summary>
        /// Monto total de importe.
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Indica si se trata de un impuesto retenido (ReteIVA, ReteICA o ReteFuente).
        /// </summary>
        public bool Retenido
        {
            get
            {
                return (Tipo.Codigo == TipoImpuesto.RETE_IVA.Codigo || Tipo.Codigo == TipoImpuesto.RETE_ICA.Codigo || Tipo.Codigo == TipoImpuesto.RETE_FUENTE.Codigo);
            }
        }
    }

    public class ResumenImpuesto
    {
        /// <summary>
        /// Tipo de impuesto.
        /// </summary>
        public TipoImpuesto Tipo { get; set; }

        /// <summary>
        /// Suma de los elementos Importe de la colección Detalles.
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Detalles del impuesto.
        /// Indicar un elemento por cada porcentaje aplicado del impuesto.
        /// </summary>
        public Collection<Impuesto> Detalles { get; set; } = new Collection<Impuesto>();

        /// <summary>
        /// Indica si se trata de un impuesto retenido (ReteIVA, ReteICA o ReteFuente).
        /// </summary>
        public bool Retenido
        {
            get
            {
                return (Tipo.Codigo == TipoImpuesto.RETE_IVA.Codigo || Tipo.Codigo == TipoImpuesto.RETE_ICA.Codigo || Tipo.Codigo == TipoImpuesto.RETE_FUENTE.Codigo);
            }
        }
    }

    public class Anticipo
    {
        /// <summary>
        /// Identificador (interno) del anticipo.
        /// </summary>
        public string Identificador { get; set; } = "NP" + DateTime.Now.Ticks.ToString();

        /// <summary>
        /// Valor del anticipo.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Fecha en la cual el pago fue realizado.
        /// </summary>
        public DateTime FechaPago { get; set; }

        /// <summary>
        /// Fecha en la cual el pago fue recibido.
        /// </summary>
        public DateTime FechaRecepcionPago { get; set; }

        /// <summary>
        /// Instrucciones relativas al pago.
        /// </summary>
        public string Instrucciones { get; set; }
    }

    public class Totales
    {
        /// <summary>
        /// Total valor bruto, suma de los valores brutos de las líneas.
        /// </summary>
        public decimal ValorBruto { get; set; }

        /// <summary>
        ///  Base imponible para el cálculo de los tributos.
        /// </summary>
        public decimal BaseImponible { get; set; }

        /// <summary>
        /// Total de Valor Bruto más tributos.
        /// </summary>
        public decimal ValorBrutoConImpuestos { get; set; }

        /// <summary>
        /// Suma de todos los descuentos globales aplicados al documento.
        /// </summary>
        public decimal Descuentos { get; set; }

        /// <summary>
        /// Suma de todos los cargos globales aplicados al documento.
        /// </summary>
        public decimal Cargos { get; set; }

        /// <summary>
        /// Anticipo total del documento.
        /// </summary>
        public decimal Anticipo { get; set; }

        /// <summary>
        /// Valor total del documento.
        /// Suma de Valor Bruto más tributos - Valor del Descuento Total + Valor del Cargo Total - Valor del Anticipo Total.
        /// </summary>
        public decimal TotalPagable { get; set; }
    }

    public class CodigoPrecioReal
    {
        public string Valor { get; set; }

        public static CodigoPrecioReal VALOR_COMERCIAL => new CodigoPrecioReal { Valor = "01" };
        public static CodigoPrecioReal VALOR_EN_INVENTARIOS => new CodigoPrecioReal { Valor = "02" };
        public static CodigoPrecioReal OTROS => new CodigoPrecioReal { Valor = "03" };
    }

    public class TipoCodigoProducto
    {
        public string Identificador { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        /// <summary>
        /// United Nations Standard Products and Services Code
        /// </summary>
        public static TipoCodigoProducto UNSPSC => new TipoCodigoProducto
        {
            Identificador = "001",
            Codigo = "10",
            Nombre = "UNSPSC"
        };

        /// <summary>
        /// Números Globales de Identificación de Productos
        /// </summary>
        public static TipoCodigoProducto GTIN => new TipoCodigoProducto
        {
            Identificador = "010",
            Codigo = "9",
            Nombre = "GTIN"
        };

        /// <summary>
        /// Partida Arancelarias
        /// </summary>
        public static TipoCodigoProducto PARTIDAS_ARANCELARIAS => new TipoCodigoProducto
        {
            Identificador = "020",
            Codigo = "195",
            Nombre = "Partida Arancelarias"
        };

        /// <summary>
        /// Estándar de adopción del contribuyente
        /// </summary>
        public static TipoCodigoProducto ESTANDAR_INTERNO => new TipoCodigoProducto
        {
            Identificador = "999",
            Codigo = "",
            Nombre = "Estándar de adopción del contribuyente"
        };
    }

    public class CodigoProducto
    {
        public TipoCodigoProducto Tipo { get; set; } = TipoCodigoProducto.ESTANDAR_INTERNO;
        public string Valor { get; set; }
    }

    public class Linea
    {
        /// <summary>
        /// Cantidad de ítems.
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Código del producto. Opcional.
        /// </summary>
        public CodigoProducto CodigoProducto { get; set; }

        /// <summary>
        /// Nombre del producto o servicio.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Obligatorio si es factura de exportación.
        /// </summary>
        public string Marca { get; set; } = "UNKNOW_BRAND";

        /// <summary>
        /// Obligatorio si es factura de exportación.
        /// </summary>
        public string Modelo { get; set; } = "UNKNOW_MODEL";

        /// <summary>
        /// Precio unitario (a cobrar) sin impuestos.
        /// </summary>
        public decimal PrecioUnitario { get; set; }

        /// <summary>
        /// Obligatorio cuando Costo total es igual a 0.
        /// </summary>
        public decimal PrecioUnitarioReal { get; set; }

        /// <summary>
        /// Código de referencia para Precio unitario real.
        /// Obligatorio cuando Costo total es igual a 0.
        /// </summary>
        public CodigoPrecioReal CodigoPrecioReal { get; set; } = CodigoPrecioReal.OTROS;

        /// <summary>
        /// (Cantidad + Precio unitario) + Cargos - Descuentos
        /// </summary>
        public decimal CostoTotal { get; set; }

        /// <summary>
        /// Descuentos detallados.
        /// </summary>
        public Collection<Descuento> Descuentos { get; private set; } = new Collection<Descuento>();

        /// <summary>
        /// Cargos detallados.
        /// </summary>
        public Collection<Cargo> Cargos { get; private set; } = new Collection<Cargo>();

        /// <summary>
        /// Detalles de impuesto.
        /// </summary>
        public Collection<Impuesto> Impuestos { get; private set; } = new Collection<Impuesto>();

        /// <summary>
        /// NIT del mandante. Opcional en facturas tipo Mandato.
        /// </summary>
        public string NitMandante { get; set; }

        /// <summary>
        /// Anotaciones opcionales. Obligatorio para facturas AIU.
        /// </summary>
        public string Anotacion { get; set; }
    }

    public class ParteEvento
    {
        public TipoIdentificacion TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
    }

    public class ConceptoEventoRechazo
    {
        public string Codigo { get; set; }

        /// <summary>
        /// Documento con inconsistencias
        /// </summary>
        public static ConceptoEventoRechazo INCONSISTENCIAS_DOCUMENTO => new ConceptoEventoRechazo { Codigo = "01" };

        /// <summary>
        /// Mercancía no entregada totalmente
        /// </summary>
        public static ConceptoEventoRechazo MERCANCIA_NO_ENTREGADA_TOTALMENTE => new ConceptoEventoRechazo { Codigo = "02" };

        /// <summary>
        /// Mercancía no entregada parcialmente
        /// </summary>
        public static ConceptoEventoRechazo MERCANCIA_NO_ENTREGADA_PARCIALMENTE => new ConceptoEventoRechazo { Codigo = "03" };

        /// <summary>
        /// Servicio no prestado
        /// </summary>
        public static ConceptoEventoRechazo SERVICIO_NO_PRESTADO => new ConceptoEventoRechazo { Codigo = "04" };
    }

    public class PeriodoFacturacion
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class OrdenCompra
    {
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class DetallesEntrega
    {
        public DateTime Fecha { get; set; }
        public DireccionFisica Direccion { get; set; }

        /// <summary>
        /// Información sobre la empresa de transporte.
        /// </summary>
        public Transportador Transportador { get; set; }
    }

    public class Transportador: Adquiriente
    {

    }
}