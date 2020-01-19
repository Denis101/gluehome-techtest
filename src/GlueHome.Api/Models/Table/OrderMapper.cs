using GlueHome.Api.Mysql;
using MySql.Data.MySqlClient;

namespace GlueHome.Api.Models.Table
{
    public class OrderMapper : IDataMapper<Order>
    {
        public Order map(MySqlDataReader reader)
        {
            return new Order()
            {
                OrderId = reader.GetInt64("order_id"),
                RecipientId = reader.GetInt64("recipient_id"),
                Sender = reader.GetString("sender"),
                DeliveryState = reader.GetString("delivery_state"),
                DeliveryStartDate = reader.GetDateTime("delivery_start_date"),
                DeliveryEndDate = reader.GetDateTime("delivery_end_date")
            };
        }
    }
}