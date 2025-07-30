using MongoDB.Driver;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.BusinessLogicLayer.ServiceContracts;
using OrderService.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.Services;

public class OrdersService : IOrdersService
{
    public Task<OrderResponse?> AddOrder(OrderAddRequest addRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrder(Guid orderID)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderResponse?>> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderResponse>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponse?> UpdateOrder(OrderUpdateRequest updateRequest)
    {
        throw new NotImplementedException();
    }
}
