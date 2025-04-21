using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Manager_Device_Service.Repositories.Interface.ISeedWorks
{
    public interface IRepositoryBase<T, Key> where T : class
    {
        Task<T> GetByIdAsync(Key id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        #region ignore
        //void Add(T entity);
        //void AddRange(IEnumerable<T> entities);
        //void Remove(T entity);
        //void RemoveRange(IEnumerable<T> entities);
        #endregion


        //tny add - has actor 
        Task<T> CreateAsync(T entity);

        Task<IList<Key>> CreateRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(IEnumerable<T> entities);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(IEnumerable<T> entities);

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task EndTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
