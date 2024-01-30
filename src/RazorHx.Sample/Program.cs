using RazorHx.Components.Builder;
using RazorHx.Components.DependencyInjection;
using RazorHx.Components.Results;
using RazorHx.Sample.Components;
using Index = RazorHx.Sample.Index;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorHxComponents(options => {
    options.RootComponent = typeof(Index);
});

var app = builder.Build();

app.UseRouting();
app.UseRazorHxComponents();

app.MapGet("/", () => new RazorHxComponentResult<Hello>());
app.MapGet("/withboost", () => new RazorHxComponentResult<World>());
app.MapGet("/withoutboost", () => new RazorHxComponentResult<World>());
app.MapGet("/get", () => new RazorHxComponentResult<Get>());

app.Run();