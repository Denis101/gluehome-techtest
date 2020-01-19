using GlueHome.Api.Models.Rest;

namespace GlueHome.Api.Processors
{
    public interface IDeliveryStateProcessor
    {
        DeliveryState Create(Delivery delivery);
        DeliveryState Approve(Delivery delivery);
        DeliveryState Complete(Delivery delivery);
        DeliveryState Cancel(Delivery delivery);
    }
}