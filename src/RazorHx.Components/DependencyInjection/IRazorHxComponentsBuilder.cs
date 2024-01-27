using Microsoft.Extensions.DependencyInjection;

namespace RazorHx.Components.DependencyInjection;

public interface IRazorHxComponentsBuilder {
    public IServiceCollection Services { get; }
}