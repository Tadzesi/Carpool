using Carpool.CoreApi.Configurations;
using Carpool.CoreApi.Middleware;
using GuardNet;

namespace Carpool.CoreApi.Extensions;

public static class AppExtensions
{
    /// <summary>
    /// Adds global exception handling middleware
    /// </summary>
    /// <param name="app"></param>
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        Guard.NotNull(app, nameof(app));
        app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }

    /// <summary>
    /// Adds global exception handling middleware
    /// </summary>
    /// <param name="app"></param>
    public static IApplicationBuilder UseResponseTimeMiddleware(this IApplicationBuilder app)
    {
        Guard.NotNull(app, nameof(app));
        app.UseMiddleware<ResponseTimeMiddleware>();
        return app;
    }

    /// <summary>
    /// Register Swagger and SwaggerUI middleware
    /// </summary>
    /// <param name="app"></param>
    /// <param name="config"></param>
    public static void UseSwaggerMiddleware(this IApplicationBuilder app, IConfiguration config)
    {
        var swaggerConfig = config.GetSection(nameof(SwaggerConfig)).Get<SwaggerConfig>();
        app.UseSwagger(options =>
        {
            options.RouteTemplate = $"{swaggerConfig.RoutePrefix}/{{documentName}}/{swaggerConfig.DocsFile}";
        });
        app.UseSwaggerUI();
    }

    /// <summary>
    /// Register the ReDoc middleware
    /// </summary>
    /// <param name="app"></param>
    /// <param name="config"></param>
    public static void UseReDocMiddleware(this IApplicationBuilder app, IConfiguration config)
    {
        var swaggerConfig = config.GetSection(nameof(SwaggerConfig)).Get<SwaggerConfig>();
        app.UseReDoc(sa =>
        {
            sa.DocumentTitle = $"{swaggerConfig.Title} Documentation";
            sa.SpecUrl = $"/{swaggerConfig.DocRoutePrefix}/V1/{swaggerConfig.DocsFile}";
        });
    }
}
