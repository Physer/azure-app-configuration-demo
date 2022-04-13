using Logic.Models;

namespace Logic.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetUsers();
}
