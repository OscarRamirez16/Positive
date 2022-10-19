using System.Security.Cryptography;
using System.Text;

namespace eFacturacionColombia_V2.Util
{
    public static class CryptographyHelper
    {
        public static byte[] Sha1(byte[] data)
        {
            using (var sha = new SHA1CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }

        public static string Sha1(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);

            var hash = Sha1(data);

            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static byte[] Sha256(byte[] data)
        {
            using (var sha = new SHA256CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }

        public static string Sha256(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);

            var hash = Sha256(data);

            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static byte[] Sha384(byte[] data)
        {
            using (var sha = new SHA384CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }

        public static string Sha384(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);

            var hash = Sha384(data);

            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static byte[] Sha512(byte[] data)
        {
            using (var sha = new SHA512CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }

        public static string Sha512(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);

            var hash = Sha512(data);

            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}