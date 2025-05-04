using GeoSolution.Models.MQ;

namespace GeoSolution.Services.Notification
{
    public class NotificationManager : INotificationManager
    {
        private readonly IEnumerable<INotificationSender> _senders;

        public NotificationManager(IEnumerable<INotificationSender> senders)
        {
            _senders = senders;
        }

        public async Task HandleAsync(UserRegisteredEvent evt, CancellationToken ct = default)
        {
            var hasEmail = !string.IsNullOrWhiteSpace(evt.Email);
            foreach (var sender in _senders)
            {
                try
                {
                    if(hasEmail)
                    await sender.SendAsync(evt, ct);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Ошибка в {sender.GetType().Name}: {ex.Message}");
                }
            }
        }
    }
}
