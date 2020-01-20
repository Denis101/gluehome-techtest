using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GlueHome.Api.IntegrationTest
{
    public class DockerWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) 
        {
            builder.ConfigureServices(services => {
                // TODO, connect to local mysql, populate with data.
            });
        }
    }
}