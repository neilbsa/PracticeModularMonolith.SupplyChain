using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Users.Application.Users.GetUserById;
using SupplyChain.Modules.Users.PublicApi;

namespace SupplyChain.Modules.Users.Infrastructure.PublicApis;
internal sealed class UserPublicApi : IUserPublicApi
{
    private readonly ISender _sender;

    public UserPublicApi(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Result<UserApiResponse?>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Result<UserResponse?> result =await _sender.Send(new GetUserByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<UserApiResponse?>(result.Error);
        }



        return Result.Success<UserApiResponse?>(new UserApiResponse() {
             Email= result.Value.Email,
              FirstName= result.Value.FirstName,
               LastName= result.Value.LastName,
                Id= result.Value.Id
        });


    }
}
