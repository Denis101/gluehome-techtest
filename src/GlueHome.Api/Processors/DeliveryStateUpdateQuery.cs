using GlueHome.Api.Models.Rest;

namespace GlueHome.Api.Processors
{
    public class DeliveryStateUpdateQuery
    {
        public DeliveryState Current { get; set; }
        public DeliveryState Desired { get; set; }
        public DateTimeWindow Window { get; set; }
        public bool IsPartner { get; set; }
    }
}