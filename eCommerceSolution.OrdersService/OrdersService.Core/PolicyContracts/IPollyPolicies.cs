using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.PolicyContracts;

public interface IPollyPolicies
{
    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount);
    IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(int intHandledEventsAllowedBeforeBreaking, TimeSpan timespanDurationOfBreak);
    IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(TimeSpan timespanTimeOut);    
}
