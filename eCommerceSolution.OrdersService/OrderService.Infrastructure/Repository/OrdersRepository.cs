﻿using MongoDB.Driver;
using OrderService.DataAccessLayer.Entities;
using OrderService.DataAccessLayer.RespositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccessLayer.Repository;

public class OrdersRepository : IOrdersRepository
{
    private readonly IMongoCollection<Order> _orders;
    private readonly string collectionName = "orders";
    public OrdersRepository(IMongoDatabase mongoDatabase)
    {
        _orders = mongoDatabase.GetCollection<Order>(collectionName);
    }
    public async Task<Order?> AddOrder(Order order)
    {
        order.OrderID = Guid.NewGuid();
        order._id = order._id;

        foreach (OrderItem item in order.OrderItems)
        {
            order._id = Guid.NewGuid();
        }

        await _orders.InsertOneAsync(order);

        return order;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        FilterDefinition<Order> filter =  Builders<Order>.Filter.Eq(temp => temp.OrderID, orderID);

        Order? existingOrder = await (await _orders.FindAsync(filter)).FirstOrDefaultAsync();

        if (existingOrder == null)
            return false;

        DeleteResult deleteResult = await _orders.DeleteOneAsync(filter);

        return deleteResult.DeletedCount > 0;
    }

    public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        return   await (await _orders.FindAsync(filter)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>?> GetOrders()
    {
        return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
    }

    public async Task<IEnumerable<Order>?> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).ToList();
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(temp => temp.OrderID, order.OrderID);

        Order? existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

        if (existingOrder == null)
            return null;
   
        ReplaceOneResult replaceOneResult = await _orders.ReplaceOneAsync(filter, order);

        return order;
    }
}
