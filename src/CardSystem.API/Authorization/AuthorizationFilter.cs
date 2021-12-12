using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardSystem.API.Authorization
{
    public class AuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService authService;

        public AuthorizeFilter(IAuthorizationService authorizationService)
        {
            authService = authorizationService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var authorizationPolicyBuilder = new AuthorizationPolicyBuilder()
                                                    .RequireAuthenticatedUser();
            var name = context.ActionDescriptor.RouteValues.First().Value;
            var res = await authService.AuthorizeAsync(context.HttpContext.User, authorizationPolicyBuilder.Build());
            if (!res.Succeeded && !(name == "Authenticate" ||
                name == "Logout" ||
                name == "ResetPassword" ||
                name == "Revoke" ||
                name == "ResetPassword" ||
                name == "SendToken"))
            {
                context.Result = new UnauthorizedResult();
            }

        }
    }
}
