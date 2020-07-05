using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Core.Helper;
using Core.Infrastructure.Domain.Aggregate.RefTypeValue;
using Core.Infrastructure.Domain.Aggregate.User;
using Core.Infrastructure.Domain.Contract.DTO.RefType;
using Core.Infrastructure.Domain.Contract.Service;
using Microsoft.AspNetCore.Identity;

namespace Core.Infrastructure.Application.Service
{
    public class CoreApplicationService : ICoreApplicationService
    {
        private readonly IUserManagerService userManagerService;
        private readonly IRefTypeService refTypeService;
        private readonly ISignInManagerService signInManagerService;

        public CoreApplicationService(IUserManagerService userManagerService, IRefTypeService refTypeService, ISignInManagerService signInManagerService)
        {
            this.userManagerService = userManagerService;
            this.refTypeService = refTypeService;
            this.signInManagerService = signInManagerService;
        }

        #region identity services

        /// <summary>
        /// Gets the user by email.
        /// </summary>
        /// <param name="requestEmail">The request email.</param>
        /// <returns></returns>
        public async Task<ApplicationUser> GetUserByEmail(string requestEmail) =>
            await this.userManagerService.GetUserByEmail(requestEmail);

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password) => await this.userManagerService.CreateAsync(user, password);

        /// <summary>
        /// Finds the by login asynchronous.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindByLoginAsync(string provider, string key) =>
            await this.userManagerService.FindByLoginAsync(provider, key);

        /// <summary>
        /// Signs the in asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistance">if set to <c>true</c> [is persistance].</param>
        public async Task SignInAsync(ApplicationUser user, bool isPersistance) => await this.signInManagerService.SignInAsync(user, isPersistance);

        /// <summary>
        /// Passwords the sign in asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="isPersistance">if set to <c>true</c> [is persistance].</param>
        /// <param name="lockoutOnFailure">if set to <c>true</c> [lockout on failure].</param>
        /// <returns></returns>
        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistance,
            bool lockoutOnFailure) => await this.signInManagerService.PasswordSignInAsync(email, password, isPersistance, lockoutOnFailure);

        /// <summary>
        /// Adds the login asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="userLoginInfo">The user login information.</param>
        /// <returns></returns>
        public async Task<IdentityResult> AddLoginAsync(ApplicationUser appUser, UserLoginInfo userLoginInfo) => await this.userManagerService.AddLoginAsync(appUser, userLoginInfo);

        /// <summary>
        /// Signs the out asynchronous.
        /// </summary>
        public async Task SignOutAsync() => await this.signInManagerService.SignOutAsync();

        /// <summary>
        /// Generates the user token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ApplicationUser user,
            string token) => await this.signInManagerService.UpdateExternalAuthenticationTokensAsync(user, token);

        /// <summary>
        /// Removes the login asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="providerKey">The provider key.</param>
        /// <returns></returns>
        public async Task<IdentityResult>
            RemoveLoginAsync(ApplicationUser appUser, string loginProvider, string providerKey) =>
            await this.userManagerService.RemoveLoginAsync(appUser, loginProvider, providerKey);

        /// <summary>
        /// Removes the token asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        /// <param name="loginProvider">The login provider.</param>
        /// <param name="tokenName">Name of the token.</param>
        /// <returns></returns>
        public async Task<IdentityResult> RemoveAuthenticationTokenAsync(ApplicationUser appUser, string loginProvider, string tokenName) =>
            await this.userManagerService.RemoveAuthenticationTokenAsync(appUser, loginProvider, tokenName);

        /// <summary>
        /// Refreshes the sign in asynchronous.
        /// </summary>
        /// <param name="appUser">The application user.</param>
        public async void RefreshSignInAsync(ApplicationUser appUser) =>
            this.signInManagerService.RefreshSignInAsync(appUser);
        #endregion

        #region RefTypeValue Aggregate
        public async Task<ResponseDTO<AddRefTypeResponseDTO>> AddRefType(AddRefTypeRequestDTO request)
        {
            return await this.refTypeService.CreateAsync(request);
        }

        public async Task<ResponseDTO<RefTypeDTO>> DeleteRefType(long id)
        {
            return await this.refTypeService.DeleteAsync(new RefTypeDTO { Id = id });
        }

        public async Task<ResponseListDTO<RefTypeDTO>> GetRefTypes()
        {
            return await this.refTypeService.GetRefTypesAsync();
        }

        public Task<ResponseDTO<RefTypeDTO>> GetRefTypeById(RefTypeDTO contextSource) =>
             this.refTypeService.GetByIdAsync(contextSource.Id);

        public ResponseDTO<RefTypeDTO> UpdateRefType(RefTypeDTO request)
        {
            return this.refTypeService.Update(request);
        }

        public ResponseDTO<RefTypeDTO> DeleteRefType(RefTypeDTO request)
        {
            return this.refTypeService.Delete(request);
        }

        public async Task<ResponseListDTO<RefTypeDTO>> GetRefTypesByParent(long parentId)
        {
            return await this.refTypeService.GetByParentAsync(parentId);
        }

        ResponseListDTO<RefTypeDTO> ICoreApplicationService.GetRefTypesByParent(long parentId)
        {
            throw new System.NotImplementedException();
        }

        ResponseDTO<AddRefTypeResponseDTO> ICoreApplicationService.AddRefType(AddRefTypeRequestDTO request)
        {
            throw new System.NotImplementedException();
        }

        ResponseDTO<RefTypeDTO> ICoreApplicationService.DeleteRefType(long parentId)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<RefTypeDTO>> ICoreApplicationService.GetRefTypes()
        {
            throw new System.NotImplementedException();
        }

        Task<RefTypeDTO> ICoreApplicationService.GetRefTypeById(RefTypeDTO contextSource)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
