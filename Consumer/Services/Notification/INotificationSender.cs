using GeoSolution.Shared.Models.MQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSolution.Consumer.Services.Notification
{
    public interface INotificationSender
    {
        Task SendAsync(UserRegisteredEvent evt, CancellationToken ct = default);
    }
}
