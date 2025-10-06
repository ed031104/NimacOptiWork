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

        public async Task<IEnumerable<TDomain>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<TDomain?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(TDomain entity) => await _repository.AddAsync(entity);
        public async Task UpdateAsync(TDomain entity) => await _repository.UpdateAsync(entity);
        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
        public async Task<IEnumerable<TDomain>> GetAllAsync(int pageNumber, int pageSize) => await _repository.GetAllAsync(pageNumber, pageSize);
        public async Task<int> Count() => await _repository.Count();
    }
}
