using Logic.Models;

namespace Logic.Repositories;

public class FakeUserRepository : IUserRepository
{
    public IEnumerable<User> GetUsers() => throw new NotImplementedException();
}
