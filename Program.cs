
using System;
using System.Net;
using System.Net.Mail;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.Runtime;

namespace REDEBAN.PRY.BOTONCLOUD.SNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Envio_Correo();
            //Envio_SMS();
        }

        static void Envio_Correo(){
            String FROM = "megan71545@flmcat.com"; //Cuenta verificada FROM
            String FROMNAME = "PANCHO"; 
            String TO = "megan71545@flmcat.com"; //Cuenta verificada TO
            String SMTP_USERNAME = "AKIA4FWWTKX5MRNYYIXE";  //Nombre Usuario SMTP
            String SMTP_PASSWORD = "BL0qSe9DP10vGvEZW0igs3fFL0yRZFpvdQQKgElSNjo0";  //Contraseña SMTP
            String HOST = "email-smtp.sa-east-1.amazonaws.com"; //Nombre del Servidor SMTP
            int PORT = 587;
            String SUBJECT = "Envio Correo por SNS"; 
            String BODY = "<h1>Amazon SES Test</h1>" +
                "<p>Aquí va el correo con la plantilla embebida</p>";

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(FROM, FROMNAME);
            message.To.Add(new MailAddress(TO));
            message.Subject = SUBJECT;
            message.Body = BODY;
            message.Attachments.Add(new Attachment(@"d:\Voucher_Test CARLOS_Y_JOSE_GARAY.pdf")); //Attachment url/bytes
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

    static void Envio_SMS(){
    //Para enviar un email por Amazon SNS se requiere: 
    //Crear un Grupo/Usuario en IAM con permisos para SNS
    //Luego de crear el usuario Amazon proveerá su AccessKeyID y SecretAccessKey
    String awsKeyId = "AKIA4FWWTKX5LGY65TU6"; //Access Key ID
    String awsKeySecret = "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"; //Secret Access Key

    String message = "Envio de Correo por SES";
    var awsCredentials = new BasicAWSCredentials(awsKeyId, awsKeySecret);
    var smsClient = new AmazonSimpleNotificationServiceClient(awsCredentials, Amazon.RegionEndpoint.SAEast1);
    var pubRequest = new PublishRequest
    {
        Message = message,
        PhoneNumber = "+51940375749" //Teléfono del Cliente
    };

    pubRequest.MessageAttributes.Add("AWS.SNS.SMS.SMSType", new MessageAttributeValue { StringValue = "Transactional", DataType = "String" });

    try {
        var response = smsClient.PublishAsync(pubRequest);
        Console.WriteLine("Mensaje enviado");
        Console.ReadKey();
        }
    catch (Exception ex) {
        Console.WriteLine("Caught exception publishing request:");
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        }
        }
    }
}

