using GeoSolution.Shared.Models.MQ;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSolution.Consumer.Services.Notification
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
            if (evt.NotifyUserByEmail && !string.IsNullOrWhiteSpace(evt.Email))
            {
                var emailSenders = _senders.Where(s => s is EmailNotificationSender);
                foreach (var sender in emailSenders)
                {

                    try
                    {
                        await sender.SendAsync(evt, ct);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Ошибка в {sender.GetType().Name}: {ex.Message}");
                    }
                }
            }
            if (evt.NotifyUserBySms && !string.IsNullOrWhiteSpace(evt.PhoneNumber))
            {
                var smsSenders = _senders.Where(s => s is SmsNotificationSender);
                foreach (var sender in smsSenders)
                {
                    try
                    {
                        await sender.SendAsync(evt, ct);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Ошибка в {sender.GetType().Name}: {ex.Message}");
                    }
                }
            }
            if (evt.NotifyAdminByEmail)
            {
                var adminSenders = _senders.Where(s => s is AdminEmailNotificationSender);
                foreach (var sender in adminSenders)
                {
                    try
                    {
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
}
