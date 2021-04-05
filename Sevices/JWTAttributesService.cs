using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace OpenReferrals.Sevices
{
    public static class JWTAttributesService
    {
        public static string GetSubject(HttpRequest request)
        {
            var header = request.Headers["Authorization"];
            var jwt = header.ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Subject;
        }

        public static string GetEmail(HttpRequest request)
        {
            var header = request.Headers["Authorization"];
            var jwt = header.ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var email = token.Claims.ToList().Find(x => x.Type == "emails").Value;
            return email;
        }
    }
}
