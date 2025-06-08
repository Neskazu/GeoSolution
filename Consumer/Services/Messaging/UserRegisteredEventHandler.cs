using GeoSolution.Consumer.Services.Notification;
using GeoSolution.Shared.Models.MQ;
using GeoSolution.Shared.Services.Messaging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeoSolution.Consumer.Services.Messaging
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
