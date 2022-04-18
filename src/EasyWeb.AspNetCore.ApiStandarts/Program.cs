using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWeb.AspNetCore.ApiStandarts;

/// <summary>
/// Extensions for configuring MVC using an <see cref="IServiceCollection"/>.
/// </summary>
public static class Program
{
    /// <summary>
    /// Configure Web Api standard.
    /// These standards;
    /// - HttpContextAccessor
    /// - Memory Cache
    /// - Response Caching
    /// - Api Versioning
    /// </summary>
    /// <param name="services">
    /// Service Collection <see cref="IServiceCollection"/>
    /// </param>
    /// <returns>
    /// IServiceCollection See <see cref="IServiceCollection"/>
    /// </returns>
    public static IServiceCollection ConfigureWebApiStandarts(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddMemoryCache();
        services.AddResponseCaching();
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });
        services.AddVersionedApiExplorer();
        return services;
    }
}