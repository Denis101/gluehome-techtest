using System.Collections.Generic;
using GlueHome.Api.Models.Rest;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Services
{
    public class DeliveryService : IDeliveryReader, IDeliveryWriter
    {
        private readonly ILogger<DeliveryService> logger;

        public DeliveryService(ILogger<DeliveryService> logger) 
        {
            this.logger = logger;
        }

        public Delivery Create(Delivery delivery)
        {
            throw new System.NotImplementedException();
        }

        public Delivery Delete(Delivery delivery)
        {
            throw new System.NotImplementedException();
        }

        public Delivery Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Delivery> List()
        {
            throw new System.NotImplementedException();
        }

        public Delivery Update(Delivery delivery)
        {
            throw new System.NotImplementedException();
        }
    }
}