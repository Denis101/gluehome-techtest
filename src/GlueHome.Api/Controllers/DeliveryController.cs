using System.Collections.Generic;
using GlueHome.Api.Models.Rest;
using GlueHome.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Controllers
{
    [ApiController]
    [Route("api/delivery")]
    public class DeliveryController : ControllerBase
    {
        private readonly ILogger<DeliveryController> logger;
        private readonly IDeliveryReader deliveryReader;
        private readonly IDeliveryWriter deliveryWriter;

        public DeliveryController(
            ILogger<DeliveryController> logger,
            IDeliveryReader deliveryReader, 
            IDeliveryWriter deliveryWriter)
        {
            this.logger = logger;
            this.deliveryReader = deliveryReader;
            this.deliveryWriter = deliveryWriter;
        }

        [HttpGet]
        [Route("{id}")]
        public Delivery Get([FromRoute] long id)
        {
            return deliveryReader.Get(id);
        }

        [HttpGet]
        public IEnumerable<Delivery> List() 
        {
            return deliveryReader.List();
        }

        [HttpPost]
        public Delivery Create([FromBody] Delivery delivery)
        {
            return deliveryWriter.Create(delivery);
        }

        [HttpPut]
        public Delivery Update([FromRoute] long id, [FromBody] Delivery delivery)
        {
            return deliveryWriter.Update(delivery);
        }
    }
}
