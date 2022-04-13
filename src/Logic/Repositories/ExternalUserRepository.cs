using Logic.Models;
using System.Text.Json;

namespace Logic.Repositories;

public class ExternalUserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;

    public ExternalUserRepository(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IEnumerable<User>> GetUsersAsync() => JsonSerializer.Deserialize<IEnumerable<User>>(await _httpClient.GetStringAsync("users"), new JsonSerializerOptions
    {
        MaxDepth = 5,
        PropertyNameCaseInsensitive = true
    }) ?? Enumerable.Empty<User>();
}
