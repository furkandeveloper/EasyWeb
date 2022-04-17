using EasyWeb.AspNetCore.Filters.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWeb.AspNetCore.Filters;

/// <summary>
/// Extensions for configuring MVC using an <see cref="IMvcBuilder"/>.
/// </summary>
public static class Program
{
    /// <summary>
    /// Registers an action to configure <see cref="MvcOptions"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IMvcBuilder"/>.</param>
    /// <returns>The <see cref="IMvcBuilder"/>.</returns>
    public static IMvcBuilder AddEasyWebCore(this IMvcBuilder builder)
    {
        builder.AddMvcOptions(options => options.Filters.Add<ModelStateFilterAttribute>());
        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        return builder;
    }
}