using System.Collections.Generic;
using GlueHome.Api.Models;
using GlueHome.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Controllers
{
    [ApiController]
    [Route("api/delivery")]
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryService _deliveryService;
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(ILogger<DeliveryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Delivery Get([FromRoute] long id)
        {
            return _deliveryService.Get(id);
        }

        [HttpGet]
        public IEnumerable<Delivery> List() 
        {
            return _deliveryService.List();
        }

        [HttpPost]
        public Delivery Create([FromBody] Delivery delivery)
        {
            return _deliveryService.Create(delivery);
        }

        [HttpPut]
        public Delivery Update([FromRoute] long id, [FromBody] Delivery delivery)
        {
            return _deliveryService.Update(delivery);
        }
    }
}
