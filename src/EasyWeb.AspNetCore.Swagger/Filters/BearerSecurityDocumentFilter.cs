using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyWeb.AspNetCore.Swagger.Filters;

public class BearerSecurityDocumentFilter : IDocumentFilter
{
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