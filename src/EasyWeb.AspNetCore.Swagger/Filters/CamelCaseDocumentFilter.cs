using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EasyWeb.AspNetCore.Swagger.Filters;

/// <summary>
/// Camel Case Document Filter for Swagger.
/// </summary>
public class CamelCaseDocumentFilter : IDocumentFilter
{
    /// <summary>
    /// Apply Camel Case Document Filter.
    /// </summary>
    /// <param name="swaggerDoc">
    /// Open Api Document object. See <see cref="OpenApiDocument"/>
    /// </param>
    /// <param name="context">
    /// Document Filter Context. See <see cref="DocumentFilterContext"/>
    /// </param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths;
        var newPaths = new OpenApiPaths();
        var removeKeys = new List<string>();
        foreach (var (key, value) in paths)
        {
            var routes = key.Split('/');
            var newKey = "/" + string.Join('/', routes
                .Where(x => !string.IsNullOrEmpty(x) && x.Length > 1)
                .Select(s => char.ToLowerInvariant(s[0]) + s.Remove(0, 1)));


            if (newKey == key) continue;
            removeKeys.Add(key);
            newPaths.Add(newKey, value);
        }

        //	add the new keys
        foreach (var (key, value) in newPaths)
        {
            swaggerDoc.Paths.Add(key, value);
        }

        //	remove the old keys
        foreach (var key in removeKeys)
        {
            swaggerDoc.Paths.Remove(key);
        }
    }
}