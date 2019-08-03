using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SpeechRecognitionThesis.Models.Repository
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IRespositoryWrapper _repositoryWrapper;
        private const string AUTHORIZATION_KEY_STRING = "Authorization";
        private const string MISSING_AUTHORIZATION_HEADER_STRING = "Missing Authorization Header";
        private const char HEADER_VALUES_DELIMITER_CHAR = ':';

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> optionsMonitor,
            ILoggerFactory loggerFactory,
            UrlEncoder urlEncoder,
            ISystemClock systemClock,
            IRespositoryWrapper repositoryWrapper)
            : base( optionsMonitor, loggerFactory, urlEncoder, systemClock )
        {
            _repositoryWrapper = repositoryWrapper;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            User user = null;

            if ( !Request.Headers.ContainsKey( AUTHORIZATION_KEY_STRING ) )
            {
                return AuthenticateResult.Fail(MISSING_AUTHORIZATION_HEADER_STRING);
            }

            try
            {
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse( Request.Headers[ AUTHORIZATION_KEY_STRING ] );
                byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split( HEADER_VALUES_DELIMITER_CHAR );


                if( credentials.Length >= 2 )
                {
                    string usernameString = credentials[0];
                    string passwordString = credentials[1];
                    user = _repositoryWrapper.Account.Authenticate(usernameString, passwordString);
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if( user == null )
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim( ClaimTypes.NameIdentifier, user.UserId.ToString() ),
                new Claim( ClaimTypes.Name, user.NickName ),
            };

            var identity = new ClaimsIdentity( claims, Scheme.Name );
            var principal = new ClaimsPrincipal( identity );
            var ticket = new AuthenticationTicket( principal, Scheme.Name );

            return AuthenticateResult.Success( ticket );
        }
    }
}
