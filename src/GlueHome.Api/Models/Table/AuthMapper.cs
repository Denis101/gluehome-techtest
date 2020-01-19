using GlueHome.Api.Mysql;
using MySql.Data.MySqlClient;

namespace GlueHome.Api.Models.Table
{
    public class AuthMapper : IDataMapper<Auth>
    {
        public Auth map(MySqlDataReader reader)
        {
            return new Auth()
            {
                MemberId = reader.GetInt64("member_id"),
                Password = reader.GetString("password"),
                LastLogin = reader.GetDateTime("last_login")
            };
        }
    }
}