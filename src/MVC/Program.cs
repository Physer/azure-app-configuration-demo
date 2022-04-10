using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppConfig");
builder.Host
    .ConfigureAppConfiguration(builder => builder.AddAzureAppConfiguration(options =>
    {
        options.Connect(connectionString)
        .ConfigureKeyVault(kvo => kvo.SetCredential(new DefaultAzureCredential(true)))
        .ConfigureRefresh(options => options.Register("Sentinel", true).SetCacheExpiration(TimeSpan.FromSeconds(3)));
    }))
    .ConfigureServices(services => 
    {
        services.AddAzureAppConfiguration();
        services.AddControllersWithViews();
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
