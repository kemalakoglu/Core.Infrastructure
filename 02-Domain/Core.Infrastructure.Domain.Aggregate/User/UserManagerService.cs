using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Core.Contract;
using Core.Infrastructure.Domain.Aggregate.User;
using Core.Infrastructure.Domain.Contract.Service;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Domain.Aggregate.User
{
    public class UserStoreService : IUserManagerService
    {
        private readonly IUnitOfWork uow;
        private readonly UserManager<ApplicationUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserStoreService"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public UserStoreService(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            this.uow = uow;
            this.userManager = userManager;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(ApplicationUser appUser, string password) => await this.userManager.CreateAsync(appUser, password);

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="requestEmail">The request email.</param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetUserByEmail(string requestEmail) => await this.userManager.FindByEmailAsync(requestEmail);
        
        /// <summary>
        /// Finds the by login asynchronous.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindByLoginAsync(string provider, string key) => await this.userManager.FindByLoginAsync(provider, key);

        /// <summary>
        /// Adds the login asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="userLoginInfo">The user login information.</param>
        /// <returns></returns>
        public async Task<IdentityResult> AddLoginAsync(ApplicationUser appUser, UserLoginInfo userLoginInfo) => await this.userManager.AddLoginAsync(appUser, userLoginInfo);

        /// <summary>
        /// Removes the login asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="providerKey">The provider key.</param>
        /// <returns></returns>
        public async Task<IdentityResult> RemoveLoginAsync(ApplicationUser appUser, string loginProvider,
            string providerKey) => await this.userManager.RemoveLoginAsync(appUser, loginProvider, providerKey);

        /// <summary>
        /// Removes the token asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns></returns>
        public async Task<IdentityResult> RemoveAuthenticationTokenAsync(ApplicationUser appUser, string loginProvider,
            string tokenName) => await this.userManager.RemoveAuthenticationTokenAsync(appUser, loginProvider, tokenName);
    }
}