using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSolution.Shared.Models.MQ
{
    public class UserRegisteredEvent
    {
        public string Id { get; }
        public string Username { get; }
        public string? Email { get; }
        public string? PhoneNumber { get; }
        public bool NotifyUserByEmail { get; }
        public bool NotifyUserBySms { get; }
        public bool NotifyAdminByEmail { get; }

        public UserRegisteredEvent(
            string id,
            string username,
            string? email,
            string? phoneNumber = null,
            bool notifyUserByEmail = true,
            bool notifyUserBySms = false,
            bool notifyAdminByEmail = false)
        {
            Id = id;
            Username = username;
            Email = email;
            PhoneNumber = phoneNumber;
            NotifyUserByEmail = notifyUserByEmail;
            NotifyUserBySms = notifyUserBySms;
            NotifyAdminByEmail = notifyAdminByEmail;
        }
    }
}
