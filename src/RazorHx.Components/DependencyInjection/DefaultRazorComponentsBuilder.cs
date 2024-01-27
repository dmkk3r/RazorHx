using Microsoft.Extensions.DependencyInjection;

namespace RazorHx.Components.DependencyInjection;

public sealed class DefaultRazorComponentsBuilder(IServiceCollection services) : IRazorHxComponentsBuilder {
    public IServiceCollection Services { get; } = services;
}