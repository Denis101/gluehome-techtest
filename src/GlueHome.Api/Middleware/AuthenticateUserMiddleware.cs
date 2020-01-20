using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GlueHome.Api.Authentication;
using GlueHome.Api.Middleware;

namespace GlueHome.Api.Middleware
{
    public class AuthenticateUserMiddleware
    {
        private readonly ILogger<AuthenticateUserMiddleware> logger;
        private readonly IAuthenticator authenticator;
        private readonly RequestDelegate next;

        public AuthenticateUserMiddleware(
            ILogger<AuthenticateUserMiddleware> logger, 
            IAuthenticator authenticator, 
            RequestDelegate next)
        {
            this.logger = logger;
            this.authenticator = authenticator;
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
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