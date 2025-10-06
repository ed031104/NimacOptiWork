using Application.Context;
using AutoMapper;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class GenericRepository<TDomain, TEntity> : IRepository<TDomain, TEntity> 
        where TDomain : class 
        where TEntity : class
    {

        private readonly IMapper _mapper;
        private readonly NimacOptiWorkContext dbContext;

        public GenericRepository(NimacOptiWorkContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        
        public async Task AddAsync(TDomain entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<int> Count()
        {
            return await dbContext.Set<TEntity>().CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            dbContext.Remove(id);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            var entities = await dbContext.Set<TEntity>().ToListAsync();
            return _mapper.Map<IEnumerable<TDomain>>(entities);
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await dbContext.Set<TEntity>()
                .Skip((pageNumber) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TDomain>>(entities);
        }

        public async Task<TDomain?> GetByIdAsync(int id)
        {
            return await dbContext.Set<TDomain>().FindAsync(id);
        }
        
        public async Task UpdateAsync(TDomain entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
