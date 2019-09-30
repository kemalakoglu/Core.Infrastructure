using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Application.Contract.DTO.RefType;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Core.Infrastructure.Domain.Contract.Service;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Application.Service
{
    public class CoreApplicationService : ICoreApplicationService
    {
        private readonly IUserStoreService userStoreService;
        private readonly IRefTypeService refTypeService;

        public CoreApplicationService(IUserStoreService userStoreService, IRefTypeService refTypeService)
        {
            this.userStoreService = userStoreService;
            this.refTypeService = refTypeService;
        }
        public Task<IdentityUser> GetUserByMail(RegisterDTO request)
        {
            return this.userStoreService.GetUserByEmail(request.Email);
        }

        #region RefTypeValue Aggregate
        public ResponseDTO<AddRefTypeResponseDTO> AddRefType(AddRefTypeRequestDTO request)
        {
            return this.refTypeService.Create(request);
        }

        public ResponseDTO<RefTypeDTO> DeleteRefType(long id)
        {
            return this.refTypeService.Delete(new RefTypeDTO { Id = id });
        }

        public Task<IEnumerable<RefTypeDTO>> GetRefTypes() =>
            this.refTypeService.GetRefTypes();

        public Task<RefTypeDTO> GetRefTypeById(RefTypeDTO contextSource) =>
            this.refTypeService.GetById(contextSource.Id);

        public ResponseDTO<RefTypeDTO> UpdateRefType(RefTypeDTO request)
        {
            return this.refTypeService.Update(request);
        }

        public ResponseDTO<RefTypeDTO> DeleteRefType(RefTypeDTO request)
        {
            return this.refTypeService.Delete(request);
        }

        public ResponseListDTO<RefTypeDTO> GetRefTypesByParent(long parentId)
        {
            return this.refTypeService.GetByParent(parentId);
        }
        #endregion
    }
}
