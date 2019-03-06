using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PowerApps
{
    public static class Extensions
    {
        public static AuthenticationBuilder AddApiKeyAuthentication(this AuthenticationBuilder builder, Action<ApiKeyAuthOptions> configureOptions)
        {
            return builder.AddScheme<ApiKeyAuthOptions, ApiKeyAuthHandler>("ApiKey", null, configureOptions);
        }
    }

    public class ApiKeyAuthAttribute : AuthorizeAttribute
    {

        public ApiKeyAuthAttribute()
        {
            AuthenticationSchemes = "ApiKey";
        }
    }

    public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthOptions>
    {
        public ApiKeyAuthHandler(IOptionsMonitor<ApiKeyAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        // todo: add caching?
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var token = GetTokenFromAuthHeader();

                    if (token == Options.Key)
                    {
                        var cid = new ClaimsIdentity("ApiKey");
                        cid.AddClaim(new Claim("ApiKey", token));
                        var ticket = new AuthenticationTicket(new ClaimsPrincipal(cid), "ApiKey");
                        return AuthenticateResult.Success(ticket);
                    }
                    else
                    {
                        return AuthenticateResult.Fail("Authentication failed.");
                    }
                });
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private string GetTokenFromAuthHeader()
        {

            if (!Request.Headers.TryGetValue("Authorization", out var header))
                return null;

            return header;
        }
    }

    public class ApiKeyAuthOptions : AuthenticationSchemeOptions
    {
        public string Key { get; set; }

        public ApiKeyAuthOptions AddKey(string key)
        {
            Key = key;
            return this;
        }
    }
}
