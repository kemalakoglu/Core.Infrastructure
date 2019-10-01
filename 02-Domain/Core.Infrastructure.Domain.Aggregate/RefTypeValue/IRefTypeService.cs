using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Domain.Aggregate.Base;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public interface IRefTypeService: IBaseService<RefTypeDTO>
    {
        ResponseListDTO<RefTypeDTO> GetByParent(long parentId);
        ResponseDTO<AddRefTypeResponseDTO> Create(AddRefTypeRequestDTO DTO);
        Task<IEnumerable<RefTypeDTO>> GetRefTypes();
        Task<RefTypeDTO> GetById(long contextSourceId);
    }
}
