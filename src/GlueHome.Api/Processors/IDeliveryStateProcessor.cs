using GlueHome.Api.Models.Rest;

namespace GlueHome.Api.Processors
{
    public interface IDeliveryStateProcessor
    {
        DeliveryState Update(DeliveryStateUpdateQuery query);
    }
}