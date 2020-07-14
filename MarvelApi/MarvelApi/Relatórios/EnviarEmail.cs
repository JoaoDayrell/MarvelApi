using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;

namespace MarvelApi
{
    class EnviarEmail

    {
        //Definição dos valores retirados do site mailTrap
        static string enderecoSmtp = "smtp.mailtrap.io";
        static int porta = 587;
        static bool enableSSL = true;
        static string enderecoEnvio = "teste@123.com"; 
        static string senha = "e866d2b421a5f2"; 
      
        
         
      
        //Metodo de envio de e-mail solicitando como parametro o destinarário, o nome de exibição, o corpo e o caminho do anexo
        public  void Enviar(string destinatario, string exibicao, string assunto, string corpo, string caminho)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(enderecoEnvio, exibicao);
                mail.To.Add(destinatario);
                mail.Subject = assunto;
                mail.Body = corpo;
                mail.IsBodyHtml = true;
                Attachment attachment = new Attachment(caminho, MediaTypeNames.Text.Plain);
                mail.Attachments.Add(attachment);
                using (SmtpClient smtp = new SmtpClient(enderecoSmtp, porta))
                {
                    smtp.Credentials = new NetworkCredential(enderecoEnvio, senha);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }

        }
        //Metodo de envio de e-mail solicitando como parametro o destinarário, o nome de exibição
        public void Enviar(string destinatario, string exibicao, string assunto, string corpo)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(enderecoEnvio, exibicao);
                mail.To.Add(destinatario);
                mail.Subject = assunto;
                mail.Body = corpo;
                mail.IsBodyHtml = true;              
                using (SmtpClient smtp = new SmtpClient(enderecoSmtp, porta))
                {
                    smtp.Credentials = new NetworkCredential(enderecoEnvio, senha);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);

                }
            }
            
        

        }
        public void Enviar(string assunto, string corpo)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("e6f4fba92659e3", "e866d2b421a5f2"),
                EnableSsl = true
            };
            client.Send("from@example.com", "to@example.com", assunto, corpo);
            
        }
    }
}
