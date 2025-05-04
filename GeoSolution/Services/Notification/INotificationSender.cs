using GeoSolution.Models.MQ;

namespace GeoSolution.Services.Notification
{
    public interface INotificationSender
    {
        Task SendAsync(UserRegisteredEvent evt, CancellationToken ct = default);
    }
}
