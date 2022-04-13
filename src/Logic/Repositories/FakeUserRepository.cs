using AutoBogus;
using Logic.Models;

namespace Logic.Repositories;

public class FakeUserRepository : IUserRepository
{
    public Task<IEnumerable<User>> GetUsersAsync() => Task.FromResult(AutoFaker.Generate<IEnumerable<User>>(configuration => configuration.WithRepeatCount(10)));
}
