using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IServices<TDomain, TEntity>
        where TDomain : class
        where TEntity : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<TDomain?> GetByIdAsync(int id);
        System.Threading.Tasks.Task AddAsync(TDomain entity);
        System.Threading.Tasks.Task UpdateAsync(TDomain entity);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
