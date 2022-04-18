using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyWeb.AspNetCore.Swagger.Filters;

/// <summary>
/// Bearer Security Document Filter for Swagger.
/// </summary>
public class BearerSecurityDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// Apply Bearer Security Document Filter
    /// </summary>
    /// <param name="swaggerDoc">
    /// Open Api Document object. See <see cref="OpenApiDocument"/>
    /// </param>
    /// <param name="context">
    /// Document Filter Context. See <see cref="DocumentFilterContext"/>
    /// </param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.SecurityRequirements ??= new List<OpenApiSecurityRequirement>();


        var requirement = new OpenApiSecurityRequirement();

        var scheme = new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Scheme = "Authorization",
            BearerFormat = "Bearer",
            Type = SecuritySchemeType.ApiKey,
        };

        requirement.Add(scheme, new string[] { "Bearer" });

        swaggerDoc.SecurityRequirements.Add(requirement);
    }
}