using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using TodoList.Common.Auth;
using TodoList.Common.Auth.Abstractions;
using TodoList.Common.Auth.Helpers;
using TodoList.DAL.Context;

namespace TodoList.WebAPI.Auth
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly TodoListDbContext _context;
        private readonly IUserIdSetter _userIdSetter;

        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            TodoListDbContext dbContext,
            IUserIdSetter userIdSetter) : base(options, logger, encoder, clock)
        {
            _context = dbContext;
            _userIdSetter = userIdSetter;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(BasicAuthDefaults.AuthorizationHeaderKey))
            {
                return FailAuthentication(BasicAuthDefaults.MissingAuthorizationMessage);
            }

            string authorizationHeader = Request.Headers[BasicAuthDefaults.AuthorizationHeaderKey].ToString();
            if (!authorizationHeader.StartsWith(BasicAuthDefaults.BasicAuthPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return FailAuthentication(BasicAuthDefaults.InvalidAuthorizationMessage);
            }

            string credentials = ExtractCredentials(authorizationHeader);
            string decodedCredentials = DecodeCredentials(credentials);

            if (!TryParseCredentials(decodedCredentials, out var clientId, out var clientSecret))
            {
                return FailAuthentication(BasicAuthDefaults.InvalidAuthorizationMessage);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == clientId);

            if (user is null || !PasswordHasher.VerifyPassword(user.PasswordHash, clientSecret))
            {
                return FailAuthentication(BasicAuthDefaults.UserWasNotFoundMessage);
            }

            _userIdSetter.SetUserId(user.Id);

            var claimsPrincipal = CreateClaimsPrincipal(clientId);
            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);

            return AuthenticateResult.Success(authenticationTicket);
        }

        private static AuthenticateResult FailAuthentication(string errorMessage)
        {
            return AuthenticateResult.Fail(errorMessage);
        }

        private static string ExtractCredentials(string authorizationHeader)
        {
            return authorizationHeader[BasicAuthDefaults.BasicAuthPrefix.Length..].Trim();
        }

        private static string DecodeCredentials(string credentials)
        {
            byte[] decodedBytes = Convert.FromBase64String(credentials);
            return Encoding.UTF8.GetString(decodedBytes);
        }

        private static bool TryParseCredentials(string credentials, out string clientId, out string clientSecret)
        {
            var splittedCreds = credentials.Split(':', 2);

            if (splittedCreds.Length == 2)
            {
                clientId = splittedCreds[0];
                clientSecret = splittedCreds[1];
                return true;
            }

            clientId = null!;
            clientSecret = null!;
            return false;
        }

        private static ClaimsPrincipal CreateClaimsPrincipal(string clientId)
        {
            var client = new BasicAuthClient
            {
                AuthenticationType = BasicAuthDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = clientId,
            };

            return new ClaimsPrincipal(new ClaimsIdentity(client, new[] { new Claim(ClaimTypes.Name, clientId) }));
        }
    }
}
