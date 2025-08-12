using Microsoft.Extensions.Logging;
using OrderService.BusinessLogicLayer.DTOs;
using OrderService.BusinessLogicLayer.PolicyContracts;
using Polly;
using Polly.Fallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using System.Reflection.Metadata;
using Polly.Bulkhead;

namespace OrderService.BusinessLogicLayer.Policies

{
    public class ProductsMicroservicePolicies : IProductsMicroservicePolicies
    {
        private ILogger<ProductsMicroservicePolicies> _logger;
        public ProductsMicroservicePolicies(ILogger<ProductsMicroservicePolicies> logger)
        {
            _logger = logger;
        }

        public IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy()
        {
            AsyncBulkheadPolicy<HttpResponseMessage> policy = Policy.BulkheadAsync<HttpResponseMessage>
                (
                    maxParallelization: 2, //allow up to 2 concurrent requests
                    maxQueuingActions: 40, //Queue up to 40 addtional requests
                    onBulkheadRejectedAsync: (context) =>
                    {
                        _logger.LogWarning("BulkheadIsolation triggered. Can't send any more requests since the queue is full");
                        throw new BulkheadRejectedException("Bulk queuw is full");
                    });
            return policy;
        }

        public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            AsyncFallbackPolicy<HttpResponseMessage> policy =
            Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .FallbackAsync(async (context) =>
            {
                _logger.LogWarning("Service could not be reach. Fallback has been triggered:  Returning dummy data");

                ProductResponse product = new(ProductID: Guid.Empty,
                                              ProductName: "Temporarily Unavailable (Fallback)",
                                              Category: "Temporarily Unavailable (Fallback)",
                                              UnitPrice: 0,
                                              QuantityInStock: 0);

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json")

                };

                return response;                    
            });
            
            return  policy;
        }
    }
}
