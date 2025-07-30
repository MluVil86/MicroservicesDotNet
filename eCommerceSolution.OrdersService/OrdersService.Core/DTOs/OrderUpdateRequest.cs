using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.DTOs;

public record OrderUpdateRequest(
    Guid ProductID,
    decimal UnitPrice,
    int Quantity
    )
{
    public OrderUpdateRequest():this(default, default, default)
    {
        
    }
}
