using Infraestructure.Context;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RepositoryLogin : IRepositoryLogin
    {
        private readonly NimacOptiWorkContext _context;
        private readonly IMapper _mapper;

        public RepositoryLogin(NimacOptiWorkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ChangePasswordAsync(string username, string newPassword)
        {
            try
            {
                var isUserExist = _context.Users
                    .Where(u => u.Username == username);

                if (!isUserExist.Any())
                {
                    return false;
                }

                var user = isUserExist.First();

                await _context.Users.ExecuteUpdateAsync(u => u.SetProperty(
                    user => user.Password, newPassword
                ));
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserRole> getRoleUser(string username)
        {
            try
            {
                var user = await _context.UserRoles
                    .Include(ur => ur.Rol)
                    .Include(ur => ur.User)
                    .Where(ur => ur.User.Username == username)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    return null!;
                }
                var userMapper = _mapper.Map<UserRole>(user);
                return userMapper;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidateCredentialAsync(string username, string password)
        {
            try
            {
                var isUserExist = _context.Users
                    .Where(u => u.Username == username && u.Password == password);
                return await isUserExist.AnyAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidateEmailAsync(string email)
        {
            try
            {
                var isEailExist = _context.Users
                    .Where(u => u.Email == email);

                return await isEailExist.AnyAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ValidatePasswordAsync(string username, string oldPassword, string newPassword)
        {
            try
            {
                var isUserExist = _context.Users
                    .Where(u => u.Username == username && u.Password == oldPassword);
                return await isUserExist.AnyAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
