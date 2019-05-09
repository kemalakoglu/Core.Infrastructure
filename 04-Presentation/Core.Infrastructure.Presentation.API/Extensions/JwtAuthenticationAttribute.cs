using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Infrastructure.Presentation.API.Extensions
{
    public class JwtAuthenticationAttribute : Attribute, IFilterFactory
    {
        // Implement IFilterFactory
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new InternalAddHeaderFilter();
        }

        public bool IsReusable { get; }

        public static bool ValidateToken(string token)
        {
            return CheckToken(token) != string.Empty;
        }

        private static string CheckToken(string token)
        {
            var username = string.Empty;


            GetTokenInformation(token, out username);
            return username;
        }

        private static void GetTokenInformation(string token, out string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            username = jwtToken.Subject;
        }

        private class InternalAddHeaderFilter : IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {
                var controller = (Controller) context.Controller;
                var key = controller.Request.Headers.ToArray().FirstOrDefault(x => x.Key == "Authorization").Value
                    .ToString().Replace("Bearer ", "");

                if (!ValidateToken(key)) throw new Exception("JWT Token is invalid. Token is: " + key);
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                //Log.Write(LogEventLevel.Information, "Jwt token is succeed.");
            }
        }
    }
}