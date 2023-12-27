using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Entities.Abstract;

namespace TurtLearn.Shared.DataAccess.Repositories
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        T GetById(int id); //id'ye göre getirir
        Task<T> GetByIdAsync(int id);//id'ye göre asenkron getirir
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);//linq sorgusu yaparak getirir
        IQueryable<T> GetAll(); // entityleri getirir
        void Add(T Entity); //ekler
        void AddRange(IEnumerable<T> entities); //çoklu entity ekler
        void Update(T Entity);
        void Delete(T Entity);
        Task BulkInsert(IList<T> entities);
        #region dynamic Repository
        /// <summary>
        /// Asenkron olarak ilgili kaydı verilen predicateye göre getirir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        //var kullanici = repository.GetAsync(k=>k.Id==15)
        //IQueryable<T> GetAsQueryable();
        /// <summary>
        /// Asenkron olarak tüm kayıtları IList döner, predicate verilebilir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties);
        /// <summary>
        /// Asenkron olarak kayıt ekler.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Asenkron olarak kayıt günceller.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// denemek için yazıldı. state vererek güncelleme
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T UpdateWithStateAsync(T entity);
        /// <summary>
        /// Asenkron olarak kayıt siler. (Entity ile)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
        /// <summary>
        /// Verilen predicate'deki kayıt var mı yok mu?
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Verilen predicate'de kaç tane kayıt var?
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        #endregion
    }
}
