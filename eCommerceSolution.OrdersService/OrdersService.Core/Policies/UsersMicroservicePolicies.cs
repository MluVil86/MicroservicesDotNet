using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using OrderService.BusinessLogicLayer.PolicyContracts;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;
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


    //Circuit breaker to block further requests if an attempts fails a number of times 
    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        AsyncCircuitBreakerPolicy<HttpResponseMessage> policy = 
         Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
         .CircuitBreakerAsync(
             handledEventsAllowedBeforeBreaking: 3,
             durationOfBreak: TimeSpan.FromMinutes(2),
             onBreak: (outcome, timespan) =>
             {
                 _logger.LogInformation($"Circuit breaker opened for {timespan.TotalMinutes} minutes due to 3 consecutive failures.  " +
                     $"The subsequent requests will be blocked");
             }, onReset: () => 
             {
                 _logger.LogInformation($"Circuit breaker closed. The subsequent requests will be allowed.");
             });

        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
    {
        var retryPolicy = GetRetryPolicy();
        var circuitBreakerPolicy = GetCircuitBreakerPolicy();
        var timeoutPolicy = GetTimeoutPolicy();

        AsyncPolicyWrap<HttpResponseMessage> policyWrap =  Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);

        return policyWrap;
    }

    //Retry sending request for a number of times
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        AsyncRetryPolicy<HttpResponseMessage> policy =
        Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .WaitAndRetryAsync(
            retryCount: 5, 
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), 
            onRetry:(outcome, timespan, retryAttempt, content) =>
            {
                _logger.LogInformation($"Retry {retryAttempt} after {timespan.TotalSeconds} seconds");
            });

        return policy;
    }

    public IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
    {
        AsyncTimeoutPolicy<HttpResponseMessage> policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(1500));

        _logger.LogInformation($"Timeout policy: {TimeSpan.FromMilliseconds(1500)}");

        return policy;  
    }
}
