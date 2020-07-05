using System.Threading.Tasks;
using Core.Infrastructure.Domain.Aggregate.User;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Domain.Contract.Service
{
    public interface IUserManagerService
    {
        Task<IdentityResult> CreateAsync(ApplicationUser appUser, string password);
        Task<ApplicationUser> GetUserByEmail(string requestEmail);
        Task<ApplicationUser> FindByLoginAsync(string provider, string key);
        Task<IdentityResult> AddLoginAsync(ApplicationUser appUser, UserLoginInfo userLoginInfo);
        Task<IdentityResult> RemoveLoginAsync(ApplicationUser appUser, string loginProvider, string providerKey);
        Task<IdentityResult> RemoveAuthenticationTokenAsync(ApplicationUser appUser, string loginProvider, string tokenName);
    }
}