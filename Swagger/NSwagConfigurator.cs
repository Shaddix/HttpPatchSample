using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace HttpPatchSample.Swagger
{
    public class NSwagConfigurator
    {
        public NSwagConfigurator(IConfiguration configuration)
        {
            SwaggerOptions = configuration.GetSection("Swagger").Get<SwaggerOptions>();
        }

        private SwaggerOptions SwaggerOptions { get; set; }


        public virtual void AddSwagger(IServiceCollection services)
        {
            services.AddOpenApiDocument(
                options =>
                {
                    options.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token",
                        new OpenApiSecurityScheme
                        {
                            Type = OpenApiSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                            Description = "Copy 'Bearer ' + valid JWT token into field",
                            In = OpenApiSecurityApiKeyLocation.Header
                        }));


                    options.PostProcess = document =>
                    {
                        document.Info = new NSwag.OpenApiInfo
                        {
                            Version = SwaggerOptions.Version,
                            Title = SwaggerOptions.Title,
                            Description = SwaggerOptions.Description,
                            Contact = new NSwag.OpenApiContact
                            {
                                Email = SwaggerOptions.Contact.Email
                            },
                            License = new NSwag.OpenApiLicense
                            {
                                Name = SwaggerOptions.License.Name
                            },
                        };
                    };


                    options.AddSecurity("Bearer", new OpenApiSecurityScheme()
                    {
                        Type = OpenApiSecuritySchemeType.OAuth2,
                        Description = "OAuth2 Authentication",
                        Flow = OpenApiOAuth2Flow.Password,
                        Flows = new NSwag.OpenApiOAuthFlows()
                        {
                            Password = new NSwag.OpenApiOAuthFlow()
                            {
                                TokenUrl = "/connect/token",
                                RefreshUrl = "/connect/token",
                                AuthorizationUrl = "/connect/token",
                                Scopes = new Dictionary<string, string>()
                                {
                                    {"profile", "profile"},
                                    {"offline_access", "offline_access"},
                                    {"HttpPatchSampleAPI", "HttpPatchSampleAPI"},
                                }
                            }
                        }
                    });
                    options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
                    options.SchemaProcessors.Add(new RequireValueTypesSchemaProcessor());
                    options.FlattenInheritanceHierarchy = true;
                    options.GenerateEnumMappingDescription = true;
                });
        }

        public virtual void UseSwagger(IApplicationBuilder app)
        {
            if (!SwaggerOptions.Enabled)
            {
                return;
            }

            app.UseOpenApi(options => { options.Path = SwaggerOptions.Endpoint.Url; });
            app.UseSwaggerUi3(
                options =>
                {
                    options.Path = SwaggerOptions.Endpoint.UiUrl;
                    options.DocumentPath = SwaggerOptions.Endpoint.Url;
                });
        }
    }
}