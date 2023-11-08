using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using TodoList.Common.Auth;
using TodoList.Common.Auth.Helpers;
using TodoList.DAL.Context;

namespace TodoList.WebAPI.Auth
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly TodoListDbContext _context;

        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            TodoListDbContext dbContext) : base(options, logger, encoder, clock)
        {
            _context = dbContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(BasicAuthDefaults.AuthorizationHeaderKey))
            {
                return AuthenticateResult.Fail(BasicAuthDefaults.MissingAuthorizationMessage);
            }

            string authorizationHeader = Request.Headers[BasicAuthDefaults.AuthorizationHeaderKey].ToString();
            if (!authorizationHeader.StartsWith(BasicAuthDefaults.BasicAuthPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail(BasicAuthDefaults.InvalidAuthorizationMessage);
            }

            string authBase64Decoded = authorizationHeader[BasicAuthDefaults.BasicAuthPrefix.Length..].Trim();
            byte[] decodedBytes = Convert.FromBase64String(authBase64Decoded);
            string decodedCredentials = Encoding.UTF8.GetString(decodedBytes);

            string[] splittedCreds = decodedCredentials.Split(':', 2);
            if (splittedCreds.Length is not 2)
            {
                return AuthenticateResult.Fail(BasicAuthDefaults.InvalidAuthorizationMessage);
            }

            string clientId = splittedCreds[0];
            string clientSecret = splittedCreds[1];

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == clientId);
            if (user is null)
            {
                return AuthenticateResult.Fail(BasicAuthDefaults.UserWasNotFoundMessage);
            }

            if (!PasswordHasher.VerifyPassword(user.PasswordHash, clientSecret))
            {
                return AuthenticateResult.Fail(BasicAuthDefaults.InvalidPasswordMessage);
            }

            var client = new BasicAuthClient()
            {
                AuthenticationType = BasicAuthDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = clientId,
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
                new Claim(ClaimTypes.Name, clientId)
            }));

            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return AuthenticateResult.Success(authenticationTicket);
        }
    }
}
