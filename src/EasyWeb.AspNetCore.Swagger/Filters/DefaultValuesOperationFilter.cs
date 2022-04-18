using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyWeb.AspNetCore.Swagger.Filters;

/// <summary>
/// Default Values Operation Filter
/// </summary>
public class DefaultValuesOperationFilter : IOperationFilter
{
    /// <summary>  
    /// Applies the filter to the specified operation using the given context.  
    /// </summary>  
    /// <param name="operation">The operation to apply the filter to.</param>  
    /// <param name="context">The current operation filter context.</param>  
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            return;
        }
        foreach (var parameter in operation.Parameters)
        {
            var description = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);
            var routeInfo = description?.RouteInfo;

            parameter.Description ??= description?.ModelMetadata?.Description;

            if (routeInfo == null)
            {
                continue;
            }

            if (parameter.Schema?.Default == null)
            {
                if (parameter.Schema != null)
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue?.ToString());
            }

            parameter.Required |= !routeInfo.IsOptional;
        }
    }
}