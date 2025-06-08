using GeoSolution.Shared.Models.MQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSolution.Consumer.Services.Notification
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
