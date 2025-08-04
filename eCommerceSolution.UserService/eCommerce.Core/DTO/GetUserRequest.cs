using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public record GetUserRequest(
        Guid UserID, 
        string? Email, 
        string PersonName, 
        GenderOptions Gender        
        )
    {
    }
}
