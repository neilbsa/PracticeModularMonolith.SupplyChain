using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace SupplyChain.Common.Presentation.Endpoint;
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
