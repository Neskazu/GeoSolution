using GeoSolution.Models.MQ;

namespace GeoSolution.Services.Notification
{
    public interface INotificationManager
    {
        Task HandleAsync(UserRegisteredEvent evt, CancellationToken ct = default);
    }
}
