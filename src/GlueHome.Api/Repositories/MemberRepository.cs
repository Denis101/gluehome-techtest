using System.Collections.Generic;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Mysql;

namespace GlueHome.Api.Repositories
{
    public class MemberRepository
    {
        private static readonly MemberMapper mapper = new MemberMapper();
        private readonly IMysqlDataClient _mysqlDataClient;

        public Member FindOne(long id)
        {
            return _mysqlDataClient.ExecuteQuery(
                "SELECT * FROM logistics.tb_member WHERE member_id = @id",
                new Dictionary<string, dynamic>() { { "@id", id } },
                mapper);  
        }

        public Member FindByEmail(string email)
        {
            return _mysqlDataClient.ExecuteQuery(
                "SELECT * FROM logistics.tb_member WHERE email = @email",
                new Dictionary<string, dynamic>() { { "@email", email } },
                mapper);
        }
    }
}