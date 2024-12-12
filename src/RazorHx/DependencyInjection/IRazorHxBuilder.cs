using Microsoft.Extensions.DependencyInjection;

namespace RazorHx.DependencyInjection;

public interface IRazorHxBuilder
{
    public IServiceCollection Services { get; }
}