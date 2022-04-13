using Logic.Models;

namespace Logic.Repositories;

public class ExternalUserRepository : IUserRepository
{
    public IEnumerable<User> GetUsers() => throw new NotImplementedException();
}
