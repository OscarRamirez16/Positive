
namespace eFacturacionColombia_V2.ServiciosWeb
{
    public abstract class RespuestaDian
    {
        /// <summary>
        ///  Procesado Corectamente
        /// </summary>
        public static string PROCESADO_CORRECTAMENTE => "00";

        /// <summary>
        /// Documento en proceso de validación.
        /// </summary>
        public static string PROCESANDO_VALIDACIONES => "98";

        /// <summary>
        ///  Procesado Corectamente
        /// </summary>
        public static string VALIDACION_EXITOSA => "0";

        /// <summary>
        ///  NSU no encontrado
        /// </summary>
        public static string NSU_NO_ENCONTRADO => "66";

        /// <summary>
        /// TrackId no encontrado
        /// </summary>
        public static string TRACKID_NO_ENCONTRADO => "90";

        /// <summary>
        /// Validaciones contienen errores en campos mandatorios
        /// </summary>
        public static string VALIDACION_CON_ERRORES => "99";

        /// <summary>
        /// Acción completada OK
        /// </summary>
        public static string ACCION_COMPLETADA => "100";

        /// <summary>
        /// Pasados 8 días después de la recepción no es posible registrar eventos
        /// </summary>
        public static string NO_SE_REGISTRARON_LOS_EVENTOS => "200";

        /// <summary>
        /// Evento registrado previamente
        /// </summary>
        public static string EVENTO_REGISTRADO_PREVIAMENTE => "201";

        /// <summary>
        /// No se puede rechazar un documento que ha sido aceptado previamente
        /// </summary>
        public static string NO_SE_PUEDE_RECHAZAR_DOCUMENTO_ACEPTADO => "202";

        /// <summary>
        /// No se puede aceptar un documento que ha sido rechazado previamente
        /// </summary>
        public static string NO_SE_PUEDE_ACEPTAR_DOCUMENTO_RECHAZADO => "203";

        /// <summary>
        /// No se puede dar recepción de bienes a un documento que ha sido rechazado previamente
        /// </summary>
        public static string NO_SE_PUEDE_DAR_RECEPCION_DE_BIENES => "204";

        /// <summary>
        ///  XML no encontrado
        /// </summary>
        public static string XML_NO_ENCONTRADO => "205";

        /// <summary>
        /// XML con errores en los campos numero documento Emisor o Numero documento Receptor Receptor
        /// </summary>
        public static string XM_CON_ERRORES_PRIMARIOS => "206";

        /// <summary>
        /// Evento no Implementado
        /// </summary>
        public static string EVENTO_NO_IMPLEMENTADO => "222";

        /// <summary>
        ///  No autorizado
        /// </summary>
        public static string NO_AUTORIZADO => "401";
    }
}