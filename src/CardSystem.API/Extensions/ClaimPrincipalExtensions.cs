using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CardSystem.API.Extensions
{
    public static class ClaimPrincipialExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var idClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "Id");
            if (idClaim != null)
            {
                return idClaim.Value;
            }

            return string.Empty;
        }
    }
}
