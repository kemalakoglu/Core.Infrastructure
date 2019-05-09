using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Infrastructure.Core.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Domain.Repository
{
    public class MainRepository<T> : IRepository<T> where T : class
    {
        public MainRepository(Context.Context.Context repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        protected Context.Context.Context repositoryContext { get; set; }

        /// <summary>
        ///     UserManager
        /// </summary>
        public UserManager<IdentityUser<string>> UserManager { get; set; }

        /// <summary>
        ///     RoleManager
        /// </summary>
        public RoleManager<IdentityUserRole<string>> RoleManager { get; set; }

        public IEnumerable<T> FindAll()
        {
            return repositoryContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return repositoryContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            repositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            repositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            repositoryContext.Set<T>().Remove(entity);
        }

        public void DeleteBulk(IEnumerable<T> entity)
        {
            //repositoryContext.Set<T>().RemoveRange(entity);
            foreach (var item in entity)
                repositoryContext.Remove(item);
        }

        public void InsertBulk(IEnumerable<T> entity)
        {
            repositoryContext.Set<T>().AddRange(entity);
        }

        public void UpdateBulk(IEnumerable<T> entity)
        {
            repositoryContext.Set<T>().UpdateRange(entity);
        }

        public void Save()
        {
            repositoryContext.SaveChanges();

        }

        public T GetByKey(int key)
        {
            return repositoryContext.Set<T>().Find(key);
        }

        public T GetByKey(string key)
        {
            return repositoryContext.Set<T>().Find(key);
        }

        public T GetByKey(object key)
        {
            return repositoryContext.Set<T>().Find(key);
        }

        /// <summary>
        /// Query Method
        /// </summary>
        /// <returns>RepositoryQueryHelper</returns>
        public virtual IRepositoryQueryHelper<T> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQueryHelper<T>(this);

            return repositoryGetFluentHelper;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includeProperties = null, int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = repositoryContext.Set<T>();

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }
    }
}
