using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace GlueHome.Api
{
    public class EnvironmentSettings
    {
        public EnvironmentSettings(IConfiguration configuration)
        {
            this.MysqlConnectionString = configuration["MYSQL_CONNECTION_STRING"];   
        }

        public string MysqlConnectionString{ get; private set; }
        
        public IEnumerable<string> MissingSettings()
        {
            if (string.IsNullOrWhiteSpace(MysqlConnectionString)) {
                yield return "MYSQL_CONNECTION_STRING";
            }
        }
    }
}