using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Infrastructure.Core.Contract.JoinedClass;

namespace Core.Infrastructure.Core.Contract
{
    public interface IRepositoryQueryHelper<T> where T : class
    {
        /// <summary>
        /// To filter Operations
        /// </summary>
        /// <param name="pFilter">filter exp. : <code>.Filter(x=>x.EntityProperty != null)</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> Filter(Expression<Func<T, bool>> pFilter);
        /// <summary>
        /// To Order Operations
        /// </summary>
        /// <param name="orderBy">Order Exp. <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code></param>
        /// <returns>Added filter functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        /// <summary>
        /// To GroupBy Operations
        /// </summary>
        /// <param name="groupBy">GroupBy Operations <code>.OrderBy(x => x.OrderBy(y => y.EntityProperty).ThenBy(z => z.EntityProperty2))</code></param>
        /// <returns>Added GroupBy functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> GroupBy(Func<IQueryable<T>, IGrouping<int, GroupCountResult>> groupBy);
        /// <summary>
        /// To Include Operations (Set Included Tables Data)
        /// </summary>
        /// <param name="expression">Include Exp. <code>.Include(x=>x.EntityName)</code></param>
        /// <returns>Added Include functionality to RepositoryQueryHelper Class</returns>
        IRepositoryQueryHelper<T> Include(Expression<Func<T, object>> expression);
        /// <summary>
        /// To paging by filtered, ordered and included or not
        /// </summary>
        /// <param name="pPage">Page Number</param>
        /// <param name="pPageSize">Data number</param>
        /// <param name="totalCount">Toplam Kayıt Sayısı</param>
        /// <returns>Data List</returns>
        IEnumerable<T> GetPage(
            int pPage, int pPageSize, out int totalCount);
        /// <summary>
        /// To get all data by filtered, ordered and included or not
        /// </summary>
        /// <returns>Data List</returns>
        IEnumerable<T> Get(bool isAsNoTracking = false);

        /// <summary>
        /// To get all data by filtered, ordered and included or not
        /// </summary>
        /// <returns>Data List</returns>
        T GetFirst();
    }
}