namespace GlueHome.Api.Models.Rest
{
    public class Delivery
    {
        public DeliveryState State { get; set; }
        public DateTimeWindow AccessWindow { get; set; }
        public User Recipient { get; set; }
        public OrderReference Order { get; set; }
    }
}