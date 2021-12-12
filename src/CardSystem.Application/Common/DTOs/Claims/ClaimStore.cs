using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace CardSystem.Application.DTOs.USerClaims
{
    public static class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Test","Test")
        };

    }
}
