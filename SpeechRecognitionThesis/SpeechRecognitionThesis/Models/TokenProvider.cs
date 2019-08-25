using Microsoft.IdentityModel.Tokens;
using SpeechRecognitionThesis.Models;
using SpeechRecognitionThesis.Models.Scripts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SpeechRecognitionThesis
{
    public class TokenProvider
    {
        private const string TOKEN_SESSION_KEY                      = "JWToken";
        private const string TOKEN_VALUE_STRING                     = "YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv";

        public static string GetTokenSessionKeyString()
        {
            return TOKEN_SESSION_KEY;
        }

        public string CreateUserTokenString(User user)
        {
            JwtSecurityToken JWToken = new JwtSecurityToken(
                issuer:             UserTools.URL_WEBSITE_STRING,
                audience:           UserTools.URL_WEBSITE_STRING,
                claims:             GetUserClaims(user),
                notBefore:          new DateTimeOffset(DateTime.Now).DateTime,
                expires:            new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,     
                //Using HS256 Algorithm to encrypt Token
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey( TokenProvider.GetEncodedTokenString() ),
                                        SecurityAlgorithms.HmacSha256Signature )
            );

            string token = new JwtSecurityTokenHandler().WriteToken( JWToken );

            return token;
        }

        private IEnumerable<Claim> GetUserClaims( User user )
        {
            IEnumerable<Claim> userClaimsEnumerable = new Claim[]
            {
                new Claim( ClaimTypes.Name,                                   user.NickName ),
                new Claim( UserTools.USER_ID_PROPERTY_STRING,                 user.Id.ToString() ),
                new Claim( UserTools.REGISTER_DATE_PROPERTY_STRING,           user.CreateAccountDate ),
                new Claim( UserTools.USER_LAST_LOGGED_DATE_PROPERTY_STRING,   user.LastLoggedAccountDate ),
                new Claim( UserTools.USER_EMAIL_PROPERTY_STRING,              user.Email ),
            };

            return userClaimsEnumerable;
        }

        public static string GetRegisterUserPropertyString( IIdentity identity, string userPropertyString )
        {
            Claim userClaim = null;
            string userPropertyValueString = string.Empty;

            if( identity.Name != UserTools.ANONYMOUS_USER_NICKNAME 
                && ( userPropertyString.Equals( UserTools.REGISTER_DATE_PROPERTY_STRING )
                    || userPropertyString.Equals( UserTools.USER_LAST_LOGGED_DATE_PROPERTY_STRING )
                    || userPropertyString.Equals( UserTools.USER_ID_PROPERTY_STRING ) ) )
            {
                userClaim = ((ClaimsIdentity)identity).FindFirst( userPropertyString );
                userPropertyValueString = ( userClaim != null ) ? userClaim.Value : string.Empty;
            }

            return userPropertyValueString;
        }

        public static byte[] GetEncodedTokenString()
        {
            return Encoding.ASCII.GetBytes(TOKEN_VALUE_STRING);
        }
    }
}
