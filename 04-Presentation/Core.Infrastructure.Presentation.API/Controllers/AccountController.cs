using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Domain.Aggregate.User;
using Core.Infrastructure.Domain.Contract.DTO.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Infrastructure.Presentation.API.Controllers
{
        //[Produces("application/json")]
        public class AccountController : Controller
        {
            private readonly IConfiguration configuration;
            private readonly ICoreApplicationService appService;

            public AccountController(
                IConfiguration configuration,
                ICoreApplicationService appService)
            {
                this.configuration = configuration;
                this.appService = appService;
            }

            /// <summary>
            ///     Account Login to take JWT Token
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            [Route("api/Account/Login")]
            [HttpPost]
            public async Task<LoginResponseDTO> Login([FromBody] LoginDTO model)
            {
                string hash = "956E90B4-BBB5-4F85-9101-583C2B3B3D89";
                string password = Convert.ToBase64String(GenerateSaltedHash(Encoding.UTF8.GetBytes(model.Password), Encoding.UTF8.GetBytes(hash)));
                var appUser = await this.appService.GetUserByEmail(model.Email);

                var signInResult = await this.appService.PasswordSignInAsync(appUser.Email, password, model.IsPersistance, false);
                if (signInResult.Succeeded)
                {
                    var checkToken = await this.appService.FindByLoginAsync(appUser.PasswordHash, appUser.SecurityStamp);
                    if (checkToken != null)
                    {
                        var removeTokenResult = await
                            this.appService.RemoveAuthenticationTokenAsync(appUser, appUser.PasswordHash, appUser.PasswordHash);
                        var removeLoginresult = await this.appService.RemoveLoginAsync(appUser, appUser.PasswordHash, appUser.SecurityStamp);
                        if (removeLoginresult.Succeeded && removeTokenResult.Succeeded)
                            await this.appService.SignOutAsync();
                    }

                    var token = await GenerateJwtToken(model.Email, appUser);
                    UserLoginInfo userLoginInfo = new UserLoginInfo(appUser.PasswordHash, appUser.SecurityStamp, token.ToString());
                    var result = await this.appService.AddLoginAsync(appUser, userLoginInfo);
                    var tokenResult = await this.appService.UpdateExternalAuthenticationTokensAsync(appUser, token.ToString());
                    if (result.Succeeded && tokenResult.Succeeded) return await Task.FromResult(new LoginResponseDTO { Token = token.ToString(), Username = appUser.UserName });
                }

                throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
            }

            /// <summary>
            ///     Get User By Mail
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            [Route("api/Account/GetUserByEmail")]
            [HttpPost]
            public async Task<IdentityUser> GetUserByEmail([FromBody] RegisterDTO request)
            {
                return await this.appService.GetUserByEmail(request.Email);
            }

            /// <summary>
            ///     User Registration
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            [Route("api/Account/Register")]
            [HttpPost]
            public async Task<RegisterDTO> Register([FromBody] RegisterDTO model)
            {
                string hash = "956E90B4-BBB5-4F85-9101-583C2B3B3D89";
                string password = Convert.ToBase64String(GenerateSaltedHash(Encoding.UTF8.GetBytes(model.Password), Encoding.UTF8.GetBytes(hash)));
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await this.appService.CreateAsync(user, password);
                if (result.Succeeded) await this.appService.SignInAsync(user, false);

                if (result.Succeeded)
                    return await Task.FromResult(model);
                throw new ApplicationException("UNKNOWN_ERROR");
            }

            /// <summary>
            /// Logouts the specified model.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <returns></returns>
            /// <exception cref="ApplicationException">UNKNOWN_ERROR</exception>
            [Route("api/Account/Logout")]
            [HttpPost]
            public async Task<LoginResponseDTO> Logout([FromBody] LogoutDTO model)
            {
                var appUser = await this.appService.GetUserByEmail(model.Email);
                var removeTokenResult = await
                    this.appService.RemoveAuthenticationTokenAsync(appUser, appUser.PasswordHash, appUser.PasswordHash);
                var result = await this.appService.RemoveLoginAsync(appUser, appUser.PasswordHash, appUser.SecurityStamp);
                if (result.Succeeded && removeTokenResult.Succeeded)
                    await this.appService.SignOutAsync();

                if (result.Succeeded)
                    return await Task.FromResult(new LoginResponseDTO { Token = string.Empty });
                throw new ApplicationException("UNKNOWN_ERROR");

            }

            /// <summary>
            /// Generates the JWT token.
            /// </summary>
            /// <param name="email">The email.</param>
            /// <param name="user">The user.</param>
            /// <returns></returns>
            private async Task<object> GenerateJwtToken(string email, IdentityUser user)
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

                var token = new JwtSecurityToken(
                    configuration["JwtIssuer"],
                    configuration["JwtIssuer"],
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            /// <summary>
            /// Generates the salted hash.
            /// </summary>
            /// <param name="plainText">The plain text.</param>
            /// <param name="salt">The salt.</param>
            /// <returns></returns>
            private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
            {
                HashAlgorithm algorithm = new SHA256Managed();

                byte[] plainTextWithSaltBytes =
                    new byte[plainText.Length + salt.Length];

                for (int i = 0; i < plainText.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainText[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainText.Length + i] = salt[i];
                }

                return algorithm.ComputeHash(plainTextWithSaltBytes);
            }
        }
}