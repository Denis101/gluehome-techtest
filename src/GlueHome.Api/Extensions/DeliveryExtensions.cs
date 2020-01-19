using System;
using GlueHome.Api.Models.Rest;
using GlueHome.Api.Models.Table;

namespace GlueHome.Api.Extensions
{
    public static class DeliveryExtensions
    {
        public static Delivery MapFromOrder(this Delivery delivery, Order order)
        {
            delivery.State = (DeliveryState)Enum.Parse(typeof(DeliveryState), order.DeliveryState);

            delivery.Order = new OrderReference()
            {
                OrderNumber = order.OrderId.ToString(),
                Sender = order.Sender
            };

            delivery.AccessWindow = new DateTimeWindow()
            {
                StartTime = order.DeliveryStartDate,
                EndTime = order.DeliveryEndDate
            };
            
            return delivery;
        }

        public static Delivery MapFromMember(this Delivery delivery, Member member)
        {
            delivery.Recipient = new User()
            {
                Name = string.Format("{0} {1}", member.Forename, member.Surname),
                Address = string.Format("{0}, {1}, {2}, {3}", 
                    member.AddressLine1, 
                    member.AddressLine2, 
                    member.AddressLine3, 
                    member.Postcode),
                Email = member.Email,
                PhoneNumber = member.PhoneNumber
            };

            return delivery;
        }
    }
}