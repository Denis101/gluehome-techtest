using GlueHome.Api.Models;

namespace GlueHome.Api.Services
{
    public interface IDeliveryWriter
    {
        Delivery Create(Delivery delivery);
        Delivery Update(Delivery delivery);
        Delivery Delete(Delivery delivery);
    }
}