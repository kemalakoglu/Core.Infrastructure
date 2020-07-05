using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Infrastructure.Core.Contract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Domain.Aggregate.User
{
    public class SignInManagerService : ISignInManagerService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOfWork uow;

        public SignInManagerService(IUnitOfWork uow,
            SignInManager<ApplicationUser> signInManager)
        {
            this.uow = uow;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Signs the in asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistance">if set to <c>true</c> [is persistance].</param>
        public async Task SignInAsync(ApplicationUser user, bool isPersistance) =>
            await signInManager.SignInAsync(user, isPersistance);

        /// <summary>
        /// Passwords the sign in asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="isPErsistance">if set to <c>true</c> [is p ersistance].</param>
        /// <param name="lockoutOnFailure">if set to <c>true</c> [lockout on failure].</param>
        /// <returns></returns>
        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistance,
            bool lockoutOnFailure) =>
            await this.signInManager.PasswordSignInAsync(email, password, isPersistance, lockoutOnFailure);

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public async Task SignOutAsync() => await this.signInManager.SignOutAsync();

        /// <summary>
        /// Generates the user token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ApplicationUser user, string token)
        {
            List<AuthenticationToken> tokens = new List<AuthenticationToken>();
            tokens.Add(new AuthenticationToken
            {
                Name = user.PasswordHash,
                Value = token
            });
            ExternalLoginInfo loginInfo =
                new ExternalLoginInfo(new ClaimsPrincipal(), user.PasswordHash, user.SecurityStamp, token);
            loginInfo.AuthenticationTokens = tokens;
            return await this.signInManager.UpdateExternalAuthenticationTokensAsync(loginInfo);
        }

        /// <summary>
        /// Refreshes the sign in asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        public async void RefreshSignInAsync(ApplicationUser appUser) =>
            await this.signInManager.RefreshSignInAsync(appUser);
    }
}
