using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.DTOs;

public record UserResponse(
    Guid UserID,
    string? Email,
    string PersonName,
    string Gender        
    )
{
}
