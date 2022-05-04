global using FastEndpoints;
using FastEndpoints.Swagger;
using Hangfire;
using HangFireLogic;
using HangFireLogic.HostedServices;
using HangFireLogic.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5006");
/*builder.Logging.AddJsonConsole();*/ // To much data!
builder.Services
    .AddHostedService<Scheduler>()
    .AddFastEndpoints()
    .AddMemoryCache()
    .AddSwaggerDoc()
    .AddScoped<ISuperAwesomeStuff, SuperAwesomeStuff>()
    .AddSingleton<CacheService>()
    .AddHangfire(config =>
    {
        config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseColouredConsoleLogProvider()
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseInMemoryStorage();
    })
    .AddHangfireServer();

var app = builder.Build();

app.UseAuthorization();
app.UseHangfireDashboard();
app.UseFastEndpoints();
app.UseOpenApi();

app.UseSwaggerUi3(c => c.ConfigureDefaults());

await app.RunAsync();

