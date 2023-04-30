using Carpool.CoreApi.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using WebTD.Api.Services.Swagger.Filters;
using WebTDApi.Services.Swagger.Filters;

namespace Carpool.CoreApi.Swagger
{
    /// <summary>
    /// Configures the Swagger generation options
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiProvider;
        private readonly SwaggerConfig _swaggerConfig;



        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class
        /// </summary>
        /// <param name="apiProvider">The <see cref="IApiVersionDescriptionProvider">apiProvider</see> used to generate Swagger documents.</param>
        /// <param name="swaggerConfig"></param>
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiProvider, IOptions<SwaggerConfig> swaggerConfig)
        {
            _apiProvider = apiProvider ?? throw new ArgumentNullException(nameof(apiProvider));
            _swaggerConfig = swaggerConfig.Value;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // Add a custom operation filter which sets default values
            options.OperationFilter<SwaggerDefaultValues>();

            // Add a Enum filer for openapi-generator
            options.SchemaFilter<EnumSchemaFilter>();

            options.UseAllOfToExtendReferenceSchemas();
            options.UseAllOfForInheritance();
            options.UseOneOfForPolymorphism();

            // Add a swagger document for each discovered API version
            // Note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _apiProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            // Add JWT Bearer Authorization
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            // Add Security Requirement
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            // Include Document file for WebTD.Api project
            options.IncludeXmlComments(GetXmlCommentsPath());

            // Include Document file for WebTD.Api.Core project
            options.IncludeXmlComments(GetXmlCommentsPathForWebTDApiCore());

        }

        private static string GetXmlCommentsPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }

        private static string GetXmlCommentsPathForWebTDApiContracts()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.Contracts.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }

        private static string GetXmlCommentsPathForWebTDApiApplicationCore()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.ApplicationCore.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }

        private static string GetXmlCommentsPathForWebTDApiCore()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.Core.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }

        /// <summary>
        /// Create API version
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = _swaggerConfig.Title,
                Version = description.ApiVersion.ToString(),
                Description = _swaggerConfig.Description,
                Contact = new OpenApiContact
                {
                    Name = _swaggerConfig.ContactName,
                    Email = _swaggerConfig.ContactEmail
                },
                License = new OpenApiLicense
                {
                    Name = _swaggerConfig.LicenseName,
                    Url = new Uri(_swaggerConfig.LicenseUrl)
                },
                // Add a logo to ReDoc page
                Extensions = new Dictionary<string, IOpenApiExtension>
                {
                    {
                        "x-logo", new OpenApiObject
                        {
                            {"url", new OpenApiString("../swagger/vissim-logo.jpg")}
                        }
                    }
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " ** THIS API VERSION HAS BEEN DEPRECATED!";
            }

            return info;
        }
    }
}