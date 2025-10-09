using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IServicesLogin
    {
        Task<bool> ValidateCredentialAsync(string username, string password);
        Task<bool> ValidatePasswordAsync(string username, string oldPassword, string newPassword);
        Task<bool> ChangePasswordAsync(string username, string newPassword);
        Task<bool> ValidateEmailAsync(string email);
    }
}
