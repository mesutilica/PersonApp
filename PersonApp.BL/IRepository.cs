using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PersonApp.BL
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> expression);
        T Get();
        T Get(Expression<Func<T, bool>> expression);
        T GetInclude(string table, Expression<Func<T, bool>> expression);
        T Find(int id);
        int Add(T entity);
        int Update(T entity);
        int Remove(T entity);
        IQueryable<T> GetAllInclude(string table);

        Task<T> FindAsync(int id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> GetIncludeAsync(string table, Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllIncludeAsync(string table);
        Task<IEnumerable<T>> GetAllIncludeAsync(string table, Expression<Func<T, bool>> expression);
        Task<int> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
    }
}
