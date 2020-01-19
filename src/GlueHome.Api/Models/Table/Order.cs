using System;

namespace GlueHome.Api.Models.Table
{
    public class Order
    {
        public long OrderId { get; set; }
        public long RecipientId { get; set; }
        public string Sender { get; set; }
        public string DeliveryState { get; set; }
        public DateTime DeliveryStartDate { get; set; }
        public DateTime DeliveryEndDate { get; set; }
    }
}