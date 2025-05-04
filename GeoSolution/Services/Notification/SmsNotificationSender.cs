using GeoSolution.Models.MQ;

namespace GeoSolution.Services.Notification
{
    public class SmsNotificationSender : INotificationSender
    {
        public Task SendAsync(UserRegisteredEvent evt, CancellationToken ct = default)
        {
            Console.WriteLine($"SMS would be sent to {evt.Username}");
            return Task.CompletedTask;
        }
    }
}
