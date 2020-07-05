using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Core.Helper;
using Core.Infrastructure.Domain.Aggregate.Base;
using Core.Infrastructure.Domain.Contract.DTO.RefType;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefTypeService: IRefTypeService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public RefTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public ResponseDTO<RefTypeDTO> GetByKey(long key)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<RefTypeDTO> Create(RefTypeDTO DTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO<AddRefTypeResponseDTO>> CreateAsync(AddRefTypeRequestDTO DTO)
        {
            var entity = new RefType(DateTime.Now, DTO.Name, true);
            if (DTO.Parent.Id > 0)
                entity.SetParent(this.unitOfWork.Repository<RefType>().GetByKey(DTO.Parent.Id));

            unitOfWork.Repository<RefType>().Create(entity);
            unitOfWork.EndTransaction();
            DTO.Id = entity.Id;
            return CreateResponse<AddRefTypeResponseDTO>.Return(mapper.Map(DTO, new AddRefTypeResponseDTO()), "Create");
        }

        public async Task<ResponseListDTO<RefTypeDTO>> GetRefTypesAsync()
        {
            var entity = this.unitOfWork.Repository<RefType>().Get();
            return CreateResponse<RefTypeDTO>.Return(mapper.Map<RefType[], IEnumerable<RefTypeDTO>>(entity.ToArray()), "GetRefTypes");
        }

        public async Task<ResponseDTO<RefTypeDTO>> GetByIdAsync(long contextSourceId)
        {
            var entity = this.unitOfWork.Repository<RefType>().Query()
                .Filter(x => x.Id == contextSourceId).Get().FirstOrDefault();
            return CreateResponse<RefTypeDTO>.Return(mapper.Map(entity, new RefTypeDTO()), "GetById");
        }
        

        public async Task<ResponseDTO<RefTypeDTO>> UpdateAsync(RefTypeDTO DTO)
        {
            var entity = unitOfWork.Repository<RefType>().GetByKey(DTO.Id);
            entity.Update(DTO.Status, DTO.Name, DTO.IsActive, DTO.UpdateDate);
            unitOfWork.Repository<RefType>().Update(entity);
            unitOfWork.EndTransaction();
            return CreateResponse<RefTypeDTO>.Return(DTO, "Update");
        }

        public async Task<ResponseDTO<RefTypeDTO>> DeleteAsync(RefTypeDTO DTO)
        {
            var entity = unitOfWork.Repository<RefType>().GetByKey(DTO.Id);
            unitOfWork.Repository<RefType>().Delete(entity);
            unitOfWork.EndTransaction();
            return CreateResponse<RefTypeDTO>.Return(DTO, "Delete");
        }

        public async Task<ResponseListDTO<RefTypeDTO>> GetByParentAsync(long parentId)
        {
            IEnumerable<RefType> entity;
            if (parentId > 0)
                entity = unitOfWork.Repository<RefType>().Query().Filter(x => x.Parent.Id == parentId).Get();
            else
                entity = unitOfWork.Repository<RefType>().Query().Filter(x => x.Parent.Id == null).Get();

            unitOfWork.EndTransaction();

            //List<RefTypeDTO> response = new List<RefTypeDTO>();
            return CreateResponse<RefTypeDTO>.Return(mapper.Map<RefType[], IEnumerable<RefTypeDTO>>(entity.ToArray()), "GetByParent");
        }

        ResponseDTO<RefTypeDTO> IBaseService<RefTypeDTO>.Update(RefTypeDTO DTO)
        {
            throw new NotImplementedException();
        }

        ResponseDTO<RefTypeDTO> IBaseService<RefTypeDTO>.Delete(RefTypeDTO DTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<RefTypeDTO> SoftDelete(long Id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<RefTypeDTO>> GetByKeyAsync(long key)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<RefTypeDTO>> CreateAsync(RefTypeDTO DTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDTO<RefTypeDTO>> SoftDeleteAsync(long Id)
        {
            throw new NotImplementedException();
        }
    }
}
