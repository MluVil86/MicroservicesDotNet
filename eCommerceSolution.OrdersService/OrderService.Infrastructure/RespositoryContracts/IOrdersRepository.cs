using MongoDB.Driver;
using OrderService.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccessLayer.RespositoryContracts;

public interface IOrdersRepository
{
    Task<IEnumerable<Order>?> GetOrders();
    Task<IEnumerable<Order>?> GetOrdersByCondition(FilterDefinition<Order> filter);
    Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter);
    Task<Order?> AddOrder(Order order);
    Task<Order?> UpdateOrder(Order order);
    Task<bool> DeleteOrder(Guid orderID);
}
