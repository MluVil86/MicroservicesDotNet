using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.DataAccessLayer.Entities;

namespace OrderService.BusinessLogicLayer.DTOs;

public record OrderAddRequest(
    Guid UserID,
    DateTime OrderDate,
    List<OrderItemAddRequest> OrderItems     
    )
{
    public OrderAddRequest(): this(default, default, default)
    {
        
    }
}
