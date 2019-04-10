using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.IO;
using dTax.Data.Interfaces;

namespace dTax.Services
{
    public class EmailService
    {
        private IDBWorkFlow DBWorkflow;

        public EmailService(IDBWorkFlow dBWorkFlow)
        {
            DBWorkflow = dBWorkFlow;
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "dTax-mailing@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            await SendEmail(emailMessage);

        }
        // TODO  string remoteIpAddress
        public async Task AuthEmailAsync(string email)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("dTax-info", "dTax-mailing@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Оповещение системы безопасности";

            var user = DBWorkflow.UserRepository.FindUserEmail(email);

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<p>Здравствуйте, " + "<strong>"+user.FirstName + "</strong>" +
                @"<br><p>В ваш аккаунт выполнен вход<br>
                    <p>Это письмо было отправлено вам по соображениям безопасности. 
Мы не смогли определить, выполнялся ли ранее вход в систему через данное устройство или приложение. 
Возможно, его выполнили Вы, воспользовавшись новым компьютером, телефоном или браузером. 
Если Вы не производили подобных действий, то существует высокая вероятность того, 
что Ваш аккаунт был взломан.<br>
                                                <p><sub>Письмо сгенерировано автоматически и не требует ответа.<sub><br>"
            };

            //string html = File.ReadAllText("~/HtmlResources/Auth.html");


            await SendEmail(emailMessage);
        }

        private async Task SendEmail(MimeMessage message)
        {
            var client = new SmtpClient();

            await client.ConnectAsync("smtp.yandex.ru", 25, false);
            await client.AuthenticateAsync("dTax-mailing@yandex.ru", "M9S206");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }



    }
}
