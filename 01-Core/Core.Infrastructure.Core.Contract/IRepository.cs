using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Core.Contract
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        ///     UserManager
        /// </summary>
        UserManager<IdentityUser<string>> UserManager { get; set; }

        /// <summary>
        ///     RoleManager
        /// </summary>
        RoleManager<IdentityUserRole<string>> RoleManager { get; set; }

        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteBulk(IEnumerable<T> entity);
        void InsertBulk(IEnumerable<T> entity);
        void UpdateBulk(IEnumerable<T> entity);
        void Save();
        T GetByKey(int key);
        T GetByKey(string key);
        T GetByKey(object key);
        /// <summary>
        /// Query Method
        /// </summary>
        /// <returns>RepositoryQueryHelper (Sorgu Yardımcı Sınıfı)</returns>
        IRepositoryQueryHelper<T> Query();

        /// <summary>
        /// To Set Data to Table, Definition Some Helper Parameters
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);
    }
}