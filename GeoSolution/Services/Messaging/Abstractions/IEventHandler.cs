using System.Text.Json;

namespace GeoSolution.Services.Messaging.Abstractions
{
    public interface IEventHandler
    {
        string EventType {  get; }
        Task HandleAsync(JsonElement payload, CancellationToken cancellationToken);
    }
}
