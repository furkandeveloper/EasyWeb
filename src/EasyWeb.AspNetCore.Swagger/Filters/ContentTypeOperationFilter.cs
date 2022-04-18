using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyWeb.AspNetCore.Swagger.Filters;

/// <summary>
/// Content Type Operation Filter for Swagger
/// </summary>
public class ContentTypeOperationFilter : IOperationFilter
{
    /// <summary>
    /// Apply Content Type Operation Filter
    /// </summary>
    /// <param name="operation">
    /// Open Api Operation object. See <see cref="OpenApiOperation"/>
    /// </param>
    /// <param name="context">
    /// Operation Filter Context. See <see cref="OperationFilterContext"/>
    /// </param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept",
            In = ParameterLocation.Header,
            Description = "Content-Type",
            Style = ParameterStyle.Simple,
            Examples = new Dictionary<string, OpenApiExample>
            {
                { "json", new OpenApiExample{ Value = new OpenApiString("application/json") } },
                { "xml", new OpenApiExample{ Value = new OpenApiString("application/xml") } }
            },
            Required = false,
        });
    }
}