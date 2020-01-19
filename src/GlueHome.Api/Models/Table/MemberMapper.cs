using GlueHome.Api.Mysql;
using MySql.Data.MySqlClient;

namespace GlueHome.Api.Models.Table
{
    public class MemberMapper : IDataMapper<Member>
    {
        public Member map(MySqlDataReader reader)
        {
            return new Member()
            {
                MemberId = reader.GetInt64("member_id"),
                Forename = reader.GetString("forename"),
                Surname = reader.GetString("surname"),
                Email = reader.GetString("email"),
                PhoneNumber = reader.GetString("phone_number"),
                AddressLine1 = reader.GetString("address_line_1"),
                AddressLine2 = reader.GetString("address_line_2"),
                AddressLine3 = reader.GetString("address_line_3"),
                Postcode = reader.GetString("postcode"),
                IsPartner = reader.GetBoolean("is_partner"),
                CreateDate = reader.GetDateTime("create_date"),
                ModifiedDate = reader.GetDateTime("modified_date"),
                DeleteDate = reader.GetDateTime("delete_date")
            };
        }
    }
}