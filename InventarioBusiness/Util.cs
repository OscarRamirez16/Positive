using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventarioDao;
using InventarioItem;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace InventarioBusiness
{
    public class Util
    {
        string key = "hqs";

        /// <summary>
        /// Valida la estructura de un correo electronico.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>booleano según sea la respuesta de la validación</returns>
        public static bool ValidarEmail(string email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene la hora según la configuración de la zona horaria donde se encuentre la empresa
        /// </summary>
        /// <returns></returns>
        public static DateTime ObtenerFecha(long IdEmpresa)
        {
            tblEmpresaBusiness oEmpB = new tblEmpresaBusiness(ObtenerCadenaConexion());
            DateTime dt = DateTime.Now;
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(oEmpB.ObtenerEmpresa(IdEmpresa).ZonaHoraria);
            dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, timeZone);
            return dt;
        }

        public static string ObtenerCadenaConexion()
        {
            string CadenaConexion = ConfigurationManager.AppSettings["Inventario"];
            return CadenaConexion;
        }

        /// <summary>
        /// Valida si una cadena se puede convertir en entero
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool EsEntero(string strValor) {
            int intValor;
            try
            {
                intValor = int.Parse(strValor);
                return true;
            }
            catch {
                return false;
            }
        }
        /// <summary>
        /// Valida si una cadena se puede convertir en decimal
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool EsDecimal(string strValor)
        {
            decimal decValor;
            try
            {
                decValor = decimal.Parse(strValor);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Valida si una cadena se puede construir en un tipo fecha
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool EsFecha(string strValor) {
            DateTime dateValor;
            try
            {
                dateValor = DateTime.Parse(strValor);
                return true;
            }
            catch {
                return false;
            }
        }
        /// <summary>
        /// Valida si una cadena se puede convertir en Hora (DateTime)
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static bool EsHora(string strValor) {
            DateTime hourValor;
            try
            {
                hourValor = ObtenerHora(strValor);
                return true;
            }
            catch {
                return false;
            }
        }
        /// <summary>
        /// Convierte un string en un valor hora valido
        /// </summary>
        /// <param name="strValor"></param>
        /// <returns></returns>
        public static DateTime ObtenerHora(string strValor) {
            return DateTime.Parse(string.Format("1900/01/01 {0}", strValor));
        }
        /// <summary>
        /// Convierte una hora para presentacion
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        public static string ConvertirHoraGUI(DateTime dateValue) {
            string strValor ="";
            string strAMPM = "";
            int Hour;
            DateTime minValue = new DateTime(1900, 01, 01);
            if (dateValue != minValue) {
                if (dateValue.Hour > 12)
                {
                    Hour = dateValue.Hour - 12;
                    strAMPM = "PM";
                }
                else
                {
                    Hour = dateValue.Hour;
                    strAMPM = "AM";
                }
                strValor = string.Format("{0}:{1} {2}", Hour, dateValue.Minute.ToString().PadLeft(2, char.Parse("0")), strAMPM);
            }                         
            return strValor;
        }
        /// <summary>
        /// Metodo para encriptar en MD5
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public string Encriptar(string texto)
        {
            //arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            //arreglo de bytes donde guardaremos el texto
            //que vamos a encriptar
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

            //se utilizan las clases de encriptación
            //provistas por el Framework
            //Algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //se guarda la llave para que se le realice
            //hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            //Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //se empieza con la transformación de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //arreglo de bytes donde se guarda la
            //cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

            tdes.Clear();

            //se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }
        /// <summary>
        /// Metodo para desencriptar
        /// </summary>
        /// <param name="textoEncriptado"></param>
        /// <returns></returns>
        public string Desencriptar(string textoEncriptado)
        {
            byte[] keyArray;
            //convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

            //se llama a las clases que tienen los algoritmos
            //de encriptación se le aplica hashing
            //algoritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

            tdes.Clear();
            //se regresa en forma de cadena
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string ObtenerFormatoEntero()
        {
            try
            {
                return "C0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ObtenerFormatoDecimal()
        {
            try
            {
                return "C2";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int ImagenAlto() {
            try
            {
                return int.Parse(ConfigurationManager.AppSettings["ImagenAlto"]);
            }
            catch {
                return 128;
            }
        }
        public static int ImagenAncho()
        {
            try
            {
                return int.Parse(ConfigurationManager.AppSettings["ImagenAncho"]);
            }
            catch
            {
                return 128;
            }
        }
    }
}
