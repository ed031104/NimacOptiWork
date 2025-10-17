using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserSession
    {
        public int? UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? IdRole { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }

        public UserSession() { }

        public UserSession(int userId, string userName, string email, string role, int? idRole)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Role = role;
            LoginTime = DateTime.Now;
            IdRole = idRole;
        } 

        public void SetSession(int userId, string userName, string email, string role, int? idRole)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Role = role;
            LoginTime = DateTime.Now;
            IdRole = idRole;
        }

        public bool IsLoggedIn()
        {
            return UserId.HasValue;
        }

        public void ClearSession()
        {
            UserId = null;
            UserName = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            LoginTime = DateTime.MinValue;
            IdRole = null;
        }
    }
}
