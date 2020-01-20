using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GlueHome.Api.Authentication;
using GlueHome.Api.Middleware;

namespace GlueHome.Api.Middleware
{
    public class AuthenticateUserMiddleware
    {
        readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly IAuthenticator authenticator;

        public AuthenticateUserMiddleware(
            ILogger logger, 
            IAuthenticator authenticator, 
            RequestDelegate next)
        {
            this.logger = logger;
            this.authenticator = authenticator;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(new PathString("/swagger")))
            {
                await next.Invoke(context);
                return;
            }

            var header = context.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(header)) 
            {
                context.Response.StatusCode = 403;
                return;
            }

            var authHeaderBytes = System.Convert.FromBase64String(header);
            var authHeader = System.Text.Encoding.UTF8.GetString(authHeaderBytes).Split(':');
            var username = authHeader[0];
            var password = authHeader[1];

            if (!authenticator.IsAuthenticated(username, password)) {
                context.Response.StatusCode = 403;
                return;
            }
            
            await next.Invoke(context);
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class AuthenticateUserMiddlewareExtensions
    {
        public static IApplicationBuilder AddUserAuthentication(this IApplicationBuilder builder, ILogger logger, IAuthenticator authenticator)
        {
            return builder.UseMiddleware<AuthenticateUserMiddleware>(logger, authenticator);
        }
    }
}