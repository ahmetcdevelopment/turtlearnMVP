using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace TurtLearn.Shared.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity Entity)
        {
            _context.Add(Entity);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().AnyAsync(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().CountAsync(predicate);
        }

        public void Delete(TEntity Entity)
        {
            _context.Entry(Entity).State = EntityState.Modified;
            _context.Set<TEntity>().Remove(Entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Remove(entity); });
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            //IQueryable<TEntity> query = _context.Set<TEntity>();
            //if (predicate !=null)
            //{
            //    query = query.Where(predicate);
            //}
            return _context.Set<TEntity>().AsNoTracking().AsQueryable();
            //return query.AsNoTracking();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.SingleOrDefaultAsync(); ;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity Entity)
        {
            _context.Attach(Entity);
            _context.Entry(Entity).State = EntityState.Modified;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => { _context.Set<TEntity>().Update(entity); });
            return entity;
        }

        public TEntity UpdateWithStateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }
    }
}
