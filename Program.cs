﻿
using System;
using System.Net;
using System.Net.Mail;

namespace REDEBAN.PRY.BOTONCLOUD.SNS
{
    class Program
    {
        static void Main(string[] args)
        {
            String FROM = "megan71545@flmcat.com";
            String FROMNAME = "Luis";
            String TO = "jokkupusto@biyac.com";
            String SMTP_USERNAME = "AKIA4FWWTKX5BYNIXRHI";
            String SMTP_PASSWORD = "BPxlSW97wkXxAxnhOdC0c6hdXPNiCMK6Yg+XgVBfz6hm";
            String HOST = "email-smtp.sa-east-1.amazonaws.com";
            int PORT = 587;

            String SUBJECT = "Redeban - AMAZON SES test";
 
            String BODY = "<h1>Amazon SES Test</h1>" +
                "<p>Aquí va el correo con la plantilla embebida " +
                "<a href='https://aws.amazon.com/ses'>Amazon SES</a> SMTP interface " +
                "</p>";
 
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
         

            using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
            { 
                client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);                 
                client.EnableSsl = true;
 
                try
                {
                    Console.WriteLine("Intentando enviar el correo...");
                    client.Send(message);
                    Console.WriteLine("Email enviado!");
                    Console.Read();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("El email no fue enviado.");
                    Console.WriteLine("Error message: " + ex.Message);
                    Console.Read();
                }
            }
        }
    }
}
