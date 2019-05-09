using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Core.Contract.JoinedClass;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Domain.Repository
{
    public class RepositoryQueryHelper<T> : IRepositoryQueryHelper<T> where T : class
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="repository">Main Repository</param>
        public RepositoryQueryHelper(IRepository<T> repository)
        {
            this.repository = repository;
            includeProperties = new List<Expression<Func<T, object>>>();
        }

        /// <summary>
        ///     To filter Operations
        /// </summary>
        /// <param name="pFilter">filter exp. : <code>.Filter(x=>x.EntityProperty != null)</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        public IRepositoryQueryHelper<T> Filter(Expression<Func<T, bool>> pFilter)
        {
            filter = pFilter;
            return this;
        }

        /// <summary>
        ///     To Order Operations
        /// </summary>
        /// <param name="orderBy">
        ///     Order Exp.
        ///     <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code>
        /// </param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        public IRepositoryQueryHelper<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            orderByQuerable = orderBy;
            return this;
        }

        public IRepositoryQueryHelper<T> GroupBy(Func<IQueryable<T>, IGrouping<int, GroupCountResult>> groupBy)
        {
            groupByQuerable = groupBy;
            return this;
        }

        /// <summary>
        ///     To Include Operations (Set Included Tables Data)
        /// </summary>
        /// <param name="expression">Include Exp. <code>.Include(x=>x.EntityName)</code></param>
        /// <returns>Added Include functionality to RepositoryQueryHelper Class</returns>
        public IRepositoryQueryHelper<T> Include(Expression<Func<T, object>> expression)
        {
            includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<T> GetPage(int pPage, int pPageSize, out int totalCount)
        {
            page = pPage;
            pageSize = pPageSize;
            totalCount = repository.Get(filter).Count();

            return repository.Get(
                filter,
                orderByQuerable, includeProperties, page, pageSize);
        }

        public IEnumerable<T> Get(bool isAsNoTracking = false)
        {
            IQueryable<T> queryable = repository.Get(
                filter,
                orderByQuerable, includeProperties, page, pageSize);

            if (isAsNoTracking)
                queryable = queryable.AsNoTracking();

            return queryable;
        }

        public T GetFirst()
        {
            T queryable = repository.Get(
                filter,
                orderByQuerable, includeProperties, page, pageSize).FirstOrDefault();

            return queryable;
        }

        #region Properties

        private readonly List<Expression<Func<T, object>>> includeProperties;
        private readonly IRepository<T> repository;
        private Expression<Func<T, bool>> filter;

        private Func<IQueryable<T>,
            IOrderedQueryable<T>> orderByQuerable;

        private Func<IQueryable<T>,
            IGrouping<int, GroupCountResult>> groupByQuerable;

        private int? page;
        private int? pageSize;

        #endregion
    }
}