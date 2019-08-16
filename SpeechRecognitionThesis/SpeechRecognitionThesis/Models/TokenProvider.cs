using Microsoft.IdentityModel.Tokens;
using SpeechRecognitionThesis.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpeechRecognitionThesis
{
    public class TokenProvider
    {
        private const string TOKEN_SESSION_KEY = "JWToken";
        private const string TOKEN_VALUE_STRING = "YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv";

        public static string GetTokenSessionKeyString()
        {
            return TOKEN_SESSION_KEY;
        }

        public string CreateUserTokenString(User user)
        {
            JwtSecurityToken JWToken = new JwtSecurityToken(
                issuer: "http://localhost:8080/",
                audience: "http://localhost:8080/",
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,     
                //Using HS256 Algorithm to encrypt Token
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey( TokenProvider.GetEncodedTokenString() ),
                                        SecurityAlgorithms.HmacSha256Signature )
            );

            string token = new JwtSecurityTokenHandler().WriteToken( JWToken );

            return token;
        }

        private IEnumerable<Claim> GetUserClaims( User user )
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim( ClaimTypes.Name, user.NickName ),
                new Claim( "UserId", user.Id.ToString() ),
                new Claim( "Email", user.Email ),
            };

            return claims;
        }

        public static byte[] GetEncodedTokenString()
        {
            return Encoding.ASCII.GetBytes(TOKEN_VALUE_STRING);
        }
    }
}
