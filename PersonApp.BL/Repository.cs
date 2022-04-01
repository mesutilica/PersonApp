using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PersonApp.DAL;
using PersonApp.Entities;

namespace PersonApp.BL
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private DataBaseContext context;
        private DbSet<T> _objectSet;
        public Repository()
        {
            if (context == null)
            {
                context = new DataBaseContext();
                _objectSet = context.Set<T>();
            }
        }
        public int Add(T entity)
        {
            _objectSet.Add(entity);
            return context.SaveChanges();
        }

        public async Task<int> AddAsync(T entity)
        {
            await _objectSet.AddAsync(entity);
            return await context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _objectSet.AddRangeAsync(entities);
        }

        public T Find(int id)
        {
            return _objectSet.Find(id);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return _objectSet.FirstOrDefaultAsync(expression);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _objectSet.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _objectSet.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _objectSet.Where(expression).ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _objectSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _objectSet.Where(expression).ToListAsync();
        }

        public IQueryable<T> GetAllInclude(string table)
        {
            return _objectSet.Include(table);
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(string table)
        {
            return await _objectSet.Include(table).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(string table, Expression<Func<T, bool>> expression)
        {
            return await _objectSet.Include(table).Where(expression).ToListAsync();
        }

        public async Task<T> FindAsync(int id)
        {
            return await _objectSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _objectSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _objectSet.Update(entity);
        }

        public T Get()
        {
            return _objectSet.FirstOrDefault();
        }

        public T GetInclude(string table, Expression<Func<T, bool>> expression)
        {
            return _objectSet.Include(table).FirstOrDefault(expression);
        }

        public async Task<T> GetIncludeAsync(string table, Expression<Func<T, bool>> expression)
        {
            return await _objectSet.Include(table).FirstOrDefaultAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
