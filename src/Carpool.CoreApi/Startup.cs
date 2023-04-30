using Carpool.CoreApi.ApplicationCore.Database;
using Carpool.CoreApi.ApplicationCore.Models;
using Carpool.CoreApi.ApplicationCore.Queries;
using Carpool.CoreApi.ApplicationCore.Services.CarsService;
using Carpool.CoreApi.ApplicationCore.Services.EmployeesService;
using Carpool.CoreApi.ApplicationCore.Services.Interfaces;
using Carpool.CoreApi.ApplicationCore.Services.RidesService;
using Carpool.CoreApi.ApplicationCore.Validations;
using Carpool.CoreApi.Configurations;
using Carpool.CoreApi.Core.Configuration.ApiBehavior;
using Carpool.CoreApi.Core.Controllers.Convetions;
using Carpool.CoreApi.Extensions;
using Carpool.CoreApi.Middleware;
using Carpool.CoreApi.Services.Filters;
using CorrelationId;
using CorrelationId.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using WebTDApi.Middleware;

namespace Carpool.CoreApi;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public Startup(IWebHostEnvironment env, IConfiguration configuration)
    {
        Environment = env;
        Configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services required for using options
        services.AddOptions();

        // Add the whole configuration object here
        services.AddSingleton(Configuration);

        // Add health check services
        services.AddHealthChecks();

        RegisterConfigurations(services);
        RegisterServices(services);
        RegisterFluentValidation(services);


        services.AddCorsPolicy("EnableCORS");

        // Adds service API versioning
        services.AddAndConfigureApiVersioning();

        // Adds services for controllers
        services.AddControllers(options =>
        {
            options.Conventions
                .Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
        })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = DefaultApiBehaviorStrategy.InvalidModelStateResponseFactory;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
            });


        // Adds Swagger support
        services.AddSwaggerMiddleware();

        services.AddDefaultCorrelationId(options =>
        {
            options.IncludeInResponse = true;
            options.AddToLoggingScope = true;
        });

        services.AddResponseCaching();
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.AddDbContext<CoreContext>(x => x.UseSqlServer(Configuration.GetConnectionString("MicrosoftSQLConnection")));

        services.AddHttpClient();

        //fluent validation 
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationRulesToSwagger();

        services.AddRepositoryFactory();


    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
    {
        // Needed for a ReDoc logo
        const string LOGO_FILE_PATH = "wwwroot/swagger";
        var fileprovider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, LOGO_FILE_PATH));
        var requestPath = new PathString($"/{LOGO_FILE_PATH}");

        app.UseDefaultFiles(new DefaultFilesOptions
        {
            FileProvider = fileprovider,
            RequestPath = requestPath,
        });

        app.UseFileServer(new FileServerOptions()
        {
            FileProvider = fileprovider,
            RequestPath = requestPath,
            EnableDirectoryBrowsing = false
        });

        // Add correlation IDs fo request
        app.UseCorrelationId();

        app.UseStaticFiles();

        // Register ReDoc middleware
        // URL ../api-docs/index.html
        app.UseReDocMiddleware(config);

        // Register Swagger and SwaggerUI middleware
        // URL ../swagger/index.html
        app.UseSwaggerMiddleware(config);

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();

        // For elevated security, it is recommended to remove this middleware and set your server to only listen on https. 
        // A slightly less secure option would be to redirect http to 400, 505, etc.
        app.UseHttpsRedirection();


        // Adds global error handling middleware
        app.UseExceptionHandlingMiddleware();

        // Adds enpoint routing middleware
        app.UseRouting();

        // Adds a CORS middleware
        app.UseCors("EnableCORS");

        app.UseEndpoints(configure =>
        {
            configure.MapControllers();
            configure.MapDefaultControllerRoute();
            configure.MapHealthChecks("health");
            // Redirect root to Swagger UI
            configure.MapGet("", context =>
            {
                context.Response.Redirect("./swagger/index.html", permanent: false);
                return Task.FromResult(0);
            });
        });
    }

    /// <summary>
    /// Register a configuration instances which TOptions will bind against
    /// </summary>
    /// <param name="services"></param>
    protected void RegisterConfigurations(IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        services.Configure<SwaggerConfig>(Configuration.GetSection(nameof(SwaggerConfig)));
    }

    /// <summary>
    /// Register services and middlewares
    /// </summary>
    /// <param name="services"></param>
    protected virtual void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ValidModelStateAsyncActionFilter>();

        // Register middlewares
        services.AddTransient<ErrorHandlingMiddleware>();
        services.AddTransient<RequestResponseLoggingMiddleware>();

        // Services
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<ICarTypeService, CarTypeService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IRideService, RideService>();

    }

    /// <summary>
    /// Register Fluent Validation validators
    /// </summary>
    /// <param name="services"></param>
    protected virtual void RegisterFluentValidation(IServiceCollection services)
    {
        services.AddTransient<IValidator<UpdateCarModel>, UpdateCarModelValidation>();
        services.AddTransient<IValidator<CreateCarModel>, CreateCarModelValidation>();
        services.AddTransient<IValidator<UpdateCarTypeModel>, UpdateCarTypeModelValidation>();
        services.AddTransient<IValidator<CreateCarTypeModel>, CreateCarTypeModelValidation>();
        services.AddTransient<IValidator<UpdateEmployeeModel>, UpdateEmployeeModelValidation>();
        services.AddTransient<IValidator<CreateEmployeeModel>, CreateEmployeeModelValidation>();
        services.AddTransient<IValidator<DateTimePickerPeriodQuery>, DateTimePickerPeriodQueryValidator>();
    }
}
