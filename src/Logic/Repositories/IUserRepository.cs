using Logic.Models;

namespace Logic.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
}
