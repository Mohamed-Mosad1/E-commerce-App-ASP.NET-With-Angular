using E_commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity<int>
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);

        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
