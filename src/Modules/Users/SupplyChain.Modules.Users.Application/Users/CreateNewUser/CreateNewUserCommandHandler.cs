using SupplyChain.Common.Application.Messaging;
using SupplyChain.Common.Domain;
using SupplyChain.Modules.Users.Application.Abstractions.Data;
using SupplyChain.Modules.Users.Domain.Users;
using SupplyChain.Modules.Users.Domain.Users.Repository;
using System;
using System.Linq;

namespace SupplyChain.Modules.Users.Application.Users.CreateNewUser;


internal sealed class CreateNewUserCommandHandler : ICommandHandler<CreateNewUserCommand, Guid>
{


    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateNewUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        User? email = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if(email is not null)
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExist(request.Email));
        }

        var user = User.Create(request.FirstName, request.LastName, request.Email);
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
