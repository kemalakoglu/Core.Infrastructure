using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Domain.Contract.Service
{
    public interface IUserStoreService
    {
        Task<IdentityResult> Register(IdentityUser user);
        Task<IdentityUser> GetUserByEmail(string requestEmail);
        Task<IdentityUser> FindByLoginAsync(string provider, string key);
    }
}