using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Infrastructure.Core.Helper;
using Core.Infrastructure.Domain.Aggregate.Base;
using Core.Infrastructure.Domain.Contract.DTO.RefType;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public interface IRefTypeService: IBaseService<RefTypeDTO>
    {
        Task<ResponseDTO<AddRefTypeResponseDTO>> CreateAsync(AddRefTypeRequestDTO DTO);
        Task<ResponseListDTO<RefTypeDTO>> GetRefTypesAsync();
        Task<ResponseDTO<RefTypeDTO>> GetByIdAsync(long contextSourceId);
        Task<ResponseListDTO<RefTypeDTO>> GetByParentAsync(long parentId);
    }
}
