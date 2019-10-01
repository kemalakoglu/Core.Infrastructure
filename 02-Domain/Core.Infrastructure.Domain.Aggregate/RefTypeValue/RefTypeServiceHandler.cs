using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Core.Contract;
using MediatR;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefTypeServiceHandler: IRequestHandler<GetRefTypesResponseDTO, GetRefTypesResponseDTO>, IRequestHandler<RefTypeDTO, RefTypeDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        public RefTypeServiceHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<GetRefTypesResponseDTO> Handle(GetRefTypesResponseDTO request, CancellationToken cancellationToken)
        {
            var entity = this.unitOfWork.Repository<RefType>().Get();
            GetRefTypesResponseDTO response=new GetRefTypesResponseDTO();
            var entityList = Mapper.Map<RefType[], IEnumerable<RefTypeDTO>>(entity.ToArray());
            response.RefTypes = entityList;
            return Task.FromResult(response);
        }

        public Task<RefTypeDTO> Handle(RefTypeDTO request, CancellationToken cancellationToken)
        {
            var entity = this.unitOfWork.Repository<RefType>().Query().Filter(x=>x.Id==request.Id).Get().FirstOrDefault();
            return Task.FromResult(Mapper.Map(entity,new RefTypeDTO()));
        }
    }
}
