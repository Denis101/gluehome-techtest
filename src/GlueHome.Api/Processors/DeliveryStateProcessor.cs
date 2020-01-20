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

        public DeliveryState Update(DeliveryStateUpdateQuery query)
        {
            if (ShouldExpire(query.Current, query.Window)) 
            {
                return DeliveryState.Expired;
            }

            switch (query.Desired) {
                case DeliveryState.Approved:
                    if (DateTime.UtcNow < query.Window.StartTime && !query.IsPartner) {
                        return DeliveryState.Approved;
                    }
                    
                    return query.Current;
                case DeliveryState.Cancelled:
                    return DeliveryState.Cancelled;
                case DeliveryState.Completed:
                    return query.IsPartner ? DeliveryState.Completed : query.Current;
                default:
                    return DeliveryState.Created;
            }
        }

        public bool ShouldExpire(DeliveryState current, DateTimeWindow window)
        {
            return current != DeliveryState.Completed &&
                DateTime.Now > window.EndTime;
        }
    }
}