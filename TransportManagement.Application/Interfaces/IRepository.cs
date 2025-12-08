using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
  
        Task<T?> GetByIdAsync(Guid id);  
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> Predicate);

        Task AddSync(T entity);
        void DeleteSync(T entity);
        void UpdateSync(T entity);

    }
}
