using Azure.Identity;
using Logic.Repositories;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppConfig");
builder.Host
    .ConfigureAppConfiguration((context, builder) => builder.AddAzureAppConfiguration(options =>
    {
        options.Connect(connectionString)
        .Select(KeyFilter.Any, LabelFilter.Null)
        .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
        .ConfigureKeyVault(kvo => kvo.SetCredential(new DefaultAzureCredential(true)))
        .ConfigureRefresh(options => options.Register("Sentinel", true).SetCacheExpiration(TimeSpan.FromSeconds(3)))
        .UseFeatureFlags(options =>
        {
            options.Select(KeyFilter.Any, LabelFilter.Null);
            options.Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName);
            options.CacheExpirationInterval = TimeSpan.FromSeconds(3);
        });
    }))
    .ConfigureServices(services =>
    {
        services.AddAzureAppConfiguration();
        services.AddFeatureManagement();
        services.AddControllersWithViews();

        services.AddHttpClient<IUserRepository, UserRepository>(config => config.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAzureAppConfiguration();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
