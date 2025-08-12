using OrderService.BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using Polly.Bulkhead;
using Microsoft.Extensions.Logging;

namespace OrderService.BusinessLogicLayer.HttpClients;

public class ProductMicroserviceClient
{
    private readonly HttpClient _httpClient;
    private ILogger<ProductMicroserviceClient> _logger;
    public ProductMicroserviceClient(HttpClient httpClient, ILogger<ProductMicroserviceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ProductResponse?> GetProductByProdcutID(Guid ProductID)
    {
        try
        {
            if (ProductID == Guid.Empty)
                return null;

            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"/api/products/search/productid/{ProductID}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                    return null;
                else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                    throw new HttpRequestException("Bad request", null, HttpStatusCode.BadRequest);
                else
                {
                    throw new HttpRequestException("Http request failed with status code", null, responseMessage.StatusCode);
                }
            }

            ProductResponse? returnProdcut = await responseMessage.Content.ReadFromJsonAsync<ProductResponse>();

            if (returnProdcut == null)
                throw new ArgumentException("Invalid Product ID");
            return returnProdcut;
        }
        catch (BulkheadRejectedException ex)
        {
            _logger.LogError(ex, "Bulkhead isolation blocks requests since the request queue is full");
            return new ProductResponse(ProductID: Guid.Empty,
                                       ProductName: "Temporarily Unavailable (Fallback)",
                                       Category: "Temporarily Unavailable (Fallback)",
                                       UnitPrice: 0,
                                       QuantityInStock: 0);
        }
    }
}
