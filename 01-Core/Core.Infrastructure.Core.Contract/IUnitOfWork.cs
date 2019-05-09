using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Core.Contract
{
    public interface IUnitOfWork
    {
        /// <summary>
        ///     To save context data end end transaction
        /// </summary>
        void EndTransaction();

        /// <summary>
        ///     To clear memory leak
        /// </summary>
        void Dispose();

        /// <summary>
        ///     MainRepository Interface
        /// </summary>
        /// <typeparam name="T">Entity Classes</typeparam>
        /// <returns>MainRepository Object</returns>
        IRepository<T> Repository<T>() where T : class;

        /// <summary>
        ///     To clear memory leak byParameter
        /// </summary>
        /// <param name="disposing"></param>
        void Dispose(bool disposing);
    }
}