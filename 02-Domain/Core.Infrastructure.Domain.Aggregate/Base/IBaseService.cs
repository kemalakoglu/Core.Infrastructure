using Core.Infrastructure.Application.Contract.DTO;

namespace Core.Infrastructure.Domain.Aggregate.Base
{
    public interface IBaseService<T> where T : class
    {
        ResponseDTO GetByKey(string key);
        ResponseDTO Create(T DTO);
        ResponseDTO Update(T DTO);
        ResponseDTO Delete(T DTO);
    }
}