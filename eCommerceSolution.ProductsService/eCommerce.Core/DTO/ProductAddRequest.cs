using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO;

public class ProductAddRequest(
    string? ProductName,
    string? Category,
    double? UnitPrice,
    int? Quantity
    );

