using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.PolicyContracts;

public interface IUsersMicroservicePolicies
{ 
    IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy();
}
