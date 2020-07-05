using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Domain.Aggregate.User
{
    public interface ISignInManagerService
    {
        /// <summary>
        /// Signs the in asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistance">if set to <c>true</c> [is persistance].</param>
        /// <returns></returns>
        Task SignInAsync(ApplicationUser user, bool isPersistance);

        /// <summary>
        /// Passwords the sign in asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="isPersistance">if set to <c>true</c> [is persistance].</param>
        /// <param name="lockoutOnFailure">if set to <c>true</c> [lockout on failure].</param>
        /// <returns></returns>
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistance,
            bool lockoutOnFailure);

        /// <summary>
        /// Signs the out asynchronous.
        /// </summary>
        /// <returns></returns>
        Task SignOutAsync();

        /// <summary>
        /// Generates the user token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ApplicationUser user, string token);

        /// <summary>
        /// Refreshes the sign in asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        void RefreshSignInAsync(ApplicationUser appUser);
    }
}
