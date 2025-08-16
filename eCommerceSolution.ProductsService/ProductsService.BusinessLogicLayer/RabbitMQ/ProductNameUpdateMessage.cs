using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsService.BusinessLogicLayer.RabbitMQ
{
    public record ProductNameUpdateMessage(
        Guid ProductID, string? ProductName)
    {
        public ProductNameUpdateMessage(): this(default, default)
        {
            
        }
    }
}
