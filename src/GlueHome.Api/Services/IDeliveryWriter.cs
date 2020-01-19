using GlueHome.Api.Models.Rest;

namespace GlueHome.Api.Services
{
    public interface IDeliveryWriter
    {
        Delivery Create(Delivery delivery);
        Delivery Update(Delivery delivery);
        Delivery Delete(Delivery delivery);
    }
}