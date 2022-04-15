using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:AppConfig");
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, builder) => builder.AddAzureAppConfiguration(options =>
    {
        options.Connect(connectionString)
        .Select("_")
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
        services.AddFeatureManagement().AddFeatureFilter<PercentageFilter>();
    })
    .Build();

host.Run();