using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Core.Helper;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefTypeService: IRefTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public RefTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ResponseDTO GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO Create(RefTypeDTO DTO)
        {
            RefType entity = this.mapper.Map<RefType>(DTO);
            this.unitOfWork.Repository<RefType>().Create(entity);
            return CreateResponse<RefTypeDTO>.Return(DTO, "Create");
        }

        public ResponseDTO Update(RefTypeDTO DTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO Delete(RefTypeDTO DTO)
        {
            throw new NotImplementedException();
        }
    }
}
