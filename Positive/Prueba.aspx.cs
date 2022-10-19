using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;

namespace Positive
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPrueba_Click(object sender, EventArgs e)
        {
            //MailMessage correo = new MailMessage();
            //correo.From = new MailAddress("agroserviciosdelcauca.app@gmail.com", "Oscar Ramirez", System.Text.Encoding.UTF8);//Correo de salida
            //correo.To.Add("osragu90@gmail.com"); //Correo destino?
            //correo.To.Add("juangmg40@gmail.com"); //Correo destino?
            //correo.Subject = "Correo de prueba"; //Asunto
            //correo.Body = "Este es un correo de prueba desde c#"; //Mensaje del correo
            //correo.IsBodyHtml = true;
            //correo.Priority = MailPriority.Normal;
            //SmtpClient smtp = new SmtpClient();
            //smtp.UseDefaultCredentials = false;
            //smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            //smtp.Port = 587; //Puerto de salida
            //smtp.Credentials = new System.Net.NetworkCredential("agroserviciosdelcauca.app@gmail.com", "1234.5678");//Cuenta de correo
            //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            //smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.Send(correo);


            MailMessage email = new MailMessage();
            SmtpClient protocol = new SmtpClient();
            email.To.Add("osragu90@gmail.com");
            email.To.Add("juangmg40@gmail.com");
            string message = $"<h1>Sr(a). Juan Gabriel</h1><p><h2>Cordial saludo,</h2></p><h1></p>";
            email.From = new MailAddress("agroserviciosdelcauca.app@gmail.com", "Oscar Ramirez", System.Text.Encoding.UTF8);
            email.Subject = "Correo de prueba";
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = message;
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = true;
            //email.Attachments.Add();

            protocol.Credentials = new System.Net.NetworkCredential("agroserviciosdelcauca.app@gmail.com", "1234.5678");
            protocol.Port = 587;
            protocol.Host = "smtp.gmail.com";
            protocol.EnableSsl = true;
            protocol.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                protocol.Send(email);
            }
            catch (SmtpException ex)
            {

            }
        }
    }
}