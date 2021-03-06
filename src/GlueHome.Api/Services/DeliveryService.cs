using System.Collections.Generic;
using GlueHome.Api.Models.Rest;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Repositories;
using GlueHome.Api.Extensions;
using Microsoft.Extensions.Logging;
using System;
using GlueHome.Api.Processors;

namespace GlueHome.Api.Services
{
    public class DeliveryService : IDeliveryReader, IDeliveryWriter
    {
        private readonly ILogger<DeliveryService> logger;
        private readonly MemberRepository memberRepository;
        private readonly OrderRepository orderRepository;
        private readonly IDeliveryStateProcessor deliveryStateProcessor;

        public DeliveryService(
            ILogger<DeliveryService> logger,
            IRepository<Member> memberRepository,
            IRepository<Order> orderRepository,
            IDeliveryStateProcessor deliveryStateProcessor)
        {
            this.logger = logger;
            this.memberRepository = (MemberRepository)memberRepository;
            this.orderRepository = (OrderRepository)orderRepository;
            this.deliveryStateProcessor = deliveryStateProcessor;
        }

        public Delivery Get(long id)
        {
            var order = orderRepository.FindOne(id);
            var member = memberRepository.FindOne(order.RecipientId);
            return new Delivery().MapFromOrder(order).MapFromMember(member);
        }

        public IEnumerable<Delivery> List()
        {
            var results = new List<Delivery>();
            var orders = orderRepository.List();

            foreach (var order in orders)
            {
                var member = memberRepository.FindOne(order.RecipientId);
                results.Add(new Delivery().MapFromOrder(order).MapFromMember(member));
            }

            return results;
        }

        public Delivery Create(Delivery delivery)
        {
            var member = memberRepository.FindByEmail(delivery.Recipient.Email);
            var order = orderRepository.Insert(OrderFromDelivery(delivery, member.MemberId));
            return new Delivery().MapFromOrder(order).MapFromMember(member);
        }

        public Delivery Update(Delivery delivery)
        {
            var currentOrder = orderRepository.FindOne(int.Parse(delivery.Order.OrderNumber));
            var member = memberRepository.FindByEmail(delivery.Recipient.Email);

            delivery.State = deliveryStateProcessor.Update(new DeliveryStateUpdateQuery()
            {
                Desired = delivery.State,
                Current = (DeliveryState)Enum.Parse(typeof(DeliveryState), currentOrder.DeliveryState),
                Window = delivery.AccessWindow,
                IsPartner = member.IsPartner,
            });

            return new Delivery()
                .MapFromOrder(orderRepository.Update(OrderFromDelivery(delivery, member.MemberId)))
                .MapFromMember(member);
        }

        public Delivery Delete(Delivery delivery)
        {
            orderRepository.Delete(int.Parse(delivery.Order.OrderNumber));
            return delivery;
        }

        private static Order OrderFromDelivery(Delivery delivery, long memberId)
        {
            return new Order()
            {
                RecipientId = memberId,
                Sender = delivery.Order.Sender,
                DeliveryState = DeliveryState.Created.ToString(),
                DeliveryStartDate = delivery.AccessWindow.StartTime,
                DeliveryEndDate = delivery.AccessWindow.EndTime
            };
        }
    }
}