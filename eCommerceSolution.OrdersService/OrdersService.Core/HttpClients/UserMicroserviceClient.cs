using OrderService.BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using Polly.CircuitBreaker;
using Microsoft.Extensions.Logging;
using Polly.Timeout;

namespace OrderService.BusinessLogicLayer.HttpClients;

public class UserMicroserviceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserMicroserviceClient> _logger;
    public UserMicroserviceClient(HttpClient httpClient, ILogger<UserMicroserviceClient> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<UserResponse?> GetUserByUserID(Guid UserID)
    {
        try
        {
            if (UserID == Guid.Empty)
                return null;

            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"/api/users/{UserID}");

            if (!responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.StatusCode == HttpStatusCode.NotFound)
                    return null;
                else if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                    throw new HttpRequestException("Bad request", null, HttpStatusCode.BadRequest);
                else
                {

                    //throw new HttpRequestException("Http request failed with status code", null, responseMessage.StatusCode);
                    //Instead of throwing an error, save dummy data
                    _logger.LogError("Request failed because of circuit breaker is in open state. Returning dummy data");
                    return new UserResponse(UserID: Guid.Empty, PersonName: "Temporarily Unavailable", Email: "Temporarily Unavailable", Gender: "Temporarily Unavailable");
                }
            }

            UserResponse? returnUser = await responseMessage.Content.ReadFromJsonAsync<UserResponse>();

            if (returnUser == null)
                throw new ArgumentException("Invalid user ID");
            return returnUser;
        }
        catch (BrokenCircuitException ex)
        {
            _logger.LogError(ex, "Request failed because of circuit breaker is in open state. Returning dummy data");
            return new UserResponse(UserID: Guid.Empty, PersonName: "Temporarily Unavailable (Broken Circuit)", Email: "Temporarily Unavailable Broken Circuit)", Gender: "Temporarily Unavailable (Broken Circuit)");            
        }
        catch (TimeoutRejectedException ex)
        {
            _logger.LogError(ex, "Request failed because of circuit breaker is in open state. Returning dummy data");
            return new UserResponse(UserID: Guid.Empty, PersonName: "Temporarily Unavailable(Timeout)", Email: "Temporarily Unavailable(Timeout)", Gender: "Temporarily Unavailable(Timeout)");
        }

    }
}
