using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Customer.Microservice.Infrastructure.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        #region LINQ QUERY

        /// <summary>
        /// Count all item in a DB table.
        /// </summary>
        int Count { get; }
        /// <summary>
        /// Count all item in a DB table.
        /// </summary>
        long LongCount { get; }
        /// <summary>
        /// add a item in a table. item never be added untill call savechanges method.
        /// </summary>
        /// <param name="item">object of a class which will be added into corresponding DB table.</param>
        void Add(T item);

        /// <summary>
        /// Remove a item in a table. item never be Removed untill call savechanges method.
        /// </summary>
        /// <param name="item">object of a class which will be Removed into corresponding DB table.</param>
        void Remove(T item);

        /// <summary>
        /// Modify a item in a table. item never be Modified untill call savechanges method.
        /// </summary>
        /// <param name="item">object of a class which will be Modified into corresponding DB table.</param>
        void Modify(T item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition.</param>
        /// <returns>Return a object of a class into corresponding DB table.</returns>
        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>  An System.Linq.IQueryable`1 that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
        IQueryable<T> All();
        int CountFunc(Expression<Func<T, bool>> predicate);
        long LongCountFunc(Expression<Func<T, bool>> predicate);
        bool IsExist(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        T Find(Expression<Func<T, bool>> predicate);
        string Max(Expression<Func<T, string>> predicate);
        string MaxFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition.</param>
        /// <returns>  An System.Linq.IQueryable`1 that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition.</param>
        /// <returns>  An System.Linq.IQueryable`1 that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a item in a table.
        /// </summary>
        /// <param name="item">object of a class which will be Modified into corresponding DB table.</param>
        /// <returns>Return a object of a class into corresponding DB table.</returns>

        T Create(T item);

        /// <summary>
        /// Update a item in a table.
        /// </summary>
        /// <param name="item">object of a class which will be Modified into corresponding DB table.</param>
        /// <returns>Return a '1' if successfully update or '0' if failed to  update.</returns>
        int Update(T item);
        int Update(Expression<Func<T, bool>> predicate);
        int Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
        int Delete(T item);
        int Delete(Expression<Func<T, bool>> predicate);
        string Min(Expression<Func<T, string>> predicate);
        string MinFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);

        #endregion

        #region DATABSE TRANSACTION

        void Attach<TEntity>(TEntity item) where TEntity : class;
        void SetModified<TEntity>(TEntity item) where TEntity : class;
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
        void Commit();
        int SaveChanges();
        void CommitAndRefreshChanges();
        void RollbackChanges();

        #endregion

        #region LINQ ASYNC

        Task<ICollection<T>> GetAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<int> UpdateAsync(T item);
        Task<int> UpdateAsync(Expression<Func<T, bool>> predicate);
        Task<int> DeleteAsync(T t);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<long> LongCountAsync();
        Task<int> CountFuncAsync(Expression<Func<T, bool>> predicate);
        Task<long> LongCountFuncAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<string> MaxAsync(Expression<Func<T, string>> predicate);
        Task<string> MaxFuncAsync(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);
        Task<string> MinAsync(Expression<Func<T, string>> predicate);
        Task<string> MinFuncAsync(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
        Task<int> SaveChangesAsync();
        #endregion

        int UpdateTest<TViewModel>(Expression<Func<T, bool>> predicate, TViewModel item);
    }
}
