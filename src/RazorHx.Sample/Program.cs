using RazorHx.Builder;
using RazorHx.DependencyInjection;
using RazorHx.Results;
using RazorHx.Sample;
using RazorHx.Sample.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorHxComponents(options => {
    options.RootComponent = typeof(MainLayout);
});

var app = builder.Build();

app.UseRouting();
app.UseRazorHxComponents();

app.MapGet("/", () => new RazorHxResult<Hello>());
app.MapGet("/withboost", () => new RazorHxResult<World>());
app.MapGet("/withoutboost", () => new RazorHxResult<World>());
app.MapGet("/get", () => new RazorHxResult<Get>());
app.MapGet("/layoutoverride", () => new RazorHxResult<LayoutOverride>());

app.Run();