using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using Polly;
using server.Data;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace server;

public class AuthorizeController : Controller
{
    [Authorize]
    [HttpGet("authorize")]
    public async Task<ActionResult> Authorize([FromServices] IOpenIddictScopeManager scopeManager, [FromServices] UserManager<AppUser> userManager)
    {

        var request = this.HttpContext.GetOpenIddictServerRequest();

        var identifier = request["userId"];

        var users = userManager.Users;

        if (identifier != "test")
        {
            return Challenge(
                authenticationSchemes: new[] { OpenIddictServerAspNetCoreDefaults.AuthenticationScheme },
                properties: new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidRequest,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The specified identity is invalid."
                }));
        }

        // Create the claims-based identity that will be used by OpenIddict to generate tokens.
        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: Claims.Name,
            roleType: Claims.Role);

        // Add the claims that will be persisted in the tokens.
        identity.AddClaim(new Claim(Claims.Subject, identifier.Value.ToString()));
        identity.AddClaim(new Claim(Claims.Name, "test"));

        // Note: in this sample, the client is granted all the requested scopes for the first identity (Alice)
        // but for the second one (Bob), only the "api1" scope can be granted, which will cause requests sent
        // to Zirku.Api2 on behalf of Bob to be automatically rejected by the OpenIddict validation handler,
        // as the access token representing Bob won't contain the "resource_server_2" audience required by Api2.
        identity.SetScopes(request.GetScopes());

        identity.SetResources(await scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());

        // Allow all claims to be added in the access tokens.
        identity.SetDestinations(claim => new[] { Destinations.AccessToken });

        return SignIn(new ClaimsPrincipal(identity), properties: null, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
}
