using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Users.Application.Users.GetUserById;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Presentation.Users;



internal sealed class GetUserById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("user/{id}/get-by-id", async (Guid id, ISender _sender) =>
        {
            Result<UserResponse?> result = await _sender.Send(new GetUserByIdQuery(id));

            return result.Match(Results.Ok, ApiResults.Problem);

        }).WithTags(Tags.users);
    }
}
