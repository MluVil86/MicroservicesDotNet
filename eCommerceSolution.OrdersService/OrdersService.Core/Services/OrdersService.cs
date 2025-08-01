﻿using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MongoDB.Driver;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.BusinessLogicLayer.ServiceContracts;
using OrderService.DataAccessLayer.Entities;
using OrderService.DataAccessLayer.RespositoryContracts;

namespace OrderService.BusinessLogicLayer.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<OrderAddRequest> _validatorOrderAddRequest;
    private readonly IValidator<OrderItemAddRequest> _validatorOrderItemAddRequest;
    private readonly IValidator<OrderItemUpdateRequest> _validatorOrderItemUpdateRequest;
    private readonly IValidator<OrderUpdateRequest> _validatorOrderUpdateRequest;
    private readonly IOrdersValidator _ordersValidator;
    public OrdersService(IOrdersRepository ordersRepository, IMapper mapper, 
        IValidator<OrderAddRequest> validatorOrderAddRequest, 
        IValidator<OrderItemAddRequest> validatorOrderItemAddReques,
        IValidator<OrderItemUpdateRequest> validatorOrderItemUpdateRequest, 
        IValidator<OrderUpdateRequest> validatorOrderUpdateRequest,
        IOrdersValidator ordersValidator
        )
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
        _validatorOrderAddRequest = validatorOrderAddRequest;
        _validatorOrderItemAddRequest= validatorOrderItemAddReques;
        _validatorOrderItemUpdateRequest = validatorOrderItemUpdateRequest;
        _validatorOrderUpdateRequest = validatorOrderUpdateRequest;
        _ordersValidator = ordersValidator;
    }

    public async Task<OrderResponse?> AddOrder(OrderAddRequest addRequest)
    {
        if (addRequest == null)
            throw new ArgumentNullException(nameof(addRequest));


        var validationResult = await _ordersValidator.Validate(_validatorOrderAddRequest, addRequest);                        
        if (!validationResult.Any())
            throw new ArgumentException(string.Join(", ", validationResult));

      
        var itemValidationResult = await _ordersValidator.Validate(_validatorOrderItemAddRequest, addRequest.OrderItems);
        if (itemValidationResult.Any())
            throw new ArgumentException(string.Join(", ", itemValidationResult));


        //Add logic for checking if user UserID exists in Users microservice




        Order orderInput = _mapper.Map<Order>(addRequest);
        orderInput.OrderID = Guid.NewGuid();
        
        foreach (OrderItem orderItem in orderInput.OrderItems) 
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;        
        }

        orderInput.TotalBill = orderInput.OrderItems.Sum(s => s.TotalPrice);

        Order? orderOutput = await _ordersRepository.AddOrder(orderInput);

        if (orderOutput == null)
            return null;

        return _mapper.Map<OrderResponse>(orderOutput);
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderID, orderID);

        Order? existingOrder = await _ordersRepository.GetOrderByCondition(filter);

        if (existingOrder == null) 
            return false;

        return await _ordersRepository.DeleteOrder(orderID);
    }

    public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        Order? order = await _ordersRepository.GetOrderByCondition(filter);

        if (order == null) 
            return null;

        return _mapper.Map<OrderResponse>(order);
    }

    public async Task<List<OrderResponse>?> GetOrders()
    {
        IEnumerable<Order>? orders = await _ordersRepository.GetOrders();

        if (orders == null)
            return null;

        IEnumerable<OrderResponse> orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(orders);

        return orderResponses.ToList();
    }

    public async Task<List<OrderResponse>?> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        IEnumerable<Order>? orders = await _ordersRepository.GetOrdersByCondition(filter);

        if (orders == null)
            return null;

        IEnumerable<OrderResponse> orderResponses = _mapper.Map<List<OrderResponse>>(orders);

        return orderResponses.ToList();
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest updateRequest)
    {
        if (updateRequest == null)
            throw new ArgumentNullException(nameof(updateRequest));


        var validationResult = await _ordersValidator.Validate(_validatorOrderUpdateRequest, updateRequest);
        if (!validationResult.Any())
            throw new ArgumentException(string.Join(", ", validationResult));


        var itemValidationResult = await _ordersValidator.Validate(_validatorOrderItemUpdateRequest, updateRequest.OrderItems);
        if (itemValidationResult.Any())
            throw new ArgumentException(string.Join(", ", itemValidationResult));
      
        //Add logic for checking if user UserID exists in Users microservice




        Order orderUpdate = _mapper.Map<Order>(updateRequest);
        orderUpdate.OrderID = Guid.NewGuid();

        foreach (OrderItem orderItem in orderUpdate.OrderItems)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }

        orderUpdate.TotalBill = orderUpdate.OrderItems.Sum(s => s.TotalPrice);

        Order? orderOutput = await _ordersRepository.AddOrder(orderUpdate);

        if (orderOutput == null)
            return null;

        return _mapper.Map<OrderResponse>(orderOutput);
    }
}
