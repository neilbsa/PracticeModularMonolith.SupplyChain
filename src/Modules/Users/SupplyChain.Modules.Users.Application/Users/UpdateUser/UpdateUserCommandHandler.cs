using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Users.Application.Abstractions.Data;
using SupplyChain.Modules.Users.Domain.Users;
using SupplyChain.Modules.Users.Domain.Users.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Application.Users.UpdateUser;



internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.Id));
        }

        Result result = user.Update(request.Firstname, request.LastName);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return result;

    }
}

