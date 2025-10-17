using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Generic
{
    public class ServicesLogin : IServicesLogin
    {
        IRepositoryLogin _repositoryLogin;
        UserSession _userSession;

        public ServicesLogin(IRepositoryLogin login, UserSession userSession)
        {
            _repositoryLogin = login;
            _userSession = userSession;
        }

        public async Task<bool> ChangePasswordAsync(string username, string newPassword) => await _repositoryLogin.ChangePasswordAsync(username, newPassword);

        public async Task<bool> ValidateCredentialAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            bool isvalidUser = await _repositoryLogin.ValidateCredentialAsync(username, password);

            if (!isvalidUser) {
                return false;
            }

            var roleUser = await _repositoryLogin.getRoleUser(username);

            if (roleUser == null) {
                return false;
            }

            _userSession.SetSession(roleUser.User.Id, roleUser.User.Email, roleUser.User.Email, roleUser.Rol.Name, roleUser.Rol.Id);

            return true;
        }

        public async Task<bool> ValidateEmailAsync(string email) => await _repositoryLogin.ValidateEmailAsync(email);

        public async Task<bool> ValidatePasswordAsync(string username, string oldPassword, string newPassword) => await _repositoryLogin.ValidatePasswordAsync(username, oldPassword, newPassword);
    }
}
