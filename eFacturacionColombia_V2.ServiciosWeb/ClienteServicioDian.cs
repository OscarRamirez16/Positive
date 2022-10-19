using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using eFacturacionColombia_V2.ServiciosWeb.DianServices;
using eFacturacionColombia_V2.Util;

namespace eFacturacionColombia_V2.ServiciosWeb
{
    public class ClienteServicioDian
    {
        public static string ENDPOINT_PRUEBAS = "https://vpfe-hab.dian.gov.co/WcfDianCustomerServices.svc";

        public static string ENDPOINT_PRODUCCION = "https://vpfe.dian.gov.co/WcfDianCustomerServices.svc";

        public AmbienteServicio Ambiente { get; set; }

        //public string RutaCertificado { get; set; }

        public string ClaveCertificado { get; set; }

        public X509Certificate2 Certificado { get; set; }

        public byte[] CertificadoFE { get; set; }


        public ClienteServicioDian()
        {
            Ambiente = AmbienteServicio.PRUEBAS;
        }

        public ClienteServicioDian(AmbienteServicio ambiente, byte[] certificadoFE, string claveCertificado)
        {
            Ambiente = ambiente;
            CertificadoFE = certificadoFE;
            ClaveCertificado = claveCertificado;
        }


        public WcfDianCustomerServicesClient ObtenerClienteServicio()
        {
            var binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.TransportWithMessageCredential;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
            binding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Basic256Sha256Rsa15;
            binding.Security.Message.EstablishSecurityContext = false;
            binding.MaxReceivedMessageSize = 1024 * 1024 * 25;

            var url = (Ambiente == AmbienteServicio.PRODUCCION) ? ENDPOINT_PRODUCCION : ENDPOINT_PRUEBAS;
            var endpoint = new EndpointAddress(url);

            var client = new WcfDianCustomerServicesClient(binding, endpoint);

            client.ClientCredentials.ClientCertificate.Certificate = Certificado ?? new X509Certificate2(CertificadoFE, ClaveCertificado);

            var vsd = client.Endpoint.EndpointBehaviors.FirstOrDefault((i) => i.GetType().Namespace == "Microsoft.VisualStudio.Diagnostics.ServiceModelSink");
            if (vsd != null) client.Endpoint.Behaviors.Remove(vsd);

            return client;
        }

        public UploadDocumentResponse EnviarSetDocumentos(List<byte[]> xmlFirmados)
        {
            var client = ObtenerClienteServicio();

            var files = new Dictionary<string, byte[]>();

            for (int x = 0; x < xmlFirmados.Count; x++)
            {
                var name = "document" + (x + 1).ToString("00") + ".xml";

                files.Add(name, xmlFirmados[x]);
            }

            var bytesZip = ZipHelper.Compress(files);

            return client.SendBillAsync("documents.zip", bytesZip);
        }

        public UploadDocumentResponse EnviarSetPruebas(List<byte[]> xmlFirmados, string idSetPruebas)
        {
            var client = ObtenerClienteServicio();

            var files = new Dictionary<string, byte[]>();

            for (int x = 0; x < xmlFirmados.Count; x++)
            {
                var name = "document" + (x + 1).ToString("00") + ".xml";

                files.Add(name, xmlFirmados[x]);
            }

            var bytesZip = ZipHelper.Compress(files);

            return client.SendTestSetAsync("documents.zip", bytesZip, idSetPruebas);
        }

        public DianResponse EnviarDocumento(byte[] xmlFirmado)
        {
            var client = ObtenerClienteServicio();

            var bytesZip = ZipHelper.Compress(xmlFirmado, "document.xml");

            return client.SendBillSync("document.zip", bytesZip);
        }

        public DianResponse[] ConsultarDocumentos(string zipKey)
        {
            var client = ObtenerClienteServicio();

            return client.GetStatusZip(zipKey);
        }

        public DianResponse ConsultarDocumento(string uuid)
        {
            var client = ObtenerClienteServicio();

            return client.GetStatus(uuid);
        }

        public DianResponse EnviarEvento(byte[] xmlFirmado)
        {
            var client = ObtenerClienteServicio();

            var bytesZip = ZipHelper.Compress(xmlFirmado, "event.xml");

            return client.SendEventUpdateStatus(bytesZip);
        }

        public EventResponse ObtenerDocumentoXml(string uuid)
        {
            var client = ObtenerClienteServicio();

            return client.GetXmlByDocumentKey(uuid);
        }

        public NumberRangeResponseList ObtenerRangosNumeracion(string nit, string identificadorSoftware)
        {
            var client = ObtenerClienteServicio();
            return client.GetNumberingRange(nit, nit, identificadorSoftware);
        }
    }
}
