using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Users.Domain.Users;
using SupplyChain.Modules.Users.Domain.Users.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Application.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse?>
{

    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResponse?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse?>(UserErrors.NotFound(request.Id));
        }

        return new UserResponse()
        {
            Id = user.Id,
            FirstName = user.FirstName.Value,
            LastName = user.LastName.Value,
            Email = user.Email.Value,

        };



    }
}
