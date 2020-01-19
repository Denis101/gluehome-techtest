using System.Collections.Generic;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Mysql;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Repositories
{
    public class MemberRepository : IRepository<Member>
    {
        public IDataMapper<Member> Mapper => new MemberMapper();
        private readonly ILogger<MemberRepository> logger;
        private readonly IMysqlContext mysqlContext;

        public MemberRepository(
            ILogger<MemberRepository> logger,
            IMysqlContext mysqlContext)
        {
            this.logger = logger;
            this.mysqlContext = mysqlContext;
        }

        public Member FindOne(long id)
        {
            return mysqlContext.ExecuteQuery(
                "SELECT * FROM logistics.tb_member WHERE member_id = @id",
                new Dictionary<string, dynamic>() { { "@id", id } },
                Mapper);  
        }

        public Member FindByEmail(string email)
        {
            return mysqlContext.ExecuteQuery(
                "SELECT * FROM logistics.tb_member WHERE email = @email",
                new Dictionary<string, dynamic>() { { "@email", email } },
                Mapper);
        }
    }
}