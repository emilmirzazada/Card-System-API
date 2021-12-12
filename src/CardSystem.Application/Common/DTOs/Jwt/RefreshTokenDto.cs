using System;
using System.Collections.Generic;
using System.Text;

namespace CardSystem.Application.DTOs.Jwt
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }

        public RefreshTokenDto(string token, DateTimeOffset expires)
        {
            Token = token;
            Expires = expires;
        }
    }
}
