using System.Threading.Tasks;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Domain.Aggregate.User;
using Core.Infrastructure.Domain.Contract.Service;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Domain.Aggregate.User
{
    public class UserStoreService : IUserStoreService
    {
        private readonly SignInManager<IdentityUser> signInManager;

        //private readonly IUserStoreRepository userStoreRepository;
        private readonly IUnitOfWork uow;
        private readonly UserManager<IdentityUser> userManager;

        public UserStoreService(IUnitOfWork uow, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            //this.userStoreRepository = userStoreRepository;
            this.uow = uow;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public Task<IdentityResult> Register(IdentityUser user)
        {
            uow.Repository<IdentityUser>().UserManager.CreateAsync(user);
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityUser> GetUserByEmail(string requestEmail)
        {
            var response = uow.Repository<IdentityUser>().UserManager.FindByEmailAsync(requestEmail);
            return Task.FromResult(new IdentityUser
            {
                Email = response.Result.Email
            });
        }

        public Task<IdentityUser> FindByLoginAsync(string provider, string key)
        {
            var response = userManager.FindByLoginAsync(provider, key);
            uow.EndTransaction();
            return response;
        }
    }
}