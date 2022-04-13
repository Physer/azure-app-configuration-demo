using AutoBogus;
using Logic.Models;
using Microsoft.FeatureManagement;
using System.Text.Json;

namespace Logic.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;
    private readonly IFeatureManager _featureManager;

    public UserRepository(HttpClient httpClient, 
        IFeatureManager featureManager)
    {
        _httpClient = httpClient;
        _featureManager = featureManager;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        if(await _featureManager.IsEnabledAsync(FeatureFlags.UseExternalUsers.ToString()))
            return JsonSerializer.Deserialize<IEnumerable<User>>(await _httpClient.GetStringAsync("users"), new JsonSerializerOptions
            {
                MaxDepth = 5,
                PropertyNameCaseInsensitive = true
            }) ?? Enumerable.Empty<User>();

        return AutoFaker.Generate<IEnumerable<User>>(configuration => configuration.WithRepeatCount(10));
    }
}
