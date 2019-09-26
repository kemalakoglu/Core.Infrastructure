using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Core.Helper;

namespace Core.Infrastructure.Domain.Aggregate.RefTypeValue
{
    public class RefTypeService: IRefTypeService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public RefTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ResponseDTO<RefTypeDTO> GetByKey(long key)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<RefTypeDTO> Create(RefTypeDTO DTO)
        {
            throw new NotImplementedException();
        }

        public ResponseDTO<AddRefTypeResponseDTO> Create(AddRefTypeRequestDTO DTO)
        {
            var entity = new RefType(DTO.Status, DTO.InsertDate, DTO.Name, DTO.IsActive);
            if (DTO.Parent.Id > 0)
                entity.SetParent(this.unitOfWork.Repository<RefType>().GetByKey(DTO.Parent.Id));

            unitOfWork.Repository<RefType>().Create(entity);
            unitOfWork.EndTransaction();
            return CreateResponse<AddRefTypeResponseDTO>.Return(Mapper.Map(DTO, new AddRefTypeResponseDTO()), "Create");
        }

        public async Task<IEnumerable<RefTypeDTO>> GetRefTypes()
        {
            var entity = this.unitOfWork.Repository<RefType>().Get();
            return Mapper.Map<RefType[], IEnumerable<RefTypeDTO>>(entity.ToArray()).ToList();
        }

        public async Task<RefTypeDTO> GetById(long contextSourceId)
        {
            var entity = Mapper.Map(this.unitOfWork.Repository<RefType>().Query()
                .Filter(x => x.Id == contextSourceId).Get().FirstOrDefault(), new RefTypeDTO());
            return Mapper.Map(entity, new RefTypeDTO());
        }
        

        public ResponseDTO<RefTypeDTO> Update(RefTypeDTO DTO)
        {
            var entity = unitOfWork.Repository<RefType>().GetByKey(DTO.Id);
            entity.Update(DTO.Status, DTO.Name, DTO.IsActive, DTO.UpdateDate);
            unitOfWork.Repository<RefType>().Update(entity);
            unitOfWork.EndTransaction();
            return CreateResponse<RefTypeDTO>.Return(DTO, "Update");
        }

        public ResponseDTO<RefTypeDTO> Delete(RefTypeDTO DTO)
        {
            var entity = unitOfWork.Repository<RefType>().GetByKey(DTO.Id);
            unitOfWork.Repository<RefType>().Delete(entity);
            unitOfWork.EndTransaction();
            return CreateResponse<RefTypeDTO>.Return(DTO, "Delete");
        }

        public ResponseListDTO<RefTypeDTO> GetByParent(long parentId)
        {
            IEnumerable<RefType> entity;
            if (parentId > 0)
                entity = unitOfWork.Repository<RefType>().Query().Filter(x => x.Parent.Id == parentId).Get();
            else
                entity = unitOfWork.Repository<RefType>().Query().Filter(x => x.Parent.Id == null).Get();

            unitOfWork.EndTransaction();

            //List<RefTypeDTO> response = new List<RefTypeDTO>();
            return CreateResponse<RefTypeDTO>.Return(Mapper.Map<RefType[], IEnumerable<RefTypeDTO>>(entity.ToArray()), "GetByParent");
        }
    }
}
