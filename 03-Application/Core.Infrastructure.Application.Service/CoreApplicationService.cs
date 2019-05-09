using System;
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
        public ResponseDTO AddRefType(RefTypeDTO request)
        {
            return this.refTypeService.Create(request);
        }
#endregion
    }
}
