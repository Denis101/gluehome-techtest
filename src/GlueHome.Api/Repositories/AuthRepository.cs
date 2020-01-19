using System.Collections.Generic;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Mysql;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Repositories
{
    public class AuthRepository : IRepository<Auth>
    {
        public IDataMapper<Auth> Mapper => new AuthMapper();
        private readonly ILogger<AuthRepository> logger;
        private readonly IMysqlContext mysqlContext;

        public AuthRepository(
            ILogger<AuthRepository> logger, 
            IMysqlContext mysqlContext)
        {
            this.logger = logger;
            this.mysqlContext = mysqlContext;
        }

        public Auth FindOne(long id)
        {
            return mysqlContext.ExecuteQuery(
                "SELECT * FROM logistics.tb_auth WHERE member_id = @id",
                new Dictionary<string, dynamic>() { { "@id", id } },
                Mapper);    
        }
    }
}