using System;
using GlueHome.Api.Models.Rest;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Processors
{
    public class DeliveryStateProcessor : IDeliveryStateProcessor
    {
        private readonly ILogger<DeliveryStateProcessor> logger;

        public DeliveryStateProcessor(ILogger<DeliveryStateProcessor> logger)
        {
            this.logger = logger;
        }

        public DeliveryState Approve(Delivery delivery)
        {
            if (DateTime.Now < delivery.AccessWindow.StartTime) {
                return DeliveryState.Approved;
            }

            return DeliveryState.Created;
        }

        public DeliveryState Cancel(Delivery delivery)
        {
            return DeliveryState.Cancelled;
        }

        public DeliveryState Complete(Delivery delivery)
        {
            throw new System.NotImplementedException();
        }

        public DeliveryState Create(Delivery delivery)
        {
            throw new System.NotImplementedException();
        }

        public bool ShouldExpire(Delivery delivery)
        {
            return delivery.State != DeliveryState.Completed &&
                DateTime.Now > delivery.AccessWindow.EndTime;
        }
    }
}