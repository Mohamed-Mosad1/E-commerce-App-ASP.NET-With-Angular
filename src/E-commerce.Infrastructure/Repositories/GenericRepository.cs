﻿using E_commerce.Core.Interfaces;
using E_commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking().ToList();
        }

        public async Task<T> GetByIdAsync(T id, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var item in include)
            {
                query = query.Include(item);
            }
            return await ((DbSet<T>)query).FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            // Apply any include
            foreach (var item in include)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(T id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T id, T entity)
        {
            var currentEntity = await _dbContext.Set<T>().FindAsync(id);
            if (currentEntity is not null)
            {
                _dbContext.Update(currentEntity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
