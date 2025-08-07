﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.DTOs;

public record ProductResponse(
    Guid ProductID,
    string ProductName,
    string Category,
    double UnitPrice,
    int QuantityInStock)
{
}
