
using SupplyChain.Common.Domain;


namespace SupplyChain.Modules.Users.PublicApi;

public interface IUserPublicApi
{
    Task<Result<UserApiResponse?>> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);


}


public class UserApiResponse
{
    public Guid Id { get; set; }
    public string Email { get;  set; }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
}
