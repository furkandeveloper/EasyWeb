using System.Text.Json.Serialization;
using EasyWeb.AspNetCore.ApiStandarts;
using EasyWeb.AspNetCore.Filters;
using EasyWeb.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddEasyWebCore();

builder.Services.ConfigureWebApiStandarts();

builder.Services.ConfigureSwagger(options =>
{
    options.Title = "Demo Service Title";
    options.Description = "Demo Service Description";
});

var app = builder.Build();

app.UseRouting();

app.ApplySwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();