using System;
using System.IO;
using System.Reflection;
using EasyWeb.AspNetCore.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EasyWeb.AspNetCore.Swagger;

/// <summary>
/// Swagger Configuration extension
/// </summary>
public static class Program
{
    /// <summary>
    /// Configure Swagger
    /// </summary>
    /// <param name="services">
    /// Service Collection. See <see cref="IServiceCollection"/>
    /// </param>
    /// <param name="action">
    /// Swagger Configuration Object. See <see cref="Configuration"/>
    /// </param>
    public static void ConfigureSwagger(this IServiceCollection services, Action<Configuration> action)
    {
        var configuration = new Configuration();
        action.Invoke(configuration);
        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                var title = configuration.Title;
                options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                {
                    Title = description.IsDeprecated ? title + "- [Deprecated]" : title,
                    Version = description.ApiVersion.ToString(),
                    Description = configuration.Description,
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html"),
                    }
                });
            }
            
            options.OperationFilter<DefaultValuesOperationFilter>();
            options.OperationFilter<AuthorizationOperationFilter>();
            options.OperationFilter<ContentTypeOperationFilter>();
            
            options.DocumentFilter<CamelCaseDocumentFilter>();
            options.DocumentFilter<BearerSecurityDocumentFilter>();
        });
    }

    /// <summary>
    /// Apply Configuration and use for swagger
    /// </summary>
    /// <param name="app">
    /// A program abstraction. <see cref="IApplicationBuilder"/>
    /// </param>
    public static void ApplySwaggerConfiguration(this IApplicationBuilder app)
    {
        var versioningProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.EnableDeepLinking();
            options.ShowExtensions();
            options.DisplayRequestDuration();
            options.DisplayOperationId();
            options.DocExpansion(DocExpansion.None);
            options.EnableFilter();
            options.EnableValidator();
            options.ShowCommonExtensions();
            options.ShowExtensions();
            options.RoutePrefix = "api-docs";
            foreach (var description in versioningProvider.ApiVersionDescriptions)
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            
        });
    }
}