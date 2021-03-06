using System.Collections.Generic;
using GlueHome.Api.Models.Rest;

namespace GlueHome.Api.Services
{
    public interface IDeliveryReader
    {
        Delivery Get(long id);
        IEnumerable<Delivery> List();
    }
}