using System.Text.Json.Serialization;
using EasyWeb.AspNetCore.ApiStandarts;
using EasyWeb.AspNetCore.Filters;
using EasyWeb.AspNetCore.Swagger;
using MarkdownDocumenting.Extensions;

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

builder.Services.AddDocumentation();

var app = builder.Build();

app.UseRouting();

app.UseDocumentation(config =>
{
    config.HighlightJsStyle = "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.13.1/styles/github.min.css";
    config.GetMdlStyle = "https://code.getmdl.io/1.1.3/material.teal-orange.min.css";
    config.RootPathHandling = HandlingType.Redirect;
    config
        .AddCustomLink(new MarkdownDocumenting.Elements.CustomLink("Swagger", "/api-docs"))
        .AddFooterLink(new MarkdownDocumenting.Elements.CustomLink("furkandeveloper", "https://github.com/furkandeveloper"))
        .AddFooterLink(new MarkdownDocumenting.Elements.CustomLink("Repository", "https://github.com/furkandeveloper/EasyWeb"));
});

app.ApplySwaggerConfiguration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();