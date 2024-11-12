using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SupplyChain.Common.Domain;
using SupplyChain.Common.Presentation.ApiResults;
using SupplyChain.Common.Presentation.Endpoint;
using SupplyChain.Modules.Users.Application.Users.CreateNewUser;
using SupplyChain.Modules.Users.Application.Users.GetUserById;
using SupplyChain.Modules.Users.Application.Users.UpdateUser;

namespace SupplyChain.Modules.Users.Presentation.Users;
internal sealed class CreateNewUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/create", async (CreateNewUserRequest request , ISender _sender) =>
        {
            Result<Guid> result = await 
            _sender.Send(new CreateNewUserCommand(request.FirstName, request.LastName, request.Email));

            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.users);
    }


    internal sealed class CreateNewUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    } 
}
