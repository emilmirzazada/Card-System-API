using CardSystem.Application.DTOs.Jwt;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CardSystem.Application.DTOs.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public JwtTokenDto Jwt { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}
