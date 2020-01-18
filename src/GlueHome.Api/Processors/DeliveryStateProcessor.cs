using System;
using GlueHome.Api.Models;

namespace GlueHome.Api.Processors
{
    public class DeliveryStateProcessor : IDeliveryStateProcessor
    {
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