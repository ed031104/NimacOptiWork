using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Generic
{
    public class ServicesGeneric<TDomain, TEntity> : IServices<TDomain, TEntity> 
        where TDomain : class
        where TEntity : class
    {
        private readonly IRepository<TDomain, TEntity> _repository;

        public ServicesGeneric(IRepository<TDomain, TEntity> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TDomain>> GetAllAsync() => _repository.GetAllAsync();
        public Task<TDomain?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public System.Threading.Tasks.Task AddAsync(TDomain entity) => _repository.AddAsync(entity);
        public System.Threading.Tasks.Task UpdateAsync(TDomain entity) => _repository.UpdateAsync(entity);
        public System.Threading.Tasks.Task DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
