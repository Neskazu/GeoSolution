using GeoSolution.Models.MQ;
using NETCore.MailKit.Core;

namespace GeoSolution.Services.Notification
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly IEmailService _email; 

        public EmailNotificationSender(IEmailService email)
        {
            _email = email;
        }

        public Task SendAsync(UserRegisteredEvent evt, CancellationToken ct = default)
        {
            var subject = "Добро пожаловать!";
            var body = $"Привет, {evt.Username}! Спасибо за регистрацию.";
            return _email.SendAsync(evt.Email, subject, body, false); 
        }

    }
}
