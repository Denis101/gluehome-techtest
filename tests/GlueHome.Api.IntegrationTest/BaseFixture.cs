using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GlueHome.Api.IntegrationTest
{
    public abstract class BaseFixture : IClassFixture<DockerWebApplicationFactory>
    {
        
    }
}