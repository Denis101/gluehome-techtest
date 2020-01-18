using GlueHome.Api.Models;

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