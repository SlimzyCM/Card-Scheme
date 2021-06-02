using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CardScheme.Commons;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CardScheme.Api.Middleware
{
    /// <summary>
    /// Custom auth and authentication handler
    /// </summary>
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions { }

    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {

        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// Required class to implement AuthenticationHandler
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Check that all header payload keys are contained
            if (!Request.Headers.ContainsKey("Authorization")
                || !Request.Headers.ContainsKey("timeStamp")
                || !Request.Headers.ContainsKey("appKey")
            )
            {
                // adds error message to the the context
                if (!Context.Response.HasStarted)
                {
                    Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    var result = JsonConvert.SerializeObject(new { error = "Invalid message request" });
                    Context.Response.ContentType = "application/json";
                    await Context.Response.WriteAsync(result);
                }
                else
                {
                    await Context.Response.WriteAsync(string.Empty);
                }

                return AuthenticateResult.Fail("Invalid message request");

            }
            
            string authorizationHeader = Request.Headers["Authorization"];

            string timeStamp = Request.Headers["timeStamp"];

            string appKey = Request.Headers["appKey"];

            //Check if the headers Keys are not empty
            if (string.IsNullOrEmpty(authorizationHeader)
                || string.IsNullOrEmpty(timeStamp)
                || string.IsNullOrEmpty(appKey)
            )
            {
                return AuthenticateResult.NoResult();
            }

            //checks if the "3line" is contained in the auth hashed key
            if (!authorizationHeader.StartsWith("3line", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Invalid authorization key");
            }

            var token = authorizationHeader.Substring("3line".Length).Trim();

            //check if the auth key is empty after removing "3line"
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                //call the method to compare the header values and return a ticket 
                return ValidateToken(appKey, timeStamp, token);
            }
            catch (Exception ex)
            {
                if (!Context.Response.HasStarted)
                {
                    Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    var result = JsonConvert.SerializeObject(new { error = "invalid authorization key" });
                    Context.Response.ContentType = "application/json";
                    await Context.Response.WriteAsync(result);
                }
                else
                {
                    await Context.Response.WriteAsync(string.Empty);
                }
                return AuthenticateResult.Fail(ex.Message);
            }
            
        }

        /// <summary>
        /// Verify the Key matches and creates the claim
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="timeStamp"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private AuthenticateResult ValidateToken(string appKey, string timeStamp, string token)
        {
            //call the static method
            Utilities.CompareHash(appKey, timeStamp, token);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, token),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}

