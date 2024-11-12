using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Users.Application.Users.UpdateUser;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Presentation.Users;




internal sealed class UpdateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("user/user-update", async (UserUpdateRequest request, ISender _sender) =>
        {
            var command = new UpdateUserCommand(request.Id, request.FirstName, request.LastName);

            Result result = await _sender.Send(command);

            return result.Match(() => Results.Ok(), ApiResults.Problem);


        }).WithTags(Tags.users);
    }

    internal sealed class UserUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }

    }
}
