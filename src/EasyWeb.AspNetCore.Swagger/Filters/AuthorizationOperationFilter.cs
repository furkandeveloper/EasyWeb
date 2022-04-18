using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyWeb.AspNetCore.Swagger.Filters;

/// <summary>
/// Authorization Operation Filter for Swagger
/// </summary>
public class AuthorizationOperationFilter : IOperationFilter
{
    /// <summary>
    /// Apply Operation Filter for Authorization
    /// </summary>
    /// <param name="operation">
    /// Operation Object see <see cref="OpenApiOperation"/>
    /// </param>
    /// <param name="context">
    /// Operation Filter Context. See <see cref="OperationFilterContext"/>
    /// </param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is AuthorizeAttribute) is
            AuthorizeAttribute filter)
        {
            operation.Security = (filter.AuthenticationSchemes ?? filter.Policy)?.Split(',')
                .Select(s => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = s,
                            }
                        },
                        Array.Empty<string>()
                    }
                })
                .ToList();
        }
    }
}