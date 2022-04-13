using AutoBogus;
using Logic.Models;

namespace Logic.Repositories;

public class FakeUserRepository : IUserRepository
{
    public IEnumerable<User> GetUsers() => AutoFaker.Generate<IEnumerable<User>>(configuration => configuration.WithRepeatCount(10));
}
