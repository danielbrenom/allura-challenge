using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Allura.Challenge.Database.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        #region Reading
        bool Exists(string id);
        Task<bool> ExistsAsync(string id);
        T Get(string id);
        Task<T> GetAsync(string id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> query);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> query);

        #endregion

        #region Writing

        bool Add(T entity);
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity);
        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity);

        #endregion
    }
}