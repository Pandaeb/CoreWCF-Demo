using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;

namespace CoreWCF_Demo_Infrastructure.AuthSchemes.UserPasswordScheme;

public class UserPasswordSchemeOptions : AuthenticationSchemeOptions
{
    public const string Name = "UserPasswordScheme";
}

public class UserPasswordSchemeHandler : AuthenticationHandler<UserPasswordSchemeOptions>
{
    private readonly UserPasswordAuthConfiguration _userPasswordAuthConfiguration;

    public UserPasswordSchemeHandler(IOptionsMonitor<UserPasswordSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, UserPasswordAuthConfiguration userPasswordAuthConfiguration)
        : base(options, logger, encoder)
    {
        _userPasswordAuthConfiguration = userPasswordAuthConfiguration;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authTokenFromSoapEnvelope = await Request!.GetAuthenticationHeaderFromSoapEnvelope();

        if (authTokenFromSoapEnvelope != null)
        {
            var identity = new GenericIdentity(authTokenFromSoapEnvelope.Username);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }

        return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Context.Response.StatusCode = 401;
        await Context.Response.WriteAsync("You are not logged in via Basic auth");
    }
}
