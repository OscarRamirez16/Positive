using System.IO;
using System.Xml;

namespace eFacturacionColombia_V2.Util
{
    public static class XmlDocumentExtensions
    {
        public static string ToXmlString(this XmlDocument document, bool omitXmlDeclaration = false)
        {
            using (var sw = new StringWriter())
            {
                var settings = new XmlWriterSettings
                {
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace,
                    Indent = true,
                    OmitXmlDeclaration = omitXmlDeclaration
                };

                using (var xw = XmlWriter.Create(sw, settings))
                {
                    document.WriteTo(xw);

                    xw.Flush();
                    sw.Flush();

                    return sw.ToString();
                }
            }
        }
    }
}