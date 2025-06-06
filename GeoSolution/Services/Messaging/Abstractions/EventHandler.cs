using System.Text.Json;

namespace GeoSolution.Services.Messaging.Abstractions
{
    public abstract class EventHandler<T> : IEventHandler
    {
        public abstract string EventType { get; }
        protected abstract Task HandleDomainAsync(T @event, CancellationToken cancellationToken);
        public async Task HandleAsync(JsonElement payload, CancellationToken cancellationToken)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var domainEvent = payload.Deserialize<T>(options);
            if (domainEvent == null)
                throw new InvalidOperationException(
                    $"Payload is null for {typeof(T).Name}");

            await HandleDomainAsync(domainEvent, cancellationToken);
        }
    }
}
