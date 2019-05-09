using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure.Application.Contract.DTO;
using Core.Infrastructure.Application.Contract.Services;
using Core.Infrastructure.Domain.Contract.Service;
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
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICoreApplicationService appService;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IUserStoreService userStoreService,
            ICoreApplicationService appService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
        public async Task<LoginDTO> Login([FromBody] LoginDTO model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                var token = await GenerateJwtToken(model.Email, appUser);

                await userManager.AddLoginAsync(appUser,
                    new UserLoginInfo("JWT Token", token.ToString(),
                        appUser.UserName == string.Empty ? string.Empty : appUser.UserName));

                return await Task.FromResult(model);
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
            return await this.appService.GetUserByMail(request);
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
            var user = new IdentityUser {UserName = model.Email, Email = model.Email};
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) await signInManager.SignInAsync(user, false);

            if (result.Succeeded)
                return await Task.FromResult(model);
            throw new ApplicationException("UNKNOWN_ERROR");
        }

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
    }
}