using System;
using System.Collections.Generic;
using GlueHome.Api.Extensions;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Mysql;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        public IDataMapper<Order> Mapper => new OrderMapper();
        private readonly ILogger<OrderRepository> logger;
        private readonly IMysqlContext mysqlContext;

        public OrderRepository(
            ILogger<OrderRepository> logger,
            IMysqlContext mysqlContext)
        {
            this.logger = logger;
            this.mysqlContext = mysqlContext;
        }

        public IEnumerable<Order> List()
        {
            return mysqlContext.ExecuteQueryBatch("SELECT * FROM logistics.tb_order", Mapper);
        }

        public Order FindOne(long id)
        {
            return FindOne(id, false);   
        }

        private Order FindOne(long id, bool includeDeleted)
        {
            var query = "";
            if (includeDeleted)
            {
                query = "SELECT * FROM logistics.tb_order WHERE order_id = @id";
            }
            else
            {
                query = "SELECT * FROM logistics.tb_order WHERE order_id = @id AND delete_date = NULL";
            }

            return mysqlContext.ExecuteQuery(query, new Dictionary<string, dynamic>() { { "@id", id } }, Mapper);
        }

        public Order Update(Order order)
        {
            var query = 
            @"UPDATE logistics.tb_order SET
                recipient_id = @recipient_id,
                sender = @sender,
                delivery_state = @delivery_state,
                delivery_start_date = @delivery_start_date,
                delivery_end_date = @delivery_end_date,
                modified_date = @modified_date
            WHERE
                order_id = @id
            AND
                delete_date = NULL";

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@id", order.OrderId },
                { "@recipient_id", order.RecipientId },
                { "@sender", order.Sender },
                { "@delivery_state", order.DeliveryState },
                { "@delivery_start_date", order.DeliveryStartDate.ToUnixTimestamp() },
                { "@delivery_end_date", order.DeliveryEndDate.ToUnixTimestamp() },
                { "@modified_date", DateTime.UtcNow }
            };

            mysqlContext.ExecuteQuery(query, parameters, Mapper);
            return FindOne(order.OrderId);
        }

        public Order Delete(long id)
        {
            var query =
            @"UPDATE logistics.tb_order SET
                delete_date = @delete_date
            WHERE
                order_id = @id";

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@id", id },
                { "@delete_date", DateTime.UtcNow },
            };

            mysqlContext.ExecuteQuery(query, parameters, Mapper);
            return FindOne(id, true);
        }

        public Order Insert(Order order)
        {
            var query =
            @"INSERT INTO logistics.tb_order (
                recipient_id,
                sender,
                delivery_state,
                delivery_start_date,
                delivery_end_date,
                create_date
            ) VALUES (
                @recipient_id,
                @sender,
                @delivery_state,
                @delivery_start_date,
                @delivery_end_date,
                @create_date
            )";

            var parameters = new Dictionary<string, dynamic>()
            {
                { "@recipient_id", order.RecipientId },
                { "@sender", order.Sender },
                { "@delivery_state", order.DeliveryState },
                { "@delivery_start_date", order.DeliveryStartDate.ToUnixTimestamp() },
                { "@delivery_end_date", order.DeliveryEndDate.ToUnixTimestamp() },
                { "@create_date", DateTime.UtcNow }
            };

            mysqlContext.ExecuteQuery(query, parameters, Mapper);
            return FindOne(order.OrderId);
        }
    }
}