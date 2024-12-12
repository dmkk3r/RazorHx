using Microsoft.Extensions.DependencyInjection;

namespace RazorHx.DependencyInjection;

public sealed class DefaultRazorHxBuilder(IServiceCollection services) : IRazorHxBuilder
{
    public IServiceCollection Services { get; } = services;
}