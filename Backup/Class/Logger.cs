using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Backup
{
    public class Logger
    {
        public static void WriteToLog(string message)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"{message} - {DateTime.Now}");
            }
        }
        public static void WriteToMailLog(string message)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailLog.txt");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"{message} - {DateTime.Now}");
            }

            SendEmail(XmlDetaylar.GetMail(),"Backup Projesi ile ilgili bilgi",$"Backup projesinde bir mesaj\n\n\nMesaj: {message}\n {DateTime.Now}\n\n");
        }
       

        public static void SendEmail(string userEmail, string subject, string body)
        {
            try
            {
                string email = "kanca1268@gmail.com";
                string sifre = "yyka fsgg yhlw oeit";//Emreee1230.//Yunusemre1268.
                string host = "smtp.gmail.com";
                int port = 587;
                using (MailMessage mail = new MailMessage(email, userEmail, subject, body))
                {
                    using (SmtpClient smtp = new SmtpClient(host, port))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(email, sifre); // Uygulama şifresi
                        smtp.Send(mail);
                    }
                }
                //

                // E-posta gönderimini kaydet
                WriteToLog($"{userEmail}: Email sent successfully.");
            }
            catch (SmtpException ex)
            {
                // Hata oluştuğunda kaydet
                WriteToLog($"{userEmail}: Error sending email: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Genel hatalar için
                WriteToLog($"{userEmail}: Unexpected error: {ex.Message}");
            }
        }
    }
}
