using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeoSolution.Shared.Services.Messaging.Abstractions
{
    public interface IEventHandler
    {
        string EventType { get; }
        Task HandleAsync(JsonElement payload, CancellationToken cancellationToken);
    }
}
