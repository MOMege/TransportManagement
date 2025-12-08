using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;
using TransportManagement.Infrastructure.Persistence;

namespace TransportManagement.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TransportDbContext? _dbcontext;
        protected readonly DbSet<T> _dbset;

       public Repository( TransportDbContext dbContext)
        {
         _dbcontext = dbContext;
         _dbset= dbContext.Set<T>();

        }
        // Addasync  entity To database
        public async Task AddSync(T entity)
       => await _dbset.AddAsync(entity);

        //delete from dataset
        public void DeleteSync(T entity)
       => _dbset.Remove(entity);
        //Update from data set
        public void UpdateSync(T entity)
       => _dbset.Update(entity);
        //find entity by condition 
        public async  Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> Predicate)
        => await _dbset.Where(Predicate).ToListAsync();

      
        public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbset.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id)
        => await _dbset.FindAsync(id);

       
    }
}
