using System.Collections.Generic;
using GlueHome.Api.Extensions;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Mysql;

namespace GlueHome.Api.Repositories
{
    public class OrderRepository
    {
        private static readonly OrderMapper mapper = new OrderMapper();
        private readonly IMysqlDataClient _mysqlDataClient;

        public Order FindOne(long id)
        {
            return _mysqlDataClient.ExecuteQuery(
                "SELECT * FROM logistics.tb_order WHERE order_id = @id",
                new Dictionary<string, dynamic>() { { "@id", id } },
                mapper);
        }

        public Order Update(Order order)
        {
            var query = 
            @"UPDATE logistics.tb_order SET
                recipient_id = @recipient_id,
                sender = @sender,
                delivery_state = @delivery_state,
                delivery_start_date = @delivery_start_date,
                delivery_end_date = @delivery_end_date
            WHERE
                order_id = @id";

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@id", order.OrderId },
                { "@recipient_id", order.RecipientId },
                { "@sender", order.Sender },
                { "@delivery_state", order.DeliveryState },
                { "@delivery_start_date", order.DeliveryStartDate.ToUnixTimestamp() },
                { "@delivery_end_date", order.DeliveryEndDate.ToUnixTimestamp() }
            };

            _mysqlDataClient.ExecuteQuery(query, parameters, mapper);
            return FindOne(order.OrderId);
        }

        public Order Insert(Order order)
        {
            var query =
            @"INSERT INTO logistics.tb_order (
                recipient_id,
                sender,
                delivery_state,
                delivery_start_date,
                delivery_end_date
            ) VALUES (
                @recipient_id,
                @sender,
                @delivery_state,
                @delivery_start_date,
                @delivery_end_date
            )";

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@recipient_id", order.RecipientId },
                { "@sender", order.Sender },
                { "@delivery_state", order.DeliveryState },
                { "@delivery_start_date", order.DeliveryStartDate.ToUnixTimestamp() },
                { "@delivery_end_date", order.DeliveryEndDate.ToUnixTimestamp() }
            };

            _mysqlDataClient.ExecuteQuery(query, parameters, mapper);
            return FindOne(order.OrderId);
        }
    }
}