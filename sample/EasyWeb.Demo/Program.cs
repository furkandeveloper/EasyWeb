using System.Text.Json.Serialization;
using EasyWeb.AspNetCore.ApiStandarts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });


builder.Services.ConfigureWebApiStandarts();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();