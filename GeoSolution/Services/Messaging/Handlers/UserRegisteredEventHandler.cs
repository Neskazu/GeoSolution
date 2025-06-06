using GeoSolution.Models.MQ;
using GeoSolution.Services.Messaging.Abstractions;
using GeoSolution.Services.Notification;
using System.Text.Json;

namespace GeoSolution.Services.Messaging.Handlers
{
    public class UserRegisteredEventHandler : IEventHandler
    {
        public string EventType => "UserRegistered";
        private readonly NotificationManager _notificationManager;
        public UserRegisteredEventHandler(NotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        public async Task HandleAsync(JsonElement payload, CancellationToken cancellationToken)
        {
            var userEvent = payload.Deserialize<UserRegisteredEvent>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (userEvent == null)
                throw new InvalidOperationException("Cant deserialize UserRegisteredEvent.");

            await _notificationManager.HandleAsync(userEvent, cancellationToken);
        }
    }
}
