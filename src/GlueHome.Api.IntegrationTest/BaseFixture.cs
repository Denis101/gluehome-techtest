using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GlueHome.Api.IntegrationTest
{
    public class BaseFixture : IClassFixture<DockerWebApplicationFactory>
    {
        
    }
}