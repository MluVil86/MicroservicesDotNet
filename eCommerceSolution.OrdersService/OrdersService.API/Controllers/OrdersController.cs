using Microsoft.AspNetCore.Mvc;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.BusinessLogicLayer.ServiceContracts;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using OrderService.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using FluentValidation;
using OrderService.BusinessLogicLayer.Validators;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IValidator<OrderAddRequest> _validatorOrderAddRequest;
        private readonly IValidator<OrderItemAddRequest> _validatorOrderItemAddRequest;
        private readonly IValidator<OrderItemUpdateRequest> _validatorOrderItemUpdateRequest;
        private readonly IValidator<OrderUpdateRequest> _validatorOrderUpdateRequest;
        private readonly IOrdersValidator _ordersValidator;
        public OrdersController(IOrdersService ordersService,
            IValidator<OrderAddRequest> validatorOrderAddRequest,
            IValidator<OrderItemAddRequest> validatorOrderItemAddRequest,
            IValidator<OrderItemUpdateRequest> validatorOrderItemUpdateRequest,
            IValidator<OrderUpdateRequest> validatorOrderUpdateRequest,
            IOrdersValidator ordersValidator)
        {
            _ordersService = ordersService;            
            _validatorOrderAddRequest = validatorOrderAddRequest;
            _validatorOrderItemAddRequest = validatorOrderItemAddRequest;
            _validatorOrderItemUpdateRequest = validatorOrderItemUpdateRequest;
            _validatorOrderUpdateRequest= validatorOrderUpdateRequest;
            _ordersValidator = ordersValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderResponse>?> Get()
        {
            List<OrderResponse>? orders = await _ordersService.GetOrders();
            return orders;
        }

        [HttpGet("search/orderid/{orderID}")]
        public async Task<OrderResponse?> GetOrderByOrderID(Guid orderID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(temp => temp.OrderID, orderID);

            OrderResponse? order = await _ordersService.GetOrderByCondition(filter);
            return order;
        }

        [HttpGet("search/productid/{productID}")]
        public async Task<IEnumerable<OrderResponse>?> GetOrdersByProductID(Guid productID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.ElemMatch(temp => temp.OrderItems,
              Builders<OrderItem>.Filter.Eq(tempProduct => tempProduct.ProductID, productID)
              );

            List<OrderResponse>? orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }

        [HttpGet("/search/orderDate/{orderDate}")]
        public async Task<IEnumerable<OrderResponse>?> GetOrdersByOrderDate(DateTime orderDate)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(temp => temp.OrderDate.ToString("yyyy-MM-dd"), orderDate.ToString("yyyy-MM-dd")
              );

            List<OrderResponse>? orders = await _ordersService.GetOrdersByCondition(filter);
            return orders;
        }

        [HttpGet("/api/Orders/search/userId/{userID}")]
        public async Task<IEnumerable<OrderResponse>?> GetOrdersByUserID(Guid userID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.UserID, userID);

            IEnumerable<OrderResponse>? orders = await _ordersService.GetOrdersByCondition(filter);

            return orders;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OrderAddRequest addRequest) 
        {

            if(addRequest == null) 
                return BadRequest("Invalid Order details");

            var validationResult = await _ordersValidator.Validate(_validatorOrderAddRequest, addRequest);
            if (!validationResult.Any())
                return BadRequest(validationResult);

            var itemValidationResult = await _ordersValidator.Validate(_validatorOrderItemAddRequest, addRequest.OrderItems);
            if (itemValidationResult.Any())
                return BadRequest(itemValidationResult);

            OrderResponse? orderResponse = await _ordersService.AddOrder(addRequest);

            if (orderResponse == null) 
                return Problem("Error adding order");

            return Created($"api/Orders/search/orderID/{orderResponse.OrderID}", orderResponse);                
        }

        [HttpPut("{orderID}")]
        public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest updateRequest)
        {
            if (updateRequest == null)
                return BadRequest("Invalid order details");
          
            if (orderID != updateRequest.OrderID)
                return BadRequest("Details mismatch");

            var validationResult = await _ordersValidator.Validate(_validatorOrderUpdateRequest, updateRequest);
            if (!validationResult.Any())
                return BadRequest(validationResult);

            var itemValidationResult = await _ordersValidator.Validate(_validatorOrderItemUpdateRequest, updateRequest.OrderItems);
            if (itemValidationResult.Any())
                return BadRequest(itemValidationResult);

            OrderResponse? orderResponse = await _ordersService.UpdateOrder(updateRequest);

            if (orderResponse == null)
                return Problem("Error updating order");

            return Ok(orderResponse);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(Guid orderId)
        {
            if (orderId == Guid.Empty)
                return BadRequest("OrderID cannot be empty");

            bool deleteResponse = await _ordersService.DeleteOrder(orderId);

            if (!deleteResponse)
                return Problem("Unable to delete order");

            return Ok(deleteResponse);
        }
    }
}
