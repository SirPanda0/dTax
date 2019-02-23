﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace dTax.Services
{
    public class EmailService
    {
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

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("dTax-mailing@yandex.ru", "M9S206");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }

        }
    }
}
