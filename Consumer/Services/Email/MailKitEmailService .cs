using GeoSolution.Shared.Options;
using Microsoft.Extensions.Options;
using MimeKit;
using NETCore.MailKit.Core;
using System.Text;
using MailKit.Net.Smtp;
using NETCore.MailKit.Infrastructure.Internal;

namespace GeoSolution.Consumer.Services.Email
{
    public class MailKitEmailService : IEmailService
    {
        private readonly SmtpSettings _settings;

        public MailKitEmailService(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendAsync(string toEmail, string subject, string body, CancellationToken ct = default)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_settings.From));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_settings.Host, _settings.Port, _settings.UseSsl, ct);
            await client.AuthenticateAsync(_settings.Username, _settings.Password, ct);
            await client.SendAsync(message, ct);
            await client.DisconnectAsync(true, ct);
        }
        Task IEmailService.SendAsync(string mailTo, string subject, string message, bool isHtml, SenderInfo sender)
        {
            return SendAsyncInternal(mailTo, subject, message, isHtml);
        }
        private async Task SendAsyncInternal(string mailTo, string subject, string message, bool isHtml)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_settings.From));
            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = subject;

            email.Body = new TextPart(isHtml ? "html" : "plain")
            {
                Text = message
            };

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_settings.Host, _settings.Port, _settings.UseSsl);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
        void IEmailService.Send(string mailTo, string subject, string message, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        void IEmailService.Send(string mailTo, string subject, string message, string[] attachments, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        void IEmailService.Send(string mailTo, string subject, string message, Encoding encoding, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        void IEmailService.Send(string mailTo, string subject, string message, string[] attachments, Encoding encoding, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        void IEmailService.Send(string mailTo, string mailCc, string mailBcc, string subject, string message, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        void IEmailService.Send(string mailTo, string mailCc, string mailBcc, string subject, string message, string[] attachments, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        void IEmailService.Send(string mailTo, string mailCc, string mailBcc, string subject, string message, Encoding encoding, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        void IEmailService.Send(string mailTo, string mailCc, string mailBcc, string subject, string message, Encoding encoding, string[] attachments, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        Task IEmailService.SendAsync(string mailTo, string subject, string message, string[] attachments, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        Task IEmailService.SendAsync(string mailTo, string subject, string message, Encoding encoding, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        Task IEmailService.SendAsync(string mailTo, string subject, string message, string[] attachments, Encoding encoding, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        Task IEmailService.SendAsync(string mailTo, string mailCc, string mailBcc, string subject, string message, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        Task IEmailService.SendAsync(string mailTo, string mailCc, string mailBcc, string subject, string message, string[] attachments, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        Task IEmailService.SendAsync(string mailTo, string mailCc, string mailBcc, string subject, string message, Encoding encoding, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }

        Task IEmailService.SendAsync(string mailTo, string mailCc, string mailBcc, string subject, string message, string[] attachments, Encoding encoding, bool isHtml, SenderInfo sender)
        {
            throw new NotImplementedException();
        }
    }
}
