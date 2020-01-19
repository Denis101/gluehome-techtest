using System.Collections.Generic;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Mysql;

namespace GlueHome.Api.Repositories
{
    public class AuthRepository
    {
        private static readonly AuthMapper mapper = new AuthMapper();
        private readonly IMysqlDataClient _mysqlDataClient;

        public Auth FindOne(long id)
        {
            return _mysqlDataClient.ExecuteQuery(
                "SELECT * FROM logistics.tb_member WHERE member_id = @id",
                new Dictionary<string, dynamic>() { { "@id", id } },
                mapper);    
        }
    }
}