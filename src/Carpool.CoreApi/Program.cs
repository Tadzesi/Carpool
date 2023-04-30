using Carpool.CoreApi.ApplicationCore.Database.Seeding;
using Carpool.CoreApi.ApplicationCore.Extensions;
using Serilog;
using Serilog.Events;

namespace Carpool.CoreApi;
public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            IHost host = CreateHostBuilder(args).Build();
            await PostInitializeAsync(host);
            await host.RunAsync();

        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Application execution failed: {Err}", e.Message);
        }
    }

    private static async Task PostInitializeAsync(IHost host)
    {
        await host.MigrateDbContextAsync(new CoreContextSeeder());
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        // Configure Serilog
        .UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                //.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext())
        // Set the content root to be the current directory
        .UseContentRoot(Directory.GetCurrentDirectory())
        // Disable the dependency injection scope validation feature
        .UseDefaultServiceProvider(options => options.ValidateScopes = false)
        .UseEnvironment(Environments.Development)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseIISIntegration();
            webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureAppConfiguration((builderContext, config) =>
        {
            var env = builderContext.HostingEnvironment;
            config.SetBasePath(env.ContentRootPath);
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
        })
        .ConfigureLogging(logging =>
        {
            // Clear default logging providers
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddSerilog();
        });
}




