using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.Policies;

public class UsersMicroservicePolicies : IUsersMicroservicePolicies
{
    private readonly ILogger<UsersMicroservicePolicies> _logger;
    public UsersMicroservicePolicies(ILogger<UsersMicroservicePolicies> logger)
    {
        _logger = logger;
    }
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        AsyncRetryPolicy<HttpResponseMessage> policy =
        Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(
            retryCount: 3, 
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
            onRetry:(outcome, timespan, retryAttempt, content) =>
            {
                _logger.LogInformation($"Retry {retryAttempt} after {timespan.TotalSeconds} seconds");
            });

        return policy;
    }
}
