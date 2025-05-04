using GeoSolution.Models.MQ;
using NETCore.MailKit.Core;

namespace GeoSolution.Services.Notification
{
    public class AdminEmailNotificationSender : INotificationSender
    {
        private readonly IEmailService _email;
        private readonly string _adminEmail;

        public AdminEmailNotificationSender(IEmailService email, IConfiguration config)
        {
            _email = email;
            _adminEmail = config.GetValue<string>("Notifications:AdminEmail")
                          ?? throw new InvalidOperationException("AdminEmail is not configured");
        }

        public Task SendAsync(UserRegisteredEvent evt, CancellationToken ct = default)
        {
            var subject = "Новый пользователь зарегистрировался";
            var body = $@" Пользователь {evt.Username} ({evt.Email ?? "без email"}) зарегистрировался. ID: {evt.Id}";
            return _email.SendAsync(_adminEmail, subject, body, false);
        }
    }
}
