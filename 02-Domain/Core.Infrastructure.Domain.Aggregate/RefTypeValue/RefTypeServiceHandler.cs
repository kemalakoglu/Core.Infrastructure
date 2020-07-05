using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Domain.Contract.DTO.RefType;
using MediatR;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefTypeServiceHandler: IRequestHandler<GetRefTypesResponseDTO, GetRefTypesResponseDTO>, IRequestHandler<RefTypeDTO, RefTypeDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RefTypeServiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public Task<GetRefTypesResponseDTO> Handle(GetRefTypesResponseDTO request, CancellationToken cancellationToken)
        {
            var entity = this.unitOfWork.Repository<RefType>().Get();
            GetRefTypesResponseDTO response=new GetRefTypesResponseDTO();
            var entityList = mapper.Map<RefType[], IEnumerable<RefTypeDTO>>(entity.ToArray());
            response.RefTypes = entityList;
            return Task.FromResult(response);
        }
        
        public Task<RefTypeDTO> Handle(RefTypeDTO request, CancellationToken cancellationToken)
        {
            var entity = this.unitOfWork.Repository<RefType>().Query().Filter(x=>x.Id==request.Id).Get().FirstOrDefault();
            return Task.FromResult(mapper.Map(entity,new RefTypeDTO()));
        }
    }
}
