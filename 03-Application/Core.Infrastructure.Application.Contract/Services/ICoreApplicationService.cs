using Core.Infrastructure.Core.Helper;
using Core.Infrastructure.Domain.Aggregate.User;
using Core.Infrastructure.Domain.Contract.DTO.RefType;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Core.Infrastructure.Application.Contract.Services
{
    public interface ICoreApplicationService
    {
        #region ApplicationUser

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="requestEmail">The request email.</param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserByEmail(string requestEmail);

        /// <summary>
        /// Finds the by login asynchronous.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Task<ApplicationUser> FindByLoginAsync(string provider, string key);

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
        /// Adds the login asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="userLoginInfo">The user login information.</param>
        /// <returns></returns>
        Task<IdentityResult> AddLoginAsync(ApplicationUser appUser, UserLoginInfo userLoginInfo);

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
        Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ApplicationUser user,
            string token);

        /// <summary>
        /// Removes the login asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="providerKey">The provider key.</param>
        /// <returns></returns>
        Task<IdentityResult> RemoveLoginAsync(ApplicationUser appUser, string loginProvider, string providerKey);

        /// <summary>
        /// Removes the token asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns></returns>B
        Task<IdentityResult> RemoveAuthenticationTokenAsync(ApplicationUser appUser, string loginProvider,
            string tokenName);

        /// <summary>
        /// Refreshes the sign in asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        void RefreshSignInAsync(ApplicationUser appUser);

        #endregion
        ResponseDTO<RefTypeDTO> UpdateRefType(RefTypeDTO request);
        ResponseListDTO<RefTypeDTO> GetRefTypesByParent(long parentId);
        ResponseDTO<AddRefTypeResponseDTO> AddRefType(AddRefTypeRequestDTO request);
        ResponseDTO<RefTypeDTO> DeleteRefType(long parentId);
        Task<IEnumerable<RefTypeDTO>> GetRefTypes();
        Task<RefTypeDTO> GetRefTypeById(RefTypeDTO contextSource);
    }
}
