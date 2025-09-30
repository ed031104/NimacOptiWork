using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<TDomain, TEntity>
        where TDomain : class
        where TEntity : class
    {
        Task<IEnumerable<TDomain>> GetAllAsync();
        Task<TDomain?> GetByIdAsync(int id);
        Task AddAsync(TDomain entity);
        Task UpdateAsync(TDomain entity);
        Task DeleteAsync(int id);
    }
}
