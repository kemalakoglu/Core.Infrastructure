using Core.Infrastructure.Core.Helper;
using System.Threading.Tasks;

namespace Core.Infrastructure.Domain.Aggregate.Base
{
    public interface IBaseService<T> where T : class
    {
        ResponseDTO<T> GetByKey(long key);
        ResponseDTO<T> Create(T DTO);
        ResponseDTO<T> Update(T DTO);
        ResponseDTO<T> Delete(T DTO);
        ResponseDTO<T> SoftDelete(long Id);
        Task<ResponseDTO<T>> GetByKeyAsync(long key);
        Task<ResponseDTO<T>> CreateAsync(T DTO);
        Task<ResponseDTO<T>> UpdateAsync(T DTO);
        Task<ResponseDTO<T>> DeleteAsync(T DTO);
        Task<ResponseDTO<T>> SoftDeleteAsync(long Id);
    }
}